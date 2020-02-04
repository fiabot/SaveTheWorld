/* Fiona Shyne 
Set building and policy buttons to be inactive if there is no current research 
Set building button to be inactive if a region is not selected

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inactive_if_not_selected : MonoBehaviour
{
    public Button this_button; 
    public bool active_with_research; 
    public bool active_when_selected;
    // Start is called before the first frame update
    void Awake()
    {
        this_button =  GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active_with_research){
            bool has_research = false; 
            foreach (KeyValuePair<string, int> i in God.research_levels){
                if (i.Value != 0){
                    has_research = true;
                }
            }
            if (has_research){
                //make button uninteractable if region is not selected 
                if (active_when_selected){
                    if(God.selected_region == "World"){
                        this_button.interactable = false;
                    }else{
                        this_button.interactable = true;
                    }
                }else{
                    this_button.interactable = true;
                }
                
            }else{
                this_button.interactable = false; 
            }

        }
    }
}
