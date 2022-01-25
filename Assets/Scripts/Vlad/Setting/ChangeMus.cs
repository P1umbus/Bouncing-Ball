using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMus : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _activeMusButton;
    [SerializeField] private Button _deactivationMusButton;
    private void Awake()
    {
        _slider.onValueChanged.AddListener(delegate { ChangeMusValue();});
        _activeMusButton.onClick.AddListener(ChangeMusStatus);
        _deactivationMusButton.onClick.AddListener(ChangeMusStatus);
    }
    private void ChangeMusValue()
    {
        GameEvent.SoundEvents.ChangeSoundValue?.Invoke(2);
    }
    private void ChangeMusStatus()
    {
        GameEvent.SoundEvents.ChangeSoundOptions?.Invoke();
    }
    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveAllListeners();
        _activeMusButton.onClick.RemoveAllListeners();
        _deactivationMusButton.onClick.RemoveAllListeners();
    }
}
