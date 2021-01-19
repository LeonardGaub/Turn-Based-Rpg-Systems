using System.Collections;
using System.Collections.Generic;
using GameDevTV.Core.UI.Tooltips;
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
