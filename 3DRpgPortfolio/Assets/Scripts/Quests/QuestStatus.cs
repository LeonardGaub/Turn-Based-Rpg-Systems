using System;
using System.Collections.Generic;
using Rpg.Saving;

[Serializable]
public class QuestStatus
{
    private Quest _quest;
    private List<string> _completedObjectives = new List<string>();

    [Serializable]
    private class QuestStatusRecord
    {
        public string questName;
        public List<string> completedObjectives = new List<string>();
    }

    public QuestStatus(Quest quest)
    {
        this._quest = quest;
    }

    public QuestStatus(object state)
    {
        QuestStatusRecord status = state as QuestStatusRecord;
     
        _quest =  Quest.GetByName(status.questName);
        _completedObjectives = status.completedObjectives;
    } 
    
    public Quest GetQuest()
    {
        return _quest;
    }

    public int GetComepletedCount()
    {
        return _completedObjectives.Count;
    }

    public bool IsObjectiveComplete(string objective)
    {
        return _completedObjectives.Contains(objective);
    }

    public void CompleteObjective(string objective)
    {
        if (_quest.HasObjective(objective))
        {
            _completedObjectives.Add(objective);   
        }
    }

    public object CaptureState()
    {
        QuestStatusRecord state = new QuestStatusRecord();
        state.questName = _quest.name;
        state.completedObjectives = _completedObjectives;

        return state;
    }


    public bool IsComplete()
    {
        foreach (var objective in _quest.GetObjectives())
        {
            if (!_completedObjectives.Contains(objective.refrence))
            {
                return false;
            }
        }

        return true;
    }
}
