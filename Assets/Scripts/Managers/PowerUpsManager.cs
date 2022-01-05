using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        EventsManager.OnItemPurchased.AddListener(OnItemPurchased);
    }

    private void RemoveListeners()
    {
        EventsManager.OnItemPurchased.RemoveListener(OnItemPurchased);
    }

    private void OnItemPurchased(ItemInstance item)
    {
        switch(item.ItemId)
        {
            case "doublePoints":
                EventsManager.OnDoublePointsPurchased.Invoke();
                break;

            case "triplePoints":
                EventsManager.OnTriplePointsPurchased.Invoke();
                break;

            case "speedUp":

                break;

            case "5Gems":
                EventsManager.OnGemsWon.Invoke(5);
                break;
        }
    }
}
