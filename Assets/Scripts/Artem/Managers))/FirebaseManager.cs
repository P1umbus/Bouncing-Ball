using Firebase.Analytics;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/FirebaseManager", fileName = "FirebaseManager")]
public class FirebaseManager : BaseDataLoader
{
    private Firebase.FirebaseApp _app;

    public FirebaseManager()
    {
        Key = DataLoaders.FirebaseManager;
    }

    public override IEnumerator Init()
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

        yield return null;
    }

    private void GetLevelNumber()
    {
        Debug.Log(PlayerPrefs.GetInt(Constants.PPname.NumbActiveLevel));
    }

    public void StartLevel()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
            new Parameter(FirebaseAnalytics.ParameterLevelName, PlayerPrefs.GetInt(Constants.PPname.NumbActiveLevel)));
    }

    public void EndLevel(float timeCompleteLevel)
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd,
            new Parameter(FirebaseAnalytics.ParameterLevelName, PlayerPrefs.GetInt(Constants.PPname.NumbActiveLevel)),
            new Parameter("timeCompleteLevel", timeCompleteLevel),
            new Parameter("coins received", CoinManager.Instance.CoinNumber));
    }
}
