using System.Collections.Generic;
using UnityEngine;

public class CuadroInteractivo : MonoBehaviour
{
    public Transform mano;  
    public float distancia = 3f;
    private CuadrosPuzzle cuadroActual = null;
    public GameObject Arm;
    public List<CuadrosPuzzle> cuadrosPuzzle;  
    private int cuadrosColocados = 0;  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cuadroActual == null)
            {
                
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distancia))
                {
                    
                    CuadrosPuzzle cuadro = hit.collider.GetComponent<CuadrosPuzzle>();
                    if (cuadro != null && !cuadro.estaAgarrado && !cuadro.yaColocado)  
                    {
                        Arm.SetActive(true);
                        cuadro.estaAgarrado = true;
                        cuadro.transform.SetParent(mano);
                        cuadro.transform.localPosition = Vector3.zero;
                        cuadro.transform.localRotation = Quaternion.identity;
                        cuadroActual = cuadro;
                    }
                }
            }
            else
            {
                
                cuadroActual.IntentarColocar();

                
                if (!cuadroActual.estaAgarrado)
                {
                    cuadroActual = null;
                    Arm.SetActive(false);
                    VerificarCuadrosColocados();  
                }
            }
        }
    }

    
    private void VerificarCuadrosColocados()
    {
        
        cuadrosColocados = 0;

        foreach (CuadrosPuzzle cuadro in cuadrosPuzzle)
        {
            if (cuadro.yaColocado)
            {
                cuadrosColocados++;
            }
        }

        
        if (cuadrosColocados == cuadrosPuzzle.Count)
        {
            ActivarEvento();  
        }
    }

    
    private void ActivarEvento()
    {
        Debug.Log("¡Todos los cuadros están en su lugar! Evento activado.");
        
    }
}
