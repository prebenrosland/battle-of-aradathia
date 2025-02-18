using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogues;
    public QuestManager questManager;

    public UnityEvent OnDialogueEnd;

    public void TriggerDialogueByQuestID(int questId)
    {
        Quests quest = questManager.GetQuestByID(questId);

        if (quest == null)
        {
            Debug.LogError("Quest ID not found.");
            return;
        }

        Dialogue dialogue = System.Array.Find(dialogues, d => d.questId == questId);
        if (dialogue != null)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
        }
        else
        {
            Debug.LogError("Dialogue not found for quest ID: " + questId);
        }
    }

    public void EndDialogue()
    {
        OnDialogueEnd.Invoke();
    }
}
