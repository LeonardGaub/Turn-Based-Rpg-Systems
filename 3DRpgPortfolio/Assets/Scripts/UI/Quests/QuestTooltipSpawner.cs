using Rpg.UI.Tooltips;
using UnityEngine;

public class QuestTooltipSpawner : TooltipSpawner
{
    
    public override void UpdateTooltip(GameObject tooltip)
    {
        QuestStatus currentQuestStatus = GetComponent<QuestItemUI>().GetQuestStatus();
        tooltip.GetComponent<QuestTooltipUI>().SetUp(currentQuestStatus);
    }

    public override bool CanCreateTooltip()
    {
        return true;
    }
}
