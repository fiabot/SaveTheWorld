using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public GameObject button;
    public static GameObject button_copy;
    // Start is called before the first frame update
    void Start()
    {
        button_copy = button;
        button.SetActive(false);
    }
    public static void show_notification(){
        button_copy.SetActive(true);
    }

    public static void on_click(){
        button_copy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
