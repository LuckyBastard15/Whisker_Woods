using UnityEngine;

public class LOCK : MonoBehaviour
{
    [SerializeField] private GameObject candado;
    [SerializeField] private GameObject playerObject; // ← arrastra el jugador aquí

    private PlayerMovement playerMovementScript;

    private void Start()
    {
        if (playerObject != null)
        {
            playerMovementScript = playerObject.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2D")) // Asegúrate que el jugador tenga el tag "Player"
        {
            candado.SetActive(true);

            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
                Debug.Log("Movimiento del jugador desactivado al activar el candado");
            }
        }
    }
}
