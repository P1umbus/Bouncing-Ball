using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private int Price;
    [SerializeField] private int SellPrice;
    [SerializeField] private int SkinNumb;
    [SerializeField] private Text PriceText;
    [SerializeField] private bool CanSell = true;
    private int Status;
    private bool isPurchased = false;
    private string PPname;

    private void Awake()
    {

        LoadDate();
        GameEvent.ChangeMaterial += CheckStatus;

    }

    void Start()
    {
        CheckStatus();
    }

    public void Buy()
    {
        Debug.Log("TruBuy");
        if (Bank.instance.IsEnough(Price) && isPurchased == false)
        {
            Debug.Log("TruBuy1");
            PlayerPrefs.SetInt(PPname, 1);
            PlayerPrefs.Save();
            Bank.instance.ReduceCoinNumb(Price);
            UpdateChange();
            GameEvent.SoundEvents.Shop.Buy?.Invoke();
        }
        else if (isPurchased == true)
        {
            Select();
        }
    }
    public void TrySell()
    {
        if(CanSell == true)
        {
            if (isPurchased == true)
            {
                GameEvent.TrySell?.Invoke(this);
            }
        }
        
    }
    public void Sell()
    {
        if (isPurchased == true)
        {
            GameEvent.SoundEvents.Shop.Sell?.Invoke();
            if (IsSelectedSkin() == true)
            {
                PlayerPrefs.SetInt(PPname, 0);
                Bank.instance.PluralIncreaseCoinNumb(SellPrice);
                PlayerPrefs.SetInt("SelectedSkin", 0);
                UpdateChange();
                GameEvent.ChangeMaterial();
            }
            else
            {
                PlayerPrefs.SetInt(PPname, 0);
                UpdateChange();
                PriceText.text = Price.ToString();
                Bank.instance.PluralIncreaseCoinNumb(SellPrice);
            }

        }

    }
    public int GetSellPrice()
    {
        return SellPrice;
    }
    private void Select()
    {
        PlayerPrefs.SetInt("SelectedSkin", SkinNumb);
        GameEvent.ChangeMaterial?.Invoke();
    }
    private bool IsSelectedSkin()
    {
        var a = PlayerPrefs.GetInt("SelectedSkin");
        if (SkinNumb == a)
        {
            return true;
        }
        return false;
    }
    private void CheckStatus()
    {
        if(Status == 0)
        {
            PriceText.text = Price.ToString();
            isPurchased = false;
        }
        else if(Status == 1)
        {
            if (IsSelectedSkin())
            {
                PriceText.text = "Taken";
                isPurchased = true;
            }
            else
            {
                PriceText.text = "Bought";
                isPurchased = true;
            }
           
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
    private void UpdateChange()
    {
        LoadDate();
        CheckStatus();
    }
}
