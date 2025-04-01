using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject canvasitem;
    public Transform dropPosition; // Posición específica donde este objeto debe ser soltado

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Inventory inventory = FindObjectOfType<Inventory>();

            if (inventory != null)
            {
                // Verifica si el item ya fue colocado antes de activar su canvas
                if (!inventory.placedItems.Contains(gameObject))
                {
                    canvasitem.SetActive(true);
                    inventory.AddItem(gameObject, canvasitem, dropPosition);
                }
            }
        }
    }


}
