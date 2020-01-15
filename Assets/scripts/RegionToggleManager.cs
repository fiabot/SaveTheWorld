/*Fiona Shyne 
Manages region selection 

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionToggleManager : MonoBehaviour
{
    //toggle objects set up in ui
    public string selected_region= God.selected_region; 
    public Toggle North_America_toggle;
    public Toggle South_America_toggle; 
    // Start is called before the first frame update
    void Start()
    {
        string x = get_selected_region();
    }
    //when a toggle is changes, update selected region
    public void selected_region_changed(){
        string x = get_selected_region();
    }

    //update selected region 
    public string get_selected_region(){ 
        //updates selected_region from toggles, returns region
        if (North_America_toggle.isOn){
            God.selected_region = "North America";
            return "North America";
        }else if (South_America_toggle.isOn){
            God.selected_region = "South America";
            return "South America"; 
        }else{
            God.selected_region = "World"; 
            return "World";
        }
    
    }
}
