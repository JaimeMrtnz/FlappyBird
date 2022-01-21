using PlayFab;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemsQueueManager : MonoBehaviour
{
    private class QueueItem
    {
        public PlayFabAuthenticationContext AuthenticationContext;
        public CatalogItem_CatalogCustomData CatalogItem;
    }
    private PlayFabTimer playFabTimer;

    private int queueSize = 1;
    private Queue<QueueItem> queue = new Queue<QueueItem>();
    private bool canTakeNext = true;
    private DateTime goalTime;

    public int QueueSize { get => queueSize; }

    private void Awake()
    {
        playFabTimer = new PlayFabTimer();
    }


    private void Update()
    {
        if (queue.Count > 0 && canTakeNext)
        {
            canTakeNext = false;

            var item = queue.Dequeue();

            goalTime = DateTime.UtcNow.AddSeconds(float.Parse(item.CatalogItem.CustomData.Timer));

            playFabTimer.SetItemTimer(
                item.AuthenticationContext, 
                item.CatalogItem.CatalogItem.ItemId,
                goalTime);
        }
    }

    /// <summary>
    /// Sets an item in the queue
    /// </summary>
    /// <param name="authenticationContext"></param>
    /// <param name="catalogItem"></param>
    public void SetItemTimer(
        PlayFabAuthenticationContext authenticationContext, 
        CatalogItem_CatalogCustomData catalogItem)
    {
        if (CanAddItem())
        {
            playFabTimer.OnSetItemTimerSuccess.AddListener(OnItemTimerSuccess);
            playFabTimer.OnSetItemTimerSuccess.AddListener(() =>
            {
                EventsManager.OnItemTimerSuccess.Invoke(
                    catalogItem.CatalogItem.ItemId,
                    float.Parse(catalogItem.CustomData.Timer),
                    goalTime);
            });

            queue.Enqueue(
                new QueueItem()
                {
                    AuthenticationContext = authenticationContext,
                    CatalogItem = catalogItem,
                });
        }
}

    /// <summary>
    /// Checks if item can be added due to the limit
    /// </summary>
    /// <returns></returns>
    public bool CanAddItem()
    {
        return queue.Count < queueSize;
    }

    /// <summary>
    /// Handler that allows to take the next item in queue
    /// </summary>
    private void OnItemTimerSuccess()
    {
        playFabTimer.OnSetItemTimerSuccess.RemoveAllListeners();
        canTakeNext = true;
    }
}
