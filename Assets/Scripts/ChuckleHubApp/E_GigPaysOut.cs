using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GigPaysOut : MonoBehaviour, IGigEvent
{
    private ComedianData activeData;
    
    public void RunEvent(ComedianData data)
    {
        // For testing
        var emailManager = GameObject.Find("Window - Email App").GetComponent<EmailManager>();
        activeData = data;
        int randomPercentage = Random.Range(0, 4);
        if (randomPercentage < data.Statistics.HitRate)
        {
            TriggerPayout();
            emailManager.GeneratedEmail(emailManager.positiveEmail);
            emailManager.RefreshEmails();
            // End testing
            return;
        } 
        
        ChuckleHubManager.Instance.AddToGigSummary(data.name + " flopped...");
        emailManager.GeneratedEmail(emailManager.negativeEmail);
        emailManager.RefreshEmails();
    }   
    

    public void TriggerPayout()
    {
        int payout = activeData.Statistics.Buzz * Random.Range(5, 25);
        ChuckleHubManager.Instance.AddToGigSummary(activeData.name + " had a banger! They earned you a cut of $" + payout);
        FindObjectOfType<BankApp>().money += payout;

    }

    //TODO make these a little more bespoke
    Dictionary<string, string> positiveEmail = new Dictionary<string, string>
    {
        ["from"] = "Bonk.Donson@comedyenjoyer.com",
        ["subject"] = "A Night to Remember - Thank You for the Laughter!",
        ["body"] = @"
    Dear Comedy Show Team,

    I just wanted to take a moment to express my heartfelt appreciation for the incredible show last Friday. It was, without a doubt, one of the most enjoyable evenings I've had in a long time.

    From the moment the first comedian stepped onto the stage, I was hooked. The jokes were fresh, the storytelling was impeccable, and the atmosphere was just perfect. It's not often that you find a show where every act is as strong as the last, but you guys managed it flawlessly.

    I'm still laughing at some of the jokes! It was a much-needed escape from the everyday hustle and bustle, and I cannot thank you enough for that. The way you interacted with the audience made us feel like we were a part of the show, creating a truly unique experience.

    Thank you again for a wonderful night. I can't wait to see what you have in store for your next show, and rest assured, I'll be there, front and center!

    Warm regards,
    Bonk"
    };

    Dictionary<string, string> negativeEmail = new Dictionary<string, string>
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
