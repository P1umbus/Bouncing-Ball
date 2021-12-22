using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHyest : MonoBehaviour
{
    [SerializeField] private RectTransform _ui;

    private void Update()
    {
           _ui.position = Camera.main.WorldToScreenPoint( transform.position);
    }
}
