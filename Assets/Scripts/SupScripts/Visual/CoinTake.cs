using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTake : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject CoinUI;
    void Start()
    {
        LeanTween.move(Coin,CoinUI.transform.position,1.5f);
        LeanTween.scale(Coin, new Vector2(1f, 1f), 2f);
    }

    void Update()
    {

    }
}
