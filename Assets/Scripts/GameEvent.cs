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
        public static Action ChangeSoundĪptions;
        public static Action <float> ChangeSoundValue;
    }
}
