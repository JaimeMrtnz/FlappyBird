using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private List<ItemInstance> inventory;
    private Dictionary<string, UIItemObjController> itemsSpawned;

    protected override void Initialize()
    {
        base.Initialize();

        itemsSpawned = new Dictionary<string, UIItemObjController>();

        Hide();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnCatalogItemsReceived.AddListener(OnStoreItemsReceived);
        EventsManager.OnItemTimerSuccess.AddListener(OnItemTimerSuccess);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnCatalogItemsReceived.RemoveListener(OnStoreItemsReceived);
        EventsManager.OnItemTimerSuccess.RemoveListener(OnItemTimerSuccess);
    }


    /// <summary>
    /// On store items received event listener
    /// </summary>
    /// <param name="arg0"></param>
    private void OnStoreItemsReceived(PlayFabPurchaseManager playFabPurchase, List<ItemInstance> inventory)
    {
        this.playFabPurchase = playFabPurchase;
        this.inventory = inventory;
        RefreshItems();
    }

    /// <summary>
    /// On item timer set handler
    /// </summary>
    /// <param name="arg0"></param>
    private void OnItemTimerSuccess(string itemId, float timer, DateTime time)
    {
        itemsSpawned[itemId].SetTimer(time);
    }

    /// <summary>
    /// Refreshes items in store
    /// </summary>
    private void RefreshItems()
    {
        foreach (var item in playFabPurchase.Items)
        {
            UIItemObjController itemObj = null;

            if(itemsSpawned.ContainsKey(item.ItemId))
            {
                itemObj = itemsSpawned[item.ItemId];
            }

            if(itemObj == null)
            {
                itemObj = Instantiate(itemPrefab, content).GetComponent<UIItemObjController>();
            }

            itemObj.ButtonText = item.DisplayName;

            itemObj.ItemID = item.ItemId;

            itemObj.Initialize();

            itemObj.SetPrice(item.VirtualCurrencyPrices);

            itemObj.SetPurchased(item.ItemId, item.Consumable, inventory.Any(x => x.ItemId.Equals(item.ItemId))); ;
        
            itemsSpawned.TryAdd(item.ItemId, itemObj);
        }
    }
}
