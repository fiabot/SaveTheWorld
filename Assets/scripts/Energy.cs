﻿/*Fiona Shyne 
Manages energy plant, hold energy production, apply restrictions 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    //set up varriables 
    public string name;   
    public int[] energy_production;//list of energy production by level
    public int[] co2_production;// list of co2 prodcution by level 
    public int level;
    public int energy_potential;
    public int co2_potential;
    public float energy_restriction;
    public int current_energy; 
    public int current_co2; 
    
    // Keep object for rest of game 
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //update varriables at creation
    public void initalize(){ 
        energy_restriction = 1;
        upgrade_energy(level);
    }
    
    //update energy to new energy level 
    public void upgrade_energy(int level){
        //update level and energy/co2 production potentials 
        level = level; 
        energy_potential = energy_production[level]; 
        co2_potential = co2_production[level];

        //update restriction and apply ir 
        energy_restriction = God.energy_restrictions[name + "/" + level.ToString()];
        current_energy =(int) Mathf.Floor(energy_potential * energy_restriction);
        current_co2 =(int) Mathf.Floor(co2_potential * energy_restriction);

    }

    //scale energy based on new restriction
    public void scale_energy(){
        Debug.Log("scaling energy"); 
        current_energy =(int) Mathf.Floor(energy_potential * energy_restriction);
        current_co2 = (int) Mathf.Floor(co2_potential * energy_restriction);
    }

    //how much energy would change under a restriction 
    public int energy_change(float restriction){
        int change = (int) Mathf.Floor(energy_potential * restriction) - current_energy;
        return change;
    }
    //how much co2 would change under a restriction
    public int co2_change(float restriction){
        int change =  (int) Mathf.Floor(co2_potential * restriction) - current_co2;
        return change;
    }
}
