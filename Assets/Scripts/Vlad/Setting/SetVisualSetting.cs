using UnityEngine;
using UnityEngine.UI;

public class SetVisualSetting : MonoBehaviour
{
    [SerializeField] private GameObject _musOn;
    [SerializeField] private GameObject _musOff;
    [SerializeField] private Slider _musValue;
    [SerializeField] private Slider _sensValue;
    [SerializeField] private SoundOptions _soundOptions;
        
    private void Start()
    {
        SetActiveMusStatus();
        SetMusValue();
        SetSensValue();
    }

    private void SetActiveMusStatus()
    {
        if (_soundOptions.GetIsMusOn() == true)
        {
            _musOn.SetActive(true);
            _musOff.SetActive(false);
        }
        else
        {
            _musOn.SetActive(false);
            _musOff.SetActive(true);
        }
    }
    private void SetMusValue()
    {
        _musValue.value = _soundOptions.GetMusValue();
    }
    private void SetSensValue()
    {
        _sensValue.value = PlayerPrefs.GetFloat(Constants.PPname.ControlSensivity);
    }
}
