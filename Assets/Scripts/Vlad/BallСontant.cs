using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallСontant : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPickable _object) == true)
        {
            _object.OnTake();
        }        
    }
}
