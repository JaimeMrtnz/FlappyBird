using System;
using UnityEngine;

/// <summary>
/// Controls the most global functionality of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    private uint totalGoldCoins = 0;
    private uint totalGems = 0;
    private uint score = 0;
    private uint multiplier = 1;

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

        EventsManager.OnGoldCoinsReceived.AddListener(OnGoldCoinsReceived);

        EventsManager.OnGemsReceived.AddListener(OnGemsReceived);

        EventsManager.OnDoublePointsPurchased.AddListener(OnDoublePointsPurchased);

        EventsManager.OnTriplePointsPurchased.AddListener(OnTriplePointsPurchased);
    }

    private void RemoveListeners()
    {
        EventsManager.OnGameStart.AddListener(OnGameStart);

        EventsManager.OnGameOver.RemoveListener(OnGameOver);

        EventsManager.OnReplay.RemoveListener(OnReplay);

        EventsManager.OnPipeSucceeded.RemoveListener(OnPipeSucceeded);

        EventsManager.OnGoldCoinsReceived.RemoveListener(OnGoldCoinsReceived);

        EventsManager.OnGemsReceived.RemoveListener(OnGemsReceived);

        EventsManager.OnTriplePointsPurchased.RemoveListener(OnTriplePointsPurchased);
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

        var scoreMultiplied = score * multiplier;
        totalGoldCoins += scoreMultiplied;

        EventsManager.OnNewTotalGoldCoins.Invoke(totalGoldCoins);
        EventsManager.OnGoldCoinsWon.Invoke(scoreMultiplied);
    }

    /// <summary>
    /// On total gold coins received from server
    /// </summary>
    /// <param name="totalGoldCoins"></param>
    private void OnGoldCoinsReceived(uint totalGoldCoins)
    {
        this.totalGoldCoins = totalGoldCoins;
        EventsManager.OnNewTotalGoldCoins.Invoke(totalGoldCoins);
    }

    /// <summary>
    /// On total gems received from server
    /// </summary>
    /// <param name="totalGoldCoins"></param>
    private void OnGemsReceived(uint totalGems)
    {
        this.totalGems = totalGems;
        EventsManager.OnNewTotalGems.Invoke(totalGems);
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

    /// <summary>
    /// x2 coins earned in game.
    /// If x3 has been purchased, it will do nothing :)
    /// -earning x3 is better than x2, isn't it?-
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnDoublePointsPurchased()
    {
        if (multiplier < 2)
        {
            multiplier = 2; 
        }
    }

    /// <summary>
    /// x3 coins earned in game
    /// </summary>
    private void OnTriplePointsPurchased()
    {
        multiplier = 3;
    }
}
