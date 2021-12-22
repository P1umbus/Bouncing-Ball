using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            GetDamaged();
        }
    }

    private void GetDamaged()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            Death();
        }
        else
        {



        }
    }

    private void Death()
    {
        
    }
}
