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
    public static void add_relief_timer(int damage){
        foreach(var i in GameTime.disaster_timer){
            Debug.Log(i.Key);
        }
        //add longer waits for time when there is less co2
        float perecent_till_doom = (float)God.world_co2_total / (float)God.max_co2; //percent of max until player losses 
        int days_to_wait = (int) Mathf.Floor(perecent_till_doom * God.maximum_relief_wait);//set days to max wait * percentage 
        Debug.Log("days to wait");
        Debug.Log(perecent_till_doom);
        Debug.Log(days_to_wait);

        GameTime.disaster_timer.Add(damage.ToString(), days_to_wait);
    }
    //unleash the distaster if a random percent is less then the percent of total co2 over max co2
    public static bool erupt(){
        return true;
        /*float percent_co2_left = God.world_co2_total / God.max_co2;
        float random_percent = Random.Range(God.damage_percent_min, God.damage_percent_max);

        if (random_percent > percent_co2_left){
            return true;
        }else{
            return false;
        }*/

    }
    //runs when timer is finished, if erupt return true increase added energy needs 
    public static void timer_finished(string timer){
        if (timer == "disaster"){
            add_disaster_timer();//add a new timer regardless of eruption
            Debug.Log("disaster timer finished");

            if (erupt()){
                //randomly chose name from list 
                int random_index = Random.Range(0, God.disaster_names.Length);
                string name = God.disaster_names[random_index];
                Debug.Log("Disaster erupted"); 
                Debug.Log(name);

                //cause energy increase from random percent of co2 production
                float random_percent = Random.Range(God.damage_percent_min, God.damage_percent_max);
                float damage_float = random_percent * God.world_co2_total;
                damage_float = Mathf.Floor(damage_float);
                int damage = (int) damage_float;
                God.added_energy_needs += damage;
                Debug.Log("damage");
                Debug.Log(random_percent * God.world_co2_total);
                Debug.Log(damage);

                //add timer for damage relief 
                add_relief_timer(damage); 
            }
        }else{ //reset energy when timer is done 
            int damage = int.Parse(timer); 
            God.added_energy_needs -= damage;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
