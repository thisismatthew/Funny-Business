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

    public Animator TransitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tickTimer >= HourTickTime)
        {
            HoursPassed++;
            if (HoursPassed > 17)
            {
                TransitionAnimator.Play("ShutDown");
                ChuckleHubManager.Instance.EndDay();
                HoursPassed = 9;
            }

            tickTimer = 0;
        }

        tickTimer += Time.deltaTime;
        TimeDisplay.text = HoursPassed + ":00";
    }
}
