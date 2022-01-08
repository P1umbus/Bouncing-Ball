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
    private int CoinNamber;
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
        return CoinNamber;
    }
    public void MultiplyCoin(int Multiply)
    {
        if(MultiplyAbility == true)
        {
            Bank.instance.PluralIncreaseCoinNumb(CoinNamber * (Multiply-1));
            CoinNamber *= Multiply;
            GameEvent.MultiplyCoin?.Invoke();
            MultiplyAbility = false;
        }      
    }
    public void IncreaseCoinNumb(int Numb)
    {
        if (Numb >= 0)
        {
           
            CoinNamber += Numb;
            Debug.Log(CoinNamber);
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
        CoinNumbText.text = CoinNamber.ToString();
    }
    private void PlayCoinSound()
    {
        _CoinSound.Play();
    }
}
