using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject truePauseMenu;
    public GameObject settingsMenu;
    public bool IsPaused;


    void Start()
    {
        Time.timeScale = 1f;
        ResetMenu();
    }

    public void PauseUnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started == false)
            return;

        if (IsPaused == false)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void ResetMenu()
    {
        settingsMenu.SetActive(false);
        truePauseMenu.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        IsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        ResetMenu();
        IsPaused = false;
    }

    public void Quits()
    {
        ResetMenu();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
