using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [HideInInspector] public static CoinManager Instance;
    public List<Coin> _Coin = new List<Coin>();
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
        Invoke("UpdateUI", 0.05f);
    }
    private void IncreaseCoinNumb()
    {
        CoinNamber++;
        OnCoinTake();
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
        float a = ((float)CoinNamber /(float)_Coin.Count)*100f;
        return a;
    }
    private void OnDestroy()
    {
        GameEvent.TakeCoin -= IncreaseCoinNumb;
    }
}
