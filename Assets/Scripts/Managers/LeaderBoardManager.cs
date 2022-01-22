using PlayFab;
using UnityEngine;

/// <summary>
/// Manages the leaderboard status
/// </summary>
public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField]
    private int maxResults = 8;

    private PlayFabAuthenticationContext authenticationContext;
    private string leaderBoardId;

    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        EventsManager.OnUpdateLeaderBoardSuccess.AddListener(OnUpdateLeaderBoardSuccess);
        EventsManager.OnLastScore.AddListener(OnLastScore);
    }

    private void RemoveListeners()
    {
        EventsManager.OnUpdateLeaderBoardSuccess.RemoveListener(OnUpdateLeaderBoardSuccess);
        EventsManager.OnLastScore.RemoveListener(OnLastScore);
    }

    public void Initialize(PlayFabAuthenticationContext authenticationContext, string leaderBoardId)
    {
        this.authenticationContext = authenticationContext;
        this.leaderBoardId = leaderBoardId;
    }

    /// <summary>
    /// On new score event handler
    /// </summary>
    /// <param name="newScore"></param>
    private void OnLastScore(uint newScore)
    {
        EventsManager.OnRefreshingLeaderboard.Invoke();
        PlayFabLeaderBoardManager.UpdateLeaderBoard(leaderBoardId, (int)newScore);
    }

    /// <summary>
    /// on update leaderboard success event handler
    /// </summary>
    private void OnUpdateLeaderBoardSuccess()
    {
        PlayFabLeaderBoardManager.GetLeaderBoard(authenticationContext, leaderBoardId, maxResults);
    }
}
