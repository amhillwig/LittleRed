using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathQuestion : MonoBehaviour
{
    public GameObject parentObject;

    private GameObject childObject;

    void Start()
    {
        // Path to the nested child (Parent/Child/Grandchild)
        Transform childTransform = parentObject.transform.Find("DeathImage/Background");

        if (childTransform != null)
        {
            childObject = childTransform.gameObject;
            Debug.Log("Found nested child: " + childObject.name);

            // Hide the child
            childObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Nested child not found! Check the path.");
        }
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
