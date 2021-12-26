using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visual Settings")]
    [SerializeField] private float _animDuration = 0.3f;
    [Range(0, 1)]
    [SerializeField] private float _maxAlpha = 0.7f;
    [SerializeField] private CanvasGroup _redPanel;
    [SerializeField] private GameObject[] _hearts;

    [Header("Parameters")]
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    [SerializeField] private float _immunityTime;
    private float _currentImmunityTime;


    private void Awake()
    {
        _currentHealth = _maxHealth;
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
        if (_currentHealth <= 0)
        {
            Death();
        }
        else
        {
            LeanTween.alphaCanvas(_redPanel, _maxAlpha, _animDuration);
            LeanTween.alphaCanvas(_redPanel, 0, _animDuration).setDelay(_animDuration);

            Debug.Log(_currentHealth);

            _currentImmunityTime = 0;
        }

        _currentHealth--;
        ShowHearts();
    }

    private void ShowHearts()
    {
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

    private void Death()
    {
        Debug.Log("Death");
    }
}
