using UnityEngine;

public class Coin : MonoBehaviour , IPickable
{
    private int _value = 1;

    public void OnTake()
    {
        this.gameObject.SetActive(false);
        DataLoadSystem.GetLoader<Bank>(DataLoaders.Bank).PluralIncreaseCoinNumb(_value);
        CoinManager.Instance.IncreaseCoinNumb(_value);
        TakeCoinTween.Instance.Move(this.transform.position);
    }
}
