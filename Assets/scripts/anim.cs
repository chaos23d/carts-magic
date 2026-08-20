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
            func.test_message = "col anim is "+value;
            Debug.Log("col_anim is " + value);

            cl_an = value;
            if(cl_an==0 && next_dialog)
            {
                par_dialog.id_now++;
                par_dialog.par_dialog_now.start_dialog();
                next_dialog = false;
            }
        }
    }
    public static bool next_dialog=false;
    private static int cl_an = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
