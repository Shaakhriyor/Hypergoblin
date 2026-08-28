using UnityEngine;
using TMPro;

public enum GateType { Add, Multiply, Subtract, Divide }

public class MathGate : MonoBehaviour
{
    [Header("Gate Settings")]
    public GateType type = GateType.Add;
    public int value = 5;

    [Header("References")]
    public TMP_Text displayText;
    public GameObject sisterGate;

    [Header("Colors")]
    public MeshRenderer glassRenderer;
    public Material positiveMaterial; // Blue material
    public Material negativeMaterial; // Red material

    private bool isTriggered = false;

    void Start()
    {
        UpdateGateVisuals();
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

            // Trigger the crowd manager math
            if (CrowdManager.Instance != null)
            {
                CrowdManager.Instance.ApplyGateMath(type, value);
            }

            // Disable sister gate
            if (sisterGate != null)
            {
                sisterGate.SetActive(false);
            }

            // Disable gate visuals
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