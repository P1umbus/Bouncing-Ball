using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gem : MonoBehaviour, IPickable
{
    [Range(0, 50)]
    [SerializeField] private int _value;
    public void OnTake()
    {
        this.gameObject.SetActive(false);
        Bank.instance.PluralIncreaseCoinNumb(_value);
        CoinManager.Instance.IncreaseCoinNumb(_value);
        TakeCoinTween.Instance.Move(this.transform.position); // Optional
    }
}
