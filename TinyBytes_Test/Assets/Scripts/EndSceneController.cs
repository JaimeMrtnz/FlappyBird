using UnityEngine;

/// <summary>
/// Detects pipes at the and stops them
/// </summary>
public class EndSceneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.PIPE_TAG))
        {
            collision.transform.parent.GetComponent<PipeController>().Stop();
        }
    }
}
