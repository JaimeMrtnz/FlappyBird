using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages every online step
/// </summary>
public class OnlineManager : MonoBehaviour
{
    private PlayFabTitleDataRetriever playFabDataRetriever;
    private PlayFabPurchase playFabPurchase;

    private InitialUserData userData;

    private void Awake()
    {
        Initialize();
        AddListeners();

        PlayFabLogin.LogIn();
        GetStoreItems();
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
    }

    private void RemoveListeners()
    {
        EventsManager.OnLoginSuccess.RemoveListener(OnLoginSuccess);
    }

    /// <summary>
    /// On login success event listener
    /// </summary>
    private void OnLoginSuccess()
    {
        userData = playFabDataRetriever.RetrieveTitleData();
    }

    /// <summary>
    /// Retrieves items from the store
    /// </summary>
    private void GetStoreItems()
    {
        PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
        { 
            StoreId = userData.StoreId
        },
        successResult => 
        {
            playFabPurchase = new PlayFabPurchase(successResult.Store);
            EventsManager.OnStoreItemsReceived.Invoke(playFabPurchase);
        },
        error => 
        {
            Debug.Log("Error retrieving store items: ");
            Debug.Log(error.ErrorMessage);
        });
    }

    private void PurchaseItem(string itemID)
    {
        PlayFabPurchase.PurchaseItem(itemID);
    }
}
