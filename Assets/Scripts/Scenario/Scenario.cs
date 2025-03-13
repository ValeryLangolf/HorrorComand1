using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameInteractions
{
    None,
    FirstQuestionSmell,
    SitOnLogWood,
    OldecHeheVoice,
    SourceStenchFound,
    TakeItem,
}

public class Scenario : MonoBehaviour
{
    [SerializeField] private PositionAdjuster _adjuster;
    [SerializeField] private Interaction _interaction;
    [SerializeField] private Player _player;
    [SerializeField] private Oldec _oldec;
    [SerializeField] private TouchTrigger _oldecHeheTrigger;
    [SerializeField] private HintTrigger _oldecHintTrigger;
    [SerializeField] private HintTrigger _logWood;
    [SerializeField] private TargetPosition _logWoodTargetPosition;
    [SerializeField] private SourceUnpleasantOdor _sourceUnpleasantOdor;
    [SerializeField] private MothSpline _mothSpline;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private TaskElement _taskLookAround;
    [SerializeField] private TaskElement _taskTalkOldMan;
    [SerializeField] private TaskElement _taskFindPlaceSit;
    [SerializeField] private TaskElement _taskFollowButterfly;

    [SerializeField] private AudioClip _awakening;
    [SerializeField] private AudioClip _awakeningMonolog;
    [SerializeField] private AudioClip _hehe_voice;
    [SerializeField] private AudioClip _thoughtsTolkingOldec;
    [SerializeField] private AudioClip _firstQuestionSmell;
    [SerializeField] private AudioClip _answerThisLongHistory;
    [SerializeField] private AudioClip _storyAboutHedgehog;
    [SerializeField] private AudioClip _secondQuestionSmell;
    [SerializeField] private AudioClip _easyAnswerSmell;
    [SerializeField] private AudioClip _questionGetOutForest;
    [SerializeField] private AudioClip _answerFollowMoth;

    private List<InteractiveTrigger> _itemsInInventory = new();

    private void OnEnable()
    {
        _player.InteractionButtonPressed += OnInteraction;
        _player.Triggered += HandleState;
    }

    private void OnDisable()
    {
        _player.InteractionButtonPressed -= OnInteraction;
        _player.Triggered -= HandleState;
    }

    private void OnInteraction() =>
        HandleState(_interaction.CurrentInteraction);

    private void HandleState(InteractiveTrigger interactiveTrigger)
    {
        if (interactiveTrigger == null)
            return;

        switch (interactiveTrigger.Name)
        {
            case GameInteractions.OldecHeheVoice:
                OnPlayOldecHehe(interactiveTrigger);
                break;

            case GameInteractions.FirstQuestionSmell:
                AskFirstQuestionSmell(interactiveTrigger);
                break;

            case GameInteractions.SitOnLogWood:
                MoveCloserToLogWood(interactiveTrigger);
                break;

            case GameInteractions.SourceStenchFound:
                AwardAchievementFindingSourceSmell(interactiveTrigger);
                break;

            case GameInteractions.TakeItem:
                TakeItem(interactiveTrigger);
                break;

            default:
                throw new NotImplementedException();
        }
    }

    private void StartAwakening()
    {
        _player.DisableControl();
        _player.PlaySound(_awakening);
        _player.ShowGettingUp(OnGettingUpFinished);
        Invoke(nameof(StartDialogAwakening), 2f);
    }

    private void OnGettingUpFinished() =>
        _player.EnableControl();

    private void StartDialogAwakening()
    {
        _player.PlaySound(_awakeningMonolog, ShowTaskLookAround);
    }

    private void ShowTaskLookAround()
    {
        _taskLookAround.Enable();
        _oldecHeheTrigger.EnableCollider();
    }

    private void OnPlayOldecHehe(InteractiveTrigger trigger)
    {
        trigger.DisableCollider();
        _taskLookAround.Disable();
        _oldec.PlaySound(_hehe_voice, PlayThoughtsTolkingOldec);
    }

    private void PlayThoughtsTolkingOldec() =>
        _player.PlaySound(_thoughtsTolkingOldec, EnableOldecHintTrigger);

    private void EnableOldecHintTrigger()
    {
        _oldecHintTrigger.EnableCollider();
        _taskTalkOldMan.Enable();
    }

    private void AskFirstQuestionSmell(InteractiveTrigger trigger)
    {
        trigger.DisableCollider();
        _taskTalkOldMan.Disable();
        _player.PlaySound(_firstQuestionSmell, ListenAnswerThisLongHistory);
    }

    private void ListenAnswerThisLongHistory() =>
        _oldec.PlaySound(_answerThisLongHistory, EnableLogWood);

    private void EnableLogWood()
    {
        _taskFindPlaceSit.Enable();
        _logWood.EnableCollider();    
    }

    private void MoveCloserToLogWood(InteractiveTrigger trigger)
    {
        trigger.DisableCollider();
        _taskFindPlaceSit.Disable();
        _player.DisableControl();
        _player.DisableCollider();
        _player.DisableGravity();
        _adjuster.Adjust(_player.transform, _logWoodTargetPosition.transform, 1.5f, SitOnLogWood);
    }

    private void SitOnLogWood() =>
        _player.ShowSitting(ListenStory);

    private void ListenStory() =>
        _oldec.PlaySound(_storyAboutHedgehog, AskSecondQuestionSmell);

    private void AskSecondQuestionSmell() =>
        _player.PlaySound(_secondQuestionSmell, ListenEasyAnswerSmell);

    private void ListenEasyAnswerSmell() =>
        _oldec.PlaySound(_easyAnswerSmell, AskHowGetOutForest);

    private void AskHowGetOutForest() =>
        _player.PlaySound(_questionGetOutForest, ListenAnswerFollowMoth);

    private void ListenAnswerFollowMoth()
    {
        _mothSpline.Enable();
        _oldec.PlaySound(_answerFollowMoth, RiseFromLogWood);
    }

    private void RiseFromLogWood()
    {
        _taskFollowButterfly.Enable();
        _player.ShowSitToStand(EnableControl);
    }

    private void EnableControl()
    {
        _player.EnableControl();
        _player.EnableCollider();
        _player.EnableGravity();
        _sourceUnpleasantOdor.Enable();
    }

    private void AwardAchievementFindingSourceSmell(InteractiveTrigger trigger)
    {
        _taskFollowButterfly.Disable();
        trigger.DisableCollider();
        _mothSpline.Disable();
        _winPanel.Enable();
    }

    private void TakeItem(InteractiveTrigger trigger)
    {
        trigger.Hide();
        _itemsInInventory.Add(trigger);
        Debug.Log("Подобрана записка");
    }
}