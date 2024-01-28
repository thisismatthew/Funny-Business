using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankApp : MonoBehaviour
{
    public TextMeshProUGUI displayedAmount;
    public int money;

    // Update is called once per frame
    void Update()
    {
        displayedAmount.text = "$" + money;
    }
}
