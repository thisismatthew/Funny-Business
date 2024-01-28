using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;


public class EmailManager : MonoBehaviour
{
    public int time = 0;
    public static EmailManager Instance { get; private set; }
    public TextMeshProUGUI email_text;
    public GameObject emailDisplay;
    public Image emailBckground;
    public string CurrentId;
    public Transform contentTransform;
    private Dictionary<string, List<string>> emailCsv;
    int numberOfRows;

    void Start()
    {
        email_text.text = "";
        emailCsv = TSVReader.ReadTSV("fbemails.txt");
        numberOfRows = emailCsv.First().Value.Count;
        emailCsv["loadedAlready"] = Enumerable.Repeat("FALSE", numberOfRows).ToList();
        RefreshEmails();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEmailId(string id)
    {
        CurrentId = id;
        DisplayContentForIcon(CurrentId);
    }

    public void DisplayContentForIcon(string id)
    {
        int idIndex = SearchEmails(emailCsv, id);
        string tmp_text = $@"From: {emailCsv["from"][idIndex]}
Sent: {emailCsv["msgtime"][idIndex]}
To: Manager
Subject: {emailCsv["subject"][idIndex]}

{emailCsv["body"][idIndex]}
    ";

        email_text.text = tmp_text;
    }

    // This runs whenever we need to get new emails.
    // Right now I just use time, I'll add better logic later.
    public void AdvanceTime()
    {
        time++;
        // This is temporary, but this is a public method, can be used by other objects.
        if (time == 1) {
            SendEmail("jackie1");
            SendEmail("jetstar1");
            SendEmail("SICF1");
            SendEmail("slambo1");
            SendEmail("mum1");
        }
        if (time == 2) {
            SendEmail("chuckle2");
            SendEmail("witty1");
            SendEmail("chuckle3");
            SendEmail("mum2");
            SendEmail("mum3");
        }
        Debug.Log($"Time is {time}.");
        
        RefreshEmails();
    }

    void RefreshEmails() {

        for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            if (emailCsv["conditions"][rowIndex] == "TRUE" & emailCsv["loadedAlready"][rowIndex] == "FALSE"){
                spawnEmail(rowIndex);
            }
        }

        return;
    }

    void spawnEmail(int rowIndex)
    {
        GameObject newEmailDisplay = Instantiate(emailDisplay, new Vector3(0, 0, 0), Quaternion.identity);
        newEmailDisplay.transform.SetParent(contentTransform, false);
        IconClickHandler script = newEmailDisplay.GetComponent<IconClickHandler>();
        script.iconID = emailCsv["id"][rowIndex];
        script.txtFrom.text = emailCsv["from"][rowIndex];
        script.txtSubject.text = emailCsv["subject"][rowIndex];
        script.txtTime.text = emailCsv["msgtime"][rowIndex];
        emailCsv["loadedAlready"][rowIndex] = "TRUE";

        return;
    }

    int SearchEmails(Dictionary<string, List<string>> emails, string searchId)
    {
        for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++) {
            if (emails["id"][rowIndex] == searchId) {
                return rowIndex;
            }
        }
        throw new Exception($"Error: id {searchId} not found in email list.");
    }

    public void SendEmail(string idToActivate, bool sendASAP = false)
    {
        int indexToActivate = SearchEmails(emailCsv, idToActivate);
        // Excuse this DISGUSTING behaviour
        emailCsv["conditions"][indexToActivate] = "TRUE";
        if (sendASAP) {
            RefreshEmails();
        }
    }
}

