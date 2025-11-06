using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToCutScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject teleportObject = GameObject.Find("Teleport");
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("WolfCutScene");
            teleportObject.SetActive(false);
        }
    }
}
