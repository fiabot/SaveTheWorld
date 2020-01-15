using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifcationButton : MonoBehaviour
{
    public Text title;
    public Text subtitle;
    Button this_button;
    
    // Start is called before the first frame update
    void Start()
    {
        title.text = God.notification_title; 
        subtitle.text = God.notification_subtitle;

        this_button =  GetComponent<Button>();

        this_button.onClick.AddListener(Notification.on_click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
