using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathGame : MonoBehaviour
{
    

    void Start()
    {
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            QuestionDialogueUI.Instance.ShowQuestion("Which number is it?", () => {
                SceneManager.LoadScene("Death");
            }, () => {
                SceneManager.LoadScene("Death");
                });
        }
    }
}
