using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class CoinManager : MonoBehaviour
{
    [HideInInspector] public static CoinManager Instance;
    public List<Coin> _Coin = new List<Coin>();
    [SerializeField] private Text CoinNumbText;
    [SerializeField]private AudioSource _CoinSound;
    private int _coinNamber;
    public int CoinNamber => _coinNamber;
    private float CollectedCoinsPercentage;
    private bool MultiplyAbility = true;

    private void Awake()
    {
        Instance = this;
        _CoinSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Invoke("UpdateUI", 0.05f);
    }
    public int GetCoin()
    {
        return _coinNamber;
    }
    public void MultiplyCoin(Vector3 pos,int Multiply)
    {
        if(MultiplyAbility == true)
        {
            int MultiplyNumb = _coinNamber * (Multiply - 1);
            Bank.instance.PluralIncreaseCoinNumb(MultiplyNumb);
            TakePluralCoinTween.Instance.ScreenMove(pos, MultiplyNumb);
            _coinNamber *= Multiply;
            GameEvent.MultiplyCoin?.Invoke();
            MultiplyAbility = false;
        }      
    }
    public void IncreaseCoinNumb(int Numb)
    {
        if (Numb >= 0)
        {
           
            _coinNamber += Numb;
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
        CoinNumbText.text = _coinNamber.ToString();
    }
    private void PlayCoinSound()
    {
        _CoinSound.Play();
    }
}
