using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameDevTV.Inventories;
using GameDevTV.Saving;
using UnityEngine;

public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
{
    [SerializeField] private List<QuestStatus> statuses = new List<QuestStatus>();

    public event Action onUpdate;
    
    public IEnumerable GetStatuses()
    {
        return statuses;
    }

    public void AddQuest(Quest newQuest)
    {
        if (HasQuest(newQuest))
        {
            return;
        }
        QuestStatus newStatus = new QuestStatus(newQuest);
        statuses.Add(newStatus);
        if (onUpdate != null)
        {
            onUpdate();
        }
    }

    public bool HasQuest(Quest quest)
    {
        return GetQuestStatus(quest) != null;
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        QuestStatus status = GetQuestStatus(quest);
        status.CompleteObjective(objective);
        if (status.IsComplete())
        {
            GiveReward(quest);
        }
        if (onUpdate != null)
        {
            onUpdate();
        }
    }

    private void GiveReward(Quest quest)
    {
        foreach (var reward in quest.GetRewards())
        {
            Inventory playerInventory = GetComponent<Inventory>();
            bool wasSucess = playerInventory.AddToFirstEmptySlot(reward.item, reward.amount);
            if (!wasSucess)
            {
                GetComponent<ItemDropper>().DropItem(reward.item, reward.amount);
            }
        }
    }

    private QuestStatus GetQuestStatus(Quest quest)
    {
        foreach (var status in statuses)
        {
            if (status.GetQuest().Equals(quest))
            {
                return status;
            } 
        }

        return null;
    }

    public object CaptureState()
    {
        List<object> state = new List<object>();
        foreach (var questStatus in statuses)
        {
            state.Add(questStatus.CaptureState());
        }

        return state;
    }

    public void RestoreState(object state)
    {
        List<object> stateList = state as List<object>;
        if (stateList == null) return;
        
        statuses.Clear();
        foreach (object objectState in stateList)
        {
            statuses.Add(new QuestStatus(objectState));
        }


    }

    public bool? Evaluate(string predicate, string[] parameters)
    {
        switch (predicate)
        {
            case "Has Quest":
                return HasQuest(Quest.GetByName(parameters[0])); 
            case "Completed Quest":
                return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
        }

        return null;
    }
}
