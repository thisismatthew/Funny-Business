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
        numberOfRows = emailCsv.First().Value.Count;
        email_text.text = "";
        emailCsv = TSVReader.ReadTSV("emails.txt");
        emailCsv["loadedAlready"] = Enumerable.Repeat("FALSE", numberOfRows).ToList();
        RefreshEmails(emailCsv);
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
        Debug.Log(idIndex.ToString());
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
    public void AdvanceTime(int currentTime)
    {
        currentTime++;
        SendEmail("aa");
        SendEmail("ab");
        SendEmail("ac");
        SendEmail("ad");
        SendEmail("ae");
        SendEmail("af");
        SendEmail("ag", true);
    }

    void RefreshEmails(Dictionary<string, List<string>> emails)
    {
        int numberOfRows = emails.First().Value.Count;

        for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            if (emails["conditions"][rowIndex] == "TRUE" & emails["loadedAlready"][rowIndex] == "FALSE"){
                spawnEmail(emails, rowIndex);
            }
        }

        return;
    }

    void spawnEmail(Dictionary<string, List<string>> listEmails, int rowIndex)
    {
        Debug.Log("Created item.");
        GameObject newEmailDisplay = Instantiate(emailDisplay, new Vector3(0, 0, 0), Quaternion.identity);
        newEmailDisplay.transform.SetParent(contentTransform, false);
        IconClickHandler script = newEmailDisplay.GetComponent<IconClickHandler>();
        script.iconID = listEmails["id"][rowIndex];
        script.txtFrom.text = listEmails["from"][rowIndex];
        script.txtSubject.text = listEmails["subject"][rowIndex];
        script.txtTime.text = listEmails["msgtime"][rowIndex];
        listEmails["loadedAlready"][rowIndex] = "TRUE";

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
            RefreshEmails(emailCsv);
        }
    }
}

