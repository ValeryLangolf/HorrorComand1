using System;
using UnityEngine;

public class Book : Entity
{
    [SerializeField] private PageBook[] _pages;
    [SerializeField] private PlotNote[] _notes;
    [SerializeField] private ButtonArrow[] _arrows;
    [SerializeField] private NoteInBookTarget _noteInBookTarget;

    private PageBookManager _pageManager;
    private ButtonBookManager _buttonManager;
    private NoteBookManager _noteManager;
    private PositionAdjuster _notePositionAdjuster;

    private ButtonClickInformer _currentNote;
    private ButtonClickInformer _noteInQueue;
    private ButtonClickInformer _arrowInQueue;
    private Vector3 _defaultNotePosition;
    private Quaternion _defaultNoteRotation;
    private bool _isNoteSelected;

    private bool _isInit;

    public event Action<SoundParams> SoundPlayBack;

    private void Awake() =>
        Init();

    private void Start() =>
        ShowInfo();

    private void Init()
    {
        if (_isInit)
            return;

        _isInit = true;

        _pageManager = new PageBookManager(_pages);
        _buttonManager = new ButtonBookManager(_arrows, OnButtonArrowPressed);
        _noteManager = new NoteBookManager(_notes, OnNotePressed);
        _notePositionAdjuster = new(this, 1, 100);
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

        _noteManager.SetActiveNote(trigger);
        _pageManager.UpdatePageIndex(trigger);
        ShowInfo();
    }

    private void OnButtonArrowPressed(ButtonClickInformer button)
    {
        if (_isNoteSelected)
        {
            _arrowInQueue = button;
            ZoomOutNote(OnButtonArrowPressed);
        }
        else
        {
            _pageManager.UpdatePageIndex(button);
            SoundPlayBack?.Invoke(new(SoundName.TurningPage, transform));
            ShowInfo();
        }
    }

    private void OnButtonArrowPressed() =>
        OnButtonArrowPressed(_arrowInQueue);

    private void OnNotePressed(ButtonClickInformer button)
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

    private void ZoomInOnNote(ButtonClickInformer button)
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

    public void ZoomOutNote(CallbackFinished callback = null)
    {
        if (_isNoteSelected == false)
        {
            callback.Invoke();
            return;
        }

        _isNoteSelected = false;

        PlotNote note = _currentNote as PlotNote;

        _notePositionAdjuster.Adjust(_currentNote.transform.parent, note.DefaultPosition, callback);
        SoundPlayBack?.Invoke(new(SoundName.NoteClosed, transform));
    }

    private void ShowInfo()
    {
        _pageManager.ShowCurrentPage();
        _buttonManager.UpdateButtonVisibility(_pageManager.CurrentPageIndex, _pageManager.GetPageCount);
    }
}