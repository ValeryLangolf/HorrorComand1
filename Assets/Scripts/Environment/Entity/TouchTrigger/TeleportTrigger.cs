public abstract class TeleportTrigger : TouchTrigger
{
    protected override void Awake()
    {
        base.Awake();
        SetIsTriggerCollider();
    }
}