public class NoteBookManager
{
    private readonly PlotNote[] _notes;

    public NoteBookManager(PlotNote[] notes)
    {
        _notes = notes;
        HideNotes();
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
                note.ShowObject();
    }
}