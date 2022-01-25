using UnityEngine;
using UnityEngine.EventSystems;

public class BallControl : MonoBehaviour, IDragHandler
{
    [Range(0.5f, 1.5f)]
    [SerializeField] private float _controlSensivity;

    [SerializeField] private float _limitVerticalBoostSpeed;

    [Header("Damage system")]
    [SerializeField] private CanvasGroup _redPanel;
    [SerializeField] private GameObject[] _hearts;

    private float _verticalBoostSpeed;

    private Player _player;
    private SquashAndStretch _sqAndStr;
    private Rigidbody _rb;

    private void Start()
    {
        _controlSensivity = PlayerPrefs.GetFloat(Constants.PPname.ControlSensivity);

        if (Player.Instance != null)
        {
            _player = Player.Instance;

            _rb = _player.RB;
            _player.Touch += OnTouch;

            if (_player.SqAndStr != null)
                _sqAndStr = _player.SqAndStr;

            if (_redPanel != null)
                _player.RedPanel = _redPanel;
            if (_hearts != null)
                _player.Hearts = _hearts;
        }
        else
        {
            Debug.LogWarning("Can't find a player");
        }

        FirebaseManager.Instance?.StartLevel();
    }

    private void OnTouch()
    {
        _verticalBoostSpeed = 0;
    }

    private void AddRandomTorque()
    {
        _rb.AddTorque(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        float screenDifferenceMultiplier = 2160 / (float)Screen.height; // 

        Vector3 vector = (Vector3)eventData.delta * Time.deltaTime * _controlSensivity * screenDifferenceMultiplier;

        if (_verticalBoostSpeed > _limitVerticalBoostSpeed)
        {
            if (vector.y > 0)
                vector.y = 0;
            else if (vector.y < 0)
                vector.y /= 2;
        }

        if (_player.IsGround == false)
            _verticalBoostSpeed += Mathf.Abs(vector.y);

        if (_sqAndStr?.IsGround == false)
            _rb.velocity += vector;
        else
            _sqAndStr.SavedVelocity += vector;

        AddRandomTorque();
    }

    private void OnDestroy()
    {
        _player.Touch -= OnTouch;
    }
}
