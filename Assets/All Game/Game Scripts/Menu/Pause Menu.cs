using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class PauseMenu : MonoBehaviour

{
    [SerializeField] private GameObject canvasPause;
    [SerializeField] private GameObject canvasTutorialMove;
    [SerializeField] private GameObject canvasTutotrialPc;
    [SerializeField] private bool isPause = false;
    [SerializeField] private GameObject player3D;
    [SerializeField] private PlayerController playerControllerScript;
    [SerializeField] private GameObject settingsPanel;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player3D.GetComponent<PlayerController>().canMove = true;
        playerControllerScript = player3D.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                ResumeActive(); 
            }
            else
            {
                PauseMenuActive();
            }
        }
    }


    public void PauseMenuActive() 
    {
        
        canvasPause.SetActive(true);
        canvasTutorialMove.SetActive(false);
        canvasTutotrialPc.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        player3D.GetComponent<PlayerController>().canMove = false;
        isPause = true;

    }

    public void ResumeActive()
    {
        if (!settingsPanel.activeSelf)

        {
            Time.timeScale = 1;
            canvasPause.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player3D.GetComponent<PlayerController>().canMove = true;
            isPause = false;
        }
        else
        {
            settingsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsActive()
    {
        settingsPanel.SetActive(true);
    }
}
