using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/Bank", fileName = "Bank")]
public class Bank : BaseDataLoader
{
    private string PPCoinName = Constants.PPname.CoinNumb;
    private int _coinNumb;

    public Bank()
    {
        Key = DataLoaders.Bank;
    }

    public override IEnumerator Init()
    {
        yield return new WaitForSeconds(0f);
        DataLoad();
    }
    public int GetCoin()
    {
        return _coinNumb;
    }
    public bool IsEnough(int namber)
    {
        if (_coinNumb >= namber)
        {
            return true;
        }
        return false;
    }
    public void PluralIncreaseCoinNumb(int coin)
    {
        if (coin >= 0)
        {
            _coinNumb += coin;
            SaveData();
            GameEvent.ChangeCoinNumb?.Invoke();
        }
        else
        {
            Debug.LogError("You can't plural increase the number of coins by a negative number ");
        }
    }
    public void ReduceCoinNumb(int coin)
    {
        if (IsEnough(coin) == true)
        {
            if (coin >= 0) 
            {
                _coinNumb -= coin;
                SaveData();
                GameEvent.ChangeCoinNumb?.Invoke();
            }
            else
            {
                Debug.LogError("You can't reduction the number of coins by a negative number ");
            }

        }
        else
        {
            Debug.LogError("You can't reduction coins , not enough coins");
        }
    }
    private void DataLoad()
    {
        if (PlayerPrefs.HasKey(PPCoinName))
        {
            _coinNumb = PlayerPrefs.GetInt(PPCoinName);
        }
        else
        {
            PlayerPrefs.SetInt(PPCoinName, 0);
            _coinNumb = PlayerPrefs.GetInt(PPCoinName);
        }
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(PPCoinName,_coinNumb);
    }

}
