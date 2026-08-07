using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart_spawn : abs_physics
{
    // Start is called before the first frame update
    public void next_cart()
    {
        
        //if (lt_v3.ContainsKey(v2_start_all))
        //{
            if (lt_v3[v2_start_base].Count > 0)
                lt_v3[v2_start_base].Dequeue();
            if (lt_v3[v2_start_base].Count == 0)
                lt_v3.Remove(v2_start_base);
       // }

        if (lt_v3.ContainsKey(v2_start_base) && lt_v3[v2_start_base].Count > 0)
            lt_v3[v2_start_base].Peek().active = true;
        /*
        foreach (Vector3 v3_ in cart_spawn.lt_v3.Keys)
        {
            Debug.Log(v3_ + " end_ " + cart_spawn.lt_v3[v3_].Count);
        }
        */
    }
    public void Destroyed()
    {
        /*
        foreach (Vector3 v3_ in lt_v3.Keys)
        {
            Debug.Log(v3_+" "+lt_v3[v3_].Count);
        }
        */

        //lt_v3[v2_start_all].Peek().active = true;
        /*
        if (lt_v3[v2_start_all].Count > 0)
            lt_v3[v2_start_all].Dequeue();
        if (lt_v3[v2_start_all].Count == 0)
            lt_v3.Remove(v2_start_all);
        if (lt_v3.ContainsKey(v2_start_all) && lt_v3[v2_start_all].Count > 0)
            lt_v3[v2_start_all].Peek().active = true;
        */
        anim.col_anim--;
        Destroy(gameObject);
    }
    // Update is called once per frame
    public bool bl_next_cart = false;
    bool bl_a = false;
    void FixedUpdate()
    {

        base.FixedUpdate();

        if (time_now >= time)
            if (!bl_a)
            {
                bl_a = true;
                sets(0.5f,true, parent.transform.position);
                
            }
            else
                Destroyed();
        else if(time_now>=0.1f && !bl_next_cart)
        {
            //next_cart();
            bl_next_cart = true;
        }
            
    }
}
