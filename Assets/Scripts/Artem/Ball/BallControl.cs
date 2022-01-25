using UnityEngine;
using UnityEngine.EventSystems;

public class BallControl : MonoBehaviour, IDragHandler
{
    [Range(0.5f, 1.5f)]
    [SerializeField] private float _controlSensivity;
    [SerializeField] private float _limitVerticalBoostSpeed;
    [SerializeField] private float _torque;

    [SerializeField] private Player _player;

    private float _verticalBoostSpeed;

    private SquashAndStretch _sqAndStr;
    private Rigidbody _rb;

    private void Start()
    {
        _controlSensivity = PlayerPrefs.GetFloat(Constants.PPname.ControlSensivity);
        _rb = _player.RB;
        _player.Touch += OnTouch;

        if (_player.SqAndStr != null)
            _sqAndStr = _player.SqAndStr;

        FirebaseManager.Instance?.StartLevel();
    }

    private void OnTouch()
    {
        _verticalBoostSpeed = 0;
    }

    private void AddRandomTorque()
    {
        _rb.AddTorque(RandomFloatNumber(_torque), RandomFloatNumber(_torque), RandomFloatNumber(_torque));
    }

    private float RandomFloatNumber(float num)
    {
        return Random.Range(-num, num);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float screenDifferenceMultiplier = 2160 / (float)Screen.height;

        Vector3 vector = (Vector3)eventData.delta * Time.deltaTime * _controlSensivity * screenDifferenceMultiplier;

        if (_verticalBoostSpeed > _limitVerticalBoostSpeed)
        {
            if (vector.y > 0)
                vector.y = 0;
            else if (vector.y < 0)
                vector.y /= 2;
        }

        if (_player.IsGrounded == false)
            _verticalBoostSpeed += Mathf.Abs(vector.y);

        if (_sqAndStr?.IsGrounded == false)
            _rb.velocity += vector;
        else if (_sqAndStr != null)
            _sqAndStr.AddSaveVelocity(vector);

        AddRandomTorque();
    }

    private void OnDestroy()
    {
        _player.Touch -= OnTouch;
    }
}
