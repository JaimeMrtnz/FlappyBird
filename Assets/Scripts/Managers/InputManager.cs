using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class that manages Inputs and invokes their events
/// </summary>
public class InputManager : MonoBehaviour
{
    public static UnityEvent OnTap = new UnityEvent();

    private void Update()
    {
        CheckTouch();
    }

    /// <summary>
    /// Checks when the screen is touched
    /// </summary>
    private static void CheckTouch()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                OnTap?.Invoke();
            }
        }
    }
}
