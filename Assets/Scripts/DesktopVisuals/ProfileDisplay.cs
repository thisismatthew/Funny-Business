using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;



public class ProfileDisplay : Window
{
    public Image profilePic;
    public TextMeshProUGUI Name, Bio, GigSpeed, HitRate, Probo, SelfObs, Buzz;
    private ComedianData loadedComedian;
    public Button DropComic;
    
    private void HireTalent()
    {
        FindObjectOfType<ChuckleHubManager>().AddToRoster(loadedComedian);
        CloseWindow();
    }

    public void LoadInComdian(ComedianData comedian, bool HiredView = false)
    {
        loadedComedian = comedian;
        profilePic.sprite = comedian.Portrait;
        Name.text = comedian.Name;
        Bio.text = comedian.Bio;
        GigSpeed.text = "Gig Prep Time: " + comedian.Statistics.GigSpeedDays;
        HitRate.text = "Hit Rate: " + comedian.Statistics.HitRatePhrase[comedian.Statistics.HitRate-1];
        SelfObs.text = "Self Obsession: " + comedian.Statistics.SelfObsessedPhrases[comedian.Statistics.SelfObsession-1];
        Probo.text = "How Probbo: " + comedian.Statistics.ProbboPhrases[comedian.Statistics.Probo-1];
        Buzz.text = "Buzz: " + comedian.Statistics.BuzzPhrases[comedian.Statistics.Buzz-1];
        OpenWindow();
        
        
        if (HiredView)
            DropComic.gameObject.SetActive(true);
        else
            DropComic.gameObject.SetActive(false);
    }

    public void LetsHireAComic()
    {
        FindObjectOfType<ChuckleHubManager>().AddToRoster(loadedComedian);
        CloseWindow();
    }
    public void LetsDropAComic()
    {
        FindObjectOfType<ChuckleHubManager>().RemoveFromRoster(loadedComedian);
        CloseWindow();
    }
    
}
