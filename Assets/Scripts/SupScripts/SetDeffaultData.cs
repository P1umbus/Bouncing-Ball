using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeffaultData : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("IsAccessLvl1", 1);
    }
}
