using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Preloader : MonoBehaviour
{
    private AsyncOperation operation;
    [SerializeField] private Animator _anim;
    [SerializeField] private Text _loadingPercent;
    [SerializeField] private Image _loadingProgressBar;
    private const string ANIM_TRIGGER_NAME = "FadeEnd";
    private const string LOADING = "Loading";

    private void LevelLoad()
    {
        StartCoroutine(Loader());
    }

    private IEnumerator Loader()
    {
        operation = SceneManager.LoadSceneAsync(MainLevelList.Menu.ToString());
        operation.allowSceneActivation = false;
        while (operation.progress < 0.89)
        {
            LoaderUI(operation.progress);
            yield return null;
        }
        _anim.SetTrigger(ANIM_TRIGGER_NAME);
        LoaderUI(operation.progress);
    }

    private void LoaderUI(float progress)
    {
        _loadingPercent.text = LOADING + (int)((progress / 0.9) * 100) + "%";
        _loadingProgressBar.fillAmount = progress / 0.9f;
    }
    private void FadeStartAnimationOver()
    {
        LevelLoad();
    }
    private void FadeEndtAnimationOver()
    {
        operation.allowSceneActivation = true;
    }
}
