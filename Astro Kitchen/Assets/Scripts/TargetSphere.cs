using UnityEngine;

public class TargetSphere : MonoBehaviour
{
    public Material blueMaterial;
    public Material greenMaterial;

    public bool isHovered = false; // The Laser Pointer
    public bool isTouched = false; // Physical Overlap

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = blueMaterial; 
    }

    // --- RAY LOGIC (Called by XR Simple Interactable) ---
    public void OnHoverEnter()
    {
        meshRenderer.material = greenMaterial;
        isHovered = true;
    }

    public void OnHoverExit()
    {
        if (!isTouched) meshRenderer.material = blueMaterial;
        isHovered = false;
    }

    // --- PINCH LOGIC (Called by Unity Physics) ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController")) 
        {
            meshRenderer.material = greenMaterial;
            isTouched = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            if (!isHovered) meshRenderer.material = blueMaterial;
            isTouched = false;
        }
    }
}