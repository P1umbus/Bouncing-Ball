using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTake : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject CoinUI;
    void Start()
    {
        LeanTween.move(Coin,CoinUI.transform.position,1f);
        LeanTween.scale(Coin, new Vector2(0.4f, 0.4f), 1f);
    }

    void Update()
    {

    }
}
