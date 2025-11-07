using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToGrandma : MonoBehaviour
{
    
    public string SceneToLoad = "Enter the scene to load";
    GameObject playerObject = GameObject.Find("Red");
    public Vector2 newPositionInCurrentScene = new Vector2(5f, 2f);

    void Start()
    {

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {

            playerObject.transform.position = newPositionInCurrentScene;
            SceneManager.LoadScene(SceneToLoad);
            
        }
    }
}
