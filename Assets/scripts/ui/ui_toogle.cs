using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ui_toogle : MonoBehaviour
{
    // Start is called before the first frame update
    public int num = 0;

    public void mob_is_wolked()
    {
        //int q = GameObject.Find("Main Camera").transform.parent.gameObject.GetComponent<par_mob>().num;
        Debug.Log(num + " " + func.mob_list[func.hod_mob_now_int].GetComponent<par_mob>().wolked);
        if (gameObject.active && anim.col_anim==0 &&  func.mob_list[num].GetComponent<par_mob>().friend)
        {
            //Debug.Log(num + " wolk 1 " + func.mob_list[func.hod_mob_now_int].GetComponent<par_mob>().wolked);
            func.mob_list[num].GetComponent<par_mob>().wolked = !func.mob_list[num].GetComponent<par_mob>().wolked;
            if (!func.mob_list[func.hod_mob_now_int].GetComponent<par_mob>().wolked && func.hod_mob_now_int==num)
            {
                func.next_hod();
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
