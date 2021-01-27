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
        string rewardTextString = "";
        foreach (var reward in quest.GetRewards())
        {
            if (rewardTextString != "")
            {
                rewardTextString += ", \n";
            }

            if (reward.amount > 1)
            {
                rewardTextString += reward.amount + " ";
            }
            Debug.Log(rewardText);
            Debug.Log(reward.item.GetDisplayName());
            rewardTextString += reward.item.GetDisplayName();
        }

        if (rewardTextString == "")
        {
            rewardTextString = "No Reward";
        }

        rewardTextString += ".";
        return rewardTextString;
    }
}
