public abstract class TeleportTrigger : TouchMarker 
{
    protected override void Awake()
    {
        base.Awake();
        SetIsTriggerCollider();
    }
}