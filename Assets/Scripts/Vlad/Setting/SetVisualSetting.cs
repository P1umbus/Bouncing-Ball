using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVisualSetting : MonoBehaviour
{
    [SerializeField] private GameObject MusOn;
    [SerializeField] private GameObject MusOff;
    [SerializeField] private Slider MusValue;
    [SerializeField] private Slider SensValue;
    [SerializeField] private SoundOptions _SoundOptions;

    private void Start()
    {
        SetActiveMusStatus();
        SetMusValue();
        SetSensValue();
    }

    private void SetActiveMusStatus()
    {
        if (_SoundOptions.GetIsMusOn() == true)
        {
            MusOn.SetActive(true);
            MusOff.SetActive(false);
        }
        else
        {
            MusOn.SetActive(false);
            MusOff.SetActive(true);
        }
    }
    private void SetMusValue()
    {
        MusValue.value = _SoundOptions.GetMusValue();
    }
    private void SetSensValue()
    {
        SensValue.value = PlayerPrefs.GetFloat(Constants.ControlSensivity);
    }
}
