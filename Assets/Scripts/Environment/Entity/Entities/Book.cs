using System;
using UnityEngine;

public class Book : Entity
{
    [SerializeField] private ButtonClickListener[] _buttons;
    [SerializeField] private PageBook[] _pages;
    [SerializeField] private PlotNote[] _notes;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioclip;

    private int _maximumPages;
    private int _pageIndex;
    private bool _wasInit;

    private void Awake()
    {
        _maximumPages = _pages.Length / 2;
    }

    public void Init()
    {
        _wasInit = true;

        HideNotes();
        ShowInfo();
    }

    private void OnEnable()
    {
        foreach (var button in _buttons)
            button.ButtonPressed += OnButtonPressed;
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
            button.ButtonPressed -= OnButtonPressed;
    }

    public void TakeNote(NoteInWorldTrigger trigger)
    {
        if(_wasInit == false)
            Init();

        trigger.HideObject();

        switch (trigger)
        {
            case NoteInWorldPasswordTV:
                ShowNote<NoteInBookPasswordTV>();
                break;

            case NoteInWorldPasswordWorkAccount:
                ShowNote<NoteInBookPasswordWorkAccount>();
                break;

            case NoteInWorld2:
                ShowNote<NoteInBook2>();
                break;

            case NoteInWorld3:
                ShowNote<NoteInBook3>();
                break;

            case NoteInWorld4:
                ShowNote<NoteInBook4>();
                break;

            case NoteInWorld4_5:
                ShowNote<NoteInBook4_5>();
                break;

            case NoteInWorld5:
                ShowNote<NoteInBook5>();
                break;

            case NoteInWorld6:
                ShowNote<NoteInBook6>();
                break;

            case NoteInWorld7:
                ShowNote<NoteInBook7>();
                break;
        }

        trigger.HideObject();

        switch (trigger)
        {
            case NoteInWorldPasswordTV:
            case NoteInWorldPasswordWorkAccount:
            case NoteInWorld2:
                _pageIndex = 0;
                break;

            case NoteInWorld3:
            case NoteInWorld4:
            case NoteInWorld4_5:
                _pageIndex = 1;
                break;

            case NoteInWorld5:
            case NoteInWorld6:
            case NoteInWorld7:
                _pageIndex = 2;
                break;
        }

        ShowInfo();
    }

    private void OnButtonPressed(ButtonClickListener button)
    {
        switch (button)
        {
            case ArrowLeft _:
                _pageIndex--;
                break;

            case ArrowRight _:
                _pageIndex++;
                break;

            default:
                throw new ArgumentOutOfRangeException(button.name);
        }

        ShowInfo();
        _audioSource.PlayOneShot(_audioclip);
    }

    private void ShowInfo()
    {
        if (_pageIndex == 0)
            HideButton<ArrowLeft>();
        else
            ShowButton<ArrowLeft>();

        if (_pageIndex == _maximumPages - 1)
            HideButton<ArrowRight>();
        else
            ShowButton<ArrowRight>();

        ShowPageCouple();
    }

    private void ShowPageCouple()
    {
        switch (_pageIndex)
        {
            case 0:
                ShowPage<FirstCouplePages>();
                break;

            case 1:
                ShowPage<SecondCouplePages>();
                break;

            case 2:
                ShowPage<ThirdCouplePages>();
                break;

            default:
                throw new ArgumentOutOfRangeException($"Page index {_pageIndex} is out of range.");
        }
    }

    private void ShowPage<T>() where T : PageBook
    {
        foreach (PageBook page in _pages)
        {
            if (page is T _)
                page.ShowObject();
            else
                page.HideObject();
        }
    }

    private void ShowButton<T>() where T : ButtonClickListener
    {
        foreach (var button in _buttons)
            if (button is T searchButton)
                searchButton.ShowObject();
    }

    private void ShowNote<T>() where T : PlotNote
    {
        foreach (var note in _notes)
            if (note is T _)
                note.ShowObject();
    }

    private void HideNotes()
    {
        foreach (var note in _notes)
            note.HideObject();
    }

    private void HideButton<T>() where T : ButtonClickListener
    {
        foreach (var button in _buttons)
            if (button is T searchButton)
                searchButton.HideObject();
    }
}