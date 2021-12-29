using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IPickable
{
    [Range(0, 50)]
    [SerializeField] private int Value;
    public void OnTake()
    {
        this.gameObject.SetActive(false);
        Bank.instance.PluralIncreaseCoinNumb(Value);
        CoinManager.Instance.IncreaseCoinNumb(Value);
        TakeCoinTween.Instance.Move(this.transform.position); // Опционально 
    }
}
