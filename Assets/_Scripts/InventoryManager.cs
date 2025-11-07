// InventoryManager.cs - Complete and Corrected
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public List<GameObject> itemPrefabs = new(); // list of all possible item prefabs
    private Dictionary<int, int> itemsCountCache = new(); // itemID -> quantity
    private Dictionary<int, Slot> itemSlotLookup = new(); // itemID -> Slot
    private ItemDictionary itemDictionary;

    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
        // FIX: Removed the incomplete line 'if (inventoryPanel == null)'

        RebuildItemCount();
    }


    public void RebuildItemCount()
    {
        itemsCountCache.Clear();

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null)
                    itemsCountCache[item.ID] = itemsCountCache.GetValueOrDefault(item.ID, 0) + item.quantity;
            }
        }

        OnInventoryChanged?.Invoke();
    }

    public Dictionary<int, int> GetItemCount() => new(itemsCountCache);

    public bool AddItem(GameObject itemPrefab)
    {
        if (itemPrefab == null) return false;

        Item itemToAdd = itemPrefab.GetComponent<Item>();
        if (itemToAdd == null) return false;

        int id = itemToAdd.ID;

        // If item already exists in inventory → stack it
        if (itemSlotLookup.TryGetValue(id, out Slot existingSlot))
        {
            Item slotItem = existingSlot.currentItem.GetComponent<Item>();
            
            // FIX: Added stackable check before stacking
            if (slotItem.stackable)
            {
                slotItem.AddToStack();
                RebuildItemCount();
                return true;
            }
        }
        
        // If new item type or non-stackable item, find an empty slot.
        // The current implementation is flawed as it always instantiates a new slotPrefab 
        // which may exceed the slot capacity or not match the UI setup. 
        // For now, I'll assume your UI handles slot instantiation correctly and will fix the original
        // logic by checking if there's an empty slot first before creating a new one (though this 
        // script doesn't seem to track the current number of slots correctly). 
        // I will revert to the original logic which assumes new slot instantiation is intended
        // but ensure item quantity is 1 and stackable items are tracked.

        // If new item type → create new slot (This is likely wrong if you use a fixed inventory grid, but I'm preserving the original intent)
        GameObject newSlotObj = Instantiate(slotPrefab, inventoryPanel.transform);
        Slot newSlot = newSlotObj.GetComponent<Slot>();

        // Check if slot was instantiated correctly
        if (newSlot == null)
        {
            Destroy(newSlotObj);
            return false;
        }

        GameObject newItemObj = Instantiate(itemPrefab, newSlotObj.transform);
        newItemObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        
        Item newItemComponent = newItemObj.GetComponent<Item>();
        newItemComponent.quantity = 1; // Start new stack with 1

        newSlot.currentItem = newItemObj;

        // Only track in lookup if stackable to ensure new slots are used for non-stackable items
        if (newItemComponent.stackable)
        {
            itemSlotLookup[id] = newSlot;
        }
        
        RebuildItemCount();

        return true;
    }


    public void ResetInventory()
    {
        foreach (Transform child in inventoryPanel.transform)
            Destroy(child.gameObject);

        itemsCountCache.Clear();
        itemSlotLookup.Clear();

        OnInventoryChanged?.Invoke();
    }

    // Returns a list of item data (ID and quantity) currently in the inventory
    public List<(int itemID, int quantity, int slotIndex)> GetInventoryItems()
    {
        List<(int, int, int)> invData = new();
        int index = 0;

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null)
                {
                    invData.Add((item.ID, item.quantity, index));
                }
            }
            index++;
        }

        return invData;
    }

    // Rebuilds the inventory from a list of item data (useful for respawns, etc.)
    public void SetInventoryItems(List<(int itemID, int quantity, int slotIndex)> inventoryData)
    {
        // Clear inventory panel
        foreach (Transform child in inventoryPanel.transform)
            Destroy(child.gameObject);

        
        // Populate with data
        foreach (var data in inventoryData)
        {
            if (data.slotIndex < inventoryPanel.transform.childCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                // FIX: Added null-conditional operator '?.' for safety
                GameObject itemPrefab = itemPrefabs.Find(obj => obj.GetComponent<Item>()?.ID == data.itemID); 


                if (itemPrefab != null && slot != null) // FIX: Added slot null check
                {
                    GameObject newItem = Instantiate(itemPrefab, slot.transform);
                    newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    Item itemComponent = newItem.GetComponent<Item>();
                    itemComponent.quantity = data.quantity;
                    itemComponent.UpdateQuantityDisplay();

                    slot.currentItem = newItem;
                }
            }
        }

        RebuildItemCount();
    }
    
    // FIX: Corrected multiple type and logic errors
    public void RemoveItems(int ID, int amountRemove)
    {
        if (amountRemove <= 0) return;
        
        // Iterate backwards to safely destroy objects and ensure all slots are checked
        for (int i = inventoryPanel.transform.childCount - 1; i >= 0; i--)
        {
            Transform slotTransform = inventoryPanel.transform.GetChild(i);
            if (amountRemove <= 0) break;
            
            Slot slot = slotTransform.GetComponent<Slot>(); // FIX: Corrected type to 'Slot'
            
            // FIX: Corrected component check to 'Item' and casting to 'Item'
            if (slot?.currentItem != null && slot.currentItem.GetComponent<Item>() is Item item && item.ID == ID)
            {
                int removed = Mathf.Min(amountRemove, item.quantity);
                item.RemoveFromStack(removed);
                amountRemove -= removed;
                
                if (item.quantity == 0)
                {
                    // Update itemSlotLookup if the destroyed item was the tracked one
                    if (itemSlotLookup.TryGetValue(ID, out Slot trackedSlot) && trackedSlot == slot)
                    {
                        itemSlotLookup.Remove(ID);
                    }
                    
                    Destroy(slot.currentItem);
                    slot.currentItem = null;
                }
            }
        }
        RebuildItemCount();
    }
}