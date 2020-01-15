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
        }else if (God.player_lost){
            title.text = "You Lost!";
        }else{
            title.text = "You Suck!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
