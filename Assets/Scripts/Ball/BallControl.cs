using UnityEngine;
using UnityEngine.EventSystems;

public class BallControl : MonoBehaviour, IDragHandler
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Limiter _limiter;
    [Range(0.5f, 1.5f)]
    [SerializeField] private float _forceMagnitudeTouches;

    [SerializeField] private float _limitVerticalBoostSpeed;

    private float _verticalBoostSpeed; //!public

    private void Update()
    {
        if (_limiter.IsGround == true)
            _verticalBoostSpeed = 0;
    }

    private void AddRandomTorque()
    {
        _rb.AddTorque(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        float screenDifferenceMultiplier = 2160 / (float)Screen.height; // 

        Vector3 vector = (Vector3)eventData.delta * Time.deltaTime * _forceMagnitudeTouches * screenDifferenceMultiplier;

        if (_verticalBoostSpeed > _limitVerticalBoostSpeed)
            vector = new Vector3(vector.x, 0, 0);

        _verticalBoostSpeed += Mathf.Abs(vector.y);

        _rb.velocity += vector;

        AddRandomTorque();
    }
}
