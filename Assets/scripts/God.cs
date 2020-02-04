/* Fiona Shyne 
Climate change simuluation game 
Research new renewable energy sources, build energy plants, and enact policy to reach net zero emmissions 


Manages main variables and functions to be used throughout game framework 
Contains all varriables that are to be across scripts 
Updates world co2 and energy production as well us popularity 
Trigger money drops 
Increase mininum energy needs 
Transitions between scenes 
Start and reset games 
Trigger win and loss events 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class God : MonoBehaviour
{
    //dont create twice 
     private static God godInstance;

    //set up inital variables

    //constant variables 
    public static int seconds_per_day = 2;

    public static int money_drop_max;
    public static int money_drop_min;
    public static float money_percent_min; //percent of excess energy to award
    public static float money_percent_max;

    public static int min_energy_increase_amount_rate; // the rate the amount of min energy increase 
    public static int build_wait;
    public static int research_wait;
    public static int policy_wait;//number of days to wait before you can propose poilcy again
    public static int max_co2;
    public static int starting_energy_plants; // amount of energy that player starts with

    public static string[] region_names = {"North America", "South America", "Africa", "Asia", "Europe", "Middle East", "Pacific"}; 
    public GameObject blank_region_object;
    public static GameObject blank_region_object_copy;
    public static Dictionary<string, string> region_advantage = new Dictionary<string,string>(); 
    public static int advantage_pts;

    public static string[] energy_names=  {"Coal","Oil", "Biofuel", "Natural Gas", "Hydropower", "Solar"}; 
    public GameObject blank_energy_object; 
    public static  GameObject blank_energy_object_copy;
    public static Dictionary<string, int[]> energy_production_by_name= new Dictionary<string, int[]>(); 
    public static Dictionary<string, int[]> co2_production_by_name= new Dictionary<string, int[]>(); 
    public static Dictionary<string, int[]> energy_research_cost = new Dictionary<string, int[]>(); 
    public static Dictionary<string, int[]> energy_research_energy_increase = new Dictionary<string, int[]>(); 
    public static Dictionary<string, int[]> energy_build_cost = new Dictionary<string, int[]>(); 
    public static Dictionary<string, int[]> energy_build_energy_increase = new Dictionary<string, int[]>(); 

    public static string[] disaster_names = {"Floods", "Drought", "Hurricane", "Forest Fires"};
    public static float damage_percent_min; 
    public static float damage_percent_max;
    public static int maximum_damage_wait;
    public static int maximum_relief_wait;


    //varrying variables 
    public static int world_energy_production; 
    public static int world_co2_production; // co2 produced per day
    public static int world_co2_total; 
    public static string selected_region = "World";

    public static Dictionary<string, Region> regions = new Dictionary<string, Region>();
    public static Dictionary<string,int> research_levels = new Dictionary<string,int>(); 
    public static Dictionary<string, float> energy_restrictions = new Dictionary<string, float>();
    
    public static int current_energy_needs; 
    public static int added_energy_needs; 
    public static int min_energy_needs; 
    public static int current_surplus;
    
    public static int current_day; 
    public static float seconds_since_start;
    public static bool is_time_passing;

    public static int total_money; 
    
    public static int min_energy_increase_time; //time it takes to increase the min energy needs
    public static int min_energy_increase_amount; //amount energy increases each time  

    public static double current_popularity;
    public static bool can_policy; //true if player is able to enact policy
    public static double energy_pop_min;//mininum percentage added to popularity from energy production
    public static double energy_pop_max;//max percentage added to popularity from energy production 
    public static double co2_pop_min;
    public static double co2_pop_max;

    public static bool player_won; 
    public static bool player_lost; 

    public static string notification_title; 
    public static string notification_subtitle;

    
    //define unchanging variables
    void Start(){
        //so like we don't have to start over each game 
        DontDestroyOnLoad(this.gameObject);
        if (godInstance == null){
            godInstance = this; 
            //define unchanging variables 
            advantage_pts = 2;  
            is_time_passing = false;
            money_drop_max = 5;
            money_drop_min = 3;
            money_percent_min = 0.1f; 
            money_percent_max = 0.4f; 
            build_wait = 4; 
            research_wait = 7;
            min_energy_increase_amount_rate = 0;
            max_co2 = 4000;
            starting_energy_plants = 2;
            policy_wait = 5;

            damage_percent_min = 0.5f; 
            damage_percent_max = 1.0f;
            maximum_damage_wait = 30;
            maximum_relief_wait = 20;
            
            blank_energy_object_copy = blank_energy_object;
            blank_region_object_copy = blank_region_object;

            //define regions advantages
            region_advantage.Add("North America", "Coal"); 
            region_advantage.Add("South America", "Biofuel"); 
            region_advantage.Add("Africa", "Solar"); 
            region_advantage.Add("Asia", "Natural Gas"); 
            region_advantage.Add("Europe", "Hydropower"); 
            region_advantage.Add("Middle East", "Oil");
            region_advantage.Add("Pacific", "Hydropower");

            //set up energy production levels 
            int[] coal_energy_int = {0,5,10,15};
            int[] oil_energy_int = {0,4,8,12};
            int[] bio_energy_int = {0,7,12,17};
            int[] gas_energy_int = {0,8,14,20};
            int[] hydro_energy_int = {0,6,8,15};
            int[] solar_energy_int = {0,5,17,25};
            energy_production_by_name.Add("Coal", coal_energy_int); 
            energy_production_by_name.Add("Oil", oil_energy_int); 
            energy_production_by_name.Add("Biofuel", bio_energy_int); 
            energy_production_by_name.Add("Natural Gas", gas_energy_int); 
            energy_production_by_name.Add("Hydropower", hydro_energy_int); 
            energy_production_by_name.Add("Solar", solar_energy_int); 

            //set up co2 prodcution levels 
            int[] coal_co2_int = {0,10,20,30};
            int[] oil_co2_int = {0,9,17,25}; 
            int[] bio_co2_int = {0,7,14,19}; 
            int[] gas_co2_int = {0,6,11,17}; 
            int[] hydro_co2_int = {0,3,0,0}; 
            int[] solar_co2_int = {0,0,0,0}; 
            co2_production_by_name.Add("Coal", coal_co2_int); 
            co2_production_by_name.Add("Oil", oil_co2_int); 
            co2_production_by_name.Add("Biofuel", bio_co2_int); 
            co2_production_by_name.Add("Natural Gas", gas_co2_int); 
            co2_production_by_name.Add("Hydropower", hydro_co2_int); 
            co2_production_by_name.Add("Solar", solar_co2_int); 

            //set up research cost levels 
            int[] coal_res_cost_int = {5,10,15};//money to increase to next level
            int[] oil_res_cost_int = {5,10,15}; 
            int[] bio_res_cost_int = {10,15,20}; 
            int[] gas_res_cost_int = {13,18,25}; 
            int[] hydro_res_cost_int = {15,20,19}; 
            int[] solar_res_cost_int = {15,27,33}; 
            energy_research_cost.Add("Coal", coal_res_cost_int); 
            energy_research_cost.Add("Oil", oil_res_cost_int); 
            energy_research_cost.Add("Biofuel", bio_res_cost_int); 
            energy_research_cost.Add("Natural Gas", gas_res_cost_int);
            energy_research_cost.Add("Hydropower", hydro_res_cost_int);
            energy_research_cost.Add("Solar", solar_res_cost_int);  
            

            //set up research energy increase levels 
            int[] coal_res_en_int = {4,10,15};
            int[] oil_res_en_int = {4,10,15};
            int[] bio_res_en_int = {8,13,18};
            int[] gas_res_en_int = {10,15,21};
            int[] hydro_res_en_int = {8,14,17};
            int[] solar_res_en_int = {8,23,28};
            energy_research_energy_increase.Add("Coal", coal_res_en_int); 
            energy_research_energy_increase.Add("Oil", oil_res_en_int); 
            energy_research_energy_increase.Add("Biofuel", bio_res_en_int);
            energy_research_energy_increase.Add("Natural Gas", gas_res_en_int);
            energy_research_energy_increase.Add("Hydropower", hydro_res_en_int);
            energy_research_energy_increase.Add("Solar", solar_res_en_int);

            //set up build cost levels 
            int[] coal_bu_cost_int = {0,3,8,13};//money to build current level
            int[] oil_bu_cost_int = {0,3,8,13}; 
            int[] bio_bu_cost_int = {0,7,10,15}; 
            int[] gas_bu_cost_int = {0,9,12,17}; 
            int[] hydro_bu_cost_int = {0,6,10,15}; 
            int[] solar_bu_cost_int = {0,7,15,20}; 
            energy_build_cost.Add("Coal", coal_bu_cost_int); 
            energy_build_cost.Add("Oil", oil_bu_cost_int); 
            energy_build_cost.Add("Biofuel", bio_bu_cost_int); 
            energy_build_cost.Add("Natural Gas", gas_bu_cost_int); 
            energy_build_cost.Add("Hydropower", hydro_bu_cost_int); 
            energy_build_cost.Add("Solar", solar_bu_cost_int); 
            
            //set up build energy levels 
            int[] coal_bu_en_int = {0,2,5,10};
            int[] oil_bu_en_int = {0,2,5,10}; 
            int[] bio_bu_en_int = {0,4,7,12}; 
            int[] gas_bu_en_int = {0,2,6,10}; 
            int[] hydro_bu_en_int = {0,2,5,10}; 
            int[] solar_bu_en_int = {0,5,4,2}; 
            energy_build_energy_increase.Add("Coal", coal_bu_en_int); 
            energy_build_energy_increase.Add("Oil", oil_bu_en_int);  
            energy_build_energy_increase.Add("Biofuel", bio_bu_en_int);    
            energy_build_energy_increase.Add("Natural Gas", gas_bu_en_int);  
            energy_build_energy_increase.Add("Hydropower", hydro_bu_en_int);  
            energy_build_energy_increase.Add("Solar", solar_bu_en_int);  
        }else{
            Destroy(gameObject);
        }
        
        
        
    }
    public void different_start(){
        Debug.Log("different Start");
    }



    //runs when start buttton is clicked
    //reset variables, add starting timers and loads main screen
    static public void start_game(){
        Debug.Log("starting");
        clear_out();
        //sets up game when start is pressed

        //reset all changing 
        current_day = 0; 
        seconds_since_start = 0;
        is_time_passing = true;

        min_energy_needs = 0;
        current_energy_needs = 0; 
        added_energy_needs = 0;

        min_energy_increase_time = 10; 
        min_energy_increase_amount = 3;

        current_popularity = 0.8;
        Research.toggles_active = true;
        energy_pop_min = 0.2;
        co2_pop_min = 0.3;
        energy_pop_max = 0.5; 
        co2_pop_max = 0.5;

        player_lost = false; 
        player_won = false;

        world_co2_production = 0; 
        world_energy_production = 0; 
        world_co2_total = 0; 

        can_policy = true;

        //reset energy_restriction dictionary 
        energy_restrictions.Clear();
        foreach(string i in energy_names){
            for(int n = 1; n < 4; n++){
                string restriction_name = i + "/" + n.ToString();
                energy_restrictions.Add(restriction_name, 1);
            }
            
        }

        //set staring money to be just enough to build a coal plant 
        total_money = energy_research_cost["Coal"][0] + energy_build_cost["Coal"][1]; 

        //create new money drop timer 
        add_new_money_timer();

        //create new min energy increse timer 
        add_new_energy_increase_timer();

        //create first disater timer 
        Disaster.add_disaster_timer();


        //creates region objects, intializes them, and adds them to region list
        regions.Clear();
        foreach(var i in region_names){
            GameObject new_region_ob = Instantiate(blank_region_object_copy); 
            Region new_region_script = new_region_ob.GetComponent<Region>();
            new_region_script.name = i; 
            new_region_script.advantage = region_advantage[i];
            new_region_script.advantage_pts = advantage_pts;
            new_region_script.initalize(energy_names); 
            regions.Add(i, new_region_script); 
        }

        //reset research levels 
        research_levels.Clear();
        foreach (string i in energy_names){ 
            research_levels.Add(i,0);
        }

        //start off with researching coal level 1 
        research_levels["Coal"] = 1;
        
        //build starting energy 
        for (int i = 0; i < starting_energy_plants; i++){
            int random_num = Random.Range(0, region_names.Length);//get random index for region
            string region_name = region_names[random_num];
            buy_energy_plant("Coal", 1, region_name);

        }

        //update energy, co2 and other important varriables
        update_world();


        //loads main scene
        SceneManager.LoadScene("MainScene");

    }



    //save current game status
    public static void save(){
        SaveGame.Save<int>("current_day", current_day);
        SaveGame.Save<float>("second_since_start", seconds_since_start);
        SaveGame.Save<int>("min_energy_needs", min_energy_needs);
        SaveGame.Save<int>("current_energy_needs", current_energy_needs);
        SaveGame.Save<int>("added_energy_needs", added_energy_needs);
        SaveGame.Save<int>("min_energy_increase_amount", min_energy_increase_amount);
        SaveGame.Save<int>("min_energy_increase_time", min_energy_increase_time);
        SaveGame.Save<double>("current_popularity", current_popularity);
        SaveGame.Save<double>("energy_pop_min", energy_pop_min);
        SaveGame.Save<double>("energy_pop_min", energy_pop_max);
        SaveGame.Save<double>("co2_pop_min", co2_pop_min);
        SaveGame.Save<double>("co2_pop_min", co2_pop_max);
        SaveGame.Save<int>("total_money", total_money);

        SaveGame.Save<int>("world_energy_production", world_energy_production);
        SaveGame.Save<int>("co2_energy_production", world_co2_production);
        SaveGame.Save<int>("world_co2_total", world_co2_total);

        SaveGame.Save<Dictionary<string, float>>("energy_restrictions", energy_restrictions);
        SaveGame.Save<Dictionary<string, Region>>("regions", regions);
        SaveGame.Save<Dictionary<string, int>>("research_levels", research_levels);

        SaveGame.Save<bool>("Saved Game", true);

    }
    


    //load existing game 
    public static void load(){ 
        clear_out();
        current_day = SaveGame.Load<int>("current_day");
        seconds_since_start = SaveGame.Load<float>("second_since_start");
        min_energy_needs = SaveGame.Load<int>("min_energy_needs");
        current_energy_needs = SaveGame.Load<int>("current_energy_needs");
        added_energy_needs = SaveGame.Load<int>("added_energy_needs");
        min_energy_increase_amount = SaveGame.Load<int>("min_energy_increase_amount");
        min_energy_increase_time = SaveGame.Load<int>("min_energy_increase_time");
        current_popularity = SaveGame.Load<double>("current_popularity");
        energy_pop_min = SaveGame.Load<double>("energy_pop_min");
        energy_pop_max = SaveGame.Load<double>("energy_pop_min");
        co2_pop_min = SaveGame.Load<double>("co2_pop_min");
        co2_pop_max = SaveGame.Load<double>("co2_pop_min");
        total_money = SaveGame.Load<int>("total_money");

        world_energy_production = SaveGame.Load<int>("world_energy_production");
        world_co2_production = SaveGame.Load<int>("co2_energy_production");
        world_co2_total = SaveGame.Load<int>("world_co2_total");

        energy_restrictions = SaveGame.Load<Dictionary<string, float>>("energy_restrictions");
        regions = SaveGame.Load<Dictionary<string, Region>>("regions");
        research_levels = SaveGame.Load<Dictionary<string, int>>("research_levels") ;

        player_lost = false; 
        player_won = false;
        is_time_passing = true; 
        can_policy = true; 

        //create new money drop timer 
        add_new_money_timer();

        //create new min energy increse timer 
        add_new_energy_increase_timer();

        //create first disater timer 
        Disaster.add_disaster_timer();

         //update energy, co2 and other important varriables
        update_world();

        //loads main scene
        SceneManager.LoadScene("MainScene");
    }

    // map value point from one range to another 
     public static double remap (double value, double from1, double to1, double from2, double to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }



    //start new timer for money drop
    public static void add_new_money_timer(){
        int money_drop_time = Random.Range(money_drop_min, money_drop_max);
        GameTime.god_timer.Add("money", money_drop_time);

    }
    //start a timer to increase mininum energy needs
    public static void add_new_energy_increase_timer(){
        GameTime.god_timer.Add("energy", min_energy_increase_time);
    }



    //calculate and update popularity 
    public static void calc_popularity(){
        if(world_co2_production != 0){
            //calculate quality of co2 and energy production 
            double percent_till_doom = 1 - ((double)world_co2_total/(double)max_co2);
            double percent_surplus = (double) current_surplus/(double)world_energy_production; 


            //remap values between min and max values 
            double mapped_till_doom = remap(percent_till_doom, 0, 1, co2_pop_min, co2_pop_max); 
            double mapped_surplus = remap(percent_surplus, 0, 1, energy_pop_min, energy_pop_max); 

            //add energy and co2 quality to get total popularity 
            current_popularity = mapped_till_doom + mapped_surplus;

        }

    }


    //returns true if player lost game 
    public static bool if_lost(){ 
        //return true if co2 exceeds maximum
        if (world_co2_total >= max_co2){
            Debug.Log("co2 loss");
            return true; 
        //retrun ture if not producing enough energy 
        }else if (current_energy_needs >= world_energy_production){
            Debug.Log("energy loss");
            return true;
        }else{ 
            return false;
        }
    }



    //returns true if player won game 
    public static bool if_won(){ 
        //if reached net zero emmissions return true 
        if(world_co2_production == 0){
            return true;
        }else{
            return false;
        }
    }



    //update world energy, co2 production and popularity 
    public static void update_world(){ 
        
        //update the current world levels of energy and co2 
        int energy_count = 0; 
        int co2_count = 0; 
        foreach(KeyValuePair<string, Region> i in regions){
            energy_count += i.Value.energy_production; 
            co2_count += i.Value.co2_production; 
        }
        world_energy_production = energy_count; 
        world_co2_production = co2_count; 
        
        current_energy_needs = min_energy_needs + added_energy_needs;

        current_surplus  = world_energy_production - current_energy_needs;

        //update popularity
        calc_popularity();

        //check if player has lost or won the game
        if(if_lost()){
            is_time_passing = false;
            player_lost = true; 
            load_end();
        }else if (if_won()){
            is_time_passing = false;
            player_won = true; 
            load_end();
        }
        
    }

    //add a energy plant to region 
    public static void buy_energy_plant(string energy_name, int level, string region){
        //create new energy object 
        GameObject new_energy_ob = Instantiate(blank_energy_object_copy); 
        Energy new_energy_script = new_energy_ob.GetComponent<Energy>();
        new_energy_script.name = energy_name; 
        
        //initalize  starting variables 
        new_energy_script.energy_production = energy_production_by_name[energy_name]; 
        new_energy_script.co2_production = co2_production_by_name[energy_name];
        new_energy_script.level = level; 
        new_energy_script.energy_restriction = energy_restrictions[energy_name + "/"+ level.ToString()];
        new_energy_script.initalize();

        //add energy plant to region
        regions[region].add_energy_plant(new_energy_script);
    }

    //restrict energy of a certain type to a percentage
    public static void restrict_energy(string name, int level, float restriction){
        //restrict energy in all regions
        foreach (var i in regions){
            i.Value.apply_restriction(name, level, restriction);
        }

        //make all future plants also retricted
        energy_restrictions[name + "/" + level.ToString()] = restriction;
        update_world();
    }

    //Calculate how much energy would change under a restriction 
    public static int[] restriction_change(string name, int level, float restriction){
        int[] change = {0,0};
        foreach (var i in regions){
            int[] region_change = i.Value.restriction_energy_change(name, level, restriction); 
            change[0] += region_change[0]; 
            change[1] += region_change[1];
        }
        return change;
    }

   
    //runs when timer is completed 
    public static void timer_finished(string value){
        //if the money timer is ended, create new money button
        if(value == "money"){
            //calculate money to award
            float excess_energy = (float) current_surplus;
            float random = Random.Range(money_percent_min, money_percent_max);
            float money_awarded =  random * excess_energy;
            money_awarded = Mathf.Ceil(money_awarded);
            
            //create button
            if(money_awarded != 0){
                MoneyManager.create_button((int)money_awarded);
            }

            // start another timer 
            add_new_money_timer();

        //if energy timer is complete, increase min energy needs 
        }else if (value == "energy"){
            min_energy_needs += min_energy_increase_amount; 
            min_energy_increase_amount += min_energy_increase_amount_rate;

            update_world();

            //start new timer 
            add_new_energy_increase_timer();

        }
    }

    //wipes all nessesary variables at end of game 
    public static void clear_out(){
        GameTime.clear_out();
    }
        
    //display policy screen 
    public void display_policy(){
        //runs when policy button is pressed 
        is_time_passing = false;
        SceneManager.LoadScene("PolicyScreen");
    }
    //display research sceen 
    public void display_research(){ 
        is_time_passing = false;
        SceneManager.LoadScene("ResearchScreen");
    }
    //display building sceen 
    public void display_building(){ 
        is_time_passing = false;
        SceneManager.LoadScene("BuildScreen");
    }
    //goes back to main screen, used for back buttons 
    public void back_to_main(){
        selected_region = "World";
        is_time_passing = true;
        SceneManager.LoadScene("MainScene");
    }
    //duplicate for ease of use outside of script
    public static void static_back_to_main(){
        selected_region = "World";
        is_time_passing = true;
        SceneManager.LoadScene("MainScene");
    }
    //loads ending screen
    public static void load_end(){
        SaveGame.Save<bool>("Saved Game", false);
        SceneManager.LoadScene("EndScene");
        clear_out();
    }
    //loads opening screen 
    public void load_opening(){
        SceneManager.LoadScene("OpeningScene");
    }
    //loads opening screen outside of script
    public static void static_load_opening(){
        SceneManager.LoadScene("OpeningScene");
    }
    
}
