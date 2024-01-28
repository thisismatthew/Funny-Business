using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    var emailManager; 
    var chuckleHub;

    void Start()
    {
        emailManager = GameObject.Find("Window - Email App").GetComponent<EmailManager>();
        chuckleHub = GameObject.Find("Window - Email App").GetComponent<ChuckleHubManager>();
    }

    void runOtherEvents()
    {
        // Ego death
        
    }
}
