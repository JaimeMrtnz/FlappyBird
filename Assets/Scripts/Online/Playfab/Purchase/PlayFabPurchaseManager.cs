using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the store and purchases
/// </summary>
public class PlayFabPurchaseManager 
{
    private string softCurrency;
    private string hardCurrency;
    private string storeId;
    private Dictionary<string, CatalogItem_CatalogCustomData> items;
    private IParser parser;

    public List<CatalogItem> Items { get => items.Values.Select(x => x.CatalogItem).ToList(); }
    public string SoftCurrency { get => softCurrency; }
    public string HardCurrency { get => hardCurrency; }
    public Dictionary<string, CatalogItem_CatalogCustomData> ItemsCollection { get => items; }

    public PlayFabPurchaseManager(string softCurrency, string hardCurrency, string storeId, List<CatalogItem> items, IParser parser)
    {
        this.softCurrency     = softCurrency;
        this.hardCurrency     = hardCurrency;
        this.storeId          = storeId;
        this.items            = new Dictionary<string, CatalogItem_CatalogCustomData>();
        this.parser = parser;

        foreach (var item in items)
        {
            var catalogItem_catalogCustomData = new CatalogItem_CatalogCustomData()
            {
                CatalogItem = item,
                CustomData = parser.Deserialize<CatalogItemCustomData>(item.CustomData)
            };
            
            this.items.Add(item.ItemId, catalogItem_catalogCustomData);
        }
    }

    /// <summary>
    /// Makes a purchase
    /// </summary>
    /// <param name="itemID"></param>
    public void PurchaseItem(string itemID)
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {
            StoreId = storeId,
            ItemId = itemID,
            CatalogVersion = items[itemID].CatalogItem.CatalogVersion,
            VirtualCurrency = items[itemID].CatalogItem.VirtualCurrencyPrices.FirstOrDefault().Key,
            Price = (int)items[itemID].CatalogItem.VirtualCurrencyPrices.FirstOrDefault().Value,
        },
        Purchased => OnSuccess(Purchased),
        Error => OnError(Error));
    }

    private void OnSuccess(PurchaseItemResult successResult)
    {
        var item = successResult.Items.FirstOrDefault();
        EventsManager.OnItemPurchased.Invoke(item, items[item.ItemId]);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error purchasing item: ");
        Debug.Log(error.ErrorMessage);
        EventsManager.OnError.Invoke(error.ErrorMessage);
    }
}
