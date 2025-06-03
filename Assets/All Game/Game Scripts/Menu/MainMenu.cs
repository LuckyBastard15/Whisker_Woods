#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    //[SerializeField] PlayerController playerController;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsPanel.SetActive(false);
        }
    }

    public void playGame()
    {
        //playerController.canMove = true;
        SceneManager.LoadScene("Game");
        //SceneManager.LoadSceneAsync("Game");
    }

    public void exitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SettingsActive()
    {
        settingsPanel.SetActive(true);
    }


}
