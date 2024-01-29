using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PoblematicBehaviour : MonoBehaviour, IGigEvent
{
    public int ProboAmount = 20;
    public void RunEvent(ComedianData data)
    {
        
        int ProblomaticAction = data.Statistics.Probo * 2;
        int random = Random.Range(1, ProboAmount);
        if (random < ProblomaticAction)
        {
            foreach (var otherComic in ChuckleHubManager.Instance.OnRoster)
            {
                if (otherComic.name != data.name)
                {
                    if (otherComic.Statistics.SelfObsession > 2)
                    {
                        ChuckleHubManager.Instance.RemoveFromRoster(otherComic);
                        ChuckleHubManager.Instance.AddToGigSummary(data.name + " offended " + otherComic.name + " so deeply they have dropped you as a manager.");
                    }
                    else if (otherComic.Statistics.Probo > 2)
                    {
                        otherComic.Statistics.Probo = BumpStat(otherComic.Statistics.Probo);
                        
                        ChuckleHubManager.Instance.AddToGigSummary(data.name + " inspired " + otherComic.name + " with their edgy takes.");
                        ChuckleHubManager.Instance.UpdateComic(data);
                    } 
                }
            }
        }
    }
    
    public int BumpStat(int stat)
    {
        if (stat == 5) return 5;
        else return stat+1;
    }
}
