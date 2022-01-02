using UnityEngine;

/// <summary>
/// Moves pipes
/// </summary>
public class PipeController : MonoBehaviour
{
    private float speed;
    private bool canMove = false;

    public void Update()
    {
        if (canMove)
        {
            transform.position += Vector3.left * speed * Time.deltaTime; 
        }
    }

    /// <summary>
    /// begins moving the pipes and shows them
    /// </summary>
    /// <param name="speed"></param>
    public void Ready(float speed, float height)
    {
        this.speed = speed;
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
