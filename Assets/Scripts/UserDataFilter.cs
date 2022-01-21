using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper that allows to filter user data
/// </summary>
public class UserDataFilter 
{
    /// <summary>
    /// Returns a relationship of itemId - DateTime for timers
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    public static Dictionary<string, DateTime> GetTimers(Dictionary<string, UserDataRecord> userData)
    {
        var timers = new Dictionary<string, DateTime>();
        foreach (var data in userData)
        {
            var key = data.Key;
            if (key.Contains("t_"))
            {
                timers.Add(key.Substring(2), DateTime.Parse(data.Value.Value));
            }
        }

        return timers;
    }
}
