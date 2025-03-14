using System;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    public event Action LeftFoodSteppedOn;
    public event Action RightFoodSteppedOn;

    public void InvokeLeftFoodSteppedOn() =>
        LeftFoodSteppedOn?.Invoke();

    public void InvokeRightFoodSteppedOn() =>
        RightFoodSteppedOn?.Invoke();
}