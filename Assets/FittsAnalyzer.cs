using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public static class FittsAnalyzer
{
    private class TrialRecord
    {
        public int trialNumber;
        public int repetition;
        public string expectedMethod;
        public string performedMethod;
        public float distanceA;
        public float diameterW;
        public string direction;
        public bool wasHit;
        public float movementTime;
        public float shannonID;
        public float shannonTP;
        public float originalID;
        public float originalTP;
    }

    private class ConditionSummary
    {
        public string expectedMethod;
        public float distanceA;
        public float diameterW;
        public string direction;

        public int totalTrials;
        public int hits;
        public int misses;
        public float errorRatePercent;

        public float meanMT;
        public float meanShannonID;
        public float meanShannonTP;
        public float meanOriginalID;
        public float meanOriginalTP;
    }

    private struct RegressionResult
    {
        public float interceptA;
        public float slopeB;
        public float rSquared;
    }

    public static void Analyze(string rawCsvPath, string summaryCsvPath, string comparisonCsvPath)
    {
        if (!File.Exists(rawCsvPath))
        {
            Debug.LogError("Raw CSV file not found: " + rawCsvPath);
            return;
        }

        List<TrialRecord> records = ReadRawCsv(rawCsvPath);

        if (records.Count == 0)
        {
            Debug.LogError("No trial records found in raw CSV.");
            return;
        }

        List<ConditionSummary> summaries = BuildConditionSummaries(records);

        WriteConditionSummaryCsv(summaryCsvPath, summaries);
        WriteComparisonCsv(comparisonCsvPath, summaries);

        Debug.Log("Fitts analysis complete.");
        Debug.Log("Summary CSV: " + summaryCsvPath);
        Debug.Log("Comparison CSV: " + comparisonCsvPath);
    }

    private static List<TrialRecord> ReadRawCsv(string path)
    {
        List<TrialRecord> records = new List<TrialRecord>();
        string[] lines = File.ReadAllLines(path);
        CultureInfo culture = CultureInfo.InvariantCulture;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] parts = lines[i].Split(',');

            if (parts.Length < 13)
                continue;

            TrialRecord record = new TrialRecord
            {
                trialNumber = int.Parse(parts[0], culture),
                repetition = int.Parse(parts[1], culture),
                expectedMethod = parts[2],
                performedMethod = parts[3],
                distanceA = float.Parse(parts[4], culture),
                diameterW = float.Parse(parts[5], culture),
                direction = parts[6],
                wasHit = parts[7].Trim().Equals("Hit", StringComparison.OrdinalIgnoreCase),
                movementTime = float.Parse(parts[8], culture),
                shannonID = float.Parse(parts[9], culture),
                shannonTP = float.Parse(parts[10], culture),
                originalID = float.Parse(parts[11], culture),
                originalTP = float.Parse(parts[12], culture)
            };

            records.Add(record);
        }

        return records;
    }

    private static List<ConditionSummary> BuildConditionSummaries(List<TrialRecord> records)
    {
        CultureInfo culture = CultureInfo.InvariantCulture;
        Dictionary<string, List<TrialRecord>> groupedRecords = new Dictionary<string, List<TrialRecord>>();

        foreach (TrialRecord record in records)
        {
            string key =
                record.expectedMethod + "|" +
                record.distanceA.ToString("F4", culture) + "|" +
                record.diameterW.ToString("F4", culture) + "|" +
                record.direction;

            if (!groupedRecords.ContainsKey(key))
                groupedRecords[key] = new List<TrialRecord>();

            groupedRecords[key].Add(record);
        }

        List<ConditionSummary> summaries = new List<ConditionSummary>();

        foreach (KeyValuePair<string, List<TrialRecord>> pair in groupedRecords)
        {
            List<TrialRecord> group = pair.Value;
            TrialRecord first = group[0];

            int totalTrials = group.Count;
            int hits = group.Count(r => r.wasHit);
            int misses = totalTrials - hits;

            // Groupmate's Fix: Calculate averages FIRST
            float avgMT = group.Average(r => r.movementTime);
            float avgShannonID = group.Average(r => r.shannonID);
            float avgOriginalID = group.Average(r => r.originalID);

            ConditionSummary summary = new ConditionSummary
            {
                expectedMethod = first.expectedMethod,
                distanceA = first.distanceA,
                diameterW = first.diameterW,
                direction = first.direction,
                totalTrials = totalTrials,
                hits = hits,
                misses = misses,
                errorRatePercent = (totalTrials > 0) ? (100f * misses / totalTrials) : 0f,
                
                meanMT = avgMT,
                meanShannonID = avgShannonID,
                // Groupmate's Fix: Calculate TP using the grouped averages
                meanShannonTP = (avgMT > 0f) ? (avgShannonID / avgMT) : 0f,
                
                meanOriginalID = avgOriginalID,
                // Groupmate's Fix: Calculate TP using the grouped averages
                meanOriginalTP = (avgMT > 0f) ? (avgOriginalID / avgMT) : 0f
            };

            summaries.Add(summary);
        }

        return summaries
            .OrderBy(s => s.expectedMethod)
            .ThenBy(s => s.distanceA)
            .ThenBy(s => s.diameterW)
            .ThenBy(s => s.direction)
            .ToList();
    }

    private static void WriteConditionSummaryCsv(string path, List<ConditionSummary> summaries)
    {
        StringBuilder sb = new StringBuilder();
        CultureInfo culture = CultureInfo.InvariantCulture;

        sb.AppendLine("ExpectedMethod,DistanceA,DiameterW,Direction,TotalTrials,Hits,Misses,ErrorRatePercent,MeanMovementTimeMT,MeanShannonID,MeanShannonTP,MeanOriginalID,MeanOriginalTP");

        foreach (ConditionSummary s in summaries)
        {
            sb.AppendLine(
                s.expectedMethod + "," +
                s.distanceA.ToString(culture) + "," +
                s.diameterW.ToString(culture) + "," +
                s.direction + "," +
                s.totalTrials.ToString(culture) + "," +
                s.hits.ToString(culture) + "," +
                s.misses.ToString(culture) + "," +
                s.errorRatePercent.ToString(culture) + "," +
                s.meanMT.ToString(culture) + "," +
                s.meanShannonID.ToString(culture) + "," +
                s.meanShannonTP.ToString(culture) + "," +
                s.meanOriginalID.ToString(culture) + "," +
                s.meanOriginalTP.ToString(culture)
            );
        }

        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteComparisonCsv(string path, List<ConditionSummary> summaries)
    {
        StringBuilder sb = new StringBuilder();
        CultureInfo culture = CultureInfo.InvariantCulture;

        List<float> shannonIDs = summaries.Select(s => s.meanShannonID).ToList();
        List<float> originalIDs = summaries.Select(s => s.meanOriginalID).ToList();
        List<float> meanMTs = summaries.Select(s => s.meanMT).ToList();

        RegressionResult shannonRegression = CalculateRegression(shannonIDs, meanMTs);
        RegressionResult originalRegression = CalculateRegression(originalIDs, meanMTs);

        float overallMeanShannonTP = summaries.Average(s => s.meanShannonTP);
        float overallMeanOriginalTP = summaries.Average(s => s.meanOriginalTP);

        string betterFitModel = shannonRegression.rSquared >= originalRegression.rSquared
            ? "Shannon"
            : "OriginalFitts";

        sb.AppendLine("Model,Intercept_a,Slope_b,RSquared,OverallMeanTP");

        sb.AppendLine(
            "Shannon," +
            shannonRegression.interceptA.ToString(culture) + "," +
            shannonRegression.slopeB.ToString(culture) + "," +
            shannonRegression.rSquared.ToString(culture) + "," +
            overallMeanShannonTP.ToString(culture)
        );

        sb.AppendLine(
            "OriginalFitts," +
            originalRegression.interceptA.ToString(culture) + "," +
            originalRegression.slopeB.ToString(culture) + "," +
            originalRegression.rSquared.ToString(culture) + "," +
            overallMeanOriginalTP.ToString(culture)
        );

        sb.AppendLine();
        sb.AppendLine("BestFittingModel," + betterFitModel);
        sb.AppendLine("HigherRSquaredValue," +
            Mathf.Max(shannonRegression.rSquared, originalRegression.rSquared).ToString(culture));

        File.WriteAllText(path, sb.ToString());
    }

    private static RegressionResult CalculateRegression(List<float> xValues, List<float> yValues)
    {
        RegressionResult result = new RegressionResult
        {
            interceptA = 0f,
            slopeB = 0f,
            rSquared = 0f
        };

        if (xValues == null || yValues == null || xValues.Count != yValues.Count || xValues.Count < 2)
            return result;

        int n = xValues.Count;

        float sumX = 0f;
        float sumY = 0f;
        float sumXY = 0f;
        float sumX2 = 0f;

        for (int i = 0; i < n; i++)
        {
            sumX += xValues[i];
            sumY += yValues[i];
            sumXY += xValues[i] * yValues[i];
            sumX2 += xValues[i] * xValues[i];
        }

        float denominator = (n * sumX2) - (sumX * sumX);

        if (Mathf.Abs(denominator) < 0.00001f)
            return result;

        result.slopeB = ((n * sumXY) - (sumX * sumY)) / denominator;
        result.interceptA = (sumY - (result.slopeB * sumX)) / n;

        float meanY = sumY / n;
        float ssTotal = 0f;
        float ssResidual = 0f;

        for (int i = 0; i < n; i++)
        {
            float predicted = result.interceptA + (result.slopeB * xValues[i]);
            ssTotal += Mathf.Pow(yValues[i] - meanY, 2f);
            ssResidual += Mathf.Pow(yValues[i] - predicted, 2f);
        }

        result.rSquared = (ssTotal > 0f) ? 1f - (ssResidual / ssTotal) : 1f;

        return result;
    }
}