using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> collectedItems = new List<GameObject>(); // Inventario de ítems
    public Dictionary<GameObject, GameObject> itemCanvasMap = new Dictionary<GameObject, GameObject>(); // Relación entre ítems y canvas
    public Dictionary<GameObject, Transform> itemDropPositions = new Dictionary<GameObject, Transform>(); // Posiciones de drop específicas
    public HashSet<GameObject> placedItems = new HashSet<GameObject>(); // Ítems ya colocados
    private DropZone currentDropZone = null;
    private bool canDrop = false;
    private List<GameObject> placedCanvases = new List<GameObject>();
    private GameObject currentItem; // Para rastrear el ítem actual en el inventario



    private void Update()
    {
        if (canDrop && Input.GetKeyDown(KeyCode.Q))
        {
            TryDropItem();
        }
    }

    public void AddItem(GameObject item, GameObject canvas, Transform dropPosition)
    {
        if (!collectedItems.Contains(item) && !placedItems.Contains(item)) // Solo recoge si no ha sido colocado antes
        {
            collectedItems.Add(item);
            itemCanvasMap[item] = canvas;
            itemDropPositions[item] = dropPosition; // Asigna la posición específica de drop

            item.SetActive(false);
            item.GetComponent<Collider>().enabled = false;

            if (!placedCanvases.Contains(canvas)) // Solo activamos el Canvas si no ha sido desactivado antes
            {
                canvas.SetActive(true);
            }

            currentItem = item; // Guarda el ítem actual
        }
    }



    public void TryDropItem()
    {
        if (collectedItems.Count == 0)
        {
            Debug.Log("No hay ítems en el inventario para soltar.");
            return;
        }

        GameObject itemToDrop = collectedItems.Find(item => currentDropZone != null && currentDropZone.IsCorrectItem(item));

        if (itemToDrop == null)
        {
            Debug.Log("No tienes un ítem válido para esta zona.");
            return;
        }

        PlaceItem(itemToDrop);
    }

    private void PlaceItem(GameObject item)
    {
        collectedItems.Remove(item);
        placedItems.Add(item); // Guardamos que este ítem ya fue colocado

        item.SetActive(true);
        item.transform.position = itemDropPositions[item].position;
        item.transform.rotation = itemDropPositions[item].rotation;
        item.GetComponent<Collider>().enabled = true;

        itemCanvasMap[item].SetActive(false); // Ocultamos su Canvas definitivamente
        placedCanvases.Add(itemCanvasMap[item]); // Guardamos su Canvas en una lista de Canvases desactivados

        Debug.Log("Ítem colocado correctamente.");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropZone"))
        {
            currentDropZone = other.GetComponent<DropZone>();
            canDrop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropZone"))
        {
            currentDropZone = null;
            canDrop = false;
        }
    }
}
