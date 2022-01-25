using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private GameObject _portal;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SquashAndStretch squashAndStretch))
        {
            _ball.isKinematic = true;
            var ActiveLevel = PlayerPrefs.GetInt(Constants.PPname.NumbActiveLevel);
            if (Enum.IsDefined(typeof(GameLevelList), ActiveLevel))
            {
                SpecialSceneLoader.instace.LoadScene(((GameLevelList)ActiveLevel).ToString());
            }
           
        }
    }

}
