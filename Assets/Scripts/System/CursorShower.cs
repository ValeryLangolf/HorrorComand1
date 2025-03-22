using UnityEngine;

public class CursorShower : MonoBehaviour
{
    [SerializeField] private Exiter _exiter;
    [SerializeField] private PlayerControl _playerControl;

    private void OnEnable()
    {
        _exiter.PanelShowingChange += HandleStateCursor;
        _playerControl.ShowingBookChanged += HandleStateCursor;
    }

    private void OnDisable()
    {
        _exiter.PanelShowingChange -= HandleStateCursor;
        _playerControl.ShowingBookChanged -= HandleStateCursor;
    }

    private void HandleStateCursor()
    {
        if (_exiter.IsPanelShowed || _playerControl.IsBookShowed)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            HandleStateCursor();
        else
            Cursor.visible = true;
    }
}