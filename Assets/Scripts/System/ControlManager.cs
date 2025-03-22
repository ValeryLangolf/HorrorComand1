using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private Exiter _exiter;
    [SerializeField] private PlayerControl _playerControl;

    private void Start() =>
        HandleControlTransfer();

    private void OnEnable()
    {
        _exiter.PanelShowingChange += HandleControlTransfer;
        _playerControl.ShowingBookChanged += HandleControlTransfer;
    }

    private void OnDisable()
    {
        _exiter.PanelShowingChange -= HandleControlTransfer;
        _playerControl.ShowingBookChanged -= HandleControlTransfer;
    }

    private void HandleControlTransfer()
    {
        if (_exiter.IsPanelShowed || _playerControl.IsBookShowed)
            _playerControl.DisableRotate();
        else
            _playerControl.EnableRotate();

        if (_exiter.IsPanelShowed)
            _playerControl.DisableInputReader();
        else
            if (_playerControl.IsCanReadInput)
            _playerControl.EnableInputReader();
    }
}