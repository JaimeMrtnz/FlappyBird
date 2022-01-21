using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Stores global events
/// </summary>
public class EventsManager
{
    // Core game
    public static UnityEvent OnGameStart                                        = new UnityEvent();
    public static UnityEvent OnGameOver                                         = new UnityEvent();
    public static UnityEvent OnReplay                                           = new UnityEvent();
    public static UnityEvent OnPipeSucceeded                                    = new UnityEvent();
    public static UnityEvent<uint> OnNewScore                                   = new UnityEvent<uint>();
    public static UnityEvent<uint> OnNewTotalGoldCoins                          = new UnityEvent<uint>();
    public static UnityEvent<uint> OnNewTotalGems                               = new UnityEvent<uint>();
    public static UnityEvent<uint> OnGoldCoinsWon                               = new UnityEvent<uint>();
    public static UnityEvent<uint> OnGemsWon                                    = new UnityEvent<uint>();
    public static UnityEvent OnSpeedUp                                          = new UnityEvent();
    public static UnityEvent OnFinishSpeedUp                                    = new UnityEvent();
    public static UnityEvent OnSpeedUpClicked                                   = new UnityEvent();
    public static UnityEvent OnGameLoaded                                       = new UnityEvent();
    public static UnityEvent<int> OnNewSkin                                     = new UnityEvent<int>();
    public static UnityEvent<string, float> OnTimerTick                         = new UnityEvent<string, float>();
    public static UnityEvent<string> OnTimerCoundDownFinished                   = new UnityEvent<string>();
    public static UnityEvent<string, float, DateTime?> OnPartialTimerAdded      = new UnityEvent<string, float, DateTime?>();
    public static UnityEvent<string> OnError                                    = new UnityEvent<string>();

    // Online
    public static UnityEvent<LoginResult> OnLoginSuccess                                                                    = new UnityEvent<LoginResult>();
    public static UnityEvent OnRefreshInventory                                                                             = new UnityEvent();
    public static UnityEvent<PlayFabPurchaseManager, Dictionary<string, ItemComponents>>  OnCatalogItemsReceived            = new UnityEvent<PlayFabPurchaseManager, Dictionary<string, ItemComponents>>();
    public static UnityEvent<PlayFabPurchaseManager, Dictionary<string, ItemComponents>>  OnCatalogItemsReceivedStoreUI         = new UnityEvent<PlayFabPurchaseManager, Dictionary<string, ItemComponents>>();
    public static UnityEvent<UserInventory> OnGetUserInventorySuccess                                                       = new UnityEvent<UserInventory>();
    public static UnityEvent<InitialTitleData> OnInitialTitleDataRetrieved                                                  = new UnityEvent<InitialTitleData>();
    public static UnityEvent<Dictionary<string, UserDataRecord>> OnUserDataRetrieved                                        = new UnityEvent<Dictionary<string, UserDataRecord>>();
    public static UnityEvent<string> OnItemClicked                                                                          = new UnityEvent<string>();
    public static UnityEvent<ItemInstance, CatalogItem_CatalogCustomData> OnItemPurchased                                   = new UnityEvent<ItemInstance, CatalogItem_CatalogCustomData>();
    public static UnityEvent<string, float, DateTime?> OnItemTimerSuccess                                                   = new UnityEvent<string, float, DateTime?>();
    public static UnityEvent<uint> OnGoldCoinsReceived                                                                      = new UnityEvent<uint>();
    public static UnityEvent<uint> OnGemsReceived                                                                           = new UnityEvent<uint>();
   
    // Power ups
    public static UnityEvent OnDoublePointsPurchased    = new UnityEvent();
    public static UnityEvent OnTriplePointsPurchased    = new UnityEvent();
    public static UnityEvent OnSpeedUpPurchased         = new UnityEvent();

}
