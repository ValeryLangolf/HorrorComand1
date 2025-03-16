public class HintTrigger : TouchMarker 
{
    protected override void Awake()
    {
        base.Awake();
        SetIsTriggerCollider();
    }
}