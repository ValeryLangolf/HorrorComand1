using System.Collections;
using UnityEngine;

public class PositionAdjuster : MonoBehaviour
{
    private const float RotationSensitivity = 500f;

    private readonly WaitForFixedUpdate _wait = new();

    public delegate void AdjustmentCallback();

    public void Adjust(Transform obj, Transform target, float speed, AdjustmentCallback callback) =>
        StartCoroutine(AdjustingOverTime(obj, target, speed, callback));

    private IEnumerator AdjustingOverTime(Transform obj, Transform target, float speed, AdjustmentCallback callback)
    {
        float rotationSpeed = speed * RotationSensitivity;

        while (Vector3.Distance(obj.position, target.position) > 0.1f || Quaternion.Angle(obj.rotation, target.rotation) > 1f)
        {
            obj.SetPositionAndRotation(
                Vector3.MoveTowards(obj.position, target.position, speed * Time.fixedDeltaTime), 
                Quaternion.RotateTowards(obj.rotation, target.rotation, rotationSpeed * Time.fixedDeltaTime));

            yield return _wait;
        }

        obj.SetPositionAndRotation(
            target.position, 
            target.rotation);

        callback?.Invoke();
    }
}