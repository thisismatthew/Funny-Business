using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PayoutCalculation : GigEvent
    {
        private ComedianData MyData;

        private void Start()
        {
            MyData = GetComponent<ComedianData>();
        }

        public void OnDayComplete()
        {
            int randomNum = Random.Range(1, 100);
            if (randomNum <= MyData.Statistics.HitRatePercentage)
            {
                Debug.Log("Payout!!");
            }
        }
    }

