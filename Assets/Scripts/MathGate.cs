using UnityEngine;
using TMPro;

public enum GateType { Add, Multiply, Subtract, Divide }

public class MathGate : MonoBehaviour
{
    [Header("Gate Settings")]
    public GateType type = GateType.Add;
    public int value = 5;

    [Header("UI Reference")]
    public TMP_Text displayText;

    void Start()
    {
        UpdateGateText();
    }

    void UpdateGateText()
    {
        if (displayText == null) return;

        switch (type)
        {
            case GateType.Add:
                displayText.text = "+" + value;
                break;
            case GateType.Multiply:
                displayText.text = "x" + value;
                break;
            case GateType.Subtract:
                displayText.text = "-" + value;
                break;
            case GateType.Divide:
                displayText.text = "/" + value;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that walked into the gate has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Touched gate: {type} {value}");

            // Disables the gate so it can't be triggered twice
            gameObject.SetActive(false);
        }
    }
}