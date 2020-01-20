/* Fiona Shyne 
Changes title display on ending screen

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Text title;
    // Start is called before the first frame update
    void Start()
    {
        if(God.player_won){
            title.text = "You Won!";
        }else if (God.world_co2_total >= God.max_co2){
            title.text = "Too Much Co2";
        }else if (God.current_energy_needs >= God.world_energy_production){
            title.text = "Energy Supply Gone";
        }else{
            title.text = "You Suck!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
