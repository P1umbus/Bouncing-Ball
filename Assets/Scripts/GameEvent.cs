using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action TakeCoin;
    public static Action ChangeCoinNumb;
    public class SoundEvents
    {
        public static Action ChangeSoundÎptions;
        public static Action <float> ChangeSoundValue;
    }
}
