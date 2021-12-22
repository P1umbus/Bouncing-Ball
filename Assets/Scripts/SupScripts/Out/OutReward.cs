using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutReward : MonoBehaviour
{
    private Text NumbText;
    private void Awake()
    {
        NumbText = GetComponent<Text>();
    }
    private void Start()
    {
        OutRewardInText();
    }
    private void OutRewardInText()
    {
        NumbText.text = CoinManager.Instance.GetCoin().ToString();
    }



}
