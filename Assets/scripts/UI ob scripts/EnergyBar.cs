/*Fiona 
Set heights on energy bar 
set text and position of energy bar number displays 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image total_energy_image; 
    public Image added_energy_image;
    public Image min_energy_image;
    public float total_energy_display;// the amount of energy units the bar can display
    public Text total_suplus;
    public Text min_energy_text;
    public float bar_height;
    float surplus_offset;
    float min_energy_offset;
    // Start is called before the first frame update
    void Start()
    {
        total_energy_display = 100; 
        surplus_offset = 0;
        min_energy_offset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if amount of energy produced is greater then  set size, show every thing else as a percentage of total energy
        if (God.world_energy_production >= total_energy_display){
            total_energy_image.fillAmount = 1; 
            added_energy_image.fillAmount = (float) God.current_energy_needs / (float) God.world_energy_production; 
            min_energy_image.fillAmount =  (float) God.min_energy_needs / (float) God.world_energy_production; 

            //move and set surplus text 
            total_suplus.text = God.current_surplus.ToString();
            total_suplus.transform.position = new Vector3(80,90 + bar_height,0);

            //move and set min energy  text 
            min_energy_text.text = God.current_energy_needs.ToString();
            min_energy_offset = ((float) God.current_energy_needs / (float) God.world_energy_production) * bar_height;
            min_energy_text.transform.position = new Vector3(140,80 + min_energy_offset,0);

        }else{ //otherwise set all to percentage of total room
            total_energy_image.fillAmount = (float) God.world_energy_production /  total_energy_display; 
            added_energy_image.fillAmount = (float) God.current_energy_needs / total_energy_display; 
            min_energy_image.fillAmount =  (float) God.min_energy_needs / total_energy_display;

            //move and set surplus text 
            total_suplus.text = God.current_surplus.ToString();
            surplus_offset = ((float) God.world_energy_production / total_energy_display) *(float) bar_height;
            total_suplus.transform.localPosition = new Vector3(-55,-245 + surplus_offset,0);      

            //move and set mine energy  text 
            min_energy_text.text = God.current_energy_needs.ToString();
            min_energy_offset = ((float) God.current_energy_needs / total_energy_display) * bar_height;
            min_energy_text.transform.localPosition = new Vector3(55, -255 + min_energy_offset,0);   
    
        }
    }
}
