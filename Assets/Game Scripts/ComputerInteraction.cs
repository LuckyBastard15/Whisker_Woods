using UnityEngine;
using DG.Tweening;

public class ComputerInteraction : MonoBehaviour
{
    public Transform computerScreenPosition;
    public GameObject computerUI;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode exitKey = KeyCode.Escape;
    public Collider computerCollider; 

    private Transform player;
    private PlayerController playerControllerScript;
    private bool isUsingComputer = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public float transitionDuration = 1f;

    private int interactionCount = 0; 

    private bool hasReachedLimit = false;
    public GameObject Luces;
    private RandomLights randomLights;
    [SerializeField] private GameObject Fog;


    private void Start()
    {
        randomLights = Luces.GetComponent<RandomLights>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
            playerControllerScript = player.GetComponent<PlayerController>();

        if (computerUI != null)
            computerUI.SetActive(false);

        if (player == null)
            Debug.LogError("No se encontró el jugador con la etiqueta 'Player'.");

        if (computerCollider == null)
            Debug.LogError("¡Falta asignar el collider de la computadora en el inspector!");
    }

    private void Update()
    {
        if (player == null || computerCollider == null) return;

        bool isPlayerInside = computerCollider.bounds.Contains(player.position);

        if (Input.GetKeyDown(interactKey) && isPlayerInside && !isUsingComputer)
        {
            UseComputer();
        }

        if (isUsingComputer && Input.GetKeyDown(KeyCode.Q))
        {
            ExitComputer();
        }
    }

    void UseComputer()
    {
        if (hasReachedLimit) return; 

        if (playerControllerScript != null)
            playerControllerScript.canMove = false;

        originalPosition = player.position;
        originalRotation = player.rotation;

        player.DOMove(computerScreenPosition.position, transitionDuration).SetEase(Ease.InOutQuad);
        player.DORotateQuaternion(computerScreenPosition.rotation, transitionDuration).SetEase(Ease.InOutQuad);

        if (computerUI != null)
            computerUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isUsingComputer = true;

        interactionCount++;
        Debug.Log("Interacción #" + interactionCount);

        TriggerInteractionEvent(interactionCount);
    }


    void ExitComputer()
    {
        if (playerControllerScript != null)
            playerControllerScript.canMove = true;

        player.DOMove(originalPosition, transitionDuration).SetEase(Ease.InOutQuad);
        player.DORotateQuaternion(originalRotation, transitionDuration).SetEase(Ease.InOutQuad);

        if (computerUI != null)
            computerUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isUsingComputer = false;
    }

    void TriggerInteractionEvent(int count)
    {
        switch (count)
        {
            case 3:

                Debug.Log("Aparece un sonido extraño.");
                randomLights.enabled = true;
                break;

            case 5:
                Debug.Log(" luz parpadea.");
                Fog.SetActive(true);
                break;

            case 8:
                Debug.Log(" El entorno cambia ligeramente.");
                
                break;

            case 10:
                Debug.Log(" La pantalla de la computadora se distorsiona.");
                
                break;

            case 13:
                Debug.Log(" Se escucha un maullido aterrador.");
                
                break;

            case 15:
                Debug.Log(" La computadora intenta apagarse sola.");
                
                break;

            case 17:

          
                Debug.Log("Evento en interacción 17: ¡El jugador muere!");
                KillPlayer();
                hasReachedLimit = true;
                break;
                //KillPlayer();
                //break;

            default:
                Debug.Log("Interacción sin evento especial.");
                break;
        }
    }

    void KillPlayer()
    {

        
        
        Debug.Log("Game Over. Puedes poner aquí una animación o cargar otra escena.");
        //SceneManager.LoadScene("GameOverScene");
    }
}
