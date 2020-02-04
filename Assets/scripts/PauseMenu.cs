//Fiona Shyne 
//Display pause menu and allow player to save or return to main screen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject back_to_main; 
    public GameObject save_button; 
    public GameObject background; 
    public GameObject close;
    // Start is called before the first frame update
    void Start()
    {
        back_to_main.SetActive(false); 
        save_button.SetActive(false); 
        background.SetActive(false);
        close.SetActive(false);
    }

    public void show_menu(){
        back_to_main.SetActive(true); 
        save_button.SetActive(true); 
        background.SetActive(true);
        close.SetActive(true);

        God.is_time_passing = false;

    }

    public void close_screen(){
        back_to_main.SetActive(false); 
        save_button.SetActive(false); 
        background.SetActive(false);
        close.SetActive(false); 

        God.is_time_passing = true;
    }

    public void open_opening(){
        God.static_load_opening();
    }

    public void save_game(){ 
        God.save();
    }


}
