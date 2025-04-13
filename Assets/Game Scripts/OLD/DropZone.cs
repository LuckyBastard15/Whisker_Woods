using UnityEngine;

public class DropZone : MonoBehaviour
{
    public string requiredItemTag;

    public bool IsCorrectItem(GameObject item)
    {
        return item.CompareTag(requiredItemTag);
    }
}
