using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Vector3 direction;

    Rigidbody body;
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        body.MovePosition(body.position + direction * Time.deltaTime);
    }
}
