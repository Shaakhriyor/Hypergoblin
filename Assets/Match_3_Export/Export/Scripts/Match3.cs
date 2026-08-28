using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Match3 : MonoBehaviour
{
    public ScoreO ScoreManageri;


    private List<GameObject> clickedObjects = new List<GameObject>();

    void Update()
    {

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        // Haetaan hiiren sijainti ruudulla
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (!clickedObjects.Contains(hitObject))
            {
                clickedObjects.Add(hitObject);
                if (clickedObjects.Count == 3)
                {
                    CheckMatch3();
                }
            }
        }
    }

    void CheckMatch3()
    {
        string firstTag = clickedObjects[0].tag;


        if (clickedObjects[1].CompareTag(firstTag) && clickedObjects[2].CompareTag(firstTag))
        {
            foreach (GameObject obj in clickedObjects)
            {
                Destroy(obj);

                ScoreManageri.AddScore();


            }
        }
        else
        {

        }


        clickedObjects.Clear();
    }
}