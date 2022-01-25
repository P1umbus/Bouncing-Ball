using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsReset : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.SetInt(Constants.PPname.NumbActiveLevel, 1);
        Bank.instance.PluralIncreaseCoinNumb(100);
        SpecialSceneLoader.instace.LoadScene(MainLevelList.Menu.ToString());
    }
}
