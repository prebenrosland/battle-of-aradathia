using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int gold = 0;
    public TextMeshProUGUI goldValue;

    public LevelSystem levelSystem;
    public TextMeshProUGUI Level;
    public Slider experienceProgressBar;
    public TextMeshProUGUI currentExperienceText;
    public TextMeshProUGUI experienceToNextLevelText;

    private Quests currentQuest;

    public Merchant merchant;

    public List<Quests> activeQuests = new List<Quests>();
    public Dictionary<int, Transform> npcQuestLocations = new Dictionary<int, Transform>();

    void Awake()
    {
        levelSystem = new LevelSystem(1);
        levelSystem.OnLevelUp += HandleLevelUp;
        UpdateLevelText();
        UpdateExperienceTexts();
    }

    void Update()
    {
        goldValue.text = gold.ToString();
        merchant.goldValue.text = gold.ToString();

        activeQuests.RemoveAll(q => !q.isActive);
    }
    
    private void HandleLevelUp()
    {
        GetComponent<AS.PlayerStats>().IncreaseStats();
        UpdateLevelText();
        UpdateExperienceTexts();
    }

    private void UpdateLevelText()
    {
        Level.text = "" + levelSystem.Level.ToString();
    }

    public void GoBattle()
    {
        levelSystem.AddExperience(20);
        gold += 2;

        UpdateExperienceProgressBar();
        UpdateExperienceTexts();
        CheckQuestCompletion();
    }

    private void UpdateExperienceTexts()
    {
        currentExperienceText.text = levelSystem.Experience.ToString();
        experienceToNextLevelText.text = experienceToNextLevelText.text = "/ " + levelSystem.ExperienceToNextLevel[levelSystem.Level - 1].ToString();
    }

    private void UpdateExperienceProgressBar()
    {
        int currentLevelExp = levelSystem.Experience;
        int expToNextLevel = levelSystem.ExperienceToNextLevel[levelSystem.Level - 1];
        float progress = (float)currentLevelExp / expToNextLevel;

        experienceProgressBar.value = progress;
    }

    private void CheckQuestCompletion()
    {
        List<Quests> completedQuests = new List<Quests>();

        foreach (Quests quest in activeQuests)
        {
            if (quest.goal.goalType == GoalType.Kill || quest.goal.goalType == GoalType.KillBoss)
            {
                quest.goal.currentAmount += 1;

                if (quest.goal.IsReached())
                {
                    levelSystem.AddExperience(quest.experienceReward);
                    UpdateExperienceProgressBar();
                    UpdateExperienceTexts();
                    gold += quest.goldReward;
                    quest.Complete();
                    completedQuests.Add(quest);

                    QuestUIManager questUIManager = FindObjectOfType<QuestUIManager>();
                    if (quest.nextQuest != null)
                    {
                        quest.nextQuest.NotifyQuestStateChanged();
                    }
                }
            }
        }

        foreach (Quests completedQuest in completedQuests)
        {
            activeQuests.Remove(completedQuest);

            QuestUIManager questUIManager = FindObjectOfType<QuestUIManager>();
            if (questUIManager != null)
            {
                questUIManager.HideQuestAcceptedWindow();
            }
        }
        
        if (completedQuests.Count > 0)
        {
            UpdateMinimapObjective();
        }
    }

    public void AcceptQuest(Quests quest)
    {
        if (quest.IsPreviousQuestCompleted())
        {
            activeQuests.Add(quest);
        }
    }

    public void PickUpGold(int amount)
    {
        gold += amount;
    }

    public void RegisterBossDeath(BossStats bossStats)
    {
        bossStats.OnBossDeath.AddListener(HandleBossDeath);
    }

    private void HandleBossDeath()
    {
        List<Quests> questsCopy = new List<Quests>(activeQuests);

        foreach (Quests quest in questsCopy)
        {
            if (quest.goal.goalType == GoalType.KillBoss)
            {
                quest.goal.currentAmount++;
                CheckQuestCompletion();
            }
        }
    }

    public void CompleteBossKillQuest(int questID)
    {
        Quests bossQuest = activeQuests.Find(quest => quest.id == questID);

        if (bossQuest != null && bossQuest.isActive && bossQuest.goal.goalType == GoalType.KillBoss)
        {
            bossQuest.goal.currentAmount++;
            if (bossQuest.goal.IsReached())
            {
                bossQuest.Complete();
            }
        }
    }

    public void SetCurrentQuest(Quests quest)
    {
        currentQuest = quest;
        UpdateMinimapQuestObjective(currentQuest.questLocation);
    }

    public void UpdateMinimapQuestObjective(Transform questLocation)
    {
        MinimapObjectiveController objectiveController = FindObjectOfType<MinimapObjectiveController>();
        if (objectiveController != null)
        {
            objectiveController.questLocation = questLocation;
        }
    }

    public void UpdateMinimapObjective()
    {
        Quests availableQuest = activeQuests.Find(quest => quest.isActive && !quest.goal.IsReached());

        if (availableQuest != null)
        {
            SetCurrentQuest(availableQuest);
        }
        else
        {
            Transform firstAvailableNPC = null;
            QuestManager questManager = FindObjectOfType<QuestManager>();

            foreach (KeyValuePair<int, Transform> npcEntry in npcQuestLocations)
            {
                int npcId = npcEntry.Key;
                Transform npcLocation = npcEntry.Value;
                List<Quests> npcQuests = questManager.GetQuestsByNPCId(npcId);

                foreach (Quests npcQuest in npcQuests)
                {
                    if (!activeQuests.Contains(npcQuest) && npcQuest.IsPreviousQuestCompleted())
                    {
                        firstAvailableNPC = npcLocation;
                        break;
                    }
                }

                if (firstAvailableNPC != null)
                {
                    break;
                }
            }

            MinimapObjectiveController objectiveController = FindObjectOfType<MinimapObjectiveController>();
            if (objectiveController != null)
            {
                objectiveController.questLocation = firstAvailableNPC;
            }
        }
    }
}
