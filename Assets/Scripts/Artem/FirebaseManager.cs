using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    private Firebase.FirebaseApp _app;

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

        InitFB();
    }

    private void InitFB()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                _app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void GetLevelNumber()
    {
        Debug.Log(PlayerPrefs.GetInt(Constants.NumbActiveLevel));
    }

    public void StartLevel()
    {
        Debug.Log(PlayerPrefs.GetInt(Constants.NumbActiveLevel));

        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
            new Parameter(FirebaseAnalytics.ParameterLevelName, PlayerPrefs.GetInt(Constants.NumbActiveLevel)));
    }

    public void EndLevel(float timeCompleteLevel)
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd,
            new Parameter(FirebaseAnalytics.ParameterLevelName, PlayerPrefs.GetInt(Constants.NumbActiveLevel)),
            new Parameter("timeCompleteLevel", timeCompleteLevel),
            new Parameter("coins received", CoinManager.Instance.CoinNamber));

        Debug.Log(timeCompleteLevel);
        Debug.Log(CoinManager.Instance.CoinNamber);
    }
}
