using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Manager : MonoBehaviour
{
    public bool puzzle1Completed = false;
    public bool puzzle2Completed = false;
    public bool puzzle3Completed = false;
    public bool puzzle4Completed = false;

    [SerializeField] private PlayerDies playerDies;
    [SerializeField] private GameObject Item1;
    [SerializeField] private GameObject Item2;
    [SerializeField] private GameObject Item3;
    //[SerializeField] private GameObject Item4;

    public AudioSource audioSource;
    
    [SerializeField] private AudioSource getItem;

    void Update()
    {
        if (puzzle1Completed && puzzle2Completed && puzzle3Completed && puzzle4Completed)
        {
            AllPuzzlesCompleted();
        }

        if (puzzle1Completed)
        {
            Item1.SetActive(true);
        }
        if (puzzle2Completed)
        {
            Item2.SetActive(true);
        }
        if (puzzle3Completed)
        {
            Item3.SetActive(true);
        }


    }

    public void AllPuzzlesCompleted()
    {
        playerDies.PlayerDiesActive();
        Debug.Log("¡Todos los puzzles han sido completados!");
        this.gameObject.SetActive(false);
    }
    
    public void Puzzle1Completed()
    {
        puzzle1Completed = true;
        Debug.Log("Puzzle 1 completado");
        Item1.SetActive(true);
        audioSource.Play();
    }

    public void Puzzle2Completed()
    {
        puzzle2Completed = true;
        Debug.Log("Puzzle 2 completado");
        Item2.SetActive(true);
        audioSource.Play();

    }

    public void Puzzle3Completed()
    {
        puzzle3Completed = true;

        Debug.Log("Puzzle 3 completado");
        Item3.SetActive(true);
        audioSource.Play();

    }

    public void Puzzle4Completed()
    {
        puzzle3Completed = true;
        Debug.Log("Puzzle 3 completado");
        
        audioSource.Play();

    }
}

