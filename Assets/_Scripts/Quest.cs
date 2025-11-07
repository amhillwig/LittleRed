using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq; // for TrueForAll

[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    public string questName = "";
    public string questID = "";
    public List<QuestObjective> objectives;
    public List<QuestReward> rewards;

    public bool IsCompleted => objectives.TrueForAll(o => o.IsCompleted);
}

[System.Serializable]
public class QuestObjective
{
    public string objectiveID;
    public int requiredAmount;
    public int currentAmount;
    public bool IsCompleted => currentAmount >= requiredAmount;
}

[System.Serializable]
public class QuestProgress
{
    public Quest quest;
    public List<QuestObjective> objectives;

    public QuestProgress(Quest quest)
    {
        this.quest = quest;
        objectives = new List<QuestObjective>();

        // Deep copy
        foreach (var obj in quest.objectives)
        {
            objectives.Add(new QuestObjective
            {
                objectiveID = obj.objectiveID,
                requiredAmount = obj.requiredAmount,
                currentAmount = 0
            });
        }
    }

    public bool IsCompleted => objectives.TrueForAll(o => o.IsCompleted);
}
[System.Serializable]
public class QuestReward
{
    public RewardType type;
    public int rewardID;
}
public enum RewardType {Item, Follow, Nothing};