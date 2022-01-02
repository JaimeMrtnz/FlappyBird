using UnityEngine;

/// <summary>
/// sensor that detects if the player has passed the pipes right
/// </summary>
public class PipeSensorController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals(Constants.PLAYER_TAG))
        {
            EventsManager.OnPipeSucceeded.Invoke();
        }
    }
}
