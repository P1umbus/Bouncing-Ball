using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotation : MonoBehaviour
{
    private Transform _transform;
    private void Awake()
    {
       _transform = GetComponent<Transform>();
    }
    void Update()
    {
        _transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }
}
