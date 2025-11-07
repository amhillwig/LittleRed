using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionDialogueUI : MonoBehaviour
{
    public static QuestionDialogueUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void ShowQuestion(string questionText, System.Action yesAction, System.Action noAction)
    {
        gameObject.SetActive(true);
        textMeshPro.text = questionText;

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() =>
        {
            Hide();
            yesAction?.Invoke();
        });

        noButton.onClick.AddListener(() =>
        {
            Hide();
            noAction?.Invoke();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
