using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChuckleHubManager : MonoBehaviour
{
    public List<ComedianData> ComedyScene;
    private List<ComedianData> AvailableToHire = new List<ComedianData>(), OnRoster = new List<ComedianData>();
    public List<IGigEvent> GigEvents;
    public List<ProfileDisplay> AvailableHireWindows;


    private int RosterIndex = 0;
    public List<RosterPanel> RosterPanels;
    public Window OverBooked;


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
        if (AvailableToHire.Count > 3) hiresAvailable = 3;
        else hiresAvailable = AvailableToHire.Count;


        for (int i = 0; i < hiresAvailable; i++)
        {
            Debug.Log(i);
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
        if (OnRoster.Count==RosterPanels.Count)
        {
            OverBooked.OpenWindow(); 
            return;
        }

        foreach (var panel in RosterPanels)
        {
            if (!panel.gameObject.activeSelf)
            {
                panel.gameObject.SetActive(true);
                panel.LoadNewComic(data);
                break;
            }
        }
        OnRoster.Add(data);
        AvailableToHire.Remove(data);
    }

    public void RemoveFromRoster(ComedianData data)
    {
        foreach (var panel in RosterPanels)
        {
            if (panel.Name.text == data.Name)
            {
                panel.gameObject.SetActive(false);
                break;
            }
        }

        OnRoster.Remove(data);
        //Do we add the comic back to the avail to hire? maybe thats an event...
    }

    public void CheckRosteredComic(ComedianData data)
    {
        AvailableHireWindows[0].LoadInComdian(data, true);
    }

    public void EndDay()
    {
        foreach (var panel in RosterPanels)
        {
            if (panel.gameObject.activeSelf)
            {
                panel.UpdateGigCountdown();
            }
        }
        /*foreach (var gigEvent in GigEvents)
        {
            foreach (var comedian in OnRoster)
            {
                if (gigEvent.CheckRequirements(comedian))
                {
                    gigEvent.TriggerEvent();
                }
            }
        }*/
    }
    
    
}
