using UnityEngine;
using UnityEngine.XR;

public class GazeGrabber : MonoBehaviour
{
    [Header("Setup")]
    public Camera mainCamera;
    public LayerMask interactableLayer;

    [Header("Grab Settings")]
    public float maxGrabDistance = 10f;
    public float minReelDistance = 0.15f; 
    [Tooltip("The exact distance the object will automatically zip to when first grabbed.")]
    public float defaultGrabDistance = 0.1f; 
    public float pushPullSpeed = 4f; 
    [Tooltip("How thick the gaze beam is. Higher numbers make it easier to grab without looking perfectly at the object.")]
    public float grabRadius = 0.15f; 
    
    [Header("Visual Feedback")]
    public Color hoverTint = Color.yellow;

    // Internal variables
    private Rigidbody heldObject;
    private float currentHoldDistance;
    private float originalDrag;
    private bool previousTriggerState = false;

    // Hover variables
    private GameObject currentHoverObject;
    private Renderer hoverRenderer;
    private Color originalColor;

    void Update()
    {
        // Get BOTH controllers
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        // 1. Continuous Hover Detection
        if (heldObject == null)
        {
            DetectHover();
        }
        else
        {
            ClearHover(); 
        }

        // 2. Grabbing and Dropping (Right Trigger)
        rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);
        
        if (triggerPressed && !previousTriggerState)
        {
            if (heldObject == null) TryGrabObject();
            else DropObject();
        }
        previousTriggerState = triggerPressed;

        // 3. Push and Pull (X and Y Buttons on LEFT Controller)
        if (heldObject != null)
        {
            leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool xPressed);   // X Button
            leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool yPressed); // Y Button
            
            if (xPressed) 
            {
                // Pull closer (X Button)
                currentHoldDistance -= pushPullSpeed * Time.deltaTime;
            }
            else if (yPressed) 
            {
                // Push away (Y Button)
                currentHoldDistance += pushPullSpeed * Time.deltaTime;
            }

            // Keep the distance within limits 
            currentHoldDistance = Mathf.Clamp(currentHoldDistance, minReelDistance, maxGrabDistance);

            MaintainHoldPosition();
        }
    }

    void DetectHover()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, grabRadius, out hit, maxGrabDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.GetComponent<Rigidbody>() != null)
            {
                if (hitObject != currentHoverObject)
                {
                    ClearHover(); 
                    
                    currentHoverObject = hitObject;
                    hoverRenderer = currentHoverObject.GetComponent<Renderer>();

                    if (hoverRenderer != null)
                    {
                        originalColor = hoverRenderer.material.color;
                        hoverRenderer.material.color = hoverTint;
                    }
                }
            }
            else
            {
                ClearHover();
            }
        }
        else
        {
            ClearHover();
        }
    }

    void ClearHover()
    {
        if (currentHoverObject != null)
        {
            if (hoverRenderer != null) hoverRenderer.material.color = originalColor;
            currentHoverObject = null;
            hoverRenderer = null;
        }
    }

    void TryGrabObject()
    {
        if (currentHoverObject != null)
        {
            Rigidbody rb = currentHoverObject.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                heldObject = rb;
                originalDrag = heldObject.linearDamping;
                
                heldObject.linearDamping = 5f; 
                
                // INSTANT ZIP MECHANIC:
                // Instead of calculating the object's current distance, we immediately 
                // set the target distance to our comfortable 'defaultGrabDistance'.
                currentHoldDistance = defaultGrabDistance; 
            }
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.useGravity = false;
            heldObject.linearDamping = originalDrag;
            heldObject = null;
        }
    }

    void MaintainHoldPosition()
    {
        heldObject.useGravity = false;
        Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * currentHoldDistance;
        
        heldObject.linearVelocity = (targetPosition - heldObject.position) * 15f; 
        heldObject.angularVelocity = Vector3.zero; 
    }
}