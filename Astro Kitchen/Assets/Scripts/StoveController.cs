using UnityEngine;

public class StoveController : MonoBehaviour
{
    [Header("The Visuals")]
    // Drag your red cylinder objects into this list in the Inspector
    public GameObject[] glowingDiscs; 

    [Header("The Logic")]
    // Drag your 'BurnerTriggerZone' object here
    public StoveBurner burnerLogic; 

    private bool isOn = false;

    void Start()
    {
        // Make sure everything is turned off when the game starts
        UpdateStoveState(false);
    }

    // Call this exact function when your VR button is pressed!
    public void ToggleStove()
    {
        isOn = !isOn; // Flips the state (off becomes on, on becomes off)
        UpdateStoveState(isOn);
    }

    private void UpdateStoveState(bool state)
    {
        // 1. Turn the visual red discs on or off
        foreach (GameObject disc in glowingDiscs)
        {
            disc.SetActive(state);
        }

        // 2. Tell the invisible trigger zone if it is hot or not
        if (burnerLogic != null)
        {
            if (state) burnerLogic.TurnOnStove();
            else burnerLogic.TurnOffStove();
        }
    }
}