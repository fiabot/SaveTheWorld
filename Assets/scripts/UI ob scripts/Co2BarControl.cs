using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Co2BarControl : MonoBehaviour
{
    public Image barImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barImage.fillAmount = (float) God.world_co2_total / (float) God.max_co2;
    }
}
