using UnityEngine;

public class VisualBallManager : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Renderer _ball;

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
        var a = PlayerPrefs.GetInt(Constants.PPname.SelectedSkin);
        _ball.material = _materials[a];
    }

    private void OnDestroy()
    {
        GameEvent.SkinsUpdate -= TryUpdateMaterial;
    }
}
