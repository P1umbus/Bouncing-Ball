using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePluralCoinTween : MonoBehaviour
{
    [HideInInspector] public static TakePluralCoinTween Instance;
    [SerializeField] private GameObject MoveTo;
    [SerializeField] private float MoveTime;
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
        this.transform.position = _camera.WorldToScreenPoint(objectPosition);
        this.gameObject.SetActive(true);
        _textCoinNumb.text = numb.ToString();
        LeanTween.move(this.gameObject, MoveTo.transform.position, MoveTime).setOnComplete(OnEnd);
        //CoinMove();
    }
    public void ScreenMove(Vector3 objectPosition, int numb)
    {
        this.transform.position = objectPosition;
        this.gameObject.SetActive(true);
        _textCoinNumb.text = numb.ToString();
        LeanTween.move(this.gameObject, MoveTo.transform.position, MoveTime).setOnComplete(OnEnd);
        //CoinMove();
    }
    private void CoinMove()
    {
        LeanTween.move(this.gameObject, MoveTo.transform.position, MoveTime).setOnComplete(OnEnd);
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
    }
    //private void OnDestroy()
    //{
    //    LeanTween.cancelAll();
    //}
}
