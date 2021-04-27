using UnityEngine;

namespace DefaultNamespace
{
    public class Crasher : MonoBehaviour
    {
        public MeshRenderer rend;
        public Material destroyMat;
        private void OnCollisionEnter(Collision other)
        {
            rend.material = destroyMat;
            Destroy(GetComponent<Car>());
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}