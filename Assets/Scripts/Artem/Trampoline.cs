using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Trampoline : MonoBehaviour, ITouching
{
    [SerializeField] private float _speedMultiplier;

    private Animation _animation;

    public void OnTouch(Rigidbody rb)
    {
        rb.velocity *= _speedMultiplier;
        _animation.Play();
    }

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }
}
