using UnityEngine;

public delegate void CallbackFinished();

public abstract class Entity : MonoBehaviour
{
    public virtual void ShowObject() =>
        gameObject.SetActive(true);

    public void HideObject() =>
        gameObject.SetActive(false);
}