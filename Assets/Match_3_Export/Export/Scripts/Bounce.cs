using UnityEngine;

public class ObjectBouncer : MonoBehaviour
{
    [Header("Pomppimisen asetukset")]
    [Tooltip("Kuinka korkealle objekti pomppaa aloituspisteest‰‰n.")]
    public float bounceHeight = 2.0f;

    [Tooltip("Kuinka nopeasti objekti pomppaa.")]
    public float bounceSpeed = 3.0f;

    private Vector3 startPosition;

    void Start()
    {

        startPosition = transform.position;
    }

    void Update()
    {

        float newY = startPosition.y + Mathf.Abs(Mathf.Sin(Time.time * bounceSpeed)) * bounceHeight;

        // P‰ivitet‰‰n objektin sijainti
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}