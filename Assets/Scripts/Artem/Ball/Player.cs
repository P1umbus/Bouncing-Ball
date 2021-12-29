using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visual Settings")]
    [SerializeField] private float _animDuration = 0.3f;
    [Range(0, 1)]
    [SerializeField] private float _maxAlpha = 0.7f;
    [SerializeField] private CanvasGroup _redPanel;
    [SerializeField] private GameObject[] _hearts;
    [SerializeField] private Canvas DeadSceen;
    private AudioSource _audioSource;


    [Header("Parameters")]
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    [SerializeField] private float _immunityTime;
    private float _currentImmunityTime;


    private void Awake()
    {
        _currentHealth = _maxHealth;
        _audioSource = GetComponent<AudioSource>();
        ShowHearts();
    }

    private void Update()
    {
        if (_currentImmunityTime < _immunityTime)
            _currentImmunityTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap") && _currentImmunityTime >= _immunityTime)
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
        else
        {
            _audioSource.Play();
            LeanTween.alphaCanvas(_redPanel, _maxAlpha, _animDuration);
            LeanTween.alphaCanvas(_redPanel, 0, _animDuration).setDelay(_animDuration);

            Debug.Log(_currentHealth);

            _currentImmunityTime = 0;
        }
        ShowHearts();
    }

    private void ShowHearts()
    {
        if (_hearts == null || _hearts.Length == 0)
            return;

        int heartsCount = _currentHealth;

        for (int i = 0; i < 3; i++)
        {
            if (heartsCount > 0)
                _hearts[i].SetActive(true);
            else
                _hearts[i].SetActive(false);

            heartsCount--;
        }
    }

    private void Death() //#######################################
    {
        DeadSceen.gameObject.SetActive(true);
        Debug.Log("Death");
    }
}
