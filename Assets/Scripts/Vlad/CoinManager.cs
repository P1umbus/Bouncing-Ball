using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [HideInInspector] public static CoinManager Instance;
    [SerializeField] private Text _coinNumbText;
    [SerializeField]private AudioSource _coinSound;
    private int _coinNumber;
    public int CoinNumber => _coinNumber;
    private bool _multiplyAbility = true;

    private void Awake()
    {
        Instance = this;
        _coinSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Invoke("UpdateUI", 0.05f);
    }
    public int GetCoin()
    {
        return _coinNumber;
    }
    public void MultiplyCoin(Vector3 pos,int Multiply)
    {
        if(_multiplyAbility == true)
        {
            int MultiplyNumb = _coinNumber * (Multiply - 1);
            DataLoadSystem.GetLoader<Bank>(DataLoaders.Bank).PluralIncreaseCoinNumb(MultiplyNumb);
            TakePluralCoinTween.Instance.ScreenMove(pos, MultiplyNumb);
            _coinNumber *= Multiply;
            GameEvent.MultiplyCoin?.Invoke();
            _multiplyAbility = false;
        }      
    }
    public void IncreaseCoinNumb(int Numb)
    {
        if (Numb >= 0)
        {
           
            _coinNumber += Numb;
            OnCoinTake();
        }
        else
        {
            Debug.LogError("Negative number of coins");
        }
        OnCoinTake();
    }
    private void OnCoinTake()
    {
        UpdateUI();
        PlayCoinSound();
    }
    private void UpdateUI()
    {
        _coinNumbText.text = _coinNumber.ToString();
    }
    private void PlayCoinSound()
    {
        _coinSound.Play();
    }
}
