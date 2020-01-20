/*Fiona Shyne 
Manages building actions, toggles, and timers 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    //set up global varriables 
    public Text main_title; 
    public GameObject blank_toggle; 
    public Transform Canvas; 
    public static GameObject blank_toggle2; 
    public static Transform Canvas2; 
    public static string build_region; 
    public static string selected_energy_name; 
    public static int selected_energy_level;
    public static int selected_cost; 
    public static int selected_energy_increase;

    // Start is called before the first frame update
    void Start()
    {
        //make a copy of gambobjects to use later 
        blank_toggle2 = blank_toggle;
        Canvas2 = Canvas;

        //display the country of interest 
        main_title.text = God.selected_region; 

        //make the blank toggle inactive 
        blank_toggle.SetActive(false);
        reset_toggles();
    }
    
    //create toggle objects for each type of energy researched 
    public static void reset_toggles(){
        int offset = 0;

        //cycle through research dictionary and create a toggle for each type that has some research
        foreach (KeyValuePair<string, int> i in God.research_levels){
            if (i.Value != 0){
                //create new toggle 
                GameObject new_toggle = Instantiate(blank_toggle2, Canvas2, true);
                new_toggle.SetActive(true);  

                //define toggle script and initalize important varriables 
                BuildingToggle new_toggle_script = new_toggle.GetComponent<BuildingToggle>(); 
                new_toggle_script.energy_name = i.Key;
                new_toggle_script.energy_level = i.Value; 
                new_toggle_script.y_offset = offset;
                new_toggle_script.initalize();
                
                offset -= 45;
            }
        }

    }

    //returns true if player has enough money and energy to create plant
    public static bool can_afford(int cost, int energy_increase){
        if(God.total_money < cost){
            return (false);
        } else if(God.current_energy_needs + energy_increase >= God.world_energy_production) 
        {
            return(false);
        }else{
            return(true);
        }
    }

    //create a timer to build plant
    public static void start_timer(string energy_name, int energy_level, int cost, int energy_increase){
        if (can_afford(cost, energy_increase)){
            God.added_energy_needs += energy_increase; 
            string random_sequence = Random.value.ToString(); //add to end to avoid conflicting names
            string timer_name = energy_name + "-" + energy_level.ToString() + "-" + build_region + "-" + random_sequence;
            GameTime.build_timer.Add(timer_name, God.build_wait);
            God.total_money -= cost;
        }
    }

    //parse timer name to add correct energy plant to correct region 
    public static void timer_finished(string name){
        //parse timer name 
        Debug.Log("build wait finished" + name);
        string[] elements = name.Split('-'); 
        string energy_name = elements[0]; 
        int energy_level = int.Parse(elements[1]);
        string region = elements[2];
        
        // when building is finished reset energy needs
        int[] energy_array = God.energy_build_energy_increase[energy_name];
        int energy_increase = energy_array[energy_level];
        God.added_energy_needs -= energy_increase;

        //display notification 
        God.notification_title = "Plant Built";
        God.notification_subtitle = energy_name + " " + energy_level + " built in " + region;
        Notification.show_notification();

        //buy the energy plant 
        God.buy_energy_plant(energy_name, energy_level, region);
        
    }

    //runs submit is clicked, starts build timer, return to main 
    public void submit_clicked(){
        
        if(selected_energy_name != "none"){//only start timer if energy selected
            build_region = God.selected_region;
            start_timer(selected_energy_name, selected_energy_level, selected_cost, selected_energy_increase);
            God.static_back_to_main();
         }
    }
}
