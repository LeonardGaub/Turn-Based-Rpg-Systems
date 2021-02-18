using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletion : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private string objective;

    public void CompleteObjective()
    {
        QuestList questList = FindObjectOfType<QuestList>();
        questList.CompleteObjective(quest, objective);
    }
}
