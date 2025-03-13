using UnityEngine;

public class CursorShower : MonoBehaviour
{
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Cursor.visible = false;
    }
}