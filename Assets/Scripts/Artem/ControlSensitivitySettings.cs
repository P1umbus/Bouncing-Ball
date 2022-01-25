using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ControlSensitivitySettings : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.onValueChanged.AddListener(ChangeSensivity);
    }

    public void ChangeSensivity(float value)
    {
        PlayerPrefs.SetFloat(Constants.PPname.ControlSensivity, value);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(ChangeSensivity);
    }
}
