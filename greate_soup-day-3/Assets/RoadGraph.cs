using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class RoadGraph
    {
        public static RoadGraph Instance { get; private set; }

        public List<Vector2Int> Vertices = new List<Vector2Int>();
        public List<Edge> Edges = new List<Edge>();

        public RoadGraph()
        {
            Instance = this;
        }

        public int[][] Cached;
        public List<int>[] Connections;

        public int AddVertice(Vector2Int position)
        {
            var id = Vertices.Count;
            Vertices.Add(position);
            return id;
        }

        public void AddEdge(int from, int to)
        {
            Edges.Add(new Edge
            {
                index1 = from,
                index2 = to
            });
        }

        public void CacheDistances()
        {
            Cached = new int[Vertices.Count][];
            Connections = new List<int>[Vertices.Count];
            for (var i = 0; i < Cached.Length; i++)
            {
                Cached[i] = new int[Vertices.Count];
                Connections[i] = new List<int>();
            }

            FillCachedWith(-1);

            for (var i = 0; i < Edges.Count; i++)
            {
                var current = Edges[i];
                Connections[current.index1].Add(current.index2);
            }

            for (var i = 0; i < Vertices.Count; i++)
                CalculateDistance(i, 0, 0, new bool[Vertices.Count]);
        }

        public void GetRoute(int from, int to, List<int> buffer)
        {
            var used = new bool[Vertices.Count];
            var min = int.MaxValue;
            var current = from;
            var index = -1;
            var it = 0;
            while (current != to && it < 1000)
            {
                buffer.Add(current);
                used[current] = true;
                it++;
                for (var i = 0; i < Connections[current].Count; i++)
                {
                    var c = Connections[current][i];
                    if (c == to)
                    {
                        buffer.Add(to);
                        return;
                    }

                    if (used[c])
                        continue;

                    var dist = Cached[c][to];
                    if (dist < min && dist > -1)
                    {
                        min = dist;
                        index = c;
                    }
                }

                min = 1000000000;
                current = index;
            }
        }

        private void CalculateDistance(int from, int current, int currentDistance, bool[] used)
        {
            used[current] = true;
            Cached[from][current] = currentDistance;

            var cd = Connections[current];
            for (var i = 0; i < cd.Count; i++)
            {
                var target = cd[i];
                if (used[target])
                    continue;

                var dist = (int) Vector2Int.Distance(Vertices[from], Vertices[target]);
                var newDist = currentDistance + dist;
                CalculateDistance(from, target, newDist, used);
            }

            used[from] = false;
        }

        private void FillCachedWith(int value)
        {
            foreach (var arr in Cached)
                for (var i = 0; i < arr.Length; i++)
                    arr[i] = value;
        }

        public Vector3 GetPos(int index) => new Vector3(Vertices[index][0], 0, Vertices[index][1]);
    }

    public class Edge
    {
        public int index1;
        public int index2;
    }
}