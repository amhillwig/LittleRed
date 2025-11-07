// QuestController.cs - Complete and Corrected
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // Added for TrueForAll

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    public List<QuestProgress> activateQuests = new();
    public List<string> handinQuestIDs = new();

    // Assuming this enum is needed for QuestObjective type checking
    // Add this to QuestController.cs or move it to Quest.cs if appropriate
    public enum ObjectType { CollectItem, KillEnemy, TalkToNPC }; 

    private void Awake() {
        // FIX: Corrected singleton check: Instance == null
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // FIX: Corrected event subscription to use method group
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged += CheckInventoryForQuests;
        }
    }
    
    // FIX: Changed parameter type from QuestController to Quest
    public void AcceptQuest(Quest quest)
    {
        if (quest == null) return;
        if (IsQuestActive(quest.questID)) return;

        activateQuests.Add(new QuestProgress(quest));
    }
    
    public bool IsQuestActive(string questID) => activateQuests.Exists(q => q.quest.questID == questID);


    public bool IsWoodsmanQuest() => activateQuests.Exists(q => q.quest.questID == "1");

    // FIX: Corrected logic to iterate through activeQuests and check their objectives against inventory
    public void CheckInventoryForQuests()
    {
        if (InventoryManager.Instance == null) return;
        Dictionary<int, int> itemCounts = InventoryManager.Instance.GetItemCount();
        
        foreach (QuestProgress questProgress in activateQuests)
        {
            foreach (QuestObjective objective in questProgress.objectives)
            {
                // Assuming CollectItem objectives have their itemID stored in objectiveID
                // And assuming the objective class supports 'type' property (requires change in Quest.cs)
                // Since Quest.cs doesn't define 'type', we'll assume ALL objectives check the inventory if objectiveID is an int.
                if (!int.TryParse(objective.objectiveID, out int itemID)) continue; 

                // Use Mathf.Min to ensure currentAmount doesn't exceed requiredAmount
                int newAmount = itemCounts.TryGetValue(itemID, out int count) ? Mathf.Min(count, objective.requiredAmount) : 0;
                
                if (objective.currentAmount != newAmount) 
                {
                    objective.currentAmount = newAmount;
                    // You might want to add an event here for UI updates
                }
            }
        }
    }
    
    // FIX: Corrected lambda to check questID on the nested 'quest' object
    public bool IsQuestCompleted(string questID)
    {
        QuestProgress questProgress = activateQuests.Find(q => q.quest.questID == questID);
        // Note: The QuestProgress class already has an IsCompleted property, so we can use that.
        return questProgress != null && questProgress.IsCompleted;
    }
    
    public void HandInQuest(string questID)
    {
        // Must check if it's completed before attempting to remove items
        if (!IsQuestCompleted(questID)) return;
        
        // Remove items *before* handing in the quest (so the items are gone)
        if (!RemoveItems(questID)) return; 
        
        QuestProgress quest = activateQuests.Find(q => q.quest.questID == questID);
        if (quest != null)
        {
            handinQuestIDs.Add(questID);
            activateQuests.Remove(quest);
        }
    }
    
    public bool IsQuestHandedIn(string questID)
    {
        return handinQuestIDs.Contains(questID);
    }

    // FIX: Refactored logic to properly check if items are available and then remove them.
    // NOTE: This assumes QuestObjective has a 'type' property, but it's not in Quest.cs.
    // I've temporarily defined a placeholder enum at the top.
    public bool RemoveItems (string questID)
    {
        QuestProgress questProgress = activateQuests.Find(q => q.quest.questID == questID);
        if (questProgress == null) return false;
        
        // 1. Determine requirements
        Dictionary<int, int> required = new();
        foreach (QuestObjective obj in questProgress.objectives)
        {
            // Assuming we are only checking the inventory (CollectItem)
            // You will need to add the 'type' property to QuestObjective in Quest.cs
            // For now, assume any objectiveID that is an int is an item collection objective
            if (int.TryParse(obj.objectiveID, out int itemID)) { 
                required[itemID] = obj.requiredAmount;
            }
        }

        // 2. Check if inventory meets ALL requirements
        Dictionary<int, int> itemCounts = InventoryManager.Instance.GetItemCount();
        foreach (var itemRequirement in required)
        {
            // If the inventory count is less than the required amount, fail
            if (itemCounts.GetValueOrDefault(itemRequirement.Key) < itemRequirement.Value)
            {
                return false;
            }
        }
        
        // 3. Remove all required items
        foreach (var itemRequirement in required)
        {
            InventoryManager.Instance.RemoveItems(itemRequirement.Key, itemRequirement.Value);
        }
        
        // Success
        return true;
    }
}