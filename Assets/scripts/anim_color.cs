using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class anim_color : MonoBehaviour
{
    // Start is called before the first frame update

    public static Dictionary<par_cart.type_cart, Color> static_colors = new Dictionary<par_cart.type_cart, Color>();
    public Queue<par_cart.type_cart> lt_colors = new Queue<par_cart.type_cart>();
    public Queue<par_cart.type_cart> lt_colors_now = new Queue<par_cart.type_cart>();

    public float time_now =1;
    public Color d_color = new Color(0,0,0);
    public Color final_color = new Color(0, 0, 0);

    public void init(par_cart.type_cart tp)
    {
        //Debug.Log(tp);
        final_color = static_colors[tp];
        gameObject.GetComponent<SpriteRenderer>().color = final_color;
                
        lt_colors_now = new Queue<par_cart.type_cart>(lt_colors);
        //gameObject.GetComponent<SpriteRenderer>().color = final_color;
        tp = lt_colors_now.Dequeue();
        Color cl = static_colors[tp];

        d_color = (cl - final_color) * Time.deltaTime;
        //Debug.Log(tp + " " +d_color+"  "+tp);
        final_color = cl;
    }
    void Start()
    {

        //lt_colors.Enqueue(par_cart.type_cart.damage);
        //lt_colors.Enqueue(par_cart.type_cart.fire);
        //lt_colors.Enqueue(par_cart.type_cart.bases);
        //final_color = static_colors[lt_colors.Peek()];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (time_now < 0)
        {
            time_now += Time.deltaTime;
            if (time_now >= 0)
            {
                time_now = 0;

            }
        }
        else
        {
            //Debug.Log(gameObject.GetComponent<SpriteRenderer>().color);
            time_now += Time.deltaTime;
            Color32 cl = gameObject.GetComponent<SpriteRenderer>().color;
            cl +=d_color;
            //Debug.Log(cl.ConvertTo<Color32>());
            ///d_color = d_color;
            gameObject.GetComponent<SpriteRenderer>().color = cl;
        }
        if (time_now >= 1)
        {
            if (lt_colors_now.Count == 0)
            {
                lt_colors_now = new Queue<par_cart.type_cart>(lt_colors);

            }
            if (lt_colors.Count != 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = final_color;
                par_cart.type_cart tp = lt_colors_now.Dequeue();
                Color cl = static_colors[tp];

                d_color = (cl - final_color) * Time.deltaTime;
                //Debug.Log(tp + " " +d_color+"  "+tp);
                final_color = cl;
                time_now = -0.5f;
            }

        }
    }
}
