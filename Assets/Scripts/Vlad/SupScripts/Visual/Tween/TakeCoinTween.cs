using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoinTween : MonoBehaviour
{
    [HideInInspector] public static TakeCoinTween Instance;
    [SerializeField] private GameObject MoveTo;
    [SerializeField] private float MoveTime;
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        Instance = this;
    }
    public void Move(Vector3 objectPosition)
    {
        //this.transform.position = _camera.WorldToScreenPoint(objectPosition);
        transform.position = objectPosition;
        this.gameObject.SetActive(true);
        LeanTween.move(this.gameObject, MoveTo.transform.localPosition, MoveTime).setOnComplete(OnEnd);
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
    }

    
}
