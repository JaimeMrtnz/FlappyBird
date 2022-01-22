using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceBalancer 
{
    private static int gems5Min;
    private const float MINS_REFERENCE = 5f;

    public static void SetGemsMin(int pricesMin)
    {
        gems5Min = pricesMin;
    }

    /// <summary>
    /// Returns the amount of gems to be substracted given a remaining time in secs,
    /// The minimum will be 5 mins - 1 gem. Every value below that will be also 1 gem
    /// </summary>
    /// <param name="secs"></param>
    /// <returns></returns>
    public static float GetGemsGivenSecs(float secs)
    {
        var fiveMinsInSecs = MinsToSecs(MINS_REFERENCE);
        return secs >= fiveMinsInSecs? (secs / fiveMinsInSecs) * gems5Min : gems5Min;
    }

    private static float MinsToSecs(float mins)
    {
        return mins * 60f;
    }
}
