using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // Asegurarse de que el Rigidbody2D y el BoxCollider2D existen
        rb = GetComponent<Rigidbody2D>();

        if (GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        // Opcional: ajustar configuración del Rigidbody2D
        rb.gravityScale = 0f; // Para evitar que caiga si estás haciendo un juego 2D tipo top-down
        rb.freezeRotation = true;
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            movement.y = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            movement.y = -1;

        if (Input.GetKey(KeyCode.RightArrow))
            movement.x = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            movement.x = -1;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
