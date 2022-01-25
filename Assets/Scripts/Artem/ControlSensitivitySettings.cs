using UnityEngine;
using UnityEngine.UI;

public class ControlSensitivitySettings : MonoBehaviour
{
    [SerializeField] private Slider Value;

    public void ChangeSensivity()
    {
        PlayerPrefs.SetFloat(Constants.PPname.ControlSensivity, Value.value);
        PlayerPrefs.Save();
    }
}
