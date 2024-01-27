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
    
    private void HireTalent()
    {
        FindObjectOfType<ChuckleHubManager>().AddToRoster(loadedComedian);
        CloseWindow();
    }

    public void LoadInComdian(ComedianData comedian)
    {
        loadedComedian = comedian;
        profilePic.sprite = comedian.Portrait;
        Name.text = comedian.Name;
        Bio.text = comedian.Bio;
        GigSpeed.text = "Gig Prep Time: " + comedian.Statistics.GigSpeedDays;
        HitRate.text = "Hit Rate: " + comedian.Statistics.HitRatePhrase[comedian.Statistics.HitRate];
        SelfObs.text = "Self Obsession: " + comedian.Statistics.SelfObsessedPhrases[comedian.Statistics.SelfObsession];
        Probo.text = "How Probo: " + comedian.Statistics.ProbboPhrases[comedian.Statistics.Probo];
        Buzz.text = "How Probo: " + comedian.Statistics.BuzzPhrases[comedian.Statistics.Buzz];
        OpenWindow();
    }
    
}
