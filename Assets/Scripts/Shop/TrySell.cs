using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrySell : MonoBehaviour
{
    [SerializeField] private Text SellInfo;
    private ItemManager _ItemManager;    

    private void Awake()
    {
        GameEvent.TrySell += OnTrySell;
        this.gameObject.SetActive(false);
    }

    private void OnTrySell(ItemManager IM)
    {
        _ItemManager = IM;
        this.gameObject.SetActive(true);
        OutSellInfo();
       
    }
    public void Sell()
    {
        _ItemManager.Sell();
        this.gameObject.SetActive(false);
    }
    private void OutSellInfo()
    {
        SellInfo.text = "Sell this skin for " + _ItemManager.GetSellPrice() +" coins?";
    }

    private void OnDestroy()
    {
        GameEvent.TrySell -= OnTrySell;
    }
}
