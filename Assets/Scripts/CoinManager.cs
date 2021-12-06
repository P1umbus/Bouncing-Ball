using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Text CoinNumbText;
    [SerializeField] private AudioSource _CoinSound;
    [SerializeField] private int MaxCoinNamber;
    private int CoinNamber;
   

    private void Awake()
    {
        _CoinSound= GetComponent<AudioSource>();
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
        CoinNumbText.text = CoinNamber.ToString() + "/" + MaxCoinNamber.ToString();
    }
    private void PlayCoinSound()
    {
        _CoinSound.Play();
    }

    public int GetCoin()
    {
        return CoinNamber;
    }
    private void OnDestroy()
    {
        GameEvent.TakeCoin -= IncreaseCoinNumb;
    }
}
