using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages everything about timers
/// </summary>
public class TimersManager : MonoBehaviour
{
    private MasterTickerController masterTickerController;

    private void Awake()
    {
        masterTickerController = new MasterTickerController();
        AddListeners();   
    }

    private void Update()
    {
        masterTickerController.Update();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        EventsManager.OnItemTimerSuccess.AddListener(OnItemTimerSuccess);
        EventsManager.OnItemTimerSuccess.AddListener(OnItemTimerSuccess);
        EventsManager.OnVirtualCurrencySubstracted.AddListener(OnVirtualCurrencySubstracted);
    }

    private void RemoveListeners()
    {
        EventsManager.OnItemTimerSuccess.RemoveListener(OnItemTimerSuccess);
        EventsManager.OnVirtualCurrencySubstracted.RemoveListener(OnVirtualCurrencySubstracted);
    }

    private void OnItemTimerSuccess(string itemId, float time, DateTime? goalTime)
    {
        masterTickerController.NewTicker(
            time,
            () =>
            {
                EventsManager.OnTimerCoundDownFinished.Invoke(itemId);
            },
            (currentTime) =>
            {
                EventsManager.OnTimerTick.Invoke(itemId, currentTime);
            },
            true);
    }

    /// <summary>
    /// creates a timer that begins in the middle of a past timer
    /// this is used when a timer has not finished when start up
    /// </summary>
    /// <param name="item"></param>
    public void NewPartialTimer(ItemComponents item)
    {
        var time = (float)(item.GoalTime - DateTime.UtcNow).Value.TotalSeconds;
        masterTickerController.NewTicker(
         time,
         () =>
         {
             EventsManager.OnTimerCoundDownFinished.Invoke(item.Item.ItemId);
         },
         (currentTime) =>
         {
             EventsManager.OnTimerTick.Invoke(item.Item.ItemId, currentTime);
         },
         true);

        EventsManager.OnPartialTimerAdded.Invoke(item.Item.ItemId, 0f, item.GoalTime);
    }

    /// <summary>
    /// checks if a date has expired
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool HasExpired(DateTime? time)
    {
        return (time.Value - DateTime.UtcNow).TotalSeconds < 0;
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnVirtualCurrencySubstracted(string itemId)
    {
        EventsManager.OnTimerCoundDownFinished.Invoke(itemId);
    }
}
