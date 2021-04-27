using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class CarSpawner : MonoBehaviour
    {
        public Car Prefab;

        public void MoveFrom12()
        {
            var car = Instantiate(Prefab);
            var start = RoadGraph.Instance.GetPos(RoadIdKeeper.Instance.GetRealId(1));
            var buffer = new List<int>();
            RoadGraph.Instance.GetRoute(RoadIdKeeper.Instance.GetRealId(1), RoadIdKeeper.Instance.GetRealId(2), buffer);
            var data = new List<Vector2>();
            data.Add(new Vector2(start.x, start.z));
            foreach (var index in buffer)
            {
                var pos = RoadGraph.Instance.Vertices[index];
                data.Add(pos);
            }
            RouteHelper.Clear(data);
            RouteHelper.Shift(data);

            foreach (var v in data) Debug.DrawRay(new Vector3(v.x, 0, v.y), Vector3.up, Color.red, 5);
            car.transform.position = data[0];
            data.RemoveAt(0);
            car.Move(data, () => MoveCont(car));
            car.SetTargetSpeed(20);
            car.SetRotationHardToTarget();
        }

        public void MoveCont(Car car)
        {
            var buffer = new List<int>();
            RoadGraph.Instance.GetRoute(RoadIdKeeper.Instance.GetRealId(2), RoadIdKeeper.Instance.GetRealId(5), buffer);
            var data = new List<Vector2>();
            var start = RoadGraph.Instance.GetPos(RoadIdKeeper.Instance.GetRealId(2));
            data.Add(new Vector2(start.x, start.z));
            foreach (var index in buffer) data.Add(RoadGraph.Instance.Vertices[index]);
            RouteHelper.Clear(data);
            RouteHelper.Shift(data);
            foreach (var v in data) Debug.DrawRay(new Vector3(v.x, 0, v.y), Vector3.up, Color.red, 5);
            data.RemoveAt(0);
            car.Move(data);
            car.SetTargetSpeed(20);
        }

        public void MoveRandom()
        {
            var a = Random.Range(0, RoadGraph.Instance.Vertices.Count - 1);
            var b = Random.Range(0, RoadGraph.Instance.Vertices.Count - 1);

            var car = Instantiate(Prefab);
            var start = RoadGraph.Instance.GetPos(a);
            var buffer = new List<int>();
            RoadGraph.Instance.GetRoute(a, b, buffer);
            var data = new List<Vector2>();
            data.Add(new Vector2(start.x, start.z));
            foreach (var index in buffer)
            {
                var pos = RoadGraph.Instance.Vertices[index];
                data.Add(pos);
            }
            
            var toV = new List<Vector3>();
            foreach (var vector2 in data) toV.Add(new Vector3(vector2.x, 0, vector2.y));
            RouteViewer.Instance.ShowRoute(toV);
            
            RouteHelper.Clear(data);
            RouteHelper.Shift(data);

            foreach (var v in data) Debug.DrawRay(new Vector3(v.x, 0, v.y), Vector3.up, Color.red, 5);
            car.transform.position = start;
            data.RemoveAt(0);
            car.Move(data);
            car.SetTargetSpeed(20);
            car.SetRotationHardToTarget();
            
            
        }
    }
}