using Unity.VisualScripting;
using UnityEngine;

public class PCtutorial : MonoBehaviour
{
    [SerializeField] GameObject PcCanvas;
    [SerializeField] Collider PcCollider; 

    public float displayDuration = 7f;

    private void Start()
    {
        
        if (PcCollider == null && PcCanvas != null)
        {
            PcCollider = PcCanvas.GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowCanvasTemporarily());
        }
    }

    private System.Collections.IEnumerator ShowCanvasTemporarily()
    {
        PcCanvas.SetActive(true);           
        yield return new WaitForSeconds(displayDuration); 
        PcCanvas.SetActive(false);          

        if (PcCollider != null)
        {
            PcCollider.enabled = false;     
        }
    }
}

