using UnityEngine;

public class MoveTutorial : MonoBehaviour
{

    [SerializeField] GameObject moveCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        moveCanvas.SetActive(false);
    }



}
