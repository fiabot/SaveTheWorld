/*Fiona Shyne 
Research toggle object 
maintains data for research toggle
Trigger Research toggle responder

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchToggle : MonoBehaviour
{
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

    public ToggleGroup toggle_group;


    // Start is called before the first frame update
    void Start()
    {
        //set up variables 
        child = this.gameObject.transform.GetChild(1);
        title = child.GetComponent<Text>();
        this_toggle =  GetComponent<Toggle>();

        //this_toggle.Group = toggle_group;
        DontDestroyOnLoad(gameObject);

         this_toggle.onValueChanged.AddListener(delegate {
            TaskOnClick(this_toggle);
        }); 
    }

    //set up important varriables and set appropriate interactability for toggle
    public void initalize(){ 
        int[] cost_array = God.energy_research_cost[energy_name];
        cost = cost_array[energy_level];

        int[] energy_array = God.energy_research_energy_increase[energy_name];
        energy_increase = energy_array[energy_level];

       
        //make not interactable if already at highest level
        if (energy_level == 3){
            this_toggle.interactable = false;
        }
        int next_level = energy_level + 1;
        title.text = energy_name + " " + next_level; 

        transform.position = transform.position + new Vector3(0,y_offset,0);

    }

    //when clicked talk to Research, make toggle uninteractable 
    public void TaskOnClick(Toggle toggle){
         if(toggle.isOn){
            if(energy_level != 3){//only update Research Manager if can research
                //update selected variables in Research
                Research.selected_energy_name = energy_name; 
                Research.selected_energy_level = energy_level; 
                Research.selected_cost = cost; 
                Research.selected_energy_increase = energy_increase;

                //makes submit button interactable only if player can afford it 
                if (Research.can_afford(cost,energy_increase)){
                    submit_button.interactable = true;

                }else{
                    submit_button.interactable = false;

                }

                //find values 
                int[] energy_array = God.energy_production_by_name[energy_name];
                int[] co2_array = God.co2_production_by_name[energy_name];
                int energy_produced = energy_array[energy_level + 1];
                int co2_produced = co2_array[energy_level + 1];

                //display info 
                title_text.text = energy_name + " level " + (energy_level + 1);
                cost_text.text = "Cost: " + cost; 
                energy_cost_text.text = "Energy used: " + energy_increase; 
                energy_prod_text.text = "Energy produced: " + energy_produced;
                co2_prod_text.text = "Co2 produced: " + co2_produced;

            }

        }

    }
}
