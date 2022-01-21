
using System;
using UnityEngine;

/// <summary>
/// Creates a countdown
/// </summary>
public class Ticker
{
    public Guid Id { get => id; }


    private Guid id;
    private float time;
    private float currentTime;
    private bool dirty;
    private bool isRunning;
    private bool reverseTimer;
    private Action callback;
    private Action<float> tickAction;


    public Ticker(Guid id, float time, Action callback, Action<float> tickAction = null, bool reverseTimer = false)
    {
        this.id = id;
        this.time = time;
        this.callback = callback;
        this.tickAction = tickAction;
        this.reverseTimer = reverseTimer;
        this.dirty = false;
        this.isRunning = true;
        this.currentTime = reverseTimer ? time : 0;
    }

    public bool IsDirty() { return dirty; }


    public void Update()
    {
        if (isRunning)
        {
            if (!reverseTimer)
            {
                currentTime += Time.unscaledDeltaTime;
                tickAction?.Invoke(currentTime);

                if (currentTime >= time)
                {
                    callback?.Invoke();
                    dirty = true;
                }  
            }
            else
            {
                currentTime -= Time.unscaledDeltaTime;
                tickAction?.Invoke(currentTime);

                if (currentTime <= 0)
                {
                    callback?.Invoke();
                    dirty = true;
                }
            }
        }
    }

    public void Pause()
    {
        isRunning = false;
    }

    public void Resume()
    {
        isRunning = true;
    }
}
