using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdButton : MonoBehaviour
{
    private string _placementId = "Rewarded_Android";

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.interactable = false;
    }

    private void Start()
    {
        AdsManager.Instance.AdLoaded.AddListener(AdLoaded);

        if (AdsManager.Instance.RewardedAdIsLoad == true)
            AdLoaded(_placementId);
    }

    private void AdLoaded(string placementId)
    {
        if (placementId == _placementId)
        {
            _button.interactable = true;
            _button.onClick.AddListener(ShowAd);
        }
    }

    private void ShowAd()
    {
        AdsManager.Instance.ShowAd(_placementId);
        _button.gameObject.SetActive(false);
    }
}
