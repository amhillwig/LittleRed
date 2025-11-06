using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;

    public void Interact()
    {
        InventoryManager.Instance.AddItem(itemName);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
    }
}
