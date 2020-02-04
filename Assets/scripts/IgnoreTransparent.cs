using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IgnoreTransparent : MonoBehaviour
{
    public Image toggleImage;
    public Image checkImage;
    // Start is called before the first frame update
    void Start()
    {
        toggleImage.alphaHitTestMinimumThreshold = 0.5f;
        checkImage.alphaHitTestMinimumThreshold = 0.5f;
    }


}
