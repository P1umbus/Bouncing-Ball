using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        CoinManager.Instance._Coin.Add(this);
    }
    public void ContactWithBall()
    {
        GameEvent.TakeCoin?.Invoke();
        this.gameObject.SetActive(false);
    }
}
