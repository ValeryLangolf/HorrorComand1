using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private HintInteraction _round;
    [SerializeField] private float _distance;

    private readonly RaycastHit[] _hits = new RaycastHit[5];

    public HintTrigger CurrentInteraction { get; private set; }

    private void Awake() =>
        _round.Disable();

    private void Update() =>
        ShootRay();

    private void ShootRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        int hitCount = Physics.RaycastNonAlloc(ray, _hits, _distance);

        for (int i = 0; i < hitCount; i++)
            if (IsGameInteractive(_hits[i]))
                return;

        CurrentInteraction = null;
        _round.Disable();
    }

    private bool IsGameInteractive(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out HintTrigger interaction) == false)
            return false;

        CurrentInteraction = interaction;
        _round.Enable();

        return true;
    }
}