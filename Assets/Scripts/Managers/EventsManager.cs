using UnityEngine.Events;

/// <summary>
/// Stores global events
/// </summary>
public class EventsManager
{
    // Core game
    public static UnityEvent OnGameStart        = new UnityEvent();
    public static UnityEvent OnGameOver         = new UnityEvent();
    public static UnityEvent OnReplay           = new UnityEvent();
    public static UnityEvent OnPipeSucceeded    = new UnityEvent();
    public static UnityEvent<uint> OnNewScore   = new UnityEvent<uint>();

    // Online
    public static UnityEvent OnLoginSuccess                         = new UnityEvent();
    public static UnityEvent<PlayFabPurchase> OnStoreItemsReceived  = new UnityEvent<PlayFabPurchase>();
}
