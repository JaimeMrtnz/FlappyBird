using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Log in via android device id
/// </summary>
public sealed class LoginAndroid : LoginMethod
{
    public override void LogIn(PlayFabRequestCommon request)
    {
        PlayFabClientAPI.LoginWithAndroidDeviceID((LoginWithAndroidDeviceIDRequest)request, OnLoginSuccess, OnLoginFailure);
    }

    protected override void OnLoginSuccess(LoginResult result)
    {
        EventsManager.OnLoginSuccess.Invoke(result);
    }

    protected override void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong in log in:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
