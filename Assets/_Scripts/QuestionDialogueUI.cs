using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionDialogueUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Button yesButton;
    private Button noButton;
    private void Awake()
    {
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesButton = transform.Find("YesBtn").GetComponent<Button>();
        noButton = transform.Find("NoBtn").GetComponent<Button>();

        ShowQuestion("Do you?", () =>
        {
            Debug.Log("Yes");
        }, () =>
        {
            Debug.Log("No");
        });
        

    }

    public void ShowQuestion(string questionText, System.Action yesAction, System.Action noAction)
    {
        textMeshPro.text = questionText;
        yesButton.onClick.AddListener(() =>
        {
            Hide();
            yesAction();
        });
        noButton.onClick.AddListener(() =>
        {
            Hide();
            noAction();
        });


    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
