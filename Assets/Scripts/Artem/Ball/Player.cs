using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public Action Touch;

    [HideInInspector] public SquashAndStretch SqAndStr;
    [HideInInspector] public Rigidbody RB;

    public bool IsGrounded => _isGrounded;
    private bool _isGrounded;

    [Header("Damage Settings")]
    [SerializeField] private UIDamageSystem _uiDamageSystem;
    [SerializeField] private MeshRenderer _immunitySphere;
    private AudioSource _audioSource;

    private float _currentImmunitySphereAmount; 

    [Header("Parameters")]
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    [SerializeField] private float _immunityTime;
    private float _currentImmunityTime;

    [SerializeField] private float _maxBallSpeed;
    [SerializeField] private float _maxBallHorizontalSpeed;

    [SerializeField] private bool _isPlayer;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _audioSource = GetComponent<AudioSource>();

        RB = GetComponent<Rigidbody>();

        TryGetComponent(out SqAndStr);

        _uiDamageSystem?.ShowHearts(_currentHealth);

        _currentImmunityTime = _immunityTime;
    }

    private void Update()
    {
        if (RB.velocity.magnitude > _maxBallSpeed)
            RB.velocity = Vector3.ClampMagnitude(RB.velocity, _maxBallSpeed);

        if (RB.velocity.x > _maxBallHorizontalSpeed || RB.velocity.x < -_maxBallHorizontalSpeed)
            RB.velocity = new Vector3(Mathf.Clamp(RB.velocity.x, -_maxBallHorizontalSpeed, _maxBallHorizontalSpeed), RB.velocity.y, RB.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap") && _currentImmunityTime >= _immunityTime)
        {
            GetDamaged();
            Handheld.Vibrate();
        }

        _isGrounded = true;

        Touch?.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void GetDamaged()
    {
        _currentHealth--;

        if (_currentHealth < 0)
        {
            _uiDamageSystem.Death();
        }
        else
        {
            _audioSource.Play();

            _uiDamageSystem.GetDamaged();

            _currentImmunityTime = 0;

            _currentImmunitySphereAmount = 0;

            _immunitySphere.gameObject.SetActive(true);
            _immunitySphere.material.SetFloat(Constants.ImmunitySphereAmount, _currentImmunitySphereAmount);

            StartCoroutine(ImmunityTimer());            
        }

        _uiDamageSystem.ShowHearts(_currentHealth);
    }

    private IEnumerator ImmunityTimer()
    {
        while(_currentImmunityTime < _immunityTime)
        {
            _currentImmunityTime += Time.deltaTime;

            _currentImmunitySphereAmount = Mathf.Lerp(_currentImmunitySphereAmount, 1, Time.deltaTime / _immunityTime);
            _immunitySphere.material.SetFloat(Constants.ImmunitySphereAmount, _currentImmunitySphereAmount);

            yield return null;
        }

        if (_currentImmunityTime > _immunityTime)
        {
            _currentImmunitySphereAmount = 1;
            _immunitySphere.material.SetFloat(Constants.ImmunitySphereAmount, _currentImmunitySphereAmount);
            _immunitySphere.gameObject.SetActive(false);
        }
    }
}
