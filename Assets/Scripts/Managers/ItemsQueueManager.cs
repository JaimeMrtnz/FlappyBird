using PlayFab;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that consumes items and process them
/// </summary>
public class ItemsQueueManager : MonoBehaviour
{
    [SerializeField]
    private TimersManager timersManager;

    private class QueueItem
    {
        public PlayFabAuthenticationContext AuthenticationContext;
        public CatalogItem_CatalogCustomData CatalogItem;
        public ItemComponents ItemComponents;
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

            if(item.ItemComponents.GoalTime.HasValue)
            {
                if(timersManager.HasExpired(item.ItemComponents.GoalTime))
                {
                    playFabTimer.RemoveItemTimer(
                        item.AuthenticationContext,
                        item.CatalogItem.CatalogItem.ItemId);
                }
                else
                {
                    timersManager.NewPartialTimer(item.ItemComponents);
                }
            }
            else
            {
                goalTime = DateTime.UtcNow.AddSeconds(float.Parse(item.CatalogItem.CustomData.Timer));

                playFabTimer.SetItemTimer(
                    item.AuthenticationContext,
                    item.CatalogItem.CatalogItem.ItemId,
                    goalTime);
            }
        }
    }

    /// <summary>
    /// Sets an item in the queue
    /// </summary>
    /// <param name="authenticationContext"></param>
    /// <param name="catalogItem"></param>
    public void SetItemTimer(
        PlayFabAuthenticationContext authenticationContext, 
        CatalogItem_CatalogCustomData catalogItem,
        ItemComponents itemComponents)
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
                    ItemComponents = itemComponents
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
