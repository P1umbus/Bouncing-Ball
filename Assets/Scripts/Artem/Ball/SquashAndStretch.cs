using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SquashAndStretch : MonoBehaviour
{
    public bool IsGrounded => _isGrounded;

    [Header("Settings")]
    [SerializeField] private float _stretchMultiplier = 0.005f;
    [SerializeField] private float _squashMultiplier = 0.06f;
    [Range(0, 0.2f)]
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

    private Vector3 _savedContactNormal;
    private Vector3 _savedVelocity;

    private bool _inverted;
    private bool _isGrounded;
    private bool _isSquashDelay;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AddSaveVelocity(Vector3 vector)
    {
        _savedVelocity += vector;
    }

    private void LateUpdate()
    {
        if (_isGrounded == false)
            StretchCalculation();

        _currentScale = Mathf.Lerp(_currentScale, _targetScale, Time.deltaTime * _scaleChangeRate);
        if (_currentScale != 0)
            _scalableTransform.localScale = new Vector3(1 / _currentScale, _currentScale, 1 / _currentScale);

        Rotation();
    }

    private void StretchCalculation()
    {
        if (_rb.velocity.magnitude != 0)
            _targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.forward);

        float velocity = _rb.velocity.magnitude;
        _targetScale = 1f + velocity * velocity * _stretchMultiplier;
        _targetScale = Mathf.Clamp(_targetScale, 1f, 2f);
    }

    private void Rotation()
    {
        if (!_inverted && _currentScale >= 1f) 
        {
            _inverted = true;
            _rotatableTransform.rotation = _targetRotation = _currentRotation = Quaternion.LookRotation(_savedContactNormal, Vector3.forward);
        }

        _currentRotation = Quaternion.Lerp(_currentRotation, _targetRotation, Time.deltaTime * 10f); 
        _rotatableTransform.rotation = _currentRotation;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_isSquashDelay)
            return;

        if (collision.gameObject.layer == Constants.Layers.Trampoline)
        {
            if (collision.gameObject.TryGetComponent(out ITouching physicalImpactObject) == true)
                physicalImpactObject.OnTouch(_rb);
        }

        _savedContactNormal = collision.contacts[0].normal;

        if (_isPlayer == false)
            _rb.velocity *= 1.5f;
        _savedVelocity = _rb.velocity;

        SquashCalculation(collision);
    }

    private void SquashCalculation(Collision collision)
    {
        float velocityProjectionMagnitude = Vector3.Project(_savedVelocity, -_savedContactNormal).magnitude;
        float angleMultiplier = Mathf.Abs(Vector3.Angle(_savedVelocity, _savedContactNormal) - 90) / 100;

        if (velocityProjectionMagnitude < 5 || velocityProjectionMagnitude < _rb.velocity.magnitude * 0.5f)
            return;

        if (_isGrounded)
            return;

        _isGrounded = true;
        _isSquashDelay = true;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;

        _targetRotation = Quaternion.LookRotation(-collision.contacts[0].normal, Vector3.forward);
        _targetScale = Mathf.Lerp(1f, 0.3f, _savedVelocity.magnitude * _squashMultiplier);
        transform.position = collision.contacts[0].point + collision.contacts[0].normal * 0.5f;

        StartCoroutine(StartToSquash(angleMultiplier));
        StartCoroutine(DisableIsKinematic(angleMultiplier));
        StartCoroutine(SquashEnable());
    }

    private IEnumerator StartToSquash(float angleMultiplier)
    {
        yield return new WaitForSeconds(_delayMultiplier * angleMultiplier);
        _targetScale = Mathf.Lerp(0.5f, 1f, 1f + _savedVelocity.magnitude * _stretchMultiplier);
        _inverted = false;
    }

    private IEnumerator DisableIsKinematic(float angleMultiplier)
    {
        yield return new WaitForSeconds(_delayMultiplier * 1.5f * angleMultiplier);
        _rb.useGravity = true;
        yield return null;
        ExitSaveMode();
    }

    private void ExitSaveMode()
    {
        _rb.velocity = _savedVelocity;
        _isGrounded = false;
    }

    private IEnumerator SquashEnable()
    {
        yield return new WaitForSeconds(_delayBetweenSquash);
        _isSquashDelay = false;
    }    
}
