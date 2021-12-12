using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject[] ExtraElementsUI;
    public void PauseOn()
    {
        Time.timeScale = 0f;
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
    }
    public void OffExtraElementsUI()
    {
        foreach(GameObject GO in ExtraElementsUI)
        {
            GO.SetActive(false);
        }
    }
    public void OnExtraElementsUI()
    {
        foreach (GameObject GO in ExtraElementsUI)
        {
            GO.SetActive(true);
        }

    }
}
