using System;
using System.Collections;
using UnityEngine;

public class AnimatorWrapping
{
    private readonly MonoBehaviour _monobehaviour;
    private readonly Animator _animator;

    private Coroutine _coroutine;

    public AnimatorWrapping(Animator animator)
    {
        _monobehaviour = animator.GetComponent<MonoBehaviour>();
        _animator = animator;
    }

    protected void Play(string animationName) =>
        _animator.Play(animationName);

    protected void Play(string animationName, CallbackFinished callback)
    {
        StartIfCallback(callback, animationName);
        Play(animationName);
    }

    protected void SetTrigger(string triggerName) =>
        _animator.SetTrigger(triggerName);

    protected void SetTrigger(string triggerName, CallbackFinished callback, string animationName)
    {
        StartIfCallback(callback, animationName);
        SetTrigger(triggerName);
    }

    protected void SetBool(string boolName, bool isOn) =>
        _animator.SetBool(boolName, isOn);

    protected void SetBool(string boolName, bool isOn, CallbackFinished callback, string animationName)
    {
        StartIfCallback(callback, animationName);
        SetBool(boolName, isOn);
    }

    protected void SetFloat(string floatName, float value)
    {
        _animator.SetFloat(floatName, value);
    }

    protected void SetFloat(string floatName, float value, CallbackFinished callback, string animationName)
    {
        StartIfCallback(callback, animationName);
        SetFloat(floatName, value);
    }

    private void StartIfCallback(CallbackFinished callback, string animationName)
    {
        if (callback == null)
            throw new ArgumentNullException("Delegate is Null");

        if (_coroutine != null)
            _monobehaviour.StopCoroutine(_coroutine);

        _coroutine = _monobehaviour.StartCoroutine(WaitForAnimation(animationName, callback));
    }

    private IEnumerator WaitForAnimation(string animationName, CallbackFinished callback)
    {
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            yield return null;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1f)
        {
            stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            yield return null;
        }

        callback?.Invoke();
    }
}