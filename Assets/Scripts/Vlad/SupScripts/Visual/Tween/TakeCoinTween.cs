using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TakeCoinTween : MonoBehaviour
{
    public static TakeCoinTween Instance;
    [SerializeField] private GameObject _moveTo;
    [SerializeField] private float _moveTime;

    private void Awake()
    {
        Instance = this;
    }
    public void Move(Vector3 objectPosition)
    {
        this.transform.position = Camera.main.WorldToScreenPoint(objectPosition);
        this.gameObject.SetActive(true);
        LeanTween.move(this.gameObject, _moveTo.transform.position, _moveTime).setOnComplete(OnEnd);
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
    }
}
