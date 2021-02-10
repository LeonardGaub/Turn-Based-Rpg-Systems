using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleAnimations : MonoBehaviour
{
    Animator anim;
    private Action onAnimationComplete;
    private Action onAttackComplete;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string trigger, Action onHit, Action onComplete)
    {
        onAttackComplete = onHit;
        onAnimationComplete = onComplete;
        anim.SetTrigger(trigger);
        if (!CheckAnimation(trigger))
        {
            StartCoroutine(AnimationSavety());
        }
    }

    private bool CheckAnimation(string paramName)
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    public void PlayAnimation(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    private IEnumerator AnimationSavety()
    {
        yield return new WaitForSeconds(3f);
        onAttackComplete.Invoke();
        Debug.LogError("Trigger or Animation not found!");
    }

    public void AttackComplete()
    {
        onAttackComplete.Invoke();
    }

    public void AnimationComplete()
    {
        onAnimationComplete.Invoke();
    }
}
