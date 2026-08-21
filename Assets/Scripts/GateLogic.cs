using UnityEngine;
using TMPro;

public enum GateType { Add, Multiply, Subtract, Divide }

public class MathGate : MonoBehaviour
{
    [Header("Gate Settings")]
    public GateType gateType;
    public int value = 5;

    [Header("Visual References")]
    public TMP_Text gateText;
    public MeshRenderer glassRenderer;
    public Material positiveMaterial;
    public Material negativeMaterial;

    void Start()
    {
        UpdateGateVisuals();
    }

    public void UpdateGateVisuals()
    {
        
        switch (gateType)
        {
            case GateType.Add:
                gateText.text = "+" + value;
                glassRenderer.material = positiveMaterial;
                break;
            case GateType.Multiply:
                gateText.text = "x" + value;
                glassRenderer.material = positiveMaterial;
                break;
            case GateType.Subtract:
                gateText.text = "-" + value;
                glassRenderer.material = negativeMaterial;
                break;
            case GateType.Divide:
                gateText.text = "÷" + value;
                glassRenderer.material = negativeMaterial;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if player or leader goblin touched the gate
        if (other.CompareTag("Player"))
        {
            CrowdManager.Instance.ModifyCrowd(gateType, value);

            // Disable this gate panel so it cannot trigger twice
            gameObject.SetActive(false);
        }
    }
}