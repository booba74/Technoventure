using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    bool isPause = false;
    GameObject pauseUI;
    void Awake()
    {
        pauseUI = GameObject.FindWithTag("pauseUI");
       
    }
    private void Start()
    {
        pauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!isPause);
        }
       
    }

    public void PauseGame(bool paused)
    {
        pauseUI.SetActive(paused);
        isPause = paused;
        Time.timeScale = paused ? 0 : 1;
        LockCursor(!paused);
    }

    public void BackToMenu()
    {
        LockCursor(false);
        SceneManager.LoadScene(0);
    }

    void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

}
