using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutActiveLevel : MonoBehaviour
{
    private Text ActiveLevelText;
    private void Awake()
    {
        ActiveLevelText = GetComponent<Text>();
        PlayerPrefs.SetInt(Constants.NumbActiveLevel, 2);
    }
    void Start()
    {
        OutActiveLevelInText();
    }

    private void OutActiveLevelInText()
    {
        var ActiveLevel = PlayerPrefs.GetInt(Constants.NumbActiveLevel);
        if (Enum.IsDefined(typeof(Constants.GameLevelList), ActiveLevel))
        {
            ActiveLevelText.text = "Level " + (PlayerPrefs.GetInt(Constants.NumbActiveLevel));
        }
        else
        {
            ActiveLevelText.text = "Max Level";
        }
    }

}
