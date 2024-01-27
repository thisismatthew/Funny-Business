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
    // public GameObject EmailManager = GameObject.FindWithTag("EmailManagers");


    public Image bckground;

    void Start()
    {

        
        txtFrom.fontStyle = FontStyles.Underline | FontStyles.Bold;
        txtSubject.fontStyle = FontStyles.Underline | FontStyles.Bold;
        txtTime.fontStyle = FontStyles.Underline | FontStyles.Bold;
    }

    // Completed
    // public void Update()
    // {
    //     // Set email as focused if selected
    //     if (EmailManager.Instance.CurrentId == iconID & !focused) {
    //         focused = true;
    //         SetFocused();
    //     }

    //     if (EmailManager.Instance.CurrentId != iconID & focused) {
    //         focused = false;
    //         SetRead();
    //     }
    // }


    public void OnPointerClick(PointerEventData eventData)
    {
        EmailManager.Instance.SetEmailId(iconID);
        SetRead();
    }

    private void SetFocused()
    {
        txtFrom.fontStyle = FontStyles.Bold;
        txtSubject.fontStyle = FontStyles.Bold;
        txtTime.fontStyle = FontStyles.Bold;
        bckground.color = new Color(77, 171, 214);
    }

    private void SetRead()
    {
        txtFrom.fontStyle = FontStyles.Normal;
        txtSubject.fontStyle = FontStyles.Normal;
        txtTime.fontStyle = FontStyles.Normal;
        bckground.color = new Color(222, 222, 222);
    }
}

