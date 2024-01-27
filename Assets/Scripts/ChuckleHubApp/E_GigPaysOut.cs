using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GigPaysOut : MonoBehaviour, IGigEvent
{
    private ComedianData activeData;
    
    public bool CheckRequirements(ComedianData data)
    {
        activeData = data;
        int randomPercentage = Random.Range(1, 100);
        if (randomPercentage < data.Statistics.HitRate)
        {
            Debug.Log(data.name + " had a banger!");
            return true;
        } 
        Debug.Log(data.name + " flopped...");
        return false;
    }

    public void TriggerEvent()
    {
        int payout = activeData.Statistics.Buzz * Random.Range(1, 10);
        Debug.Log(activeData.name + " got paid $" + payout);
        
    }
}
