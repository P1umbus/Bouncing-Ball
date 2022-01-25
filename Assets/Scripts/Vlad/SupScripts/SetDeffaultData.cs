using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeffaultData : MonoBehaviour
{
    private const string DEFAUILT_MATERIAL = "Material0";
    private void Awake()
    {
        if (PlayerPrefs.HasKey(Constants.PPname.NumbActiveLevel) == false)
        {
            PlayerPrefs.SetInt(Constants.PPname.NumbActiveLevel, 1);
        }
        if (PlayerPrefs.HasKey(Constants.PPname.ControlSensivity) == false)
        {
            PlayerPrefs.SetFloat(Constants.PPname.ControlSensivity, 0.8f);
        }
        PlayerPrefs.SetInt(DEFAUILT_MATERIAL, 1);
    }
}
