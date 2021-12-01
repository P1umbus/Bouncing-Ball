using UnityEngine;
using UnityEngine.EventSystems;

public class BallControl : MonoBehaviour, IDragHandler
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _forceMagnitudeButtons;
    [SerializeField] private float _forceMagnitudeTouches;

    [SerializeField] private float _maxBallSpeed;

    private float _testSpeed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (_rb.velocity.x > -5f)
            {
                _rb.velocity += -Vector3.right * Time.deltaTime * _forceMagnitudeButtons;
                AddRandomTorque();
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (_rb.velocity.x < 5f)
            {
                _rb.velocity += Vector3.right * Time.deltaTime * _forceMagnitudeButtons;
                AddRandomTorque();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (_rb.velocity.y < 0f)
            {
                _rb.velocity += -Vector3.up * Time.deltaTime * _forceMagnitudeButtons * .1f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.magnitude > _maxBallSpeed)
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxBallSpeed);
        }
    }

    private void AddRandomTorque()
    {
        _rb.AddTorque(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        float screenDifferenceMultiplier = 2160 / (float)Screen.height;

        Vector3 vector = (Vector3)eventData.delta * Time.deltaTime * _forceMagnitudeTouches * screenDifferenceMultiplier;

        _rb.velocity += vector;

        if (_testSpeed < _rb.velocity.magnitude)
        {
            _testSpeed = _rb.velocity.magnitude;
        }

        AddRandomTorque();
    }
}
