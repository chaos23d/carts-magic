using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class par_cart_art : par_cart
{
    public enum type_q
    {
        standart,
        stop_more_1,
    }
    public enum type_perez
    {
        standart,damage,use_cart,perehod_lvl
    }
    // Start is called before the first frame update
    public type_perez tp_perez = type_perez.standart;
    public int perez;
    public int perez_now;
    public int q;
    public int q_max;
    public int q_sum=1;
    public type_q type_q_now=type_q.standart;
    public bool first = true;
    void Start()
    {
        base.Start();
    }
}
