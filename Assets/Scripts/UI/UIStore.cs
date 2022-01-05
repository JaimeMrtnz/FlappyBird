using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlayFabInventoryManager;

/// <summary>
/// Controls the store UI
/// </summary>
public class UIStore : UIPanel
{
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private Transform content;

    private PlayFabPurchaseManager playFabPurchase;
    private UserInventory inventory;
    private List<UIItemObjController> itemsSpawned;
    private IParser parser;

    protected override void Initialize()
    {
        base.Initialize();

        itemsSpawned = new List<UIItemObjController>();
        parser = new UnityJsonUtilityAdapter();

        Hide();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnCatalogItemsReceived.AddListener(OnStoreItemsReceived);
        EventsManager.OnItemPurchased.AddListener(OnItemPurchased);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnCatalogItemsReceived.RemoveListener(OnStoreItemsReceived);
        EventsManager.OnItemPurchased.RemoveListener(OnItemPurchased);
    }

    /// <summary>
    /// On store items received event listener
    /// </summary>
    /// <param name="arg0"></param>
    private void OnStoreItemsReceived(PlayFabPurchaseManager playFabPurchase, UserInventory inventory)
    {
        this.playFabPurchase = playFabPurchase;
        this.inventory = inventory;
        RefreshItems();
    }

    /// <summary>
    /// On item purchase event listener
    /// </summary>
    /// <param name="item"></param>
    /// <param name="purchased"></param>
    private void OnItemPurchased(ItemInstance item)
    {
        RefreshItems();
    }

    /// <summary>
    /// Refreshes items in store
    /// </summary>
    private void RefreshItems()
    {
        foreach (var item in playFabPurchase.Items)
        {
            UIItemObjController itemObj = null;

            var itemsFound = itemsSpawned.Where(x => x.ItemID.Equals(item.ItemId)).ToList();

            if (itemsFound.Count > 0)
            {
                itemObj = itemsFound.First();
            }

            if(itemObj == null)
            {
                itemObj = Instantiate(itemPrefab, content).GetComponent<UIItemObjController>();
            }

            itemObj.ButtonText = item.DisplayName;

            itemObj.ItemID = item.ItemId;

            itemObj.Initialize();

            itemObj.SetPrice(item.VirtualCurrencyPrices);

            itemObj.SetPurchased(item.ItemId, item.Consumable, inventory.Inventory.Any(x => x.ItemId.Equals(item.ItemId))); ;
        
            itemsSpawned.Add(itemObj);
        }
    }
}
