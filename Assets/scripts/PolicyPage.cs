using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyPage : MonoBehaviour
{
    //set up global variables 
    public Text main_title; 
    public GameObject blank_slider;
    public Transform Canvas;
    public static GameObject blank_slider_copy;
    public static Transform Canvas_copy;
    public List<PolicySlider> sliders = new List<PolicySlider>();

    // initalize important variables 
    void Start()
    {
        //main_title.text = God.selected_region;
        blank_slider_copy = blank_slider;
        Canvas_copy = Canvas;
        blank_slider.SetActive(false);
        sliders.Clear();
        create_sliders();
        

    }
    // create new slider with giving name and level 
    public void new_slider(string name, int level, int offset){
        GameObject new_slider = Instantiate(blank_slider_copy,Canvas_copy, true);
        new_slider.SetActive(true);  
        PolicySlider new_slider_script = new_slider.GetComponent<PolicySlider>(); 
        sliders.Add( new_slider_script);
        new_slider_script.name = name;
        new_slider_script.level = level;
        new_slider_script.y_offset = offset;
        new_slider_script.initalize();
    }

    //determine which sliders to create and create them 
    public void create_sliders(){
        //List<Energy> region_energy_plants = God.regions[God.selected_region].energy_plants;
        int offset = 0; 
        Debug.Log(God.research_levels);
        //repeate for each type of research 
        foreach (KeyValuePair<string, int> i in God.research_levels){
            //for each level that has been researched, add a slider
            Debug.Log(i.Value);
            if (i.Value >= 1 ){
                new_slider(i.Key, 1, offset);
                offset -= 45;
            }
            
            if (i.Value >= 2){
                new_slider(i.Key, 2, offset);
                offset -= 45;
            }

            if (i.Value >= 3){
                new_slider(i.Key, 2, offset);
                offset -= 45;
            }
            

        }
    }
    //use popularity to randomly determine if the policy passes 
    public bool policy_passes(){
        double chance = Random.value;
        if (chance < God.current_popularity){
            main_title.text = "policy passes";
            return true; 
        }
        else{
            main_title.text = "policy does not pass";
            return false;
        }

    }

    //determine if policy passes and if so restricts energy type 
    public void button_clicked(){
        if (policy_passes()){
            Debug.Log("policy passes");
            foreach(PolicySlider i in sliders){
                string name = i.name; 
                int level = i.level;
                float restriction = i.get_value();
                God.restrict_energy(name,level,  restriction);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
