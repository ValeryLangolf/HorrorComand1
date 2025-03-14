using System.Collections;
using UnityEngine;

public class AnimatorWrapping
{
    private readonly WaitForSeconds _wait;
    private readonly MonoBehaviour _monobehaviour;
    private readonly Animator _animator;
    private const float TimeWaitBeforeAnimation = 0.4f;

    public AnimatorWrapping(MonoBehaviour monoBehaviour, Animator animator)
    {
        _monobehaviour = monoBehaviour;
        _animator = animator;
        _wait = new(TimeWaitBeforeAnimation);
    }

    protected void Play(string animationName, AnimationFinishedCallback callback = null)
    {
        _animator.Play(animationName);
        StartIfCallback(callback, animationName);
    }

    protected void SetTrigger(string triggerName, AnimationFinishedCallback callback = null, string animationName = "")
    {
        _animator.SetTrigger(triggerName);
        StartIfCallback(callback, animationName);
    }

    protected void SetBool(string boolName, bool isOn, AnimationFinishedCallback callback = null, string animationName = "")
    {
        _animator.SetBool(boolName, isOn);
        StartIfCallback(callback, animationName);
    }

    protected void SetFloat(string floatName, float value, AnimationFinishedCallback callback = null, string animationName = "")
    {
        _animator.SetFloat(floatName, value);
        StartIfCallback(callback, animationName);
    }

    private void StartIfCallback(AnimationFinishedCallback callback, string animationName)
    {
        if (callback != null)
            _monobehaviour.StartCoroutine(WaitForAnimation(animationName, callback));
    }

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