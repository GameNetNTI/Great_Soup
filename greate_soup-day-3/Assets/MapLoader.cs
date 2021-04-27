using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace DefaultNamespace
{
    public class MapLoader : MonoBehaviour
    {
        private int _index;
        private int Index => _index++;
        public GameObject visualizer;

        private void Start()
        {
            LoadTest();
        }

        public void LoadTest() => Load(@"C:/test" + Index + ".json");

        public void Load(string path)
        {
            var c = transform.childCount;
            for (var i = 0; i < c; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            var model = LoadModel(path);
            CreateBuilding(model.Map.buildings);
            LoadRoads(model.Map.road);
            LoadSigns(model.Map.signs);
            RoadGraph.Instance.CacheDistances();
        }

        private MapJsonModel LoadModel(string path)
        {
            var serializer = new JsonSerializer();
            using (var stream = File.OpenText(path))
            using (var file = new JsonTextReader(stream))
                return serializer.Deserialize<MapJsonModel>(file);
        }

        void CreateBuilding(BuildingJson[] models)
        {
            foreach (var model in models)
            {
                var pos = new Vector3(model.position[0], 0, model.position[1]);
                var gm = CreatePa(Vector3.zero,
                    new Vector3(model.size[0], 10, model.size[1]));
                gm.transform.position = pos;
                gm.transform.SetParent(transform);
                Camera.main.transform.position = new Vector3(pos.x, 40, pos.z);
            }
        }

        void LoadRoads(RoadJson model)
        {
            var graph = new RoadGraph();
            var keeper = new RoadIdKeeper();
            foreach (var vertex in model.vertexes)
            {
                var id = vertex.id;
                keeper.Add(id, graph.AddVertice(new Vector2Int(vertex.position[0], vertex.position[1])));
            }

            foreach (var edge in model.edges)
            {
                var from = keeper.GetRealId(edge.from);
                var to = keeper.GetRealId(edge.to);
                graph.AddEdge(from, to);

                var startPos = graph.Vertices[from];
                var endPos = graph.Vertices[to];
                var d = endPos - startPos;
                Vector2 dir = endPos - startPos;
                var rot = Quaternion.Euler(0, 0, 90) * dir.normalized * 2;
                var pos = new Vector3(startPos.x, 0, startPos.y);
                var gm = CreatePa(Vector3.zero,
                    new Vector3(d.x + rot.x, .05f, d.y + rot.y));
                gm.transform.position = pos;
                gm.transform.SetParent(transform);
            }
        }

        void LoadSigns(SignJson[] signs)
        {
            foreach (var model in signs)
            {
                var gm = CreatePa(new Vector3(-.05f, 0, -.05f), new Vector3(.05f, 2, .05f));
                gm.transform.position = new Vector3(model.position[0], 0, model.position[1]);
                gm.transform.SetParent(transform);
            }
        }

        private GameObject CreatePa(Vector3 start, Vector3 end)
        {
            var obj = Instantiate(visualizer);
            var filter = obj.GetComponent<MeshFilter>();
            filter.mesh = Mesh(start, end);
            return obj;
        }

        private Mesh Mesh(Vector3 start, Vector3 end)
        {
            var x1 = start.x;
            var y1 = start.y;
            var z1 = start.z;

            var x2 = end.x;
            var y2 = end.y;
            var z2 = end.z;

            var mesh = new Mesh();
            var listPos = new List<Vector3>();
            var tris = new List<int>();

            var v1 = new Vector3(x1, y1, z1);
            var v2 = new Vector3(x1, y2, z1);
            var v3 = new Vector3(x2, y2, z1);
            var v4 = new Vector3(x2, y1, z1);
            var v5 = new Vector3(x1, y1, z2);
            var v6 = new Vector3(x1, y2, z2);
            var v7 = new Vector3(x2, y2, z2);
            var v8 = new Vector3(x2, y1, z2);
            listPos.AddRange(new[] {v1, v2, v3, v4, v5, v6, v7, v8});
            tris.AddRange(new[]
            {
                0, 1, 2, 0, 2, 3, 4, 5, 1, 4, 1, 0, 2, 6, 7, 2, 7, 3, 6, 5, 4, 6, 4, 7, 1, 5, 6, 1, 6, 2, 0, 3, 4, 3, 7,
                4
            });
            //AddQuad(new Vector3(x1, y1, z2), new Vector3(x2, y2, z2), listPos, tris, true);
            //AddQuad(new Vector3(x1, y1, z1), new Vector3(x1, y2, z2), listPos, tris, false);
            //AddQuad(new Vector3(x2, y1, z1), new Vector3(x2, y2, z2), listPos, tris, true);
            //AddQuad(new Vector3(x1, y1, z1), new Vector3(x2, y1, z2), listPos, tris, false);
            //AddQuad(new Vector3(x1, y2, z1), new Vector3(x2, y2, z2), listPos, tris, true);
            mesh.SetVertices(listPos);
            mesh.SetTriangles(tris, 0);
            var normal = new Vector3[listPos.Count];
            for (var i = 0; i < normal.Length; i++)
                normal[i] = Vector3.back;

            mesh.SetNormals(normal);
            return mesh;
        }

        private void AddQuad(Vector3 start, Vector3 end, List<Vector3> pos, List<int> triangles, bool inverted)
        {
            var v2 = new Vector3(start.x, end.y);
            var v4 = new Vector3(start.y, end.x);

            var startIndex = pos.Count;
            if (!inverted)
                pos.AddRange(new[] {start, v2, end, v4});
            else
                pos.AddRange(new[] {start, v4, end, v2});

            triangles.AddRange(new[]
                {startIndex, startIndex + 1, startIndex + 2, startIndex, startIndex + 2, startIndex + 3});
        }
    }
}