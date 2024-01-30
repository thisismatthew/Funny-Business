using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChuckleHubManager : MonoBehaviour
{
    public List<ComedianData> ComedyScene;
    private List<ComedianData> AvailableToHire = new List<ComedianData>();
    public List<ComedianData> OnRoster = new List<ComedianData>();
    private IGigEvent[] GigEvents;
    public List<ProfileDisplay> AvailableHireWindows;
    private List<Stats> originalStats = new List<Stats>();
    public List<ComedianData> FlagForRemovalAtEOD = new List<ComedianData>();

    private int RosterIndex = 0;
    public List<RosterPanel> RosterPanels;
    public Window OverBooked;

    public TypewriterByCharacter GigSummary;
    public Window GigSummaryWindow;
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
    private void Start()
    {
        Debug.Log("MATT: chuckle Awake on " + gameObject.name);
        RefreshHireAvails();
        foreach (var comedian in ComedyScene)
        {
            Stats statCopy = new Stats();
            statCopy.Buzz = comedian.Statistics.Buzz;
            statCopy.HitRate = comedian.Statistics.HitRate;
            statCopy.Probo = comedian.Statistics.Probo;
            statCopy.SelfObsession = comedian.Statistics.SelfObsession;
            statCopy.GigSpeedDays = comedian.Statistics.GigSpeedDays;
            originalStats.Add(statCopy);
            comedian.hired = false;
            comedian.onGig = false;
            AvailableToHire.Add(comedian);
        }
    }

    private void OnApplicationQuit()
    {
        SetOGStatsBack();
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
        data.offended = false;
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
                Debug.Log("MATT: " + comedian.name + " went to a gig");
                foreach (var gigEvent in GigEvents)
                {
                    Debug.Log("event action");
                    gigEvent.RunEvent(comedian);
                }

                comedian.onGig = false;
            }
        }

        foreach (var comic in FlagForRemovalAtEOD)
        {
            RemoveFromRoster(comic);
        }
        FlagForRemovalAtEOD.Clear();
        
        if (currentGigSummaryText == "") currentGigSummaryText = "- no one performed...";
        
        Invoke("DelayShowGigSummary", 6f);
        
    }

    public void DelayShowGigSummary()
    {
        GigSummaryWindow.OpenWindow();
        GigSummary.ShowText(currentGigSummaryText);
        currentGigSummaryText = "";
    }

    public void SetOGStatsBack()
    {

        for (int i = 0; i < originalStats.Count - 1; i++)
        {
            ComedyScene[i].Statistics = originalStats[i];
        }
    }
}
