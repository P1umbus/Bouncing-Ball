using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action TakeCoin;
    public static Action ChangeCoinNumb;
    public static Action ChangeMaterial;
    public static Action <ItemManager> TrySell;
    public class SoundEvents
    {
        public class Shop
        {
            public static Action Sell;
            public static Action Buy;
        }
        public static Action ChangeSoundĪptions;
        public static Action <float> ChangeSoundValue;
       
    }
}
