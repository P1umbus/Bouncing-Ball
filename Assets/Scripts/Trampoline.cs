using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Trampoline : MonoBehaviour
{
    //[Header("AnimationSetting")]
    //[SerializeField] private float _rotationOnCollision;
    //[SerializeField] private float _speedRotation;
    private Animation _animation;
      
    //private float _currentRotation; //

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    //private void Update()
    //{
    //    if (_currentRotation > 0)
    //    {
    //        float rotation = _speedRotation;

    //        _currentRotation -= rotation;
    //        transform.Rotate(Vector3.up * rotation);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //_currentRotation = _rotationOnCollision;
        _animation.Play();
    }
}
