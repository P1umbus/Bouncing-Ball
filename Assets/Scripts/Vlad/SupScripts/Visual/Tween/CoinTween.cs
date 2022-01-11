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
    public void OnSell(Transform ButtonPos)
    {
        Coin.transform.position = ButtonPos.position;
        this.gameObject.SetActive(true);
        LeanTween.move(Coin, MoveTo.transform.position, MoveTime).setEaseInBack().setOnComplete(OnEnd);
        LeanTween.scale(Coin, Scale, MoveTime).setEaseInQuart();
    }
    public void OnBuy(Transform ButtonPos)
    {
        this.gameObject.SetActive(true);
        LeanTween.move(Coin, ButtonPos.position, MoveTime).setEaseInQuart().setOnComplete(OnEnd);
        LeanTween.scale(Coin, Scale, MoveTime).setEaseInQuart();
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = StartPos;
        this.gameObject.LeanScale(StartScale, 0f);
    }
}
