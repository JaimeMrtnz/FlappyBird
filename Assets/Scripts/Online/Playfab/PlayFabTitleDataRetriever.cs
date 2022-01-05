using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Retrieves an title data from playfab
/// </summary>
public class PlayFabTitleDataRetriever 
{
    private string titleDataID;
    private IParser parser;
    private InitialUserData initialUserData;

    [Serializable]
    public class UserInitialized
    {
        public bool IsInitialized;
    }

    public PlayFabTitleDataRetriever(string titleDataID, IParser parser)
    {
        this.titleDataID = titleDataID;
        this.parser = parser;
    }

    /// <summary>
    /// Retrieves title data of user
    /// </summary>
    /// <returns></returns>
    public void RetrieveTitleData()
    {
        var request = new GetTitleDataRequest()
        {
            Keys = new List<string>() { titleDataID }
        };

        PlayFabClientAPI.GetTitleData(request,
        successResult => OnSuccess(successResult),
        error => OnError(error));
    }

    private void OnSuccess(GetTitleDataResult successResult)
    {
        var data = successResult.Data[titleDataID];
        initialUserData = parser.Deserialize<InitialUserData>(data);

        EventsManager.OnInitialUserDataRetrieved.Invoke(initialUserData);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error initializing user: ");
        Debug.Log(error.ErrorMessage);
    }
}
