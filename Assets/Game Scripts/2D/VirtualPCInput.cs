using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualPCInput : MonoBehaviour
{
    public Camera mainCamera;      // Cámara del jugador
    public Camera uiCamera;        // Cámara que renderiza el Canvas
    public RenderTexture renderTexture;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("PCScreen"))
                {
                    // Obtener coordenadas UV en el RenderTexture
                    Vector2 uv = hit.textureCoord;
                    Vector2 pixelCoord = new Vector2(uv.x * renderTexture.width, uv.y * renderTexture.height);

                    // Crear un evento de clic
                    PointerEventData eventData = new PointerEventData(EventSystem.current)
                    {
                        position = pixelCoord
                    };

                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(eventData, results);

                    foreach (var result in results)
                    {
                        // Simula el clic
                        ExecuteEvents.Execute(result.gameObject, eventData, ExecuteEvents.pointerClickHandler);
                        break;
                    }
                }
            }
        }
    }
}
