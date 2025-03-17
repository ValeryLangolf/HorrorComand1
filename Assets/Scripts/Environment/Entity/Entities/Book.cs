using UnityEngine;

public class Book : Entity
{
    [SerializeField] private PageBook[] _pages;
    [SerializeField] private PlotNote[] _notes;
    [SerializeField] private ButtonClickListener[] _buttons;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioclip;

    private PageBookManager _pageManager;
    private ButtonBookManager _buttonManager;
    private NoteBookManager _noteManager;

    private void Awake()
    {
        _pageManager = new PageBookManager(_pages);
        _buttonManager = new ButtonBookManager(_buttons, OnButtonPressed);
        _noteManager = new NoteBookManager(_notes);
        ShowInfo();
    }

    private void OnEnable() =>
        _buttonManager.EnableButtons();

    private void OnDisable() =>
        _buttonManager.DisableButtons();

    public void TakeNote(NoteInWorldTrigger trigger)
    {
        trigger.HideObject();

        _noteManager.ShowNoteBasedOnTrigger(trigger);
        _pageManager.UpdatePageIndex(trigger);
        ShowInfo();
    }

    private void OnButtonPressed(ButtonClickListener button)
    {
        _pageManager.UpdatePageIndex(button);
        _audioSource.PlayOneShot(_audioclip);
        ShowInfo();
    }

    private void ShowInfo()
    {
        _pageManager.ShowCurrentPage();
        _buttonManager.UpdateButtonVisibility(_pageManager.CurrentPageIndex, _pageManager.GetPageCount);
    }
}