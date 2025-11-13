using UnityEngine;
using UnityEngine.SceneManagement;


public class TeleportToScene : MonoBehaviour
{
public bool withinScene;
public GameObject player;
public Vector2 newPosition;
    public string SceneToLoad = "Enter the scene to load";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (withinScene) player.transform.position = newPosition;
        else {if (other.CompareTag("Player")) SceneManager.LoadScene(SceneToLoad);}
    }
}
