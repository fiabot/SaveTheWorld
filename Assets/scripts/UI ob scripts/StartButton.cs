using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class StartButton : MonoBehaviour
{
    public bool new_game;
    Button this_button;
    // Start is called before the first frame update
    void Start()
    {
        this_button =  GetComponent<Button>();
        if (new_game){
            this_button.onClick.AddListener(God.start_game);
        }else{
            if(SaveGame.Load<bool>("Saved Game")){
                this_button.interactable = true;
                this_button.onClick.AddListener(God.load);
            }else{
                this_button.interactable = false;
            }
                
        }
        
    }

}
