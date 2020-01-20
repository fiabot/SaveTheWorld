/*Fiona Shyne
Manage policy page and decisions 
Create policy toggles 
Enact or reject policies based on popularity

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyPage : MonoBehaviour
{
    //set up global variables 
    public Text main_title; 
    public GameObject blank_slider;
    public Transform Canvas;
    public static GameObject blank_slider_copy;
    public static Transform Canvas_copy;
    public List<PolicySlider> sliders = new List<PolicySlider>();
    public Text energy_text; 
    public Text co2_text;

    // initalize important variables 
    void Start()
    {
        //main_title.text = God.selected_region;
        blank_slider_copy = blank_slider;
        Canvas_copy = Canvas;
        blank_slider.SetActive(false);
        sliders.Clear();
        create_sliders();
        

    }
    // create new slider with giving name and level 
    public void new_slider(string name, int level, int offset){
        GameObject new_slider = Instantiate(blank_slider_copy,Canvas_copy, true);
        new_slider.SetActive(true);  
        PolicySlider new_slider_script = new_slider.GetComponent<PolicySlider>(); 
        sliders.Add( new_slider_script);
        new_slider_script.name = name;
        new_slider_script.level = level;
        new_slider_script.y_offset = offset;
        new_slider_script.initalize();
    }

    //determine which sliders to create and create them 
    public void create_sliders(){
        int offset = 0; 
        Debug.Log(God.research_levels);
        //repeate for each type of research 
        foreach (KeyValuePair<string, int> i in God.research_levels){
            //for each level that has been researched, add a slider
            Debug.Log(i.Value);
            if (i.Value >= 1 ){
                new_slider(i.Key, 1, offset);
                offset -= 65;
            }

            if (i.Value >= 2){
                new_slider(i.Key, 2, offset);
                offset -= 65;
            }

            if (i.Value >= 3){
                new_slider(i.Key, 3, offset);
                offset -= 65;
            }
            

        }
        
    }
    //use popularity to randomly determine if the policy passes 
    public bool policy_passes(){
        double chance = Random.value - 0.3;
        if (chance < God.current_popularity){
            main_title.text = "policy passes";
            return true; 
        }
        else{
            main_title.text = "policy does not pass";
            return false;
        }

    }

    //determine if policy passes and if so restricts energy type 
    public void button_clicked(){
        if(God.can_policy){
            GameTime.policy_timer.Add("Wait", God.policy_wait);
            God.can_policy = false;
            if (policy_passes()){
                foreach(PolicySlider i in sliders){
                    string name = i.name; 
                    int level = i.level;
                    float restriction = i.get_value();
                    God.restrict_energy(name, level,  restriction);
                }

            }
        }
        
    }

    public static void timer_finished(string name){
        //Debug.Log("policy wait time finished");
        God.can_policy = true;
    }

    // update predicted co2 and energy levels 
    void Update()
    {
        int[] change = {0,0};//first index is energy, second co2  
        foreach(PolicySlider i in sliders){
            string name = i.name; 
            int level = i.level;
            float restriction = i.get_value();
            int[] slider_change = God.restriction_change(name, level, restriction);
            change[0] += slider_change[0]; 
            change[1] += slider_change[1];
        } 

        int energy_effect = God.world_energy_production + change[0]; 
        int co2_effect = God.world_co2_production + change[1];

        energy_text.text = "Energy:" +  energy_effect.ToString(); 
        co2_text.text = "Co2:" + co2_effect.ToString();


    }
}
