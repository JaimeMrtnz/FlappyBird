using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// JSON parser using JSON Utility library
/// </summary>
public class UnityJsonUtilityAdapter : IParser
{
    public T Deserialize<T>(string data)
    {
        return JsonUtility.FromJson<T>(data);
    }

    public string Serialize<T>(T data)
    {
        return JsonUtility.ToJson(data);
    }
}
