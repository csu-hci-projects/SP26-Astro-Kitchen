using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenDisplayController : MonoBehaviour
{
    public enum InteractionMethod
    {
        Touch,
        Ray
    }

    [System.Serializable]
    public class TrialData
    {
        public int trialNumber;
        public int repetitionNumber;
        public float distance = 2.0f;
        public float diameter = 0.2f;
        public Vector3 direction = new Vector3(0, 0, 1);
        public InteractionMethod interactionMethod = InteractionMethod.Touch;
    }

    [Header("Counterbalancing")]
    [Tooltip("Use 1 for Latin square row 1, 2 for row 2.")]
    public int latinSquareRow = 1;

    [Header("Trial Definitions")]
    public List<TrialData> trials = new List<TrialData>();

    [Header("UI References")]
    public TextMeshProUGUI trialText;
    public TextMeshProUGUI methodText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI pathText;

    private int currentTrialIndex = 0;

    void Start()
    {
        GenerateTrials();

        if (pathText != null)
            pathText.gameObject.SetActive(false);

        ShowCurrentTrial();
    }

    public void ShowCurrentTrial()
    {
        if (trials.Count == 0) return;
        if (currentTrialIndex < 0 || currentTrialIndex >= trials.Count) return;

        TrialData currentTrial = trials[currentTrialIndex];

        if (trialText != null)
            trialText.text = $"Trial: {currentTrial.trialNumber} / {trials.Count}";

        if (methodText != null)
            methodText.text = $"Interaction: {GetMethodDisplayName(currentTrial.interactionMethod)}";

        if (resultText != null)
        {
            resultText.text = "";
            resultText.color = Color.white;
        }
    }

    public TrialData GetCurrentTrial()
    {
        if (currentTrialIndex >= 0 && currentTrialIndex < trials.Count)
            return trials[currentTrialIndex];

        return null;
    }

    public void ShowResult(bool hit)
    {
        if (resultText == null) return;

        resultText.text = hit ? "Hit!" : "Miss!";
        resultText.color = hit ? Color.green : Color.red;
    }

    public bool AdvanceToNextTrial()
    {
        currentTrialIndex++;

        if (currentTrialIndex < trials.Count)
        {
            ShowCurrentTrial();
            return true;
        }

        return false;
    }

    public void ShowFinalPath(string message)
    {
        if (trialText != null) trialText.gameObject.SetActive(false);
        if (methodText != null) methodText.gameObject.SetActive(false);
        if (resultText != null) resultText.gameObject.SetActive(false);

        if (pathText != null)
        {
            pathText.gameObject.SetActive(true);
            pathText.text = message;
            pathText.color = Color.white;
        }
    }

    public string GetDirectionLabel(Vector3 direction)
    {
        Vector3 dir = direction.normalized;

        if (Vector3.Distance(dir, new Vector3(0, 0, 1).normalized) < 0.01f)
            return "Forward";

        if (Vector3.Distance(dir, new Vector3(1, 0, 1).normalized) < 0.01f)
            return "ForwardRight";

        if (Vector3.Distance(dir, new Vector3(-1, 0, 1).normalized) < 0.01f)
            return "ForwardLeft";

        if (Vector3.Distance(dir, new Vector3(0, 0.5f, 1).normalized) < 0.01f)
            return "UpForward";

        return $"({direction.x:F2},{direction.y:F2},{direction.z:F2})";
    }

    private string GetMethodDisplayName(InteractionMethod method)
    {
        switch (method)
        {
            case InteractionMethod.Touch:
                return "Touch Target";
            case InteractionMethod.Ray:
                return "Ray Pointer";
            default:
                return method.ToString();
        }
    }

    private InteractionMethod[] GetLatinSquareMethodOrder()
    {
        // 2x2 Latin square:
        // Row 1: Touch, Ray
        // Row 2: Ray, Touch

        if (latinSquareRow == 2)
        {
            return new InteractionMethod[]
            {
                InteractionMethod.Ray,
                InteractionMethod.Touch
            };
        }

        return new InteractionMethod[]
        {
            InteractionMethod.Touch,
            InteractionMethod.Ray
        };
    }

    void GenerateTrials()
    {
        trials.Clear();

        InteractionMethod[] methodOrder = GetLatinSquareMethodOrder();

        float[] distances = { 0.3f, 0.6f };
        float[] sizes = { 0.05f, 0.10f };
        Vector3[] directions =
        {
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 1),
            new Vector3(-1, 0, 1),
            new Vector3(0, 0.5f, 1)
        };

        int trialCounter = 1;
        int directionIndex = 0;

        for (int rep = 1; rep <= 2; rep++)
        {
            foreach (var method in methodOrder)
            {
                foreach (var distance in distances)
                {
                    foreach (var size in sizes)
                    {
                        Vector3 chosenDirection = directions[directionIndex % directions.Length];
                        directionIndex++;

                        trials.Add(new TrialData
                        {
                            trialNumber = trialCounter,
                            repetitionNumber = rep,
                            distance = distance,
                            diameter = size,
                            direction = chosenDirection,
                            interactionMethod = method
                        });

                        trialCounter++;
                    }
                }
            }
        }
    }
}