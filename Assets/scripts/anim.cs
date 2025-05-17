using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    // Start is called before the first frame update
    public static int col_anim {
        get
        {
            return cl_an;
        }
        set
        {
            Debug.Log("col_anim is " + value);
            cl_an = value;
        }
    }
    private static int cl_an = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
