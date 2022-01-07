using UnityEngine;

/// <summary>
/// Moves pipes
/// </summary>
public class PipeController : MonoBehaviour
{
    [SerializeField]
    private Collider2D pipeUpCollider;

    [SerializeField]
    private Collider2D pipeDownCollider;

    private float normalSpeed;
    private float speedUpSpeed;
    private float currentSpeed;
    private bool canMove = false;

    private void Awake()
    {
        AddListeners();
    }

    public void Update()
    {
        if (canMove)
        {
            transform.position += Vector3.left * currentSpeed * Time.deltaTime; 
        }
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        EventsManager.OnSpeedUp.AddListener(OnSpeedUp);
        EventsManager.OnFinishSpeedUp.AddListener(OnFinishSpeedUp);
    }

    private void RemoveListeners()
    {
        EventsManager.OnSpeedUp.RemoveListener(OnSpeedUp);
        EventsManager.OnFinishSpeedUp.RemoveListener(OnFinishSpeedUp);
    }

    private void OnSpeedUp()
    {
        currentSpeed = speedUpSpeed;

        pipeUpCollider.enabled = false;
        pipeDownCollider.enabled = false;
    }

    private void OnFinishSpeedUp()
    {
        currentSpeed = normalSpeed;

        pipeUpCollider.enabled = true;
        pipeDownCollider.enabled = true;
    }

    /// <summary>
    /// begins moving the pipes and shows them
    /// </summary>
    /// <param name="speed"></param>
    public void Ready(float speed, float speedUpSpeed, float height)
    {
        this.normalSpeed = speed;
        this.speedUpSpeed = speedUpSpeed;
        currentSpeed = normalSpeed;
        transform.localPosition = new Vector3(0, Random.Range(-height, height), 0);
        this.canMove = true;

        Show();
    }

    /// <summary>
    /// Stops showing and movind pipes
    /// </summary>
    public void Stop()
    {
        this.canMove = false;
        Hide();
    }

    /// <summary>
    /// Shows the object
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the object
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
