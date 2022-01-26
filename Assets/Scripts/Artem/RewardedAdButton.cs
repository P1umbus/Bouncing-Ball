using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdButton : MonoBehaviour, IUnityAdsShowListener
{
    [SerializeField] private int _coinMultiplier = 2;
    [SerializeField] private string _placementId = "Rewarded_Android";

    private Button _button;
    private Vector3 _pos;

    private AdsManager _adsManager;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _pos = transform.position;
        _button.interactable = false;
    }

    private void Start()
    {
        _adsManager = DataLoadSystem.GetLoader<AdsManager>(DataLoaders.AdsManager);
        AdsManager.AdLoadChange.AddListener(AdLoaded);
        AdLoaded(_placementId);
    }

    private void AdLoaded(string placementId)
    {        
        if (_placementId.Equals(placementId))
        {
            if (AdsManager.RewardedAdIsLoad)
            {
                _button.interactable = true;
                _button.onClick.AddListener(ShowAd);
            }
        }
    }
    private void ShowAd()
    {
        Advertisement.Show(_placementId, this);
        _adsManager.AdIncluded(_placementId);
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
            CoinManager.Instance.MultiplyCoin(_pos, _coinMultiplier);
            Debug.Log("Unity Ads Rewarded Ad Completed1");
        }
    }

    private void OnDestroy()
    {
        AdsManager.AdLoadChange.RemoveListener(AdLoaded);
        _button.onClick.RemoveListener(ShowAd);
    }
}
