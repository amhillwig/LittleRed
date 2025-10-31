using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenuAttribute(fileName = "Quest", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject {
    
    public string questName = "";
    public List<QuestObjective> objectives;

    
    [System.Serializable]
    public class QuestObjective
    {
        public string objectiveID;
        public int requiredAmount, currentAmount;
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

            //deep copy
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

}
