using UnityEngine.SceneManagement;

/// <summary>
/// Manages the game over canvas
/// </summary>
public class UIGameOver : UIPanel
{
    protected override void Initialize()
    {
        base.Initialize();

        Hide();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnGameOver.AddListener(OnGameOver);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnGameOver.RemoveListener(OnGameOver);
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
