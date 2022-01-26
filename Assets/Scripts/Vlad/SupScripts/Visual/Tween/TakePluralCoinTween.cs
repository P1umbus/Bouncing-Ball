using UnityEngine;
using UnityEngine.UI;

public class TakePluralCoinTween : MonoBehaviour
{
    public static TakePluralCoinTween Instance;
    [SerializeField] private GameObject _moveTo;
    [SerializeField] private float _moveTime;
    [SerializeField] private Camera _camera;
    private Text _textCoinNumb;

    private void Awake()
    {
        Instance = this;
        _textCoinNumb = GetComponentInChildren<Text>();
        this.gameObject.SetActive(false);
    }
    public void WorldMove(Vector3 objectPosition, int numb)
    {
        Debug.Log("Pos1" + _camera.WorldToScreenPoint(objectPosition));
        this.transform.position = _camera.WorldToScreenPoint(objectPosition);
        this.gameObject.SetActive(true);
        _textCoinNumb.text = numb.ToString();
        LeanTween.move(this.gameObject, _moveTo.transform.position, _moveTime).setOnComplete(OnEnd);
        Debug.Log("Pos2" + _moveTo.transform.position);
    }
    public void ScreenMove(Vector3 objectPosition, int numb)
    {
        Debug.Log("Work");
        this.transform.position = objectPosition;
        this.gameObject.SetActive(true);
        _textCoinNumb.text = numb.ToString();
        LeanTween.move(this.gameObject, _moveTo.transform.position, _moveTime).setOnComplete(OnEnd);
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
    }
}
