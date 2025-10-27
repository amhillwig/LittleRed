using UnityEngine;

public class RedController : MonoBehaviour
{
    public float speed = 0.5f;
    private bool isMoving = false;

    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
    {
        // Dialogue is active → player cannot move
        animator.SetBool("isMoving", false);
        return;
    }
        if (Input.GetKey(KeyCode.D))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            Vector2 pos = new Vector2(
                gameObject.transform.position.x + speed * Time.deltaTime,
                gameObject.transform.position.y);

            // Assign new position vector to game object
            gameObject.transform.position = pos;

            animator.SetFloat("moveX", 1);
            animator.SetFloat("moveY", 0);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            Vector2 pos = new Vector2(
                gameObject.transform.position.x - speed * Time.deltaTime,
                gameObject.transform.position.y);

            // Assign new position vector to game object
            gameObject.transform.position = pos;

            animator.SetFloat("moveX", -1);
            animator.SetFloat("moveY", 0);

        }
        else if (Input.GetKey(KeyCode.W))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            Vector2 pos = new Vector2(
                gameObject.transform.position.x,
                gameObject.transform.position.y + speed * Time.deltaTime);

            // Assign new position vector to game object
            gameObject.transform.position = pos;

            animator.SetFloat("moveY", 1);
            animator.SetFloat("moveX", 0);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            Vector2 pos = new Vector2(
                gameObject.transform.position.x,
                gameObject.transform.position.y - speed * Time.deltaTime);

            // Assign new position vector to game object
            gameObject.transform.position = pos;

            animator.SetFloat("moveY", -1);
            animator.SetFloat("moveX", -1);

        } else
        {
            isMoving = false;
            //animator.SetFloat("moveX", 0);
            //animator.SetFloat("moveY", 0);
        }
        
        animator.SetBool("isMoving", isMoving);
    }
}