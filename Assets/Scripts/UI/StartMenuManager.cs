using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsUI;
    [SerializeField] GameObject devUI;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchSettings(bool isOpen)
    {
        settingsUI.SetActive(!isOpen);
        isOpen = !isOpen;
    }

    public void SwitchAboutDev(bool isOpen)
    {
        devUI.SetActive(!isOpen);
        isOpen = !isOpen;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
    }
}
