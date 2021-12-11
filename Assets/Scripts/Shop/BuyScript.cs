using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    [SerializeField] private int Price;
    [SerializeField] private int SkinNumb;
    [SerializeField] private Text PriceText;
    private int Status;
    private bool isPurchased = false;
    private string PPname;

    private void Awake()
    {
        LoadDate();
    }

    void Start()
    {
        CheckStatus();
    }

    public void Buy()
    {
        if (Bank.instance.IsEnough(Price) && isPurchased == false)
        {
            PlayerPrefs.SetInt(PPname, 1);
            PlayerPrefs.Save();
            Bank.instance.ReduceCoinNumb(Price);
            PriceText.text = "Bought";
            isPurchased = true;
        }
        else if (isPurchased == true)
        {
            StartCoroutine(Select());
        }
    }
    IEnumerator Select()
    {
        string lastText = PriceText.text;
        PriceText.text = "Taken";
        PlayerPrefs.SetInt("SelectedSkin", SkinNumb);
        yield return new WaitForSeconds(1f);
        PriceText.text = lastText;
    }
    private void CheckStatus()
    {
        if(Status == 0)
        {
            PriceText.text = Price.ToString();
        }
        else if(Status == 1)
        {
            PriceText.text = "Bought";
            isPurchased = true;
        }
    }
    private void LoadDate()
    {
        PPname = "Material" + SkinNumb;
        if (PlayerPrefs.HasKey(PPname))
        {
            Status = PlayerPrefs.GetInt(PPname);
        }
        else
        {
            PlayerPrefs.SetInt(PPname, 0);
            Status = PlayerPrefs.GetInt(PPname);
        }
    }
}
