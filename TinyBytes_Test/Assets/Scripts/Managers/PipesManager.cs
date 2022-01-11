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
    private float speedUpSpeed;

    [SerializeField]
    private uint amount;

    [Range(0, 2)]
    [SerializeField]
    private float rangeHeight;

    private Pooler pipesPooler;
    private float currentTime = 0;
    private float currentSpeed;

    private void Awake()
    {
        pipesPooler = new Pooler(pipesPrefab, content, amount);
        currentSpeed = initSpeed;
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
        EventsManager.OnSpeedUp.AddListener(OnSpeedUp);
        EventsManager.OnFinishSpeedUp.AddListener(OnFinishSpeedUp);
    }

    private void RemoveListeners()
    {
        EventsManager.OnReplay.RemoveListener(OnReplay);
        EventsManager.OnSpeedUp.RemoveListener(OnSpeedUp);
        EventsManager.OnFinishSpeedUp.RemoveListener(OnFinishSpeedUp);
    }

    /// <summary>
    /// On replay event listener
    /// </summary>
    private void OnReplay()
    {
        currentTime = 0;
        pipesPooler.Reset();
        ActivatePipe();
    }

    /// <summary>
    /// On speed up event listener
    /// </summary>
    private void OnSpeedUp()
    {
        currentSpeed = speedUpSpeed;
    }

    /// <summary>
    /// On finish speed up event listener
    /// </summary>
    private void OnFinishSpeedUp()
    {
        currentSpeed = initSpeed;
    }

    /// <summary>
    /// Gets a pipe from pool and activates it
    /// </summary>
    private void ActivatePipe()
    {
        var pipe = pipesPooler.GetObject().GetComponent<PipeController>();
        pipe.Ready(currentSpeed, speedUpSpeed, rangeHeight);
    }
}
