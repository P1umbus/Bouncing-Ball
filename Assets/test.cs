using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _i = 0;

    private void Update()
    {
        float interpolator = (transform.position.x - _points[_i].position.x) / (_points[_i + 1].position.x - _points[_i].position.x);
        float z = Mathf.Lerp(_points[_i].position.z, _points[_i + 1].position.z, interpolator);
        Vector3 vector = new Vector3(transform.position.x, transform.position.y, z);

        transform.position = vector;

        while (true)
        {
            if (transform.position.x > _points[_i + 1].position.x)
            {
                _i++;
            }
            else if (transform.position.x < _points[_i].position.x)
            {
                _i--;
            }
            else
            {
                return;
            }

        }

    }
}
