using System.Collections;
using UnityEngine;

public delegate void AnimationFinishedCallback();

public class AnimatorWrapping
{
    private readonly MonoBehaviour _monobehaviour;
    private readonly Animator _animator;

    public AnimatorWrapping(Animator animator)
    {
        _animator = animator;
        _monobehaviour = _animator.GetComponent<MonoBehaviour>();
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
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            yield return null;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        callback?.Invoke();
    }
}