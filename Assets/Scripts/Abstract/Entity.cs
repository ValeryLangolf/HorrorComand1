using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);
}