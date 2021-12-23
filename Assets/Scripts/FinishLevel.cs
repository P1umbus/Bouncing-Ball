using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject ControllCanvas;
    [SerializeField] private GameObject FinishCanvas;
    [SerializeField] private Rigidbody _Rigidbody;
    [SerializeField] private AudioSource _FinishMus;
    [SerializeField] private Button NextLevelButton;
    private CoinManager _CoinManager;
    private int NumbLevel;
    private int StartInLvl;
    private void Awake()
    {
        _CoinManager = FindObjectOfType<CoinManager>();
        NextLevelButton.onClick.AddListener(NextLevel);
        NumbLevel = PlayerPrefs.GetInt(Constants.NumbActiveLevel)-2;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out SquashAndStretch squashAndStretch))
        {
            OnFinish();
        }
    }

    private void OnFinish()
    {
        SetData();
        ControllCanvas.SetActive(false);
        _Rigidbody.isKinematic = true;
        FinishCanvas.SetActive(true);
        _FinishMus.Play();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(NumbLevel + 3);
    }
    private void SetData()
    {
        PlayerPrefs.SetInt(Constants.NumbActiveLevel, (NumbLevel + 3));
        PlayerPrefs.Save();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}
