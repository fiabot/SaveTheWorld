/*Fiona Shyne 
Research button object 
maintains data for research button

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour
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
        DontDestroyOnLoad(gameObject);

        this_button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
         
         
    }
    public void initalize(){ 
        int[] cost_array = God.energy_research_cost[energy_name];
        cost = cost_array[energy_level];

        int[] energy_array = God.energy_research_energy_increase[energy_name];
        energy_increase = energy_array[energy_level];

          //makes interactable only if player can afford it 
        if (Research.can_afford(cost,energy_increase)){
            this_button.interactable = true;

        }else{
            this_button.interactable = false;

        }
        //make not interactable if already at highest level
        if (energy_level == 3){
            this_button.interactable = false;
        }
        int next_level = energy_level + 1;
        title.text = energy_name + " " + next_level; 

        transform.position = transform.position + new Vector3(0,y_offset,0);

    }
    public void TaskOnClick(){
        
        Research.button_clicked(energy_name, energy_level, cost, energy_increase);
        //gameObject.SetActive(false);
        this_button.interactable = false;
        
        

    }
}
