using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.Serialization;

public class Clock : MonoBehaviour
{
    [FormerlySerializedAs("HourTickTime")] public float TickTime = 5f;

    private float tickTimer = 0;

    public TextMeshProUGUI TimeDisplay, DayDisplay;

    public Window WinWindow, LoseWindow;

    private int HoursPassed = 9;
    private int QuarterHourPassed = 0;

    private bool HourTickUp = false;
    private bool done = false;


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

    public void EndDayEarly()
    {
        TickTime = 0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (day > 14)
        {
            if (!done) CheckFinalFunds();
            done = true;
            return;
        }
        if (tickTimer >= TickTime)
        {
            QuarterHourPassed++;
            if (QuarterHourPassed >= 4)
            {
                HourTickUp = true;
                QuarterHourPassed = 0;
            }

            tickTimer = 0;
        }
        if (HourTickUp)
        {
            HoursPassed++;
            AllHours++;
            day = (AllHours / 9) + 1;
            dayOfWeek = day % 7;
            if (HoursPassed > 17)
            {
               // Debug.Log(gameObject.name);
                TransitionAnimator.Play("ShutDown");
                ChuckleHubManager.Instance.EndDay();
                HoursPassed = 9;
                //Debug.Log($"Today is {DayOfWeekName()}");
                TickTime = 4;
            }

            emailManager.RefreshEmails();

            HourTickUp = false;
        }

        tickTimer += Time.deltaTime;
        TimeDisplay.text = HoursPassed + ":" + (QuarterHourPassed * 15).ToString("D2");
        DayDisplay.text = DayOfWeekName() + " " + day;
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
            case 1: return "Mon";
            case 2: return "Tue";
            case 3: return "Wed";
            case 4: return "Thu";
            case 5: return "Fri";
            case 6: return "Sat";
            case 7: return "Sun";
            default: throw new WarningException("Day number must be between 1 and 7.");
        }
    }

    public void CheckFinalFunds()
    {
        FindObjectOfType<BankApp>().GetComponent<Window>().OpenWindow();
        
        if (FindObjectOfType<BankApp>().money >= 1200)
        {
            WinWindow.OpenWindow();
        }
        else
        {
            LoseWindow.OpenWindow();
        }
    }
}
