using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the spawn of elements
/// </summary>
public class Pooler
{
    private GameObject objPrefab;
    private readonly List<GameObject> pool;

    private readonly Transform content;

    private readonly uint amount;

    public Pooler(GameObject objPrefab, Transform content, uint amount)
    {
        this.objPrefab  = objPrefab;
        this.content    = content;
        this.amount     = amount;
        this.pool       = new List<GameObject>();
    }

    /// <summary>
    /// Spawns the specified prefab as many times as the amount
    /// </summary>
    public void SpawnObjects()
    {
        GameObject tempObj;

        for (int i = 0; i < amount; i++)
        {
            tempObj = Object.Instantiate(objPrefab, content);
            tempObj.transform.localPosition = Vector3.zero;
            tempObj.SetActive(false);
            pool.Add(tempObj);
        }
    }

    /// <summary>
    /// Returns a non activated object in pool
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        return pool.Where(x => !x.activeInHierarchy).FirstOrDefault();
    }

    /// <summary>
    /// Resets the whole pool
    /// </summary>
    public void Reset()
    {
        pool.ForEach(x => x.GetComponent<PipeController>().Stop());
    }
}
