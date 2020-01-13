using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class info_display : MonoBehaviour
{
    Text display_text; 
    public bool display_energy;
    public bool display_co2; 
    public bool display_time;
    public bool display_money;
    public bool display_total_co2;
    public bool display_current_energy_needs;
    public bool display_min_energy_needs;
    public bool display_popularity;
    // Start is called before the first frame update
    void Start()
    {
        display_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (display_energy){
            if (God.selected_region == "World"){
                int energy= God.world_energy_production;
                display_text.text = energy.ToString(); 
            } else {
                int energy =God.regions[God.selected_region].energy_production;
                display_text.text = energy.ToString();

            }
        }else if (display_co2){
        
            if (God.selected_region == "World"){
                int co2 = God.world_co2_production;
                display_text.text = co2.ToString(); 
            } else {
                int co2 = God.regions[God.selected_region].co2_production;
                display_text.text = co2.ToString();
            }

        }else if(display_time){
            display_text.text = "Day:" + God.current_day.ToString();

        }else if(display_money){
            display_text.text = "Money:" + God.total_money.ToString();
        }
        else if (display_total_co2){
            display_text.text = God.world_co2_total.ToString();
        }
        else if (display_current_energy_needs){
            display_text.text = God.current_energy_needs.ToString();
        }
        else if (display_min_energy_needs){
            display_text.text = God.min_energy_needs.ToString();
        }
        else if (display_popularity){
            Debug.Log(God.current_popularity);
            display_text.text = God.current_popularity.ToString();
        }
    }
}
