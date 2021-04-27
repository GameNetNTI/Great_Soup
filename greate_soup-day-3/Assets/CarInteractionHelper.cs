using UnityEngine;

namespace DefaultNamespace
{
    public static class CarInteractionHelper
    {
        public static bool NeedToStop(Car a)
        {
            var posA = new Vector3(a.Position.x, 1f, a.Position.y);
            var normal = a.MovingVector.normalized;
            var dirA = new Vector3(normal.x, 0, normal.y);

            posA += dirA * 1.5f;
            Debug.DrawRay(posA, dirA * 3);
            
            return Physics.Raycast(new Ray(posA, dirA), 1.5f);
        }
    }
}