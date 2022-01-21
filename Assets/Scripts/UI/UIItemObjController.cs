using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls an item in store UI
/// </summary>
public class UIItemObjController : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField]
    private TextMeshProUGUI buttonText;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Button button;

    [SerializeField]
    private RectTransform layoutRoot;

    [Header("Timer settings")]
    [SerializeField]
    private GameObject timer;

    [SerializeField]
    private Image timerImage;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [Header("Gold Coins")]
    [SerializeField]
    private GameObject goldCoinContent;

    [SerializeField]
    private TextMeshProUGUI goldCoinPriceText;


    [Header("Hard currency")]
    [SerializeField]
    private GameObject hardCurrencyContent;

    [SerializeField]
    private TextMeshProUGUI hardCurrencyPriceText;

    [Header("Colors")]
    [SerializeField]
    private Color notPurchasedColor;

    [SerializeField]
    private Color purchasedColor;

    private string itemId;

    //private float goalTime = 0;
    private DateTime goalTime;
    private float goalSecs = 0;

    private bool timerConnected = false;

    public string ButtonText { set => buttonText.text = value; }
    public string ItemID { get => itemId; set => itemId = value; }

    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    //private void Update()
    //{
        //if (timerConnected)
        //{
        //    var diff = goalTime - DateTime.UtcNow;
        //    if (diff.TotalSeconds > 0)
        //    {
        //        timerText.text = diff.ToString(@"hh\:mm\:ss");
        //        timerImage.fillAmount = (float)(diff.TotalSeconds / goalSecs);
        //    }
        //    else
        //    {
        //        timerText.text = "00:00:00";
        //        timerConnected = false;
        //        timer.SetActive(false);

        //        EventsManager.OnTimerCoundDownFinished.Invoke(itemId);
        //    } 
        //}
    //}

    public void Initialize()
    {
        goldCoinContent.SetActive(false);
        hardCurrencyContent.SetActive(false);
        timer.SetActive(false);
    }

    private void AddListeners()
    {
        EventsManager.OnTimerTick.AddListener(OnTimerTick);
        EventsManager.OnTimerCoundDownFinished.AddListener(OnTimerCoundDownFinished);
    }

    private void RemoveListeners()
    {
        EventsManager.OnTimerTick.RemoveListener(OnTimerTick);
        EventsManager.OnTimerCoundDownFinished.RemoveListener(OnTimerCoundDownFinished);
    }


    /// <summary>
    /// Sets the prices given virtual and real currencies
    /// </summary>
    /// <param name="virtualPrices"></param>
    /// <param name="hardPrices"></param>
    public void SetPrice(Dictionary<string, uint> virtualPrices)
    {
        foreach (var item in virtualPrices)
        {
            switch (item.Key)
            {
                case "GM":
                    hardCurrencyContent.SetActive(true);
                    hardCurrencyPriceText.text = item.Value.ToString();

                    break;

                case "GC":
                    goldCoinContent.SetActive(true);
                    goldCoinPriceText.text = item.Value.ToString();

                    break;
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRoot);
    }


    /// <summary>
    /// Allow interact with the button if not purchased
    /// </summary>
    /// <param name="purchased"></param>
    public void SetPurchased(string itemID, CatalogItemConsumableInfo consumableInfo, bool purchased)
    {
        if (this.itemId.Equals(itemID))
        {
            if (consumableInfo.UsageCount == null)
            {
                background.color = purchased ? purchasedColor : notPurchasedColor;
                button.interactable = purchased ? false : true;
            }
        }
    }

    /// <summary>
    /// Enables the timer in this item
    /// </summary>
    /// <param name="time"></param>
    public void SetTimer(DateTime time)
    {
        timer.SetActive(true);
        timerConnected = true;
        goalTime = time;
        goalSecs = (float)(goalTime - DateTime.UtcNow).TotalSeconds;
    }

    /// <summary>
    /// On timer tick events handler
    /// </summary>
    /// <param name="itemId"></param>
    private void OnTimerTick(string itemId)
    {
        if (this.itemId.Equals(itemId))
        {
            var diff = goalTime - DateTime.UtcNow;
            timerText.text = diff.ToString(@"hh\:mm\:ss");
            timerImage.fillAmount = (float)(diff.TotalSeconds / goalSecs);
        }
    }

    /// <summary>
    /// On timer countdown event handler
    /// </summary>
    /// <param name="itemId"></param>
    private void OnTimerCoundDownFinished(string itemId)
    {
        if (this.itemId.Equals(itemId))
        {
            timerText.text = "00:00:00";
            timerConnected = false;
            timer.SetActive(false);
        }
    }

    /// <summary>
    /// On item click
    /// </summary>
    public void OnClick()
    {
        EventsManager.OnItemClicked.Invoke(itemId);
    }
}
