using System;
using System.Collections.Generic;
using Rpg.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Quest", menuName = "RPG/Quest", order = 0)]
public class Quest : ScriptableObject
{
   [SerializeField] private List<Objective> objectives = new List<Objective>();
   [SerializeField] List<Reward> rewards = new List<Reward>();
  
  
   [Serializable]
   public class Reward
   {
      [Min(1)]
      public int amount;
      public InventoryItem item;
   }

   [Serializable]
   public class Objective
   {
      public string refrence;
      public string description;
      public int currentAmount;
      public int neededAmount;

        public void Reset()
        {
            currentAmount = 0;
        }
   }
   public string GetTitle()
   {
      return name;
   }

   public IEnumerable<Objective> GetObjectives()
   {
      return objectives;
   }

   public IEnumerable<Reward> GetRewards()
   {
      return rewards;
   }

   public int GetObjectiveCount()
   {
      return objectives.Count;
   }

   public bool HasObjective(string objectiveRef)
   {
      foreach (var objective in objectives)
      {
         if (objective.refrence == objectiveRef)
         {
            return true;
         }
      }
 
      return false;
   }

    public Objective GetObjectiveFromString(string key)
    {
        foreach(var objective in objectives)
        {
            if(objective.refrence == key)
            {
                return objective;
            }
        }
        return null;
    }

   public static Quest GetByName(string questName)
   {
      var quests =  Resources.LoadAll<Quest>("");
      foreach (var quest in quests)
      {
         if (quest.name == questName)
         {
            return quest;
         }
      }

      return null;
   }
}
