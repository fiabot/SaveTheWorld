/* Fiona Shyne
Manage disaster timers and events and inflict damage when needed 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //add a new timer with time portional to co2 total 
    public static void add_disaster_timer(){
        //add longer waits for time when there is less co2
        float perecent_till_doom = (1 - God.world_co2_total / God.max_co2); //percent of max until player losses 
        int days_to_wait = (int) Mathf.Floor(perecent_till_doom * God.maximum_damage_wait);//set days to max wait * percentage 
        Debug.Log(days_to_wait);

        GameTime.disaster_timer.Add("disaster", days_to_wait);

    }
    
    //unleash the distaster if a random percent is less then the percent of total co2 over max co2
    public static bool erupt(){
        float percent_co2_left = God.world_co2_total / God.max_co2;
        float random_percent = Random.Range(God.damage_percent_min, God.damage_percent_max);

        if (random_percent > percent_co2_left){
            return true;
        }else{
            Debug.Log("crisis adverted");
            return false;
        }

    }
    //runs when timer is finished, if erupt returns true destroy random plants 
    public static void timer_finished(string timer){
        add_disaster_timer();//add a new timer regardless of eruption
    
        if (erupt()){
            //randomly chose a disaster name from list 
            int random_name_index = Random.Range(0, God.disaster_names.Length);
            string name = God.disaster_names[random_name_index];
 

            //randomly chose  region to target 
            string[] region_list = God.region_names;
            int random_num = Random.Range(0, region_list.Length);//returns random float within range
            //int random_region_index = (int)Mathf.Ceil(random_num); //convert float to in by using ceiling 
            string region_name = region_list[random_num];
            Region region_target = God.regions[region_name];

            //chose percentage to destroy from randomness and co2 total  
            float random_percent = Random.Range(God.damage_percent_min, God.damage_percent_max);
            float perecent_till_doom = (float)God.world_co2_total / (float)God.max_co2; //percent of max until player losses 
            float damage_percent = random_percent * perecent_till_doom;//percent of plants to destroy

            //multipy percentage to get total amount of plants to destroy
            int total_plants = region_target.energy_plants.Count;
            float damage_float = (float)total_plants * damage_percent;
            damage_float = Mathf.Ceil(damage_float);
            int damage = (int) damage_float;

            Debug.Log("Destoyed " + damage.ToString() +" plants in " + region_name);

            //display notification 
            God.notification_title = name;
            God.notification_subtitle = damage.ToString() +" plant[s] destroyed in " + region_name;
            Notification.show_notification();

            //remove plants 
            region_target.disaster_hit(damage); 
        }
    }
}
