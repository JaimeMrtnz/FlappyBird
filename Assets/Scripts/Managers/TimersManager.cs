using PlayFab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void RemoveListeners()
    {
        EventsManager.OnItemTimerSuccess.RemoveListener(OnItemTimerSuccess);
    }

    private void OnItemTimerSuccess(string itemId, float time, DateTime goalTime)
    {
        masterTickerController.NewTicker(
            time,
            () =>
            {
                EventsManager.OnTimerCoundDownFinished.Invoke(itemId);
            },
            (currentTime) =>
            {
                EventsManager.OnTimerTick.Invoke(itemId);
            },
            true);
    }

    public void HandleTimers(Dictionary<string, DateTime> timers)
    {
        foreach (var timer in timers)
        {
            var t = timer.Value;
            if(HasExpired(t))
            {
                EventsManager.OnTimerCoundDownFinished.Invoke(timer.Key);
            }
        }
    }

    private bool HasExpired(DateTime time)
    {
        return (time - DateTime.UtcNow).TotalSeconds < 0;
    }
}
