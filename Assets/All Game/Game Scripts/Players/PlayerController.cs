using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    private CharacterController characterController;
    private float verticalRotation = 0f;

    public bool canMove = true;

    public float minVerticalAngle = -30f;  
    public float maxVerticalAngle = 60f;   

    void Start()
    {
        Time.timeScale = 1f;
        canMove = true;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Awake()
    {
        canMove = true;
        Debug.Log("can move");
    }

    void Update()
    {
        if (canMove && characterController != null)
        {
            Move();
            Rotate();
        }
        else if (characterController == null)
        {
            Debug.LogWarning("CharacterController es null");
        }
    }

    void Move()
    {
        float moveX = 0f;
        float moveZ = 0f;

       
        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * speed * Time.deltaTime);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        // Limitar la rotación vertical entre los valores mínimos y máximos
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
