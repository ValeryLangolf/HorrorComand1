using UnityEngine;

public class InteractionHintIndicator : MonoBehaviour
{
    [SerializeField] private HintIndicator _hintIndicator;
    [SerializeField] private float _activeDistanceToObject;

    private HintTriggerDetector _triggerDetector;

    public HintTrigger CurrentHintTrigger { get; private set; }

    private void Awake()
    {
        _triggerDetector = new(_activeDistanceToObject);
        _triggerDetector.StartDetection();
        _hintIndicator.HideObject();
    }

    private void OnEnable()
    {
        _triggerDetector.Detected += HandleDetection;
        _triggerDetector.Undetected += HandleUnetection;
    }

    private void OnDisable()
    {
        _triggerDetector.Detected -= HandleDetection;
        _triggerDetector.Undetected -= HandleUnetection;
    }

    private void HandleDetection(HintTrigger trigger)
    {
        CurrentHintTrigger = trigger;
        _hintIndicator.ShowObject();
    }

    private void HandleUnetection()
    {
        CurrentHintTrigger = null;
        _hintIndicator.HideObject();
    }
}