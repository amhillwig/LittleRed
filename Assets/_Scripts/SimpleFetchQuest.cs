using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SimpleFetchQuest : MonoBehaviour
{
    public string questName = "";
    public int requiredAmount = 3;
    private int currentAmount = 0;
    private bool isActive = false, isCompleted = false;
    public GameObject player;
    public Image itemIcon, rewardIcon;
    public TMP_Text amount;

    public static SimpleFetchQuest Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
        if (itemIcon != null) itemIcon.enabled = false;
    }

    public void StartQuest()
    {
        if (isActive || isCompleted) return;

        isActive = true;
        currentAmount = 0;
    }

    void Update()
    {
        if (isActive && !isCompleted && currentAmount >= requiredAmount)
        {
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
        isCompleted = true;
        isActive = false;
        amount.enabled = false;

    }

    public string questItemTag = "QuestItem";
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive || isCompleted) return;

        if (other.CompareTag(questItemTag))
        {
            Destroy(other.gameObject);
            currentAmount++;

            if (!itemIcon.enabled) itemIcon.enabled = true;
            else
            {
                amount.enabled = true;
                amount.text = currentAmount + "";
            }

            Debug.Log($"Collected {currentAmount}/{requiredAmount}"); //
        }
    }

    public bool IsQuestCompleted() => isCompleted;
    public bool IsQuestActive() => isActive;
}
