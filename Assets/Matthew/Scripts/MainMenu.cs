using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    void Start()
    {
        ResetMenu();
    }

    public void ResetMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
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
