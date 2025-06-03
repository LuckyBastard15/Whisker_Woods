using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLights : MonoBehaviour
{
    public List<Light> luces;
    public float flickerSpeed = 0.2f; 

    private Coroutine flickerCoroutine;

    void Start()
    {
        flickerCoroutine = StartCoroutine(FlickerLights());
    }

    IEnumerator FlickerLights()
    {
        while (true)
        {
            foreach (Light luz in luces)
            {
                luz.enabled = !luz.enabled; 
                yield return new WaitForSeconds(Random.Range(0.05f, flickerSpeed)); 
            }
        }
    }

    
    public void PauseFlicker()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }

        foreach (Light luz in luces)
        {
            luz.enabled = false; // Apaga la luz
        }
    }
}
