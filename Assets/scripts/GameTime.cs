using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static Dictionary<string, int> research_counter = new Dictionary<string,int>(); 
    public static Dictionary<string, int> build_counter = new Dictionary<string,int>(); 
    public static Dictionary<string, int> god_counter = new Dictionary<string,int>(); 
    public static Dictionary<string, int> policy_counter = new Dictionary<string,int>();  


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject); 
    }

    //clear out timers 
    public static void clear_out(){
        research_counter.Clear();
        build_counter.Clear();
        god_counter.Clear();
        policy_counter.Clear();
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

                    //update research counters 
                    List<string> research_keys= new List<string>(research_counter.Keys);
                    foreach (var i in research_keys)
                    {
                        research_counter[i] -= 1; 
                        if (research_counter[i] == 0){
                            research_counter.Remove(i);
                            Research.timer_finished(i);
                        }
                    }

                    //update building timers 
                    List<string> build_keys= new List<string>(build_counter.Keys);
                    foreach (var i in build_keys)
                    {
                        build_counter[i] -= 1; 
                        if (build_counter[i] == 0){
                            build_counter.Remove(i);
                            Building.timer_finished(i);
                        }
                    }
                    //update god times
                    List<string> god_keys= new List<string>(god_counter.Keys);
                    foreach (string i in god_keys)
                    {
                        god_counter[i] -= 1;
                        if (god_counter[i] == 0){
                            god_counter.Remove(i);
                            God.timer_finished(i);
                        }
                    }
                    List<string> policy_keys= new List<string>(policy_counter.Keys);
                    foreach (string i in policy_keys)
                    {
                        policy_counter[i] -= 1;
                        if (policy_counter[i] == 0){
                            policy_counter.Remove(i);
                            PolicyPage.timer_finished(i);
                        }
                    }
                }
        }
    }
}
