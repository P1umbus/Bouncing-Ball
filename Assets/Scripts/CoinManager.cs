using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [HideInInspector] public static CoinManager Instance;
    public List<Coin> _Coin;
    [SerializeField] private Text CoinNumbText;
    [SerializeField]private AudioSource _CoinSound;
    private int CoinNamber;
    private float CollectedCoinsPercentage;

    private void Awake()
    {
        Instance = this;
        _CoinSound = GetComponent<AudioSource>();
        GameEvent.TakeCoin += IncreaseCoinNumb;
    }
    private void Start()
    { 
        UpdateUI();
    }
    private void IncreaseCoinNumb()
    {
        CoinNamber++;
        OnCoinTake();
        Debug.Log(CoinNamber);
    }
    private void OnCoinTake()
    {
        UpdateUI();
        PlayCoinSound();
    }
    private void UpdateUI()
    {
        CoinNumbText.text = CoinNamber.ToString() + "/" + _Coin.Count;
    }
    private void PlayCoinSound()
    {
        _CoinSound.Play();
    }

    public int GetCoin()
    {
        return CoinNamber;
    }
    public float GetCollectedCoinsPercentage()
    {
        return (CoinNamber/_Coin.Count);
    }
    private void OnDestroy()
    {
        GameEvent.TakeCoin -= IncreaseCoinNumb;
    }
}
