using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    [SerializeField] private CanvasGroup Fade;
    [SerializeField] private int SceneToLoad;
    [SerializeField] private float MinLoadTime;
    private float LoadTime;
    private void Start()
    {
        Fade.alpha = 1f;
        CheckTimeLoad();
    }
    private void Update()
    {
        Load();
    }
    private void Load()
    {
        if (Time.time < MinLoadTime)
        {
            Fade.alpha = 1 - Time.time;
        }
        if (Time.time > MinLoadTime && LoadTime != 0)
        {
            Fade.alpha = Time.time - MinLoadTime;
            if (Fade.alpha >= 1)
            {
                SceneManager.LoadScene(SceneToLoad);
            }
        }
    }
    private void CheckTimeLoad()
    {
        if (Time.time < MinLoadTime)
        {
            LoadTime = MinLoadTime;
        }
        else
        {
            LoadTime = Time.time;
        }
    }
}
