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
    [SerializeField] private ParticleSystem Confetti;
    private int NumbLevel;
    private int _finishReward = 10;
    private bool Finished = false;

    private float _timeCompleteLevel;

    private void Awake()
    {
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
            Confetti.gameObject.SetActive(true);
            ControllCanvas.SetActive(false);
            _Rigidbody.isKinematic = true;
            FinishCanvas.SetActive(true);
            AddFinishReward(_finishReward);
            _FinishMus.Play();

            _timeCompleteLevel = Time.timeSinceLevelLoad;

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
    public void RestartButton()
    {
        SpecialSceneLoader.instace.LoadScene(((Constants.GameLevelList)NumbLevel).ToString()); 
    }
    private void SetData()
    {
        PlayerPrefs.SetInt(Constants.NumbActiveLevel, NumbLevel + 1);
        PlayerPrefs.Save();
    }
    private void AddFinishReward(int reward)
    {
        Bank.instance.PluralIncreaseCoinNumb(reward);
        CoinManager.Instance.IncreaseCoinNumb(reward);
        TakePluralCoinTween.Instance.WorldMove(this.transform.position, reward);
    }

    private void OnDestroy()
    {
        if (Finished == true)
        {
            FirebaseManager.Instance.EndLevel(_timeCompleteLevel);
        }
        NextLevelButton.onClick.RemoveListener(NextLevel);
    }
}
