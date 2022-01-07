using UnityEngine;

/// <summary>
/// Controls the behaviour of the player
/// </summary>
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private CapsuleCollider2D capCollider;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Color speedUpColor;

    [SerializeField]
    private Color normalColor;

    private float normalGravity;

    private void Start()
    {
        normalGravity = rb.gravityScale;

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

        EventsManager.OnSpeedUp.AddListener(OnSpeedUp);

        EventsManager.OnFinishSpeedUp.AddListener(OnFinishSpeedUp);
    }

    private void RemoveListeners()
    {
        InputManager.OnTap.RemoveListener(OnTap);

        EventsManager.OnReplay.RemoveListener(OnReplay);

        EventsManager.OnSpeedUp.RemoveListener(OnSpeedUp);

        EventsManager.OnFinishSpeedUp.RemoveListener(OnFinishSpeedUp);
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
    /// On speed up event listener
    /// </summary>
    private void OnSpeedUp()
    {
        sr.color = speedUpColor;
    }

    /// <summary>
    /// On finish speed up event listener
    /// returns to normal state
    /// </summary>
    private void OnFinishSpeedUp()
    {
        sr.color = normalColor;
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
