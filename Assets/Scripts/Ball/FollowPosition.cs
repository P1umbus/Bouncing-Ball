using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour 
{    
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _minY;
    [SerializeField] private float _yIgnor;//забыл слово которым лучше назвать это поле(

    void FixedUpdate() 
    {
        float x = Mathf.Lerp(transform.position.x, _target.position.x, Time.deltaTime * _speed);
        float y = transform.position.y;

        if (Mathf.Abs(transform.position.y - _target.position.y) > _yIgnor)
            y = Mathf.Lerp(transform.position.y, _target.position.y, Time.deltaTime * _speed * 0.3f);

        if (y < _minY)
            y = _minY;

        transform.position = new Vector3(x, y, 0);
    }    
}
