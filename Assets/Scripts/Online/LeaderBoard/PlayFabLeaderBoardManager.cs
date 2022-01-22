using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages leaderboard-related requests
/// </summary>
public class PlayFabLeaderBoardManager : MonoBehaviour
{
    private static PlayFabAuthenticationContext _authenticationContext;
    private static string _leaderBoardId;
    private static int _maxResults;

    /// <summary>
    /// Updates the leaderboard with a new score
    /// </summary>
    /// <param name="leaderBoardId"></param>
    /// <param name="newScore"></param>
    public static void UpdateLeaderBoard(string leaderBoardId, int newScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate()
                {
                    StatisticName = leaderBoardId,
                    Value = newScore
                }
            }
        },
        result => OnUpdateStatisticsSuccess(result),
        error => OnUpdateStatisticsError(error));
    }

    /// <summary>
    /// Gets and refreshes the leaderboard
    /// </summary>
    /// <param name="authenticationContext"></param>
    /// <param name="leaderBoardId"></param>
    public static void GetLeaderBoard(PlayFabAuthenticationContext authenticationContext, string leaderBoardId, int maxResults = 10)
    {
        _authenticationContext = authenticationContext;
        _leaderBoardId = leaderBoardId;
        _maxResults = maxResults;

        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest()
        {
            AuthenticationContext = authenticationContext,
            StatisticName = leaderBoardId,
            MaxResultsCount = maxResults
        },
        result => OnGetLeaderBoardSuccess(result),
        error => OnGetLeaderBoardError(error));
    }

    private static void OnUpdateStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard updated success");
        EventsManager.OnUpdateLeaderBoardSuccess.Invoke();
    }

    private static void OnUpdateStatisticsError(PlayFabError error)
    {
        Debug.Log("Leaderboard updated error: ");
        Debug.Log(error.ErrorMessage);
    }

    private static void OnGetLeaderBoardSuccess(GetLeaderboardResult result)
    {
        if (result.Leaderboard.Count > 0)
        {
            EventsManager.OnLeaderBoardRefreshed.Invoke(result.Leaderboard); 
        }
        else
        {
            // if playfab does not return already the updated list, we retry
            GetLeaderBoard(_authenticationContext, _leaderBoardId, _maxResults);
        }
    }

    private static void OnGetLeaderBoardError(PlayFabError error)
    {
        Debug.Log("Leaderboard retrieval error: ");
        Debug.Log(error.ErrorMessage);
    }
}
