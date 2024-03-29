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
    //public Image emailBckground;
    public string CurrentId;
    public Transform contentTransform;
    private Dictionary<string, List<string>> emailCsv;
    public TextAsset emailTextAsset;
    int numberOfRows;
    private Clock clock;

    void Start()
    {
        // Link up to clock
        clock = GameObject.Find("Clock").GetComponent<Clock>();

        // Load in the premade emails
        email_text.text = "";
        emailCsv = TSVReader.ReadTSV(emailTextAsset);
        numberOfRows = emailCsv.First().Value.Count;
        emailCsv["loadedAlready"] = Enumerable.Repeat("FALSE", numberOfRows).ToList();

        // Get emails
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
        int idIndex = SearchEmails(id);
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
    // public void AdvanceTime()
    // {
    //     time++;
    //     // This is temporary, but this is a public method, can be used by other objects.
    //     if (time == 1) {
    //         SendEmail("jackie1");
    //         SendEmail("jetstar1");
    //         SendEmail("SICF1");
    //         SendEmail("slambo1");
    //         SendEmail("mum1");
    //     }
    //     if (time == 2) {
    //         SendEmail("chuckle2");
    //         SendEmail("witty1");
    //         SendEmail("chuckle3");
    //         SendEmail("mum2");
    //         SendEmail("mum3");
    //     }
    //     Debug.Log($"Time is {time}.");
        
    //     RefreshEmails();
    // }

    public void RefreshEmails() {

        for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            if (emailCsv["conditions"][rowIndex] == clock.AllHours.ToString() & emailCsv["loadedAlready"][rowIndex] == "FALSE"){
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
        script.txtTime.text = clock.FormattedTime();
        emailCsv["loadedAlready"][rowIndex] = "TRUE";

        return;
    }

    int SearchEmails(string searchId)
    {
        for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++) {
            if (emailCsv["id"][rowIndex] == searchId) {
                return rowIndex;
            }
        }

        return -1;
    }

    // public void SendEmail(string idToActivate)
    // {
    //     int indexToActivate = SearchEmails(idToActivate);
    //     // Excuse this DISGUSTING behaviour
    //     emailCsv["conditions"][indexToActivate] = clock.AllHours + 1;
    // }

    public void GeneratedEmail(Dictionary<string, string> newEntry)
    {
        // Just checking that we have the right keys
        var requiredKeys = new HashSet<string> { "from", "subject", "body" };

        bool containsOnlyRequiredKeys = newEntry.Count == requiredKeys.Count && 
                                        requiredKeys.All(key => newEntry.ContainsKey(key));
        if (!containsOnlyRequiredKeys) {
            throw new Exception("The dictionary does not just have the required keys");
        }
        
        // Add to the email data structure
        foreach (KeyValuePair<string, string> kvp in newEntry)
        {
            emailCsv[kvp.Key].Add(kvp.Value);
        }
        
        // Create placeholder time
        emailCsv["msgtime"].Add(clock.FormattedTime());

        // Set loaded already
        emailCsv["loadedAlready"].Add("FALSE");

        // Create a unique ID
        // Terrible hack but running out of time.
        string tempID = newEntry["from"] + newEntry["subject"] + UnityEngine.Random.Range(1, 1000000).ToString();
        emailCsv["id"].Add(tempID);
        
        // Set send time as shortly in future to send.
        emailCsv["conditions"].Add((clock.AllHours + 1).ToString());

        // Increment number of rows
        numberOfRows++;

        //Debug.Log("Run test");
        //Debug.Log($"New index {numberOfRows - 1}, total length {numberOfRows}.");

        //Debug.Log($"New index {numberOfRows - 1}, total length {numberOfRows}.");

        foreach (KeyValuePair<string, List<string>> kvp in emailCsv)
        {
           // Debug.Log($"{kvp.Key}: ");
        }
    }

    public Dictionary<string, string> positiveEmail = new Dictionary<string, string>
    {
        ["from"] = "jane.doe@example.com",
        ["subject"] = "A Night to Remember - Thank You for the Laughter!",
        ["body"] = @"
    Dear Comedy Show Team,

    I just wanted to take a moment to express my heartfelt appreciation for the incredible show last Friday. It was, without a doubt, one of the most enjoyable evenings I've had in a long time.

    From the moment the first comedian stepped onto the stage, I was hooked. The jokes were fresh, the storytelling was impeccable, and the atmosphere was just perfect. It's not often that you find a show where every act is as strong as the last, but you guys managed it flawlessly.

    I'm still laughing at some of the jokes! It was a much-needed escape from the everyday hustle and bustle, and I cannot thank you enough for that. The way you interacted with the audience made us feel like we were a part of the show, creating a truly unique experience.

    Thank you again for a wonderful night. I can't wait to see what you have in store for your next show, and rest assured, I'll be there, front and center!

    Warm regards,
    Jane Doe"
    };

    public Dictionary<string, string> negativeEmail = new Dictionary<string, string>
    {
        ["from"] = "jane.doe@example.com",
        ["subject"] = "Disappointed by Last Night's Show",
        ["body"] = @"
    Dear Comedy Show Team,

    I am writing to express my disappointment with the show I attended last night. Unfortunately, the experience was far from what I had anticipated.

    To begin with, the performances seemed under-rehearsed and lacking in originality. The jokes were stale and predictable, and it felt like the comedians were struggling to connect with the audience. Humor is subjective, but the content last night was noticeably off the mark.

    Furthermore, the organization of the event itself was subpar. The show started late, the seating was uncomfortable, and the overall ambiance did not meet the standards that I had expected based on your reputation.

    I've always believed in providing constructive feedback, so I hope you take these comments as an opportunity to improve future shows. A comedy show should be a delightful escape, but sadly, last night's performance fell short of delivering that joy and entertainment.

    Thank you for taking the time to read my concerns. I hope to see improvements in your future events.

    Best regards,
    Jane Doe"
    };

}

