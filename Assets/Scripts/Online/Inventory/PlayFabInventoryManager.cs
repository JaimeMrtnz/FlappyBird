using PlayFab;
using PlayFab.ClientModels;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Manages the retrieval of the inventory
/// </summary>
public class PlayFabInventoryManager 
{
    /// <summary>
    /// Returns the user inventory
    /// </summary>
    /// <returns></returns>
    public static void GetUserInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
        successResult => OnGetUserInventorySuccess(successResult),
        error => OnGetUserInventoryError(error)
        );
    }

    /// <summary>
    /// Adds the user more currency
    /// ATTENTION: this is done here due to is was asked not to use CloudScript
    /// This could allow users cheating by changing currencies values
    /// </summary>
    public static void AddUserVirtualCurrency(int amount, string currency)
    {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest()
        {
            Amount = amount,
            VirtualCurrency = currency,
        },
        successResult => OnAddVirtualCurrencySuccess(successResult),
        error => OnAddVirtualCurrencyError(error));
    }

    /// <summary>
    /// Consumes an item
    /// </summary>
    /// <param name="itemInstanceId"></param>
    /// <param name="amount"></param>
    public static void ConsumeItem(string itemInstanceId, int amount)
    {
        PlayFabClientAPI.ConsumeItem(new ConsumeItemRequest()
        {
            ItemInstanceId = itemInstanceId,
            ConsumeCount = amount
        },
        successResult => OnConsumeItemSuccess(successResult),
        error => OnCosumeItemError(error));
    }

    private static void OnAddVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult successResult)
    {
        EventsManager.OnRefreshInventory.Invoke();
    }

    private static void OnAddVirtualCurrencyError(PlayFabError error)
    {
        Debug.Log("Error adding virtual currency: ");
        Debug.Log(error.ErrorMessage);
    }

    private static void OnGetUserInventorySuccess(GetUserInventoryResult successResult)
    {
        var userInventory = new UserInventory(successResult.Inventory, successResult.VirtualCurrency);
        EventsManager.OnGetUserInventorySuccess.Invoke(userInventory);
    }

    private static void OnGetUserInventoryError(PlayFabError error)
    {
        Debug.Log("Error retrieving inventory items: ");
        Debug.Log(error.ErrorMessage);
    }

    private static void OnConsumeItemSuccess(ConsumeItemResult result)
    {
        EventsManager.OnRefreshInventory.Invoke();
    }

    private static void OnCosumeItemError(PlayFabError error)
    {
        Debug.Log("Error consuming inventory item: ");
        Debug.Log(error.ErrorMessage);
    }
}
