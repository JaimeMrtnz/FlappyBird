/// <summary>
/// Manages ths beginning canvas panel
/// </summary>
public class UIStartGame : UIPanel
{
    protected override void Initialize()
    {
        base.Initialize();

        Show();
    }

    public void OnGameStartClick()
    {
        EventsManager.OnGameStart.Invoke();
        Hide();
    }
}
