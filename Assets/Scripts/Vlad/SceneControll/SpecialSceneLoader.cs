using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialSceneLoader : MonoBehaviour
{
    private bool isLoading = false;

    public static SpecialSceneLoader instace;
    public Action<float> ChangeProgress;
    private void Awake()
    {
        if(instace != null)
        {
            Destroy(gameObject);
            return;
        }
        instace = this;
        DontDestroyOnLoad(gameObject);
    }
   
    public void LoadScene(string sceneName)
    {
        if (Fader.instanse.isFading)
        {
            return;
        }
        StartCoroutine(ILoadScene(sceneName));     
    }

    private IEnumerator ILoadScene(string sceneName)
    {
        isLoading = true;
        var waitFading = true;
        Fader.instanse.FadeStart(() => waitFading = false);

        while (waitFading)
        {
            yield return null;
        }

        var asyns = SceneManager.LoadSceneAsync(sceneName);
        asyns.allowSceneActivation = false;

        while(asyns.progress < 0.9f)
        {
            ChangeProgress.Invoke(asyns.progress);
            yield return null;
        }

        asyns.allowSceneActivation = true;
        Fader.instanse.FadeEnd(() => waitFading = false);

        while (waitFading)
        {
            yield return null;
        }

        isLoading = false;
    }

}
