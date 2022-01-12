using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance { get; private set; }

    public UnityEvent<string> AdLoaded;

    [Header("AdsInit")]
    [SerializeField] private string _androidGameId;
    [SerializeField] private bool _testMode;

    [Header("RewardedAds")]
    [SerializeField] private List<string> _adsUsed = new List<string>{ "Rewarded_Android" };
    public bool RewardedAdIsLoad = false;

    private string _adUnitId;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        Advertisement.Initialize(_androidGameId, _testMode);

        Invoke(nameof(LoadAds), 1);
    }

    private void LoadAds()
    {      
        foreach (string ad in _adsUsed)
        {
            LoadAd(ad);
        }
    }

    public void LoadAd(string adName)
    {
        // IMPORTANT! Only load content AFTER initialization.
        Debug.Log("Loading Ad: " + adName);
        Advertisement.Load(adName, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == "Rewarded_Android")
            RewardedAdIsLoad = true;

        AdLoaded.Invoke(placementId);
        Debug.Log("Ad Loaded: " + placementId);
    }

    public void ShowAd(string placementId)
    {
        if (placementId == "Rewarded_Android")
            RewardedAdIsLoad = false;

        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId) {}

    public void OnUnityAdsShowClick(string placementId) {}
}
