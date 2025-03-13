using UnityEngine;

public class TaskElement : MonoBehaviour
{
    public void Enable() =>
        gameObject.SetActive(true);

    public void Disable() =>
        gameObject.SetActive(false);
}