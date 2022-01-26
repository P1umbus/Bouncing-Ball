using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OutCoinInText : MonoBehaviour
{
    [SerializeField] private Text _coinNubmerText;
    private void Awake()
    {
        GameEvent.ChangeCoinNumb += OutCoinNumber;
    }
    void Start()
    {
        Invoke(nameof(OutCoinNumber), 0.02f);
    }
    private void OutCoinNumber()
    {
        _coinNubmerText.text = DataLoadSystem.GetLoader<Bank>("1").GetCoin().ToString();
    }
    private void OnDestroy()
    {
        GameEvent.ChangeCoinNumb -= OutCoinNumber;
    }
}
