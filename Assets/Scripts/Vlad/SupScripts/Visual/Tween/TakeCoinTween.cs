using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoinTween : MonoBehaviour
{
    [HideInInspector] public static TakeCoinTween Instance;
    [SerializeField] private GameObject MoveTo;
    [SerializeField] private float MoveTime;

    private void Awake()
    {
        Instance = this;
    }
    public void Move(Vector3 objectPosition)
    {
        this.transform.position = Camera.main.WorldToScreenPoint(objectPosition);
        this.gameObject.SetActive(true);
        LeanTween.move(this.gameObject, MoveTo.transform.position, MoveTime).setOnComplete(OnEnd);
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
    }

    
}
