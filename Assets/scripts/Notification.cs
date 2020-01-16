/*Fiona Shyne 
Manages notification button 
Turns notification button active and inactive throughouht game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    //set up gameobjects to be assigned in ui
    public GameObject button;
    public static GameObject button_copy;
    public static NotifcationButton button_script;

    //copy button object and turn button off at start of game 
    void Start()
    {
        button_copy = button;
        button_script = button_copy.GetComponent<NotifcationButton>();
        button.SetActive(false);
    }

    //when other script triggers new notification, show notification button
    public static void show_notification(){
        button_copy.SetActive(true);
        button_script.set_text();
    }

    //when the button is clicked turn notification button off 
    public static void on_click(){
        button_copy.SetActive(false);
    }


}
