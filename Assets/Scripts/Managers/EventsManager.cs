using PlayFab.ClientModels;
using UnityEngine.Events;
using static PlayFabInventoryManager;

/// <summary>
/// Stores global events
/// </summary>
public class EventsManager
{
    // Core game
    public static UnityEvent OnGameStart                        = new UnityEvent();
    public static UnityEvent OnGameOver                         = new UnityEvent();
    public static UnityEvent OnReplay                           = new UnityEvent();
    public static UnityEvent OnPipeSucceeded                    = new UnityEvent();
    public static UnityEvent<uint> OnNewScore                   = new UnityEvent<uint>();
    public static UnityEvent<uint> OnNewTotalGoldCoins          = new UnityEvent<uint>();
    public static UnityEvent<uint> OnNewTotalGems               = new UnityEvent<uint>();
    public static UnityEvent<uint> OnGoldCoinsWon               = new UnityEvent<uint>();
    public static UnityEvent<uint> OnGemsWon                    = new UnityEvent<uint>();

    // Online
    public static UnityEvent OnLoginSuccess                                                         = new UnityEvent();
    public static UnityEvent<PlayFabPurchaseManager, UserInventory>  OnCatalogItemsReceived         = new UnityEvent<PlayFabPurchaseManager, UserInventory>();
    public static UnityEvent<InitialUserData> OnInitialUserDataRetrieved                            = new UnityEvent<InitialUserData>();
    public static UnityEvent<string> OnItemClicked                                                  = new UnityEvent<string>();
    public static UnityEvent<ItemInstance> OnItemPurchased                                          = new UnityEvent<ItemInstance>();
    public static UnityEvent<uint> OnGoldCoinsReceived                                              = new UnityEvent<uint>();
    public static UnityEvent<uint> OnGemsReceived                                                   = new UnityEvent<uint>();

    // Power ups
    public static UnityEvent OnDoublePointsPurchased    = new UnityEvent();
    public static UnityEvent OnTriplePointsPurchased    = new UnityEvent();

}
