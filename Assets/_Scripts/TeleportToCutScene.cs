using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToCutScene : MonoBehaviour
{
    private GameObject teleportObject;

    void Start()
    {
       
        teleportObject = GameObject.Find("Teleport");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DontDestroyOnLoad(teleportObject);
        if (other.CompareTag("Player"))
        {
            if (teleportObject != null)
            {
                teleportObject.SetActive(false);
            }
            // Load the cutscene scene
            SceneManager.LoadScene("WolfCutScene");

            
            
        }
    }
}
