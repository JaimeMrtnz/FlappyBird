using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayFabCatalogManager
{
    public static Task<List<CatalogItem>> GetItems(InitialTitleData userData)
    {
        var t = new TaskCompletionSource<List<CatalogItem>>();

        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = userData.CatalogVersion,
        },
        successResult => OnSuccess(successResult, t),
        error => OnError(error, t));

        return Task.Run(() => t.Task);
    }

    private static void OnSuccess(GetCatalogItemsResult successResult, TaskCompletionSource<List<CatalogItem>> taskCompletionSource)
    {
        taskCompletionSource.SetResult(successResult.Catalog);
    }

    private static void OnError(PlayFabError error, TaskCompletionSource<List<CatalogItem>> taskCompletionSource)
    {
        taskCompletionSource.SetResult(null);
        Debug.Log("Error retrieving catalog items: ");
        Debug.Log(error.ErrorMessage);
    }
}
