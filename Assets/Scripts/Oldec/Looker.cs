using UnityEngine;

public class Looker : MonoBehaviour
{
    [SerializeField] private PlayerHead _playerHead;

    private void Update() =>
        transform.LookAt(_playerHead.transform);
}