using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the functionality of the leaderboard ui
/// </summary>
public class UILeaderBoard : UIPanel
{
    [SerializeField]
    private GameObject loadingText;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private GameObject playerObj;

    private List<GameObject> players = new List<GameObject>();

    protected override void Initialize()
    {
        base.Initialize();

        Hide();

        loadingText.SetActive(false);
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        EventsManager.OnLeaderBoardRefreshed.AddListener(OnLeaderBoardRefreshed);
        EventsManager.OnRefreshingLeaderboard.AddListener(OnRefreshingLeaderboard);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        EventsManager.OnLeaderBoardRefreshed.RemoveListener(OnLeaderBoardRefreshed);
        EventsManager.OnRefreshingLeaderboard.RemoveListener(OnRefreshingLeaderboard);
    }


    private void OnLeaderBoardRefreshed(List<PlayerLeaderboardEntry> playersResults)
    {
        if (playersResults.Count > 0)
        {
            loadingText.SetActive(false);

            foreach (var result in playersResults)
            {
                var playerObjController = Instantiate(playerObj, content).GetComponent<UIPlayerObjController>();

                playerObjController.PositionText = string.Format("#{0}", (result.Position + 1));
                playerObjController.PlayerNameText = result.Profile.PlayerId;
                playerObjController.ScoreText = (result.StatValue).ToString();

                players.Add(playerObjController.gameObject);
            } 
        }
    }

    /// <summary>
    /// Resets the array of players in leaderboard
    /// </summary>
    private void Reset()
    {
        players.ForEach(x => Destroy(x));
        players.Clear();
    }

    private void OnRefreshingLeaderboard()
    {
        Reset();
        loadingText.SetActive(true);
    }
}
