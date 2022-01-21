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
    private Dictionary<string, ItemComponents> inventory;
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

        EventsManager.OnCatalogItemsReceivedStoreUI.AddListener(OnCatalogItemsReceived);
        EventsManager.OnItemTimerSuccess.AddListener(OnTimerSetUp);
        EventsManager.OnPartialTimerAdded.AddListener(OnTimerSetUp);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnCatalogItemsReceivedStoreUI.RemoveListener(OnCatalogItemsReceived);
        EventsManager.OnItemTimerSuccess.RemoveListener(OnTimerSetUp);
        EventsManager.OnPartialTimerAdded.RemoveListener(OnTimerSetUp);
    }


    /// <summary>
    /// On store items received event listener
    /// </summary>
    /// <param name="playFabPurchase"></param>
    /// <param name="inventory"></param>
    private void OnCatalogItemsReceived(PlayFabPurchaseManager playFabPurchase, Dictionary<string, ItemComponents> inventory)
    {
        this.playFabPurchase = playFabPurchase;
        this.inventory = inventory;
        RefreshItems();
    }

    /// <summary>
    /// On item timer set handler
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="timer"></param>
    /// <param name="time"></param>
    private void OnTimerSetUp(string itemId, float time, DateTime? timer)
    {
        itemsSpawned[itemId].SetTimer(timer.Value);
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

            itemObj.SetPurchased(item.ItemId, item.Consumable, inventory.Any(x => x.Key.Equals(item.ItemId))); ;
        
            itemsSpawned.TryAdd(item.ItemId, itemObj);
        }
    }
}
