using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank instance;
    private string PPCoinName;
    private int CoinNumb;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    private void Start()
    {
        DataLoad();
        GameEvent.TakeCoin += IncreaseCoinNumb;
        CoinNumb += 40;
    }
    public int GetCoin()
    {
        return CoinNumb;
    }
    public bool IsEnough(int namber)
    {
        if (CoinNumb >= namber)
        {
            return true;
        }
        return false;
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
    public void PluralIncreaseCoinNumb(int coin)
    {
        if (coin >= 0)
        {
            CoinNumb += coin;
            SavaData();
            GameEvent.ChangeCoinNumb?.Invoke();
        }
        else
        {
            Debug.LogError("You can't plural increase the number of coins by a negative number ");
        }
    }
    public void ReduceCoinNumb(int coin)
    {
        if(coin >= 0)
        {
            CoinNumb -= coin;
            SavaData();
            GameEvent.ChangeCoinNumb?.Invoke();
        }
        else
        {
            Debug.LogError("You can't reduction the number of coins by a negative number ");
        }
    }
    private void OnDestroy()
    {
        GameEvent.TakeCoin -= IncreaseCoinNumb;
    }

}
