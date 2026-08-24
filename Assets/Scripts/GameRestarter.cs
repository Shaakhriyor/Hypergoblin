using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 

public class GameRestarter : MonoBehaviour
{
    void Update()
    {
        
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            
            Time.timeScale = 1f;

            
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}