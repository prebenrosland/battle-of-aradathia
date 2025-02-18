using UnityEngine;
using System;

public class LevelSystem
{
    public int Level { get; private set; }
    public int Experience { get; private set; }
    private int[] experienceToNextLevel;
    public int[] ExperienceToNextLevel => experienceToNextLevel;

    public event Action OnLevelUp;

    public LevelSystem(int startLevel)
    {
        Level = startLevel;
        Experience = 0;

        experienceToNextLevel = new int[30];
        for (int i = 0; i < experienceToNextLevel.Length; i++)
        {
            experienceToNextLevel[i] = 100 + i * 50;
        }
    }

    public void AddExperience(int amount)
    {
        Experience += amount;

        while (Experience >= experienceToNextLevel[Level - 1] && Level < 30)
        {
            Experience -= experienceToNextLevel[Level - 1];
            LevelUp();
        }
    }

    public int RemainingExperienceToNextLevel
    {
        get
        {
            if (Level < 30)
            {
                return experienceToNextLevel[Level - 1] - Experience;
            }
            else
            {
                return 0;
            }
        }
    }

    private void LevelUp()
    {
        if (Level < 30)
        {
            Level++;
            OnLevelUp?.Invoke();
        }
    }
}
