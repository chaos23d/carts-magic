using System.Collections.Generic;
using UnityEngine;

public abstract class abs_physics : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed_now;
    public Vector3 v2_end;
    public Vector3 v2_start;
    public Vector3 v2_start_base;
    public float a;
    
    public float time_now = 0;
    public float time = 0;
    public GameObject parent;


    public static Dictionary<Vector3, Queue<GameObject>> lt_v3 = new Dictionary<Vector3, Queue<GameObject>>();
    public void sets(float time, bool bl, Vector3 v3)
    {
        //time++;
        //
        v2_end = v3;
        v2_start = transform.position;
        this.time = time;
        time_now = 0;

        Vector3 v3_ = (v2_end - v2_start);
        float c = Mathf.Pow(v3_.x * v3_.x + v3_.y * v3_.y, 0.5f) * 2;
        float speed_st, speed_end;
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

        a = (speed_end - speed_st) / time;
        speed_now = speed_st;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FixedUpdate()
    {
        if (time_now < time) {
            Vector3 v3 = (v2_end - v2_start);
            float c = Mathf.Pow(v3.x * v3.x + v3.y * v3.y, 0.5f);
            if (c != 0)
            {
                v3 = new Vector3((float)v3.x / c, (float)v3.y / c, 0);
                transform.position += v3 * Time.fixedDeltaTime * speed_now;

            }

            speed_now += Time.fixedDeltaTime * a;
            time_now += Time.fixedDeltaTime;
        }

        else { 
            transform.position = new Vector3(v2_end.x,v2_end.y, transform.position.z);
        }



    }
}
