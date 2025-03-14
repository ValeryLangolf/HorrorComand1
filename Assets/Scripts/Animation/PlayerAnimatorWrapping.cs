using System.Collections;
using UnityEngine;

public delegate void AnimationFinishedCallback();

public class PlayerAnimatorWrapping
{
    private const float TineWaitBeforeAnimation = 0.4f;

    public const string GettingUp = nameof(GettingUp);
    public const string RightMoving = nameof(RightMoving);
    public const string ForwardMoving = nameof(ForwardMoving);
    public const string Jump = nameof(Jump);
    public const string Sitting = nameof(Sitting);
    public const string SitToStand = nameof(SitToStand);
    public const string IsShowBook = nameof(IsShowBook);

    private readonly Animator _animator;
    private readonly WaitForSeconds _wait;
    private readonly MonoBehaviour _monobehaviour;

    public PlayerAnimatorWrapping(MonoBehaviour monoBehaviour, Animator animator)
    {
        _monobehaviour = monoBehaviour;
        _animator = animator;
        _wait = new(TineWaitBeforeAnimation);
    }

    public void ShowMove(float right, float forward)
    {
        _animator.SetFloat(RightMoving, right);
        _animator.SetFloat(ForwardMoving, forward);
    }

    public void ShowJump() =>
        _animator.SetTrigger(Jump);

    public void ShowGettingUp(AnimationFinishedCallback callback = null)
    {
        _animator.Play(GettingUp);
        _monobehaviour.StartCoroutine(WaitForAnimation(GettingUp, callback));
    }

    public void ShowSitting(AnimationFinishedCallback callback = null)
    {
        _animator.SetTrigger(Sitting);
        _monobehaviour.StartCoroutine(WaitForAnimation(Sitting, callback));
    }

    public void ShowSitToStand(AnimationFinishedCallback callback = null)
    {
        _animator.SetTrigger(SitToStand);
        _monobehaviour.StartCoroutine(WaitForAnimation(SitToStand, callback));
    }

    public void ShowBook() =>
        _animator.SetBool(IsShowBook, true);

    public void HideBook() =>
        _animator.SetBool(IsShowBook, false);

    private IEnumerator WaitForAnimation(string animationName, AnimationFinishedCallback callback)
    {
        yield return _wait;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        while (stateInfo.IsName(animationName))
        {
            yield return null;
            stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }

        callback?.Invoke();
    }
}