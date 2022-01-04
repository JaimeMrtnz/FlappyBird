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
    public InitialUserData RetrieveTitleData()
    {
        InitialUserData initialUserData = null;

        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = new List<string>() { "isInitialized" }
        },
        successResult =>
        {
            // if it has not been already initialized
            if(!successResult.Data.ContainsKey("isInitialized"))
            {
                initialUserData = InitializeUser();
            }
        },
        error =>
        {
            Debug.Log("Error retrieving title data: ");
            Debug.Log(error.ErrorMessage);
        }
        );

        return initialUserData;
    }

    /// <summary>
    /// Initializes a user
    /// </summary>
    /// <returns></returns>
    private InitialUserData InitializeUser()
    {
        var request = new GetTitleDataRequest()
        {
            Keys = new List<string>() { titleDataID }
        };

        InitialUserData initialUserData = null;

        PlayFabClientAPI.GetTitleData(request,
        successResult =>
        {
            var data = successResult.Data[titleDataID];
            initialUserData = parser.Deserialize<InitialUserData>(data);

            PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest()
            { 
                VirtualCurrency = initialUserData.SoftCurrency,
            },
                successResult => { },
                error => 
                {
                    Debug.Log("Error adding virtual currency: ");
                    Debug.Log(error.ErrorMessage);
                });

            // isInitialized flag is set to true to be considered in the future
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    { "isInitialized", parser.Serialize(new UserInitialized { IsInitialized = true})}
                }
            },
            successResult => { },
            error => 
            {
                Debug.Log("Error updating user data: ");
                Debug.Log(error.ErrorMessage);
            });
        },
        error =>
        {
            Debug.Log("Error initializing user: ");
            Debug.Log(error.ErrorMessage);
        });

        return initialUserData;
    }
}
