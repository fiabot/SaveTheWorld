using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class region : MonoBehaviour
{
    //set up variables 
    public string region_name; 
    public int region_index;
    public int energy_production; 
    public int co2_production; 
    public List<Energy> energy_plants = new List<Energy>(); 

    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        //indentify which region object is attached to 
        if (region_name == "North America"){
            region_index = 0;

        }else if (region_name == "South America"){
            region_index = 1;
        } 
    
       DontDestroyOnLoad(this.gameObject);
       
    }
   

    // Update is called once per frame
    void Update()
    {
         
    }
}
