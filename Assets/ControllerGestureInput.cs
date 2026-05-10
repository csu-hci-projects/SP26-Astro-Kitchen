using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerGestureInput : MonoBehaviour
{
    [Header("References")]
    public ExperimentManager experimentManager;

    [Header("Controller Input Actions")]
    public InputActionProperty triggerAction;
    public InputActionProperty gripAction;
    public InputActionProperty primaryButtonAction;

    void OnEnable()
    {
        EnableAction(triggerAction);
        EnableAction(gripAction);
        EnableAction(primaryButtonAction);
    }

    void OnDisable()
    {
        DisableAction(triggerAction);
        DisableAction(gripAction);
        DisableAction(primaryButtonAction);
    }

    void Update()
    {
        if (experimentManager == null) return;

        if (WasPressedThisFrame(primaryButtonAction))
        {
            Debug.Log("PRIMARY BUTTON PRESSED");
            experimentManager.OnGesturePerformed(ScreenDisplayController.InteractionMethod.ThumbsUp);
        }

        if (WasPressedThisFrame(triggerAction))
        {
            Debug.Log("TRIGGER PRESSED");
            experimentManager.OnGesturePerformed(ScreenDisplayController.InteractionMethod.Pinch);
        }

        if (WasPressedThisFrame(gripAction))
        {
            Debug.Log("GRIP PRESSED");
            experimentManager.OnGesturePerformed(ScreenDisplayController.InteractionMethod.Grab);
        }
    }

    private bool WasPressedThisFrame(InputActionProperty actionProperty)
    {
        return actionProperty.action != null && actionProperty.action.WasPressedThisFrame();
    }

    private void EnableAction(InputActionProperty actionProperty)
    {
        if (actionProperty.action != null)
            actionProperty.action.Enable();
    }

    private void DisableAction(InputActionProperty actionProperty)
    {
        if (actionProperty.action != null)
            actionProperty.action.Disable();
    }
}