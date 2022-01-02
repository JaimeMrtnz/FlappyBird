using UnityEngine;

/// <summary>
/// relocates the object in the same place when the screen changes
/// </summary>
public class ScreenLocationAdaptor : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float screenOffset;

    private void Awake()
    {
        var leftDiff  = (-camera.orthographicSize * camera.aspect) + screenOffset;
        var rightDiff = (camera.orthographicSize * camera.aspect) + screenOffset;

        transform.position = new Vector3(screenOffset <= 0 ? leftDiff : rightDiff, transform.position.y, transform.position.z);
    }
}
