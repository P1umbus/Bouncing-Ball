using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NoPlayerBall : MonoBehaviour
{
    [SerializeField] private float _velocityY;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.up * _velocityY;
    }
}
