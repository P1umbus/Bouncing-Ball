using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRot : MonoBehaviour
{
    //[SerializeField] private GameObject MaineBall;
    private Vector3 point;
    private void Awake()
    {
        point = this.transform.position;
    }

    void FixedUpdate()
    {
        //transform.Rotate(2.0f, 0, 0);
        //transform.RotateAround(point, Vector3.up, 2);
    }
}
