using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Transform inventoryPanel;
    public GameObject inventorySlot;

    private HashSet<string> items = new HashSet<string>();

    void Awake()
    {
        if (Instance = null) Instance = this;
    }
    public void AddItem(string itemName)
    {
        if (items.Add(itemName))
        {
            UpdateUI();
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    void UpdateUI()
    {
        //foreach (Transform child in inventoryPanel)
            //Destroy(child.gameObject);

        //foreach (string item in items)
        {
            //var slot = Instantiate(inventorySlotPrefab, inventoryPanel);
            //slot.GetComponentInChildren<Text>().text = item;
        }
    }

    public void RemoveItem(string itemName)
    {
        if (items.Remove(itemName))
            UpdateUI();
    }
}
