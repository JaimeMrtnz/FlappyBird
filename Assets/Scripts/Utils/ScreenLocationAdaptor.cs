using UnityEngine;

/// <summary>
/// relocates the object in the same place when the screen changes
/// </summary>
public class ScreenLocationAdaptor : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    private void Awake()
    {
        var leftDiff  = (-camera.orthographicSize * camera.aspect) - Constants.SCREEN_OFFSET;
        var rightDiff = (camera.orthographicSize * camera.aspect) + Constants.SCREEN_OFFSET;

        transform.position = new Vector3(transform.position.x <= 0 ? leftDiff : rightDiff, 0, 0);
    }
}
