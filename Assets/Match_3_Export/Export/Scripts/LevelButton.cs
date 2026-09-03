
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string LevelName;

    public void OpenLevel()
    {
        SceneManager.LoadScene(LevelName);
    }


}
