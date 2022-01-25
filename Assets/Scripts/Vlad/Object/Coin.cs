using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour , IPickable
{
    private int value = 1;

    public void OnTake()
    {
        this.gameObject.SetActive(false);
        Bank.instance.PluralIncreaseCoinNumb(value);
        CoinManager.Instance.IncreaseCoinNumb(value);
        TakeCoinTween.Instance.Move(this.transform.position);
    }
}
