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

    private void Awake() =>
        _maximumPages = _pages.Length / 2;

    public void Init()
    {
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

    public void TakeNote(NoteTrigger trigger)
    {
        trigger.HideObject();

        switch (trigger)
        {
            case WorldNotePasswordTV:
                ShowNote<NotePasswordTV>();
                break;

            case WorldNotePasswordWorkAccount:
                ShowNote<NotePasswordWorkAccount>();
                break;

            case WorldNote2:
                ShowNote<Note2>();
                break;

            case WorldNote3:
                ShowNote<Note3>();
                break;

            case WorldNote4:
                ShowNote<Note4>();
                break;

            case WorldNote4_5:
                ShowNote<Note4_5>();
                break;

            case WorldNote5:
                ShowNote<Note5>();
                break;

            case WorldNote6:
                ShowNote<Note6>();
                break;

            case WorldNote7:
                ShowNote<Note7>();
                break;
        }

        trigger.HideObject();

        switch (trigger)
        {
            case WorldNotePasswordTV:
            case WorldNotePasswordWorkAccount:
            case WorldNote2:
                _pageIndex = 0;
                break;

            case WorldNote3:
            case WorldNote4:
            case WorldNote4_5:
                _pageIndex = 1;
                break;

            case WorldNote5:
            case WorldNote6:
            case WorldNote7:
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