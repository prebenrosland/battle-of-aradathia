using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletion : MonoBehaviour 
{
    public void CompleteQuest() {
        Player player = FindObjectOfType<Player>();
        Quests completedQuest = player.activeQuests.Find(q => q.isActive && q.goal.IsReached());
        if (completedQuest != null) {
            completedQuest.Complete();
        }
    }
}