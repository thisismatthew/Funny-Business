using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RosterPanel : MonoBehaviour
{
    public int gigCountDown = 0;
    public bool OnGig = false;
    public Image ProfilePic;
    public TextMeshProUGUI Name, GigCountDown;
    public Button BookGig, CheckComic;

    private ComedianData currentLoadedComic;
    // Start is called before the first frame update
    void Start()
    {
    
        Name = GetComponentsInChildren<TextMeshProUGUI>()[0];
        GigCountDown =  GetComponentsInChildren<TextMeshProUGUI>()[1];
        BookGig = GetComponentsInChildren<Button>()[0];
        CheckComic =  GetComponentsInChildren<Button>()[1];
    }

    public void LoadNewComic(ComedianData data)
    {
        currentLoadedComic = data;
        ProfilePic.sprite = data.Portrait;
        Name.text = data.Name;
        gigCountDown = data.Statistics.GigSpeedDays;
        GigCountDown.text = "<wave>" + gigCountDown + " days until ready to perform.";
    }

    public void UpdateGigCountdown()
    {
        gigCountDown--;
        if (gigCountDown < 0) gigCountDown = currentLoadedComic.Statistics.GigSpeedDays;
        GigCountDown.text = "<wave>" + gigCountDown + " days until ready to perform.";
        if (gigCountDown == 0)
        {
            BookGig.interactable = true;
        }
        else
        {
            BookGig.interactable = false;
        }
    }

    public void BookComedianOnGig()
    {
        OnGig = true;
        BookGig.interactable = false;
        GigCountDown.text = "<wave> Getting Ready For Gig!";
    }

    public void CheckComedian()
    {
        FindObjectOfType<ChuckleHubManager>().CheckRosteredComic(currentLoadedComic);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
