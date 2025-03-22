using System.Collections;
using UnityEngine;

public class PositionAdjuster
{
    private const float PositionThreshold = 0.01f;
    private const float RotationThreshold = 0.01f;

    private readonly MonoBehaviour _monoBehaviour;
    private readonly float _speedMoving;
    private readonly float _speedRotation;
    private Coroutine _coroutine;

    public PositionAdjuster(MonoBehaviour monoBehaviour, float speedMoving, float speedRotation)
    {
        _monoBehaviour = monoBehaviour;
        _speedMoving = speedMoving;
        _speedRotation = speedRotation;
    }

    public void Adjust(Transform obj, Transform target, CallbackFinished callback = null)
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);

        _coroutine = _monoBehaviour.StartCoroutine(AdjustingOverTime(obj, target, callback));
    }

    private IEnumerator AdjustingOverTime(Transform obj, Transform target, CallbackFinished callback)
    {
        while (Vector3.Distance(obj.position, target.position) > PositionThreshold
            || Quaternion.Angle(obj.rotation, target.rotation) > RotationThreshold)
        {
            obj.SetPositionAndRotation(
                Vector3.MoveTowards(obj.position, target.position, _speedMoving * Time.deltaTime),
                Quaternion.RotateTowards(obj.rotation, target.rotation, _speedRotation * Time.deltaTime));

            yield return null;
        }

        obj.SetPositionAndRotation(
            target.position,
            target.rotation);

        _coroutine = null;
        callback?.Invoke();
    }
}