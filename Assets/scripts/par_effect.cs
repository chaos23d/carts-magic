using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static par_cart;
using static par_mob;

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
            if (lt_type_carts[0]!= par_cart.type_cart.derlorg && lt_type_carts[0] != par_cart.type_cart.sp_dych && lt_type_carts[0] != par_cart.type_cart.sp_bomb)
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
                    else if (art.name == "cart derlorg potoc")
                    {
                        Stack<object> st = new Stack<object>();
                        st.Push(cart_par);
                        st.Push("par spawn derlorg");
                        mb.activate_art(art, x, y, st);
                    }
                }
            }
            foreach (par_cart_art art in mb.lt_carts_art)
            {
                if (art.name == "cart dungalorg(boss)")
                {
                    art_dungalorg_boss = art;
                }
                else if (art.perez_now == 0)
                {
                    if (art.name == "cart mini shaman")
                    {
                        func.create_effect(name_in_lt, mb_par.x, mb_par.y, parent, 1, cart_par);
                        mb.activate_art(art, mb_par.x, mb_par.y);
                    }
                    else if (art.name == "cart shaman boss")
                    {
                        func.create_effect(name_in_lt, mb_par.x, mb_par.y, mb.gameObject, 1 + (mb.hp_max - mb.hp) / 5, cart_par);
                        mb.activate_art(art, mb_par.x, mb_par.y);
                    }
                }
                
            }

            foreach (par_baf baf in mb.lt_bafs)
            {
                bool bl = false;
                //par_cart.type_cart tp0 = GameObject.Find(cart_par[0]).GetComponent<par_cart>().type;
                if (lt_type_carts[0] == par_cart.type_cart.damage && gameObject.name!="spirit base")
                {
                    if (baf.type == par_baf.type_baf.bar)
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_rock )
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_blood )
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_fire )
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_energy )
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_poition )
                    {
                        bl = true;
                    }
                    else if (baf.type == par_baf.type_baf.bar_ice )
                    {
                        bl = true;
                    }
                }




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
            /*
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
            */
        }

        if (gameObject.name == "bomb create"  && power > 0)
        {
            //par_nat nt = func.create("crystall", x, y).GetComponent<par_nat>();
            //nt.gameObject.active = func.eath[x, y].active;
            //nt.type = power.ToString();
            Debug.Log("ef cr");
            //List<string> lt_ = new List<string>(cart_par);
            Queue<par_cart.type_cart> lt_que = new Queue<par_cart.type_cart>();


            //lt_.RemoveAt(0);
            /*
            foreach (string st in lt_)
            {
                lt_que.Enqueue(GameObject.Find(st).GetComponent<par_cart>().type);
            }
            lt_que.Enqueue(par_cart.type_cart.sp_crystall);
            lt_que.Enqueue(par_cart.type_cart.sp_crystall);
            */
            //lt_que.Enqueue(par_cart.type_cart.bases);

            GameObject l_g = func.create_lovyshka(x, y, "lov bomb", parent, 1, power);
            par_lovyshka l =l_g.GetComponent<par_lovyshka>();
            //l_g.GetComponent<anim_color>().lt_colors = lt_que;
            List<string> lt_ = new List<string>(cart_par);
            lt_.RemoveAt(0);
            l.par_carts = cart_par;
            Stack<string> pr= new Stack<string>();
            pr.Push(func.get_name_effect(lt_, null));
            func.set_anim_color(l_g, lt_);
            l.st_par = pr;
            
        }
        else if (gameObject.name == "bomb destroy" && power > 0)
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
            Color c = anim_color.static_colors[func.baza[cart_par[0]].GetComponent<par_cart>().type];

            cart_par.RemoveAt(0);
            GameObject gm = null;
            gm=func.create_effect(name, x , y, parent, true, power*2, cart_par);
            gm.GetComponent<SpriteRenderer>().color = c;
            if (x > 0)
            {
                gm=func.create_effect(name, x - 1, y, parent, true, power, cart_par);

                gm.GetComponent<SpriteRenderer>().color = c;
            }
            if (y > 0)
            {
                gm=func.create_effect(name, x, y - 1, parent, true, power, cart_par);
                gm.GetComponent<SpriteRenderer>().color = c;

            }
            if (x < func.size_global - 1)
            {
                func.create_effect(name, x + 1, y, parent, true, power, cart_par);
                gm.GetComponent<SpriteRenderer>().color = c;

            }
            if (y< func.size_global - 1)
            {
                func.create_effect(name, x, y + 1, parent, true, power, cart_par);
                gm.GetComponent<SpriteRenderer>().color = c;

            }
        }

        else if (gameObject.name == "spirit base" && power > 0)
        {
            List<string> lt_st_c = new List<string>();

            foreach (string st in cart_par)
            {
                if (st == "cart spirit fire")
                    lt_st_c.Add("cart fire");
                else if (st == "cart spirit poition")
                    lt_st_c.Add("cart poition");
                else if (st == "cart spirit rock")
                    lt_st_c.Add("cart rock");
                else if (st == "cart spirit blood")
                    lt_st_c.Add("cart blood");
                else if (st == "cart spirit ice")
                    lt_st_c.Add("cart ice");
                else if (st == "cart spirit energy")
                    lt_st_c.Add("cart energy");
                else if (st != "cart spirit(boss)")
                    lt_st_c.Add(st);
            }
            //mb.effect_now.GetComponent<par_effect>().st_par.Push(func.get_name_effect(lt_st_c, null));


            GameObject g_c = func.create_lovyshka(x, y, "spirit base", parent, power*2, 1);
            g_c.GetComponent<par_lovyshka>().par_carts = lt_st_c;
            func.set_anim_color(g_c, lt_st_c);



        }
        else if (gameObject.name == "spawn dych" && power > 0)
        {
            if (func.object_bool(x, y))
            {
                cart_par.RemoveAt(0);
                par_mob mb_d= func.create("dych", x, y).GetComponent<par_mob>();
                func.set_anim_color(mb_d.gameObject.transform.Find("body").gameObject, cart_par);
                mb_d.hp_max = power*2;
                mb_d.hp = power*2;
                mb_d.long_life = 20;
                foreach (string cart in cart_par)
                {
                    int lvl = func.baza[cart].GetComponent<par_cart>().lvl;
                    par_cart.type_cart tp = func.baza[cart].GetComponent<par_cart>().type;
                    string st = "";
                    if (lvl == 2)
                        st = " 1";
                    else if (lvl == 4)
                        st = " 2";
                    if (tp == par_cart.type_cart.damage)
                    {
                        mb_d.add_cart_art("cart sp atack" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                    }
                        

                    else if (tp == par_cart.type_cart.sp_barier)
                    {
                        mb_d.add_cart_art("cart sp barier" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                    }
                        
                    else if (tp == par_cart.type_cart.sp_bomb)
                    {
                        mb_d.add_cart_art("cart sp bomb" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.bomb, 1));
                    }
                       

                    else if (tp == par_cart.type_cart.blood)
                    {
                        mb_d.add_cart_art("cart sp blood" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                    }
                        
                    else if (tp == par_cart.type_cart.rock)
                    {
                        mb_d.add_cart_art("cart sp rock" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                    }
                        
                    else if (tp == par_cart.type_cart.poition)
                    {
                        mb_d.add_cart_art("cart sp poition" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
                    }
                        
                    else if (tp == par_cart.type_cart.fire)
                    {
                        mb_d.add_cart_art("cart sp fire" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
                    }
                        
                    else if (tp == par_cart.type_cart.ice)
                    {
                        mb_d.add_cart_art("cart sp ice" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
                    }
                        
                    else if (tp == par_cart.type_cart.energy)
                    {
                        mb_d.add_cart_art("cart sp energy" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
                    }
                        

                    else if (tp == par_cart.type_cart.derlorg)
                    {
                        mb_d.add_cart_art("cart sp derlorg" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                    }


                    else if (tp == par_cart.type_cart.spirit_blood)
                    {
                        mb_d.add_cart_art("cart sp spirit blood" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                    }

                    else if (tp == par_cart.type_cart.spirit_rock)
                    {
                        mb_d.add_cart_art("cart sp spirit rock" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                    }

                    else if (tp == par_cart.type_cart.spirit_poition)
                    {
                        mb_d.add_cart_art("cart sp spirit poition" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
                    }

                    else if (tp == par_cart.type_cart.spirit_fire)
                    {
                        mb_d.add_cart_art("cart sp spirit fire" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
                    }

                    else if (tp == par_cart.type_cart.spirit_ice)
                    {
                        mb_d.add_cart_art("cart sp spirit ice" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
                    }

                    else if (tp == par_cart.type_cart.spirit_energy)
                    {
                        mb_d.add_cart_art("cart sp spirit energy" + st);
                        mb_d.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
                    }




                    if (func.baza[cart].GetComponent<par_cart>().spetial)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            mb_d.add_cart(cart);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            mb_d.add_cart(cart);
                        }
                    }
                }
                mb_d.viz(func.eath[x, y].active);
                mb_d.friend = parent.GetComponent<par_mob>().friend;
                mb_d.parent=parent;
                if (mb_d.friend)
                {
                    mb_d.friend_type = par_mob.type_friend.auto;
                }
                else
                    mb_d.bl_wolk = true;
                mb_d.update_viz();
                //

            }
        }
        /*
        else if (gameObject.name == "spirit blood ef" && power > 0)
        {
            GameObject g_c=func.create_lovyshka(x, y, "spirit base", parent, power, 1);
            g_c.GetComponent<par_lovyshka>().par_carts = new List<string> { "cart blood" };
            func.set_anim_color(g_c, g_c.GetComponent<par_lovyshka>().par_carts);

        }
        else if (gameObject.name == "spirit fire ef" && power > 0)
        {
            GameObject g_c = func.create_lovyshka(x, y, "spirit base", parent, power, 1);
            g_c.GetComponent<par_lovyshka>().par_carts = new List<string> { "cart damage", "cart fire" };
            func.set_anim_color(g_c, new List<string> { "cart damage","cart fire" });
        }
        else if (gameObject.name == "spirit ice ef" && power > 0)
        {
            GameObject g_c = func.create_lovyshka(x, y, "spirit base", parent, power, 1);
            g_c.GetComponent<par_lovyshka>().par_carts = new List<string> { "cart ice" };
            func.set_anim_color(g_c, g_c.GetComponent<par_lovyshka>().par_carts);
        }
        else if (gameObject.name == "spirit poition ef" && power > 0)
        {
            GameObject g_c = func.create_lovyshka(x, y, "spirit base", parent, power, 1);
            g_c.GetComponent<par_lovyshka>().par_carts = new List<string> { "cart poition" };
            func.set_anim_color(g_c, g_c.GetComponent<par_lovyshka>().par_carts);
        }


        */

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
                    int pow_baf = 0;
                    if (st_par==null || st_par.Count==0)
                    {
                        pow_baf = 1;
                    }
                    else
                    {
                        st_par.Pop();
                        pow_baf = int.Parse(st_par.Pop());
                    }

                    par_baf.type_baf type_baf = par_baf.type_baf.seal_spetial;
                    
                    
                    mb.add_baf(type_baf, power * 4,pow_baf,cart_par);
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

                int dop_ef = 1;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit rock")
                        dop_ef++;
                        
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit rock heart")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart rock heart" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart,x,y);
                    }
                }
                if (regen > 0)
                {
                    mb.regen(power * regen, "blood");
                }
                else if (power > 0)
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

                int dop_ef = 1;
                int regen = 0;
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit blood")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit blood heart")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart blood heart" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
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
                int dop_ef = 1;
                int regen = 0;
                //if(mb_par!=null)
                foreach (par_cart_art cart in mb_par.lt_carts_art)
                {
                    if (cart.name == "cart sp spirit fire")
                        dop_ef++;
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit fire heart")
                    {
                        regen++;
                    }
                    else if(cart.name == "cart fire heart" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
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
                    if (cart.name == "cart spirit energy heart")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart energy heart" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
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
                    if (cart.name == "cart spirit ice heart")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart ice heart" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
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
                    if (cart.name == "cart spirit poition heart")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart poition heart" && cart.q > 0)
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
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




        else if (lt_type_carts[0] == par_cart.type_cart.damage)
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
                    type_dm = "base";
                }
            }
            else
            {
                int pow1 = GameObject.Find(cart_par[1]).GetComponent<par_cart>().lvl;

                if (lt_type_carts[1] == par_cart.type_cart.rock)
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
                        type_dm = "rock";
                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.blood)
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
                        type_dm = "blood";
                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.fire)
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
                        type_dm = "fire";
                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.energy)
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
                        type_dm = "energy";
                    }

                }

                else if (lt_type_carts[1] == par_cart.type_cart.ice)
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
                        type_dm = "ice";
                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.poition)
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
                        type_dm = "poition";
                    }

                }
            }


            if (mb != null)
            {
                int regen = 0;
                int dop_ef = 1;
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.name == "cart spirit poition heart" && type_dm == "poition")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart poition heart" && cart.q > 0 && type_dm == "poition")
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
                    }

                    else if (cart.name == "cart spirit rock heart" && type_dm == "rock")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart rock heart" && cart.q > 0 && type_dm == "rock")
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
                    }

                    else if (cart.name == "cart spirit blood heart" && type_dm == "blood")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart blood heart" && cart.q > 0 && type_dm == "blood")
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
                    }

                    else if (cart.name == "cart spirit ice heart" && type_dm == "ice")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart ice heart" && cart.q > 0 && type_dm == "ice")
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
                    }

                    else if (cart.name == "cart spirit energy heart" && type_dm == "energy")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart energy heart" && cart.q > 0 && type_dm == "energy")
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
                    }

                    else if (cart.name == "cart spirit fire heart" && type_dm == "fire")
                    {
                        regen++;
                    }
                    else if (cart.name == "cart fire heart" && cart.q > 0 && type_dm == "fire")
                    {
                        regen++;
                        mb.activate_art(cart, x, y);
                    }
                }
                if(regen>0)
                    mb.regen(power * regen, "blood");
                else
                    mb.damage(power*dop_ef, type_dm);
            }
                
        }
        else if (lt_type_carts[0] == par_cart.type_cart.sp_barier)
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

                if (lt_type_carts[1] == par_cart.type_cart.rock)
                {
                    if (mb != null)
                    {

                        type_dm = par_baf.type_baf.bar_rock;

                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.blood)
                {
                    if (mb != null)
                    {

                        type_dm = par_baf.type_baf.bar_blood;

                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.fire)
                {
                    if (mb != null)
                    {
                        type_dm = par_baf.type_baf.bar_fire;

                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.energy)
                {
                    if (mb != null)
                    {

                        type_dm  = par_baf.type_baf.bar_energy;

                    }

                }

                else if (lt_type_carts[1] == par_cart.type_cart.ice)
                {
                    if (mb != null)
                    {

                        type_dm = par_baf.type_baf.bar_ice;

                    }

                }
                else if (lt_type_carts[1] == par_cart.type_cart.poition)
                {
                    if (mb != null)
                    {
                        //Debug.Log("damage effect");
                        //if (player_name == "poition player" && bl_pl)
                        //power += pow1 * 2;
                        //mb.damage(power, "energy");
                        type_dm = par_baf.type_baf.bar_poition;
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
            int dop_ef = 1;
            /*
            foreach (par_cart_art cart in mb_par.lt_carts_art)
            {
                if (cart.name == "cart shaman boss" && cart.perez_now == 0)
                {
                    dop_ef++;
                    mb.activate_art(cart);
                }
            }
            */
            if (mb != null && power > 0)
                mb.add_baf(type_dm, -1, power * 2*dop_ef);
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
