using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool puzzle1Completed = false;
    public bool puzzle2Completed = false;
    public bool puzzle3Completed = false;
    public bool puzzle4Completed = false;

    [SerializeField] private PlayerDies playerDies;

    void Update()
    {
        if (puzzle1Completed && puzzle2Completed && puzzle3Completed && puzzle4Completed)
        {
            AllPuzzlesCompleted();
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
    }

    public void Puzzle2Completed()
    {
        puzzle2Completed = true;
        Debug.Log("Puzzle 2 completado");
    }

    public void Puzzle3Completed()
    {
        puzzle3Completed = true;
        Debug.Log("Puzzle 3 completado");
    }

    public void Puzzle4Completed()
    {
        puzzle3Completed = true;
        Debug.Log("Puzzle 3 completado");
    }
}

