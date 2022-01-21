using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Retrieves an title data from playfab
/// </summary>
public class PlayFabDataRetriever 
{
    private string titleDataID;
    private IParser parser;
    private InitialTitleData initialUserData;

    [Serializable]
    public class UserInitialized
    {
        public bool IsInitialized;
    }

    public PlayFabDataRetriever(string titleDataID, IParser parser)
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
        result => OnRetrieveTitleDataSuccess(result),
        error => OnRetrieveTitleDataError(error));
    }

    public void RetrieveUserData(string playFabId, List<string> keys = default(List<string>))
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = playFabId,
            Keys = keys
        },
        result => OnUserDataSucess(result),
        error => OnUserDataError(error));
    }

    private void OnRetrieveTitleDataSuccess(GetTitleDataResult successResult)
    {
        var data = successResult.Data[titleDataID];
        initialUserData = parser.Deserialize<InitialTitleData>(data);

        EventsManager.OnInitialTitleDataRetrieved.Invoke(initialUserData);
    }

    private void OnRetrieveTitleDataError(PlayFabError error)
    {
        Debug.Log("Error initializing user: ");
        Debug.Log(error.ErrorMessage);
    }

    private static void OnUserDataSucess(GetUserDataResult result)
    {
        EventsManager.OnUserDataRetrieved.Invoke(result.Data);
    }

    private static void OnUserDataError(PlayFabError error)
    {
        Debug.Log("Error retrieving user data: ");
        Debug.Log(error.ErrorMessage);
    }
}
