using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class par_baf : parametrs
{
    // Start is called before the first frame update
    public enum type_baf
    {
        fire,
        poition,
        energy,
        ice,
        seal,
        bar,

        bar_rock,
        bar_fire,
        bar_blood,
        bar_energy,
        bar_ice,
        bar_poition,

        rock_bar,
        fire_bar,
        blood_bar,
        energy_bar,
        ice_bar,
        poition_bar,

        rage,
        berserk,
        seal_spetial
    }

    public int long_baf = 0;
    public int power = 0;
    public type_baf type;

    public List<string> lt_cart= new List<string>();

    public bool passive = true;
    public bool before = true;
    public bool skip_first = false;

    public GameObject parent = null;
    public GameObject gm_anim = null;
    public void baf()
    {
        //Debug.Log("is baf");
        if (long_baf != -1)
        {
            long_baf--;
            if (long_baf <= 0)
                Destroy(this);
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
