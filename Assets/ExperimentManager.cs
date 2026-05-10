using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    [Header("References")]
    public ScreenDisplayController screenDisplay;

    void Start()
    {
        if (screenDisplay == null)
        {
            Debug.LogError("ScreenDisplayController not assigned.");
            return;
        }

        screenDisplay.ShowCurrentTrial();
    }

    public void OnGesturePerformed(ScreenDisplayController.InteractionMethod performedMethod)
    {
        if (screenDisplay == null) return;

        ScreenDisplayController.TrialData currentTrial = screenDisplay.GetCurrentTrial();
        if (currentTrial == null) return;

        bool hit = currentTrial.interactionMethod == performedMethod;

        screenDisplay.ShowResult(hit);

        if (hit)
        {
            Invoke(nameof(AdvanceTrial), 0.75f);
        }
    }

    void AdvanceTrial()
    {
        screenDisplay.NextTrial();
    }
}