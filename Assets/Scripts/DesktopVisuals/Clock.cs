using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float HourTickTime = 5f;

    private float tickTimer = 0;

    public TextMeshProUGUI TimeDisplay;

    private int HoursPassed = 9;

<<<<<<< HEAD
    public int AllHours = 0;
    private EmailManager emailManager;
=======
    public Animator TransitionAnimator;
>>>>>>> e7386c4d8032245e8b65d21a2b5faebd5798c25a
    // Start is called before the first frame update
    void Start()
    {
        emailManager = GameObject.Find("Window - Email App").GetComponent<EmailManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tickTimer >= HourTickTime)
        {
            HoursPassed++;
            AllHours++;
            if (HoursPassed > 17)
            {
                TransitionAnimator.Play("ShutDown");
                ChuckleHubManager.Instance.EndDay();
                HoursPassed = 9;
            }

             // Testing
            if (AllHours == 10)
            {
                Debug.Log("RUNNING TEST");
            emailManager.GeneratedEmail(emailManager.positiveEmail);
            }

            emailManager.RefreshEmails();

            tickTimer = 0;
        }

        tickTimer += Time.deltaTime;
        TimeDisplay.text = HoursPassed + ":00";
    }

    public string FormattedTime()
    {
        // Get a random minute value
        int randomNumber = UnityEngine.Random.Range(1, 60); // Generates a number between 1 and 59
        string randomMinute = randomNumber.ToString("D2");

        // Get day and time
        string displayTime = HoursPassed + ":" + randomMinute;
        int day = (AllHours / 9) + 1;

        return $"{displayTime} {day.ToString()}/3";
    }
}
