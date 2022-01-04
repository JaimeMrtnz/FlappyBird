using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the store UI
/// </summary>
public class UIStore : UIPanel
{
    [SerializeField]
    private GameObject itemPrefab;

    private PlayFabPurchase playFabPurchase;

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnStoreItemsReceived.AddListener(OnStoreItemsReceived);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnStoreItemsReceived.RemoveListener(OnStoreItemsReceived);
    }

    /// <summary>
    /// On store items received event listener
    /// </summary>
    /// <param name="arg0"></param>
    private void OnStoreItemsReceived(PlayFabPurchase playFabPurchase)
    {
        this.playFabPurchase = playFabPurchase;
        DisplayItems();
    }

    private void DisplayItems()
    {
        foreach (var item in playFabPurchase.StoreItems)
        {
        }
    }
}
