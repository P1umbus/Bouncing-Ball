using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string NumbLevel;
    [SerializeField] private int NumbLevelToLoad;
    [SerializeField] private GameObject[] Star;
    [SerializeField] private GameObject LockedImg;
    private string AccessNamePP;
    private string StarInLvlNamePP;

    private bool IsAccess;
    private int StarInLvl;

    private void Awake()
    {
        LoadData();
        DisplayLocked();
        DisplayStar();
    }    

    public void GoToLevel()
    {
        if (IsAccess == true)
        {
            SceneManager.LoadScene(NumbLevelToLoad);
        } 
    }
    private void DisplayLocked()
    {
        if(IsAccess == true)
        {
            LockedImg.SetActive(false);
        }
        else
        {
            LockedImg.SetActive(true);
        }
    }
    private void DisplayStar()
    {
        if(StarInLvl == 1)
        {
            Star[0].SetActive(true);
        }
        else if (StarInLvl == 2)
        {
            Star[0].SetActive(true);
            Star[1].SetActive(true);
        }
        else if (StarInLvl == 3)
        {
            Star[0].SetActive(true);
            Star[1].SetActive(true);
            Star[2].SetActive(true);
        }
    }

    private void LoadData()
    {
        AccessNamePP = "IsAccessLvl" + NumbLevel;
        StarInLvlNamePP = "StarInLvl" + NumbLevel;
        if (PlayerPrefs.HasKey(AccessNamePP))
        {
            CheckAccess();
        }
        else
        {
            PlayerPrefs.SetInt(AccessNamePP, 0);
            CheckAccess();
        }
        if (PlayerPrefs.HasKey(StarInLvlNamePP))
        {
            StarInLvl = PlayerPrefs.GetInt(StarInLvlNamePP);
        }
        else
        {
            PlayerPrefs.SetInt(StarInLvlNamePP, 0);
            StarInLvl = PlayerPrefs.GetInt(StarInLvlNamePP);
        }
    }

    private void CheckAccess()
    {
        if(PlayerPrefs.GetInt(AccessNamePP) == 0)
        {
            IsAccess = false;
        }
        else
        {
            IsAccess = true;
        }
    }
}
