using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public GameObject blank_money_button;
    public static GameObject blank_money_button_copy;
    public Transform canvas; 
    public static Transform canvas_copy;
    // Start is called before the first frame update
    void Start()
    {
        blank_money_button_copy = blank_money_button;
        blank_money_button.SetActive(false);
        canvas_copy = canvas;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void create_button(int amount){
        GameObject new_button = Instantiate(blank_money_button_copy, canvas_copy, true);
        new_button.SetActive(true);  
        MoneyButton new_button_script = new_button.GetComponent<MoneyButton>();
        new_button_script.value = amount; 
    
    }
}
