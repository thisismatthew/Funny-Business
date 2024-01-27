using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckleHubManager : MonoBehaviour
{
    public List<ComedianData> ComedyScene;
    private List<ComedianData> AvailableToHire = new List<ComedianData>(), OnRoster = new List<ComedianData>();
    public List<IGigEvent> GigEvents;
    public List<ProfileDisplay> AvailableHireWindows;


    private void Start()
    {
        RefreshHireAvails();
        foreach (var comedian in ComedyScene)
        {
            AvailableToHire.Add(comedian);
        }
    }

    public void OpenHireWindows()
    {
        int hiresAvailable = 0;
        if (AvailableToHire.Count > 3) hiresAvailable = 2;
        else hiresAvailable = AvailableToHire.Count;


        for (int i = 0; i < hiresAvailable; i++)
        {
            AvailableHireWindows[i].LoadInComdian(AvailableToHire[i]);
        }
    }

    public void RefreshHireAvails()
    {
        AvailableToHire.Clear();
        foreach (var comedian in ComedyScene)
        {
            foreach (var working in OnRoster)
            {
                if (comedian.Name != working.Name)
                    AvailableToHire.Add(comedian);
            }
        }
    }

    public void AddToRoster(ComedianData data)
    {
        OnRoster.Add(data);
        AvailableToHire.Remove(data);
    }

    public void EndDay()
    {
        foreach (var gigEvent in GigEvents)
        {
            foreach (var comedian in OnRoster)
            {
                if (gigEvent.CheckRequirements(comedian))
                {
                    gigEvent.TriggerEvent();
                }
            }
        }
    }
    
    
}
