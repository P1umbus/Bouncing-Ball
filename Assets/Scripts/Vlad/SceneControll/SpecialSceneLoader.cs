using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialSceneLoader : MonoBehaviour
{
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
        if (Fader.instance.isFading)
        {
            return;
        }
        StartCoroutine(ILoadScene(sceneName));     
    }

    private IEnumerator ILoadScene(string sceneName)
    {
        var waitFading = true;
        Fader.instance.FadeStart(() => waitFading = false);

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
        //ChangeProgress.Invoke(asyns.progress);
        asyns.allowSceneActivation = true;
        Fader.instance.FadeEnd(() => waitFading = false);

        while (waitFading)
        {
            yield return null;
        }
    }
}
