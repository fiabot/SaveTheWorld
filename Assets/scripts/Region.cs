/*Fiona Shyne
Manage Region action
Contain energy plants and update energy production 
Restrict energy in regions plants 
Destroy plants in disaster event

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region: MonoBehaviour
{
    //set up variables 
    public string name; 
    public int region_index;
    public int energy_production; 
    public int co2_production; 
    public string advantage; 
    public int advantage_pts; 
    public List<Energy> energy_plants = new List<Energy>(); 
    public Dictionary<string, int> energy_by_type = new Dictionary<string,int>(); 
    
    
    // Start is called before the first frame update
    void Awake()
    {
       
    
       DontDestroyOnLoad(this.gameObject);
       
    }
   
   public void initalize(string[] energy_names){
       //reset variables 
       energy_production = 0; 
       co2_production = 0;  

        foreach (string i in energy_names){
            energy_by_type.Add(i,0);
        }
   }
   public void add_energy_plant(Energy energy){ 
       //add a new energy plant to region
       energy_plants.Add(energy); 
       update_energy();
       energy_by_type[energy.name] += energy.current_energy; //TODO add this function to update energy
       update_co2();
       God.update_world(); 
   }
   public void update_energy(){
       int energy_timer = 0;
       //add up the energy collected from each plant
       foreach (Energy i in energy_plants)
       {
           energy_timer += i.current_energy; 

           //if region has an advantage in this regions, add the extra energy
           if (advantage == i.name){
               energy_timer += advantage_pts;
           }
       }
       //set the total count to regions energy productions 
       energy_production = energy_timer;
   }
   public void update_co2(){
       int co2_timer = 0; 
       //add up the co2 production from each plant 
       foreach (Energy i in energy_plants)
       {
           co2_timer += i.current_co2; 
       }
       co2_production = co2_timer;
   }
   public void apply_restriction(string energy_name, int level, float restriction){
       Debug.Log("applying restriciont");
       foreach(Energy i in energy_plants){
           if (i.name == energy_name && i.level == level){
                i.energy_restriction = restriction; 
                i.scale_energy(); 
           }
       }
       update_energy();
       update_co2(); 

   }
   //remove energy plants in the case of a disaster
   public void disaster_hit(int num_removed){
       //for each plant to be removed, randomly chose one from the list of keys 
       for (int i = 0; i<num_removed; i++){
           int random_index = Random.Range(0, energy_plants.Count);
           energy_plants.Remove(energy_plants[random_index]);
       }
       update_energy(); 
       update_co2();
   }
    //return how much energy would change under a restriction 
    public int[] restriction_energy_change(string energy_name, int level, float restriction){
        int[] change = {0,0};
        foreach(Energy i in energy_plants){
            if (i.name == energy_name && i.level == level){
               change[0] += i.energy_change(restriction);
               change[1] += i.co2_change(restriction);
           }
       }
       return change;
   }
}
