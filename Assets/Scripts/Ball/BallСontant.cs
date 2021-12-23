using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallСontant : MonoBehaviour
{
    private TakeCoinTween _takeCoinTween;
    private void Awake()
    {
        _takeCoinTween = FindObjectOfType<TakeCoinTween>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin) == true)
        {
            coin.ContactWithBall();
            _takeCoinTween.Move(coin.transform.position);
        }
    }
}
