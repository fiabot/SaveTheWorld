using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research: MonoBehaviour
{
    public GameObject blank_button; 
    public Transform Canvas;
    ResearchButton blank_script;
    public static Transform Canvas2;
    public static GameObject blank_button2;
    public static bool buttons_active;

    // Start is called before the first frame update
    void Start()
    {
        //copy variables that can be used later 
        blank_button2 = blank_button;
        Canvas2 = Canvas;
        blank_button.SetActive(false);
        blank_script = blank_button.GetComponent<ResearchButton>();
        blank_script.energy_name = "This is not a button";
        reset_buttons();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
    //display buttons according to current research level
    public static void reset_buttons(){
        int offset = 0;
        foreach (var i in God.energy_names){
            GameObject new_button = Instantiate(blank_button2, Canvas2, true);
            if(buttons_active){
                new_button.SetActive(true);
            }
             
            ResearchButton new_button_script = new_button.GetComponent<ResearchButton>(); 
            new_button_script.energy_name = i;
            new_button_script.energy_level = God.research_levels[i]; 
            new_button_script.y_offset = offset;
            new_button_script.initalize();
            if (God.research_levels[i] == 0){
                break; 
            }
            offset -= 35;
        }

    }
    //subtract money and start a counter for research to start 
    public static void start_counter(string energy_name, int energy_level, int cost, int energy_increase){
        buttons_active = false;
        if (can_afford(cost, energy_increase)){
            God.added_energy_needs += energy_increase; 
            string counter_name = energy_name + " " + (string) energy_level.ToString();
            GameTime.research_counter.Add(counter_name, God.research_wait);
            God.total_money -= cost;
        }
    } 
    //when timer is finished, parse name and update research 
    public static void timer_finished(string name){
        Debug.Log("timer finished");
        buttons_active = true;
        Debug.Log(name);
        string[] elements = name.Split(' '); 
        Debug.Log(elements[1]);
        string energy_name = elements[0]; 
        int energy_level = int.Parse(elements[1]);
        
        // when building is finished reset energy needs
        int[] energy_array = God.energy_research_energy_increase[energy_name];
        int energy_increase = energy_array[energy_level];
        God.added_energy_needs -= energy_increase;

        update_research(energy_name, energy_level);
    }

    public static bool can_afford(int cost, int energy_increase){
        if(God.total_money < cost){
            Debug.Log("not enough money");
            Debug.Log(cost);
            Debug.Log(God.total_money);
            return (false);
        } else if(God.current_energy_needs + energy_increase > God.world_energy_production) 
        {
            Debug.Log("not enough energy");
            return(false);
        }else{
            return(true);
        }
    }
    public static void update_research(string name, int level){
        Debug.Log("updating research");
        Debug.Log(name);
        Debug.Log(level);
        God.research_levels[name] = level + 1; 
        //reset_buttons();
    }
    
     public static void  button_clicked(string name, int level, int cost, int energy_increase){
         start_counter(name,level,cost, energy_increase);
        
        
        
    }
}

