using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using Lean.Localization;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private int _skinNumb;
    [SerializeField] private Image[] _rareBg;
    [SerializeField] private Text[] _statusText;
    [SerializeField] private LeanLocalToken _priceToken;
    [SerializeField] private CoinTween _coinBuyTween;
    [SerializeField] private CoinTween _coinSellTween;
    [SerializeField] private Color _commonColor;
    [SerializeField] private Color _rareColor;
    [SerializeField] private Color _mythicalColor;
    private int _sellPrice;
    private int _status;
    private bool isPurchased = false;
    private string PPname;
  

    //Artem
    [SerializeField] private Rarity _rarity;
    public int Rarity => ((int)_rarity);

    private void Awake()
    {
        LoadDate();
    }

    private void Start()
    {
        CheckStatus();
        _sellPrice = _price / 3;
    }

    public void Buy()
    {
        if (DataLoadSystem.GetLoader<Bank>("1").IsEnough(_price) && isPurchased == false)
        {
            PlayerPrefs.SetInt(PPname, 1);
            PlayerPrefs.Save();
            DataLoadSystem.GetLoader<Bank>("1").ReduceCoinNumb(_price);
            UpdateChange();
            GameEvent.SoundEvents.Shop.Buy?.Invoke();
            _coinBuyTween.OnBuy(this.gameObject.transform);
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
            _coinSellTween.OnSell(this.gameObject.transform);
            if (IsSelectedSkin() == true)
            {
                PlayerPrefs.SetInt(PPname, 0);
                DataLoadSystem.GetLoader<Bank>("1").PluralIncreaseCoinNumb(_sellPrice);
                PlayerPrefs.SetInt("SelectedSkin", 0);
                GameEvent.SkinsUpdate?.Invoke();
                UpdateChange();
                CheckStatus();
            }
            else
            {
                PlayerPrefs.SetInt(PPname, 0);
                UpdateChange();
                _statusText[0].text = _price.ToString();
                DataLoadSystem.GetLoader<Bank>("1").PluralIncreaseCoinNumb(_sellPrice);
            }
        }
    }
    public int GetSellPrice()
    {
        return _sellPrice;
    }
    public bool IsItemBougnt()
    {
        return isPurchased;
    }
    private void Select()
    {
        PlayerPrefs.SetInt("SelectedSkin", _skinNumb);
        CheckStatus();
        GameEvent.SkinsUpdate?.Invoke();
    }
    private bool IsSelectedSkin()
    {
        var a = PlayerPrefs.GetInt("SelectedSkin");
        if (_skinNumb == a)
        {
            return true;
        }
        return false;
    }
    private void CheckStatus()
    {
        if(_status == 0)
        {
            UpdateStatus(0);
            _priceToken.SetValue(_price);
            isPurchased = false;
        }
        else if(_status == 1)
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

        for (int i = 0; i < _statusText.Length; i++)
        {
            if(i != activeText)
            {
                _statusText[i].gameObject.SetActive(false);
            }
            else
            {
                _statusText[i].gameObject.SetActive(true);
            }
        }
    }
    private void LoadDate()
    {
        PPname = "Material" + _skinNumb;
        if (PlayerPrefs.HasKey(PPname))
        {
            _status = PlayerPrefs.GetInt(PPname);
        }
        else
        {
            PlayerPrefs.SetInt(PPname, 0);
            _status = PlayerPrefs.GetInt(PPname);
        }
    }
    private void UpdateChange()
    {
        LoadDate();
        CheckStatus();
    }
    private void RarCheck()
    {
        if (_rarity == global::Rarity.Common)
        {
            UpdateBgColor(_commonColor);
        }
        else if (_rarity == global::Rarity.Rare)
        {
            UpdateBgColor(_rareColor);
        }
        else if (_rarity == global::Rarity.Mythical)
        {
            UpdateBgColor(_mythicalColor);
        }
    }
    private void UpdateBgColor(Color activeColor)
    {
        foreach (Image bg in _rareBg)
        {
            bg.color = activeColor;
        }
    }
    #if UNITY_EDITOR
    [ContextMenu("UpdateUI")]
    private void UpdateUI()
    {
        foreach (Image bg in _rareBg)
        {
            Undo.RecordObject(bg, "test");
        }
        Undo.RecordObject(_statusText[0], "test");
        _statusText[0].text = _price.ToString();
        RarCheck();
    }
    #endif
}
