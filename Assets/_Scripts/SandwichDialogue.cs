using UnityEngine;
using UnityEngine.SceneManagement;

public class SandwichDialogue : MonoBehaviour
{
    

    void Start()
    {
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            QuestionDialogueUI.Instance.ShowQuestion("Do you want to offer the wolf grandma's basket?", () => {
                SceneManager.LoadScene("ForestAfterWolf");
            }, () => {
                SceneManager.LoadScene("Death");
                });
        }
    }
}
