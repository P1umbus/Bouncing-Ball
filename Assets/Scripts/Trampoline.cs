using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("AnimationSetting")]
    [SerializeField] private float _rotationOnCollision;
    [SerializeField] private float _speedRotation;

    //[Header("PhysicsSetting")]
    //[SerializeField] private float _boostSpeed;

    private float _currentRotation; //

    private void Update()
    {
        if (_currentRotation > 0)
        {
            float rotation = _speedRotation;

            _currentRotation -= rotation;
            transform.Rotate(Vector3.up * rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _currentRotation = _rotationOnCollision;

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        //    collision.rigidbody.velocity += Vector3.up * _boostSpeed;
    }
}
