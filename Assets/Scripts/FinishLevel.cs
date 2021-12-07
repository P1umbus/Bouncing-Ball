using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] Starts;
    [SerializeField] private GameObject ControllCanvas;
    [SerializeField] private GameObject FinishCanvas;
    [SerializeField] private Rigidbody _Rigidbody;
    [SerializeField] private AudioSource _FinishMus;
    [SerializeField] private int NumbLevel;
    private string AccessNamePP;
    private string StarInLvlNamePP;
    private CoinManager _CoinManager;


    private void Awake()
    {
        _CoinManager = FindObjectOfType<CoinManager>();
        AccessNamePP = "IsAccessLvl" + (NumbLevel+1);
        StarInLvlNamePP = "StarInLvl" + NumbLevel;
    }
    private void OnTriggerEnter(Collider other)
    {
        OnFinish();
    }

    private void OnFinish()
    {
        ControllCanvas.SetActive(false);
        _Rigidbody.isKinematic = true;
        FinishCanvas.SetActive(true);
        SetData();
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        var a = _CoinManager.GetCollectedCoinsPercentage();
        if (a < 50 && a > 20)
        {
            Starts[0].SetActive(true);
            PlayerPrefs.SetInt(StarInLvlNamePP, 1);
            _FinishMus.Play();
        }
        else if (a < 100 && a >= 50)
        {
            Starts[0].SetActive(true);
            _FinishMus.Play();
            yield return new WaitForSeconds(1f);
            Starts[1].SetActive(true);
            PlayerPrefs.SetInt(StarInLvlNamePP, 2);
            _FinishMus.Play();
        }
        else if (a == 100)
        {
            Starts[0].SetActive(true);
            _FinishMus.Play();
            yield return new WaitForSeconds(1f);
            Starts[1].SetActive(true);
            _FinishMus.Play();
            yield return new WaitForSeconds(1f);
            Starts[2].SetActive(true);
            PlayerPrefs.SetInt(StarInLvlNamePP, 3);
            _FinishMus.Play();
        }
    }

    private void SetData()
    {
        PlayerPrefs.SetInt(AccessNamePP, 1);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}
