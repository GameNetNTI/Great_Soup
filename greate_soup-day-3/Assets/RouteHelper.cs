using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public static class RouteHelper
    {
        public static void Clear(List<Vector2> route)
        {
            if (route.Count < 3)
                return;

            for (var i = 0; i < route.Count - 2; i++)
            {
                var a = route[i];
                var b = route[i + 1];
                var c = route[i + 2];

                if (!IsOnLine(a, b, c))
                    continue;
                i--;
                route.RemoveAt(i + 1);
            }
        }

        public static void Shift(List<Vector2> route)
        {
            for (var i = 0; i < route.Count - 1; i++)
            {
                var a = route[i];
                var b = route[i + 1];
                var shift = GetShift(b - a);
                route[i] = a + shift;
                route[i + 1] = b + shift;
            }
        }

        private static bool IsOnLine(Vector2 a, Vector2 b, Vector2 c) =>
            Math.Abs(a.x - b.x) < .001f && Math.Abs(b.x - c.x) < .001f ||
            Math.Abs(a.y - b.y) < .001f && Math.Abs(b.y - c.y) < .001f;

        private static Vector2 GetShift(Vector2 dir)
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                return dir.x >= 0 ? Vector2.down : Vector2.up;
            return dir.y >= 0 ? Vector2.right : Vector2.left;
        }
    }
}