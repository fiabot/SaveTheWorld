/*Fiona Shyne
Manages research 
create research toggles 
Trigger and respond to research timers 
updates research levels

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research: MonoBehaviour
{
    public GameObject blank_toggle; 
    public Transform Canvas;
    ResearchToggle blank_script;
    public static Transform Canvas2;
    public static GameObject blank_toggle2;
    public static bool toggles_active;

    public static string selected_energy_name; 
    public static int selected_energy_level; 
    public static int selected_cost; 
    public static int selected_energy_increase;

    //create toggles and copy varriables 
    void Start()
    {
        //copy variables that can be used later 
        blank_toggle2 = blank_toggle;
        Canvas2 = Canvas;
        blank_toggle.SetActive(false);
        blank_script = blank_toggle.GetComponent<ResearchToggle>();
        blank_script.energy_name = "This is not a toggle";
        reset_toggles();

        selected_energy_name = "none";
    }

    //display toggles according to current research level
    public static void reset_toggles(){
        int offset = 0;
        int count = 0; //Allow for two new energy to be researched at a time
        foreach (var i in God.energy_names){
            GameObject new_toggle = Instantiate(blank_toggle2, Canvas2, true);
            if(toggles_active){
                new_toggle.SetActive(true);
            }
             
            ResearchToggle new_toggle_script = new_toggle.GetComponent<ResearchToggle>(); 
            new_toggle_script.energy_name = i;
            new_toggle_script.energy_level = God.research_levels[i]; 
            new_toggle_script.y_offset = offset;
            new_toggle_script.initalize();
            if (God.research_levels[i] == 0){
                break;
                /*if(count == 1){
                    break;
                }else{
                    count += 1; 
                }*/
            }
            offset -= 50;
        }

    }

    //subtract money and start a timer for research to start 
    public static void start_timer(string energy_name, int energy_level, int cost, int energy_increase){
        toggles_active = false;
        if (can_afford(cost, energy_increase)){
            God.added_energy_needs += energy_increase; 
            string timer_name = energy_name + "/" + (string) energy_level.ToString();
            GameTime.research_timer.Add(timer_name, God.research_wait);
            God.total_money -= cost;
        }
    } 

    //when timer is finished, parse name and update research 
    public static void timer_finished(string name){
        Debug.Log("research finished:" + name);
        toggles_active = true;
        string[] elements = name.Split('/'); 
        Debug.Log(elements[1]);
        string energy_name = elements[0]; 
        int energy_level = int.Parse(elements[1]);
        
        // when building is finished reset energy needs
        int[] energy_array = God.energy_research_energy_increase[energy_name];
        int energy_increase = energy_array[energy_level];
        God.added_energy_needs -= energy_increase;

        //display notification 
        God.notification_title = "Energy Unlocked";
        int next_level = energy_level + 1;
        God.notification_subtitle = energy_name + " " + next_level + " research complete";
        Notification.show_notification();

        update_research(energy_name, energy_level);
    }

    //return true if player can afford cost and energy increase 
    public static bool can_afford(int cost, int energy_increase){
        if(God.total_money < cost){
            Debug.Log("not enough money");
            Debug.Log(cost);
            Debug.Log(God.total_money);
            return (false);
        } else if(God.current_energy_needs + energy_increase >= God.world_energy_production) 
        {
            Debug.Log("not enough energy");
            return(false);
        }else{
            return(true);
        }
    }

    //increment research level by one when research completed
    public static void update_research(string name, int level){
        God.research_levels[name] = level + 1; 
    }
    
    //runs when toggle is clicked, trigger timer, return to main  
     public void  submit_clicked(){
         if(selected_energy_name != "none"){//only start timer if energy selected
            start_timer(selected_energy_name, selected_energy_level, selected_cost, selected_energy_increase);
            God.static_back_to_main();
         }
        
    }
}

