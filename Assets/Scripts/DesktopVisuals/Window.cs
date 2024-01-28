using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Window : MonoBehaviour
{
    public bool isOpen = false;
    private Vector2 OpenPosition;
    //Todo set open position to the starting position of the window

    private void Start()
    {
        OpenPosition = transform.position;
    }

    public void BringToFocus()
    {
        // ideally you click on one and it gets bumped into the top layer...
        // but I don't want to get distracted with this yet.
    }
    
    public void OpenWindow()
    {
        isOpen = true;
        transform.position = OpenPosition;
        transform.DOScale(1, 0.2f);
    }

    public void CloseWindow()
    {
        isOpen = false;
        transform.DOScale(0, 0.2f);
    }

    private void MoveWindow()
    {
        
    }
}
