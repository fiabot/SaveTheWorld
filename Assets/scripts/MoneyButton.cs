using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyButton : MonoBehaviour
{
    Button this_button;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        this_button =  GetComponent<Button>();

        this_button.onClick.AddListener(TaskOnClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //add money when clicked 
    void TaskOnClick(){
        God.total_money += value; 
        Debug.Log(God.total_money);
        Destroy(gameObject);
    }
}
