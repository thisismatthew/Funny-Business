using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChuckleHubManager : MonoBehaviour
{
    public List<ComedianData> ComedyScene;
    private List<ComedianData> AvailableToHire = new List<ComedianData>(), OnRoster = new List<ComedianData>();
    private IGigEvent[] GigEvents;
    public List<ProfileDisplay> AvailableHireWindows;


    private int RosterIndex = 0;
    public List<RosterPanel> RosterPanels;
    public Window OverBooked;

    public TextMeshProUGUI GigSummary;
    public string currentGigSummaryText;

    public void AddToGigSummary(string summaryAddition)
    {
        if (currentGigSummaryText == "") 
            currentGigSummaryText = "- " + summaryAddition;
        else
            currentGigSummaryText += "\n- " + summaryAddition;
    }
    public static ChuckleHubManager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }

        GigEvents = GetComponents<IGigEvent>();
    }
    
    public void SendOnGig(ComedianData data)
    {
        foreach (var comedian in OnRoster)
        {
            if (comedian.Name == data.Name)
                comedian.onGig = true;
        }
    }

    public void UpdateComic(ComedianData data)
    {
        foreach (var comedian in OnRoster)
        {
            if (comedian.Name == data.Name)
                comedian.Statistics = data.Statistics;
        }
        foreach (var panel in RosterPanels)
        {
            if (panel.Name.text == data.Name)
            {
                panel.LoadNewComic(data);
            }
                
        }
    }
    private void Start()
    {
        RefreshHireAvails();
        foreach (var comedian in ComedyScene)
        {
            comedian.hired = false;
            comedian.onGig = false;
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
        data.hired = true;
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
        data.hired = false;
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

        foreach (var comedian in OnRoster)
        {
            //OK so Gig events now ONLY proc if your comedian has been sent out on a gig.
            if (comedian.onGig)
            {
                foreach (var gigEvent in GigEvents)
                {
                    gigEvent.RunEvent(comedian);
                }

                comedian.onGig = false;
            }
        }
            if (currentGigSummaryText == "") currentGigSummaryText = "- no one performed...";
        GigSummary.text = currentGigSummaryText;
        currentGigSummaryText = "";
    }
    
    
}
