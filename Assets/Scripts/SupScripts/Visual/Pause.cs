using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public void PauseOn()
    {
        Time.timeScale = 0f;
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
    }
}
