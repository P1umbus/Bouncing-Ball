using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Limiter : MonoBehaviour
{
    [SerializeField] private float _maxBallSpeed;
    [SerializeField] private float _maxBallHorizontalSpeed;
    private Rigidbody _rb;

    public bool IsGround;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_rb.velocity.magnitude > _maxBallSpeed)
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxBallSpeed);
        }

        if (_rb.velocity.x > _maxBallHorizontalSpeed || _rb.velocity.x < -_maxBallHorizontalSpeed)
        {
            _rb.velocity = new Vector3(Mathf.Clamp(_rb.velocity.x, -_maxBallHorizontalSpeed, _maxBallHorizontalSpeed), _rb.velocity.y, _rb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGround = false;
    }
}
