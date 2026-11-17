using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart_spawn : MonoBehaviour
{

    // Start is called before the first frame update
    public void next_cart()
    {
        
        //if (lt_v3.ContainsKey(v2_start_all))
        //{
            if (lt_v3[p1].Count > 0)
                lt_v3[p1].Dequeue();
            if (lt_v3[p1].Count == 0)
                lt_v3.Remove(p1);
       // }

        if (lt_v3.ContainsKey(p1) && lt_v3[p1].Count > 0)
            lt_v3[p1].Peek().active = true;
        /*
        foreach (Vector3 v3_ in cart_spawn.lt_v3.Keys)
        {
            Debug.Log(v3_ + " end_ " + cart_spawn.lt_v3[v3_].Count);
        }
        */
    }
    public void Destroyed()
    {

        anim.col_anim--;
        Destroy(gameObject);
    }
    // Update is called once per frame
    public bool bl_next_cart = false;
    bool bl_a = false;
    void FixedUpdate()
    {

        if (time_now < time)
        {
            float a = time_now / time;
            Vector2 pp1 = p1 + (p2 - p1) * a;
            Vector2 pp2 = p2 + (p3 - p2) * a;
            Vector2 pp = pp1 + (pp2 - pp1) * a;
            transform.position = pp;
            time_now += Time.deltaTime;
        }
        else
        {
            Destroyed();
        }
    }

    public float time_now = 0;
    public float time = 0;
    public GameObject parent;


    public static Dictionary<Vector3, Queue<GameObject>> lt_v3 = new Dictionary<Vector3, Queue<GameObject>>();


    public Vector2 p1;
    public Vector2 p2;
    public Vector2 p3;
    public void sets(float time, Vector3 v3_p1, Vector3 v3_p3)
    {



        time_now = 0;
        this.time = time;
        p1 = v3_p1;
        transform.position = p1;
        p2 = p1+Vector2.up*2 + new Vector2(Random.Range(-20, 20) , Random.Range(-10, 10)) / 20;
        p3 = v3_p3;
    }
}
