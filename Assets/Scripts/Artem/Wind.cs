using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float _force;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Rigidbody rb = other.attachedRigidbody;

            rb.AddForce(Vector3.up * _force * Time.deltaTime);
        }
    }
}
