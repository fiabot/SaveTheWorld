/*Fiona Shyne 
Manage money button 
Has awarness of money button 
Add new money button when money is awarded 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    //set up varriables 
    public GameObject blank_money_button;
    public static GameObject blank_money_button_copy;
    public Transform canvas; 
    public static Transform canvas_copy;

    //Make a copy of dummy objects to be used later in script
    void Start()
    {
        blank_money_button_copy = blank_money_button;
        blank_money_button.SetActive(false);
        canvas_copy = canvas;
    }

    //runs when God triggers it, create a new money button 
    public static void create_button(int amount){
        GameObject new_button = Instantiate(blank_money_button_copy, canvas_copy, true);
        new_button.SetActive(true);  
        MoneyButton new_button_script = new_button.GetComponent<MoneyButton>();
        new_button_script.value = amount; 
    
    }
}
