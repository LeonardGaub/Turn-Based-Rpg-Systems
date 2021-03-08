using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] List<Quest> quests;

    public void GiveQuest(int index)
    {
        FindObjectOfType<QuestList>().AddQuest(quests[index]);
    }
}
