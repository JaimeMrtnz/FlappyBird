using TMPro;
using UnityEngine;

/// <summary>
/// Sets total scores
/// </summary>
public class UITotalGoldCoin : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI totalGoldCoinsText;

    protected override void Initialize()
    {
        base.Initialize();

        Show();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnNewTotalGoldCoins.AddListener(OnNewTotalGoldCoins);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnNewTotalGoldCoins.RemoveListener(OnNewTotalGoldCoins);
    }

    private void OnNewTotalGoldCoins(uint newTotalScore)
    {
        totalGoldCoinsText.text = newTotalScore.ToString();
    }
}
