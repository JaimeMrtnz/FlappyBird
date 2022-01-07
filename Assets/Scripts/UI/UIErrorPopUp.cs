using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages errors and shows it to the user
/// Not all errors will be notified to user, only that ones that are interesting for him
/// </summary>
public class UIErrorPopUp : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI errorText;

    protected override void Initialize()
    {
        base.Initialize();

        Hide();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnError.AddListener(OnError);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnError.RemoveListener(OnError);
    }

    private void OnError(string error)
    {
        errorText.text = error;
        Show();
    }
}
