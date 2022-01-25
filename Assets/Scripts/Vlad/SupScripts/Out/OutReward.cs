using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutReward : MonoBehaviour
{
    private Text _numbText;
    private void Awake()
    {
        _numbText = GetComponent<Text>();
        GameEvent.MultiplyCoin += OutRewardInText;
    }
    private void Start()
    {
        OutRewardInText();
    }
    private void OutRewardInText()
    {
        _numbText.text = CoinManager.Instance.GetCoin().ToString();
    }
    private void OnDestroy()
    {
        GameEvent.MultiplyCoin -= OutRewardInText;
    }



}
