using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AS;

public class QuestUIManager : MonoBehaviour
{
    public Player player;

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI goldText;

    public GameObject questAcceptedWindow;
    public TextMeshProUGUI questAcceptedTitleText;

    public CameraHandler cameraHandler;

    private Quests displayedQuest;

    public void OpenQuestWindow()
    {
        bool isActive = questWindow.activeSelf;
        questWindow.SetActive(!isActive);
        cameraHandler.ToggleCameraRotation(isActive);
        UpdateQuestInfo();
    }

    public void CloseWindow()
    {
        questWindow.SetActive(false);
        cameraHandler.ToggleCameraRotation(true);
    }

    private void UpdateQuestInfo()
    {
        displayedQuest = GetNextAvailableQuest();

        if (displayedQuest != null)
        {
            titleText.text = displayedQuest.title;
            descriptionText.text = displayedQuest.description;
            experienceText.text = displayedQuest.experienceReward.ToString();
            goldText.text = displayedQuest.goldReward.ToString();
        }
        else
        {
            titleText.text = "No available quests.";
            descriptionText.text = "";
            experienceText.text = "";
            goldText.text = "";
        }
    }

    private Quests GetNextAvailableQuest()
    {
        foreach (Quests quest in player.activeQuests)
        {
            if (!quest.IsCompleted())
            {
                return quest;
            }
        }

        return null;
    }

    public void AcceptQuest()
    {
        if (displayedQuest != null)
        {
            player.AcceptQuest(displayedQuest);
            ShowQuestAcceptedWindow(displayedQuest.title);
            UpdateQuestInfo();
        }
    }

    public void ShowQuestAcceptedWindow(string questTitle)
    {
        questAcceptedTitleText.text = questTitle;
        questAcceptedWindow.SetActive(true);
    }

    private IEnumerator HideQuestAcceptedWindowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        questAcceptedWindow.SetActive(false);
    }

    public void HideQuestAcceptedWindow()
    {
        questAcceptedWindow.SetActive(false);
    }
}
