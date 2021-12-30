using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SquashAndStretch : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _stretchMultiplier = 0.005f;
    [SerializeField] private float _squashMultiplier = 0.06f;
    [SerializeField] private float _delayMultiplier = 0.2f;
    [SerializeField] private float _scaleChangeRate = 20f;

    [SerializeField] private float _delayBetweenSquash = 0.4f;

    [SerializeField] private bool _isPlayer;

    [Header("Details")]
    [SerializeField] private Transform _rotatableTransform;
    [SerializeField] private Transform _scalableTransform;

    private Rigidbody _rb;
    private Quaternion _targetRotation;
    private Quaternion _currentRotation;

    private float _currentScale = 1f;
    private float _targetScale = 1f;
    private Vector3 _savedVelocity;
    private Vector3 _savedContactNormal;
    private bool _isGround = false;
    private bool _inverted;

    private bool _isSquashDelay = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (_isGround == false) 
        {
            if (_rb.velocity.magnitude != 0) //get the direction of movement
                _targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.forward);

            float velocity = _rb.velocity.magnitude; //stretch calculation
            _targetScale = 1f + velocity * velocity * _stretchMultiplier;
            _targetScale = Mathf.Clamp(_targetScale, 1f, 2f);
        }

        _currentScale = Mathf.Lerp(_currentScale, _targetScale, Time.deltaTime * _scaleChangeRate); //stretching
        if (_currentScale != 0)
            _scalableTransform.localScale = new Vector3(1 / _currentScale, _currentScale, 1 / _currentScale);

        if (!_inverted && _currentScale >= 1f) //turn after falling
        {
            _inverted = true;
            _rotatableTransform.rotation = _targetRotation = _currentRotation = Quaternion.LookRotation(_savedContactNormal, Vector3.forward);
        }

        _currentRotation = Quaternion.Lerp(_currentRotation, _targetRotation, Time.deltaTime * 10f); //rotation in the direction of travel
        _rotatableTransform.rotation = _currentRotation;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_isSquashDelay == true)
            return;

        _savedContactNormal = collision.contacts[0].normal;
        _savedVelocity = _rb.velocity;

        if (_isPlayer == false)
        {
            _savedVelocity *= 1.7F;
            _rb.velocity = _savedVelocity;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Trampoline")) //Trampoline
        {
            _savedVelocity *= 2;
            _rb.velocity = _savedVelocity;
        }

        float velocityProjectionMagnitude = Vector3.Project(_savedVelocity, -_savedContactNormal).magnitude;
        float angleMultiplier = Mathf.Abs(Vector3.Angle(_savedVelocity, _savedContactNormal) - 90) / 100;

        if (velocityProjectionMagnitude < 5 || velocityProjectionMagnitude < _rb.velocity.magnitude * 0.5f)
            return;

        if (_isGround) 
            return;

        _isGround = true;
        _isSquashDelay = true;

        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;

        _targetRotation = Quaternion.LookRotation(-collision.contacts[0].normal, Vector3.forward);

        _targetScale = Mathf.Lerp(1f, 0.3f, _savedVelocity.magnitude * _squashMultiplier);

        float groundedTime = velocityProjectionMagnitude * _delayMultiplier;
        groundedTime = Mathf.Clamp(groundedTime, 0f, 0.15f);

        transform.position = collision.contacts[0].point + collision.contacts[0].normal * 0.5f;               

        Invoke(nameof(StartToStretch), groundedTime * angleMultiplier);
        Invoke(nameof(DisableIsKinematic), groundedTime * 1.5f * angleMultiplier);

        Invoke(nameof(SquashEnable), _delayBetweenSquash);
    }

    private void StartToStretch()
    {
        _targetScale = Mathf.Lerp(0.5f, 1f, 1f + _savedVelocity.magnitude * _stretchMultiplier);
        _inverted = false;
    }

    private void DisableIsKinematic()
    {
        _rb.useGravity = true;

        Invoke(nameof(ExitSaveMode), 0.02f);
    }

    private void ExitSaveMode()
    {
        _rb.velocity = _savedVelocity;
        _isGround = false;
    }

    private void SquashEnable()
    {
        _isSquashDelay = false;
    }
}
