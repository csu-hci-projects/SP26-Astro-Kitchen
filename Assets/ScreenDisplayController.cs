using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenDisplayController : MonoBehaviour
{
    public enum InteractionMethod
    {
        ThumbsUp,
        Pinch,
        Grab
    }

    [System.Serializable]
    public class TrialData
    {
        public float distance = 2.0f;
        public float diameter = 0.2f;
        public Vector3 direction = new Vector3(0, 0, 1);
        public InteractionMethod interactionMethod = InteractionMethod.ThumbsUp;
    }

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
        if (pathText != null)
            pathText.gameObject.SetActive(false);

        ShowCurrentTrial();
    }

    public void ShowCurrentTrial()
    {
        if (trials.Count == 0)
        {
            if (trialText != null) trialText.text = "No trials defined";
            if (methodText != null) methodText.text = "";
            if (resultText != null) resultText.text = "";
            return;
        }

        if (currentTrialIndex < 0 || currentTrialIndex >= trials.Count)
            return;

        TrialData currentTrial = trials[currentTrialIndex];

        if (trialText != null)
            trialText.text = $"Trial: {currentTrialIndex + 1} / {trials.Count}";

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

    public void NextTrial()
    {
        currentTrialIndex++;

        if (currentTrialIndex < trials.Count)
        {
            ShowCurrentTrial();
        }
        else
        {
            ShowFinalPath(Application.persistentDataPath + "/your_csv_file.csv");
        }
    }

    public void ShowFinalPath(string path)
    {
        if (trialText != null) trialText.gameObject.SetActive(false);
        if (methodText != null) methodText.gameObject.SetActive(false);
        if (resultText != null) resultText.gameObject.SetActive(false);

        if (pathText != null)
        {
            pathText.gameObject.SetActive(true);
            pathText.text = "Experiment Complete!\nData saved to:\n" + path;
            pathText.color = Color.white;
        }
    }

    private string GetMethodDisplayName(InteractionMethod method)
    {
        switch (method)
        {
            case InteractionMethod.ThumbsUp: return "Thumbs-Up";
            case InteractionMethod.Pinch: return "Pinch";
            case InteractionMethod.Grab: return "Grab";
            default: return method.ToString();
        }
    }
}