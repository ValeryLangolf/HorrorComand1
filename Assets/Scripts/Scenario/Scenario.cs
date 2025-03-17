using UnityEngine;

public class Scenario : MonoBehaviour
{
    [SerializeField] private InteractionHintIndicator _interaction;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private TaskElement _taskLookAround;
    [SerializeField] private TaskElement _taskTalkOldMan;
    [SerializeField] private TaskElement _taskFindPlaceSit;
    [SerializeField] private TaskElement _taskFollowButterfly;
    [SerializeField] private Book _book;

    private void OnEnable()
    {
        _playerControl.InteractionKeyPressed += OnInteraction;
        _player.Triggered += HandleState;
    }

    private void OnDisable()
    {
        _playerControl.InteractionKeyPressed -= OnInteraction;
        _player.Triggered -= HandleState;
    }

    private void OnInteraction() =>
        HandleState(_interaction.CurrentHintTrigger);

    private void HandleState(TouchMarker interactiveTrigger)
    {
        if (interactiveTrigger == null)
            return;

        switch (interactiveTrigger)
        {
            case NoteInWorldTrigger trigger:
                TakeNote(trigger);
                break;
        }
    }

    private void TakeNote(NoteInWorldTrigger trigger)
    {
        _playerControl.ShowBook(_book.ZoomInCurrentNote);
        _book.TakeNote(trigger);
    }

    private void EnableControl()
    {
        _playerControl.EnableControl();
        _player.EnableCollider();
        _player.EnableGravity();
    }
}