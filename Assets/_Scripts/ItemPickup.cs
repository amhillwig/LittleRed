// ItemPickup.cs - Complete and Corrected
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // FIX: Removed private InventoryManager field as it should use the singleton

    private void Start() {
        // FIX: No need to FindObjectOfType if using singleton pattern
    }

    /* FIX: Removed the incorrect Interact() method
    public void Interact()
    {
        InventoryManager.Instance.AddItem(itemName); // 'itemName' is undefined
        Destroy(gameObject);
    }
    */
    
    private void OnTriggerEnter2D(Collider2D collision) {
        // FIX: Changed tag check from "Item" to "Player"
        if (collision.CompareTag("Player"))
        {
            // Get the Item component on *this* ItemPickup object
            Item item = GetComponent<Item>(); 
            if (item != null)
            {
                // We pass *this* instance's GameObject, assuming InventoryManager.AddItem is smart
                // enough to handle an instance or you intend to pass the object with Item component.
                bool itemAdded = InventoryManager.Instance.AddItem(gameObject); 
                
                if (itemAdded) 
                {
                    // Destroy the item in the world after it's been added to inventory
                    Destroy(gameObject); 
                }
            }
        }
    }
}