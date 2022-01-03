using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private int Price;
    [SerializeField] private int SkinNumb;
    [SerializeField] private Sprite BallImmage;
    [SerializeField] private Image BgImage;
    [SerializeField] private Image BgButtonImage;
    [SerializeField] private Image BgButtonImage2;
    [SerializeField] private Image Ball;
    //[SerializeField] private Rarity Rar;
    [SerializeField] private Text PriceText;
    [SerializeField] private bool CanSell = true;
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
        GameEvent.ChangeMaterial += CheckStatus;
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
            CoinBuyTween.OnBuy(this.gameObject);
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
                GameEvent.TrySell.Invoke(this);
            }
        }        
    }
    public void Sell()
    {
        if (isPurchased == true)
        {
            GameEvent.SoundEvents.Shop.Sell?.Invoke();
            CoinSellTween.OnSell(this.gameObject);
            if (IsSelectedSkin() == true)
            {
                PlayerPrefs.SetInt(PPname, 0);
                Bank.instance.PluralIncreaseCoinNumb(SellPrice);
                PlayerPrefs.SetInt("SelectedSkin", 0);
                GameEvent.SkinsUpdate?.Invoke();
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
    public bool IsItemBougnt()
    {
        return isPurchased;
    }
    private void Select()
    {
        PlayerPrefs.SetInt("SelectedSkin", SkinNumb);
        GameEvent.ChangeMaterial?.Invoke();
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
    //private enum Rarity
    //{
    //    Common,
    //    Rare,
    //    Mythical
    //}
    //public string GetRarity()
    //{
    //    return Rar.ToString();
    //}
    private void RarCheck()
    {
        if (_rarity == Constants.Rarity.Common)
        {
            BgImage.color = CommonColor;
            BgButtonImage.color = CommonColor;
            BgButtonImage2.color = CommonColor;
        }
        else if (_rarity == Constants.Rarity.Rare)
        {
            BgImage.color =  RareColor;
            BgButtonImage.color = RareColor;
            BgButtonImage2.color = RareColor;
        }
        else if (_rarity == Constants.Rarity.Mythical)
        {
            BgImage.color = MythicalColor;
            BgButtonImage.color = MythicalColor;
            BgButtonImage2.color = MythicalColor;
        }
    }
    private void OnDestroy()
    {
        GameEvent.ChangeMaterial -= CheckStatus;
    }

    #if UNITY_EDITOR
    [ContextMenu("UpdateUI")]
    private void UpdateUI()
    {
        Undo.RecordObject(BgImage, "test");
        Undo.RecordObject(BgButtonImage, "test");
        Undo.RecordObject(BgButtonImage2, "test");
        Undo.RecordObject(PriceText, "test");
        PriceText.text = Price.ToString();
        Ball.sprite = BallImmage;
        RarCheck();
    }
    #endif
}
