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

    /// <summary>
    /// On login success event handler
    /// </summary>
    /// <param name="result"></param>
    private void OnLoginSuccess(LoginResult result)
    {
        authenticationContext = result.AuthenticationContext;
    }

    /// <summary>
    /// on item purchased event handler
    /// </summary>
    /// <param name="item"></param>
    /// <param name="catalogItem"></param>
    private void OnItemPurchased(ItemInstance item, CatalogItem_CatalogCustomData catalogItem)
    {
        var itemComponent = new ItemComponents()
        {
            Item = item
        };

        RefreshByItem(itemComponent, catalogItem, false);
    }

    /// <summary>
    /// OnCatalog item received event handler
    /// </summary>
    /// <param name="playFabPurchaseManager"></param>
    /// <param name="inventory"></param>
    private void OnCatalogItemsReceived(PlayFabPurchaseManager playFabPurchaseManager, Dictionary<string, ItemComponents> inventory)
    {
        RefreshPowerUpsInventory(inventory, playFabPurchaseManager.ItemsCollection);
    }

    /// <summary>
    /// Timer count down event handler
    /// </summary>
    /// <param name="itemId"></param>
    private void OnTimerCoundDownFinished(string itemId)
    {
        var itemComponent = new ItemComponents()
        {
            Item = powerUpsPurchased[itemId]
        };
        RefreshByItem(itemComponent, items[itemId], true);
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

    /// <summary>
    /// Makes refresh power ups item by item
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="items"></param>
    private void RefreshPowerUpsInventory(Dictionary<string, ItemComponents> inventory, Dictionary<string, CatalogItem_CatalogCustomData> items)
    {
        this.items = items;
        foreach (var item in inventory)
        {
            RefreshByItem(item.Value, items[item.Value.Item.ItemId], !item.Value.GoalTime.HasValue);
        }
    }

    /// <summary>
    /// Resfres by item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="catalogItem"></param>
    /// <param name="timerFinished"></param>
    private void RefreshByItem(ItemComponents item, CatalogItem_CatalogCustomData catalogItem, bool timerFinished)
    {
        powerUpsPurchased.TryAdd(item.Item.ItemId, item.Item);

        switch (item.Item.ItemId)
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
                PlayFabInventoryManager.ConsumeItem(item.Item.ItemInstanceId, 1);
                break;

            case "redBird":

                //if (item.GoalTime.HasValue)
                //{
                //    if (timersManager.HasExpired(item.GoalTime))
                //    {
                //        EventsManager.OnNewSkin.Invoke(1);
                //    }
                //    else
                //    {

                //        var test = (item.GoalTime - DateTime.UtcNow).Value.TotalSeconds;

                //        timersManager.NewPartialTimer(item, item.GoalTime);
                //    }

                    if (!timerFinished)
                    {
                        itemsQueueManager.SetItemTimer(authenticationContext, catalogItem, item);
                    }
                    else
                    {
                        EventsManager.OnNewSkin.Invoke(1);
                    }
                //}

                break;
        }
    }
}
