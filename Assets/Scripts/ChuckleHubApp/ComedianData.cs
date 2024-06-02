using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]


public class Stats
{
    public int GigSpeedDays = 1;

    [FormerlySerializedAs("HitRatePercentage")] public int HitRate = 1;

    [FormerlySerializedAs("ProboPercentage")] public int Probo = 1;

    [FormerlySerializedAs("SelfEsteemPercentage")] public int SelfObsession = 1;

    public int Buzz = 1;

    public List<string> HitRatePhrase = new List<string>();
    public List<string> ProbboPhrases = new List<string>();
    public List<string> SelfObsessedPhrases = new List<string>();
    public List<string> BuzzPhrases = new List<string>();

    public Stats()
    {
        HitRatePhrase.Add("Always Bombs");
        HitRatePhrase.Add("For a VERY Niche Crowd");
        HitRatePhrase.Add("Pretty Funny Actually");
        HitRatePhrase.Add("90% Bangers");
        HitRatePhrase.Add("Never Misses");


        ProbboPhrases.Add("Your Mum Would Love");
        ProbboPhrases.Add("All Smooth, No Edge");
        ProbboPhrases.Add("M 15+");
        ProbboPhrases.Add("Offends Particular Groups");
        ProbboPhrases.Add("Banned From All Socials");
        
        SelfObsessedPhrases.Add("Lowest Self-Esteem");
        SelfObsessedPhrases.Add("Believes in Themselves");
        SelfObsessedPhrases.Add("Googles Themselves");
        SelfObsessedPhrases.Add("Clinical Grade Narcissist");
        SelfObsessedPhrases.Add("Starting a Cult");
        
        BuzzPhrases.Add("Underground");
        BuzzPhrases.Add("Up and Coming");
        BuzzPhrases.Add("On Panel Shows");
        BuzzPhrases.Add("Famous, Baby!");
        BuzzPhrases.Add("PM Uses For Clout");
    }
}
[CreateAssetMenu(fileName = "Comedian", menuName = "Comedian Data", order = 1)]
public class ComedianData : ScriptableObject
{
    public string Name, Bio;
    public Sprite Portrait;
    public Stats Statistics;
    public bool hired;
    public bool onGig;
    public bool offended = false;
}



