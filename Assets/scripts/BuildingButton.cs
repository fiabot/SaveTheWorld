using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public string energy_name; 
    public int energy_level; 
    public int cost; 
    public int energy_increase;
    public int y_offset; 
    Transform child;
    public Text title; 
    public Button this_button; 
    // Start is called before the first frame update
    void Start()
    {
        //set up variables 
        child = this.gameObject.transform.GetChild(0);
        title = child.GetComponent<Text>();
        this_button =  GetComponent<Button>();

        



        this_button.onClick.AddListener(TaskOnClick);        

    }

    // Update is called once per frame
    void Update()
    {
         
         
    }
    public void initalize(){ 
        this_button =  GetComponent<Button>();

        int[] cost_array = God.energy_build_cost[energy_name];
        cost = cost_array[energy_level];

        int[] energy_array = God.energy_build_energy_increase[energy_name];
        energy_increase = energy_array[energy_level];
        
          //makes interactable only if player can afford it 
        if (Building.can_afford(cost, energy_increase)){
            this_button.interactable = true;

        }else{
            Debug.Log("cannot afford");
            this_button.interactable = false;

        }
        
        child = this.gameObject.transform.GetChild(0);//TODO, figure out why the fuck c doesn't know what title is 
        title = child.GetComponent<Text>();
        title.text = energy_name + ' ' + energy_level;

        transform.position = transform.position + new Vector3(0,y_offset,0);

    }
    public void TaskOnClick(){
        Debug.Log("in button");
        Debug.Log(energy_name);
        
        Building.button_clicked(energy_name, energy_level, cost, energy_increase);
        
        
        

    }
}
