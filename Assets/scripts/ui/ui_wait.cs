using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_wait : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.col_anim == 0 )
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        else if (anim.col_anim != 0 )
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
