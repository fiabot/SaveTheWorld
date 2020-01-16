/*Fiona Shyne

Manage single building toggle 
Contain energy name and level information 
Respond to user input 

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingToggle : MonoBehaviour
{
    //set up varriables 
    public string energy_name; 
    public int energy_level; 
    public int cost; 
    public int energy_increase;
    public int y_offset; 
    Transform child;
    public Text title; 
    public Toggle this_toggle; 
    public Button submit_button;

    public Text title_text;
    public Text cost_text; 
    public Text energy_cost_text; 
    public Text energy_prod_text; 
    public Text co2_prod_text;

    // Start is called before the first frame update
    void Start()
    {
        //set up and copy variables 
        child = this.gameObject.transform.GetChild(0);
        title = child.GetComponent<Text>();
        this_toggle =  GetComponent<Toggle>();

        this_toggle.onValueChanged.AddListener(delegate {
            TaskOnClick(this_toggle);
        });        

    }

    //update energy and cost information, apply offset 
    public void initalize(){ 
        this_toggle =  GetComponent<Toggle>();

        int[] cost_array = God.energy_build_cost[energy_name];
        cost = cost_array[energy_level];

        int[] energy_array = God.energy_build_energy_increase[energy_name];
        energy_increase = energy_array[energy_level];
        
        child = this.gameObject.transform.GetChild(1);//TODO, figure out why the fuck c doesn't know what title is 
        title = child.GetComponent<Text>();
        title.text = energy_name + ' ' + energy_level;

        transform.position = transform.position + new Vector3(0,y_offset,0);

    }
    
    //send info to Building when clicked 
    public void TaskOnClick(Toggle toggle){
        if(toggle.isOn){

            //update selected variables in building
            Building.selected_energy_name = energy_name; 
            Building.selected_energy_level = energy_level; 
            Building.selected_cost = cost; 
            Building.selected_energy_increase = energy_increase;

            //makes submit button interactable only if player can afford it 
            if (Research.can_afford(cost,energy_increase)){
                submit_button.interactable = true;

            }else{
                submit_button.interactable = false;

            }

            //find values 
            int[] energy_array = God.energy_production_by_name[energy_name];
            int[] co2_array = God.co2_production_by_name[energy_name];
            int energy_produced = energy_array[energy_level];
            int co2_produced = co2_array[energy_level];

            //display info 
            title_text.text = energy_name + " level " + energy_level;
            cost_text.text = "Cost: " + cost; 
            energy_cost_text.text = "Energy used: " + energy_increase; 
            energy_prod_text.text = "Energy produced: " + energy_produced;
            co2_prod_text.text = "Co2 produced: " + co2_produced;


        }
       
    }
}
