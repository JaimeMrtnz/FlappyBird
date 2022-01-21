using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingSplash : UIPanel
{
    protected override void Initialize()
    {
        base.Initialize();

        Show();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnGameLoaded.AddListener(Hide);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnGameLoaded.RemoveListener(Hide);
    }
}
