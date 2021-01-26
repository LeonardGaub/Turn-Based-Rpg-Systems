using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] List<Trigger> triggers = new List<Trigger>();

    public void Trigger(string actionToTrigger)
    {
        foreach (var trigger in triggers)
        {
            if (actionToTrigger == trigger.actionToRespond)
            {
                trigger.onTrigger.Invoke();
            }   
        }
    }
}

[Serializable]
public class Trigger
{
    public string actionToRespond;
    public UnityEvent onTrigger;
}
