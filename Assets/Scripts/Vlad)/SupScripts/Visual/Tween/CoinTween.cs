using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTween : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject MoveTo;
    [SerializeField] private float MoveTime;
    [SerializeField] private Vector2 Scale;
    private RectTransform _rectTransform;
    private Vector2 StartScale;
    private Vector3 StartPos;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        StartScale = new Vector2(_rectTransform.localScale.x, _rectTransform.localScale.y);
        StartPos = this.gameObject.transform.position;
    }
    public void OnSell(GameObject button)
    {
        Coin.transform.position = button.transform.position;
        this.gameObject.SetActive(true);
        LeanTween.move(Coin, MoveTo.transform.position, MoveTime).setEaseInBack().setOnComplete(OnEnd);
        LeanTween.scale(Coin, Scale, MoveTime).setEaseInQuart();
    }
    public void OnBuy(GameObject button)
    {
        MoveTo = button;
        this.gameObject.SetActive(true);
        LeanTween.move(Coin, MoveTo.transform.position, MoveTime).setEaseInQuart().setOnComplete(OnEnd);
        //LeanTween.rotate(Coin, new Vector3(270, 180, 0), 0.2f).setLoopPingPong(7);
        //LeanTween.delayedCall(1f, Callback);
        LeanTween.scale(Coin, Scale, MoveTime).setEaseInQuart();
    }
    private void Callback()
    {
        LeanTween.alpha(_rectTransform, 0.5f, 0.5f);
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = StartPos;
        this.gameObject.LeanScale(StartScale, 0f);
        _rectTransform.LeanAlpha(255f, 0f);
    }
}
