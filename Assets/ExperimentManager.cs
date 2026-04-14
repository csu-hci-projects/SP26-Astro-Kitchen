using UnityEngine;
using System.IO;
using System.Collections;
using System.Globalization;

public class ExperimentManager : MonoBehaviour
{
    [Header("Experiment Settings")]
    public string groupName = "MyGroup";

    [Header("References")]
    public ScreenDisplayController screenDisplay;
    public GameObject targetPrefab;
    public Transform playerOrigin;

    private GameObject currentTargetObject;

    private string rawCsvFilePath;
    private string summaryCsvFilePath;
    private string comparisonCsvFilePath;

    private float trialStartTime;
    private readonly CultureInfo culture = CultureInfo.InvariantCulture;

    IEnumerator Start()
    {
        if (screenDisplay == null || targetPrefab == null || playerOrigin == null)
            yield break;

        yield return null;

        rawCsvFilePath = Path.Combine(Application.persistentDataPath, groupName + "_Outputfile.csv");
        summaryCsvFilePath = Path.Combine(Application.persistentDataPath, groupName + "_ConditionSummary.csv");
        comparisonCsvFilePath = Path.Combine(Application.persistentDataPath, groupName + "_ModelComparison.csv");

        CreateRawCsvFile();

        screenDisplay.ShowCurrentTrial();
        SpawnNextTarget();
    }

    private void CreateRawCsvFile()
    {
        string header =
            "TrialNumber,Repetition,ExpectedMethod,PerformedMethod,DistanceA,DiameterW,Direction,HitOrMiss,MovementTimeMT,ShannonID,ShannonTP,OriginalID,OriginalTP\n";

        File.WriteAllText(rawCsvFilePath, header);
    }

    public void SpawnNextTarget()
    {
        ScreenDisplayController.TrialData currentTrial = screenDisplay.GetCurrentTrial();
        if (currentTrial == null) return;

        Vector3 worldDirection = playerOrigin.TransformDirection(currentTrial.direction.normalized);
        Vector3 spawnPosition = playerOrigin.position + (worldDirection * currentTrial.distance);
        spawnPosition.y = playerOrigin.position.y;

        currentTargetObject = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        currentTargetObject.transform.localScale = new Vector3(
            currentTrial.diameter,
            currentTrial.diameter,
            currentTrial.diameter
        );

        trialStartTime = Time.time;
    }

    public void AttemptInteraction(ScreenDisplayController.InteractionMethod performedMethod)
    {
        ScreenDisplayController.TrialData currentTrial = screenDisplay.GetCurrentTrial();
        if (currentTrial == null || currentTargetObject == null) return;

        TargetSphere targetScript = currentTargetObject.GetComponent<TargetSphere>();
        if (targetScript == null) return;

        bool onTarget = false;

        if (performedMethod == ScreenDisplayController.InteractionMethod.Touch)
        {
            onTarget = targetScript.isTouched;
        }
        else if (performedMethod == ScreenDisplayController.InteractionMethod.Ray)
        {
            onTarget = targetScript.isHovered;
        }

        bool correctMethod = (currentTrial.interactionMethod == performedMethod);
        bool isHit = (onTarget && correctMethod);

        RecordData(isHit, performedMethod);

        if (currentTargetObject != null)
            Destroy(currentTargetObject);

        Invoke(nameof(AdvanceTrial), 0.75f);
    }

    private void RecordData(bool hit, ScreenDisplayController.InteractionMethod methodUsed)
    {
        ScreenDisplayController.TrialData currentTrial = screenDisplay.GetCurrentTrial();
        if (currentTrial == null) return;

        screenDisplay.ShowResult(hit);

        float MT = Time.time - trialStartTime;
        float A = currentTrial.distance;
        float W = currentTrial.diameter;

        float shannonID = CalculateShannonID(A, W);
        float shannonTP = (MT > 0f) ? shannonID / MT : 0f;

        float originalID = CalculateOriginalFittsID(A, W);
        float originalTP = (MT > 0f) ? originalID / MT : 0f;

        string hitOrMiss = hit ? "Hit" : "Miss";
        string directionLabel = screenDisplay.GetDirectionLabel(currentTrial.direction);

        string dataRow =
            currentTrial.trialNumber.ToString(culture) + "," +
            currentTrial.repetitionNumber.ToString(culture) + "," +
            currentTrial.interactionMethod + "," +
            methodUsed + "," +
            A.ToString(culture) + "," +
            W.ToString(culture) + "," +
            directionLabel + "," +
            hitOrMiss + "," +
            MT.ToString(culture) + "," +
            shannonID.ToString(culture) + "," +
            shannonTP.ToString(culture) + "," +
            originalID.ToString(culture) + "," +
            originalTP.ToString(culture) + "\n";

        File.AppendAllText(rawCsvFilePath, dataRow);
    }

    private float CalculateShannonID(float A, float W)
    {
        if (A <= 0f || W <= 0f) return 0f;
        return Mathf.Log((A / W) + 1f, 2f);
    }

    private float CalculateOriginalFittsID(float A, float W)
    {
        if (A <= 0f || W <= 0f) return 0f;

        float value = (2f * A) / W;
        if (value <= 0f) return 0f;

        return Mathf.Log(value, 2f);
    }

    private void AdvanceTrial()
    {
        bool hasMoreTrials = screenDisplay.AdvanceToNextTrial();

        if (hasMoreTrials)
        {
            SpawnNextTarget();
        }
        else
        {
            FinalizeExperiment();
        }
    }

    private void FinalizeExperiment()
    {
        FittsAnalyzer.Analyze(rawCsvFilePath, summaryCsvFilePath, comparisonCsvFilePath);

        string finalMessage =
            "Experiment Complete!\n\n" +
            "Raw trial CSV:\n" + rawCsvFilePath + "\n\n" +
            "Condition summary CSV:\n" + summaryCsvFilePath + "\n\n" +
            "Model comparison CSV:\n" + comparisonCsvFilePath;

        screenDisplay.ShowFinalPath(finalMessage);
    }
}