using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class QuestTooltipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Transform objectiveContainer;

    [SerializeField] private GameObject objectivePrefab;
    [SerializeField] private GameObject objectiveIncompletePrefab;
    
    public void SetUp(QuestStatus status) 
    {
        title.text = status.GetQuest().GetTitle();
        foreach (Transform child in transform)
        {
            Destroy(child);
        }

        foreach (var objective in status.GetQuest().GetObjectives())
        {
            GameObject prefab = objectiveIncompletePrefab;
            if (status.IsObjectiveComplete(objective.refrence))
            {
                prefab = objectivePrefab;
            }
            var newObjective = Instantiate(prefab, objectiveContainer.transform);
            
            newObjective.GetComponentInChildren<TextMeshProUGUI>().text = objective.description;
        }

        rewardText.text = GetRewardText(status.GetQuest());
    }

    private string GetRewardText(Quest quest)
    {
        string rewardText = "";
        foreach (var reward in quest.GetRewards())
        {
            if (rewardText != "")
            {
                rewardText += ", \n";
            }

            if (reward.amount > 1)
            {
                rewardText += reward.amount + " ";
            }
            rewardText += reward.item.GetDisplayName();
        }

        if (rewardText == "")
        {
            rewardText = "No Reward";
        }

        rewardText += ".";
        return rewardText;
    }
}
