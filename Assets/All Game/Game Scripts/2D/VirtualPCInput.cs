using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualPCInput : MonoBehaviour
{
    public Camera mainCamera;       // Cámara del mundo 3D
    public Camera uiCamera;         // Cámara que renderiza la UI (RenderTexture)
    public RenderTexture renderTexture;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Lanza un rayo desde la cámara principal hacia la escena
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Asegura que se hizo clic sobre el objeto que muestra el RenderTexture
                if (hit.collider.CompareTag("PCScreen"))
                {
                    // Coordenadas UV del punto de impacto
                    Vector2 uv = hit.textureCoord;

                    // Coordenadas en píxeles dentro del RenderTexture
                    Vector2 pixelCoord = new Vector2(uv.x * renderTexture.width, uv.y * renderTexture.height);

                    // Convertir a coordenadas de pantalla en la uiCamera
                    Vector3 uiWorldPoint = uiCamera.ScreenToWorldPoint(new Vector3(pixelCoord.x, pixelCoord.y, uiCamera.nearClipPlane));
                    Vector2 screenPos = uiCamera.WorldToScreenPoint(uiWorldPoint);

                    // Crear el evento de clic para el sistema UI
                    PointerEventData eventData = new PointerEventData(EventSystem.current)
                    {
                        position = screenPos
                    };

                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(eventData, results);

                    foreach (var result in results)
                    {
                        // Simula el clic sobre el primer objeto UI encontrado
                        ExecuteEvents.Execute(result.gameObject, eventData, ExecuteEvents.pointerClickHandler);
                        break;
                    }
                }
            }
        }
    }
}
