using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event Action LeftFoodSteppedOn;
    public event Action RightFoodSteppedOn;

    public void InvokeLeftFoodSteppedOn() =>
        LeftFoodSteppedOn?.Invoke();

    public void InvokeRightFoodSteppedOn() =>
        RightFoodSteppedOn?.Invoke();
}