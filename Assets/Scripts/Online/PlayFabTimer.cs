using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Updates timers for items
/// </summary>
public class PlayFabTimer 
{
    public UnityEvent OnSetItemTimerSuccess = new UnityEvent();
    public UnityEvent OnRemoveItemTimerSuccess = new UnityEvent();

    public void SetItemTimer(PlayFabAuthenticationContext authenticationContext, string itemId, DateTime timer)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            AuthenticationContext = authenticationContext,
            Data = new Dictionary<string, string>()
            {
                { "t_"+itemId, timer.ToString() }
            }
        },
        result => OnSetSuccess(result),
        error => OnSetError(error));
    }

    public void RemoveItemTimer(PlayFabAuthenticationContext authenticationContext, string itemId)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            AuthenticationContext = authenticationContext,
            KeysToRemove = new List<string>() { "t_" + itemId },
        },
        result => OnRemoveSuccess(result),
        error => OnRemoveError(error));
    }

    private void OnSetSuccess(UpdateUserDataResult result)
    {
        OnSetItemTimerSuccess.Invoke();
    }

    private void OnSetError(PlayFabError error)
    {
        EventsManager.OnError.Invoke(error.ErrorMessage);
    }

    private void OnRemoveSuccess(UpdateUserDataResult result)
    {
        OnRemoveItemTimerSuccess.Invoke();
    }

    private void OnRemoveError(PlayFabError error)
    {
        EventsManager.OnError.Invoke(error.ErrorMessage);
    }
}
