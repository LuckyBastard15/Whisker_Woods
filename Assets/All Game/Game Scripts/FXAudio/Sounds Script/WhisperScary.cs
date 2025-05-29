using UnityEngine;

public class WhisperScary : MonoBehaviour
{
    [SerializeField] AudioSource whisperScary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        whisperScary.Play();
    }
}
