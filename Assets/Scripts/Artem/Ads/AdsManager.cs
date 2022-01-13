using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener
{
    public static AdsManager Instance { get; private set; }

    public static bool RewardedAdIsLoad { get; private set; } = false;

    public static UnityEvent<string> AdLoadChange = new UnityEvent<string>();

    [Header("AdsInit")]
    [SerializeField] private string _androidGameId;
    [SerializeField] private bool _testMode;

    [Header("AdsSettings")]
    [SerializeField] private List<string> _adsUsed = new List<string>{ "Rewarded_Android" };

    private string _adUnitId;

    public static void AdIncluded(string placementId)
    {
        if (placementId == "Rewarded_Android")
            RewardedAdIsLoad = false;

        Instance.LoadAd(placementId);

        AdLoadChange.Invoke(placementId);
    }

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

    private void LoadAd(string adName)
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

        AdLoadChange.Invoke(placementId);
        Debug.Log("Ad Loaded: " + placementId);
    }

    //public void ShowAd(string placementId)
    //{
    //    if (placementId == "Rewarded_Android")
    //        RewardedAdIsLoad = false;

    //    Advertisement.Show(placementId, this);
    //    AdLoadChange.Invoke(placementId);
    //}

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
    }
    public void OnUnityAdsShowClick(string placementId) {}
}
