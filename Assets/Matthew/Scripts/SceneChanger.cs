using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string Level;
    public void ChangeScene()
    {
        SceneManager.LoadScene(Level);
    }
}
