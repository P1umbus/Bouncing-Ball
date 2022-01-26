using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rarity Rar = Rarity.Common;
    private string t = "t";
    private string T;
    private string TT;

    private void Start()
    {
        if(t == Rarity.Common.ToString())
        {
            Debug.Log(2);
        }
        T = Rarity.Common.ToString();

        TT = Rar.ToString();
    }
}
