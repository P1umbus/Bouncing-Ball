using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private AsyncOperation operation;
    [SerializeField] private Animator _anim;
    [SerializeField] private Text LoadingPercent;
    [SerializeField] private Image LoadingProgressBar;
    private void Start()
    {

    }
    private void LevelLoad()
    {
        StartCoroutine(Loader());
    }

    IEnumerator Loader()
    {
        operation = SceneManager.LoadSceneAsync(Constants.MainLevelList.Menu.ToString());
        operation.allowSceneActivation = false;
        while (operation.progress < 0.89)
        {
            LoaderUI(operation.progress);
            yield return null;
        }
        _anim.SetTrigger("FadeEnd");
        LoaderUI(operation.progress);
    }

    private void LoaderUI(float progress)
    {
        LoadingPercent.text = "Loading" + (int)((progress / 0.9) * 100) + "%";
        LoadingProgressBar.fillAmount = progress / 0.9f;
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
