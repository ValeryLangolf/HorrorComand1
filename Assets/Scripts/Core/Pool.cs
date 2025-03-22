using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour, IDeactivatable<T>
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Stack<T> _elements = new();
    private readonly Action<T> _initializeCallback;

    public Pool(T prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public T Get()
    {
        T element = _elements.Count > 0 ? _elements.Pop() : CreateNew();
        element.gameObject.SetActive(true);
        element.Deactivated += Return;

        return element;
    }

    private void Return(T element)
    {
        element.Deactivated -= Return;
        element.gameObject.SetActive(false);
        _elements.Push(element);
    }

    private T CreateNew()
    {
        T instance = UnityEngine.Object.Instantiate(_prefab, _parent);

        return instance;
    }
}