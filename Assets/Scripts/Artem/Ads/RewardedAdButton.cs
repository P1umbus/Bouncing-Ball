using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdButton : MonoBehaviour, IUnityAdsShowListener
{
    [SerializeField] private int _coinMultiplier = 2;

    [SerializeField] private string _placementId = "Rewarded_Android";

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.interactable = false;
    }

    private void Start()
    {
        AdsManager.AdLoadChange.AddListener(AdLoaded);

        AdLoaded(_placementId);
    }

    private void AdLoaded(string placementId)
    {
        if (placementId == _placementId)
        {
            if (AdsManager.RewardedAdIsLoad == true)
            {
                _button.interactable = true;
                _button.onClick.AddListener(ShowAd);
            }
        }
    }
    private void ShowAd()
    {
        Advertisement.Show(_placementId, this);

        AdsManager.AdIncluded(_placementId);
        _button.gameObject.SetActive(false);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_placementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            CoinManager.Instance.MultiplyCoin(_coinMultiplier);
            Debug.Log("Unity Ads Rewarded Ad Completed1");
        }
    }
}
