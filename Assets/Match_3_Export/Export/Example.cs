using UnityEngine;

public class Example : MonoBehaviour

{
    public int score = 0;
    public int neededpointstowin = 5;
    bool PlayerHasEnoughGoblins = false;


    private void Update()
    {
        score = score + 1;
        if (score == neededpointstowin)
        {
            BOss();
        }    
    }

    // T‰st‰ alla oleva osuus olisi osana boss scripti‰
    public void BOss()
    {
        PlayerHasEnoughGoblins = true;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Player") && PlayerHasEnoughGoblins))
        {
            //pelaaja voittaa
        }
        else
        {
            //h‰vi‰‰
        }
    }

} 
