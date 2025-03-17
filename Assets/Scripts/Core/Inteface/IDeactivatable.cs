using System;

public interface IDeactivatable<T>
{
    event Action<T> Deactivated;
}