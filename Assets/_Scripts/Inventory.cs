using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject panel, slotPrefab;
    public int slotCount = 1;
    private ItemDictionary dictionary;
    public List<GameObject> itemPrefabs = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dictionary = FindObjectOfType<ItemDictionary>();
    }

    public List<(int itemID, int quantity, int slotIndex)> GetInventoryItems()
    {
        List<(int, int, int)> invData = new();
        int index = 0;

        foreach (Transform slotTransform in panel.transform)
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
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);

        // Rebuild slots based on existing slotCount
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, panel.transform);
        }

        // Populate with data
        foreach (var data in inventoryData)
        {
            if (data.slotIndex < panel.transform.childCount)
            {
                Slot slot = panel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemPrefabs.Find(obj => obj.GetComponent<Item>().ID == data.itemID);

                if (itemPrefab != null)
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
    }
}
