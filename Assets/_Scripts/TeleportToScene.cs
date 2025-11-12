using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public string SceneToLoad = "Enter the scene to load";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) SceneManager.LoadScene(SceneToLoad);
    }
}
