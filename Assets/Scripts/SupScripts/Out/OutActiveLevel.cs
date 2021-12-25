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
        //PlayerPrefs.SetInt(Constants.NumbActiveLevel, 3);
    }
    void Start()
    {
        OutActiveLevelInText();
    }

    private void OutActiveLevelInText()
    {
        ActiveLevelText.text = "Level  " + (PlayerPrefs.GetInt(Constants.NumbActiveLevel) - 2);
    }

}
