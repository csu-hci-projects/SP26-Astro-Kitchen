using UnityEngine;
using UnityEngine.InputSystem;

public class ProceduralSqueeze : MonoBehaviour
{
    [Header("Input")]
    public InputActionProperty triggerValue; // Needs XRI Left/Activate Value

    private Vector3 originalScale;

    void Start()
    {
        // Remember the hand's original size
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (triggerValue.action != null)
        {
            // Read how hard the trigger is pulled (0.0 to 1.0)
            float squeezeAmount = triggerValue.action.ReadValue<float>();

            // Squish the hand slightly on the Y and Z axis to simulate a clench/pinch
            float squishMultiplier = 1f - (squeezeAmount * 0.15f); 
            
            transform.localScale = new Vector3(originalScale.x, originalScale.y * squishMultiplier, originalScale.z * squishMultiplier);
        }
    }
}