using UnityEngine;
using UnityEngine.UI;

public class ControlSensitivitySettings : MonoBehaviour
{
    [SerializeField] private Slider Value;

    public void ChangeSensivity()
    {
        PlayerPrefs.SetFloat(Constants.ControlSensivity, Value.value);
        PlayerPrefs.Save();
    }
}
