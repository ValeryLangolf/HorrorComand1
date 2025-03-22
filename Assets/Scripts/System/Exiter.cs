using System;
using UnityEngine;
public class Exiter : MonoBehaviour
{
    [SerializeField] private PanelExit _panelExit;
    [SerializeField] private ButtonEscap _buttonYes;
    [SerializeField] private ButtonEscap _buttonNo;

    public bool IsPanelShowed { get; private set; }

    public event Action PanelShowingChange;

    private void Awake() =>
        _panelExit.HideObject();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            HandleVisibilityPanelExit();
    }

    private void OnEnable()
    {
        _buttonYes.ButtonPressed += OnExit;
        _buttonNo.ButtonPressed += OnHidePanelExit;
    }

    private void OnDisable()
    {
        _buttonYes.ButtonPressed -= OnExit;
        _buttonNo.ButtonPressed -= OnHidePanelExit;
    }

    private void HandleVisibilityPanelExit()
    {
        if (_panelExit.gameObject.activeInHierarchy)
            HidePanelExit();
        else
            ShowPanelExit();
    }

    private void OnExit(ButtonEscap _) =>
        Application.Quit();

    private void OnHidePanelExit(ButtonEscap _) =>
        HidePanelExit();

    private void ShowPanelExit()
    {
        _panelExit.ShowObject();
        IsPanelShowed = true;
        PanelShowingChange?.Invoke();
    }

    private void HidePanelExit()
    {
        _panelExit.HideObject();
        IsPanelShowed = false;
        PanelShowingChange?.Invoke();
    }
}