using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutActiveLevel : MonoBehaviour
{
    private LeanLocalToken LevelNumb;
    [SerializeField] private Text _maxlevel;
    private void Awake()
    {
        LevelNumb = GetComponentInChildren<LeanLocalToken>();
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
            LevelNumb.SetValue(PlayerPrefs.GetInt(Constants.NumbActiveLevel));
        }
        else
        {
            _maxlevel.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
