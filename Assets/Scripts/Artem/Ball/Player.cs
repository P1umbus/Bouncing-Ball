using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    public Action Touch;

    [HideInInspector] public CanvasGroup RedPanel;
    [HideInInspector] public GameObject[] Hearts;
    [HideInInspector] public SquashAndStretch SqAndStr;
    [HideInInspector] public Rigidbody RB;
    //public bool IsGround;

    [Header("Damage Settings")]
    [SerializeField] private float _rednessDuration = 0.3f;
    [Range(0, 1)]
    [SerializeField] private float _rednessMaxAlpha = 0.7f;
    [SerializeField] private Canvas DeadScreen;
    [SerializeField] private Canvas MaineScreen;
    private AudioSource _audioSource;

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
        if (_isPlayer == true)
            Instance = this;

        _currentHealth = _maxHealth;
        _audioSource = GetComponent<AudioSource>();

        RB = GetComponent<Rigidbody>();

        TryGetComponent<SquashAndStretch>(out SqAndStr);

        ShowHearts();

        StartCoroutine(nameof(ImmunityTimer));
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

        Touch?.Invoke();
    }

    private void GetDamaged()
    {
        _currentHealth--;

        if (_currentHealth < 0)
        {
            Death();
        }
        else
        {
            _audioSource.Play();

            if (RedPanel != null)
            {
                LeanTween.alphaCanvas(RedPanel, _rednessMaxAlpha, _rednessDuration);
                LeanTween.alphaCanvas(RedPanel, 0, _rednessDuration).setDelay(_rednessDuration);
            }

            _currentImmunityTime = 0;

            StartCoroutine(nameof(ImmunityTimer));
        }

        ShowHearts();
    }

    private IEnumerator ImmunityTimer()
    {
        while(_currentImmunityTime < _immunityTime)
        {
            _currentImmunityTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void ShowHearts()
    {
        if (Hearts == null || Hearts.Length == 0)
            return;

        int heartsCount = _currentHealth;

        for (int i = 0; i < 3; i++)
        {
            if (heartsCount > 0)
                Hearts[i].SetActive(true);
            else
                Hearts[i].SetActive(false);

            heartsCount--;
        }
    }

    private void Death() //#######################################
    {
        MaineScreen.gameObject.SetActive(false);
        DeadScreen.gameObject.SetActive(true);
        Debug.Log("Death");
    }
}
