using UnityEngine;

public class SimpleFetchQuest : MonoBehaviour
{

    public string questName = "";
    public int requiredAmount = 3;
    private int currentAmount = 0;
    private bool isActive = false, isCompleted = false;

    public GameObject[] questItems; // assign all mushrooms in Inspector
    public GameObject player;

    void Start()
    {
        foreach (GameObject item in questItems)
        {
            if (item != null && item.GetComponent<Collider2D>() == null)
                item.AddComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    public void StartQuest()
    {
        if (isActive || isCompleted) return;

        isActive = true;
        currentAmount = 0;
    }

    void Update()
    {
        if (isActive && !isCompleted)
        {
            if (currentAmount >= requiredAmount)
            {
                CompleteQuest();
            }
        }
    }

    void CompleteQuest()
    {
        isCompleted = true;
        isActive = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // only handle item collection
        for (int i = 0; i < questItems.Length; i++)
        {
            if (questItems[i] != null && other.gameObject == questItems[i] && isActive)
            {
                Destroy(questItems[i]);
                questItems[i] = null;
                currentAmount++;
                Debug.Log($"Collected {currentAmount}/{requiredAmount}");
            }
        }
    }

    public bool IsQuestCompleted()
    {
        return isCompleted;
    }

    public bool IsQuestActive()
    {
        return isActive;
    }
}
