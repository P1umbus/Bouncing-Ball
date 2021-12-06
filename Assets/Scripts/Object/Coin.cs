using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void ContactWithBall()
    {
        GameEvent.TakeCoin?.Invoke();
        this.gameObject.SetActive(false);
    }
}
