using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private const float Distance = 0.14f;

    private readonly RaycastHit[] _hits = new RaycastHit[4];

    public bool IsGround()
    {
        Ray ray = new(transform.position, Vector3.down);
        int hitCount = Physics.RaycastNonAlloc(ray, _hits, Distance);

        for (int i = 0; i < hitCount; i++)
            if (_hits[i].collider.TryGetComponent(out Player _) == false)
                return true;

        return false;
    }
}