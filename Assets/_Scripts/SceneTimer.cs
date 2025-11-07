using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneTimer : MonoBehaviour
{
    public string sceneToLoad = "Your next scene name";
    public float delaySeconds = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());

        
    }
    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);

        SceneManager.LoadScene(sceneToLoad);
    }
}
