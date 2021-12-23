using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoinTween : MonoBehaviour
{
    [SerializeField] private GameObject MoveTo;
    [SerializeField] private float MoveTime;
    [SerializeField] private Camera _camera;

    public void Move(Vector3 objectPosition)
    {
        this.transform.position = _camera.WorldToScreenPoint(objectPosition);
        this.gameObject.SetActive(true);
        LeanTween.move(this.gameObject, MoveTo.transform.position, MoveTime).setOnComplete(OnEnd);
    }
    private void OnEnd()
    {
        this.gameObject.SetActive(false);
    }

    
}
