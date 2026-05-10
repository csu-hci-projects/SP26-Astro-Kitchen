using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        // Automatically finds your VR Headset camera when the object spawns
        if (Camera.main != null)
        {
            mainCamera = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        // Rotates the text to perfectly face the camera every frame
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.forward);
        }
    }
}