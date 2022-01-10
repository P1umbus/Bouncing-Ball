using System;
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
    private bool Finished = false;
    private void Awake()
    {
        _CoinManager = FindObjectOfType<CoinManager>();
        NextLevelButton.onClick.AddListener(NextLevel);
        NumbLevel = PlayerPrefs.GetInt(Constants.NumbActiveLevel);
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
        if(Finished == false)
        {
            SetData();
            ControllCanvas.SetActive(false);
            _Rigidbody.isKinematic = true;
            Bank.instance.PluralIncreaseCoinNumb(10);
            CoinManager.Instance.IncreaseCoinNumb(10);
            FinishCanvas.SetActive(true);
            _FinishMus.Play();
            Finished = true;
        }
    }
    public void NextLevel()
    {
        if (Enum.IsDefined(typeof(Constants.GameLevelList), NumbLevel+1))
        {
            SpecialSceneLoader.instace.LoadScene(((Constants.GameLevelList)(NumbLevel+1)).ToString());
        }   
    }
    private void SetData()
    {
        PlayerPrefs.SetInt(Constants.NumbActiveLevel,NumbLevel+1);
        PlayerPrefs.Save();
    }

    public void RestartButton()
    {
        SpecialSceneLoader.instace.LoadScene(((Constants.GameLevelList)NumbLevel).ToString()); 
    }
}
