using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] private QuestItemUI questPrefab;
    private QuestList questList;
    private void Start()
    {
        questList = FindObjectOfType<QuestList>();
         questList.onUpdate += Redraw;
         Redraw();
    }

    private void Redraw()
    {
        foreach (QuestItemUI child in transform.GetComponentsInChildren<QuestItemUI>())
        {
            Destroy(child.gameObject);
        }
        foreach (QuestStatus status in questList.GetStatuses())
        {
            if (status.IsComplete()) { continue; }
            var newQuest = Instantiate(questPrefab, transform);
            newQuest.SetUp(status);
        }
    }
}
