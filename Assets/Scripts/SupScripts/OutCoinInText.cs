using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutCoinInText : MonoBehaviour
{
    [SerializeField] private Text CoinNubmerText;
    private void Awake()
    {
        GameEvent.ChangeCoinNumb += OutCoinNumber;
    }
    void Start()
    {
        OutCoinNumber();
    }
    private void OutCoinNumber()
    {
        CoinNubmerText.text = Bank.instance.GetCoin().ToString();
    }
    private void OnDestroy()
    {
        GameEvent.ChangeCoinNumb -= OutCoinNumber;
    }
}
