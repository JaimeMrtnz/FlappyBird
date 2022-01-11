using TMPro;
using UnityEngine;

public class UITotalGem : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI totalGemText;

    protected override void Initialize()
    {
        base.Initialize();

        Show();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnNewTotalGems.AddListener(OnNewTotalGems);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnNewTotalGems.RemoveListener(OnNewTotalGems);
    }

    private void OnNewTotalGems(uint newTotalScore)
    {
        totalGemText.text = newTotalScore.ToString();
    }
}
