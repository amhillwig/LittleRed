using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour
{
    [System.Serializable]
    public class ItemEntry
    {
        public int ID;
        public GameObject prefab;
    }

    public List<ItemEntry> items = new();

    // Get prefab by ID
    public GameObject GetItemPrefab(int id)
    {
        foreach (var entry in items)
        {
            if (entry.ID == id)
                return entry.prefab;
        }
        return null;
    }
}