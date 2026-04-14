using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerGestureInput : MonoBehaviour
{
    [Header("References")]
    public ExperimentManager experimentManager;
    public Behaviour rayLineVisual; // We will drag the Near-Far Interactor here to toggle the line
    public Animator handAnimator;   // We will drag the LeftHandQuestVisual here

    [Header("Controller Input Actions")]
    public InputActionProperty triggerAction; // Set to XRI Left/Activate
    public InputActionProperty triggerValue;  // Set to XRI Left/Activate Value (For animations)
    public InputActionProperty gripAction;    // Set to XRI Left/Select

    void OnEnable()
    {
        if (triggerAction.action != null) triggerAction.action.Enable();
        if (triggerValue.action != null) triggerValue.action.Enable();
        if (gripAction.action != null) gripAction.action.Enable();
    }

    void Update()
    {
        if (experimentManager == null) return;

        // --- 1. TOUCH LOGIC (Press Trigger) ---
        if (triggerAction.action != null && triggerAction.action.WasPressedThisFrame())
        {
            experimentManager.AttemptInteraction(ScreenDisplayController.InteractionMethod.Touch);
        }

        // --- 2. RAY LOGIC (Hold to aim, Release to select) ---
        if (gripAction.action != null)
        {
            // FIRST: Check if they released it and attempt the hit while the ray is still active!
            if (gripAction.action.WasReleasedThisFrame())
            {
                experimentManager.AttemptInteraction(ScreenDisplayController.InteractionMethod.Ray);
            }

            // SECOND: Now it is safe to turn the ray visual on or off
            bool isHoldingGrip = gripAction.action.IsPressed();
            if (rayLineVisual != null) rayLineVisual.enabled = isHoldingGrip;
        }
    }
}