using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _rotatableTransform;
    [SerializeField] private Transform _scalableTransform;

    [SerializeField] private float StretchMultiplier = 0.005f;
    [SerializeField] private float SquashMultiplier = 0.06f;
    [SerializeField] private float DelayMultiplier = 0.2f;
    [SerializeField] private float ScaleChangeRate = 20f;

    private Quaternion _targetRotation;
    private Quaternion _currentRotation;

    private float _currentScale = 1f;
    private float _targetScale = 1f;
    private Vector3 _savedVelocity;
    private Vector3 _savedContactNormal;
    private bool _ground = false;
    private bool _inverted;

    private bool _delay = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin) == true)
        {
            coin.ContactWithBall();
        }
    }
    private void LateUpdate()
    {
        if (_ground == false)
        {
            _targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.forward);
            float velocity = _rigidbody.velocity.magnitude;
            _targetScale = 1f + velocity * velocity * StretchMultiplier;
            _targetScale = Mathf.Clamp(_targetScale, 1f, 2f);
        }

        _currentScale = Mathf.Lerp(_currentScale, _targetScale, Time.deltaTime * ScaleChangeRate);

        SquashScale(_currentScale);

        if (!_inverted && _currentScale >= 1f)
        {
            _inverted = true;
            _rotatableTransform.rotation = _targetRotation = _currentRotation = Quaternion.LookRotation(_savedContactNormal, Vector3.forward);
        }

        _currentRotation = Quaternion.Lerp(_currentRotation, _targetRotation, Time.deltaTime * 10f);
        _rotatableTransform.rotation = _currentRotation;
    }

    private void SquashScale(float value)
    {
        if (value == 0f) return;
        _scalableTransform.localScale = new Vector3(1 / value, value, 1 / value);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_delay == true)
            return;

        _savedContactNormal = collision.contacts[0].normal;
        _savedVelocity = _rigidbody.velocity;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Trampoline")) //Trampoline
        {
            _savedVelocity *= 2;
            _rigidbody.velocity = _savedVelocity;
        }

        float velocityProjectionMagnitude = Vector3.Project(_savedVelocity, -_savedContactNormal).magnitude;
        float angleMultiplier = Mathf.Abs(Vector3.Angle(_savedVelocity, _savedContactNormal) - 90) / 100;

        if (velocityProjectionMagnitude < 5 || velocityProjectionMagnitude < _rigidbody.velocity.magnitude * 0.5f)
            return;

        if (_ground) 
            return;

        _ground = true;
        _delay = true;

        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;

        _targetRotation = Quaternion.LookRotation(-collision.contacts[0].normal, Vector3.forward);

        _targetScale = Mathf.Lerp(1f, 0.3f, _savedVelocity.magnitude * SquashMultiplier);

        float groundedTime = velocityProjectionMagnitude * DelayMultiplier;
        groundedTime = Mathf.Clamp(groundedTime, 0f, 0.15f);

        transform.position = collision.contacts[0].point + collision.contacts[0].normal * 0.5f;               

        Invoke("StartToStretch", groundedTime * angleMultiplier);
        Invoke("DisableIsKinematic", groundedTime * 1.5f * angleMultiplier);

        Invoke("StretchEnable", 0.4f);
    }

    private void StartToStretch()
    {
        _targetScale = Mathf.Lerp(0.5f, 1f, 1f + _savedVelocity.magnitude * StretchMultiplier);
        _inverted = false;
    }

    private void DisableIsKinematic()
    {
        _rigidbody.useGravity = true;

        Invoke("ExitSaveMode", 0.02f);
    }

    private void ExitSaveMode()
    {
        _ground = false;
        _rigidbody.velocity = _savedVelocity;
    }

    private void StretchEnable()
    {
        _delay = false;
    }
}
