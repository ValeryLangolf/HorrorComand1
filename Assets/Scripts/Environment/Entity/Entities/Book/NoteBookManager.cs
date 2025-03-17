using System;

public class NoteBookManager
{
    private readonly PlotNote[] _notes;

    private readonly Action<ButtonClickListener> _onNotePressed;

    public NoteBookManager(PlotNote[] notes, Action<ButtonClickListener> onNotePressed)
    {
        _notes = notes;
        _onNotePressed = onNotePressed;
        HideNotes();
    }

    public PlotNote CurrentNote { get; private set; }

    public void SubscribeClick()
    {
        foreach (ButtonClickListener note in _notes)
            note.ButtonPressed += _onNotePressed;
    }

    public void UnsubscribeClick()
    {
        foreach (ButtonClickListener note in _notes)
            note.ButtonPressed -= _onNotePressed;
    }

    public void ShowNoteBasedOnTrigger(NoteInWorldTrigger trigger)
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
}