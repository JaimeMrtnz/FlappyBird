using System;
using UnityEngine;

/// <summary>
/// Controls the most global functionality of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    private uint score = 0;

    private void Awake()
    {
        Initialize();
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Initialize()
    {
        Time.timeScale = 0;
    }

    private void AddListeners()
    {
        EventsManager.OnGameStart.AddListener(OnGameStart);

        EventsManager.OnGameOver.AddListener(OnGameOver);

        EventsManager.OnReplay.AddListener(OnReplay);

        EventsManager.OnPipeSucceeded.AddListener(OnPipeSucceeded);
    }

    private void RemoveListeners()
    {
        EventsManager.OnGameStart.AddListener(OnGameStart);

        EventsManager.OnGameOver.RemoveListener(OnGameOver);

        EventsManager.OnReplay.RemoveListener(OnReplay);

        EventsManager.OnPipeSucceeded.RemoveListener(OnPipeSucceeded);
    }

    /// <summary>
    /// On game start listener
    /// </summary>
    private void OnGameStart()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// on game over listener
    /// </summary>
    private void OnGameOver()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// On game replay listener
    /// </summary>
    private void OnReplay()
    {
        score = 0;

        EventsManager.OnNewScore.Invoke(score);

        EventsManager.OnGameStart.Invoke();
    }

    /// <summary>
    /// when the character goes through a pipe event listener
    /// </summary>
    private void OnPipeSucceeded()
    {
        score++;
        EventsManager.OnNewScore.Invoke(score);
    }
}
