using System;
using UnityEngine;

public delegate void NoteOutFinishedCallback();

public class Book : Entity
{
    [SerializeField] private PageBook[] _pages;
    [SerializeField] private PlotNote[] _notes;
    [SerializeField] private ButtonArrow[] _arrow;
    [SerializeField] private NoteInBookTarget _noteInBookTarget;

    private PageBookManager _pageManager;
    private ButtonBookManager _buttonManager;
    private NoteBookManager _noteManager;
    private PositionAdjuster _notePositionAdjuster;

    private bool _isNoteSelected;
    private ButtonClickListener _currentNote;
    private ButtonClickListener _noteInQueue;
    private ButtonClickListener _arrowInQueue;
    private Vector3 _defaultNotePosition;
    private Quaternion _defaultNoteRotation;

    private bool _isInit;

    public event Action<SoundParams> SoundPlayBack;

    private void Awake() =>
        Init();

    private void Init()
    {
        if (_isInit)
            return;

        _isInit = true;

        _pageManager = new PageBookManager(_pages);
        _buttonManager = new ButtonBookManager(_arrow, OnButtonPressed);
        _noteManager = new NoteBookManager(_notes, OnNotePressed);
        _notePositionAdjuster = new(this, 1, 100);
        ShowInfo();
    }

    private void OnEnable()
    {
        _buttonManager.SubscribeClick();
        _noteManager.SubscribeClick();
    }

    private void OnDisable()
    {
        _buttonManager.UnsubscribeClick();
        _noteManager.UnsubscribeClick();
    }

    public void TakeNote(NoteInWorldTrigger trigger)
    {
        Init();

        trigger.HideObject();

        _noteManager.ShowNoteBasedOnTrigger(trigger);
        _pageManager.UpdatePageIndex(trigger);
        ShowInfo();
    }

    private void OnButtonPressed(ButtonClickListener button)
    {
        if (_isNoteSelected)
        {
            _arrowInQueue = button;
            ZoomOutNote(OnButtonPressed);
        }
        else
        {
            _pageManager.UpdatePageIndex(button);
            SoundPlayBack?.Invoke(new(SoundName.TurningPage, transform));
            ShowInfo();
        }
    }

    private void OnButtonPressed() =>
        OnButtonPressed(_arrowInQueue);

    private void OnNotePressed(ButtonClickListener button)
    {
        if (_isNoteSelected == false)
        {
            ZoomInOnNote(button);
            return;
        }

        if (_currentNote != button)
        {
            _noteInQueue = button;
            ZoomOutNote(OnNotePressed);
            return;
        }

        ZoomOutNote();
    }

    private void OnNotePressed() =>
        OnNotePressed(_noteInQueue);

    private void ZoomInOnNote(ButtonClickListener button)
    {
        _isNoteSelected = true;

        _defaultNotePosition = button.transform.parent.position;
        _defaultNoteRotation = button.transform.parent.rotation;
        _currentNote = button;

        _notePositionAdjuster.Adjust(button.transform.parent, _noteInBookTarget.transform);
        SoundPlayBack?.Invoke(new(SoundName.NoteOpened, transform));
    }

    public void ZoomInCurrentNote() =>
        ZoomInOnNote(_noteManager.CurrentNote);

    public void ZoomOutNote(AdjustmentCallback callback = null)
    {
        if (_isNoteSelected == false)
        {
            callback.Invoke();
            return;
        }

        _isNoteSelected = false;

        _notePositionAdjuster.Adjust(_currentNote.transform.parent, _defaultNotePosition, _defaultNoteRotation, callback);
        SoundPlayBack?.Invoke(new(SoundName.NoteClosed, transform));
    }

    private void ShowInfo()
    {
        _pageManager.ShowCurrentPage();
        _buttonManager.UpdateButtonVisibility(_pageManager.CurrentPageIndex, _pageManager.GetPageCount);
    }
}