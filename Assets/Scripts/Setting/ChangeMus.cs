using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMus : MonoBehaviour
{
    [SerializeField] private Slider Value;
    public void ChangeMusValue()
    {
        Debug.Log(Value.value);
        GameEvent.SoundEvents.ChangeSoundValue?.Invoke(Value.value);
    }
    public void ChangeMusStatus()
    {
        GameEvent.SoundEvents.ChangeSoundÎptions?.Invoke();
    }
}
