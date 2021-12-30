using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{
    private string PPMusStatusName = "MusStatus";
    private string PPMusValueName = "MusValue";
    private float MusValue;
    private bool IsMusOn = true;

    private void Awake()
    {
        //Object.DontDestroyOnLoad(this);
        GameEvent.SoundEvents.ChangeSound�ptions += ChangeMusStatus;
        GameEvent.SoundEvents.ChangeSoundValue += ChangeMusValue;
    }
    private void Start()
    { 
        LoadMusStatus();
        LoadValue();
        ChangeMusVolueInScene();
    }
    private void MusicOn()
    {
        AudioListener.volume = MusValue;

    }
    private void MusicOff()
    {
        AudioListener.volume = 0;
    }
    private void ChangeMusValue(float Value)
    {
        Debug.Log("ChangeMusValue");
        MusValue = Value;
        SaveMusValue();
        ChangeMusVolueInScene();
    }
    public void ChangeMusStatus()
    {
        if (IsMusOn == true)
        {
            IsMusOn = false;
            SaveMusStatus();
            ChangeMusVolueInScene();
        }
        else
        {
            IsMusOn = true;
            SaveMusStatus();
            ChangeMusVolueInScene();
        }
    }
    private void ChangeMusVolueInScene()
    {
        if (IsMusOn == true)
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
            MusValue = PlayerPrefs.GetFloat(PPMusValueName);
        }
        else
        {
            PlayerPrefs.SetFloat(PPMusValueName, 1);
            MusValue = PlayerPrefs.GetFloat(PPMusValueName);
        }
    }

    private void CheckLoadMusStatus()
    {
        if (PlayerPrefs.GetInt(PPMusStatusName) == 1)
        {
            IsMusOn = true;
        }
        else
        {
            IsMusOn = false;
        }
    }
    private void SaveMusStatus()
    {
        if (IsMusOn == true)
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
        PlayerPrefs.SetFloat(PPMusValueName, MusValue);
    }

    private void OnDestroy()
    {
        GameEvent.SoundEvents.ChangeSound�ptions -= ChangeMusStatus;
    }
    public bool GetIsMusOn()
    {
        return IsMusOn;
    }
    public float GetMusValue()
    {
        return MusValue;
    }
}

