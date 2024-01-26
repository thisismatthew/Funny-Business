using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IconClickHandler : MonoBehaviour, IPointerClickHandler
{
    public string iconID; // Unique ID for each icon
    public TextMeshProUGUI txtFrom;
    public TextMeshProUGUI txtSubject;
    public TextMeshProUGUI txtTime;


    public Image bckground;

    void Start()
    {
        txtFrom.fontStyle = FontStyles.Underline | FontStyles.Bold;
        txtSubject.fontStyle = FontStyles.Underline | FontStyles.Bold;
        txtTime.fontStyle = FontStyles.Underline | FontStyles.Bold;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        EmailManager.Instance.DisplayContentForIcon(iconID);
        SetRead();
    }

    private void SetRead()
    {
        txtFrom.fontStyle = FontStyles.Normal;
        txtSubject.fontStyle = FontStyles.Normal;
        txtTime.fontStyle = FontStyles.Normal;
        bckground.color = new Color(222, 222, 222);
    }
}

