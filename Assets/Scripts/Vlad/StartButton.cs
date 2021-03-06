using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class StartButton : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private GameObject _resetPanel;

    private Rigidbody _ballRB;

    private bool _isGround = false;

    private void Awake()
    {
        _ballRB = GetComponent<Rigidbody>();
    }

    public void OnClick()
    {
        var ActiveLevel = PlayerPrefs.GetInt(Constants.PPname.NumbActiveLevel);
        if (Enum.IsDefined(typeof(GameLevelList), ActiveLevel))
        {
            if (_isGround == false)
                _ballRB.velocity += Vector3.right * _force;
            else
                Invoke(nameof(OnClick), Time.deltaTime);
        }
        else
        {
            _resetPanel.SetActive(true);
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
