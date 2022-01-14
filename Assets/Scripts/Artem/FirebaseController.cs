using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseController : MonoBehaviour
{
    private Firebase.FirebaseApp app;

    private void Start()
    {
        InitFB();
        Invoke(nameof(Crash), 10);
    }

    private void InitFB()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

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

    private void Crash()
    {
        throw new System.Exception("CRASH");
        Debug.Log("crash)");
    }
}
