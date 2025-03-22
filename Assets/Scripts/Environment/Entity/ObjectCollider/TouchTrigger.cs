public abstract class TouchTrigger : ObjectCollider 
{
    protected virtual void Start() =>
        SetColliderAsTrigger();
}