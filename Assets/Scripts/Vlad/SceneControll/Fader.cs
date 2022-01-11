using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class Fader : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LeanLocalToken LoadingPercent;
    [SerializeField] private Image LoadingProgressBar;
    private static Fader _instanse;
    private const string FADER_PATH = "Fader";

    public bool isFading { get; private set;}

    public static Fader instanse
    {
        get
        {
            if(_instanse == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instanse = Instantiate(prefab);
                DontDestroyOnLoad(_instanse.gameObject);
            }
            return _instanse;
        }
    }

    private Action _faderStartCallBack;
    private Action _faderEndCallBack;

    public void FadeStart( Action faderStartCallBack)
    {
        if(isFading)
        {
            return;
        }
        SpecialSceneLoader.instace.ChangeProgress += UpdateLoadingPercent;
        SpecialSceneLoader.instace.ChangeProgress += UpdateLoadingProgressBar;
        isFading = true;
        _faderStartCallBack = faderStartCallBack;
        animator.SetBool("faded", true);

    }
    public void FadeEnd(Action faderEndCallBack)
    {
        if (isFading)
        {
            return;
        }
        isFading = true;
        _faderEndCallBack = faderEndCallBack;
        animator.SetBool("faded", false);
        SpecialSceneLoader.instace.ChangeProgress -= UpdateLoadingPercent;
        SpecialSceneLoader.instace.ChangeProgress -= UpdateLoadingProgressBar;

    }
    public void UpdateLoadingPercent(float p)
    {
        LoadingPercent.SetValue((int)((p/0.9)*100));
    }
    public void UpdateLoadingProgressBar(float p)
    {
        LoadingProgressBar.fillAmount = (p / 0.9f);
    }


    private void FadeStartAnimationOver()
    {
        _faderStartCallBack?.Invoke();
        _faderStartCallBack = null;
        isFading = false;
    }
    private void FadeEndtAnimationOver()
    {
        _faderEndCallBack?.Invoke();
        _faderEndCallBack = null; 
        isFading = false; 
    }
}
