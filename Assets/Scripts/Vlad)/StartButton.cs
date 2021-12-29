using System;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private float _force;

    private Rigidbody _ballRB;

    private bool _isGround = false;

    private void Awake()
    {
        _ballRB = GetComponent<Rigidbody>();
    }

    public void OnClick()
    {
        var ActiveLevel = PlayerPrefs.GetInt(Constants.NumbActiveLevel);
        if (Enum.IsDefined(typeof(Constants.GameLevelList), ActiveLevel))
        {
            if (_isGround == false)
                _ballRB.velocity += Vector3.right * _force;
            else
                Invoke(nameof(OnClick), Time.deltaTime);
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGround = false;
    }
}
