using UnityEngine;

public class Example : MonoBehaviour

{
    bool PlayerHasEnoughGoblins = false;
    public TeleportToAdd Teleport;



    // Tästä alla oleva osuus olisi osana boss scriptiä
    public void BOss()
    {
        PlayerHasEnoughGoblins = true;
        Debug.Log("WinCondition");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (PlayerHasEnoughGoblins)
            {


                
            }
            else
            {
                Teleport.OpenAdd();
            }
        }
    }
}
