using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRot : MonoBehaviour
{
    private Vector3 point;
    private void Awake()
    {
        point = this.transform.position;
    }

    void FixedUpdate()
    {
        this.gameObject.transform.Rotate(0.0f, 2.0f, 0.0f, Space.World);
    }
}
