using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Constants.MainLevelList _gameLevelList;
    
    public void LoadScene()
    {
        SpecialSceneLoader.instace.LoadScene(_gameLevelList.ToString());
    }
}
