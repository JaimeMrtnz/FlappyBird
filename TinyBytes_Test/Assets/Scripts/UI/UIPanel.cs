using UnityEngine;

/// <summary>
/// Gives basic functionality for standard canvas
/// </summary>
public class UIPanel : MonoBehaviour
{
    protected virtual void Awake()
    {
        Initialize();
        AddListeners();
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Initialize()
    { }

    protected virtual void AddListeners() 
    { }

    protected virtual void RemoveListeners() 
    { }

    public virtual void Show()
    {
        gameObject.SetActive(true); 
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false); 
    }
}
