using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionDialogueUI : MonoBehaviour
{
    public static QuestionDialogueUI Instance
    {
        get; private set;
    }
    private TextMeshProUGUI textMeshPro;
    private Button yesButton;
    private Button noButton;
    private void Awake()
    {
        Instance = this;
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesButton = transform.Find("YesBtn").GetComponent<Button>();
        noButton = transform.Find("NoBtn").GetComponent<Button>();

        Hide();
        

    }

    public void ShowQuestion(string questionText, System.Action yesAction, System.Action noAction)
    {
        gameObject.SetActive(true);
        textMeshPro.text = questionText;


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
