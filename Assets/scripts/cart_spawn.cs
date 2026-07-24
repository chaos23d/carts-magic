using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart_spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed_now;
    public Vector3 v2_end;
    public Vector3 v2_start;
    public Vector3 v2_start_all;
    public float a;
    bool bl_a = false;
    public float time_now = 0;
    public float time = 0;
    public GameObject parent;


    public static Dictionary<Vector3,Queue<GameObject>> lt_v3 = new Dictionary<Vector3, Queue<GameObject>>();
    public void sets(float time,bool bl,Vector3 v3)
    {
        //time++;
        //
        v2_end = v3;
        v2_start = transform.position;
        this.time = time;
        time_now = 0;
        
        Vector3 v3_ = (v2_end - v2_start);
        float c = Mathf.Pow(v3_.x * v3_.x + v3_.y * v3_.y, 0.5f)*2;
        float speed_st,speed_end;
        if (bl)
        {
            speed_st = 0;
            speed_end = c / time;
        }
        else
        {
            speed_end = 0;
            speed_st = c / time;
        }

        a =(speed_end - speed_st )/ time;
        speed_now = speed_st;
    }
    public void next_cart()
    {
        
        //if (lt_v3.ContainsKey(v2_start_all))
        //{
            if (lt_v3[v2_start_all].Count > 0)
                lt_v3[v2_start_all].Dequeue();
            if (lt_v3[v2_start_all].Count == 0)
                lt_v3.Remove(v2_start_all);
       // }

        if (lt_v3.ContainsKey(v2_start_all) && lt_v3[v2_start_all].Count > 0)
            lt_v3[v2_start_all].Peek().active = true;
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
    void FixedUpdate()
    {

        Vector3 v3 = (v2_end - v2_start);
        float c = Mathf.Pow(v3.x * v3.x + v3.y * v3.y, 0.5f);
        v3 = new Vector3((float)v3.x / c, (float)v3.y / c, 0);
        transform.position += v3 * Time.fixedDeltaTime * speed_now;
        speed_now += Time.fixedDeltaTime*a;
        time_now += Time.fixedDeltaTime;

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
