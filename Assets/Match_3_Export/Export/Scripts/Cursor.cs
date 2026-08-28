using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    public AudioSource EffectsAudio;
    public AudioClip GoblinAudio;
    public AudioClip PotionAudio;
    public AudioClip PickaxeAudio; 

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

                // Tarkistetaan tagi suoraan osuneesta objektista
                if (hitObject.CompareTag("Goblin"))
                {
                    EffectsAudio.PlayOneShot(GoblinAudio);
                }
                else if (hitObject.CompareTag("Potion"))
                {
                    EffectsAudio.PlayOneShot(PotionAudio);
                }
                else if (hitObject.CompareTag("Pickaxe"))
                {
                    EffectsAudio.PlayOneShot(PickaxeAudio);
                }
            }
        }
    }
}