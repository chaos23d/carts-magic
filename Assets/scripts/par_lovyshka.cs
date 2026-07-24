using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class par_lovyshka : MonoBehaviour
{
    // Start is called before the first frame update
    public int x;
    public int y;
    public int time = 0;
    public int power = 0;
    public bool skip_first = false;
    public bool skip_zero = true;

    public int num_active = 0;
    public bool before_hod = true;

    public bool in_mob = true;

    public List<string> par_carts = new List<string>();
    public Stack<string> st_par = new Stack<string>();
    public GameObject parent;
    public GameObject activate()
    {
        GameObject gm = null;

        
        if (gameObject.name == "run blood" || gameObject.name == "run fire" || gameObject.name == "run poition" || gameObject.name == "run rock" || gameObject.name == "run energy" || gameObject.name == "run ice")
        {
            par_mob mb = null;
            if (parent != null)
            {
                mb = parent.GetComponent<par_mob>();
            }
            if(mb!=null && func.distance(mb.x,mb.y,x,y) <=3)
                mb.spawn_cart(par_carts[0], gameObject);
        }

        /*
        if(gameObject.name== "spirit fire")
        {
            if(func.mob[x,y]!=null)
                gm=func.create_effect("cart fire", x, y, parent, true, 1, new List<string> { "cart fire"}) ;
        }
        else if (gameObject.name == "spirit ice")
        {
            if (func.mob[x, y] != null)
                gm = func.create_effect("cart ice", x, y, parent, true, 1, new List<string> { "cart ice" });
        }
        else if (gameObject.name == "spirit poition")
        {
            if (func.mob[x, y] != null)
                gm = func.create_effect("cart poition", x, y, parent, true, 1, new List<string> { "cart poition"});
        }
        else if (gameObject.name == "spirit rock")
        {
            if (func.mob[x, y] != null)
                gm = func.create_effect("spirit rock", x, y, parent, true, 1, new List<string> { "cart rock"});
        }
        else if (gameObject.name == "spirit energy")
        {
            if (func.mob[x, y] != null)
                gm = func.create_effect("spirit energy", x, y, parent, true, 1, new List<string> { "cart energy" });
        }
        else if (gameObject.name == "spirit blood")
        {
            if (func.mob[x, y] != null)
                gm = func.create_effect("cart blood", x, y, parent, true, 1, new List<string> { "cart blood"});
        }
        */
        if (gameObject.name == "spirit base")
        {
            //if (func.mob[x, y] != null)
                gm = func.create_effect(func.get_name_effect(par_carts,null), x, y, parent, true, 1, new List<string> (par_carts));
        }
        else if (gameObject.name == "lov bomb" && time==1)
        {
            //st_par.
            GameObject g=func.create_effect("bomb destroy", x, y, parent, true, power, par_carts);
            g.GetComponent<par_effect>().st_par = st_par;
            List<string> l = new List<string>(par_carts);
            g.GetComponent<SpriteRenderer>().color = anim_color.static_colors[func.baza[l[1]].GetComponent<par_cart>().type];
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        }

        return gm;
    }
    public void update()
    {
        Debug.Log("update lov");
        time--;
    }
    public void del()
    {
        func.lovyshka_now.Remove(gameObject);
        if (!before_hod)
        {
            func.lovyshka_after.Remove(gameObject);
        }
        func.lt_lovyshka[x, y].Remove(gameObject);
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
