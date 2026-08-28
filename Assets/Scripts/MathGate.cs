using UnityEngine;
using TMPro;

public enum GateType { Add, Multiply, Subtract, Divide }

public class MathGate : MonoBehaviour
{
    [Header("Gate Settings")]
    public GateType type;
    public int value;

    [Header("Randomization Ranges")]
    public Vector2Int addRange = new Vector2Int(1, 10);
    public Vector2Int multiplyRange = new Vector2Int(2, 4);
    public Vector2Int subtractRange = new Vector2Int(1, 8);
    public Vector2Int divideRange = new Vector2Int(2, 3);

    [Header("References")]
    public TMP_Text displayText;
    public GameObject sisterGate;

    [Header("Colors")]
    public MeshRenderer glassRenderer;
    public Material positiveMaterial;
    public Material negativeMaterial;

    private bool isTriggered = false;

    void Start()
    {
        RandomizeGate();
        UpdateGateVisuals();
    }

    void RandomizeGate()
    {
        // Pick a random operation: Add (0), Multiply (1), Subtract (2), or Divide (3)
        type = (GateType)Random.Range(0, System.Enum.GetValues(typeof(GateType)).Length);

        // Pick a random number within balanced ranges so numbers don't explode or collapse instantly
        switch (type)
        {
            case GateType.Add:
                value = Random.Range(addRange.x, addRange.y + 1);
                break;
            case GateType.Multiply:
                value = Random.Range(multiplyRange.x, multiplyRange.y + 1);
                break;
            case GateType.Subtract:
                value = Random.Range(subtractRange.x, subtractRange.y + 1);
                break;
            case GateType.Divide:
                value = Random.Range(divideRange.x, divideRange.y + 1);
                break;
        }
    }

    void UpdateGateVisuals()
    {
        if (displayText == null || glassRenderer == null) return;

        switch (type)
        {
            case GateType.Add:
                displayText.text = "+" + value;
                glassRenderer.material = positiveMaterial;
                break;
            case GateType.Multiply:
                displayText.text = "x" + value;
                glassRenderer.material = positiveMaterial;
                break;
            case GateType.Subtract:
                displayText.text = "-" + value;
                glassRenderer.material = negativeMaterial;
                break;
            case GateType.Divide:
                displayText.text = "÷" + value;
                glassRenderer.material = negativeMaterial;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag("Player"))
        {
            isTriggered = true;

            if (CrowdManager.Instance != null)
            {
                CrowdManager.Instance.ApplyGateMath(type, value);
            }

            if (sisterGate != null)
            {
                sisterGate.SetActive(false);
            }

            if (transform.parent != null)
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}