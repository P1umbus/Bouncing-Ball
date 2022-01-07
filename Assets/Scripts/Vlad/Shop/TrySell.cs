using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;

public class TrySell : MonoBehaviour
{
    private LeanLocalToken SellInfo;
    private ItemManager _ItemManager;    

    private void Awake()
    {
        GameEvent.TrySell += OnTrySell;
        SellInfo = GetComponentInChildren<LeanLocalToken>();
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
        SellInfo.SetValue(_ItemManager.GetSellPrice());
    }

    private void OnDestroy()
    {
        GameEvent.TrySell -= OnTrySell;
    }
}
