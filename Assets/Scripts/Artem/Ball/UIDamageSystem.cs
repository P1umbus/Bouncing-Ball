using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamageSystem : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CanvasGroup _redPanel;
    [SerializeField] private Canvas _deadScreen;
    [SerializeField] private Canvas _maineScreen;
    [SerializeField] private GameObject[] _hearts;

    [SerializeField] private float _rednessDuration = 0.3f;
    [Range(0, 1)]
    [SerializeField] private float _rednessMaxAlpha = 0.7f;

    public void GetDamaged()
    {
        LeanTween.alphaCanvas(_redPanel, _rednessMaxAlpha, _rednessDuration);
        LeanTween.alphaCanvas(_redPanel, 0, _rednessDuration).setDelay(_rednessDuration);
    }

    public void ShowHearts(int currentHealth)
    {
        if (_hearts == null || _hearts.Length == 0)
            return;

        for (int i = 0; i < 3; i++)
        {
            if (currentHealth > 0)
                _hearts[i].SetActive(true);
            else
                _hearts[i].SetActive(false);

            currentHealth--;
        }
    }

    public void Death()
    {
        _deadScreen.gameObject.SetActive(true);
        _player.gameObject.SetActive(false);
        _maineScreen.gameObject.SetActive(false);
    }
}
