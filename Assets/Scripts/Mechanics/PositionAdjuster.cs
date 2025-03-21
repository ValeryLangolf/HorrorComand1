using System.Collections;
using UnityEngine;

public delegate void AdjustmentCallback();

public class PositionAdjuster
{
    private const float PositionThreshold = 0.01f;
    private const float RotationThreshold = 0.01f;

    private readonly WaitForFixedUpdate _wait = new();
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

    public void Adjust(Transform obj, Transform target, AdjustmentCallback callback = null)
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);

        _coroutine = _monoBehaviour.StartCoroutine(AdjustingOverTime(obj, target, callback));
    }

    public void Adjust(Transform obj, Vector3 targetPosition, Quaternion targetRotation, AdjustmentCallback callback = null)
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);

        _coroutine = _monoBehaviour.StartCoroutine(AdjustingOverTime(obj, targetPosition, targetRotation, callback));
    }

    private IEnumerator AdjustingOverTime(Transform obj, Transform target, AdjustmentCallback callback)
    {
        while (Vector3.Distance(obj.position, target.position) > PositionThreshold
            || Quaternion.Angle(obj.rotation, target.rotation) > RotationThreshold)
        {
            obj.SetPositionAndRotation(
                Vector3.MoveTowards(obj.position, target.position, _speedMoving * Time.fixedDeltaTime),
                Quaternion.RotateTowards(obj.rotation, target.rotation, _speedRotation * Time.fixedDeltaTime));

            yield return _wait;
        }

        obj.SetPositionAndRotation(
            target.position,
            target.rotation);

        _coroutine = null;
        callback?.Invoke();
    }

    private IEnumerator AdjustingOverTime(Transform obj, Vector3 targetPosition, Quaternion targetRotation, AdjustmentCallback callback)
    {
        while (Vector3.Distance(obj.position, targetPosition) > PositionThreshold
            || Quaternion.Angle(obj.rotation, targetRotation) > RotationThreshold)
        {
            obj.SetPositionAndRotation(
                Vector3.MoveTowards(obj.position, targetPosition, _speedMoving * Time.fixedDeltaTime),
                Quaternion.RotateTowards(obj.rotation, targetRotation, _speedRotation * Time.fixedDeltaTime));

            yield return _wait;
        }

        obj.SetPositionAndRotation(
            targetPosition,
            targetRotation);

        _coroutine = null;
        callback?.Invoke();
    }
}