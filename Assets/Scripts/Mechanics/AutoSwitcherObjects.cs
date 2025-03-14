using System.Collections.Generic;
using UnityEngine;

public class AutoSwitcherObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listForOn;
    [SerializeField] private List<GameObject> _listForOff;

    private void Awake()
    {
        foreach (GameObject item in _listForOn)
            item.gameObject.SetActive(true);

        foreach (GameObject item in _listForOff)
            item.gameObject.SetActive(false);
    }
}