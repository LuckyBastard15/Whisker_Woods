using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f; // �ngulo al que se abrir� la puerta
    public float closeAngle = 0f; // �ngulo cerrado de la puerta
    public float openSpeed = 2f; // Velocidad de apertura/cierre
    private bool isOpen = false; // Estado de la puerta
    private bool playerInRange = false; // Si el jugador est� cerca

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }

        // Rotaci�n suave de la puerta
        float targetAngle = isOpen ? openAngle : closeAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen; // Cambia el estado de la puerta
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
