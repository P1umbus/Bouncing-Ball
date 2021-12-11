using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SellSourse;
    [SerializeField] private AudioSource BuySourse;
    private void Awake()
    {
        GameEvent.SoundEvents.Shop.Sell += PlaySellMus;
        GameEvent.SoundEvents.Shop.Buy += PlayBuyMus;
    }

    private void PlaySellMus()
    {
        SellSourse.Play();
    }
    private void PlayBuyMus()
    {
        BuySourse.Play();
    }
}
