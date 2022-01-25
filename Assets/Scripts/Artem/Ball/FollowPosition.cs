using UnityEngine;

public class FollowPosition : MonoBehaviour 
{    
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _minY;
    [SerializeField] private float _yIgnor;

    void FixedUpdate() 
    {
        float x = Mathf.Lerp(transform.localPosition.x, _target.localPosition.x, Time.deltaTime * _speed);
        float y = transform.localPosition.y;

        if (Mathf.Abs(transform.localPosition.y - _target.localPosition.y) > _yIgnor)
            y = Mathf.Lerp(transform.localPosition.y, _target.localPosition.y, Time.deltaTime * _speed * 0.3f);

        if (y < _minY)
            y = _minY;

        transform.localPosition = new Vector3(x, y, 0);
    }    
}
