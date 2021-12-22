using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject Portal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SquashAndStretch squashAndStretch))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(Constants.NumbActiveLevel));
        }
    }

    public void BallMove()
    {
        LeanTween.move(Ball, Portal.transform.position, 1f).setEaseInExpo();
    }
}
