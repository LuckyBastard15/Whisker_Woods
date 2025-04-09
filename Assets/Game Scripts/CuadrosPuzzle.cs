using UnityEngine;

public class CuadrosPuzzle : MonoBehaviour
{
    public Transform posicionCorrecta;
    public float distanciaParaColocar = 2f;
    public bool estaAgarrado = false;
    public bool yaColocado = false;

    private Vector3 escalaOriginal;  // 👈 Para guardar la escala original

    void Start()
    {
        // Guardar la escala original al inicio
        escalaOriginal = transform.localScale;
    }

    public void IntentarColocar()
    {
        float distancia = Vector3.Distance(transform.position, posicionCorrecta.position);

        if (distancia <= distanciaParaColocar)
        {
            // Soltar del padre
            transform.SetParent(null);

            // Forzar la posición y rotación exacta
            transform.position = posicionCorrecta.position;
            transform.rotation = posicionCorrecta.rotation;

            // Restaurar la escala original
            transform.localScale = escalaOriginal;

            estaAgarrado = false;
            yaColocado = true; // Marcar como colocado
        }
        else
        {
            Debug.Log("No estás cerca de la posición correcta");
        }
    }
}
