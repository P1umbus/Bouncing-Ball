using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[SelectionBase]
public class Creator : MonoBehaviour
{
    [Min(1)] 
    [SerializeField] private int _ount = 1;

    [SerializeField] private Vector3 _offset = Vector3.forward;
    [SerializeField] private Vector3 _rotation;

    private void Start()
    {
        if (Application.IsPlaying(this))
        {
            CorrectChildCount();
            Destroy(this);
        }
    }

    private void Update()
    {
        CorrectChildCount();
    }

    private void CorrectChildCount()
    {
        _ount = Mathf.Clamp(_ount, 1, 1000);

        if (transform.childCount < _ount)
        {
            for (int i = transform.childCount; i < _ount; i++)
            {
                Transform instantiate = Instantiate(transform.GetChild(0), transform);
                instantiate.localPosition += (_offset * i);

                if (_rotation != Vector3.zero)
                    instantiate.rotation *= Quaternion.Euler(_rotation * Random.Range(0, 3));
            }
        }
        else if (_ount < transform.childCount)
        {
            for (int i = transform.childCount - 1; i >= _ount; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
