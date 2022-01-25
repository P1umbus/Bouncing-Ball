using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{
    private string PPMusStatusName = "MusStatus";
    private string PPMusValueName = "MusValue";
    private float _musValue;
    private bool _isMusOn = true;

    private void Awake()
    {
        GameEvent.SoundEvents.ChangeSoundOptions += ChangeMusStatus;
        GameEvent.SoundEvents.ChangeSoundValue += ChangeMusValue;
    }

    private void Start()
    { 
        LoadMusStatus();
        LoadValue();
        ChangeMusVolueInScene();
    }

    public void ChangeMusStatus()
    {
        if (_isMusOn == true)
        {
            _isMusOn = false;
            SaveMusStatus();
            ChangeMusVolueInScene();
        }
        else
        {
            _isMusOn = true;
            SaveMusStatus();
            ChangeMusVolueInScene();
        }
    }

    public bool GetIsMusOn()
    {
        return _isMusOn;
    }

    public float GetMusValue()
    {
        return _musValue;
    }
    private void MusicOn()
    {
        AudioListener.volume = _musValue;

    }

    private void MusicOff()
    {
        AudioListener.volume = 0;
    }

    private void ChangeMusValue(float Value)
    {
        _musValue = Value;
        SaveMusValue();
        ChangeMusVolueInScene();
    }

    

    private void ChangeMusVolueInScene()
    {
        if (_isMusOn == true)
        {
            MusicOn();
        }
        else
        {
            MusicOff();
        }
    }

    private void LoadMusStatus()
    {
        if (PlayerPrefs.HasKey(PPMusStatusName))
        {
            CheckLoadMusStatus();
        }
        else
        {
            PlayerPrefs.SetInt(PPMusStatusName, 1);
            CheckLoadMusStatus();

        }
    }

    private void LoadValue()
    {
        if (PlayerPrefs.HasKey(PPMusValueName))
        {
            _musValue = PlayerPrefs.GetFloat(PPMusValueName);
        }
        else
        {
            PlayerPrefs.SetFloat(PPMusValueName, 1);
            _musValue = PlayerPrefs.GetFloat(PPMusValueName);
        }
    }

    private void CheckLoadMusStatus()
    {
        if (PlayerPrefs.GetInt(PPMusStatusName) == 1)
        {
            _isMusOn = true;
        }
        else
        {
            _isMusOn = false;
        }
    }
    private void SaveMusStatus()
    {
        if (_isMusOn == true)
        {
            PlayerPrefs.SetInt(PPMusStatusName, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PPMusStatusName, 0);
        }
    }
    private void SaveMusValue()
    {
        PlayerPrefs.SetFloat(PPMusValueName, _musValue);
    }

    private void OnDestroy()
    {
        GameEvent.SoundEvents.ChangeSoundOptions -= ChangeMusStatus;
        GameEvent.SoundEvents.ChangeSoundValue -= ChangeMusValue;
    }
 
}

