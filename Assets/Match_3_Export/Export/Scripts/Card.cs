using UnityEngine;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour
{
    public GameObject CardFront;
    public GameObject CardBack;
    private bool IsFlipped = false;


    private void Start()
    {
       CardFront.SetActive(true);
       CardBack.SetActive(false);
    }
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Haetaan osunut objekti muuttujaan selkeyden vuoksi
                GameObject hitObject = hit.collider.gameObject;
                if (IsFlipped == false)
                {
                    CardFront.SetActive(false);
                    CardBack.SetActive(true);
                    IsFlipped = true;

                }

            }
        }
    }
    public void ShowBack()
    {
        IsFlipped = false;
        CardBack.SetActive(false);
        CardFront.SetActive(true);

    }
}
