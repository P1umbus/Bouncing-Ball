using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class Fader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LeanLocalToken _loadingPercent;
    [SerializeField] private Image _loadingProgressBar;
    private static Fader _instance;
    private const string FADER_PATH = "Fader";
    private const string FADER_ANIM_NAME = "faded";

    public bool isFading { get; private set;}

    public static Fader instance
    {
        get
        {
            if(_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
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
        _animator.SetBool(FADER_ANIM_NAME, true);

    }
    public void FadeEnd(Action faderEndCallBack)
    {
        if (isFading)
        {
            return;
        }
        isFading = true;
        _faderEndCallBack = faderEndCallBack;
        _animator.SetBool(FADER_ANIM_NAME, false);
        SpecialSceneLoader.instace.ChangeProgress -= UpdateLoadingPercent;
        SpecialSceneLoader.instace.ChangeProgress -= UpdateLoadingProgressBar;

    }
    private void UpdateLoadingPercent(float p)
    {
        _loadingPercent.SetValue((int)((p/0.9)*100));
    }
    private void UpdateLoadingProgressBar(float p)
    {
        _loadingProgressBar.fillAmount = (p / 0.9f);
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
