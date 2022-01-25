using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinTween : MonoBehaviour
{
    [FormerlySerializedAs("Coin")] [SerializeField] private GameObject _coin;
    [FormerlySerializedAs("MoveTo")] [SerializeField] private GameObject _moveTo;
    [FormerlySerializedAs("MoveTime")] [SerializeField] private float _moveTime;
    [FormerlySerializedAs("Scale")] [SerializeField] private Vector2 _scale;
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
        _coin.transform.position = ButtonPos.position;
        this.gameObject.SetActive(true);
        LeanTween.move(_coin, _moveTo.transform.position, _moveTime).setEaseInBack().setOnComplete(OnEnd);
        LeanTween.scale(_coin, _scale, _moveTime).setEaseInQuart();
    }
    public void OnBuy(Transform ButtonPos)
    {
        this.gameObject.SetActive(true);
        LeanTween.move(_coin, ButtonPos.position, _moveTime).setEaseInQuart().setOnComplete(OnEnd);
        LeanTween.scale(_coin, _scale, _moveTime).setEaseInQuart();
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = StartPos;
        this.gameObject.LeanScale(StartScale, 0f);
    }
}
