using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Stats
{
    public int GigSpeedDays = 1, 
        HitRatePercentage = 50, 
        ProboPercentage = 2,
        SelfEsteemPercentage = 80, 
        Buzz = 1;

    public Stats()
    {
        
    }
}
[CreateAssetMenu(fileName = "Comedian", menuName = "Comedian Data", order = 1)]
public class ComedianData : ScriptableObject
{
    public string Name, Bio;
    public Sprite Portrait;
    public Stats Statistics;
    public List<GigEvent> Events;

}



