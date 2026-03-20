using UnityEngine;

public class BlueBlockController : MonoBehaviour
{
    public Renderer targetRenderer;
    public Material normalMaterial;
    public Material hoverMaterial;

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();
    }

    public void TurnWhite()
    {
        targetRenderer.material = hoverMaterial;
    }

    public void TurnBack()
    {
        targetRenderer.material = normalMaterial;
    }
}