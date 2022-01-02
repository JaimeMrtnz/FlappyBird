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
        Initialize();
        AddListeners();
    }

    private void Initialize()
    {
    }

    private void AddListeners()
    {
        InputManager.OnTap.AddListener(OnTap);
    }

    /// <summary>
    /// On tap event handler
    /// </summary>
    private void OnTap()
    {
        Jump();
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
