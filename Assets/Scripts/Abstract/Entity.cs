using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public void ShowObject() =>
        gameObject.SetActive(true);

    public void HideObject() =>
        gameObject.SetActive(false);
}