using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quests> questList = new List<Quests>();

    private void Awake()
    {
        InitializeQuestChain();
    }

    private void InitializeQuestChain()
    {
        for (int i = 0; i < questList.Count - 1; i++)
        {
            questList[i].nextQuest = questList[i + 1];
            questList[i + 1].previousQuest = questList[i];
        }
    }

    public Quests GetQuestById(int id)
    {
        return questList.Find(quest => quest.id == id);
    }

    public Quests GetPreviousQuest(Quests quest)
    {
        return questList.Find(q => q.nextQuest == quest);
    }

    public Quests GetQuestByID(int questId)
    {
        return questList.Find(q => q.id == questId);
    }

    public List<Quests> GetQuestsByNPCId(int npcId)
    {
        return questList.FindAll(quest => quest.npcId == npcId);
    }

    public Quests GetNextAvailableQuest(Player player)
    {
        foreach (Quests quest in questList)
        {
            Quests previousQuest = GetPreviousQuest(quest);
            if (!player.activeQuests.Exists(q => q.id == quest.id) && (previousQuest == null || previousQuest.IsCompleted()) && !quest.IsCompleted())
            {
                return quest;
            }
        }
        return null;
    }
}
