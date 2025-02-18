using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Quests
{
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;

    public int id;
    public int previousQuestId;
    public int npcId;
    
    public Transform questLocation;

    public QuestGoal goal;
    public Quests nextQuest;
    public Quests previousQuest;
    [NonSerialized] public Dialogue dialogue;

    public event Action<Quests> QuestStateChanged;

    public void Activate()
    {
        if (previousQuestId == -1 || (previousQuest != null && previousQuest.IsCompleted()))
        {
            isActive = true;
            QuestStateChanged?.Invoke(this);
        }
    }

    public bool IsPreviousQuestCompleted()
    {
        if (previousQuest == null || !previousQuest.isActive && previousQuest.goal.IsReached())
        {
            return true;
        }
        return false;
    }

    public void IncreaseCurrentAmount()
    {
        goal.currentAmount++;
    }

    public void Complete()
    {
        goal.IsReached();
        isActive = false;
        QuestStateChanged?.Invoke(this);
        Debug.Log(title + " was completed!");
    }

    public bool IsCompleted()
    {
        return !isActive && goal.IsReached();
    }

    public void NotifyQuestStateChanged()
    {
        QuestStateChanged?.Invoke(this);
    }
}
