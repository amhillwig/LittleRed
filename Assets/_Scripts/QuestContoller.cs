using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestContoller : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    //public List<QuestProgress> activateQuests = new();


    private void Awake() {
        if (Instance = null) Instance = this;
        else Destroy(gameObject);
    }
    public void AcceptQuest(QuestContoller quest)
    {
        if (isQuestActive(quest.questID)) return;

        //activateQuests.Add(new QuestProgress(quest));
    }
    public bool isQuestActive(string questID) => activateQuests.Exists(QuestContoller => q.QuestID == questID);
}


