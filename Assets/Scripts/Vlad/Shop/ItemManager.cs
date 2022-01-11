using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using Lean.Localization;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private int Price;
    [SerializeField] private int SkinNumb;
    [SerializeField] private Image[] RarBg;
    [SerializeField] private Text[] StatusText;
    [SerializeField] private LeanLocalToken PriceToken;
    [SerializeField] private CoinTween CoinBuyTween;
    [SerializeField] private CoinTween CoinSellTween;
    [SerializeField] private Color CommonColor;
    [SerializeField] private Color RareColor;
    [SerializeField] private Color MythicalColor;
    private int SellPrice;
    private int Status;
    private bool isPurchased = false;
    private string PPname;
  

    //Artem
    [SerializeField] private Constants.Rarity _rarity;
    public int Rarity => ((int)_rarity);

    private void Awake()
    {
        LoadDate();
    }

    private void Start()
    {
        CheckStatus();
        SellPrice = Price / 3;
    }

    public void Buy()
    {
        if (Bank.instance.IsEnough(Price) && isPurchased == false)
        {
            PlayerPrefs.SetInt(PPname, 1);
            PlayerPrefs.Save();
            Bank.instance.ReduceCoinNumb(Price);
            UpdateChange();
            GameEvent.SoundEvents.Shop.Buy?.Invoke();
            CoinBuyTween.OnBuy(this.gameObject.transform);
        }
        else if (isPurchased == true)
        {
            Select();
        }
    }
    public void TrySell()
    {
        if (isPurchased == true)
        {
            GameEvent.TrySell?.Invoke(this);
        }      
    }
    public void Sell()
    {
        if (isPurchased == true)
        {
            GameEvent.SoundEvents.Shop.Sell?.Invoke();
            CoinSellTween.OnSell(this.gameObject.transform);
            if (IsSelectedSkin() == true)
            {
                PlayerPrefs.SetInt(PPname, 0);
                Bank.instance.PluralIncreaseCoinNumb(SellPrice);
                PlayerPrefs.SetInt("SelectedSkin", 0);
                GameEvent.SkinsUpdate?.Invoke();
                UpdateChange();
                CheckStatus();
            }
            else
            {
                PlayerPrefs.SetInt(PPname, 0);
                UpdateChange();
                StatusText[0].text = Price.ToString();
                Bank.instance.PluralIncreaseCoinNumb(SellPrice);
            }
        }
    }
    public int GetSellPrice()
    {
        return SellPrice;
    }
    public bool IsItemBougnt()
    {
        return isPurchased;
    }
    private void Select()
    {
        PlayerPrefs.SetInt("SelectedSkin", SkinNumb);
        CheckStatus();
        GameEvent.SkinsUpdate?.Invoke();
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
            UpdateStatus(0);
            PriceToken.SetValue(Price);
            isPurchased = false;
        }
        else if(Status == 1)
        {
            if (IsSelectedSkin())
            {
                UpdateStatus(1);
            }
            else
            {
                UpdateStatus(2);
            }

            isPurchased = true;

        }
    }
    private void UpdateStatus(int activeText)
    {

        for (int i = 0; i < StatusText.Length; i++)
        {
            if(i != activeText)
            {
                StatusText[i].gameObject.SetActive(false);
            }
            else
            {
                StatusText[i].gameObject.SetActive(true);
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
    private void RarCheck()
    {
        if (_rarity == Constants.Rarity.Common)
        {
            UpdateBgColor(CommonColor);
        }
        else if (_rarity == Constants.Rarity.Rare)
        {
            UpdateBgColor(RareColor);
        }
        else if (_rarity == Constants.Rarity.Mythical)
        {
            UpdateBgColor(MythicalColor);
        }
    }
    private void UpdateBgColor(Color activeColor)
    {
        foreach (Image bg in RarBg)
        {
            bg.color = activeColor;
        }
    }
    #if UNITY_EDITOR
    [ContextMenu("UpdateUI")]
    private void UpdateUI()
    {
        foreach (Image bg in RarBg)
        {
            Undo.RecordObject(bg, "test");
        }
        Undo.RecordObject(StatusText[0], "test");
        StatusText[0].text = Price.ToString();
        RarCheck();
    }
    #endif
}
