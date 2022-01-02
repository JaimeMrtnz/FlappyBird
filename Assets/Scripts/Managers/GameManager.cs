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
        InputManager.Initialize();

        Initialize();
        AddListeners();
    }

    private void Update()
    {
        InputManager.Update();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Initialize()
    {
        EventsManager.OnNewScore.Invoke(0);
        Time.timeScale = 0;
    }

    private void AddListeners()
    {
        EventsManager.OnGameStart.AddListener(OnGameStart);

        EventsManager.OnGameOver.AddListener(OnGameOver);

        EventsManager.OnPipeSucceeded.AddListener(OnPipeSucceeded);
    }

    private void RemoveListeners()
    {
        EventsManager.OnGameStart.AddListener(OnGameStart);

        EventsManager.OnGameOver.RemoveListener(OnGameOver);

        EventsManager.OnPipeSucceeded.RemoveListener(OnPipeSucceeded);
    }

    private void OnGameStart()
    {
        Time.timeScale = 1;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
    }

    private void OnPipeSucceeded()
    {
        score++;
        EventsManager.OnNewScore.Invoke(score);
    }
}
