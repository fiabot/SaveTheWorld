/*Fiona Shyne
Control money button 
Responds to user input 
Hold money value 
When clicked increment total money
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyButton : MonoBehaviour
{
    //set up varriables 
    Button this_button;
    public int value;

    //set up button ob and onclick funtion
    void Start()
    {
        int rand_width = Random.Range(100, Screen.width-100); 
        int rand_height = Random.Range(100, Screen.height-100);
        transform.position = new Vector3(rand_width,rand_height,0);
        this_button =  GetComponent<Button>();

        this_button.onClick.AddListener(TaskOnClick);
        
    }

    //add money when clicked and self destruct 
    void TaskOnClick(){
        God.total_money += value; 
        Destroy(gameObject);
    }
}
