using System;
using System.Collections;
using System.Collections.Generic;
using Rpg.Inventories;
using Rpg.Saving;
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
        foreach(var objective in newStatus.GetQuest().GetObjectives())
        {
            objective.Reset();
        }
        statuses.Add(newStatus);
        onUpdate?.Invoke();
    }

    private void RemoveQuest(QuestStatus questToRemove)
    {
        if (!HasQuest(questToRemove.GetQuest()))
        {
            return;
        }
        statuses.Remove(questToRemove);
        onUpdate?.Invoke();
    }

    public bool HasQuest(Quest quest)
    {
        return GetQuestStatus(quest) != null;
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        if (!HasQuest(quest))
        {
            return;
        }
        QuestStatus status = GetQuestStatus(quest);
        status.CompleteObjective(objective);
        if (status.IsComplete())
        {
            GiveReward(quest);
            RemoveQuest(GetQuestStatus(quest));
        }
        onUpdate?.Invoke();
    }

    public void KilledEnemy(string enemy)
    {
        foreach(QuestStatus status in statuses)
        {
            if (status.GetQuest().HasObjective(enemy))
            {
                CompleteObjective(status.GetQuest(), enemy);
            }
        }
    }

    private void GiveReward(Quest quest)
    {
        foreach (var reward in quest.GetRewards())
        {
            Inventory playerInventory = GetComponentInParent<Inventory>();
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
        Debug.Log("Save Staus");
        List<object> state = new List<object>();
        foreach (var questStatus in statuses)
        {
            state.Add(questStatus.CaptureState());
        }

        return state;
    }

    public void RestoreState(object state)
    {
        Debug.Log("Restore Status");
        List<object> stateList = state as List<object>;
        if (stateList == null) return;
        
        statuses.Clear();
        foreach (object objectState in stateList)
        {
            statuses.Add(new QuestStatus(objectState));
        }
        onUpdate?.Invoke();
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
