using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    [Header("Ruta correcta (IDs en orden)")]
    public List<int> correctPath;

    [Header("Línea que dibuja el camino")]
    public LineDrawer lineDrawer;

    private List<int> currentPath = new List<int>();
    [SerializeField] GameObject pathCanvas;
    [SerializeField] private GameObject candadoImage;

    [SerializeField] private PlayerMovement playerMovementScript;
    public GameObject npcObject;

    public void TrySelectNode(PathNode node)
    {
        currentPath.Add(node.nodeID);

        int index = currentPath.Count - 1;

        if (index >= correctPath.Count)
        {
            ResetPuzzle("Demasiados nodos seleccionados");
            return;
        }

        if (currentPath[index] != correctPath[index])
        {
            ResetPuzzle("Nodo incorrecto");
            return;
        }

        Debug.Log("Nodo correcto: " + node.nodeID);

        if (lineDrawer != null)
        {
            lineDrawer.AddPoint(node.rectTransform);
        }

        if (currentPath.Count == correctPath.Count)
        {
            Debug.Log("¡Puzzle resuelto!");
            pathCanvas.SetActive(false);
            candadoImage.SetActive(false);
            playerMovementScript.enabled = true;
            NpcInteraction npcScript = npcObject.GetComponent<NpcInteraction>();
            Collider2D col2d = npcObject.GetComponent<Collider2D>();
            

            if (npcScript != null)
            {
                col2d.enabled = false;
                npcScript.enabled = false;
            }
            
            Debug.Log("Dar Item");

            
        }
    }


    private void ResetPuzzle(string reason)
    {
        Debug.Log("Error: " + reason + ". Reiniciando camino.");

        currentPath.Clear();

        PathNode[] allNodes = FindObjectsOfType<PathNode>();
        foreach (PathNode node in allNodes)
        {
            node.ResetVisual();
        }

        if (lineDrawer != null)
        {
            lineDrawer.ResetLine();
        }
    }
}
