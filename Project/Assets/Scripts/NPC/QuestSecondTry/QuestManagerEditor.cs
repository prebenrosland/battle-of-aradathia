using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{
    private Quests newQuest = new Quests();
    private bool showNewQuest = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QuestManager questManager = (QuestManager)target;

        GUILayout.Space(10);
        GUILayout.Label("Add a new quest:", EditorStyles.boldLabel);

        showNewQuest = EditorGUILayout.Foldout(showNewQuest, "New Quest");

        if (showNewQuest)
        {
            EditorGUI.indentLevel++;
            newQuest.id = EditorGUILayout.IntField("ID:", newQuest.id);
            newQuest.title = EditorGUILayout.TextField("Title:", newQuest.title);
            newQuest.description = EditorGUILayout.TextField("Description:", newQuest.description);
            newQuest.experienceReward = EditorGUILayout.IntField("Experience Reward:", newQuest.experienceReward);
            newQuest.goldReward = EditorGUILayout.IntField("Gold Reward:", newQuest.goldReward);
            newQuest.previousQuestId = EditorGUILayout.IntField("Previous Quest ID:", newQuest.previousQuestId);
            EditorGUI.indentLevel--;
        }

        if (GUILayout.Button("Add Quest"))
        {
            if (newQuest != null)
            {
                questManager.questList.Add(newQuest);
                newQuest = new Quests();
            }
            else
            {
                Debug.LogError("No quest information provided.");
            }
        }
    }
}
