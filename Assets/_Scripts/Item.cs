using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public int ID;
    public int quantity = 1;
    public bool stackable = true;
    public TMP_Text quantityText;

    public void AddToStack(int amount = 1)
    {
        quantity += amount;
        UpdateQuantityDisplay();
    }
    public void RemoveFromStack(int amount = 1)
    {
        quantity = Mathf.Max(0, quantity - amount);
        UpdateQuantityDisplay();
    }

    public void UpdateQuantityDisplay()
    {
        if (quantityText != null)
        {
            if (stackable && quantity > 1)
            {
                quantityText.text = quantity.ToString();
                quantityText.gameObject.SetActive(true);
            }
            else
            {
                quantityText.text = "";
                quantityText.gameObject.SetActive(false);
            }
        }
    }
}
