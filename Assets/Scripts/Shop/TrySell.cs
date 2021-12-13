using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrySell : MonoBehaviour
{
    [SerializeField] private Text SellInfo;
    private ShopScript _ShopScript;    

    private void Awake()
    {
        GameEvent.TrySell += OnTrySell;
        this.gameObject.SetActive(false);
    }
    private void OnTrySell(ShopScript SS)
    {
        _ShopScript = SS;
        this.gameObject.SetActive(true);
        OutSellInfo();
       
    }
    public void Sell()
    {
        _ShopScript.Sell();
        this.gameObject.SetActive(false);
    }
    private void OutSellInfo()
    {
        SellInfo.text = "Sell this skin for " + _ShopScript.GetSellPrice() +" coins?";
    }

    private void OnDestroy()
    {
        GameEvent.TrySell -= OnTrySell;
    }
}
