using PlayFab.ClientModels;
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

    private string itemID;

    public string ButtonText { set => buttonText.text = value; }
    public string ItemID { get => itemID; set => itemID = value; }

    public void Initialize()
    {
        goldCoinContent.SetActive(false);
        hardCurrencyContent.SetActive(false);
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
        if (this.itemID.Equals(itemID))
        {
            if (consumableInfo.UsageCount == null)
            {
                background.color = purchased ? purchasedColor : notPurchasedColor;
                button.interactable = purchased ? false : true;
            }
            else
            {

            }
        }
    }

    /// <summary>
    /// On item click
    /// </summary>
    public void OnClick()
    {
        EventsManager.OnItemClicked.Invoke(itemID);
    }
}
