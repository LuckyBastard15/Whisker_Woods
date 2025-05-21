using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private string lastDirection = "Down"; // Para saber qué Idle usar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (GetComponent<BoxCollider2D>() == null)
            gameObject.AddComponent<BoxCollider2D>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        movement = Vector2.zero;

        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);

        if (up)
        {
            movement.y = 1;
            SetWalkDirection("Up");
        }
        else if (down)
        {
            movement.y = -1;
            SetWalkDirection("Down");
        }

        if (right)
        {
            movement.x = 1;
            SetWalkDirection("Right");
        }
        else if (left)
        {
            movement.x = -1;
            SetWalkDirection("Left");
        }

        if (!up && !down && !left && !right)
        {
            SetIdleDirection(lastDirection);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void SetWalkDirection(string direction)
    {
        ResetAllBools();
        animator.SetBool("Walk" + direction, true);
        lastDirection = direction;
    }

    void SetIdleDirection(string direction)
    {
        ResetAllBools();
        animator.SetBool("Idle" + direction, true);
    }

    void ResetAllBools()
    {
        string[] directions = { "Up", "Down", "Left", "Right" };
        foreach (string dir in directions)
        {
            animator.SetBool("Walk" + dir, false);
            animator.SetBool("Idle" + dir, false);
        }
    }
}
