using TMPro;
using UnityEngine;

/// <summary>
/// Manages the score ui
/// </summary>
public class UIScore : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    protected override void Initialize()
    {
        base.Initialize();

        Show();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnNewScore.AddListener(OnNewScore);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnNewScore.AddListener(OnNewScore);
    }

    /// <summary>
    /// Listener for new score event
    /// </summary>
    /// <param name="newScore"></param>
    private void OnNewScore(uint newScore)
    {
        scoreText.text = newScore.ToString();
    }
}
