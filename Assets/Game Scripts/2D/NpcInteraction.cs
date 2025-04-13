using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject lockImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        text1.SetActive(true);
        lockImage.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text1.SetActive(false);
    }

}
