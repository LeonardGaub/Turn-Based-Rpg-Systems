using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;
    private Action onAttackComplete;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string trigger, Action onHit)
    {
        onAttackComplete = onHit;
        anim.SetTrigger(trigger);
    }

    public void AnimationComplete()
    {
        onAttackComplete.Invoke();
    }
}
