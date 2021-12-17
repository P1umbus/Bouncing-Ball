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
    [SerializeField] private Image Ball;
    [SerializeField] private ParticleSystem SellParticle;
    //[SerializeField] private Rarity Rar;
    [SerializeField] private Text PriceText;
    [SerializeField] private bool CanSell = true;
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
            StartCoroutine(ISell());
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
    public bool IsItemBougnt()
    {
        return isPurchased;
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
    IEnumerator ISell()
    {
        SellParticle.Play();
        yield return new WaitForSeconds(1f);
        SellParticle.Stop();
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
            BgImage.color = new Color(0.8396226f, 0.7873442f, 0.7873442f);
        }
        else if (_rarity == Constants.Rarity.Rare)
        {
            BgImage.color = new Color(0.2941177f, 0.4117647f, 1f);
        }
        else if (_rarity == Constants.Rarity.Mythical)
        {
            BgImage.color = new Color(0.632f, 0.256f, 1f);
        }
    }
    private void OnDestroy()
    {
        GameEvent.ChangeMaterial -= CheckStatus;
    }
    [ContextMenu("UpdateUI")]
    private void UpdateUI()
    {
        Undo.RecordObject(BgImage, "test");
        Undo.RecordObject(PriceText, "test");
        PriceText.text = Price.ToString();
        Ball.sprite = BallImmage;
        RarCheck();
    }
}
