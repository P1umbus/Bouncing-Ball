using UnityEngine;
using UnityEngine.UI;

public class OutCoinInText : MonoBehaviour
{
    [SerializeField] private Text _coinNubmerText;
    private void Awake()
    {
        GameEvent.ChangeCoinNumb += OutCoinNumber;
    }
    void Start()
    {
        Invoke(nameof(OutCoinNumber), 0.02f);
    }
    private void OutCoinNumber()
    {
        _coinNubmerText.text = DataLoadSystem.GetLoader<Bank>(DataLoaders.Bank).GetCoin().ToString();
    }
    private void OnDestroy()
    {
        GameEvent.ChangeCoinNumb -= OutCoinNumber;
    }
}
