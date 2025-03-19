public class HintTrigger : TouchTrigger
{
    protected override void Awake()
    {
        base.Awake();
        SetIsTriggerCollider();
    }
}