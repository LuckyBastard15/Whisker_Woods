using UnityEngine;
using UnityEngine.UI;

public class PathNode : MonoBehaviour
{
    public int nodeID;
    public PuzzleManager puzzleManager;

    [HideInInspector] public RectTransform rectTransform;

    private Image image;
    private Color originalColor;
    private bool alreadyClicked = false;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalColor = image.color;

        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (alreadyClicked || puzzleManager == null) return;

        alreadyClicked = true;
        image.color = Color.green;
        button.interactable = false;

        puzzleManager.TrySelectNode(this);
    }

    public void ResetVisual()
    {
        alreadyClicked = false;

        if (image != null)
            image.color = originalColor;

        if (button != null)
            button.interactable = true;
    }
}
