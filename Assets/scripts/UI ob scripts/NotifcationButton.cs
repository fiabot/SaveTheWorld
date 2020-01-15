/*Fiona Shyne
Controls notification button
interacts with Notification Manager with user input 
Set text on button to be varriables from god

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifcationButton : MonoBehaviour
{
    //objects set up in ui 
    public Text title;
    public Text subtitle;
    Button this_button;
    
    //Change text and add onclick function 
    void Start()
    {
        title.text = God.notification_title; 
        subtitle.text = God.notification_subtitle;

        this_button =  GetComponent<Button>();

        this_button.onClick.AddListener(Notification.on_click);
    }

}
