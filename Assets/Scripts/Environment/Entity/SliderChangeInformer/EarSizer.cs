using UnityEngine;

public class EarSizer : MonoBehaviour
{
    [SerializeField] private float _minimumScale;
    [SerializeField] private float _maximumScale;

    public void ResizeByVolume(float volume)
    {
        float newScale = Mathf.Lerp(_minimumScale, _maximumScale, volume);
        transform.localScale = new Vector3(newScale, newScale, 1);
    }
}