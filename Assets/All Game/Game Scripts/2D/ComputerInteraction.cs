using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Audio;

public class ComputerInteraction : MonoBehaviour
{
    public Transform computerScreenPosition;
    public GameObject computerUI;
    //public KeyCode interactKey = KeyCode.E;
    //public KeyCode exitKey = KeyCode.Q;  // Tecla para salir
    public Collider computerCollider;

    private Transform player;
    private PlayerController playerControllerScript;
    private bool isUsingComputer = false;
    private Vector3 originalPosition;
    private Vector3 originalCameraPosition;
    private Quaternion originalRotation;

    public float transitionDuration = 1f;

    private int interactionCount = 0;

    private bool hasReachedLimit = false;
    public GameObject Luces;
    [SerializeField] private RandomLights randomLights;
    [SerializeField] private GameObject Fog;
    [SerializeField] private GameObject player3D;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private PlayerDies PlayerDies;

    [SerializeField] private VirtualPCInput VirtualPCInput;

    [SerializeField] private Transform cameraPos;
    [SerializeField] private Transform cameraPlayer;

    //sounds
    public AudioSource audioSourceSong;
    [SerializeField] private AudioSource song2DLoop;

    public AudioSource audioSourceWhisper;
    [SerializeField] private AudioSource wisperExit;

    public AudioSource audioSourceDoor;
    [SerializeField] private AudioSource door;

    // delay entre interacciones
    private bool canInteract = true;
    public float interactionDelay = 5f;  

    private void Start()
    {
        

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
        if (isUsingComputer == true)
        {
            player3D.GetComponent<PlayerController>().canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (player == null || computerCollider == null) return;

        bool isPlayerInside = computerCollider.bounds.Contains(player.position);

        // Iniciar la computadora solo si puede interactuar
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInside && !isUsingComputer && canInteract)
        {
            StartCoroutine(UseComputerWithDelay());
        }

        // Salir de la computadora solo si puede interactuar
        if (isUsingComputer && Input.GetKeyDown(KeyCode.Q) && canInteract)
        {
            StartCoroutine(ExitComputerWithDelay());
        }
    }

    private IEnumerator UseComputerWithDelay()
    {
        canInteract = false;  // Desactivar interacción mientras está el delay
        UseComputer();
        yield return new WaitForSeconds(interactionDelay);  // Espera el delay antes de permitir más interacciones
        canInteract = true;  // Rehabilitar interacción
    }

    private void UseComputer()
    {
        //VirtualPCInput.UsingPcNow();
        if (audioSourceSong && song2DLoop)
        {
            audioSourceSong.clip = song2DLoop.clip;
            audioSourceSong.loop = true;
            audioSourceSong.Play();
        }

        if (hasReachedLimit) return;

        if (playerControllerScript != null)
            playerControllerScript.canMove = false;

        originalPosition = player.position;
        originalRotation = player.rotation;
        originalCameraPosition = cameraPlayer.position;

        cameraPlayer.DOMove(cameraPos.position, transitionDuration).SetEase(Ease.InOutQuad);
        cameraPlayer.DORotateQuaternion(cameraPos.rotation, transitionDuration).SetEase(Ease.InOutQuad);

        // Mover y rotar al player suavemente
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

    private IEnumerator ExitComputerWithDelay()
    {
        canInteract = false;  // Desactivar interacción mientras está el delay
        ExitComputer();
        yield return new WaitForSeconds(interactionDelay);  // Espera el delay antes de permitir más interacciones
        canInteract = true;  // Rehabilitar interacción
    }

    private void ExitComputer()

    {
        audioSourceSong.Stop();
        audioSourceSong.loop = false;

        if (playerControllerScript != null)
            playerControllerScript.canMove = true;

        player.DOMove(originalPosition, transitionDuration).SetEase(Ease.InOutQuad);
        player.DORotateQuaternion(originalRotation, transitionDuration).SetEase(Ease.InOutQuad);

        cameraPlayer.DOMove(originalCameraPosition, transitionDuration).SetEase(Ease.InOutBack);


        if (computerUI != null)
            computerUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isUsingComputer = false;

        audioSourceWhisper.Play();
    }

    void TriggerInteractionEvent(int count)
    {
        switch (count)
        {
            case 3:
                Debug.Log("Aparece un sonido extraño.");
                audioSourceDoor.Play();
                break;

            case 5:
                Debug.Log(" luz parpadea.");
                randomLights.enabled = true;

                break;

            case 8:
                Debug.Log(" El entorno cambia ligeramente.");
                //KillPlayer();
                Fog.SetActive(true);

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

            default:
                Debug.Log("Interacción sin evento especial.");
                break;
        }
    }

    void KillPlayer()
    {
        ExitComputer();
        PlayerDies.PlayerDiesActive();
        Debug.Log("Game Over. Puedes poner aquí una animación o cargar otra escena.");
        //SceneManager.LoadScene("GameOverScene");
    }
}
 