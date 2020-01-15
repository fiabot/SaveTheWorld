/*Fiona Shyne 
Manages building actions, buttons, and timers 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    //set up global varriables 
    public Text main_title; 
    public GameObject blank_button; 
    public Transform Canvas; 
    public static GameObject blank_button2; 
    public static Transform Canvas2; 
    public static string build_region; 

    // Start is called before the first frame update
    void Start()
    {
        //make a copy of gambobjects to use later 
        blank_button2 = blank_button;
        Canvas2 = Canvas;

        //display the country of interest 
        main_title.text = God.selected_region; 

        //make the blank button inactive 
        blank_button.SetActive(false);
        reset_buttons();
    }
    
    //create button objects for each type of energy researched 
    public static void reset_buttons(){
        int offset = 0;

        //cycle through research dictionary and create a button for each type that has some research
        foreach (KeyValuePair<string, int> i in God.research_levels){
            if (i.Value != 0){
                //create new button 
                GameObject new_button = Instantiate(blank_button2, Canvas2, true);
                new_button.SetActive(true);  

                //define button script and initalize important varriables 
                BuildingButton new_button_script = new_button.GetComponent<BuildingButton>(); 
                new_button_script.energy_name = i.Key;
                new_button_script.energy_level = i.Value; 
                new_button_script.y_offset = offset;
                new_button_script.initalize();
                
                offset -= 35;
            }
        }

    }

    //returns true if player has enough money and energy to create plant
    public static bool can_afford(int cost, int energy_increase){
        if(God.total_money < cost){
            return (false);
        } else if(God.current_energy_needs + energy_increase > God.world_energy_production) 
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

    //runs when button is clicked, starts build timer
    public static void button_clicked(string energy_name, int energy_level, int cost, int energy_increase){
        build_region = God.selected_region;
        start_timer(energy_name, energy_level, cost, energy_increase);
        
    }
}
