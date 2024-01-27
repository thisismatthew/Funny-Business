using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGigEvent
{
    public bool CheckRequirements(ComedianData data);
    public void TriggerEvent();
}
