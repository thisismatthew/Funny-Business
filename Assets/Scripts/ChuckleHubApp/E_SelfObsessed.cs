using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SelfObsessed : MonoBehaviour, IGigEvent
{
    public int SelfObsessionChance;
    public void RunEvent(ComedianData data)
    {
        Debug.Log("checking Self Obsession");
        int obsessionLikliness = data.Statistics.SelfObsession * 2;
        int random = Random.Range(1, SelfObsessionChance);
        if (obsessionLikliness > random)
        {
            data.Statistics.SelfObsession = BumpStat(data.Statistics.SelfObsession);
            data.Statistics.Buzz = BumpStat(data.Statistics.SelfObsession);
            data.Statistics.Probo = BumpStat(data.Statistics.SelfObsession);
            if (data.Statistics.GigSpeedDays >0)
                data.Statistics.GigSpeedDays--;
            data.Statistics.HitRate = BumpStat(data.Statistics.HitRate);
            
            ChuckleHubManager.Instance.AddToGigSummary("they had a menty-b after the show, and have changed...");
            ChuckleHubManager.Instance.UpdateComic(data);
        }
    }
    
    public int BumpStat(int stat)
    {
        if (stat == 5) return 5;
        else return stat+1;
    }
}
