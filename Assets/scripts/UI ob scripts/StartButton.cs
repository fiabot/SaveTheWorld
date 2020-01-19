using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button this_button;
    // Start is called before the first frame update
    void Start()
    {
        this_button =  GetComponent<Button>();
        this_button.onClick.AddListener(God.start_game);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
