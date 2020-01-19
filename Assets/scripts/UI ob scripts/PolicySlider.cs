/*Fiona Shyne
Manages policy slider 
hold energy type info and sends data to policypage 


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PolicySlider : MonoBehaviour
{
    public string name; 
    public int max_amount; 
    public int y_offset;
    public static Text title; 
    public static Slider slider;
    public int index;
    public int level; 

    // Define objects 
    void Start()
    {
        title = GetComponent<Text>(); 
        slider = GetComponent<Slider>();
    }

    //Update text and positon 
    public void initalize(){
        title = GetComponent<Text>(); 
        
        title.text = name + " " + level.ToString(); 
        transform.position = transform.position + new Vector3(0,y_offset,0);

        slider = GetComponent<Slider>(); 
        slider.value = God.energy_restrictions[name + "/" + level.ToString()];

    }

    //Resturn value of slider
    public float get_value(){
        slider = GetComponent<Slider>();
        return slider.value;

    }

}
