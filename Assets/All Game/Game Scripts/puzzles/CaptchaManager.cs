using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptchaManager : MonoBehaviour
{
    public Button boton0;
    public Button boton1;
    public Button boton2;
    public Button boton3;
    public Button boton4;
    public Button boton5;

    public Button verificarButton;
    public GameObject captchaCanvas;

    private HashSet<Button> seleccionadas = new HashSet<Button>();
    private HashSet<Button> correctas = new HashSet<Button>();

    private Dictionary<Button, int> mapaBotones = new Dictionary<Button, int>();
    private Dictionary<int, Button> mapaIndices = new Dictionary<int, Button>();
    private Dictionary<int, Vector3> posicionesOriginales = new Dictionary<int, Vector3>();
    [SerializeField] private PlayerMovement playerMovementScript;
    public GameObject npcObject;
    public GameObject lockImage;

    [SerializeField] private Manager manager;

    void Start()
    {
        // Mapeo de botones
        List<Button> botones = new List<Button> { boton0, boton1, boton2, boton3, boton4, boton5 };

        for (int i = 0; i < botones.Count; i++)
        {
            Button boton = botones[i];
            mapaBotones[boton] = i;
            mapaIndices[i] = boton;
            posicionesOriginales[i] = boton.transform.localPosition;

            // Asignar listener dinámicamente
            boton.onClick.AddListener(() => SeleccionarImagenManual(boton));

            // Elegir botones correctos (puedes cambiar los índices según desees)
            if (i == 1 || i == 3 || i == 5)
            {
                correctas.Add(boton);
            }
        }

        verificarButton.onClick.AddListener(VerificarSeleccion);
    }

    public void SeleccionarImagenManual(Button boton)
    {
        if (seleccionadas.Contains(boton))
        {
            seleccionadas.Remove(boton);
            boton.image.color = Color.white; // Deseleccionar
        }
        else if (seleccionadas.Count < 3)
        {
            seleccionadas.Add(boton);
            boton.image.color = Color.green; // Seleccionar
        }
    }

    void VerificarSeleccion()
    {
        NpcInteraction npcScript = npcObject.GetComponent<NpcInteraction>();
        Collider2D col2d = npcObject.GetComponent<Collider2D>();

        if (seleccionadas.SetEquals(correctas))
        {
            Debug.Log("Botones correctos.");
            captchaCanvas.SetActive(false);
            lockImage.SetActive(false);
            playerMovementScript.enabled = true;
            col2d.enabled = false;
            npcScript.enabled = false;
            manager.Puzzle4Completed();
        }
        else
        {
            Debug.Log("Botones incorrectos.");
            ReiniciarCaptcha();
        }
    }

    void ReiniciarCaptcha()
    {
        // Restaurar color de todos los seleccionados
        foreach (Button boton in seleccionadas)
        {
            boton.image.color = Color.white;
        }

        seleccionadas.Clear();

        // Mezclar posiciones de los botones
        List<int> indices = new List<int>(mapaIndices.Keys);
        List<Vector3> posiciones = new List<Vector3>();
        foreach (int i in indices)
            posiciones.Add(posicionesOriginales[i]);

        // Shuffle
        for (int i = 0; i < posiciones.Count; i++)
        {
            Vector3 temp = posiciones[i];
            int randomIndex = Random.Range(i, posiciones.Count);
            posiciones[i] = posiciones[randomIndex];
            posiciones[randomIndex] = temp;
        }

        // Asignar nuevas posiciones
        for (int i = 0; i < indices.Count; i++)
        {
            mapaIndices[indices[i]].transform.localPosition = posiciones[i];
        }
    }
}
