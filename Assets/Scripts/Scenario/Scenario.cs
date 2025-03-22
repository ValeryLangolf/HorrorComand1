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
        _player.Touched += HandleState;
    }

    private void OnDisable()
    {
        _playerControl.InteractionKeyPressed -= OnInteraction;
        _player.Touched -= HandleState;
    }

    private void OnInteraction()
    {
        switch (_interaction.CurrentHintTrigger)
        {
            case NoteInWorldTrigger trigger:
                _playerControl.TakeNote(trigger);
                break;
        }
    }

    private void HandleState(TouchTrigger interactiveTrigger)
    {
        
    }
}