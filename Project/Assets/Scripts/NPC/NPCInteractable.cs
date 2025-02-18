using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private List<int> questIds = new List<int>();
    public GameObject questMarker;
    public QuestUIManager questUIManager;

    public int npcId;
    public Transform questLocationMarker;

    private Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    private void Start()
    {
        UpdateQuestMarkerVisibility();

        QuestManager questManager = FindObjectOfType<QuestManager>();
        foreach (int questId in questIds)
        {
            Quests quest = questManager.GetQuestById(questId);
            if (quest != null)
            {
                quest.QuestStateChanged += OnQuestStateChanged;
            }
        }

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            RegisterNpcWithPlayer(player);
        }
    }

    public void RegisterNpcWithPlayer(Player player)
    {
        if (!player.npcQuestLocations.ContainsKey(npcId))
        {
            player.npcQuestLocations.Add(npcId, questLocationMarker);
        }
    }

    private void OnDestroy()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        
        if (questManager != null)
        {
            foreach (int questId in questIds)
            {
                Quests quest = questManager.GetQuestById(questId);
                if (quest != null)
                {
                    quest.QuestStateChanged -= OnQuestStateChanged;
                }
            }
        }
    }

    private void OnQuestStateChanged(Quests quest)
    {
        UpdateQuestMarkerVisibility();
    }

    public virtual void Interact(Transform interactorTransform)
    {
        Player player = interactorTransform.GetComponent<Player>();
        if (player != null)
        {
            float playerHeight = 1.7f;
            npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);

            QuestManager questManager = FindObjectOfType<QuestManager>();
            Quests nextAvailableQuest = GetNextAvailableQuest(player, questManager);

            DialogueTrigger dialogueTrigger = GetComponent<DialogueTrigger>();
            if (dialogueTrigger != null && nextAvailableQuest != null)
            {
                dialogueTrigger.TriggerDialogueByQuestID(nextAvailableQuest.id);
                animator.SetTrigger("Talk");
            }
            else
            {
                animator.SetTrigger("ShakeHead");
            }

            if (nextAvailableQuest != null)
            {
                player.activeQuests.Add(nextAvailableQuest);
                nextAvailableQuest.Activate();
                Debug.Log("Quest accepted: " + nextAvailableQuest.title);
                questUIManager.ShowQuestAcceptedWindow(nextAvailableQuest.title);
            }
            else
            {
                Debug.Log("No available quests.");
            }

            UpdateQuestMarkerVisibility();
        }
    }

    private Quests GetNextAvailableQuest(Player player, QuestManager questManager)
    {
        foreach (int questId in questIds)
        {
            Quests quest = questManager.GetQuestById(questId);
            Quests previousQuest = questManager.GetPreviousQuest(quest);

            if (quest != null && !player.activeQuests.Exists(q => q.id == questId) && (previousQuest == null || previousQuest.IsCompleted()) && !quest.IsCompleted())
            {
                return quest;
            }
        }

        return null;
    }

    private void UpdateQuestMarkerVisibility()
    {
        Player player = FindObjectOfType<Player>();
        QuestManager questManager = FindObjectOfType<QuestManager>();

        if (GetNextAvailableQuest(player, questManager) != null)
        {
            questMarker.SetActive(true);
        }
        else
        {
            questMarker.SetActive(false);
        }
    }

    public void DeactivateQuest(Quests quest)
    {
        if (quest != null && quest.isActive)
        {
            quest.isActive = false;
            Debug.Log("Quest deactivated: " + quest.title);
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
