
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages countdowns
/// </summary>
public class MasterTickerController 
{
    private List<Ticker> tickers;

    public MasterTickerController()
    {
        tickers = new List<Ticker>();
    }

    /// <summary>
    /// Creates a new ticker and stores it
    /// </summary>
    /// <param name="time"></param>
    /// <param name="callback"></param>
    public Guid NewTicker(float time, Action callback, Action<float> tickAction = null, bool reverseTimer = false)
    {
        var id = new Guid();
        tickers.Add(new Ticker(id, time, callback, tickAction, reverseTimer));
        return id;
    }

    /// <summary>
    /// Resumes a ticker given an id
    /// </summary>
    /// <param name="id"></param>
    public void Resume(Guid id)
    {
        var elems = tickers.Where(x => x.Id == id);
        if(elems.Count() > 0)
        {
            elems.First().Resume();
        }
    }

    /// <summary>
    /// Pauses a ticker given an id
    /// </summary>
    /// <param name="id"></param>
    public void Pause(Guid id)
    {
        var elems = tickers.Where(x => x.Id == id);
        if (elems.Count() > 0)
        {
            elems.First().Pause();
        }
    }

    /// <summary>
    /// Updates or destroys tickers
    /// </summary>
    public void Update()
    {
        for(int i = 0; i < tickers.Count; i++)
        {
            if (!tickers[i].IsDirty())
            {
                tickers[i].Update(); 
            }
            else
            {
                tickers.RemoveAt(i);
            }
        }
    }
}
