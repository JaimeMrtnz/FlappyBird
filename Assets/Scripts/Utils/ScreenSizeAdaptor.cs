using UnityEngine;

/// <summary>
/// Adapts the sprite to the screen
/// </summary>
public class ScreenSizeAdaptor : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    private void Awake()
    {
        transform.localScale = Vector3.one;

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
            worldScreenWidth / width,
            worldScreenHeight / height, 
            transform.localScale.z);
    }
}
