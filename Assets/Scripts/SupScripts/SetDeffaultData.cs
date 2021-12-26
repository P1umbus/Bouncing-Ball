using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeffaultData : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey(Constants.NumbActiveLevel) == false)
        {
            PlayerPrefs.SetInt(Constants.NumbActiveLevel, 1);
        }
       
    }
}
