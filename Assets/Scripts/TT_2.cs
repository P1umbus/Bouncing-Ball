using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TT_2 : MonoBehaviour
{
    [SerializeField] private Constants.Level _Level;
    public void Test()
    {
        SpecialSceneLoader.instace.LoadScene(_Level.ToString());
        Debug.Log(((int)_Level));
    }
}
