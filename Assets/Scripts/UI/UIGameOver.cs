using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the game over canvas
/// </summary>
public class UIGameOver : UIPanel
{
    [Header("Multiplier fields")]
    [SerializeField]
    private GameObject multiplierContent;

    [SerializeField]
    private TextMeshProUGUI multiplierText;

    private bool tripleMultiplierPurchased = false;

    protected override void Initialize()
    {
        base.Initialize();

        Hide();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnGameOver.AddListener(OnGameOver);
        EventsManager.OnDoublePointsPurchased.AddListener(OnDoublePointsPurchased);
        EventsManager.OnTriplePointsPurchased.AddListener(OnTriplePointsPurchased);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnGameOver.RemoveListener(OnGameOver);
        EventsManager.OnDoublePointsPurchased.RemoveListener(OnDoublePointsPurchased);
        EventsManager.OnTriplePointsPurchased.RemoveListener(OnTriplePointsPurchased);
    }

    private void OnDoublePointsPurchased()
    {
        if (!tripleMultiplierPurchased)
        {
            multiplierContent.SetActive(true);
            multiplierText.text = "x2"; 
        }
    }

    private void OnTriplePointsPurchased()
    {
        tripleMultiplierPurchased = true;
        multiplierContent.SetActive(true);
        multiplierText.text = "x3";
    }

    /// <summary>
    /// On replay button click handler
    /// </summary>
    public void OnReplayClick()
    {
        EventsManager.OnReplay.Invoke();
        Hide();
    }

    /// <summary>
    /// Listener when the game is over. Shows this panel
    /// </summary>
    private void OnGameOver()
    {
        Show();
    }
}
