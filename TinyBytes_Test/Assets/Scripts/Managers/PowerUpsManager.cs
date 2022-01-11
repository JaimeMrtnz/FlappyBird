using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages powerups states and execution
/// </summary>
public class PowerUpsManager : MonoBehaviour
{
    [SerializeField]
    private float speedUpTime;

    private Dictionary<string, ItemInstance> powerUpsPurchased;

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
        EventsManager.OnItemPurchased.AddListener(OnItemPurchased);
        EventsManager.OnCatalogItemsReceived.AddListener(OnCatalogItemsReceived);
        EventsManager.OnSpeedUpClicked.AddListener(OnSpeedUpClicked);
    }

    private void RemoveListeners()
    {
        EventsManager.OnItemPurchased.RemoveListener(OnItemPurchased);
        EventsManager.OnCatalogItemsReceived.RemoveListener(OnCatalogItemsReceived);
        EventsManager.OnSpeedUpClicked.RemoveListener(OnSpeedUpClicked);
    }

    private void OnItemPurchased(ItemInstance item)
    {
        FilterByItem(item);
    }

    private void OnCatalogItemsReceived(PlayFabPurchaseManager playFabPurchaseManager, List<ItemInstance> inventory)
    {
        RefreshPowerUpsInventory(inventory);
    }

    private void RefreshPowerUpsInventory(List<ItemInstance> inventory)
    {
        foreach (var item in inventory)
        {
            FilterByItem(item);
        }
    }

    private void FilterByItem(ItemInstance item)
    {
        switch (item.ItemId)
        {
            case "doublePoints":
                powerUpsPurchased.TryAdd(item.ItemId, item);
                EventsManager.OnDoublePointsPurchased.Invoke();
                break;

            case "triplePoints":
                powerUpsPurchased.TryAdd(item.ItemId, item);
                EventsManager.OnTriplePointsPurchased.Invoke();
                break;

            case "speedUp":
                powerUpsPurchased.TryAdd(item.ItemId, item);
                EventsManager.OnSpeedUpPurchased.Invoke();
                break;

            case "5Gems":
                powerUpsPurchased.TryAdd(item.ItemId, item);
                EventsManager.OnGemsWon.Invoke(5);
                PlayFabInventoryManager.ConsumeItem(item.ItemInstanceId, 1);
                break;
        }
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
}
