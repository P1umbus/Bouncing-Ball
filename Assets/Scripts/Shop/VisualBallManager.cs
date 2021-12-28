using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBallManager : MonoBehaviour
{
    [SerializeField] private Material[] _Materials;
    [SerializeField] private Renderer Ball;

    private void Awake()
    {
        GameEvent.SkinsUpdate += TryUpdateMaterial;
    }

    void Start()
    {
        TryUpdateMaterial();
    }

    private void TryUpdateMaterial()
    {
        var a = PlayerPrefs.GetInt("SelectedSkin");
        Ball.material = _Materials[a];
    }

    private void OnDestroy()
    {
        GameEvent.SkinsUpdate -= TryUpdateMaterial;
    }
}
