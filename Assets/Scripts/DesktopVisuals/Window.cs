using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Window : MonoBehaviour
{
    public bool isOpen = false;
    public Vector2 OpenPosition;

    public void BringToFocus()
    {
        // ideally you click on one and it gets bumped into the top layer...
        // but I don't want to get distracted with this yet.
    }
    
    public void OpenWindow()
    {
        Debug.Log(gameObject.name + "opened");
        isOpen = true;
        transform.position = OpenPosition;
        transform.DOScale(1, .2f);
    }

    public void CloseWindow()
    {
        Debug.Log(gameObject.name + "closed");
        isOpen = false;
        transform.DOScale(0, .2f);
    }

    private void MoveWindow()
    {
        
    }
}
