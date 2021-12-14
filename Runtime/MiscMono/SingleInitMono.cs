using UnityEngine;
/// <summary>
/// something classes that are initialized once, or initialized externally can inherit from.
/// </summary>
public class SingleInitMono : MonoBehaviour
{
    /// <summary>
    /// whether the Monobehavior has been initialized.
    /// </summary>
    [HideInInspector] public bool initialized;
    /// <summary>
    /// whether this Monobehavior is initialized externally
    /// </summary>
    public bool initializedExternally;
    protected virtual void OnEnable()
    {
        if (!initializedExternally)
            Init();
    }
    public virtual void Init() { }
}