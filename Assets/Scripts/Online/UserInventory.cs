using PlayFab.ClientModels;
using System;
using System.Collections.Generic;

public class UserInventory 
{
    public Dictionary<string, ItemComponents> Inventory;
    public Dictionary<string, int> CurrenciesBalances;

    public UserInventory(List<ItemInstance> newInventory, Dictionary<string, int> newBalances)
    {
        var itemsColl = new Dictionary<string, ItemComponents>();
        foreach (var item in newInventory)
        {
            itemsColl.Add(
                item.ItemId,
                new ItemComponents()
                {
                    Item = item,
                    GoalTime = null
                }
            );
        }

        Inventory = itemsColl;

        CurrenciesBalances = newBalances;
    }

    public void SetTimers(Dictionary<string, DateTime> timers)
    {
        foreach (var timer in timers)
        {
            Inventory[timer.Key].GoalTime = timer.Value;
        }
    }
}
