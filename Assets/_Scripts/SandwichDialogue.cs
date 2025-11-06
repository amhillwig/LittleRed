using UnityEngine;
using UnityEngine.SceneManagement;

public class SandwichDialogue : MonoBehaviour
{
    public GameObject parentObject;

    private GameObject childObject;

    void Start()
    {
        // Path to the nested child (Parent/Child/Grandchild)
        /*Transform childTransform = parentObject.transform.Find("GameManager/UI/DialogueBox");

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
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            QuestionDialogueUI.Instance.ShowQuestion("Are you sure you want to give the wolf grandma's sandwich?", () => {
                SceneManager.LoadScene("Forest");
            }, () => {
                SceneManager.LoadScene("Death");
                });
        }
    }
}
