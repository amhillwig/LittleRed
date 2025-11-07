using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathQuestion : MonoBehaviour
{
    

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            QuestionDialogueUI.Instance.ShowQuestion("Do you want to restart the game", () => {
                SceneManager.LoadScene("Village");
            }, () => {
                SceneManager.LoadScene("Menu");
                });
        }
    }
}
