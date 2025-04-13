using UnityEngine;

public class SymbolLockManager : MonoBehaviour
{
    public SymbolLock[] symbolLocks; // Arrastra aquí los 3 símbolos
    public int[] correctCombination = { 2, 0, 1 }; // Define tu combinación correcta
    [SerializeField] private GameObject candado;
    [SerializeField] private GameObject candadoImage;
    [SerializeField] private GameObject playerObject; // ← arrastra el jugador aquí

    private PlayerMovement playerMovementScript;
    public GameObject npcObject;


    private void Start()
    {

        if (playerObject != null)
        {
            playerMovementScript = playerObject.GetComponent<PlayerMovement>();
        }
    }


    public void CheckCombination()
    {
        for (int i = 0; i < symbolLocks.Length; i++)
        {
            if (symbolLocks[i].GetCurrentIndex() != correctCombination[i])
            {
                return; // Al menos uno está mal
            }
        }

        Debug.Log("¡Candado desbloqueado!");
        // Aquí puedes llamar a una función para abrir puerta, mostrar animación, etc.\\

        candado.SetActive(false);
        candadoImage.SetActive(false);
        NpcInteraction npcScript = npcObject.GetComponent<NpcInteraction>();
        
        Collider2D col2d = npcObject.GetComponent<Collider2D>();
        col2d.enabled = false;

        if (npcScript != null)
        {
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true;
                Debug.Log("Movimiento del jugador desactivado al activar el candado");
            }
            npcScript.enabled = false;
            Debug.Log("NpcInteraction desactivado");
            Debug.Log("Dar Item");

        }
    }
}


