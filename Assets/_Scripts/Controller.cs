using UnityEngine;

public class CharacterController : MonoBehaviour
{
public float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //modify x position
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector2 pos = new Vector2(gameObject.transform.position.x + speed,
            gameObject.transform.position.y);
            gameObject.transform.position = pos;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 pos = new Vector2(gameObject.transform.position.x - speed,
            gameObject.transform.position.y);
            gameObject.transform.position = pos;
        } //modify y position
        if(Input.GetKeyDown(KeyCode.W)) {
            Vector2 pos = new Vector2(gameObject.transform.position.y + speed,
            gameObject.transform.position.x);
            gameObject.transform.position = pos;
        } else if(Input.GetKeyDown(KeyCode.S))
        {
            Vector2 pos = new Vector2(gameObject.transform.position.y - speed,
            gameObject.transform.position.x);
            gameObject.transform.position = pos;
        }
    }
}
