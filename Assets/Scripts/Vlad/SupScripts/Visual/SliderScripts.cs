using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;

    private void Start()
    {
        FillSlider();
    }

    public void FillSlider()
    {
        _fill.fillAmount = _slider.value;
    }
}
