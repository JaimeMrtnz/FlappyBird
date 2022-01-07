using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages external buttons for power ups
/// </summary>
public class UIPowerUpButtons : UIPanel
{
    [SerializeField]
    private GameObject speedUpButton;

    protected override void Initialize()
    {
        base.Initialize();

        speedUpButton.SetActive(false);
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnSpeedUpPurchased.AddListener(OnSpeedUpPurchased);
        EventsManager.OnSpeedUp.AddListener(OnSpeedUp);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnSpeedUpPurchased.RemoveListener(OnSpeedUpPurchased);
        EventsManager.OnSpeedUp.RemoveListener(OnSpeedUp);
    }

    private void OnSpeedUpPurchased()
    {
        speedUpButton.SetActive(true);
    }

    private void OnSpeedUp()
    {
        speedUpButton.SetActive(false);
    }

    public void OnSpeedUpClick()
    {
        EventsManager.OnSpeedUpClicked.Invoke();
    }
}
