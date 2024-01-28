using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public float HourTickTime = 5f;

    private float tickTimer = 0;

    public TextMeshProUGUI TimeDisplay;

    private int HoursPassed = 9;


    public int AllHours = 0;
    int day = 1;
    int dayOfWeek = 1;
    private EmailManager emailManager;
    public Animator TransitionAnimator;
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
            day = (AllHours / 9) + 1;
            dayOfWeek = day % 7;
            if (HoursPassed > 17)
            {
                TransitionAnimator.Play("ShutDown");
                ChuckleHubManager.Instance.EndDay();
                HoursPassed = 9;
                Debug.Log($"Today is {DayofWeekName()}");
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

        // Get time
        string displayTime = HoursPassed + ":" + randomMinute;

        return $"{displayTime} {day.ToString()}/3";
    }

    public string DayOfWeekName()
    {
        switch(dayOfWeek)
        {
            case 1: return "Monday";
            case 2: return "Tuesday";
            case 3: return "Wednesday";
            case 4: return "Thursday";
            case 5: return "Friday";
            case 6: return "Saturday";
            case 7: return "Sunday";
            default: throw new Exception("Day number must be between 1 and 7.");
        }
    }
}
