using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDataMus : MonoBehaviour
{
    [SerializeField] private GameObject MusOn;
    [SerializeField] private GameObject MusOff;
    [SerializeField] private Slider MusValue;
    private SoundOptions _SoundOptions;

    private void Awake()
    {
        _SoundOptions = FindObjectOfType <SoundOptions>();
    }
    void Start()
    {
        SetActiveMusStatus();
        SetMusValue();
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
}
