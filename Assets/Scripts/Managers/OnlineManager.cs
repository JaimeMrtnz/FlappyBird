using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages every online step
/// </summary>
public class OnlineManager : MonoBehaviour
{
    [SerializeField]
    private ItemsQueueManager itemsQueueManager;

    private string playFabId;
    private PlayFabAuthenticationContext authenticationContext;
    private PlayFabDataRetriever playFabDataRetriever;
    private PlayFabPurchaseManager playFabPurchase;

    private UserInventory inventory;
    private InitialTitleData titleData;
    private Dictionary<string, DateTime> timers;
    private Dictionary<string, UserDataRecord> userData;


    private void Awake()
    {
        Initialize();
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Initialize()
    {
        PlayFabLoginManager.LogIn();
    }

    private void AddListeners()
    {
        EventsManager.OnLoginSuccess.AddListener(OnLoginSuccess);
        EventsManager.OnInitialTitleDataRetrieved.AddListener(OnInitialTitleDataRetrieved);
        EventsManager.OnUserDataRetrieved.AddListener(OnUserDataRetrieved);
        EventsManager.OnItemClicked.AddListener(OnItemClicked);
        EventsManager.OnGoldCoinsWon.AddListener(OnGoldCoinsWon);
        EventsManager.OnGemsWon.AddListener(OnGemsWon);
        EventsManager.OnItemPurchased.AddListener(OnItemPurchased);
        EventsManager.OnRefreshInventory.AddListener(OnRefreshInventory);
        EventsManager.OnGetUserInventorySuccess.AddListener(OnGetUserInventorySuccess);
        EventsManager.OnTimerCoundDownFinished.AddListener(OnTimerCoundDownFinished);
    }

    private void RemoveListeners()
    {
        EventsManager.OnLoginSuccess.RemoveListener(OnLoginSuccess);
        EventsManager.OnInitialTitleDataRetrieved.RemoveListener(OnInitialTitleDataRetrieved);
        EventsManager.OnUserDataRetrieved.RemoveListener(OnUserDataRetrieved);
        EventsManager.OnItemClicked.RemoveListener(OnItemClicked);
        EventsManager.OnGoldCoinsWon.RemoveListener(OnGoldCoinsWon);
        EventsManager.OnGemsWon.RemoveListener(OnGemsWon);
        EventsManager.OnItemPurchased.RemoveListener(OnItemPurchased);
        EventsManager.OnRefreshInventory.RemoveListener(OnRefreshInventory);
        EventsManager.OnGetUserInventorySuccess.RemoveListener(OnGetUserInventorySuccess);
        EventsManager.OnTimerCoundDownFinished.RemoveListener(OnTimerCoundDownFinished);
    }


    /// <summary>
    /// On login success event listener
    /// </summary>
    private void OnLoginSuccess(LoginResult result)
    {
        playFabId = result.PlayFabId;
        authenticationContext = result.AuthenticationContext;
        playFabDataRetriever = new PlayFabDataRetriever("initialUserData_V1_0_0", new UnityJsonUtilityAdapter());
        playFabDataRetriever.RetrieveTitleData();
    }

    /// <summary>
    /// On initial user data received
    /// </summary>
    /// <param name="initialtitleData"></param>
    private async void OnInitialTitleDataRetrieved(InitialTitleData initialtitleData)
    {
        titleData = initialtitleData;

        var items = await PlayFabCatalogManager.GetItems(titleData);
        playFabPurchase = new PlayFabPurchaseManager(titleData.SoftCurrency, titleData.HardCurrency, titleData.StoreId, items, new UnityJsonUtilityAdapter());
        playFabDataRetriever.RetrieveUserData(playFabId);
    }

    /// <summary>
    /// On data user received
    /// </summary>
    /// <param name="userData"></param>
    private void OnUserDataRetrieved(Dictionary<string, UserDataRecord> userData)
    {
        this.userData = userData;
        RefreshInventory();
    }

    /// <summary>
    /// On gold coins won
    /// </summary>
    /// <param name="value"></param>
    private void OnGoldCoinsWon(uint value)
    {
        /// ATTENTION: this is done here due to is was asked not to use CloudScript
        /// This could allow users cheating by changing currencies values
        PlayFabInventoryManager.AddUserVirtualCurrency((int)value, playFabPurchase.SoftCurrency);
    }

    /// <summary>
    /// On gems won
    /// </summary>
    /// <param name="value></param>
    private void OnGemsWon(uint value)
    {
        /// ATTENTION: this is done here due to is was asked not to use CloudScript
        /// This could allow users cheating by changing currencies values
        PlayFabInventoryManager.AddUserVirtualCurrency((int)value, playFabPurchase.HardCurrency);
    }

    /// <summary>
    /// Purchases an item
    /// </summary>
    /// <param name="itemID"></param>
    private void OnItemClicked(string itemID)
    {
        if (itemsQueueManager.CanAddItem())
        {
            playFabPurchase.PurchaseItem(itemID); 
        }
        else
        {
            EventsManager.OnError.Invoke(string.Format("Only {0} items at the same time", itemsQueueManager.QueueSize));
        }
    }

    /// <summary>
    /// Item purchased event listener
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="purchased"></param>
    private void OnItemPurchased(ItemInstance item, CatalogItem_CatalogCustomData catalogItem)
    {
        RefreshInventory();
    }

    private void OnTimerCoundDownFinished(string itemId)
    {
        new PlayFabTimer().RemoveItemTimer(authenticationContext, itemId);
    }

    /// <summary>
    /// Refreshes the inventory
    /// </summary>
    private void OnRefreshInventory()
    {
        RefreshInventory();
    }

    /// <summary>
    /// Refreshes the user inventory currencies with server's data
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    private void RefreshInventory()
    {
        PlayFabInventoryManager.GetUserInventory();
    }

    /// <summary>
    /// Get user inventory event handler success
    /// </summary>
    /// <param name="userInventory"></param>
    private void OnGetUserInventorySuccess(UserInventory userInventory)
    {
        inventory = userInventory;
        timers = UserDataFilter.GetTimers(userData);
        inventory.SetTimers(timers);

        EventsManager.OnCatalogItemsReceivedStoreUI.Invoke(playFabPurchase, inventory.Inventory);
        EventsManager.OnCatalogItemsReceived.Invoke(playFabPurchase, inventory.Inventory);
        EventsManager.OnGoldCoinsReceived.Invoke((uint)inventory.CurrenciesBalances[playFabPurchase.SoftCurrency]);
        EventsManager.OnGemsReceived.Invoke((uint)inventory.CurrenciesBalances[playFabPurchase.HardCurrency]);

        EventsManager.OnGameLoaded.Invoke();
    }
}
