using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour , IPickable
{
    private int value = 10;
    private void Start()
    {
        CoinManager.Instance._Coin.Add(this);
    }
    public void OnTake()
    {
        //GameEvent.TakeCoin?.Invoke();
        this.gameObject.SetActive(false);
        Bank.instance.PluralIncreaseCoinNumb(value);
        CoinManager.Instance.IncreaseCoinNumb(value);
        TakeCoinTween.Instance.Move(this.transform.position);
    }
}
