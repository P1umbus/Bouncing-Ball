using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagementUI : MonoBehaviour
{
    [SerializeField] private Image[] _extraElements;
    [SerializeField] private Text[] _extraElementsText;
    [SerializeField] private Button On;
    [SerializeField] private Button Off;
    void Start()
    {
        Off.onClick.AddListener(DisconnectionUI);
        On.onClick.AddListener(ConnectionUI);
    }
    public void DisconnectionUI()
    {
        foreach (Image GO in _extraElements)
        {
            GO.color = new Color(GO.color.r, GO.color.g, GO.color.b, 0);
        }
        foreach (Text GO in _extraElementsText)
        {
            GO.color = new Color(GO.color.r, GO.color.g, GO.color.b, 0);
        }
    }
    public void ConnectionUI()
    {
        foreach (Image GO in _extraElements)
        {
            GO.color = new Color(GO.color.r, GO.color.g, GO.color.b, 1);
        }
        foreach (Text GO in _extraElementsText)
        {
            GO.color = new Color(GO.color.r, GO.color.g, GO.color.b, 1);
        }
    }
}
