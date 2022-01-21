using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages powerups states and execution
/// </summary>
public class PowerUpsManager : MonoBehaviour
{
    [SerializeField]
    private float speedUpTime;

    [SerializeField]
    private ItemsQueueManager itemsQueueManager;

    private Dictionary<string, ItemInstance> powerUpsPurchased;
    private PlayFabAuthenticationContext authenticationContext;
    private Dictionary<string, CatalogItem_CatalogCustomData> items;

    private void Awake()
    {
        powerUpsPurchased = new Dictionary<string, ItemInstance>();
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        EventsManager.OnLoginSuccess.AddListener(OnLoginSuccess);
        EventsManager.OnItemPurchased.AddListener(OnItemPurchased);
        EventsManager.OnCatalogItemsReceived.AddListener(OnCatalogItemsReceived);
        EventsManager.OnSpeedUpClicked.AddListener(OnSpeedUpClicked);
        EventsManager.OnTimerCoundDownFinished.AddListener(OnTimerCoundDownFinished);
    }

    private void RemoveListeners()
    {
        EventsManager.OnLoginSuccess.RemoveListener(OnLoginSuccess);
        EventsManager.OnItemPurchased.RemoveListener(OnItemPurchased);
        EventsManager.OnCatalogItemsReceived.RemoveListener(OnCatalogItemsReceived);
        EventsManager.OnSpeedUpClicked.RemoveListener(OnSpeedUpClicked);
        EventsManager.OnTimerCoundDownFinished.RemoveListener(OnTimerCoundDownFinished);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        authenticationContext = result.AuthenticationContext;
    }

    private void OnItemPurchased(ItemInstance item, CatalogItem_CatalogCustomData catalogItem)
    {
        FilterByItem(item, catalogItem);
    }

    private void OnCatalogItemsReceived(PlayFabPurchaseManager playFabPurchaseManager, List<ItemInstance> inventory)
    {
        RefreshPowerUpsInventory(inventory, playFabPurchaseManager.ItemsCollection);
    }

    /// <summary>
    /// Timer count down event handler
    /// </summary>
    /// <param name="itemId"></param>
    private void OnTimerCoundDownFinished(string itemId)
    {
        FilterByItem(powerUpsPurchased[itemId], items[itemId], true);
    }

    /// <summary>
    /// Speed up clicked button event listener
    /// </summary>
    private void OnSpeedUpClicked()
    {
        EventsManager.OnSpeedUp.Invoke();
        PlayFabInventoryManager.ConsumeItem(powerUpsPurchased["speedUp"].ItemInstanceId, 1);
        Invoke("OnFinishSpeedUp", speedUpTime);
    }

    /// <summary>
    /// Finishes speed up and returns to normal speed
    /// </summary>
    private void OnFinishSpeedUp()
    {
        EventsManager.OnFinishSpeedUp.Invoke();
    }
    private void RefreshPowerUpsInventory(List<ItemInstance> inventory, Dictionary<string, CatalogItem_CatalogCustomData> items)
    {
        this.items = items;
        foreach (var item in inventory)
        {
            FilterByItem(item, items[item.ItemId]);
        }
    }

    private void FilterByItem(ItemInstance item, CatalogItem_CatalogCustomData catalogItem, bool timerCountDown = false)
    {
        powerUpsPurchased.TryAdd(item.ItemId, item);

        switch (item.ItemId)
        {
            case "doublePoints":
                EventsManager.OnDoublePointsPurchased.Invoke();
                break;

            case "triplePoints":
                EventsManager.OnTriplePointsPurchased.Invoke();
                break;

            case "speedUp":
                EventsManager.OnSpeedUpPurchased.Invoke();
                break;

            case "5Gems":
                EventsManager.OnGemsWon.Invoke(5);
                PlayFabInventoryManager.ConsumeItem(item.ItemInstanceId, 1);
                break;

            case "redBird":
                if (!timerCountDown)
                {
                    itemsQueueManager.SetItemTimer(authenticationContext, catalogItem); 
                }
                else
                {
                    EventsManager.OnNewSkin.Invoke(1);
                }
                break;
        }
    }
}
