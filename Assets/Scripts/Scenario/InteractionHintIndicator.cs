using UnityEngine;

public class InteractionHintIndicator : MonoBehaviour
{
    [SerializeField] private HintIndicator _hintIndicator;
    [SerializeField] private HintTriggerDetector _triggerDetector;

    public HintTrigger CurrentHintTrigger { get; private set; }

    private void Awake() =>
        _hintIndicator.HideObject();

    private void OnEnable()
    {
        _triggerDetector.Detected += HandleDetection;
        _triggerDetector.Undetected += HandleUndetection;
    }

    private void OnDisable()
    {
        _triggerDetector.Detected -= HandleDetection;
        _triggerDetector.Undetected -= HandleUndetection;
    }

    private void HandleDetection(HintTrigger trigger)
    {
        CurrentHintTrigger = trigger;
        _hintIndicator.ShowObject();
    }

    private void HandleUndetection()
    {
        CurrentHintTrigger = null;
        _hintIndicator.HideObject();
    }
}