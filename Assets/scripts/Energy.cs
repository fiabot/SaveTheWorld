using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public string name;   
    public int[] energy_production;//list of energy production by level
    public int[] co2_production;// list of co2 prodcution by level 
    public int level;
    public int energy_potential;
    public int co2_potential;
    public float energy_restriction;
    public int current_energy; 
    public int current_co2; 
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void initalize(){ 
        energy_restriction = 1;
        upgrade_energy(level);
    }
    
    public void upgrade_energy(int level){
        level= level; 
        energy_potential = energy_production[level]; 
        co2_potential = co2_production[level];
        current_energy =(int) Mathf.Floor(energy_potential * energy_restriction);
        current_co2 =(int) Mathf.Floor(co2_potential * energy_restriction);
        current_co2 = co2_production[level];
    }
    public void scale_energy(){
        Debug.Log("scaling energy"); 
        current_energy =(int) Mathf.Floor(energy_potential * energy_restriction);
        current_co2 = (int) Mathf.Floor(co2_potential * energy_restriction);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
