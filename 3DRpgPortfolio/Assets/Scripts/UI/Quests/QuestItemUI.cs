using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI progress;

    private QuestStatus _quest;
    
    public void SetUp(QuestStatus status)
    {
        _quest = status;
        title.text = status.GetQuest().GetTitle();
        progress.text = status.GetComepletedCount() + "/" + status.GetQuest().GetObjectiveCount();
    }

    public QuestStatus GetQuestStatus()
    {
        return _quest;
    }
}
