using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static par_cart;

public class par_effect : MonoBehaviour
{
    // Start is called before the first frame update
    public bool bl_que = false;
    public List<string> cart_par = new List<string>();
    public int power = 1;
    public GameObject parent;
    public Stack<string> st_par = new Stack<string>();
    public string name_in_lt;
    public int x;
    public int y;

    public Sprite spr_baf;

    
    void Start()
    {
        if (parent != null && gameObject.name!="spawn cart")
        {
            foreach (Transform tr in parent.transform.Find("carts active"))
            {
                Destroy(tr.gameObject);
            }
        }
    }
    public void Destroyed()
    {
        par_mob mb = null;
        if (parent != null)
            mb = parent.GetComponent<par_mob>();
        //if()
        if(gameObject.name== "activate art")
        {
            Vector3 v3 = transform.position;
            if (cart_spawn.lt_v3[v3].Count > 0)
                cart_spawn.lt_v3[v3].Dequeue();
            if (cart_spawn.lt_v3[v3].Count == 0)
                cart_spawn.lt_v3.Remove(v3);
            if (cart_spawn.lt_v3.ContainsKey(v3) && cart_spawn.lt_v3[v3].Count > 0)
                cart_spawn.lt_v3[v3].Peek().active = true;

            foreach (Vector3 v3_ in cart_spawn.lt_v3.Keys)
            {
                Debug.Log(v3_ + " end " + cart_spawn.lt_v3[v3_].Count);
            }
        }

        //Debug.Log(func.que_effects[x, y].Count+"  queue");
        //if(func.que_effects[x, y].Count>0)
        //Debug.Log(x + "  " + y+"    "+ func.que_effects[x, y].Peek());
        if (bl_que)
        {
            func.que_effects[x, y].Dequeue();
            if (func.que_effects[x, y].Count != 0)
            {
                GameObject gm = func.que_effects[x, y].Peek();
                //Debug.Log(gm.name + "  queue 1");
                gm.active = true;
                //parent.GetComponent<par_mob>().gggggg = gm;
                //Debug.Log(gm.active + "  queue 2");
            }
        }
        anim.col_anim--;
        Destroy(gameObject);
    }
    public void activate()
    {
        string player_name = data.data_now.pl_name_now;
        bool bl_pl = (parent == GameObject.Find(player_name));
        //if (player_name == "berserk player" && bl_pl)
        //power *= 2;
        par_cart_art art_bar = null;
        par_cart_art art_att = null;

        par_cart_art art_dungalorg_boss = null;

        par_mob mb = null;
        par_mob mb_par= null;


        List<par_baf> lt_del_bafs = new List<par_baf>();
        List<par_baf.type_baf> lt_del_bafs_type = new List<par_baf.type_baf>();

        List<par_cart.type_cart> lt_type_carts = cart_par.ConvertAll<par_cart.type_cart>(n =>
        {
            return GameObject.Find(n).GetComponent<par_cart>().type;
        });


        if (gameObject.name == "berserk ef")
        {
            mb = func.mob[x, y].GetComponent<par_mob>();
            if (mb != null)
                mb.add_baf(par_baf.type_baf.berserk, power, 1);
            return;
        }
        else if (gameObject.name == "create cave derlorg" && func.nature[x, y] == null)
        {
            par_nat nt = func.create("cave derlorg", x, y).GetComponent<par_nat>();
            nt.gameObject.active = func.eath[x, y].active;
            return;
        }

        if (func.mob[x, y] != null)
        {
            mb = func.mob[x, y].GetComponent<par_mob>();
        }
        if (parent != null)
        {
            mb_par = parent.GetComponent<par_mob>();
        }
        if (mb != null)
        {
            if (lt_type_carts[0]!= par_cart.type_cart.derlorg)
            {
                foreach (par_cart_art art in mb.lt_carts_art)
                {
                    if(art.name== "cart dungalorg heard")
                    {
                        for (int i = 0; i < cart_par.Count; i++)
                        {
                            //mb.
                            Stack<object> st = new Stack<object>();
                            st.Push("par spawn damage");
                            mb.activate_art(art,x,y,st);
                        }
                    }
                    else if (art.name == "cart potoc blood")
                    {
                        Stack<object> st = new Stack<object>();
                        st.Push(cart_par);
                        st.Push("par spawn blood");
                        mb.activate_art(art, x, y, st);
                    }
                    else if (art.name == "cart potoc rock")
                    {
                        Stack<object> st = new Stack<object>();
                        st.Push(cart_par);
                        st.Push("par spawn rock");
                        mb.activate_art(art, x, y, st);
                    }
                    else if (art.name == "cart potoc ice")
                    {
                        Stack<object> st = new Stack<object>();
                        st.Push(cart_par);
                        st.Push("par spawn ice");
                        mb.activate_art(art, x, y, st);
                    }
                    else if (art.name == "cart potoc energy")
                    {
                        Stack<object> st = new Stack<object>();
                        st.Push(cart_par);
                        st.Push("par spawn energy");
                        mb.activate_art(art, x, y, st);
                    }
                    else if (art.name == "cart potoc fire")
                    {
                        Stack<object> st = new Stack<object>();
                        st.Push(cart_par);
                        st.Push("par spawn fire");
                        mb.activate_art(art, x, y, st);
                    }
                    else if (art.name == "cart potoc poition")
                    {
                        Stack<object> st = new Stack<object>();
                        st.Push(cart_par);
                        st.Push("par spawn poition");
                        mb.activate_art(art, x, y, st);
                    }
                    /*
                    else if (art.name == "cart shaman boss" && art.perez_now==0)
                    {
                        foreach (string st_cart in cart_par)
                        {
                            par_cart cart = GameObject.Find(st_cart).GetComponent<par_cart>();
                            GameObject gm_l = null;
                            Debug.Log("true " + st_cart);
                            if (cart.type == par_cart.type_cart.rock)
                            {
                                gm_l = func.create_lovyshka(x, y, "run rock", parent, cart.lvl, 1);
                                gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart rock");
                            }
                                
                            else if (cart.type == par_cart.type_cart.blood)
                            {
                                gm_l = func.create_lovyshka(x, y, "run blood", parent, cart.lvl, 1);
                                gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart blood");
                            }
                                
                            else if (cart.type == par_cart.type_cart.fire)
                            {
                                gm_l = func.create_lovyshka(x, y, "run fire", parent, cart.lvl, 1);
                                gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart fire");
                            }
                                
                            else if (cart.type == par_cart.type_cart.poition)
                            {
                                gm_l = func.create_lovyshka(x, y, "run poition", parent, cart.lvl, 1);
                                gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart poition");
                            }
                                
                            else if (cart.type == par_cart.type_cart.ice)
                            {
                                gm_l = func.create_lovyshka(x, y, "run ice", parent, cart.lvl, 1);
                                gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart ice");
                            }
                                
                            else if (cart.type == par_cart.type_cart.energy)
                            {
                                gm_l = func.create_lovyshka(x, y, "run energy", parent, cart.lvl, 1);
                                gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart energy");
                            }
                                
                        }
                    }
                    */
                }
            }
            foreach (par_cart_art art in mb.lt_carts_art)
            {
                if (art.name == "cart dungalorg(boss)")
                {
                    art_dungalorg_boss = art;
                }
            }
                /*
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart bar st" && cart.perez_now == 0)
                    {
                        art_bar = cart;
                    }

                }
                foreach (par_cart_art cart in parent.GetComponent<par_mob>().lt_carts_art)
                {
                    if (cart.name == "cart att st" && cart.perez_now == 0)
                    {
                        art_att = cart;
                    }

                }
                */
                /*
                int dop_dm_metca = 1;
            foreach (par_baf baf in mb.lt_bafs)
            {
                if (baf.type == par_baf.type_baf.rage)
                {
                    dop_dm_metca ++;
                }
            }
                */
            foreach (par_baf baf in mb.lt_bafs)
            {
                bool bl = false;
                par_cart.type_cart tp0 = GameObject.Find(cart_par[0]).GetComponent<par_cart>().type;
                if(cart_par[0] != "cart derlorg" && cart_par[0] != "cart derlorg 1" && cart_par[0] != "cart tuman(boss)")
                {
                    if (baf.type == par_baf.type_baf.bar && tp0== par_cart.type_cart.damage)
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_rock && tp0 == par_cart.type_cart.damage)
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_blood && tp0 == par_cart.type_cart.damage)
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_fire && tp0 == par_cart.type_cart.damage)
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_energy && tp0 == par_cart.type_cart.damage)
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_poition && tp0 == par_cart.type_cart.damage)
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_ice && tp0 == par_cart.type_cart.damage)
                    {
                        bl = true;
                    }
                }



                /*
                if (bl)
                {
                    if (art_bar == null && art_att != null)
                    {
                        //power = 0;

                        parent.GetComponent<par_mob>().activate_art(art_att, mb.x, mb.y);
                        bl = false;
                        //art_bar.perez_now = art_bar.perez;
                    }
                    else if (art_bar != null && art_att == null)
                    {
                        power = 0;

                        mb.activate_art(art_bar, mb.x, mb.y);
                        //art_bar.perez_now = art_bar.perez;
                    }
                    else if (art_bar != null && art_att != null)
                    {
                        //power = 0;
                        parent.GetComponent<par_mob>().activate_art(art_att, mb.x, mb.y);
                        mb.activate_art(art_bar, mb.x, mb.y);
                        //art_bar.perez_now = art_bar.perez;
                    }

                }
                */


                if (bl)
                {
                    //power *= dop_dm_metca;
                    int a = power;
                    power -= baf.power;
                    baf.power -= a;

                    if (baf.power <= 0)
                    {
                        lt_del_bafs.Add(baf);
                        lt_del_bafs_type.Add(baf.type);
                    }
                    if (power < 0)
                        power = 0;
                    //else
                        //power /= dop_dm_metca;
                }
            }
            while (lt_del_bafs.Count > 0)
            {
                par_baf bf_del = lt_del_bafs[0];
                mb.lt_bafs.Remove(bf_del);
                lt_del_bafs_type.RemoveAt(0);
                if (!lt_del_bafs_type.Contains(bf_del.type))
                {
                    if (bf_del.type == par_baf.type_baf.bar)
                    {
                        Destroy(mb.gameObject.transform.Find("bar bases").gameObject);
                    }

                    else if (bf_del.type == par_baf.type_baf.bar_rock)
                    {
                        Destroy(mb.gameObject.transform.Find("bar bases rock").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.bar_energy)
                    {
                        Destroy(mb.gameObject.transform.Find("bar bases energy").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.bar_blood)
                    {
                        Destroy(mb.gameObject.transform.Find("bar bases blood").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.bar_fire)
                    {
                        Destroy(mb.gameObject.transform.Find("bar bases fire").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.bar_ice)
                    {
                        Destroy(mb.gameObject.transform.Find("bar bases ice").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.bar_poition)
                    {
                        Destroy(mb.gameObject.transform.Find("bar bases poition").gameObject);
                    }

                    else if (bf_del.type == par_baf.type_baf.rock_bar)
                    {
                        Destroy(mb.gameObject.transform.Find("bar rock").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.energy_bar)
                    {
                        Destroy(mb.gameObject.transform.Find("bar energy").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.blood_bar)
                    {
                        Destroy(mb.gameObject.transform.Find("bar blood").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.fire_bar)
                    {
                        Destroy(mb.gameObject.transform.Find("bar fire").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.ice_bar)
                    {
                        Destroy(mb.gameObject.transform.Find("bar ice").gameObject);
                    }
                    else if (bf_del.type == par_baf.type_baf.poition_bar)
                    {
                        Destroy(mb.gameObject.transform.Find("bar poition").gameObject);
                    }
                }
                Destroy(bf_del);
                lt_del_bafs.RemoveAt(0);
            }
        }
        if (parent != null)
        {
            foreach (par_cart_art art in parent.GetComponent<par_mob>().lt_carts_art)
            {

                if (art.name == "cart shaman boss" && art.perez_now == 0)
                {
                    
                    foreach (string st_cart in cart_par)
                    {
                        //Debug.Break();
                        par_cart cart = GameObject.Find(st_cart).GetComponent<par_cart>();
                        GameObject gm_l = null;
                        Debug.Log("true " + st_cart);
                        if (cart.type == par_cart.type_cart.rock || cart.type == par_cart.type_cart.spirit_rock)
                        {
                            gm_l = func.create_lovyshka(x, y, "run rock", parent, cart.lvl, 1, true);
                            gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart rock");
                        }

                        else if (cart.type == par_cart.type_cart.blood || cart.type == par_cart.type_cart.spirit_blood)
                        {
                            gm_l = func.create_lovyshka(x, y, "run blood", parent, cart.lvl, 1, true);
                            gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart blood");
                        }

                        else if (cart.type == par_cart.type_cart.fire || cart.type == par_cart.type_cart.spirit_fire)
                        {
                            gm_l = func.create_lovyshka(x, y, "run fire", parent, cart.lvl, 1, true);
                            gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart fire");
                        }

                        else if (cart.type == par_cart.type_cart.poition || cart.type == par_cart.type_cart.spirit_poition)
                        {
                            gm_l = func.create_lovyshka(x, y, "run poition", parent, cart.lvl, 1, true);
                            gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart poition");
                        }

                        else if (cart.type == par_cart.type_cart.ice || cart.type == par_cart.type_cart.spirit_ice)
                        {
                            gm_l = func.create_lovyshka(x, y, "run ice", parent, cart.lvl, 1, true);
                            gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart ice");
                        }

                        else if (cart.type == par_cart.type_cart.energy || cart.type == par_cart.type_cart.spirit_energy)
                        {
                            gm_l = func.create_lovyshka(x, y, "run energy", parent, cart.lvl, 1,true);
                            gm_l.GetComponent<par_lovyshka>().par_carts.Add("cart energy");
                        }

                    }
                }
            }
        }

        if (gameObject.name == "crystall create"  && power > 0)
        {
            //par_nat nt = func.create("crystall", x, y).GetComponent<par_nat>();
            //nt.gameObject.active = func.eath[x, y].active;
            //nt.type = power.ToString();
            Debug.Log("ef cr");
            List<string> lt_ = new List<string>(cart_par);
            Queue<par_cart.type_cart> lt_que = new Queue<par_cart.type_cart>();


            lt_.RemoveAt(0);

            foreach (string st in lt_)
            {
                lt_que.Enqueue(GameObject.Find(st).GetComponent<par_cart>().type);
            }
            lt_que.Enqueue(par_cart.type_cart.bases);
            lt_que.Enqueue(par_cart.type_cart.bases);
            //lt_que.Enqueue(par_cart.type_cart.bases);

            GameObject l_g = func.create_lovyshka(x, y, "mini crystall", parent, 1, power);
            par_lovyshka l =l_g.GetComponent<par_lovyshka>();
            l_g.GetComponent<anim_color>().lt_colors = lt_que;
            l.par_carts = cart_par;
            Stack<string> pr= new Stack<string>();
            pr.Push(func.get_name_effect(lt_, null));
            l.st_par = pr;
            
        }
        else if (gameObject.name == "crystall destroy" && power > 0)
        {
            string name= st_par.Peek();
            //if (dop_ef != null)
            //{
            //ef_cr.GetComponent<par_effect>().st_par.Push(dop_ef);
            //ef_cr.GetComponent<par_effect>().st_par.Push("par pow");
            //}

            //par_nat nt = func.create("crystall", x, y).GetComponent<par_nat>();
            //nt.gameObject.active = func.eath[x, y].active;
            //nt.type = power.ToString();
            cart_par.RemoveAt(0);
            func.create_effect(name, x , y, parent, true, power*2, cart_par);
            if(x>0)
                func.create_effect(name, x-1, y, parent, true, power, cart_par);
            if (y > 0)
                func.create_effect(name, x, y-1, parent, true, power, cart_par);
            if (x <func.size_global-1)
                func.create_effect(name, x + 1, y, parent, true, power, cart_par);
            if (y< func.size_global - 1)
                func.create_effect(name, x, y+1, parent, true, power, cart_par);
        }
        /*
        if (gameObject.name == "destroy crystall" && st_par.Pop() == "par_crystall")
        {

            //int dop_pow_base = int.Parse(func.nature[x, y].GetComponent<par_nat>().type)+ int.Parse(st_par.Pop());
            //st_par.Pop();
            int dop_pow_base = int.Parse(func.nature[x, y].GetComponent<par_nat>().type) + int.Parse(st_par.Pop());
            Debug.Log("dop pow base= " + dop_pow_base);
            GameObject gm = func.nature[x, y];
            func.nature[x, y] = null;

            //dop_pow+=int.Parse(st_par.Pop());
            int dop_pow = dop_pow_base;
            string ef = st_par.Pop();
            string dop_ef = null;
            if (st_par.Count > 1 && st_par.Pop() == "par pow")
                dop_ef = st_par.Pop();
            if (x > 0)
            {
                //Debug.Log("x -1 " + x + " " + y);
                List<GameObject> lt_del = new List<GameObject>();
                Queue<GameObject> que1 = new Queue<GameObject>();
                //bool bl1_ef = false;

                foreach (GameObject gm1 in func.que_effects[x - 1, y])
                {
                    if (func.cart_effects[ef].name == gm1.name)
                    {
                        lt_del.Add(gm1);
                        //Debug.Log(gm1.name);
                    }
                    else
                    {
                        que1.Enqueue(gm1);
                    }
                }
                //Debug.Log("x -1 " + x + " " + y + "   " + lt_del.Count);

                while (lt_del.Count > 0)
                {

                    //if(lt_del[0].name==)
                    dop_pow += lt_del[0].GetComponent<par_effect>().power;

                    lt_del[0].GetComponent<par_effect>().bl_que = false;
                    lt_del[0].active = true;
                    lt_del[0].GetComponent<par_effect>().Destroyed();
                    lt_del.RemoveAt(0);
                }

                //dop_pow += dop_pow_base;
                func.que_effects[x - 1, y] = que1;
                GameObject ef_cr= func.create_effect(ef, x - 1, y, parent, true, dop_pow, cart_par);
                ef_cr.active = true;
                if (dop_ef != null)
                {
                    ef_cr.GetComponent<par_effect>().st_par.Push(dop_ef);
                    ef_cr.GetComponent<par_effect>().st_par.Push("par pow");
                }
            }
            dop_pow = dop_pow_base;

            if (y > 0)
            {
                //Debug.Log("x -1 " + x + " " + y);
                List<GameObject> lt_del = new List<GameObject>();
                Queue<GameObject> que1 = new Queue<GameObject>();
                //bool bl1_ef = false;

                foreach (GameObject gm1 in func.que_effects[x, y - 1])
                {
                    if (func.cart_effects[ef].name == gm1.name)
                    {
                        lt_del.Add(gm1);
                        //Debug.Log(gm1.name);
                    }
                    else
                    {
                        que1.Enqueue(gm1);
                    }
                }
                //Debug.Log("x -1 " + x + " " + y + "   " + lt_del.Count);
                while (lt_del.Count > 0)
                {

                    //if(lt_del[0].name==)
                    dop_pow += lt_del[0].GetComponent<par_effect>().power;

                    lt_del[0].GetComponent<par_effect>().bl_que = false;
                    lt_del[0].active = true;
                    lt_del[0].GetComponent<par_effect>().Destroyed();
                    lt_del.RemoveAt(0);
                }

                //dop_pow += dop_pow_base;
                func.que_effects[x, y - 1] = que1;
                GameObject ef_cr = func.create_effect(ef, x, y-1, parent, true, dop_pow, cart_par);
                ef_cr.active = true;
                if (dop_ef != null)
                {
                    ef_cr.GetComponent<par_effect>().st_par.Push(dop_ef);
                    ef_cr.GetComponent<par_effect>().st_par.Push("par pow");
                }
            }
            dop_pow = dop_pow_base;

            if (x < func.size_global - 1)
            {
                //Debug.Log("x -1 " + x + " " + y);
                List<GameObject> lt_del = new List<GameObject>();
                Queue<GameObject> que1 = new Queue<GameObject>();
                //bool bl1_ef = false;
                foreach (GameObject gm1 in func.que_effects[x + 1, y])
                {
                    if (func.cart_effects[ef].name == gm1.name)
                    {
                        lt_del.Add(gm1);
                        //Debug.Log(gm1.name);
                    }
                    else
                    {
                        que1.Enqueue(gm1);
                    }
                }
                //Debug.Log("x -1 " + x + " " + y + "   " + lt_del.Count);
                while (lt_del.Count > 0)
                {

                    //if(lt_del[0].name==)
                    dop_pow += lt_del[0].GetComponent<par_effect>().power;

                    lt_del[0].GetComponent<par_effect>().bl_que = false;
                    lt_del[0].active = true;
                    lt_del[0].GetComponent<par_effect>().Destroyed();
                    lt_del.RemoveAt(0);
                }
                //dop_pow += dop_pow_base;
                func.que_effects[x + 1, y] = que1;
                GameObject ef_cr = func.create_effect(ef, x + 1, y, parent, true, dop_pow, cart_par);
                ef_cr.active = true;
                if (dop_ef != null)
                {
                    ef_cr.GetComponent<par_effect>().st_par.Push(dop_ef);
                    ef_cr.GetComponent<par_effect>().st_par.Push("par pow");
                }
            }
            dop_pow = dop_pow_base;

            if (y < func.size_global - 1)
            {
                //Debug.Log("x -1 " + x + " " + y);
                List<GameObject> lt_del = new List<GameObject>();
                Queue<GameObject> que1 = new Queue<GameObject>();
                //bool bl1_ef = false;
                foreach (GameObject gm1 in func.que_effects[x, y + 1])
                {
                    if (func.cart_effects[ef].name == gm1.name)
                    {
                        lt_del.Add(gm1);
                        //Debug.Log(gm1.name);
                    }
                    else
                    {
                        que1.Enqueue(gm1);
                    }
                }
                //Debug.Log("x -1 " + x + " " + y + "   " + lt_del.Count);
                while (lt_del.Count > 0)
                {

                    //if(lt_del[0].name==)
                    dop_pow += lt_del[0].GetComponent<par_effect>().power;

                    lt_del[0].GetComponent<par_effect>().bl_que = false;
                    lt_del[0].active = true;
                    lt_del[0].GetComponent<par_effect>().Destroyed();
                    lt_del.RemoveAt(0);
                }
                //dop_pow += dop_pow_base;
                func.que_effects[x, y + 1] = que1;
                GameObject ef_cr = func.create_effect(ef, x, y+1, parent, true, dop_pow, cart_par);
                ef_cr.active = true;
                if (dop_ef != null)
                {
                    ef_cr.GetComponent<par_effect>().st_par.Push(dop_ef);
                    ef_cr.GetComponent<par_effect>().st_par.Push("par pow");
                }
            }

            Destroy(gm);


        }
        else if (func.nature[x, y] != null && func.nature[x, y].name == "crystall")
        {
            //Debug.Log(func.nature[x, y] == null);
            if (power > 0)
            {
                if (gameObject.name == "create crystall")
                {
                    par_nat nt = func.nature[x, y].GetComponent<par_nat>();
                    nt.type = (int.Parse(nt.type) + power).ToString();
                }
                else
                {
                    par_effect ef = func.create_effect("destroy crystall", x, y, parent, false, power + int.Parse(func.nature[x, y].GetComponent<par_nat>().type), cart_par).GetComponent<par_effect>();
                    if(cart_par!=null && cart_par.Count>1 && cart_par[0]=="cart derlorg")
                    {
                        //st_par.Pop();
                        //st_par.
                        string st1_par = st_par.Pop();
                        ef.st_par.Push(st_par.Peek());
                        ef.st_par.Push(st1_par);
                    }
                    ef.st_par.Push(name_in_lt);
                    ef.st_par.Push(power.ToString());
                    ef.st_par.Push("par_crystall");
                }
            }


        }
        else if (gameObject.name == "create crystall" && func.object_bool(x, y) && power>0)
        {
            par_nat nt = func.create("crystall", x, y).GetComponent<par_nat>();
            nt.gameObject.active = func.eath[x, y].active;
            nt.type = power.ToString();
        }

        */



        /*
        else if (gameObject.name == "spawn dych blood" && func.object_bool(x, y) && power > 0)
        {
            par_mob mb_sp = func.create("dych blood", x, y).GetComponent<par_mob>();
            mb_sp.parent = parent;
            parent.GetComponent<par_mob>().childs.Add(mb_sp.gameObject);
            mb_sp.friend = parent.GetComponent<par_mob>().friend;
            mb_sp.hp_max = power;
            mb_sp.hp = power;
            mb_sp.viz(mb_sp.friend || func.eath[x, y].active);
        }
        else if (gameObject.name == "spawn dych fire" && func.object_bool(x, y) && power > 0)
        {
            par_mob mb_sp = func.create("dych fire", x, y).GetComponent<par_mob>();
            mb_sp.parent = parent;
            parent.GetComponent<par_mob>().childs.Add(mb_sp.gameObject);
            mb_sp.friend = parent.GetComponent<par_mob>().friend;
            mb_sp.hp_max = power;
            mb_sp.hp = power;
            mb_sp.viz(mb_sp.friend || func.eath[x, y].active);
        }
        else if (gameObject.name == "spawn dych poition" && func.object_bool(x, y) && power>0)
        {
            par_mob mb_sp = func.create("dych poition", x, y).GetComponent<par_mob>();
            mb_sp.parent = parent;
            parent.GetComponent<par_mob>().childs.Add(mb_sp.gameObject);
            mb_sp.friend = parent.GetComponent<par_mob>().friend;
            mb_sp.hp_max = power;
            mb_sp.hp = power;
            mb_sp.viz(mb_sp.friend || func.eath[x, y].active);
        }
        else if (gameObject.name == "spawn dych energy" && func.object_bool(x, y) && power > 0)
        {
            par_mob mb_sp = func.create("dych energy", x, y).GetComponent<par_mob>();
            mb_sp.parent = parent;
            parent.GetComponent<par_mob>().childs.Add(mb_sp.gameObject);
            mb_sp.friend = parent.GetComponent<par_mob>().friend;
            mb_sp.hp_max = power;
            mb_sp.hp = power;
            mb_sp.viz(mb_sp.friend || func.eath[x, y].active);
        }
        else if (gameObject.name == "spawn dych ice" && func.object_bool(x, y) && power > 0)
        {
            par_mob mb_sp = func.create("dych ice", x, y).GetComponent<par_mob>();
            mb_sp.parent = parent;
            parent.GetComponent<par_mob>().childs.Add(mb_sp.gameObject);
            mb_sp.friend = parent.GetComponent<par_mob>().friend;
            mb_sp.hp_max = power;
            mb_sp.hp = power;
            mb_sp.viz(mb_sp.friend || func.eath[x, y].active);
        }
        else if (gameObject.name == "spawn dych rock" && func.object_bool(x, y) && power > 0)
        {
            par_mob mb_sp = func.create("dych rock", x, y).GetComponent<par_mob>();
            mb_sp.parent = parent;
            parent.GetComponent<par_mob>().childs.Add(mb_sp.gameObject);
            mb_sp.friend = parent.GetComponent<par_mob>().friend;
            mb_sp.hp_max = power;
            mb_sp.hp = power;
            mb_sp.viz(mb_sp.friend || func.eath[x, y].active);
        }

        */
        else if (gameObject.name == "spirit blood ef" && power > 0)
        {
            func.create_lovyshka(x, y, "spirit blood", parent, power, 1);
        }
        else if (gameObject.name == "spirit fire ef" && power > 0)
        {
            func.create_lovyshka(x, y, "spirit fire", parent, power, 1);
        }
        else if (gameObject.name == "spirit ice ef" && power > 0)
        {
            func.create_lovyshka(x, y, "spirit ice", parent, power, 1);
        }
        else if (gameObject.name == "spirit poition ef" && power > 0)
        {
            func.create_lovyshka(x, y, "spirit poition", parent, power, 1);
        }



        else if (cart_par[0] == "cart tuman(boss)" && power > 0)
        {
            par_cart cart1 = GameObject.Find(cart_par[1]).GetComponent<par_cart>();
            string ef = null;
            if (cart1.type == par_cart.type_cart.blood)
            {
                ef = "lov blood tuman";
            }
            else if (cart1.type == par_cart.type_cart.ice)
            {
                ef = "lov ice tuman";
            }
            else if (cart1.type == par_cart.type_cart.fire)
            {
                ef = "lov fire tuman";
            }
            else if (cart1.type == par_cart.type_cart.poition)
            {
                ef = "lov poition tuman";
            }
            else if (cart1.type == par_cart.type_cart.rock)
            {
                ef = "lov rock tuman";
            }
            else if (cart1.type == par_cart.type_cart.energy)
            {
                ef = "lov energy tuman";
            }

            func.create_lovyshka(x, y, ef, parent, 2 * power, 1);
            /*
            if (x > 0)
            {
                func.create_lovyshka(x-1,y,ef,5*power,1);
            }
            if (x <func.size_global-1)
            {
                func.create_lovyshka(x + 1, y, ef, 5 * power, 1);
            }
            if (y > 0)
            {
                func.create_lovyshka(x, y-1, ef, 5 * power, 1);
            }
            if (y < func.size_global - 1)
            {
                func.create_lovyshka(x, y+1, ef, 5 * power, 1);
            }
            */
        }
        else if(cart_par[0] == "cart rage(boss)")
        {
            if (power > 0 && mb != null)
            {
                //for (int i = 0; i < power; i++)
                //{
                    mb.add_baf(par_baf.type_baf.rage, power * 2);
                //}
            }

        }
        else if (lt_type_carts[0] == par_cart.type_cart.derlorg)
        {
            mb = null;
            if (func.mob[x, y] != null)
            {
                mb = func.mob[x, y].GetComponent<par_mob>();
            }
            if (mb != null)
            {
                Debug.Log("add seal");
                if (cart_par.Count == 1)
                {
                    Debug.Log("add seal 1 pow "+power);
                    if (art_dungalorg_boss == null)
                    {
                        if (power > 0)
                            mb.add_baf(par_baf.type_baf.seal, power * 4);
                    }
                    else
                    {
                        //mb.add_cart("cart derlorg");
                        mb.activate_art(art_dungalorg_boss,x,y);
                    }
                }
                else if(power > 0)
                {
                    st_par.Pop();
                    int pow_baf = int.Parse(st_par.Pop());
                    par_baf.type_baf type_baf = par_baf.type_baf.seal;
                    
                    if (lt_type_carts[1]==par_cart.type_cart.damage)
                        type_baf = par_baf.type_baf.seal_damage;
                    else if (lt_type_carts[1] == par_cart.type_cart.sp_barier)
                        type_baf = par_baf.type_baf.seal_barier;
                    else if (lt_type_carts[1] == par_cart.type_cart.rock)
                        type_baf = par_baf.type_baf.seal_rock;
                    else if (lt_type_carts[1] == par_cart.type_cart.blood)
                        type_baf = par_baf.type_baf.seal_blood;
                    else if (lt_type_carts[1] == par_cart.type_cart.fire)
                        type_baf = par_baf.type_baf.seal_fire;
                    else if (lt_type_carts[1] == par_cart.type_cart.poition)
                        type_baf = par_baf.type_baf.seal_poition;
                    else if (lt_type_carts[1] == par_cart.type_cart.ice)
                        type_baf = par_baf.type_baf.seal_ice;
                    else if (lt_type_carts[1] == par_cart.type_cart.energy)
                        type_baf = par_baf.type_baf.seal_energy;
                    mb.add_baf(type_baf, power * 4,pow_baf);
                }


            }

        }
        else if (lt_type_carts[0] == par_cart.type_cart.rock)
        {
            mb = null;
            if (func.mob[x, y] != null)
            {
                mb = func.mob[x, y].GetComponent<par_mob>();
            }

            if (mb != null)
            {

                int dop_ef = 0;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit rock")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit rock heard")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart rock heard" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                if (regen > 0)
                {
                    mb.regen(power * regen, "blood");
                }
                else if (power > 1)
                    mb.damage(power*dop_ef, "none");
            }

        }
        else if (lt_type_carts[0] == par_cart.type_cart.blood)
        {
            mb = null;
            if (func.mob[x, y] != null)
            {
                mb = func.mob[x, y].GetComponent<par_mob>();
            }
            if (mb != null)
            {
                //power = 1;
                //if (player_name == "blood player" &&bl_pl)
                //power *= 2;
                /*
            List<par_baf> lt_del_bafs = new List<par_baf>();
            List<par_baf.type_baf> lt_del_bafs_type = new List<par_baf.type_baf>();

            par_cart_art art_def = null;
            foreach (par_cart_art cart in mb.lt_carts_art)
            {
                if (cart.name == "cart bar st" && cart.perez_now == 0)
                {
                    art_def = cart;
                }
            }

            foreach (par_baf baf in mb.lt_bafs)
            {
                bool bl = false;
                if (baf.type == par_baf.type_baf.bar_blood)
                {
                    bl = true;
                    if (art_def != null)
                    {
                        mb.activate_art(art_def, mb.x, mb.y);
                        power = 0;
                        //art_def.perez_now = art_def.perez;
                    }
                }

                if (bl)
                {
                    int a = power;
                    power -= baf.power;
                    baf.power -= a;

                    if (baf.power <= 0)
                    {
                        lt_del_bafs.Add(baf);
                        lt_del_bafs_type.Add(baf.type);
                    }
                    if (power < 0)
                        power = 0;
                }
            }
            while (lt_del_bafs.Count > 0)
            {
                par_baf bf_del = lt_del_bafs[0];
                mb.lt_bafs.Remove(bf_del);
                lt_del_bafs_type.RemoveAt(0);
                if (!lt_del_bafs_type.Contains(bf_del.type))
                {
                    if (bf_del.type == par_baf.type_baf.bar_blood)
                    {
                        Destroy(func.mob[x, y].transform.Find("bar blood").gameObject);
                    }
                }
                Destroy(bf_del);
                lt_del_bafs.RemoveAt(0);
            }

            //Debug.Log("damage effect");

                */
                int dop_ef = 1;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit blood")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit blood heard")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart blood heard" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                if (regen > 0)
                {
                    mb.regen(power * regen*2, "blood");
                }
                if (power > 0)
                    mb.regen(power*dop_ef, "blood");
            }

        }
        else if (lt_type_carts[0] == par_cart.type_cart.fire)
        {
            mb = null;
            if (func.mob[x, y] != null)
            {
                mb = func.mob[x, y].GetComponent<par_mob>();

            }
            if (mb != null)
            {
                //Debug.Log("damage effect");

                //power = 1;
                //if (player_name == "fire player" && bl_pl)
                //power *= 2;
                /*
            List<par_baf> lt_del_bafs = new List<par_baf>();
            List<par_baf.type_baf> lt_del_bafs_type = new List<par_baf.type_baf>();

            par_cart_art art_def = null;
            foreach (par_cart_art cart in mb.lt_carts_art)
            {
                if (cart.name == "cart bar st" && cart.perez_now == 0)
                {
                    art_def = cart;
                }
            }

            foreach (par_baf baf in mb.lt_bafs)
            {
                bool bl = false;
                if (baf.type == par_baf.type_baf.bar_fire)
                {
                    bl = true;
                    if (art_def != null)
                    {
                        mb.activate_art(art_def, mb.x, mb.y);
                        power = 0;
                        //art_def.perez_now = art_def.perez;
                    }
                }

                if (bl)
                {
                    int a = power;
                    power -= baf.power;
                    baf.power -= a;

                    if (baf.power <= 0)
                    {
                        lt_del_bafs.Add(baf);
                        lt_del_bafs_type.Add(baf.type);
                    }
                    if (power < 0)
                        power = 0;
                }
            }
            while (lt_del_bafs.Count > 0)
            {
                par_baf bf_del = lt_del_bafs[0];
                mb.lt_bafs.Remove(bf_del);
                lt_del_bafs_type.RemoveAt(0);
                if (!lt_del_bafs_type.Contains(bf_del.type))
                {
                    if (bf_del.type == par_baf.type_baf.bar_fire)
                    {
                        Destroy(func.mob[x, y].transform.Find("bar fire").gameObject);
                    }
                }
                Destroy(bf_del);

                lt_del_bafs.RemoveAt(0);
            }
                */
                int dop_ef = 1;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit fire")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit fire heard")
                    {
                        regen++;
                    }
                    else if(cart.name == "cart fire heard" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                if (regen > 0)
                {
                    mb.regen(power * regen, "blood");
                }
                else if (power > 0)
                    for (int i = 0; i < power; i++)
                    {
                        mb.add_baf(par_baf.type_baf.fire, 2*dop_ef,1);
                    }

            }

        }

        else if (lt_type_carts[0] == par_cart.type_cart.energy)
        {
            mb = null;
            if (func.mob[x, y] != null)
            {
                mb = func.mob[x, y].GetComponent<par_mob>();

            }
            if (mb != null)
            {


                int dop_ef = 1;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit energy")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit energy heard")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart energy heard" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                if (regen > 0)
                {
                    mb.regen(power*2*regen, "blood");
                }
                if (power > 0)
                    mb.add_baf(par_baf.type_baf.energy, power * 2 * dop_ef);
            }

        }
        else if (lt_type_carts[0] == par_cart.type_cart.ice)
        {
            mb = null;
            if (func.mob[x, y] != null)
            {
                mb = func.mob[x, y].GetComponent<par_mob>();

            }
            if (mb != null)
            {
                int dop_ef = 1;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit ice")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit ice heard")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart ice heard" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                if (regen > 0)
                {
                    mb.regen(power*regen, "blood");
                }
                else if (power > 0)
                    mb.add_baf(par_baf.type_baf.ice, power * 2 * dop_ef);
            }

        }
        else if (lt_type_carts[0] == par_cart.type_cart.poition)
        {
            mb = null;
            if (func.mob[x, y] != null)
            {
                mb = func.mob[x, y].GetComponent<par_mob>();

            }
            if (mb != null)
            {
                int dop_ef = 1;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit poition")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit poition heard")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart poition heard" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                if (regen > 0)
                {
                    mb.regen(power*regen, "blood");
                }
                else if (power > 0)
                    mb.add_baf(par_baf.type_baf.poition, power * 3,dop_ef);

            }

        }


        else if(cart_par[0] == "cart spider poition")
        {
            if(mb!=null)
            mb.damage(power, "none");



        }
        else if (cart_par[0] == "cart spider")
        {
            if (func.mob[x, y] != null && func.mob[x, y].name == "spider (spawned)")
            {
                //Debug.Log(func.nature[x, y] == null);
                if (power > 0)
                {
                    par_mob mb_sp = func.mob[x, y].GetComponent<par_mob>();
                    mb_sp.hp += power;
                    mb_sp.hp_max += power;

                }


            }
            else if (func.object_bool(x, y))
            {
                par_mob mb_sp = func.create("spider (spawned)", x, y).GetComponent<par_mob>();
                mb_sp.hp_max = power;
                mb_sp.hp = power;
                mb_sp.friend = parent.GetComponent<par_mob>().friend;
                if (mb_sp.friend)
                {
                    mb_sp.friend_type = par_mob.type_friend.auto;
                }
                mb_sp.viz(func.eath[x, y].active);
                mb_sp.update_viz();
                //mb_sp.gameObject.active = func.eath[x, y].active;

            }



        }




        else if (cart_par[0] == "cart damage" || cart_par[0] == "cart damage 1")
        {
            string type_dm = null;
            mb = null;
            //if (player_name == "atack player" && bl_pl)
            //power *= 2;

            if (cart_par.Count == 1)
            {

                if (func.mob[x, y] != null)
                {
                    mb = func.mob[x, y].GetComponent<par_mob>();
                }
                else if (func.nature[x, y] != null && func.nature[x, y].GetComponent<par_nat>().dam)
                {
                    GameObject gm = func.nature[x, y];
                    func.nature[x, y] = null;
                    Destroy(gm);
                }
                if (mb != null)
                {
                    //Debug.Log("damage effect");

                    //mb.damage(power, "base");
                    type_dm = "base";
                }
            }
            else
            {
                int pow1 = GameObject.Find(cart_par[1]).GetComponent<par_cart>().lvl;
                /*
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit poition")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit poition heard")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart poition heard" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                */

                if (cart_par[1] == "cart rock" || cart_par[1] == "cart rock 1")
                {
                    if (func.mob[x, y] != null)
                    {
                        mb = func.mob[x, y].GetComponent<par_mob>();
                    }
                    else if (func.nature[x, y] != null && func.nature[x, y].GetComponent<par_nat>().dam)
                    {
                        GameObject gm = func.nature[x, y];
                        func.nature[x, y] = null;
                        Destroy(gm);
                    }
                    if (mb != null)
                    {

                        //Debug.Log("damage effect");
                        //if (player_name == "rock player" && bl_pl)
                        // power += pow1*2;
                        type_dm = "rock";
                        //mb.damage(power, "rock");
                        /*
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.perez_now == 0)
                            {
                                if (cart.name == "cart hp rock")
                                    mb.activate_art(cart, x, y);
                            }
                        }
                        */
                    }

                }
                else if (cart_par[1] == "cart blood" || cart_par[1] == "cart blood 1")
                {
                    if (func.mob[x, y] != null)
                    {
                        mb = func.mob[x, y].GetComponent<par_mob>();
                    }
                    else if (func.nature[x, y] != null && func.nature[x, y].GetComponent<par_nat>().dam)
                    {
                        GameObject gm = func.nature[x, y];
                        func.nature[x, y] = null;
                        Destroy(gm);
                    }
                    if (mb != null)
                    {
                        //Debug.Log("damage effect");
                        //if (player_name == "blood player" && bl_pl)
                        //power += pow1 * 2;
                        //mb.damage(power, "blood");
                        type_dm = "blood";
                        /*
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.perez_now == 0)
                            {
                                if (cart.name == "cart hp blood")
                                    mb.activate_art(cart, x, y);
                            }
                        }
                        */
                    }

                }
                else if (cart_par[1] == "cart fire" || cart_par[1] == "cart fire 1")
                {
                    if (func.mob[x, y] != null)
                    {
                        mb = func.mob[x, y].GetComponent<par_mob>();
                    }
                    else if (func.nature[x, y] != null && func.nature[x, y].GetComponent<par_nat>().dam)
                    {
                        GameObject gm = func.nature[x, y];
                        func.nature[x, y] = null;
                        Destroy(gm);
                    }
                    if (mb != null)
                    {
                        //Debug.Log("damage effect");
                        //if (player_name == "fire player" && bl_pl)
                        //power += pow1 * 2;
                        //mb.damage(power, "fire");
                        type_dm = "fire";
                        /*
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.perez_now == 0)
                            {
                                if (cart.name == "cart hp fire")
                                    mb.activate_art(cart, x, y);
                            }
                        }
                        */
                    }

                }
                else if (cart_par[1] == "cart energy" || cart_par[1] == "cart energy 1")
                {
                    if (func.mob[x, y] != null)
                    {
                        mb = func.mob[x, y].GetComponent<par_mob>();
                    }
                    else if (func.nature[x, y] != null && func.nature[x, y].GetComponent<par_nat>().dam)
                    {
                        GameObject gm = func.nature[x, y];
                        func.nature[x, y] = null;
                        Destroy(gm);
                    }
                    if (mb != null)
                    {
                        //Debug.Log("damage effect");
                        //if (player_name == "energy player" && bl_pl)
                        //power += pow1 * 2;
                        //mb.damage(power, "energy");
                        type_dm = "energy";
                        /*
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.perez_now == 0)
                            {
                                if (cart.name == "cart hp energy")
                                    mb.activate_art(cart, x, y);
                            }
                        }
                        */
                    }

                }

                else if (cart_par[1] == "cart ice" || cart_par[1] == "cart ice 1")
                {
                    if (func.mob[x, y] != null)
                    {
                        mb = func.mob[x, y].GetComponent<par_mob>();
                    }
                    else if (func.nature[x, y] != null && func.nature[x, y].GetComponent<par_nat>().dam)
                    {
                        GameObject gm = func.nature[x, y];
                        func.nature[x, y] = null;
                        Destroy(gm);
                    }
                    if (mb != null)
                    {
                        //Debug.Log("damage effect");
                        //if (player_name == "ice player" && bl_pl)
                        //power += pow1 * 2;
                        //mb.damage(power, "energy");
                        type_dm = "ice";
                        /*
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.perez_now == 0)
                            {
                                if (cart.name == "cart hp energy")
                                    mb.activate_art(cart, x, y);
                            }
                        }
                        */
                    }

                }
                else if (cart_par[1] == "cart poition" || cart_par[1] == "cart poition 1")
                {
                    if (func.mob[x, y] != null)
                    {
                        mb = func.mob[x, y].GetComponent<par_mob>();
                    }
                    else if (func.nature[x, y] != null && func.nature[x, y].GetComponent<par_nat>().dam)
                    {
                        GameObject gm = func.nature[x, y];
                        func.nature[x, y] = null;
                        Destroy(gm);
                    }
                    if (mb != null)
                    {
                        //Debug.Log("damage effect");
                        //if (player_name == "poition player" && bl_pl)
                        //power += pow1 * 2;
                        //mb.damage(power, "energy");
                        type_dm = "poition";
                        /*
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.perez_now == 0)
                            {
                                if (cart.name == "cart hp energy")
                                    mb.activate_art(cart, x, y);
                            }
                        }
                        */
                    }

                }
            }

            /*

            List<par_baf> lt_del_bafs = new List<par_baf>();
            List<par_baf.type_baf> lt_del_bafs_type = new List<par_baf.type_baf>();

            par_cart_art art_bar = null;
            par_cart_art art_att = null;

            if (mb != null)
            {
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart bar st" && cart.perez_now == 0)
                    {
                        art_bar = cart;
                    }

                }
                foreach (par_cart_art cart in parent.GetComponent<par_mob>().lt_carts_art)
                {
                    if (cart.name == "cart att st" && cart.perez_now == 0)
                    {
                        art_att = cart;
                    }

                }
                foreach (par_baf baf in mb.lt_bafs)
                {
                    bool bl = false;

                    if (art_bar == null && art_att != null)
                    {
                        //power = 0;

                        mb.activate_art(art_att, mb.x, mb.y);
                        //art_bar.perez_now = art_bar.perez;
                    }
                    else
                    {


                        if (baf.type == par_baf.type_baf.bar && type_dm == "base")
                        {
                            bl = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_rock && type_dm == "rock")
                        {
                            bl = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_blood && type_dm == "blood")
                        {
                            bl = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_fire && type_dm == "fire")
                        {
                            bl = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_energy && type_dm == "energy")
                        {
                            bl = true;
                        }

                        if (art_bar != null && art_att == null && bl)
                        {
                            power = 0;

                            mb.activate_art(art_bar, mb.x, mb.y);
                            //art_bar.perez_now = art_bar.perez;
                        }
                    }


                    if (bl)
                    {
                        int a = power;
                        power -= baf.power;
                        baf.power -= a;

                        if (baf.power <= 0)
                        {
                            lt_del_bafs.Add(baf);
                            lt_del_bafs_type.Add(baf.type);
                        }
                        if (power < 0)
                            power = 0;
                    }
                }
                while (lt_del_bafs.Count > 0)
                {
                    par_baf bf_del = lt_del_bafs[0];
                    mb.lt_bafs.Remove(bf_del);
                    lt_del_bafs_type.RemoveAt(0);
                    if (!lt_del_bafs_type.Contains(bf_del.type))
                    {
                        if (bf_del.type == par_baf.type_baf.bar)
                        {
                            Destroy(mb.gameObject.transform.Find("bar bases").gameObject);
                        }
                        else if (bf_del.type == par_baf.type_baf.bar_rock)
                        {
                            Destroy(mb.gameObject.transform.Find("bar rock").gameObject);
                        }
                        else if (bf_del.type == par_baf.type_baf.bar_energy)
                        {
                            Destroy(mb.gameObject.transform.Find("bar energy").gameObject);
                        }
                        else if (bf_del.type == par_baf.type_baf.bar_blood)
                        {
                            Destroy(mb.gameObject.transform.Find("bar blood").gameObject);
                        }
                        else if (bf_del.type == par_baf.type_baf.bar_fire)
                        {
                            Destroy(mb.gameObject.transform.Find("bar fire").gameObject);
                        }
                    }

                    Destroy(bf_del);
                    lt_del_bafs.RemoveAt(0);
                }
            */
            

            if (mb != null)
            {
                int regen = 0;
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit poition heard" && type_dm == "poition")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart poition heard" && cart.q > 0 && type_dm == "poition")
                    {
                        regen++;
                        mb.activate_art(cart);
                    }

                    else if (cart.name == "cart spirit rock heard" && type_dm == "rock")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart rock heard" && cart.q > 0 && type_dm == "rock")
                    {
                        regen++;
                        mb.activate_art(cart);
                    }

                    else if (cart.name == "cart spirit blood heard" && type_dm == "blood")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart blood heard" && cart.q > 0 && type_dm == "blood")
                    {
                        regen++;
                        mb.activate_art(cart);
                    }

                    else if (cart.name == "cart spirit ice heard" && type_dm == "ice")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart ice heard" && cart.q > 0 && type_dm == "ice")
                    {
                        regen++;
                        mb.activate_art(cart);
                    }

                    else if (cart.name == "cart spirit energy heard" && type_dm == "energy")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart energy heard" && cart.q > 0 && type_dm == "energy")
                    {
                        regen++;
                        mb.activate_art(cart);
                    }

                    else if (cart.name == "cart spirit fire heard" && type_dm == "fire")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart fire heard" && cart.q > 0 && type_dm == "fire")
                    {
                        regen++;
                        mb.activate_art(cart);
                    }
                }
                if(regen>0)
                    mb.regen(power * regen, "blood");
                else
                    mb.damage(power, type_dm);
            }
                
        }
        else if (cart_par[0] == "cart barier" || cart_par[0] == "cart barier 1")
        {
            par_baf.type_baf type_dm = par_baf.type_baf.bar;
            //mb = null;
            //if (player_name == "atack player" && bl_pl)
            //power *= 2;

            if (cart_par.Count == 1)
            {
                if (mb != null)
                {
                    //Debug.Log("damage effect");

                    //mb.damage(power, "base");
                    type_dm = par_baf.type_baf.bar;
                }
            }
            else
            {

                if (cart_par[1] == "cart rock" || cart_par[1] == "cart rock 1")
                {
                    if (mb != null)
                    {

                        type_dm = par_baf.type_baf.bar_rock;

                    }

                }
                else if (cart_par[1] == "cart blood" || cart_par[1] == "cart blood 1")
                {
                    if (mb != null)
                    {

                        type_dm = par_baf.type_baf.bar_blood;

                    }

                }
                else if (cart_par[1] == "cart fire" || cart_par[1] == "cart fire 1")
                {
                    if (mb != null)
                    {
                        type_dm = par_baf.type_baf.bar_fire;

                    }

                }
                else if (cart_par[1] == "cart energy" || cart_par[1] == "cart energy 1")
                {
                    if (mb != null)
                    {

                        type_dm = type_dm = par_baf.type_baf.bar_energy;

                    }

                }

                else if (cart_par[1] == "cart ice" || cart_par[1] == "cart ice 1")
                {
                    if (mb != null)
                    {

                        type_dm = type_dm = par_baf.type_baf.bar_ice;

                    }

                }
                else if (cart_par[1] == "cart poition" || cart_par[1] == "cart poition 1")
                {
                    if (mb != null)
                    {
                        //Debug.Log("damage effect");
                        //if (player_name == "poition player" && bl_pl)
                        //power += pow1 * 2;
                        //mb.damage(power, "energy");
                        type_dm = type_dm = par_baf.type_baf.bar_poition;
                        /*
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.perez_now == 0)
                            {
                                if (cart.name == "cart hp energy")
                                    mb.activate_art(cart, x, y);
                            }
                        }
                        */
                    }

                }
            }
            if (mb != null && power > 0)
                mb.add_baf(type_dm, -1, power * 2);
        }





        if (func.mob[x, y] != null && func.mob[x, y].GetComponent<par_mob>().num == 0)
        {
            /*
            if(func.mob[x, y].GetComponent<par_mob>().hp <= 0)
            {
                data.give_achive("просто надоело");
            }
            */
            func.mob[x, y].GetComponent<par_mob>().update_bufs();
        }
    }
    public void spawn_cart()
    {
        GameObject gm = Instantiate(GameObject.Find("effects").transform.Find("fly cart").gameObject);
        gm.GetComponent<SpriteRenderer>().sprite = ui.cart_sprite[cart_par[0]];
        gm.transform.position = gameObject.transform.position;
        cart_spawn sp = gm.GetComponent<cart_spawn>();
        
        sp.parent = parent;
        Vector3 v3 = gameObject.transform.position;
        sp.v2_start_all = v3;
        v3.y++;
        sp.sets(0.5f, false, v3);
        gm.active = true;
        sp.next_cart();
        anim.col_anim++;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
