using UnityEngine;
using UnityEngine.UI;

public class SymbolLock : MonoBehaviour
{
    public Sprite[] symbolSprites; // Lista de símbolos
    private int currentSymbolIndex = 0;

    private Image symbolImage;
    private SymbolLockManager lockManagerReference;

    [System.Obsolete]
    void Start()
    {
        symbolImage = GetComponent<Image>();
        lockManagerReference = FindObjectOfType<SymbolLockManager>();

        UpdateSymbol();
    }

    public void OnClick()
    {
        currentSymbolIndex = (currentSymbolIndex + 1) % symbolSprites.Length;
        UpdateSymbol();
        lockManagerReference.CheckCombination();
    }

    void UpdateSymbol()
    {
        if (symbolImage != null && symbolSprites.Length > 0)
        {
            symbolImage.sprite = symbolSprites[currentSymbolIndex];
        }
    }

    public int GetCurrentIndex()
    {
        return currentSymbolIndex;
    }

    public void Unlock()
    {

    }    
}
