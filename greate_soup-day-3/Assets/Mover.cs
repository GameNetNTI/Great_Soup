using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 10;

    void Start()
    {
    }

    void Update()
    {
        var dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            dir += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            dir -= Vector3.forward;
        if (Input.GetKey(KeyCode.D))
            dir += Vector3.right;
        if (Input.GetKey(KeyCode.A))
            dir -= Vector3.right;
        if (Input.GetKey(KeyCode.Q))
            dir -= Vector3.down;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2;

        transform.position += dir * speed * Time.deltaTime;
    }
}