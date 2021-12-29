using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Constants.GameLevelList _gameLevelList;

    public void LoadScene()
    {
        SpecialSceneLoader.instace.LoadScene(_gameLevelList.ToString());
    }
}
