using UnityEngine;
using DG.Tweening; 

public class ComputerInteraction : MonoBehaviour
{
    public Transform computerScreenPosition;
    public GameObject computerUI;
    //public GameObject interactionCanvas;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode exitKey = KeyCode.Escape;

    private Transform player;
    private PlayerController playerControllerScript;
    private bool isUsingComputer = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public float transitionDuration = 1f; // Duración de la transición (en segundos)

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
            playerControllerScript = player.GetComponent<PlayerController>();

        if (computerUI != null)
            computerUI.SetActive(false);

        //if (interactionCanvas != null)
          //  interactionCanvas.SetActive(false);

        if (player == null)
            Debug.LogError("No se encontró el jugador con la etiqueta 'Player'.");
    }

    private void OnTriggerStay(Collider other)
    {
        // Solo activa el canvas si el jugador entra en el área de interacción
        if (other.CompareTag("Player") && isUsingComputer == false)
        {
            //if (interactionCanvas != null && !interactionCanvas.activeSelf)
              //  interactionCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Desactiva el canvas cuando el jugador sale del área de interacción
        if (other.CompareTag("Player"))
        {
            //if (interactionCanvas != null && interactionCanvas.activeSelf)
              //  interactionCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Si estamos cerca de la PC y presionamos la tecla para interactuar
        if ( Input.GetKeyDown(interactKey))
        {
            UseComputer();
        }

        // Si estamos usando la computadora y presionamos Escape, salimos
        if (isUsingComputer && Input.GetKeyDown(exitKey))
        {
            ExitComputer();
        }
    }

    void UseComputer()
    {
        if (playerControllerScript != null)
            playerControllerScript.canMove = false; // ❌ Bloquea el movimiento del jugador

        originalPosition = player.position;
        originalRotation = player.rotation;

        // Transición suave usando DOTween
        player.DOMove(computerScreenPosition.position, transitionDuration).SetEase(Ease.InOutQuad);
        player.DORotateQuaternion(computerScreenPosition.rotation, transitionDuration).SetEase(Ease.InOutQuad);

        if (computerUI != null)
            computerUI.SetActive(true);

        //if (interactionCanvas != null)
          //  interactionCanvas.SetActive(false);

        // 🔴 Habilita el mouse para interactuar con la UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isUsingComputer = true;
    }

    void ExitComputer()
    {
        if (playerControllerScript != null)
            playerControllerScript.canMove = true; // ✅ Habilita el movimiento del jugador

        // Transición suave para volver a la posición original
        player.DOMove(originalPosition, transitionDuration).SetEase(Ease.InOutQuad);
        player.DORotateQuaternion(originalRotation, transitionDuration).SetEase(Ease.InOutQuad);

        if (computerUI != null)
            computerUI.SetActive(false);

        // 🔵 Bloquea el mouse para volver a controlar la cámara
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isUsingComputer = false;
    }
}
