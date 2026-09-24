using UnityEngine;

public class WINNER : MonoBehaviour
{
    public GameObject winMenu;

    void Start()
    {
        winMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
