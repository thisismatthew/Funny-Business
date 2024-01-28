using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GigPaysOut : MonoBehaviour, IGigEvent
{
    private ComedianData activeData;
    
    public void RunEvent(ComedianData data)
    {
        activeData = data;
        int randomPercentage = Random.Range(1, 5);
        if (randomPercentage < data.Statistics.HitRate)
        {
            Debug.Log(data.name + " had a banger!");
            TriggerPayout();
            return;
        } 
        Debug.Log(data.name + " flopped...");
    }

    public void TriggerPayout()
    {
        int payout = activeData.Statistics.Buzz * Random.Range(5, 25);
        Debug.Log(activeData.name + " got paid $" + payout);
        FindObjectOfType<BankApp>().money += payout;

    }
}
