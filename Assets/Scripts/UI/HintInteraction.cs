using UnityEngine;

public class HintInteraction : MonoBehaviour 
{
    public void Enable() =>
        gameObject.SetActive(true);

    public void Disable() =>
        gameObject.SetActive(false);
}