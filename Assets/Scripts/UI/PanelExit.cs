using UnityEngine;

public class PanelExit : MonoBehaviour
{
    public void ShowPanel() =>
        transform.gameObject.SetActive(true);

    public void HidePanel() =>
        transform.gameObject.SetActive(false);
}