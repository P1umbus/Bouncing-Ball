using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sellSourse;
    [SerializeField] private AudioSource _buySourse;
    private void Awake()
    {
        GameEvent.SoundEvents.Shop.Sell += PlaySellMus;
        GameEvent.SoundEvents.Shop.Buy += PlayBuyMus;
    }

    private void PlaySellMus()
    {
        _sellSourse.Play();
    }
    private void PlayBuyMus()
    {
        _buySourse.Play();
    }

    private void OnDestroy()
    {
        GameEvent.SoundEvents.Shop.Sell -= PlaySellMus;
        GameEvent.SoundEvents.Shop.Buy -= PlayBuyMus;
    }
}
