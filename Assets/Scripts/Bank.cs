using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private string PPCoinName;
    private int CoinNumb;
    private void Awake()
    {
        DataLoad();
        GameEvent.TakeCoin += IncreaseCoinNumb;   
    }
    public int GetCoin()
    {
        return CoinNumb;
    }
    private void DataLoad()
    {
        PPCoinName = "CoinNumb";
        if (PlayerPrefs.HasKey(PPCoinName))
        {
            CoinNumb = PlayerPrefs.GetInt(PPCoinName);
        }
        else
        {
            PlayerPrefs.SetInt(PPCoinName, 0);
            CoinNumb = PlayerPrefs.GetInt(PPCoinName);
        }
    }
    private void SavaData()
    {
        PlayerPrefs.SetInt(PPCoinName,CoinNumb);
    }

    private void IncreaseCoinNumb()
    {
        CoinNumb++;
        SavaData();
        Debug.Log(CoinNumb);
    }
    private void OnDestroy()
    {
        GameEvent.TakeCoin -= IncreaseCoinNumb;
    }

}
