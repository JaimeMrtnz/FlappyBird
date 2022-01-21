using PlayFab.ClientModels;
using System.Collections.Generic;

public class UserInventory 
{
    public List<ItemInstance> Inventory;
    public Dictionary<string, int> CurrenciesBalances;

    public UserInventory(List<ItemInstance> newInventory, Dictionary<string, int> newBalances)
    {
        Inventory = newInventory;

        CurrenciesBalances = newBalances;
    }
}
