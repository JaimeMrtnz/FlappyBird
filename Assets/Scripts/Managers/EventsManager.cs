using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.Events;

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
    public static UnityEvent OnSpeedUp                          = new UnityEvent();
    public static UnityEvent OnFinishSpeedUp                    = new UnityEvent();
    public static UnityEvent OnSpeedUpClicked                   = new UnityEvent();
    public static UnityEvent<string, uint> OnItemTimer          = new UnityEvent<string, uint>();
    public static UnityEvent<string> OnError                    = new UnityEvent<string>();

    // Online
    public static UnityEvent OnLoginSuccess                                                         = new UnityEvent();
    public static UnityEvent OnRefreshInventory                                                     = new UnityEvent();
    public static UnityEvent<PlayFabPurchaseManager, List<ItemInstance>>  OnCatalogItemsReceived    = new UnityEvent<PlayFabPurchaseManager, List<ItemInstance>>();
    public static UnityEvent<InitialUserData> OnInitialUserDataRetrieved                            = new UnityEvent<InitialUserData>();
    public static UnityEvent<string, uint> OnItemClicked                                            = new UnityEvent<string, uint>();
    public static UnityEvent<ItemInstance, CatalogItem_CatalogCustomData> OnItemPurchased           = new UnityEvent<ItemInstance, CatalogItem_CatalogCustomData>();
    public static UnityEvent<uint> OnGoldCoinsReceived                                              = new UnityEvent<uint>();
    public static UnityEvent<uint> OnGemsReceived                                                   = new UnityEvent<uint>();
   
    // Power ups
    public static UnityEvent OnDoublePointsPurchased    = new UnityEvent();
    public static UnityEvent OnTriplePointsPurchased    = new UnityEvent();
    public static UnityEvent OnSpeedUpPurchased         = new UnityEvent();

}
