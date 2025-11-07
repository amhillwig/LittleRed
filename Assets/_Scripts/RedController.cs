using UnityEngine;

public class RedController : MonoBehaviour
{
    public float speed = 0.5f;
    private bool isMoving = false;
    Rigidbody2D rb;
    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (NPCController.IsDialogueActive)
        {
            // Dialogue is active → player cannot move
            animator.SetBool("isMoving", false);
            return;
        }*/
        Vector2 move = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            move.x += 1;

            animator.SetFloat("moveX", 1);
            animator.SetFloat("moveY", 0);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            move.x -= 1;

            animator.SetFloat("moveX", -1);
            animator.SetFloat("moveY", 0);

        }
        if (Input.GetKey(KeyCode.W))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            move.y += 1;

            animator.SetFloat("moveY", 1);
            animator.SetFloat("moveX", 0);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Create a new vector where we modify the x position
            // of our game object
            isMoving = true;
            move.y -= 1;

            animator.SetFloat("moveY", -1);
            animator.SetFloat("moveX", -1);

        } else
        {
            isMoving = false;
            //animator.SetFloat("moveX", 0);
            //animator.SetFloat("moveY", 0);
        }
        move = move.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move); // respects collisions
        animator.SetBool("isMoving", isMoving);
    }
}