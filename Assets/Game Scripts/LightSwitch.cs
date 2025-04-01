using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light targetLight; // Luz que se encenderá o apagará
    private bool playerInRange = false; // Para saber si el jugador está cerca
    public GameObject canvaspuzzle;
    public GameObject canvasSwitch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            canvasSwitch.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            canvasSwitch.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (targetLight != null)
            {
                targetLight.enabled = !targetLight.enabled; // Alterna el estado de la luz
                canvaspuzzle.SetActive(!targetLight.enabled);
                
            }
        }
    }
}
