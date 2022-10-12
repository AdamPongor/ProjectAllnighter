using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHelper : MonoBehaviour
{
    public UnityEvent OnAnimationTriggered, OnAttackPerformed;

    public void TriggerEvent()
    {
        OnAnimationTriggered?.Invoke();
    }
    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }
}
