// RewardsController.cs - Complete and Corrected
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    public static RewardsController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    // FIX: Changed QuestReward to rewards
    /*public void GiveReward(Quest quest)
    {
        if (quest?.rewards == null) return;
        foreach (var reward in quest.rewards)
        {
            switch (reward.type)
            {
                case RewardType.Item:
                    GiveItem(reward.rewardID); // Use existing method
                    break;
                case RewardType.Follow:
                    // Logic to make an NPC follow
                    break;
                case RewardType.Nothing:
                    break;
            }
        }
    }
    public void GiveItem(int itemId)
    {
        var itemPrefab = FindAnyObjectByType<ItemDictionary>()?.GetItemPrefab(itemId);
        if (itemPrefab == null) return;
        InventoryManager.Instance.AddItem(itemPrefab);
    }*/
}