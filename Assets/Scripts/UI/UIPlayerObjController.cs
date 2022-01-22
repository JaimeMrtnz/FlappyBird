using TMPro;
using UnityEngine;

/// <summary>
/// Controls and stores data of a player obj item for leaderboards
/// </summary>
public class UIPlayerObjController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI positionText;

    [SerializeField]
    private TextMeshProUGUI playerNameText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public string PositionText { get => positionText.text; set => positionText.text = value; }
    public string PlayerNameText { get => playerNameText.text; set => playerNameText.text = value; }
    public string ScoreText { get => scoreText.text; set => scoreText.text = value; }
}
