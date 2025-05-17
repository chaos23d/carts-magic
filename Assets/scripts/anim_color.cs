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

    public float time_now = -0.5f;
    public Color d_color = new Color(0,0,0);
    public Color final_color = new Color(0, 0, 0);
    void Start()
    {
        if (static_colors.Count == 0)
        {
            static_colors.Add(par_cart.type_cart.bases, new Color32 (255, 255, 255,255));

            static_colors.Add(par_cart.type_cart.damage, new Color32(189, 104, 255, 255));
            static_colors.Add(par_cart.type_cart.sp_barier, new Color32(255, 104, 177, 255));

            static_colors.Add(par_cart.type_cart.blood, new Color32(219, 11, 11, 255));
            static_colors.Add(par_cart.type_cart.fire, new Color32(255, 160, 29, 255));
            static_colors.Add(par_cart.type_cart.energy, new Color(123, 191, 255, 255));
            static_colors.Add(par_cart.type_cart.ice, new Color32(229, 252, 255, 255));
            static_colors.Add(par_cart.type_cart.poition, new Color32(96, 229, 83, 255));
            static_colors.Add(par_cart.type_cart.rock, new Color32(123, 83, 59, 255));

            static_colors.Add(par_cart.type_cart.spirit_blood, static_colors[par_cart.type_cart.blood]);
            static_colors.Add(par_cart.type_cart.spirit_fire, static_colors[par_cart.type_cart.fire]);
            static_colors.Add(par_cart.type_cart.spirit_energy, static_colors[par_cart.type_cart.energy]);
            static_colors.Add(par_cart.type_cart.spirit_ice, static_colors[par_cart.type_cart.ice]);
            static_colors.Add(par_cart.type_cart.spirit_poition, static_colors[par_cart.type_cart.poition]);
            static_colors.Add(par_cart.type_cart.spirit_rock, static_colors[par_cart.type_cart.rock]);
        }
        //lt_colors.Enqueue(par_cart.type_cart.damage);
        //lt_colors.Enqueue(par_cart.type_cart.fire);
        //lt_colors.Enqueue(par_cart.type_cart.bases);
        final_color = static_colors[par_cart.type_cart.bases];
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
            gameObject.GetComponent<SpriteRenderer>().color = final_color;
            par_cart.type_cart tp = lt_colors_now.Dequeue();
            Color cl =static_colors[tp];

            d_color = (cl - final_color)*Time.deltaTime;
            //Debug.Log(tp + " " +d_color+"  "+tp);
            final_color = cl;
            time_now = -0.5f;
        }
    }
}
