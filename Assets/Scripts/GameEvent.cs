using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action TakeCoin;

    public class SoundEvents
    {
        public static Action ChangeSound�ptions;
        public static Action <float> ChangeSoundValue;
    }
}
