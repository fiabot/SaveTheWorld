/*Fiona Shyne 
update game time 
manage timers and trigger finshing functions 

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static Dictionary<string, int> research_timer = new Dictionary<string,int>(); 
    public static Dictionary<string, int> build_timer = new Dictionary<string,int>(); 
    public static Dictionary<string, int> god_timer = new Dictionary<string,int>(); 
    public static Dictionary<string, int> policy_timer = new Dictionary<string,int>();  
    public static Dictionary<string, int> disaster_timer = new Dictionary<string,int>();  


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject); 
    }

    //clear out timers 
    public static void clear_out(){
        research_timer.Clear();
        build_timer.Clear();
        god_timer.Clear();
        policy_timer.Clear();
        disaster_timer.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (God.is_time_passing){
            God.seconds_since_start += Time.deltaTime;
            float seconds_rounded = Mathf.Round(God.seconds_since_start);

            //runs once a day
            if (God.current_day != (int)God.seconds_since_start/ God.seconds_per_day){  
                    //update date   
                    God.current_day = (int)God.seconds_since_start/ God.seconds_per_day;
                    God.world_co2_total += God.world_co2_production;
                    God.update_world();

                    //update all timer, and trigger finished event if needed 

                    //update research timers 
                    List<string> research_keys= new List<string>(research_timer.Keys);
                    foreach (var i in research_keys)
                    {
                        research_timer[i] -= 1; 
                        if (research_timer[i] == 0){
                            research_timer.Remove(i);
                            Research.timer_finished(i);
                        }
                    }

                    //update building timers 
                    List<string> build_keys= new List<string>(build_timer.Keys);
                    foreach (var i in build_keys)
                    {
                        build_timer[i] -= 1; 
                        if (build_timer[i] == 0){
                            build_timer.Remove(i);
                            Building.timer_finished(i);
                        }
                    }
                    //update god timers
                    List<string> god_keys= new List<string>(god_timer.Keys);
                    foreach (string i in god_keys)
                    {
                        god_timer[i] -= 1;
                        if (god_timer[i] == 0){
                            god_timer.Remove(i);
                            God.timer_finished(i);
                        }
                    }
                    //update policy timers
                    List<string> policy_keys= new List<string>(policy_timer.Keys);
                    foreach (string i in policy_keys)
                    {
                        policy_timer[i] -= 1;
                        if (policy_timer[i] == 0){
                            policy_timer.Remove(i);
                            PolicyPage.timer_finished(i);
                        }
                    }
                    //update diaster timers 
                    List<string> disaster_keys= new List<string>(disaster_timer.Keys);
                    foreach (string i in disaster_keys)
                    {
                        disaster_timer[i] -= 1;
                        if (disaster_timer[i] == 0){
                            disaster_timer.Remove(i);
                            Disaster.timer_finished(i);
                        }
                    }
                }
        }
    }
}
