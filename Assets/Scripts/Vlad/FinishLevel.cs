using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject _controllCanvas;
    [SerializeField] private GameObject _finishCanvas;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _finishMus;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private ParticleSystem _confetti;
    private int _numbLevel;
    private int _finishReward = 10;
    private bool _finished = false;

    private float _timeCompleteLevel;

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(NextLevel);
        _numbLevel = PlayerPrefs.GetInt(Constants.PPname.NumbActiveLevel);
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
        if(_finished == false)
        {
            SetData();
            _confetti.gameObject.SetActive(true);
            _controllCanvas.SetActive(false);
            _rigidbody.isKinematic = true;
            _finishCanvas.SetActive(true);
            AddFinishReward(_finishReward);
            _finishMus.Play();

            _timeCompleteLevel = Time.timeSinceLevelLoad;

            _finished = true;
        }
    }
    public void NextLevel()
    {
        if (Enum.IsDefined(typeof(GameLevelList), _numbLevel+1))
        {
            SpecialSceneLoader.instace.LoadScene(((GameLevelList)(_numbLevel+1)).ToString());
        }   
    }
    public void RestartButton()
    {
        SpecialSceneLoader.instace.LoadScene(((GameLevelList)_numbLevel).ToString()); 
    }
    private void SetData()
    {
        PlayerPrefs.SetInt(Constants.PPname.NumbActiveLevel, _numbLevel + 1);
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
        if (_finished == true)
        {
            FirebaseManager firebaseManager = DataLoadSystem.GetLoader<FirebaseManager>(DataLoaders.FirebaseManager);
            firebaseManager.EndLevel(_timeCompleteLevel);
        }
        _nextLevelButton.onClick.RemoveListener(NextLevel);
    }
}
