using System;
using UnityEngine;

/// <summary>
/// Manages everything about pipes
/// </summary>
public class PipesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pipesPrefab;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private float timeToNext;

    [SerializeField]
    private float initSpeed;

    [SerializeField]
    private uint amount;

    [Range(0, 2)]
    [SerializeField]
    private float rangeHeight;

    private Pooler pipesPooler;
    private float currentTime = 0;

    private void Awake()
    {
        pipesPooler = new Pooler(pipesPrefab, content, amount);
        pipesPooler.SpawnObjects();

        AddListeners();
        ActivatePipe();
    }

    private void Update()
    {
        if(currentTime > timeToNext)
        {
            ActivatePipe();

            currentTime = 0;
        }

        currentTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }


    private void AddListeners()
    {
        EventsManager.OnReplay.AddListener(OnReplay);
    }

    private void RemoveListeners()
    {
        EventsManager.OnReplay.RemoveListener(OnReplay);
    }

    /// <summary>
    /// On replay event listener
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnReplay()
    {
        currentTime = 0;
        pipesPooler.Reset();
        ActivatePipe();
    }

    /// <summary>
    /// Gets a pipe from pool and activates it
    /// </summary>
    private void ActivatePipe()
    {
        var pipe = pipesPooler.GetObject().GetComponent<PipeController>();
        pipe.Ready(initSpeed, rangeHeight);
    }
}
