using PlayFab.ClientModels;
using System;
using System.Threading.Tasks;
using UnityEngine;
using static PlayFabInventoryManager;

/// <summary>
/// Manages every online step
/// </summary>
public class OnlineManager : MonoBehaviour
{
    private PlayFabTitleDataRetriever playFabDataRetriever;
    private PlayFabPurchaseManager playFabPurchase;

    private UserInventory inventory;
    private InitialUserData userData;

    private void Awake()
    {
        Initialize();
        AddListeners();

        PlayFabLoginManager.LogIn();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Initialize()
    {
        playFabDataRetriever = new PlayFabTitleDataRetriever("initialUserData_V1_0_0", new UnityJsonUtilityAdapter());
    }

    private void AddListeners()
    {
        EventsManager.OnLoginSuccess.AddListener(OnLoginSuccess);
        EventsManager.OnInitialUserDataRetrieved.AddListener(OnInitialUserDataRetrieved);
        EventsManager.OnItemClicked.AddListener(OnItemClicked);
        EventsManager.OnGoldCoinsWon.AddListener(OnGoldCoinsWon);
        EventsManager.OnGemsWon.AddListener(OnGemsWon);
        EventsManager.OnItemPurchased.AddListener(OnItemPurchased);
    }

    private void RemoveListeners()
    {
        EventsManager.OnLoginSuccess.RemoveListener(OnLoginSuccess);
        EventsManager.OnInitialUserDataRetrieved.RemoveListener(OnInitialUserDataRetrieved);
        EventsManager.OnItemClicked.RemoveListener(OnItemClicked);
        EventsManager.OnGoldCoinsWon.RemoveListener(OnGoldCoinsWon);
        EventsManager.OnGemsWon.RemoveListener(OnGemsWon);
        EventsManager.OnItemPurchased.RemoveListener(OnItemPurchased);
    }

    /// <summary>
    /// On login success event listener
    /// </summary>
    private void OnLoginSuccess()
    {
        playFabDataRetriever.RetrieveTitleData();
    }

    /// <summary>
    /// On initial user data received
    /// </summary>
    /// <param name="initialUserData"></param>
    private async void OnInitialUserDataRetrieved(InitialUserData initialUserData)
    {
        userData = initialUserData;

        var items = await PlayFabCatalogManager.GetItems(userData);
        playFabPurchase = new PlayFabPurchaseManager(userData.SoftCurrency, userData.HardCurrency, userData.StoreId, items);
        await RefreshInventory();
    }

    /// <summary>
    /// On gold coins won
    /// </summary>
    /// <param name="arg0"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnGoldCoinsWon(uint value)
    {
        /// ATTENTION: this is done here due to is was asked not to use CloudScript
        /// This could allow users cheating by changing currencies values
        PlayFabInventoryManager.AddUserVirtualCurrency((int)value, playFabPurchase.SoftCurrency);
    }

    /// <summary>
    /// On gems won
    /// </summary>
    /// <param name="arg0"></param>
    /// <exception cref="NotImplementedException"></exception>
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
        playFabPurchase.PurchaseItem(itemID);
    }

    /// <summary>
    /// Item purchased event listener
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="purchased"></param>
    private async void OnItemPurchased(ItemInstance item)
    {
        if (item.RemainingUses != null)
        {
            PlayFabInventoryManager.ConsumeItem(item.ItemInstanceId, 1); 
        }
        await RefreshInventory();
    }

    /// <summary>
    /// Refreshes the user inventory currencies with server's data
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    private async Task RefreshInventory()
    {
        inventory = await PlayFabInventoryManager.GetUserInventory();
        EventsManager.OnCatalogItemsReceived.Invoke(playFabPurchase, inventory);
        EventsManager.OnGoldCoinsReceived.Invoke((uint)inventory.CurrenciesBalances[playFabPurchase.SoftCurrency]);
        EventsManager.OnGemsReceived.Invoke((uint)inventory.CurrenciesBalances[playFabPurchase.HardCurrency]);
    }
}
