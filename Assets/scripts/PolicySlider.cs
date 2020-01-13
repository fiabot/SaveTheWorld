using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PolicySlider : MonoBehaviour
{
    public string name; 
    public int max_amount; 
    public int y_offset;
    public static Text title; 
    public static Slider slider;
    public int index;
    public int level; 
    // Start is called before the first frame update
    void Start()
    {
        title = GetComponent<Text>(); 
        slider = GetComponent<Slider>();
    }
    public void initalize(){
        title = GetComponent<Text>(); 
        
        title.text = name + " " + level.ToString(); 
        transform.position = transform.position + new Vector3(0,y_offset,0);

    }
    public float get_value(){
        slider = GetComponent<Slider>();
        return slider.value;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
