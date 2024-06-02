using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    GigPrepTime,
    HitRate, 
    Probbo, 
    SelfObsessed, 
    Buzz,
}
public class E_ComedianTransforms : MonoBehaviour, IGigEvent
{
    [Header("Event Trigger Conditions")]
    public ComedianData Comedian;
    public string EvolutionText;
    public StatType StatToCheck = StatType.GigPrepTime;
    public int StatScore;
    [Header("Transformation Changes")]
    public ComedianData NewComedian;
    private void Start()
    {
        StatScore = Mathf.Clamp(StatScore, 0, 4);
        if (EvolutionText == "") Debug.LogWarning("ERROR: No Text in Transformation event");
        
    }

    public void RunEvent(ComedianData data)
    {
        if (data.name != Comedian.name) return;
        if (CheckStatConditions(data))
        {

            ChuckleHubManager.Instance.AddToGigSummary(EvolutionText);
            ChuckleHubManager.Instance.RemoveFromRoster(data);
            ChuckleHubManager.Instance.AddToRoster(NewComedian);
        }
    }

    public bool CheckStatConditions(ComedianData data)
    {
        switch (StatToCheck)
        {
            case StatType.GigPrepTime:
                if (data.Statistics.GigSpeedDays >= StatScore)
                    return true;
                break;
            case StatType.Probbo:
                if (data.Statistics.Probo >= StatScore)
                    return true;
                break;
            case StatType.Buzz:
                if (data.Statistics.Buzz >= StatScore)
                    return true;
                break;
            case StatType.SelfObsessed:
                if (data.Statistics.SelfObsession >= StatScore)
                    return true;
                break;
            case StatType.HitRate:
                if (data.Statistics.HitRate >= StatScore)
                    return true;
                break;
        }

        return false;
    }
}
