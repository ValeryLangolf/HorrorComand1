using System;
using Unity.VisualScripting;
using UnityEngine;

public class NoteBookManager
{
    private readonly PlotNote[] _notes;

    private readonly Action<ButtonClickInformer> _onNotePressed;

    public NoteBookManager(PlotNote[] notes, Action<ButtonClickInformer> onNotePressed)
    {
        _notes = notes;
        _onNotePressed = onNotePressed;
        InitNotes();
        HideNotes();
    }

    public PlotNote CurrentNote { get; private set; }

    public void SubscribeClick()
    {
        foreach (ButtonClickInformer note in _notes)
            note.ButtonPressed += _onNotePressed;
    }

    public void UnsubscribeClick()
    {
        foreach (ButtonClickInformer note in _notes)
            note.ButtonPressed -= _onNotePressed;
    }

    public void SetActiveNote(NoteInWorldTrigger trigger)
    {
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
    }

    public void HideNotes()
    {
        foreach (var note in _notes)
            note.HideObject();
    }

    private void ShowNote<T>() where T : PlotNote
    {
        foreach (var note in _notes)
            if (note is T _)
                ShowNote(note);
    }

    private void ShowNote(PlotNote note)
    {
        note.ShowObject();
        CurrentNote = note;
    }

    private void InitNotes()
    {
        foreach(PlotNote note in _notes)
            InitOneNote(note);
    }

    private void InitOneNote(PlotNote note)
    {
        Transform defaultPosition = new GameObject($"DefPosition{note.gameObject.name}").transform;
        defaultPosition.parent = note.transform.parent.parent;
        defaultPosition.SetPositionAndRotation(note.transform.parent.position, note.transform.parent.rotation);
        note.Init(defaultPosition);
    }
}