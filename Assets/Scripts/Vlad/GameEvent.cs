using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action ChangeCoinNumb;
    public static Action <ItemManager> TrySell;
    public static Action MultiplyCoin;
    public static Action SkinsUpdate;
    public class SoundEvents
    {
        public class Shop
        {
            public static Action Sell;
            public static Action Buy;
        }
        public static Action ChangeSoundOptions;
        public static Action <float> ChangeSoundValue;
       
    }
}
