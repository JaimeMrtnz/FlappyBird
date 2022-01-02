using UnityEngine;

/// <summary>
/// Controls the behaviour of the player
/// </summary>
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;

    private void Start()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        InputManager.OnTap.AddListener(OnTap);

        EventsManager.OnReplay.AddListener(OnReplay);
    }

    private void RemoveListeners()
    {
        InputManager.OnTap.RemoveListener(OnTap);

        EventsManager.OnReplay.RemoveListener(OnReplay);
    }

    /// <summary>
    /// On tap event handler
    /// </summary>
    private void OnTap()
    {
        Jump();
    }

    /// <summary>
    /// On game replay listener
    /// </summary>
    private void OnReplay()
    {
        transform.position = Vector3.zero;
    }

    /// <summary>
    /// Makes the character jump -fly-
    /// </summary>
    private void Jump()
    {
        rb.velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventsManager.OnGameOver.Invoke();
    }
}
