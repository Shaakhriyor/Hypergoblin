using UnityEngine;
using UnityEngine.SceneManagement;

public class Example : MonoBehaviour

{
    bool PlayerHasEnoughGoblins = false;
    public TeleportToAdd Teleport;
    public string TeleportName;
    public void BOss()
    {
        PlayerHasEnoughGoblins = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (PlayerHasEnoughGoblins)
            {
                SceneManager.LoadScene(TeleportName);
            }
            else
            {
                Teleport.OpenAdd();
            }
        }
    }
}
