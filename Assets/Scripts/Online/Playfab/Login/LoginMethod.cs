using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using System;

/// <summary>
/// Interface for different methods
/// </summary>
public abstract class LoginMethod 
{
    /// <summary>
    /// Logs in the device
    /// </summary>
    /// <param name="request"></param>
    public abstract void LogIn(PlayFabRequestCommon request);

    /// <summary>
    /// On login success callback
    /// </summary>
    /// <param name="result"></param>
    protected abstract void OnLoginSuccess(LoginResult result);

    /// <summary>
    /// On login failure callback
    /// </summary>
    /// <param name="error"></param>
    protected abstract void OnLoginFailure(PlayFabError error);

}
