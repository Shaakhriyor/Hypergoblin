using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToAdd : MonoBehaviour
{
    public string LevelName;


    public void OpenAdd()
    {
        SceneManager.LoadScene(LevelName);
    }
}
