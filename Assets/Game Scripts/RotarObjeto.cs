using UnityEngine;

public class RotarObjeto : MonoBehaviour
{
    public float velocidadRotacion = 50f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}