using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Anim Settings")]
    [SerializeField] private CanvasGroup _redPanel;
    [SerializeField] private float _animDuration = 0.3f;
    [Range(0, 1)]
    [SerializeField] private float _maxAlpha = 0.7f;

    [Header("Other")]
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    [SerializeField] private float _immunityTime;
    private float _currentImmunityTime;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_currentImmunityTime < _immunityTime)
            _currentImmunityTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            GetDamaged();
            Handheld.Vibrate();
        }
    }

    private void GetDamaged()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            Death();
        }
        else if(_currentImmunityTime >= _immunityTime)
        {
            LeanTween.alphaCanvas(_redPanel, _maxAlpha, _animDuration);
            LeanTween.alphaCanvas(_redPanel, 0, _animDuration).setDelay(_animDuration);

            Debug.Log(_currentHealth);

            _currentImmunityTime = 0;
        }


    }

    private void Death()
    {
        Debug.Log("Death");
    }
}
