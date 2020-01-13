using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumbitPolicyButton : MonoBehaviour
{
    Button this_button;

    // Start is called before the first frame update
    void Start()
    {
        this_button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(God.can_policy){
            this_button.interactable = true;
        }else{
            this_button.interactable = false;
        }
        
    }
}
