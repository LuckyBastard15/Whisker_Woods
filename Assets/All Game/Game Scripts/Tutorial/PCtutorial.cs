using Unity.VisualScripting;
using UnityEngine;

public class PCtutorial : MonoBehaviour
{

    [SerializeField] GameObject PcCanvas;
    public float displayDuration = 7f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowCanvasTemporarily());
        }
    }

    private System.Collections.IEnumerator ShowCanvasTemporarily()
    {
        PcCanvas.SetActive(true);           // Mostrar Canvas
        yield return new WaitForSeconds(displayDuration); // Esperar
        PcCanvas.SetActive(false);
        Destroy(PcCanvas); 
    }
}

