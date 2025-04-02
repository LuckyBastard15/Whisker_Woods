using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLights : MonoBehaviour
{
    public List<Light> luces; // Lista de luces en la escena
    public float flickerSpeed = 0.2f; // Velocidad del parpadeo

    void Start()
    {
        StartCoroutine(FlickerLights());
    }

    IEnumerator FlickerLights()
    {
        while (true)
        {
            foreach (Light luz in luces)
            {
                luz.enabled = !luz.enabled; // Alterna el estado de la luz
                yield return new WaitForSeconds(Random.Range(0.05f, flickerSpeed)); // Retraso aleatorio para cada luz
            }
        }
    }
}