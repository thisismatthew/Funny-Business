using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    EmailManager emailManager; 
    ChuckleHubManager chuckleHub;

    void Start()
    {
        emailManager = GameObject.Find("Window - Email App").GetComponent<EmailManager>();
        chuckleHub = ChuckleHubManager.Instance;
    }

    void runOtherEvents()
    {
        // Ego death
        
    }
}
