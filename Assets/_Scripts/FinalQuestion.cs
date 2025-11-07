using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalQuestion : MonoBehaviour
{
    

    void Start()
    {
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            QuestionDialogueUI.Instance.ShowQuestion("Give grandma the basket?", () => {
                SceneManager.LoadScene("Menu");
            }, () => {
                
                });
        }
    }
}
