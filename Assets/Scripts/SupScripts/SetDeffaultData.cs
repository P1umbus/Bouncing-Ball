using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeffaultData : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey(Constants.NumbActiveLevel) == false)
        {
            Debug.Log("2");
            PlayerPrefs.SetInt(Constants.NumbActiveLevel, 1);
        }
        if (PlayerPrefs.HasKey(Constants.ControlSensivity) == false)
        {
            PlayerPrefs.SetFloat(Constants.ControlSensivity, 0.8f);
        }
       
    }
}
