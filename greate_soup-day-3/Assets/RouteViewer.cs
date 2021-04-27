using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class RouteViewer : MonoBehaviour
    {
        public static RouteViewer Instance;
        private List<Vector3> data;

        public void ShowRoute(List<Vector3> positions)
        {
            data = positions;
        }

        private void Awake()
        {
            Instance = this;
        }

        public void Stop()
        {
            data = null;
        }

        public void TestView1(int a)
        {
            data = new List<Vector3>();
            var buffer = new List<int>();
            buffer.Add(RoadIdKeeper.Instance.GetRealId(1));
            RoadGraph.Instance.GetRoute(RoadIdKeeper.Instance.GetRealId(1), RoadIdKeeper.Instance.GetRealId(2), buffer);
            foreach (var index in buffer) data.Add(RoadGraph.Instance.GetPos(index));
        }
        public void TestView2()
        {
            data = new List<Vector3>();
            var buffer = new List<int>();
            buffer.Add(RoadIdKeeper.Instance.GetRealId(2));
            RoadGraph.Instance.GetRoute(RoadIdKeeper.Instance.GetRealId(2), RoadIdKeeper.Instance.GetRealId(5), buffer);
            foreach (var index in buffer) data.Add(RoadGraph.Instance.GetPos(index));
        }

        private void OnDrawGizmos()
        {
            if (data == null)
                return;

            for (var i = 0; i < data.Count; i++)
            {
                Gizmos.DrawCube(data[i], Vector3.one);
                if (i < data.Count - 1)
                    Gizmos.DrawLine(data[i], data[i + 1]);
            }
        }
    }
}