using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

/// <summary>
/// Manages the store and purchases
/// </summary>
public class PlayFabPurchase 
{
    private List<StoreItem> storeItems;

    public PlayFabPurchase(List<StoreItem> storeItems)
    {
        this.storeItems = storeItems;
    }

    public List<StoreItem> StoreItems { get => storeItems; }

    /// <summary>
    /// Makes a purchase
    /// </summary>
    /// <param name="itemID"></param>
    public static void PurchaseItem(string itemID)
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {

        },
        Purchased => { },
        Error => { });
    }
}
