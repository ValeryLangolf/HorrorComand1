using UnityEngine;

public class MothSpline : MonoBehaviour 
{
    public void Enable() =>
        gameObject.SetActive(true);

    public void Disable() =>
        gameObject.SetActive(false);
}