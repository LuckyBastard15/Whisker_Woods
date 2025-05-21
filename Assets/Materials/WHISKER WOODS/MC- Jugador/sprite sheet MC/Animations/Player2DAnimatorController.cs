using UnityEngine;

public class Player2DAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Vector2 input;
    private int direction = 0; // 0: Down, 1: Left, 2: Right, 3: Up

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            input.y = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            input.y = -1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            input.x = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            input.x = 1;
        }

        bool isMoving = input != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                direction = input.x > 0 ? 2 : 1; // Right : Left
            }
            else
            {
                direction = input.y > 0 ? 3 : 0; // Up : Down
            }

            animator.SetInteger("Direction", direction);
        }
    }
}
