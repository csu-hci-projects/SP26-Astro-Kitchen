using UnityEngine;
using UnityEngine.InputSystem; 


public class ControllerExtractor : MonoBehaviour
{
    [Header("Link your Pot Script here")]
    public MasterCookingPot potScript;

    // We build the input action directly in the code! 
    // This exact string path tells Unity to look for the "B" button on the Right hand.
    private InputAction bButtonAction = new InputAction(type: InputActionType.Button, binding: "<XRController>{RightHand}/secondaryButton");

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        // Turn the button listener on
        bButtonAction.Enable();
        bButtonAction.performed += ButtonPressed;
    }

    void OnDisable()
    {
        // Turn the button listener off
        bButtonAction.Disable();
        bButtonAction.performed -= ButtonPressed;
    }

    private void ButtonPressed(InputAction.CallbackContext context)
    {
        // ONLY extract the food if the player is actively holding the pot!
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            potScript.ExtractFood();
        }
    }
}