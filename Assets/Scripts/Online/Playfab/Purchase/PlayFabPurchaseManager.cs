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
    private Dictionary<string, CatalogItem> items;

    public List<CatalogItem> Items { get => items.Values.ToList(); }
    public string SoftCurrency { get => softCurrency; }
    public string HardCurrency { get => hardCurrency; }

    public PlayFabPurchaseManager(string softCurrency, string hardCurrency, string storeId, List<CatalogItem> items)
    {
        this.softCurrency     = softCurrency;
        this.hardCurrency     = hardCurrency;
        this.storeId          = storeId;
        this.items            = new Dictionary<string, CatalogItem>();

        items.ForEach(x => this.items.Add(x.ItemId, x));
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
            CatalogVersion = items[itemID].CatalogVersion,
            VirtualCurrency = items[itemID].VirtualCurrencyPrices.FirstOrDefault().Key,
            Price = (int)items[itemID].VirtualCurrencyPrices.FirstOrDefault().Value,
        },
        Purchased => OnSuccess(Purchased),
        Error => OnError(Error));
    }

    private void OnSuccess(PurchaseItemResult successResult)
    {
        EventsManager.OnItemPurchased.Invoke(successResult.Items.FirstOrDefault());
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error purchasing item: ");
        Debug.Log(error.ErrorMessage);
    }
}
