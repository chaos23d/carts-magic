using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.EventSystems.EventTrigger;
using System.Linq;
using JetBrains.Annotations;
using Unity.Android.Gradle.Manifest;
using type_wait= func.controller_mob_wait.type_wait;

//using System;

public class par_mob : parametrs, IPointerClickHandler, interfase_click
{
    // Start is called before the first frame update
    //public int col_skills;
    //public GameObject ray



    public void spawn_carts(List<string> lt_c,GameObject from)
    {
        foreach (string st in lt_c)
        {
            spawn_cart(st, from);
        }
       
    }
    public enum type_friend
    {
        none,
        auto,
        active,
        disactive
    }
    public class priority_atack
    {
        public int pos = 0;
        public type_atack type;
        public enum type_atack
        {
            none,
            damage,
            barier, bomb, dych,
            blood, rock, ice, energy, fire, poition,
            elemental,
            spider,
            seal,
            sp_boss

        }
        public priority_atack(type_atack tp, int pos)
        {
            this.type = tp;
            this.pos = pos;
        }
    }
    public void base_pr()
    {
        //Debug.Log(System.Enum.GetValues(typeof(priority_atack.type_atack)).Length+" aaaa");
        type_priority_attack.Add(new priority_atack(priority_atack.type_atack.none, 0));
        /*
        foreach (priority_atack.type_atack tp in System.Enum.GetValues(typeof(priority_atack.type_atack)))
        {
            Debug.Log(tp + " aaaa");
            type_priority_attack.Add( new priority_atack( tp, 0));
        }
        */
    }
    [Header("mob parametrs")]
    public List<string> par_lt_sila = new List<string>();
    public List<string> par_lt_sila_base = new List<string>();
    //public List<string> par_lt_sila_save = new List<string>();

    public List<string> par_lt_speed = new List<string>();
    public List<string> par_lt_speed_base = new List<string>();
    [Header("base parametrs")]

    public bool friendly = false;
    public type_friend friend_type = type_friend.none;
    public bool nps = false;
    //public int distanse_min = 1;
    public int distanse_max = 1;
    public int distanse_attack = 3;
    public bool vizited = false;
    public bool can_atacked = false;
    public int long_life = -1;
    //public List<string> lt_mob_speed_par = new List<string>();
    //public List<string> lt_mob_speed_par_base = new List<string>();
    //public List<string> lt_mob_sila_par = new List<string>();
    //public List<string> lt_mob_sila_par_base = new List<string>();
    public bool contains_cart_type(par_cart.type_cart tp)
    {
        bool bl = false;
        if (tp == par_cart.type_cart.damage && (lt_carts.Contains("cart damage") || lt_carts.Contains("cart damage 1") || lt_carts.Contains("cart damage 2")))
            bl = true;
        else if (tp == par_cart.type_cart.sp_barier && (lt_carts.Contains("cart barier") || lt_carts.Contains("cart barier 1") || lt_carts.Contains("cart barier 2")))
            bl = true;
        else if (tp == par_cart.type_cart.sp_dych && (lt_carts.Contains("cart dych") || lt_carts.Contains("cart dych 1") || lt_carts.Contains("cart dych 2")))
            bl = true;
        else if (tp == par_cart.type_cart.sp_bomb && (lt_carts.Contains("cart bomb") || lt_carts.Contains("cart bomb 1") || lt_carts.Contains("cart bomb 2")))
            bl = true;

        else if (tp == par_cart.type_cart.blood && (lt_carts.Contains("cart blood") || lt_carts.Contains("cart blood 1") || lt_carts.Contains("cart blood 2") || lt_carts.Contains("cart spirit blood")))
            bl = true;
        else if (tp == par_cart.type_cart.rock && (lt_carts.Contains("cart rock") || lt_carts.Contains("cart rock 1") || lt_carts.Contains("cart rock 2") || lt_carts.Contains("cart spirit rock")))
            bl = true;
        else if (tp == par_cart.type_cart.fire && (lt_carts.Contains("cart fire") || lt_carts.Contains("cart fire 1") || lt_carts.Contains("cart fire 2") || lt_carts.Contains("cart spirit fire")))
            bl = true;
        else if (tp == par_cart.type_cart.poition && (lt_carts.Contains("cart poition") || lt_carts.Contains("cart poition 1") || lt_carts.Contains("cart poition 2") || lt_carts.Contains("cart spirit poition")))
            bl = true;
        else if (tp == par_cart.type_cart.ice && (lt_carts.Contains("cart ice") || lt_carts.Contains("cart ice 1") || lt_carts.Contains("cart ice 2") || lt_carts.Contains("cart spirit ice")))
            bl = true;
        else if (tp == par_cart.type_cart.energy && (lt_carts.Contains("cart energy") || lt_carts.Contains("cart energy 1") || lt_carts.Contains("cart energy 2") || lt_carts.Contains("cart spirit energy")))
            bl = true;

        /*
        else if (tp == par_cart.type_cart.spirit_blood && lt_carts.Contains("cart spirit blood"))
            bl = true;
        else if (tp == par_cart.type_cart.spirit_rock && (lt_carts.Contains("cart spirit rock")))
            bl = true;
        else if (tp == par_cart.type_cart.spirit_fire && (lt_carts.Contains("cart spirit fire") ))
            bl = true;
        else if (tp == par_cart.type_cart.spirit_poition && (lt_carts.Contains("cart spirit poition") ))
            bl = true;
        else if (tp == par_cart.type_cart.spirit_ice && (lt_carts.Contains("cart spirit ice")))
            bl = true;
        else if (tp == par_cart.type_cart.spirit_energy && (lt_carts.Contains("cart spirit energy")))
            bl = true;
        */
        return bl;
    }
    public bool contains_pr_at(priority_atack.type_atack tp)
    {
        return (get_pr_at(tp) != -1);
    }
    public int get_pr_at(priority_atack.type_atack tp)
    {
        //if()
        bool bl_tip = false;
        int pos = -1;
        //Debug.Log("priority " + type_priority_attack.Count);
        foreach (priority_atack pr in type_priority_attack)
        {
            Debug.Log(pr.type + " " + pr.pos);
            if (pr.type == priority_atack.type_atack.none && !bl_tip)
            {
                pos = pr.pos;
            }
            else if (pr.type == tp)
            {
                pos = pr.pos;
                bl_tip = true;
                break;
            }

        }
        return pos;
    }
    public List<priority_atack> type_priority_attack = new List<priority_atack>();
    [Header("carts parametrs")]
    public List<string> lt_carts = new List<string>();
    public List<par_cart_art> lt_carts_art = new List<par_cart_art>();
    public List<GameObject> lt_carts_art_gm = new List<GameObject>();
    public Dictionary<string, int> carts_count_max = new Dictionary<string, int>();

    public List<string> lt_carts_active_now = new List<string>();
    public List<string> lt_carts_added = new List<string>();
    public List<string> lt_carts_added_now = new List<string>();
    [Header("other parametrs")]


    public List<string> lt_carts_active_now_ui = new List<string>();

    public List<Vector2> lt_v2_vizited = new List<Vector2>();
    public List<GameObject> lt_mob_vizited = new List<GameObject>();
    public List<GameObject> lt_mob_vizited_in = new List<GameObject>();

    public bool atack_auto_comb = false;
    public bool hod_now = false;
    public func func;
    public int num;
    public GameObject cel;

    public GameObject parent;
    public List<GameObject> childs = new List<GameObject>();
    public List<GameObject> atack_childs_now = new List<GameObject>();

    public List<object> lt_par = new List<object>();

    public bool run_mob = true;



    public bool friend
    {
        get
        {
            return friendly;
        }
        set
        {
            if (value)
                func.col_friend++;
            else
                func.col_friend--;
            friendly = value;
        }
    }

    public int speed = 1;
    public int speed_now = 0;
    public bool atacked = false;
    //public bool minus_speed = true;

    public bool wolked = true;
    public bool speed_hod = false;
    public List<par_baf> lt_bafs = new List<par_baf>();

    public bool bl_wolk = false;

    public int col_art = 4;
    public int col_art_inven = 5;

    public string cart_active_now = null;

    public bool selected_anim = false;
    public bool bool_func_wolk_v2 = false;

    public string effect_activate;


    public Stack<Vector2> lt_wolk = new Stack<Vector2>();

    public float time_wolk_now = -1;
    /*
    public float time_wolk_now
    {
        get
        {
            return time_wolk_now_gs;
        }
        set
        {
            Debug.Log(value);
            time_wolk_now_gs = value;
        }
    }
    */
    public float time_wolk = 0.15f;
    public Vector2 d_v2_wolk;
    public Vector2 d_v2_wolk_end;

    public GameObject effect_now
    {
        get
        {
            return gg;
        }
        set
        {
            //Debug.Log("value= " + value);
            if (gg != null)
            {
                //lt_effect_now.Add(gg);
            }
            else
            {
                lt_effect_now.Clear();
            }
            gg = value;
        }
    }
    public GameObject gg = null;
    public List<GameObject> lt_effect_now = new List<GameObject>();


    public void add_baf(par_baf.type_baf baf, int time)
    {
        add_baf(baf, time, 0);
    }
    public void add_baf(par_baf.type_baf baf, int time, int pow)
    {
        add_baf(baf, time, pow, null);
    }
    public void add_baf(par_baf.type_baf baf, int time, int power, List<string> lt_par)
    {
        Debug.Log("add baf " + baf + " " + time + " " + power);
        List<par_baf.type_baf> lt_baf_pow_time = new List<par_baf.type_baf> {



            par_baf.type_baf.seal_spetial};
        List<par_baf.type_baf> lt_baf_barier = new List<par_baf.type_baf> { par_baf.type_baf.bar,
            par_baf.type_baf.bar_fire,
            par_baf.type_baf.bar_rock,
            par_baf.type_baf.bar_poition,
            par_baf.type_baf.bar_blood,
            par_baf.type_baf.bar_energy,
            par_baf.type_baf.bar_ice };

        par_baf bf_first = null;
        bool bl = true;
        foreach (par_baf pr_baf in lt_bafs)
        {

            if (pr_baf.type == par_baf.type_baf.seal_spetial)
            {
                List<par_cart.type_cart> lt_tp1 = null;
                if (lt_par != null)
                {
                    lt_par.ConvertAll(x =>
                    {
                        return GameObject.Find(x).GetComponent<par_cart>().type;
                    });
                }

                List<par_cart.type_cart> lt_tp2 = pr_baf.lt_cart.ConvertAll(x =>
                {
                    return GameObject.Find(x).GetComponent<par_cart>().type;
                });
                //lt_tp1.
                if (lt_tp1 != null && lt_tp1.SequenceEqual(lt_tp2))
                {
                    bf_first = pr_baf;
                }
            }
            else if ((lt_baf_barier.Contains(pr_baf.type) && lt_baf_barier.Contains(baf))){
                bf_first = pr_baf;
                bl = false;
            }
            else if (pr_baf.type == baf && (!lt_baf_pow_time.Contains(pr_baf.type)) || (lt_baf_pow_time.Contains(pr_baf.type) && pr_baf.power == power))
                bf_first = pr_baf;
        }
        
        /*
        if (bf_first != null)
        {
            if (bf_first.type == par_baf.type_baf.bar)
                bl = false;
            if(bf_first.type == par_baf.type_baf.bar_energy)
                bl = false;
            if (bf_first.type == par_baf.type_baf.bar_rock)
                bl = false;
            if (bf_first.type == par_baf.type_baf.bar_blood)
                bl = false;
            if (bf_first.type == par_baf.type_baf.bar_fire)
                bl = false;
            if (bf_first.type == par_baf.type_baf.bar_ice)
                bl = false;
            if (bf_first.type == par_baf.type_baf.bar_poition)
                bl = false;
        }
        */
        par_baf new_baf = null;
        if (bf_first == null)
        {
            bl = false;
            GameObject gm_bf = null;
            new_baf = gameObject.AddComponent<par_baf>();
            if (baf == par_baf.type_baf.fire)
            {
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("fire").gameObject, gameObject.transform);
                gm_bf.name = "fire";
            }
            else if (baf == par_baf.type_baf.seal_spetial)
            {
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal").gameObject, gameObject.transform);
                List<par_cart.type_cart> lt_tp = lt_par.ConvertAll(x =>
                {
                    return GameObject.Find(x).GetComponent<par_cart>().type;
                });
                string stt = "seal ";
                for (int i = 0; i < lt_tp.Count; i++)
                {
                    stt += lt_tp[i];
                    if (i < lt_tp.Count - 1)
                        stt += "-";
                }
                //List<List<string>> lt_lt_st = par_cart.cart_combinations.get_comb_now(st_comb);
                //gm_bf.name =
                gm_bf.name = stt;

                func.set_anim_color(gm_bf, lt_par);
            }
            else if (baf == par_baf.type_baf.rage)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                new_baf.skip_first = true;
                //speed++;
                //speed_now++;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("rage").gameObject, gameObject.transform);
                gm_bf.name = "rage";
                //gameObject.GetComponent<Animator>().speed = 1.5f;
            }

            else if (baf == par_baf.type_baf.berserk)
            {
                
            }



            else if (baf == par_baf.type_baf.energy)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.before = false;
                //new_baf.skip_first = true;
                lt_bafs.Add(new_baf);
                speed++;
                speed_now++;


                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("energy").gameObject, gameObject.transform);
                gm_bf.name = "energy";

                gameObject.GetComponent<Animator>().speed = 1.5f;
                //gameObject.GetComponent<Animator>().speed = 5;
            }
            else if (baf == par_baf.type_baf.poition)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.power = power;
                new_baf.type = baf;
                new_baf.before = false;
                lt_bafs.Add(new_baf);
                //speed++;
                //speed_now++;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("poition").gameObject, gameObject.transform);
                gm_bf.name = "poition";

                //gameObject.GetComponent<Animator>().speed = 1.5f;
            }
            else if (baf == par_baf.type_baf.ice)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.before = false;
                lt_bafs.Add(new_baf);
                speed--;
                speed_now--;
                /*
                if (speed == 1)
                {
                    speed_now++;

                }
                else if (!speed_hod && hod_now)
                {
                    new_baf.skip_first = true;
                }
                */
                //if(speed==1)
                //speed_now++;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("ice").gameObject, gameObject.transform);
                gm_bf.name = "ice";

                gameObject.GetComponent<Animator>().speed = 0.5f;
                gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.25f, 1, 1, 1);
            }




            else if (baf == par_baf.type_baf.seal)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.skip_first = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal").gameObject, gameObject.transform);
                gm_bf.name = "seal";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }





            else if (baf == par_baf.type_baf.bar)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases").gameObject, gameObject.transform);
                gm_bf.name = "bar bases";
            }


            else if (baf == par_baf.type_baf.bar_energy)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases energy").gameObject, gameObject.transform);
                gm_bf.name = "bar bases energy";
            }
            else if (baf == par_baf.type_baf.bar_rock)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases rock").gameObject, gameObject.transform);
                gm_bf.name = "bar bases rock";
            }
            else if (baf == par_baf.type_baf.bar_blood)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases blood").gameObject, gameObject.transform);
                gm_bf.name = "bar bases blood";
            }
            else if (baf == par_baf.type_baf.bar_fire)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases fire").gameObject, gameObject.transform);
                gm_bf.name = "bar bases fire";
            }
            else if (baf == par_baf.type_baf.bar_ice)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases ice").gameObject, gameObject.transform);
                gm_bf.name = "bar bases ice";
            }
            else if (baf == par_baf.type_baf.bar_poition)
            {
                //new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases poition").gameObject, gameObject.transform);
                gm_bf.name = "bar bases poition";
            }

            /*
            else if (baf == par_baf.type_baf.bar_energy)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar energy").gameObject, gameObject.transform);
                gm_bf.name = "bar energy";
            }
            else if (baf == par_baf.type_baf.bar_rock)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar rock").gameObject, gameObject.transform);
                gm_bf.name = "bar rock";
            }
            else if (baf == par_baf.type_baf.bar_blood)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar blood").gameObject, gameObject.transform);
                gm_bf.name = "bar blood";
            }
            else if (baf == par_baf.type_baf.bar_fire)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar fire").gameObject, gameObject.transform);
                gm_bf.name = "bar fire";
            }
            else if (baf == par_baf.type_baf.bar_ice)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar ice").gameObject, gameObject.transform);
                gm_bf.name = "bar ice";
            }
            else if (baf == par_baf.type_baf.bar_poition)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar poition").gameObject, gameObject.transform);
                gm_bf.name = "bar poition";
            }
            */
            if (gm_bf != null)
            {

                gm_bf.transform.localPosition = new Vector3(0, 0, 0);
                gm_bf.active = vizited;
                new_baf.gm_anim = gm_bf;
            }

        }
        else
        {
            //new_baf = bf_first;
            //new_baf.gm_anim = bf_first.gm_anim;
        }
        if (baf == par_baf.type_baf.fire)
        {
            if (new_baf == null)
                new_baf = gameObject.AddComponent<par_baf>();
            new_baf.long_baf = time;
            new_baf.type = baf;
            new_baf.before = false;
            new_baf.power = power;

            lt_bafs.Add(new_baf);

            if (bf_first != null)
                new_baf.gm_anim = bf_first.gm_anim;
        }
        else if (baf == par_baf.type_baf.seal_spetial)
        {
            if (new_baf == null)
                new_baf = gameObject.AddComponent<par_baf>();
            new_baf.long_baf = time;
            new_baf.type = baf;
            new_baf.power = power;
            new_baf.before = true;
            new_baf.lt_cart = lt_par;
            lt_bafs.Add(new_baf);
            //speed--;
            //speed_now--;
            //gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal").gameObject, gameObject.transform);
            //gm_bf.name = "seal";

            //gameObject.GetComponent<Animator>().speed = 0.5f;
            //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            if (bf_first != null)
                new_baf.gm_anim = bf_first.gm_anim;
        }
        else if (baf == par_baf.type_baf.berserk)
        {
            if (new_baf == null)
                new_baf = gameObject.AddComponent<par_baf>();
            new_baf.long_baf = time;
            new_baf.power = power;
            new_baf.skip_first = true;
            //new_baf.time = time;
            new_baf.type = baf;
            new_baf.before = false;
            lt_bafs.Add(new_baf);

            if (bf_first != null)
                new_baf.gm_anim = bf_first.gm_anim;
        }
        /*
        else if (baf == par_baf.type_baf.berserk && bf_first!=null)
        {
            bf_first.power += power;


        }
        */
        else if (lt_baf_barier.Contains(baf) && bf_first != null && bf_first.power < power)
        {
            bf_first.power = power;


        }
        else if (bl)
        {
            bf_first.long_baf += time;
        }
        if (num == 0)
            update_bufs();
    }
    public void update_bufs()
    {
        GameObject pn_bf = GameObject.Find("canvases").transform.Find("Canvas").Find("pn bufs").gameObject;
        int iii = 0;
        foreach (Transform tr in pn_bf.transform)
        {
            Destroy(tr.gameObject);
            /*
            if (iii < 100)
                iii++;
            else
            {
                Debug.LogError("error");
                break;
            }
            */
        }
        /*
        while (pn_bf.transform.childCount > 0)
        {
            Destroy(pn_bf.transform.GetChild(0).gameObject);
            if (iii < 100)
                iii++;
            else
            {
                Debug.LogError("error");
                break;
            }
        }*/
        Dictionary<par_baf, int> lt_bafs_now = new Dictionary<par_baf, int>();
        foreach (par_baf pr_baf in lt_bafs)
        {
            par_baf count = null;
            foreach (par_baf bf1 in lt_bafs_now.Keys)
            {
                if (pr_baf.type == bf1.type && pr_baf.long_baf == bf1.long_baf && pr_baf.power == bf1.power)
                {
                    count = bf1;
                    break;
                }

            }
            if (count != null)
            {
                lt_bafs_now[count]++;
            }
            else
            {
                lt_bafs_now.Add(pr_baf, 1);
            }

        }
        foreach (par_baf pr_baf in lt_bafs_now.Keys)
        {
            GameObject gm_bf = Instantiate(GameObject.Find("panels").transform.Find("ignoring").Find("baf").gameObject, pn_bf.transform);
            string power = "";
            string col = "";
            string time = "";
            if (lt_bafs_now[pr_baf] > 1)
            {
                col = lt_bafs_now[pr_baf].ToString();
            }
            if (pr_baf.power != 0)
            {
                power = "[" + pr_baf.power.ToString() + "]";
            }
            if (pr_baf.long_baf > 0)
            {
                time = pr_baf.long_baf.ToString();
            }
            gm_bf.transform.Find("tx baf").gameObject.GetComponent<TMP_Text>().text = time + power;
            gm_bf.transform.Find("tx baf count").gameObject.GetComponent<TMP_Text>().text = col;
            gm_bf.transform.Find("image baf").gameObject.GetComponent<Image>().sprite = func.bf_sp[pr_baf.type];
        }
    }
    public bool activate_art(par_cart_art cart)
    {
        return activate_art(cart, -1, -1, null);
    }
    public bool activate_art(par_cart_art cart, int x, int y)
    {
        return activate_art(cart, x, y, null);
    }
    public bool activate_art(par_cart_art cart, int x, int y, object par)
    {
        Stack<object> st_par = new Stack<object>();
        st_par.Push(par);
        return activate_art(cart, x, y, st_par);
    }
    public static Stack<object> get_par_q(int i)
    {
        Stack<object> st_o = new Stack<object>();
        st_o.Push(i);
        st_o.Push("par -q");
        return st_o;
    }
    public bool activate_art(par_cart_art cart, int x, int y, Stack<object> st_par)
    {

        bool bl = false;
        bool bl_dop = true;
        List<GameObject> lt_art = new List<GameObject>();
        bool bl_passive = (x == -1 && y == -1);

        if (gameObject.transform.Find("artefacts") != null)
        {
            foreach (Transform tr in gameObject.transform.Find("artefacts"))
            {
                lt_art.Add(tr.gameObject);

            }
        }
        else
        {
            for (int i = 0; i < lt_carts_art.Count; i++)
            {
                lt_art.Add(null);
            }
        }
        int a = -1;// lt_carts_art.LastIndexOf(cart);
        for (int i = 0; i < lt_carts_art.Count; i++)
        {
            if (cart.Equals(lt_carts_art[i]))
            {
                //Debug.Log(cart.name + "  ==== " + lt_carts_art[i].name);
                a = i;
                break;
            }

        }

        //Debug.Log(a + "  " + cart.name + "  "+lt_carts_art[a].name +"  "+ lt_art[a].transform.localPosition);
        if (cart.q_max > 0)
        {

        }
        if (bl_passive)
        {
            if (!vizited)
                lt_art[a] = null;

            if (cart.name == "cart sp rock")
                bl = spawn_cart("cart rock", lt_art[a]);
            else if (cart.name == "cart sp fire")
                bl = spawn_cart("cart fire", lt_art[a]);
            else if (cart.name == "cart sp energy")
                bl = spawn_cart("cart energy", lt_art[a]);
            else if (cart.name == "cart sp blood")
                bl = spawn_cart("cart blood", lt_art[a]);
            else if (cart.name == "cart sp ice")
                bl = spawn_cart("cart ice", lt_art[a]);
            else if (cart.name == "cart sp poition")
                bl = spawn_cart("cart poition", lt_art[a]);

            else if (cart.name == "cart sp rock 1")
                bl = spawn_cart("cart rock 1", lt_art[a]);
            else if (cart.name == "cart sp fire 1")
                bl = spawn_cart("cart fire 1", lt_art[a]);
            else if (cart.name == "cart sp energy 1")
                bl = spawn_cart("cart energy 1", lt_art[a]);
            else if (cart.name == "cart sp blood 1")
                bl = spawn_cart("cart blood 1", lt_art[a]);
            else if (cart.name == "cart sp ice 1")
                bl = spawn_cart("cart ice 1", lt_art[a]);
            else if (cart.name == "cart sp poition 1")
                bl = spawn_cart("cart poition 1", lt_art[a]);

            else if (cart.name == "cart sp rock 2")
                bl = spawn_cart("cart rock 2", lt_art[a]);
            else if (cart.name == "cart sp fire 2")
                bl = spawn_cart("cart fire 2", lt_art[a]);
            else if (cart.name == "cart sp energy 2")
                bl = spawn_cart("cart energy 2", lt_art[a]);
            else if (cart.name == "cart sp blood 2")
                bl = spawn_cart("cart blood 2", lt_art[a]);
            else if (cart.name == "cart sp ice 2")
                bl = spawn_cart("cart ice 2", lt_art[a]);
            else if (cart.name == "cart sp poition 2")
                bl = spawn_cart("cart poition 2", lt_art[a]);


            else if (cart.name == "cart sp atack")
                bl = spawn_cart("cart damage", lt_art[a]);
            else if (cart.name == "cart sp barier")
                bl = spawn_cart("cart barier", lt_art[a]);

            else if (cart.name == "cart sp atack 1")
                bl = spawn_cart("cart damage 1", lt_art[a]);
            else if (cart.name == "cart sp barier 1")
                bl = spawn_cart("cart barier 1", lt_art[a]);

            else if (cart.name == "cart sp atack 2")
                bl = spawn_cart("cart damage 2", lt_art[a]);
            else if (cart.name == "cart sp barier 2")
                bl = spawn_cart("cart barier 2", lt_art[a]);

            else if (cart.name == "cart sp bomb")
                bl = spawn_cart("cart bomb", lt_art[a]);
            else if (cart.name == "cart sp bomb 1")
                bl = spawn_cart("cart bomb 1", lt_art[a]);
            else if (cart.name == "cart sp bomb 2")
                bl = spawn_cart("cart bomb 2", lt_art[a]);

            else if (cart.name == "cart sp derlorg")
                bl = spawn_cart("cart derlorg", lt_art[a]);

            else if (cart.name == "cart sp dych")
                bl = spawn_cart("cart dych", lt_art[a]);
            else if (cart.name == "cart sp dych 1")
                bl = spawn_cart("cart dych 1", lt_art[a]);
            else if (cart.name == "cart sp dych 2")
                bl = spawn_cart("cart dych 2", lt_art[a]);
            //else if (cart.name == "cart sp spider(spawned)")
            //bl = spawn_cart("cart spider poition", lt_art[a]);

            else if (cart.name == "cart elemental")
            {
                bool bl1 = false;
                bl1 = spawn_cart("cart ice", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart energy", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart poition", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart fire", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart blood", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart rock", lt_art[a]) || bl1;
                bl = bl1;
            }

            else if (cart.name == "cart universal")
            {
                bool bl1 = false;
                bl1 = spawn_cart("cart dych", lt_art[a])||bl1 ;
                //bl1 = bl1 || spawn_cart("cart damage", lt_art[a]);
                bl1 = spawn_cart("cart barier", lt_art[a]) || bl1 ;
                bl1 = spawn_cart("cart bomb", lt_art[a])|| bl1 ;
                bl = bl1;
            }


            else if (cart.name == "cart elemental 1")
            {
                bool bl1 = false;
                bl1 = spawn_cart("cart ice 1", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart energy 1", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart poition 1", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart fire 1", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart blood 1", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart rock 1", lt_art[a]) || bl1;
                bl = bl1;
            }

            else if (cart.name == "cart universal 1")
            {
                bool bl1 = false;
                bl1 = spawn_cart("cart dych 1", lt_art[a]) || bl1;
                //bl1 = bl1 || spawn_cart("cart damage", lt_art[a]);
                bl1 = spawn_cart("cart barier 1", lt_art[a]) || bl1;
                bl1 = spawn_cart("cart bomb 1", lt_art[a]) || bl1;
                bl = bl1;
            }


            else if (cart.name == "cart base st")
            {
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                bl = bl1;
            }
            else if (cart.name == "cart att st")
            {
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                spawn_cart("cart damage", lt_art[a]);
                bl = bl1;
            }
            else if (cart.name == "cart bar st")
            {
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]) || spawn_cart("cart barier", lt_art[a]);
                bl = bl1;
            }
            else if (cart.name == "cart fire st")
            {

                bl = spawn_cart("cart fire", lt_art[a]);
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                bl = bl || bl1;
            }
            else if (cart.name == "cart rock st")
            {
                bl = spawn_cart("cart rock", lt_art[a]);
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                //bl2 = spawn_cart("cart barier", lt_art[a]);
                bl = bl || bl1;
            }
            else if (cart.name == "cart blood st")
            {
                bl = spawn_cart("cart blood", lt_art[a]);
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                //bl2 = spawn_cart("cart barier", lt_art[a]);
                bl = bl || bl1;
            }
            else if (cart.name == "cart ice st")
            {
                bl = spawn_cart("cart ice", lt_art[a]);
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                //bl2 = spawn_cart("cart barier", lt_art[a]);
                bl = bl || bl1;
            }
            else if (cart.name == "cart energy st")
            {
                bl = spawn_cart("cart energy", lt_art[a]);
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                //bl2 = spawn_cart("cart barier", lt_art[a]);
                bl = bl || bl1;
            }
            else if (cart.name == "cart poition st")
            {
                bl = spawn_cart("cart poition", lt_art[a]);
                bool bl1, bl2;
                bl1 = spawn_cart("cart damage", lt_art[a]);
                //bl2 = spawn_cart("cart barier", lt_art[a]);
                bl = bl || bl1;
            }



            else if (cart.name == "cart sp spirit blood")
            {
                bl = spawn_cart("cart spirit blood", lt_art[a]);
            }
            else if (cart.name == "cart sp spirit rock")
            {
                bl = spawn_cart("cart spirit rock", lt_art[a]);
            }
            else if (cart.name == "cart sp spirit fire")
            {
                bl = spawn_cart("cart spirit fire", lt_art[a]);
            }
            else if (cart.name == "cart sp spirit poition")
            {
                bl = spawn_cart("cart spirit poition", lt_art[a]);
            }
            else if (cart.name == "cart sp spirit ice")
            {
                bl = spawn_cart("cart spirit ice", lt_art[a]);
            }
            else if (cart.name == "cart sp spirit energy")
            {
                bl = spawn_cart("cart spirit energy", lt_art[a]);
            }

            else if (cart.name == "cart spirit")
            {
                bl = spawn_cart("cart spirit(boss)", lt_art[a]);
            }
            else if (cart.name == "cart rage")
            {
                bl = spawn_cart("cart rage(boss)", lt_art[a]);
            }

            else if (cart.name == "cart dungalorg(boss)")
            {
                //bl = spawn_cart("cart dungalorg(sleep)", lt_art[a]) || spawn_cart("cart derlorg", lt_art[a]);

                bl = spawn_cart("cart dungalorg(sleep)", lt_art[a]);
                bool bl1, bl2;
                bl1 = spawn_cart("cart derlorg", lt_art[a]);
                //bl2 = spawn_cart("cart barier", lt_art[a]);
                bl = bl || bl1;
            }


            else if (cart.name == "cart berserk st")
            {
                //regen(hp_max  / 5, "none");
                bl = spawn_cart("cart damage", lt_art[a]); ;
            }
            else if (cart.name == "cart berserk")
            {
                //func.create_effect("cart rage(boss)", x, y, gameObject);
                func.create_effect(func.get_name_effect(new List<string> { "cart rage(boss)" }, null), this.x, this.y, gameObject, false, 2, new List<string> { "cart rage(boss)" });
            }


            else if (cart.type == par_cart.type_cart.art_blood_cuzna ||
                cart.type == par_cart.type_cart.art_ice_cuzna ||
                cart.type == par_cart.type_cart.art_energy_cuzna ||
                cart.type == par_cart.type_cart.art_fire_cuzna ||
                cart.type == par_cart.type_cart.art_poition_cuzna ||
                cart.type == par_cart.type_cart.art_rock_cuzna ||
                cart.type == par_cart.type_cart.art_att_cuzna||
                cart.type == par_cart.type_cart.art_bar_cuzna||
                cart.type == par_cart.type_cart.art_bomb_cuzna||
                cart.type == par_cart.type_cart.art_dych_cuzna)
            {
                spawn_cart(st_par.Peek().ToString(), lt_art[a]);
                bl = true;
                //Debug.Log("aaaaaaaccccccttt");

                //bl = true;
                //if (lt_art[a] != null)
                //func.create_effect("activate art", null, lt_art[a].transform.position, true);
                //speed_now++;
                //func.nums_mob_wolk.Add(num);
                //speed_hod = true;
                //bl_dop = false;
            }

        }
        else
        {
            bl = true;
            /*
            if (cart.name == "cart act rock")
            {
                func.create_effect("cart rock", x, y, gameObject);
            }
            else if (cart.name == "cart act blood")
            {
                func.create_effect("cart blood", x, y, gameObject);
            }
            else if (cart.name == "cart act fire")
            {
                func.create_effect("cart fire", x, y, gameObject);
            }
            else if (cart.name == "cart act energy")
            {
                func.create_effect("cart energy", x, y, gameObject);
            }
            else if (cart.name == "cart act rock 1")
            {
                func.create_effect("cart rock 1", x, y, gameObject);
            }
            */


            if (cart.name == "cart cuzna")
            {
                bl_dop = false;

            }
            if (cart.name == "cart dungalorg heard")
            {
                if (st_par != null && st_par.Peek() == "par spawn damage")
                {
                    spawn_cart("cart dungalorg(sleep)", lt_art[a]);
                    bl_dop = false;
                }

            }
            else if (cart.name == "cart dungalorg(boss)")
            {
                spawn_cart("cart derlorg", lt_art[a]);
                bl_dop = false;

            }



            else if (cart.name == "cart potoc rock")
            {
                if (st_par != null && st_par.Count > 0 && st_par.Peek() == "par spawn rock")
                {
                    st_par.Pop();
                    List<string> lt_cart_rock = (List<string>)(st_par.Pop());

                    foreach (string st_cart in lt_cart_rock)
                    {
                        int lvl = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                        if (lvl == 1)
                            spawn_cart("cart rock", lt_art[a]);
                        else if (lvl == 2)
                            spawn_cart("cart rock 1", lt_art[a]);
                        else if (lvl == 4)
                            spawn_cart("cart rock 2", lt_art[a]);
                    }
                }

                bl_dop = false;

            }
            else if (cart.name == "cart potoc blood")
            {
                if (st_par != null && st_par.Count > 0 && st_par.Peek() == "par spawn blood")
                {
                    st_par.Pop();
                    List<string> lt_cart_rock = (List<string>)(st_par.Pop());

                    foreach (string st_cart in lt_cart_rock)
                    {
                        int lvl = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                        if (lvl == 1)
                            spawn_cart("cart blood", lt_art[a]);
                        else if (lvl == 2)
                            spawn_cart("cart blood 1", lt_art[a]);
                        else if (lvl == 4)
                            spawn_cart("cart blood 2", lt_art[a]);
                    }
                }

                bl_dop = false;

            }
            else if (cart.name == "cart potoc fire")
            {
                if (st_par != null && st_par.Count > 0 && st_par.Peek() == "par spawn fire")
                {
                    st_par.Pop();
                    List<string> lt_cart_rock = (List<string>)(st_par.Pop());

                    foreach (string st_cart in lt_cart_rock)
                    {
                        int lvl = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                        if (lvl == 1)
                            spawn_cart("cart fire", lt_art[a]);
                        else if (lvl == 2)
                            spawn_cart("cart fire 1", lt_art[a]);
                        else if (lvl == 4)
                            spawn_cart("cart fire 2", lt_art[a]);
                    }
                }

                bl_dop = false;

            }
            else if (cart.name == "cart potoc energy")
            {
                if (st_par != null && st_par.Count > 0 && st_par.Peek() == "par spawn energy")
                {
                    st_par.Pop();
                    List<string> lt_cart_rock = (List<string>)(st_par.Pop());

                    foreach (string st_cart in lt_cart_rock)
                    {
                        int lvl = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                        if (lvl == 1)
                            spawn_cart("cart energy", lt_art[a]);
                        else if (lvl == 2)
                            spawn_cart("cart energy 1", lt_art[a]);
                        else if (lvl == 4)
                            spawn_cart("cart energy 2", lt_art[a]);

                    }
                }

                bl_dop = false;

            }
            else if (cart.name == "cart potoc ice")
            {
                if (st_par != null && st_par.Count > 0 && st_par.Peek() == "par spawn ice")
                {
                    st_par.Pop();
                    List<string> lt_cart_rock = (List<string>)(st_par.Pop());

                    foreach (string st_cart in lt_cart_rock)
                    {
                        int lvl = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                        if (lvl == 1)
                            spawn_cart("cart ice", lt_art[a]);
                        else if (lvl == 2)
                            spawn_cart("cart ice 1", lt_art[a]);
                        else if (lvl == 4)
                            spawn_cart("cart ice 2", lt_art[a]);
                    }
                }

                bl_dop = false;

            }
            else if (cart.name == "cart potoc poition")
            {
                if (st_par != null && st_par.Count > 0 && st_par.Peek() == "par spawn poition")
                {
                    st_par.Pop();
                    List<string> lt_cart_rock = (List<string>)(st_par.Pop());

                    foreach (string st_cart in lt_cart_rock)
                    {
                        int lvl = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                        if (lvl == 1)
                            spawn_cart("cart poition", lt_art[a]);
                        else if (lvl == 2)
                            spawn_cart("cart poition 1", lt_art[a]);
                        else if (lvl == 4)
                            spawn_cart("cart poition 2", lt_art[a]);
                    }
                }

                bl_dop = false;

            }
            else if (cart.name == "cart derlorg potoc")
            {
                if (st_par != null && st_par.Count > 0 && st_par.Peek() == "par spawn derlorg")
                {
                    st_par.Pop();
                    List<string> lt_cart_rock = (List<string>)(st_par.Pop());

                    foreach (string st_cart in lt_cart_rock)
                    {
                        int lvl = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                        spawn_cart("cart derlorg", lt_art[a]);
                    }
                }

                bl_dop = false;

            }







            /*
            else if (cart.name == "cart potoc rock")
            {
                st_par.Pop();
                int pow = (int)(st_par.Pop());

                List<string> lt_cart1 = new List<string>(lt_carts_active_now);
                lt_cart1.Add("cart rock");
                func.create_effect(func.get_name_effect(lt_cart1, null), x, y, gameObject, false, pow, lt_cart1);
                //regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart potoc blood")
            {
                st_par.Pop();
                int pow = (int)(st_par.Pop());

                List<string> lt_cart1 = new List<string>(lt_carts_active_now);
                lt_cart1.Add("cart blood");
                func.create_effect(func.get_name_effect(lt_cart1, null), x, y, gameObject, false, pow, lt_cart1);
                //regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart potoc fire")
            {
                st_par.Pop();
                int pow = (int)(st_par.Pop());

                List<string> lt_cart1 = new List<string>(lt_carts_active_now);
                lt_cart1.Add("cart fire");
                func.create_effect(func.get_name_effect(lt_cart1, null), x, y, gameObject, false, pow, lt_cart1);
                //regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart potoc energy")
            {
                st_par.Pop();
                int pow = (int)(st_par.Pop());

                List<string> lt_cart1 = new List<string>(lt_carts_active_now);
                lt_cart1.Add("cart energy");
                func.create_effect(func.get_name_effect(lt_cart1, null), x, y, gameObject, false, pow, lt_cart1);
                //regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart potoc ice")
            {
                st_par.Pop();
                int pow = (int)(st_par.Pop());

                List<string> lt_cart1 = new List<string>(lt_carts_active_now);
                lt_cart1.Add("cart ice");
                func.create_effect(func.get_name_effect(lt_cart1, null), x, y, gameObject, false, pow, lt_cart1);
                //regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart potoc poition")
            {
                st_par.Pop();
                int pow = (int)(st_par.Pop());

                List<string> lt_cart1 = new List<string>(lt_carts_active_now);
                lt_cart1.Add("cart poition");
                func.create_effect(func.get_name_effect(lt_cart1, null), x, y, gameObject, false, pow, lt_cart1);
                //regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            */

            /*
            else if (cart.name == "cart hp rock")
            {
                regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart hp blood")
            {
                regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart hp fire")
            {
                regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart hp energy")
            {
                regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart hp ice")
            {
                regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            else if (cart.name == "cart hp poition")
            {
                regen(1, null);
                //func.create_effect("cart rock 1", x, y, gameObject);
            }
            */
            /*
            else if (cart.name == "cart berserk boss" && st_par != null && (string)st_par.Peek() == "par_cart berserk boss")
            {
                st_par.Pop();
                int pow = (int)(st_par.Pop());
                int pow1 = 0;
                string type_dop_pow = null;
                if (st_par.Count > 0 && (string)st_par.Peek() == "par pow berserk boss")
                {
                    st_par.Pop();
                    pow1 = (int)(st_par.Pop());
                    type_dop_pow = "par pow";
                }

                for (int i = y - 2; i <= y + 2; i++)
                {
                    if (i > 0 && i < func.size_global - 1)
                    {
                        for (int j = x - 2; j <= x + 2; j++)
                        {
                            int c = (int)Mathf.Pow((i - y) * (i - y), 0.5f) + (int)Mathf.Pow((j - x) * (j - x), 0.5f);
                            Debug.Log(effect_now.GetComponent<par_effect>().name_in_lt);
                            if (j > 0 && j < func.size_global - 1 && c <= 2 && c > 0)
                            {
                                GameObject ef = func.create_effect(effect_now.GetComponent<par_effect>().name_in_lt, j, i, gameObject, false, pow * (2 / c), lt_carts_active_now);
                                lt_effect_now.Add(ef);
                                if (type_dop_pow != null)
                                {
                                    ef.GetComponent<par_effect>().st_par.Push((pow1).ToString());
                                    ef.GetComponent<par_effect>().st_par.Push(type_dop_pow);
                                }
                            }
                            //func.create_effect(effect_activate, x, y, gameObject);


                        }
                    }
                }

            }
            */
            if (st_par != null && st_par.Count > 0 && (string)st_par.Peek() == "par -q")
            {
                st_par.Pop();
                int q = (int)(st_par.Pop());
                cart.q -= q;
            }



            //add_baf()
            if (lt_art[a] != null && bl_dop)
                func.create_effect("activate art", null, lt_art[a].transform.position, true);
        }

        Debug.Log(cart.name + " "+bl+" "+bl_dop+" "+ bl_passive);
        if (bl && bl_dop)
        {
            //Debug.Log("q and " + cart.type_q_now);
            if (cart.type_q_now == par_cart_art.type_q.stop_more_1)
            {
                cart.q--;
                if (cart.q == 0)
                    cart.perez_now = cart.perez;
            }
            else
            {
                cart.perez_now = cart.perez;
            }

        }

        return bl;
    }
    public par_cart_art add_cart_art(string cart)
    {
        //par_mob pr_mob = func.mob_list[num].GetComponent<par_mob>();
        Dictionary<string, int> lt_cart_local = new Dictionary<string, int>();
        foreach (string st in lt_carts)
        {
            if (!lt_cart_local.ContainsKey(st))
                lt_cart_local.Add(st, 1);
            else
                lt_cart_local[st]++;
        }


        par_cart_art art = gameObject.AddComponent<par_cart_art>();
        par_cart_art art1 = func.baza[cart].GetComponent<par_cart_art>();
        art.type = art1.type;
        art.perez = art1.perez;
        art.perez_now = art1.perez;
        art.tp_perez = art1.tp_perez;
        art.q = art1.q;
        art.q_max = art1.q_max;
        art.q_sum = art1.q_sum;
        art.type_q_now = art1.type_q_now;
        art.name = cart;
        //art.name_real=
        art.lvl = art1.lvl;
        art.infa = art1.infa;

        string[] a_ = new string[3];


        a_[0] = "";
        a_[1] = " 1";
        a_[2] = " 2";

        if (!data.data_now.cart_collection.Contains(cart) && gameObject.name == data.data_now.pl_name_now)
        {
            data.data_now.cart_collection.Add(cart);
            bool bl = true;
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                if (!data.data_now.cart_collection.Contains(tr.gameObject.name))
                    bl = false;
            }
            if (bl)
                data.give_achive("коллекционер");
        }


        lt_carts_art.Add(art);
        //if (lt_carts_art.Count > 1)
        //Debug.Log("start equals " + (art == lt_carts_art[0])+" "+ lt_carts_art.Count);
        //Debug.Log("art is null " + art.name);
        if(num==0)
            ui.print_carts_art(num);

        ad_cart_art_active(cart);

        string aa = "";
        if (art.lvl > 1)
            aa = " " + (art.lvl - 1).ToString();
        if (art.lvl >= 2)
        {
            if (art1.type == par_cart.type_cart.art_rock_mast)
            {
                par_lt_sila_base.Add(par_cart.type_cart.rock.ToString());
                par_lt_sila_base.Add(par_cart.type_cart.spirit_rock.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.rock.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.spirit_rock.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_blood_mast)
            {
                par_lt_sila_base.Add(par_cart.type_cart.blood.ToString());
                par_lt_sila_base.Add(par_cart.type_cart.spirit_blood.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.blood.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.spirit_blood.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_energy_mast)
            {
                par_lt_sila_base.Add(par_cart.type_cart.energy.ToString());
                par_lt_sila_base.Add(par_cart.type_cart.spirit_energy.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.energy.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.spirit_energy.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_ice_mast)
            {
                par_lt_sila_base.Add(par_cart.type_cart.ice.ToString());
                par_lt_sila_base.Add(par_cart.type_cart.spirit_ice.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.ice.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.spirit_ice.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_fire_mast)
            {
                par_lt_sila_base.Add(par_cart.type_cart.fire.ToString());
                par_lt_sila_base.Add(par_cart.type_cart.spirit_fire.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.fire.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.spirit_fire.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_poition_mast)
            {
                par_lt_sila_base.Add(par_cart.type_cart.poition.ToString());
                par_lt_sila_base.Add(par_cart.type_cart.spirit_poition.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.poition.ToString());
                par_lt_speed_base.Add(par_cart.type_cart.spirit_poition.ToString());
            }
        }

        //Debug.Log(((par_cart)art1).lvl + " " + art1.type);
        if (art1.type == par_cart.type_cart.art_rock_sp || art1.type == par_cart.type_cart.art_rock_act || art1.type == par_cart.type_cart.art_speed_rock || art1.type == par_cart.type_cart.art_pow_rock || art1.type == par_cart.type_cart.art_rock_bomb || art1.type == par_cart.type_cart.art_rock_cuzna)
        {
            //Debug.Log("ttttttttttt"+aa);
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart rock" + a_[i]] += 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit rock"] += 2;
            //carts_count_max["cart rock" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_rock_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.rock.ToString());
                    par_lt_sila_base.Add(par_cart.type_cart.spirit_rock.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_rock)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.rock.ToString());
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_rock.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_blood_sp || art1.type == par_cart.type_cart.art_blood_act || art1.type == par_cart.type_cart.art_speed_blood || art1.type == par_cart.type_cart.art_pow_blood || art1.type == par_cart.type_cart.art_blood_bomb || art1.type == par_cart.type_cart.art_blood_cuzna)
        {
            //carts_count_max["cart blood" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart blood" + a_[i]] += 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit blood"] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_blood_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.blood.ToString());
                    par_lt_sila_base.Add(par_cart.type_cart.spirit_blood.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_blood)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.blood.ToString());
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_blood.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_fire_sp || art1.type == par_cart.type_cart.art_fire_act || art1.type == par_cart.type_cart.art_speed_fire || art1.type == par_cart.type_cart.art_pow_fire || art1.type == par_cart.type_cart.art_fire_bomb || art1.type == par_cart.type_cart.art_fire_cuzna)
        {
            //carts_count_max["cart fire" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart fire" + a_[i]] += 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit fire"] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_fire_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.fire.ToString());
                    par_lt_sila_base.Add(par_cart.type_cart.spirit_fire.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_fire)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.fire.ToString());
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_fire.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_energy_sp || art1.type == par_cart.type_cart.art_energy_act || art1.type == par_cart.type_cart.art_speed_energy || art1.type == par_cart.type_cart.art_pow_energy || art1.type == par_cart.type_cart.art_energy_bomb || art1.type == par_cart.type_cart.art_energy_cuzna)
        {
            //carts_count_max["cart energy" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart energy" + a_[i]] += 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit energy"] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_energy_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.energy.ToString());
                    par_lt_sila_base.Add(par_cart.type_cart.spirit_energy.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_energy)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.energy.ToString());
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_energy.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_ice_sp || art1.type == par_cart.type_cart.art_ice_act || art1.type == par_cart.type_cart.art_speed_ice || art1.type == par_cart.type_cart.art_pow_ice || art1.type == par_cart.type_cart.art_ice_bomb || art1.type == par_cart.type_cart.art_ice_cuzna)
        {
            //carts_count_max["cart ice" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart ice" + a_[i]] += 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit ice"] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_ice_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.ice.ToString());
                    par_lt_sila_base.Add(par_cart.type_cart.spirit_ice.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_ice)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.ice.ToString());
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_ice.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_poition_sp || art1.type == par_cart.type_cart.art_poition_act || art1.type == par_cart.type_cart.art_speed_poition || art1.type == par_cart.type_cart.art_pow_poition || art1.type == par_cart.type_cart.art_poition_bomb || art1.type == par_cart.type_cart.art_poition_cuzna)
        {
            //carts_count_max["cart poition" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart poition" + a_[i]] += 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit poition"] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_poition_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.poition.ToString());
                    par_lt_sila_base.Add(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_poition)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.poition.ToString());
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }



        else if (art1.type == par_cart.type_cart.art_att_sp || art1.type == par_cart.type_cart.art_att_act || art1.type == par_cart.type_cart.art_speed_att || art1.type == par_cart.type_cart.art_att_pow || art1.type == par_cart.type_cart.art_att_bomb || art1.type == par_cart.type_cart.art_att_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart damage" + a_[i]] += 2;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_att_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.damage.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_att)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.damage.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }

        else if (art1.type == par_cart.type_cart.art_bar_sp || art1.type == par_cart.type_cart.art_bar_act || art1.type == par_cart.type_cart.art_speed_bar || art1.type == par_cart.type_cart.art_bar_pow || art1.type == par_cart.type_cart.art_bar_bomb || art1.type == par_cart.type_cart.art_bar_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart barier" + a_[i]]++;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_bar_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.sp_barier.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_bar)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.sp_barier.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }

        else if (art1.type == par_cart.type_cart.art_dych_sp || art1.type == par_cart.type_cart.art_dych_act || art1.type == par_cart.type_cart.art_speed_dych || art1.type == par_cart.type_cart.art_dych_pow || art1.type == par_cart.type_cart.art_dych_bomb || art1.type == par_cart.type_cart.art_dych_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart dych" + a_[i]]++;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_dych_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.sp_dych.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_dych)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.sp_dych.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }

        else if (art1.type == par_cart.type_cart.art_bomb_sp || art1.type == par_cart.type_cart.art_bomb_act || art1.type == par_cart.type_cart.art_speed_bomb || art1.type == par_cart.type_cart.art_pow_bomb || art1.type == par_cart.type_cart.art_bomb_bomb || art1.type == par_cart.type_cart.art_bomb_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart bomb" + a_[i]]++;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_bomb_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.sp_bomb.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_bomb)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.sp_bomb.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }
        /*
        else if (art1.type == par_cart.type_cart.art_dych_sp)
        {
            //carts_count_max["cart crystall" + aa]++;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart dych" + a_[i]] += 2;
            }
        }
        else if (art1.type == par_cart.type_cart.art_bomb_sp)
        {
            //carts_count_max["cart crystall" + aa]++;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart crystall" + a_[i]] += 2;
            }
        }

        else if (art1.type == par_cart.type_cart.art_att_sp)
        {
            //carts_count_max["cart damage" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart damage" + a_[i]] += 2;
            }
        }
        else if (art1.type == par_cart.type_cart.art_bar_sp)
        {
            //carts_count_max["cart barier" + aa]++;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart barier" + a_[i]] += 2;
            }
        }

        */
        else if (art1.type == par_cart.type_cart.art_base_st)
        {
            carts_count_max["cart damage"] += 4;
        }

        else if (art1.type == par_cart.type_cart.art_att_st)
        {
            carts_count_max["cart damage"] += 4;
        }
        else if (art1.type == par_cart.type_cart.art_bar_st)
        {
            carts_count_max["cart barier"] += 2;
        }

        else if (art1.type == par_cart.type_cart.art_fire_st)
        {
            carts_count_max["cart fire"] += 4;
        }
        else if (art1.type == par_cart.type_cart.art_rock_st)
        {
            carts_count_max["cart rock"] += 4;
            //carts_count_max["cart rock 1"] += 3;
        }
        else if (art1.type == par_cart.type_cart.art_blood_st)
        {
            carts_count_max["cart blood"] += 4;
        }
        else if (art1.type == par_cart.type_cart.art_ice_st)
        {
            carts_count_max["cart ice"] += 4;
        }
        else if (art1.type == par_cart.type_cart.art_energy_st)
        {
            carts_count_max["cart energy"] += 4;
        }
        else if (art1.type == par_cart.type_cart.art_poition_st)
        {
            carts_count_max["cart poition"] += 4;
        }
        else if (art1.type == par_cart.type_cart.art_derlorg_st)
        {
            carts_count_max["cart derlorg"] += 4;
            //carts_count_max["cart poition 1" + aa] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_derlorg_sp)
        {
            carts_count_max["cart derlorg"] += 2;
        }

        else if (cart == "cart dop hill" || cart == "cart spirit fire heart" || cart == "cart spirit poition heart" || cart == "cart spirit energy heart" || cart == "cart spirit ice heart" || cart == "cart spirit rock heart" || cart == "cart spirit blood heart")
        {
            hp_max += 10;
        }
        else if (cart == "cart berserk boss")
        {
            hp_max += 20;
        }
        else if (cart == "cart berserk")
        {
            //hp_max += 5;
        }
        /*
        else if (cart == "cart hp blood" || cart == "cart hp rock" || cart == "cart hp fire" || cart == "cart hp energy" || cart == "cart hp ice" || cart == "cart hp poition")
        {
            hp_max+=2;
        }
        */
        else if (cart == "cart inven")
        {
            col_art += 2;

            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (func.baza[nm].GetComponent<par_cart>().spetial || type == par_cart.type_cart.sp_cuznec || nm == "cart dungalorg(sleep) 1" || nm == "cart dungalorg 1")
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 1;
                    }
                    else
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 2;
                    }
                }
            }

        }
        else if (cart == "cart speed")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_speed_base.Add(tp.ToString());
            }

        }
        else if (cart == "cart sila")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_sila_base.Add(tp.ToString());
            }

        }

        else if (cart == "cart derlorg act")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_sila_base.Add(tp.ToString());
            }

        }
        else if (cart == "cart derlorg speed")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_speed_base.Add(tp.ToString());
            }
        }
        if (cart == "cart sp derlorg" || cart == "cart derlorg act" || cart == "cart derlorg speed" || cart == "cart derlorg pow")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (func.baza[nm].GetComponent<par_cart>().spetial)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 1;
                    }
                    else
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 2;
                    }
                }
            }
        }
        if(cart == "cart derlorg att")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type==par_cart.type_cart.damage)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg bar")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.sp_barier)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 2;
                    }
                }
            }
        }
        if (cart == "cart derlorg bomb")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.sp_bomb)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 2;
                    }
                }
            }
        }
        if (cart == "cart derlorg fire")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.fire || type == par_cart.type_cart.spirit_fire)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg poition")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.poition || type == par_cart.type_cart.spirit_poition)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg ice")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.ice || type == par_cart.type_cart.spirit_ice)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg energy")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.energy || type == par_cart.type_cart.spirit_energy)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg rock")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.rock || type == par_cart.type_cart.spirit_rock)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg blood")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.blood || type == par_cart.type_cart.spirit_blood)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 4;
                    }
                }
            }
        }





        return art;
    }
    public void remove_cart_art(string cart)
    {
        int sz = lt_carts_art.Count;
        int cart_num = 0;
        for (int i = 0; i < sz; i++)
        {
            par_cart_art art = lt_carts_art[i];
            if (art.name == cart)
            {
                cart_num = i;
                Destroy(art);
                lt_carts_art.RemoveAt(i);
                break;
            }
        }
        add_cart(cart);
        Destroy(lt_carts_art_gm[cart_num]);
        lt_carts_art_gm.RemoveAt(cart_num);

        ad_cart_art_active(null);
        //rem_cart_art_active();
        if (friend)
        {
            ui.print_carts_art(num);
            ui.print_carts_tip(num);
        }


        par_cart_art art1 = GameObject.Find(cart).GetComponent<par_cart_art>();
        string aa = "";
        if (((par_cart)art1).lvl > 1)
            aa = " " + (((par_cart)art1).lvl - 1).ToString();
        string[] a_ = new string[3];

        a_[0] = "";
        a_[1] = " 1";
        a_[2] = " 2";

        if (((par_cart)art1).lvl >=2)
        {
            if (art1.type == par_cart.type_cart.art_rock_mast)
            {
                par_lt_sila_base.Remove(par_cart.type_cart.rock.ToString());
                par_lt_sila_base.Remove(par_cart.type_cart.spirit_rock.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.rock.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.spirit_rock.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_blood_mast)
            {
                par_lt_sila_base.Remove(par_cart.type_cart.blood.ToString());
                par_lt_sila_base.Remove(par_cart.type_cart.spirit_blood.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.blood.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.spirit_blood.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_energy_mast)
            {
                par_lt_sila_base.Remove(par_cart.type_cart.energy.ToString());
                par_lt_sila_base.Remove(par_cart.type_cart.spirit_energy.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.energy.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.spirit_energy.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_ice_mast)
            {
                par_lt_sila_base.Remove(par_cart.type_cart.ice.ToString());
                par_lt_sila_base.Remove(par_cart.type_cart.spirit_ice.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.ice.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.spirit_ice.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_fire_mast)
            {
                par_lt_sila_base.Remove(par_cart.type_cart.fire.ToString());
                par_lt_sila_base.Remove(par_cart.type_cart.spirit_fire.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.fire.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.spirit_fire.ToString());
            }
            else if (art1.type == par_cart.type_cart.art_poition_mast)
            {
                par_lt_sila_base.Remove(par_cart.type_cart.poition.ToString());
                par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.poition.ToString());
                par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
            }
        }

        if (art1.type == par_cart.type_cart.art_rock_sp || art1.type == par_cart.type_cart.art_rock_act || art1.type == par_cart.type_cart.art_speed_rock || art1.type == par_cart.type_cart.art_pow_rock || art1.type == par_cart.type_cart.art_rock_bomb || art1.type == par_cart.type_cart.art_rock_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart rock" + a_[i]] -= 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit rock"] -= 2;
            //carts_count_max["cart rock" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_rock_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.rock.ToString());
                    par_lt_sila_base.Remove(par_cart.type_cart.spirit_rock.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_rock)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.rock.ToString());
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_rock.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_blood_sp || art1.type == par_cart.type_cart.art_blood_act || art1.type == par_cart.type_cart.art_speed_blood || art1.type == par_cart.type_cart.art_pow_blood || art1.type == par_cart.type_cart.art_blood_bomb || art1.type == par_cart.type_cart.art_blood_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart blood" + a_[i]] -= 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit blood"] -= 2;
            //carts_count_max["cart blood" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_blood_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.blood.ToString());
                    par_lt_sila_base.Remove(par_cart.type_cart.spirit_blood.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_blood)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.blood.ToString());
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_blood.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_fire_sp || art1.type == par_cart.type_cart.art_fire_act || art1.type == par_cart.type_cart.art_speed_fire || art1.type == par_cart.type_cart.art_pow_fire || art1.type == par_cart.type_cart.art_fire_bomb || art1.type == par_cart.type_cart.art_fire_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart fire" + a_[i]] -= 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit fire"] -= 2;
            //carts_count_max["cart fire" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_fire_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.fire.ToString());
                    par_lt_sila_base.Remove(par_cart.type_cart.spirit_fire.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_fire)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.fire.ToString());
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_fire.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_energy_sp || art1.type == par_cart.type_cart.art_energy_act || art1.type == par_cart.type_cart.art_speed_energy || art1.type == par_cart.type_cart.art_pow_energy || art1.type == par_cart.type_cart.art_energy_bomb || art1.type == par_cart.type_cart.art_energy_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart energy" + a_[i]] -= 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit energy"] -= 2;
            //carts_count_max["cart energy" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_energy_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.energy.ToString());
                    par_lt_sila_base.Remove(par_cart.type_cart.spirit_energy.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_energy)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.energy.ToString());
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_energy.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_ice_sp || art1.type == par_cart.type_cart.art_ice_act || art1.type == par_cart.type_cart.art_speed_ice || art1.type == par_cart.type_cart.art_pow_ice || art1.type == par_cart.type_cart.art_ice_bomb || art1.type == par_cart.type_cart.art_ice_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart ice" + a_[i]] -= 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit ice"] -= 2;
            //carts_count_max["cart ice" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_ice_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.ice.ToString());
                    par_lt_sila_base.Remove(par_cart.type_cart.spirit_ice.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_ice)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.ice.ToString());
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_ice.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_poition_sp || art1.type == par_cart.type_cart.art_poition_act || art1.type == par_cart.type_cart.art_speed_poition || art1.type == par_cart.type_cart.art_pow_poition || art1.type == par_cart.type_cart.art_poition_bomb || art1.type == par_cart.type_cart.art_poition_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart poition" + a_[i]] -= 2;
            }
            if (((par_cart)art1).lvl == 4)
                carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_poition_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.poition.ToString());
                    par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_poition)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.poition.ToString());
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }

        else if (art1.type == par_cart.type_cart.art_att_sp || art1.type == par_cart.type_cart.art_att_act || art1.type == par_cart.type_cart.art_speed_att || art1.type == par_cart.type_cart.art_att_pow || art1.type == par_cart.type_cart.art_att_bomb || art1.type == par_cart.type_cart.art_att_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart damage" + a_[i]] -= 2;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_att_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.damage.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_att)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.damage.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }

        else if (art1.type == par_cart.type_cart.art_bar_sp || art1.type == par_cart.type_cart.art_bar_act || art1.type == par_cart.type_cart.art_speed_bar || art1.type == par_cart.type_cart.art_bar_pow || art1.type == par_cart.type_cart.art_bar_bomb || art1.type == par_cart.type_cart.art_bar_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart barier" + a_[i]] --;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_bar_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.sp_barier.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_bar)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.sp_barier.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }

        else if (art1.type == par_cart.type_cart.art_dych_sp || art1.type == par_cart.type_cart.art_dych_act || art1.type == par_cart.type_cart.art_speed_dych || art1.type == par_cart.type_cart.art_dych_pow || art1.type == par_cart.type_cart.art_dych_bomb || art1.type == par_cart.type_cart.art_dych_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart dych" + a_[i]] --;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_dych_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.sp_dych.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_dych)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.sp_dych.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }

        else if (art1.type == par_cart.type_cart.art_bomb_sp || art1.type == par_cart.type_cart.art_bomb_act || art1.type == par_cart.type_cart.art_speed_bomb || art1.type == par_cart.type_cart.art_pow_bomb || art1.type == par_cart.type_cart.art_bomb_bomb || art1.type == par_cart.type_cart.art_bomb_cuzna)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart bomb" + a_[i]]--;
            }
            //if (((par_cart)art1).lvl == 4)
            //carts_count_max["cart spirit poition"] -= 2;
            //carts_count_max["cart poition" + aa] += 2;
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_bomb_act)
                {
                    par_lt_sila_base.Remove(par_cart.type_cart.sp_bomb.ToString());
                    //par_lt_sila_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_bomb)
                {
                    par_lt_speed_base.Remove(par_cart.type_cart.sp_bomb.ToString());
                    //par_lt_speed_base.Remove(par_cart.type_cart.spirit_poition.ToString());
                }
            }
        }


        else if (art1.type == par_cart.type_cart.art_base_st)
        {
            carts_count_max["cart damage"] -= 4;
        }

        else if (art1.type == par_cart.type_cart.art_att_st)
        {
            carts_count_max["cart damage"] -= 4;
        }
        else if (art1.type == par_cart.type_cart.art_bar_st)
        {
            carts_count_max["cart barier"] -= 2;
        }

        else if (art1.type == par_cart.type_cart.art_fire_st)
        {
            carts_count_max["cart fire"] -= 4;
            //carts_count_max["cart fire 1" + aa] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_rock_st)
        {
            carts_count_max["cart rock"] -= 4;
            //carts_count_max["cart rock 1"] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_blood_st)
        {
            carts_count_max["cart blood"] -= 4;
            //carts_count_max["cart blood 1" + aa] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_ice_st)
        {
            carts_count_max["cart ice"] -= 4;
            //carts_count_max["cart ice 1" + aa] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_energy_st)
        {
            carts_count_max["cart energy"] -= 4;
            //carts_count_max["cart energy 1" + aa] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_poition_st)
        {
            carts_count_max["cart poition"] -= 4;
            //carts_count_max["cart poition 1" + aa] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_derlorg_st)
        {
            carts_count_max["cart derlorg"] -= 4;
            //carts_count_max["cart poition 1" + aa] -= 3;
        }
        else if (art1.type == par_cart.type_cart.art_derlorg_sp)
        {
            carts_count_max["cart derlorg"] -= 2;
            //hp_max += 2;
        }


        else if (cart == "cart dop hill" || cart == "cart spirit fire heart" || cart == "cart spirit poition heart" || cart == "cart spirit energy heart" || cart == "cart spirit ice heart" || cart == "cart spirit rock heart" || cart == "cart spirit blood heart")
        {
            hp_max -= 10;
            if (hp > hp_max)
                hp = hp_max;
        }
        else if (cart == "cart berserk boss")
        {
            hp_max -= 20;
            if (hp > hp_max)
                hp = hp_max;
        }
        else if (cart == "cart berserk")
        {
            //hp_max -= 5;
            //if (hp > hp_max)
            //hp = hp_max;
        }
        /*
        else if (cart == "cart hp blood" || cart == "cart hp rock" || cart == "cart hp fire" || cart == "cart hp energy" || cart == "cart hp ice" || cart == "cart hp poition")
        {
            hp_max -=2;
            if (hp > hp_max)
                hp = hp_max;
        }
        */
        else if (cart == "cart inven")
        {
            col_art -= 2;

            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(tr.gameObject.name))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (func.baza[nm].GetComponent<par_cart>().spetial || type == par_cart.type_cart.sp_cuznec || nm == "cart dungalorg(sleep) 1" || nm == "cart dungalorg 1")
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 1;
                    }
                    else
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] - 2;
                    }
                }
            }

        }
        else if (cart == "cart speed")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_speed_base.Remove(tp.ToString());
            }

        }
        else if (cart == "cart sila")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_sila_base.Remove(tp.ToString());
            }

        }



        else if (cart == "cart derlorg act")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_sila_base.Remove(tp.ToString());
            }

        }
        else if (cart == "cart derlorg speed")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_speed_base.Remove(tp.ToString());
            }
        }
        if (cart == "cart sp derlorg" || cart == "cart derlorg act" || cart == "cart derlorg speed" || cart == "cart derlorg pow")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (func.baza[nm].GetComponent<par_cart>().spetial)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 1;
                    }
                    else
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 2;
                    }
                }
            }
        }
        if (cart == "cart derlorg att")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.damage)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg bar")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.sp_barier)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 2;
                    }
                }
            }
        }
        if (cart == "cart derlorg bomb")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.sp_bomb)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 2;
                    }
                }
            }
        }
        if (cart == "cart derlorg fire")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.fire || type == par_cart.type_cart.spirit_fire)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg poition")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.poition || type == par_cart.type_cart.spirit_poition)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg ice")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.ice || type == par_cart.type_cart.spirit_ice)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg energy")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.energy || type == par_cart.type_cart.spirit_energy)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg rock")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.rock || type == par_cart.type_cart.spirit_rock)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 4;
                    }
                }
            }
        }
        if (cart == "cart derlorg blood")
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(nm))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.blood || type == par_cart.type_cart.spirit_blood)
                    {
                        carts_count_max[tr.gameObject.name] = carts_count_max[tr.gameObject.name] + 4;
                    }
                }
            }
        }
        //Debug.Log("art is null " + (lt_carts_art[lt_carts_art.Count - 1] is null));
        //ad_cart_art_active(cart);
    }
    public bool spawn_cart(string cart, GameObject obj)
    {
        int col = 0;
        foreach (string st in lt_carts)
        {
            if (st == cart)
                col++;
        }

        if (!carts_count_max.ContainsKey(cart) || col < carts_count_max[cart])
        {
            if (obj != null)
            {
                //anim.col_anim++;
                func.create_effect("spawn cart", cart, obj.transform.position, true).GetComponent<par_effect>().parent = gameObject;
            }

            add_cart(cart);
        }
        if (!carts_count_max.ContainsKey(cart))
            return true;
        else
            return (col < carts_count_max[cart]);



    }
    public void add_cart(par_cart cart)
    {
        int minus = 0;
        for (int i = 0; i < cart.col; i++)
        {
            bool bl = add_cart(cart.name);
            if (bl)
                minus++;
            else
                break;
        }
        cart.col -= minus;

    }
    public bool add_cart(string name)
    {
        int bl_d = -1;
        if (name == "cart dungalorg")
            foreach (par_cart_art art in lt_carts_art)
            {

                //{
                if (art.name == "cart dungalorg(boss)")
                {
                    if (bl_d == -1)
                        name = "cart dungalorg(sleep)";
                    //else
                    bl_d++;
                }

                //}


            }
        if (bl_d > 0)
        {
            for (int i = 0; i < bl_d; i++)
            {
                add_cart("cart dungalorg(sleep)");
            }
        }
        int col = 0;
        foreach (string st in lt_carts)
        {
            if (st == name)
                col++;
        }
        if (name == "cart gold" || GameObject.Find(name).GetComponent<par_cart_art>() != null || col < carts_count_max[name])
        {
            lt_carts.Add(name);
            if (!data.data_now.cart_collection.Contains(name) && gameObject.name == data.data_now.pl_name_now)
            {
                data.data_now.cart_collection.Add(name);
                bool bl = true;
                foreach (Transform tr in GameObject.Find("list carts").transform)
                {
                    if (!data.data_now.cart_collection.Contains(tr.gameObject.name))
                        bl = false;
                }
                if (bl)
                    data.give_achive("коллекционер");
            }

            data.data_now.col_carts_now = lt_carts.Count;
            if (data.data_now.col_carts_now >= 75 && !data.data_now.lt_achives_open.Contains("большая колода"))
                data.give_achive("большая колода");
            if (num==0 && friend_type==type_friend.active&& friend)
            {
                ui.print_carts_tip(num);
            }
            return true;
        }
        else
        {
            return false;
        }


    }
    public void remove_cart(par_cart cart)
    {
        for (int i = 0; i < cart.col; i++)
        {
            remove_cart(cart.name);
        }
    }
    public void remove_cart(string name)
    {
        lt_carts.Remove(name);
        if (func.hod_mob_now == num && friend_type==type_friend.active && friend)
        {
            ui.print_carts_tip(num);
        }
    }
    public void ad_cart_active()
    {
        //string stt = null;
        //ad_cart_active(cart_active_now,lt_carts_active_now);
        if (friend && friend_type == type_friend.active && !atack_auto_comb)
            ad_cart_active(cart_active_now, lt_carts_active_now_ui);
        else
            ad_cart_active(cart_active_now, lt_carts_active_now);
    }

    public void ad_cart_active(string name, List<string> lt_carts_ad)
    {
        //func.test_message = "error ad cart 895";
        par_cart.type_cart tp_now = par_cart.type_cart.bases;
        GameObject gm = Instantiate(func.baza[name], gameObject.transform.Find("carts active"));
        tp_now = gm.GetComponent<par_cart>().type;
        //if (gm.GetComponent<par_cart>().sp_cart_bl() || gm.GetComponent<par_cart>().hard_cart_bl())
        //gm.name = "cart sp";
        //else
        gm.name = "cart";
        gm.GetComponent<SpriteRenderer>().sortingOrder = 3;
        Destroy(gm.GetComponent<par_cart>());
        int i = 0;
        int x_ = 0;
        int[] y_ = new int[lt_carts_ad.Count];

        par_cart.type_cart tp_now_ = par_cart.type_cart.bases;
        int col = 0;
        foreach (string str in lt_carts_ad)
        {
            par_cart.type_cart tp_prov = GameObject.Find(str).GetComponent<par_cart>().type;
            if (tp_now_ == par_cart.type_cart.bases)
            {
                tp_now_ = GameObject.Find(str).GetComponent<par_cart>().type;
                i++;
            }

            else if (tp_prov == tp_now_)
            {
                i++;
            }
            else
            {
                tp_now_ = GameObject.Find(str).GetComponent<par_cart>().type;
                y_[x_] = i;
                x_++;
                i = 1;
            }
            if (lt_carts_ad.Count - 1 == col)
                y_[x_] = i;
            col++;
        }

        //int y_e = (i - 1) % 4;
        //int i1 = i;
        i = 0;
        int x_now_ = 0;
        //Debug.Log("add " + -0.25f * ((y_e) / 2.0f)+"  "+(gm==null));
        foreach (Transform tr in gameObject.transform.Find("carts active"))
        {

            if (i == y_[x_now_])
            {
                i = 0;
                x_now_++;
            }
            tr.localPosition = new Vector3(x_now_ * 0.28f, -0.25f * (y_[x_now_] - 1) + i * 0.5f, 0);

            i++;
        }
        gameObject.GetComponent<Animator>().SetBool("select_now", false);
        gameObject.GetComponent<Animator>().SetBool("perehod", true);
        selected_anim = false;
        cart_active_now = null;

        //func.controller_mob_wait contrl = func.controller_mob_wait;
        if (  func.bl_wait_hod )
        {
            atack_mob(cel);
        }
        if (gameObject.GetComponent<AudioSource>() != null)
            gameObject.GetComponent<AudioSource>().Play();

        //func.test_message = "good";
    }

    public void ad_cart_art_active(string name)
    {

        if (gameObject.transform.Find("artefacts") != null)
        {
            //rem_cart_art_active();
            //Debug.Log(true + "  ttttttttttt");
            if (name != null)
            {
                GameObject gm = Instantiate(GameObject.Find(name), gameObject.transform.Find("artefacts"));
                gm.name = "cart art";
                Destroy(gm.GetComponent<par_cart>());
                lt_carts_art_gm.Add(gm);
            }

            int i = 0;
            int i11 = 0;
            foreach (Transform tr in gameObject.transform.Find("artefacts"))
            {
                i++;
                i11++;
            }
            i = lt_carts_art.Count;
            //Debug.Log("add " + name + " " + i+" "+i11);
            //if(i>0)
            int angle = 0;
            if (i <= 4)
                angle = 30;
            else
                angle = 180 / i;
            int i1 = i;
            i = 0;

            foreach (GameObject obj in lt_carts_art_gm)
            {
                Transform tr = obj.transform;
                //int angle_now = 180 - 180 * (i % 2) + (2 * (i % 2) - 1) * angle * (i / 2);

                //float sin = (int)(Mathf.Sin(Mathf.Deg2Rad * angle_now) * 1000f) / 1000f;
                //float cos = (int)(Mathf.Cos(Mathf.Deg2Rad * angle_now) * 1000f) / 1000f;
                //Debug.Log(sin + " rad " + cos + "        " + i);
                //gm.transform.localPosition = new Vector3(cos * 0.75f, sin, 0);
                //Debug.Log(sin + " rad1 " + cos + "        " + i);
                //if(i=)
                //Debug.Log(i+ " ii1 " + i1 + gameObject.name+"  "+ gm.transform.localPosition);
                //Debug.Log(i == i1 - 1 );
                tr.localPosition = new Vector3(0, 0, 0);

                if (i1 - 1 == i && i1 % 2 != 0)
                {
                    //tr.position = new Vector3(i*i1+1, i1, 0);
                    tr.localPosition = new Vector3(-0.75f, i / 2 * 0.5f, 0);
                }
                else
                    //tr.localPosition = new Vector3(-i*i1, i1, 0);
                    tr.localPosition = new Vector3(-0.5f * (i % 2) - 0.5f, i / 2 * 0.5f, 0);
                //gm.transform.localPosition = new Vector3(-i, i1, 0);
                //tr.name ="cart art "+i1 ;
                i++;
                //Debug.Log(i + " ii12 " + i1 + gameObject.name + "  " + gm.transform.localPosition);

            }
        }

    }
    /*
    public void rem_cart_art_active()
    {
        if (gameObject.transform.Find("artefacts") != null)
        {
            int t = gameObject.transform.Find("artefacts").transform.childCount;
            Debug.Log("t " + t);
            for (int i = 0; i < t; i++)
            {
                DestroyImmediate(gameObject.transform.Find("artefacts").transform.GetChild(0).gameObject);
            }
            foreach (par_cart_art art in lt_carts_art)
            {
                ad_cart_art_active(art.name);
            }
        }

    }
    */
    public void Click()
    {
        //if (ui_move.bl_down)
        //{
        func.click(gameObject);

    }



    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Click();
    }
    /*
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        ui_move.click(gameObject);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        ui_move.unclick();
    }
    */
    public void activate_effect()
    {
        //func.test_message = "error active 1327";
        if (effect_now != null)
        {
            foreach (GameObject gm_ch in atack_childs_now)
            {
                gm_ch.GetComponent<Animator>().Play("atack act");
            }

            par_effect effect = effect_now.GetComponent<par_effect>();

            effect_now.active = true;
            foreach (GameObject gm in lt_effect_now)
            {
                gm.active = true;
            }


        }
        else
        {
            Debug.Log("del carts active");
            foreach (Transform tr in gameObject.transform.Find("carts active"))
            {
                Destroy(tr.gameObject);
            }
        }
        if (gameObject.name == data.data_now.pl_name_now && GameObject.Find("bt stop").GetComponent<Animator>().GetBool("opened"))
        {
            GameObject.Find("bt stop").GetComponent<ui_button>().opened_closed(GameObject.Find("bt stop"));
        }
        atack_childs_now.Clear();

        lt_carts_active_now_ui.Clear();
        effect_now = null;
        if (friend)
            ui.clean_pricels();

        //func.test_message = "good";
        //skill_mob_now = null;
        //func.hod_next_mob = true;

    }

    public void restart_add_cart_anim()
    {
        //anim.col_anim--;
        gameObject.GetComponent<Animator>().SetBool("perehod", false);
        //skill_mob_now = null;
        //func.hod_next_mob = true;

    }
    public void end_hod_anim()
    {

        gameObject.GetComponent<Animator>().SetBool("atack", false);
        gameObject.GetComponent<Animator>().SetBool("selected", false);
        gameObject.GetComponent<Animator>().SetBool("perehod", false);

        //anim.col_anim--;



        //skill_mob_now = null;
        //func.hod_next_mob = true;

    }
    public void end_anim(string name)
    {

        if (name != null && name != "")
            gameObject.GetComponent<Animator>().SetBool(name, false);
        anim.col_anim--;
        if (atack_comb_now!=null && atack_comb_now.action_after != null)
        {
            atack_comb_now.action_after.Invoke();
        }
        atack_comb_now = null;
        //skill_mob_now = null;
        //func.hod_next_mob = true;

    }
    public void activate_nps()
    {
        /*
        if (gameObject.name == "nps berserk")
        {
            par_mob pl = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
            pl.hp_max += func.carts[pl.x, pl.y].GetComponent<par_cart>().lvl;
            pl.hp += func.carts[pl.x, pl.y].GetComponent<par_cart>().lvl;
            Destroy(func.carts[pl.x, pl.y]);
            func.carts[pl.x, pl.y] = null;
        }
        */
        if (gameObject.name == "nps derlorg portal")
        {
            Vector2 v2_par = (Vector2)lt_par[0];
            lt_par.Clear();
            int x_par = (int)v2_par.x;
            int y_par = (int)v2_par.y;
            func.create_effect("create cave derlorg", x_par, y_par, null, true, 1, new List<string>());

        }
        //skill_mob_now = null;
        //func.hod_next_mob = true;

    }
    public void rem_sila(string cart)
    {
        par_lt_sila.Remove(cart);
    }
    public bool proverka_atack(List<string> lt_carts_now, bool final, bool activate)
    {
        bool bl = false;

        string st_start = null;
        st_start = lt_carts_now[0];


        par_cart.cart_combinations.comb_cart now = null;
        List<string> lt_carts_active_now_base = new List<string>();
        List<string> lt_carts_sila = new List<string>();
        List<string> lt_carts_sila_base = new List<string>();
        par_cart.type_cart cart0 = GameObject.Find(st_start).GetComponent<par_cart>().type;
        if (cart0 == par_cart.type_cart.sp_cuznec)
        {
            if (lt_carts_now.Count == 2 || (lt_carts_now.Count == 3 && lt_carts_now[1] == lt_carts_now[2]))
            {
                return true;
            }
        }

        foreach (string st_cart in lt_carts_now)
        {
            par_cart.type_cart cart_type = GameObject.Find(st_cart).GetComponent<par_cart>().type;
            par_cart.type_cart cart_type1 = par_cart.type_cart.bases;

            if (lt_carts_active_now_base.Count == 0)
                lt_carts_active_now_base.Add(st_cart);
            else
            {
                cart_type1 = GameObject.Find(lt_carts_active_now_base[lt_carts_active_now_base.Count - 1]).GetComponent<par_cart>().type;
                if (cart_type != cart_type1)
                {
                    lt_carts_active_now_base.Add(st_cart);
                }
                else
                {
                    //Debug.Log(st_cart);
                    lt_carts_sila.Add(st_cart);
                }
            }
        }
        //lt_carts_sila_base = new List<string>(lt_carts_sila);
        foreach (string st_cart in lt_carts_active_now_base)
        {
            Debug.Log(st_cart);
        }

        foreach (par_cart.cart_combinations.comb_cart ct in par_cart.cart_combinations.lt_comb)
        {
            if (ct.name == st_start)
            {
                now = ct;
                bool bl1 = true;
                Debug.Log("prow at " + now.next.Count + " " + lt_carts_active_now_base.Count);
                if (now.next.Count > 0 && lt_carts_active_now_base.Count > 1)
                    for (int i = 1; i < lt_carts_active_now_base.Count; i++)
                    {

                        if (now.next.ContainsKey(lt_carts_active_now_base[i]))
                            now = now.next[lt_carts_active_now_base[i]];
                        else bl1 = false;
                    }
                else if (now.next.Count == 0 && lt_carts_active_now_base.Count > 1)
                    bl1 = false;
                if (now != null && bl1)
                //(lt_carts_active_now_base.Count == 0 || now.next.ContainsKey(lt_carts_active_now_base[lt_carts_active_now_base.Count-1])
                {
                    bl = true;
                    break;
                }
                //now1 = ct;
                //break;
            }


        }
        //Debug.Log(bl + " bl two ");
        if (!final)
            return bl;
        else if (bl)
        {
            foreach (string cart_sila in par_lt_sila)
            {
                //Debug.Log(cart_sila + " del " + lt_carts_sila.Count);



                if (lt_carts_sila.Contains(cart_sila))
                {
                    lt_carts_sila.Remove(cart_sila);
                    lt_carts_sila_base.Add(cart_sila);
                }

                else
                {
                    par_cart.type_cart tp1 = par_cart.type_cart.bases;
                    bool bl_type = false;
                    try
                    {
                        foreach (string st_s in lt_carts_sila)
                        {
                            if (GameObject.Find(st_s).GetComponent<par_cart>().type.ToString() == cart_sila)
                            {
                                lt_carts_sila.Remove(st_s);
                                lt_carts_sila_base.Add(cart_sila);
                                break;
                            }
                        }
                        //tp1 = GameObject.Find("tp1").GetComponent<par_cart>().type;
                    }
                    catch (System.Exception)
                    {


                    }
                }

            }
            if (lt_carts_sila.Count != 0)
            {

                bl = false;
            }
            else if(activate)
            {
                foreach (string st in lt_carts_sila_base)
                {
                    rem_sila(st);
                }
            }

        }
        Debug.Log(bl + " " + lt_carts_sila.Count);
        return bl;
    }
    public int max_sila(object cart)
    {
        int col = 0;
        string cart_st = cart.ToString();//либо тип enum либо тип string

        foreach (string cart_sila in par_lt_sila)
        {
            //Debug.Log(cart_sila + " del " + lt_carts_sila.Count);



            if (cart_st == cart_sila)
            {
                col++;
            }

            else
            {
                par_cart.type_cart tp1 = par_cart.type_cart.bases;
                bool bl_type = false;
                try
                {
                    if (GameObject.Find(cart_st).GetComponent<par_cart>().type.ToString() == cart_sila)
                    {
                        col++;
                    }

                    //tp1 = GameObject.Find("tp1").GetComponent<par_cart>().type;
                }
                catch (System.Exception)
                {


                }
            }

        }
        return col;
    }
    public int max_speed(object cart)
    {
        int col = 0;
        string cart_st = cart.ToString();

        foreach (string cart_speed in par_lt_speed)
        {
            //Debug.Log(cart_sila + " del " + lt_carts_sila.Count);



            if (cart_st == cart_speed)
            {
                col++;
            }

            else
            {
                par_cart.type_cart tp1 = par_cart.type_cart.bases;
                bool bl_type = false;
                try
                {
                    if (GameObject.Find(cart_st).GetComponent<par_cart>().type.ToString() == cart_speed)
                    {
                        col++;
                    }

                    //tp1 = GameObject.Find("tp1").GetComponent<par_cart>().type;
                }
                catch (System.Exception)
                {


                }
            }

        }
        return col;
    }

    public bool atack_mob(GameObject gm)
    {
        Debug.Log("atack " + num);
        //func.test_message = "error atack mob";
        par_mob mb_click = gm.GetComponent<par_mob>();
        par_mob mb = this;


        int x_cl = -1;
        int y_cl = -1;
        if (gm.GetComponent<par_eath>() != null)
        {
            x_cl = gm.GetComponent<par_eath>().x;
            y_cl = gm.GetComponent<par_eath>().y;
        }
        if (gm.GetComponent<par_nat>() != null)
        {
            x_cl = gm.GetComponent<par_nat>().x;
            y_cl = gm.GetComponent<par_nat>().y;
        }
        if (gm.GetComponent<par_mob>() != null)
        {
            x_cl = gm.GetComponent<par_mob>().x;
            y_cl = gm.GetComponent<par_mob>().y;
        }
        //Debug.Log("atack " + num+" "+ lt_carts_added.Count);
        //func.test_message = "error atack mob 1386";
        if (lt_carts_added.Count > 0)
        {
            if (gameObject.GetComponent<Animator>().GetBool("selected"))
                gameObject.GetComponent<Animator>().SetBool("selected", true);
            else
            {
                gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                //Debug.Log(true);
            }
            //func.test_message = "error atack mob 1396";
            gameObject.GetComponent<Animator>().SetBool("select_now", true);
            //gameObject.GetComponent<Animator>().SetBool("perehod", false);
            selected_anim = true;
            lt_carts_added_now.Add(lt_carts_added[0]);
            remove_cart(lt_carts_added[0]);
            cart_active_now = lt_carts_added[0];
            lt_carts_active_now.Add(lt_carts_added[0]);

            //remove_cart(type);

            lt_carts_added.RemoveAt(0);
        }
        else
        {
            mb = this;

            Dictionary<par_cart_art, int> lt_art_rem_q = new Dictionary<par_cart_art, int>();
            List<par_cart_art> lt_art_rem = new List<par_cart_art>();
            //mb.gameObject.GetComponent<Animator>().SetBool("selected", false);
            // mb.gameObject.GetComponent<Animator>().SetBool("select_now", false);

            //mb.gameObject.GetComponent<Animator>().SetBool("atack", true);


            atacked = true;
            atack_auto_comb = false;

            int dop_pow_mn = 1;
            int pow_rez = 0;
            int pow_rez_seal = 0;

            int col_sila = 0;
            //func.test_message = "error atack mob 1417";
            List<int> dp_pow_lt = new List<int>();
            //data.data_now.cl_carts_speed ++;


            bool bl_att_hod = true;
            Queue<par_cart_art> art_att_hod = new Queue<par_cart_art>();

            //bool bl_art_boss_berserk = false;
            par_cart_art bl_art_boss_dungalorg = null;
            //if (data.data_now.cl_carts_speed >= 4)
            //{
            //data.give_achive("быстрее некуда");
            //}
            int pow_baf = 0;
            foreach (par_baf baf in mb.lt_bafs)
            {
                if (baf.type == par_baf.type_baf.berserk)
                {
                    //Debug.Log("pow baf true "+ baf.power);
                    pow_baf += baf.power;
                    //break;
                }
                else if (baf.type == par_baf.type_baf.rage)
                {
                    //Debug.Log("pow baf true "+ baf.power);
                    pow_baf++;
                    //break;
                }
            }
            List<string> lt_carts_active_now_base = new List<string>();
            Dictionary<string, int> lt_art_base = new Dictionary<string, int>();

            int par_cart_shaman = 0;
            HashSet<par_cart.type_cart> hs_carts = new HashSet<par_cart.type_cart>();

            foreach (string stt in lt_carts_active_now)
            {
                par_cart.type_cart tp_cart = func.baza[stt].GetComponent<par_cart>().type;
                hs_carts.Add(tp_cart);

            }


            foreach (par_cart_art cart in mb.lt_carts_art)
            {
                bool bl_art = false;
                if (cart.name == "cart poition touch")
                {
                    bl_art = true;
                }
                else if (cart.name == "cart fire touch")
                {
                    bl_art = true;
                }
                else if (cart.name == "cart rock touch")
                {
                    bl_art = true;
                }
                else if (cart.name == "cart blood touch")
                {
                    bl_art = true;
                }
                else if (cart.name == "cart energy touch")
                {
                    bl_art = true;
                }
                else if (cart.name == "cart ice touch")
                {
                    bl_art = true;
                }
                else if (cart.perez_now == 0)
                {
                    //if (par_cart_shaman == 0)
                    //{
                        if (cart.name == "cart mini spirit blood" && hs_carts.Contains(par_cart.type_cart.blood))
                        {
                            //par_cart_shaman.Add(cart);
                            par_cart_shaman+=3;
                            lt_art_rem.Add(cart);
                        }
                        else if (cart.name == "cart mini spirit rock" && hs_carts.Contains(par_cart.type_cart.rock))
                        {
                            par_cart_shaman+=3;
                            lt_art_rem.Add(cart);
                            //cart_shaman.Add(cart);
                        }
                        else if (cart.name == "cart mini spirit ice" && hs_carts.Contains(par_cart.type_cart.ice))
                        {
                            par_cart_shaman+=3;
                            lt_art_rem.Add(cart);
                            //cart_shaman.Add(cart);
                        }
                        else if (cart.name == "cart mini spirit energy" && hs_carts.Contains(par_cart.type_cart.energy))
                        {
                            par_cart_shaman+=3;
                            lt_art_rem.Add(cart);
                            //cart_shaman.Add(cart);
                        }
                        else if (cart.name == "cart mini spirit fire" && hs_carts.Contains(par_cart.type_cart.fire))
                        {
                            par_cart_shaman+=3;
                            lt_art_rem.Add(cart);
                            //cart_shaman.Add(cart);
                        }
                        else if (cart.name == "cart mini spirit poition" && hs_carts.Contains(par_cart.type_cart.poition))
                        {
                            par_cart_shaman+=3;
                            lt_art_rem.Add(cart);
                            //cart_shaman.Add(cart);
                        }
                    //}

                    else if (cart.name == "cart mini spirit")
                    {
                        par_cart_shaman+=3;
                        lt_art_rem.Add(cart);
                        //cart_shaman.Add(cart);
                    }
                }


                if (bl_art)
                {
                    if (lt_art_base.ContainsKey(cart.name))
                    {
                        lt_art_base[cart.name]++;
                    }
                    else
                    {



                        lt_art_base.Add(cart.name, 1);
                    }
                }

            }

            foreach (string st_cart_1 in mb.lt_carts_active_now)
            {
                string st_cart = st_cart_1;
                int pw = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                //if()

                par_cart.type_cart cart_type = GameObject.Find(st_cart).GetComponent<par_cart>().type;
                /*
                if (par_cart_shaman > 0)
                {
                    if (cart_type == par_cart.type_cart.blood)
                    {
                        cart_type = par_cart.type_cart.spirit_blood;
                        st_cart = "cart spirit blood";
                    }
                    else if (cart_type == par_cart.type_cart.rock)
                    {
                        cart_type = par_cart.type_cart.spirit_rock;
                        st_cart = "cart spirit rock";
                    }
                    else if (cart_type == par_cart.type_cart.energy)
                    {
                        cart_type = par_cart.type_cart.spirit_energy;
                        st_cart = "cart spirit energy";
                    }
                    else if (cart_type == par_cart.type_cart.ice)
                    {
                        cart_type = par_cart.type_cart.spirit_ice;
                        st_cart = "cart spirit ice";
                    }
                    else if (cart_type == par_cart.type_cart.fire)
                    {
                        cart_type = par_cart.type_cart.spirit_fire;
                        st_cart = "cart spirit fire";
                    }
                    else if (cart_type == par_cart.type_cart.poition)
                    {
                        cart_type = par_cart.type_cart.spirit_poition;
                        st_cart = "cart spirit poition";
                    }
                    //pw = pw * 4 * par_cart_shaman;
                }
                */
                bool cart_spetial = GameObject.Find(st_cart).GetComponent<par_cart>().spetial;
                par_cart.type_cart cart_type1 = par_cart.type_cart.bases;



                if (lt_carts_active_now_base.Count == 0)
                    lt_carts_active_now_base.Add(st_cart);
                else
                {
                    par_cart pr_cart1 = GameObject.Find(lt_carts_active_now_base[lt_carts_active_now_base.Count - 1]).GetComponent<par_cart>();
                    cart_type1 = pr_cart1.type;
                    if (cart_type != cart_type1 || cart_type == par_cart.type_cart.sp_cuznec)
                    {
                        lt_carts_active_now_base.Add(st_cart);
                    }
                    else
                    {
                        col_sila++;
                        //pw += pr_cart1.lvl;
                    }
                }
                int dop_pow = pw;
                int pow_base = pw;
                //int pow_seal = 0;

                dop_pow += pow_baf;

                List<par_cart_art> bl_cart_art_pow = new List<par_cart_art>();
                if (mb.lt_carts_active_now[0] != "cart cuznec")
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {
                        if (cart.name == "cart potoc poition" && (cart_type == par_cart.type_cart.poition || cart_type == par_cart.type_cart.spirit_poition))
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.poition || cart_now.type == par_cart.type_cart.spirit_poition)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 6);
                        }
                        else if (cart.name == "cart potoc fire" && (cart_type == par_cart.type_cart.fire || cart_type == par_cart.type_cart.spirit_fire))
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.fire || cart_now.type == par_cart.type_cart.spirit_fire)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 6);
                        }
                        else if (cart.name == "cart potoc rock" && (cart_type == par_cart.type_cart.rock || cart_type == par_cart.type_cart.spirit_rock))
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.rock || cart_now.type == par_cart.type_cart.spirit_rock)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 6);
                        }
                        else if (cart.name == "cart potoc blood" && (cart_type == par_cart.type_cart.blood || cart_type == par_cart.type_cart.spirit_blood))
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.blood || cart_now.type == par_cart.type_cart.spirit_blood)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 6);
                        }
                        else if (cart.name == "cart potoc energy" && (cart_type == par_cart.type_cart.energy || cart_type == par_cart.type_cart.spirit_energy))
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.energy || cart_now.type == par_cart.type_cart.spirit_energy)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 6);
                        }
                        else if (cart.name == "cart potoc ice" && (cart_type == par_cart.type_cart.ice || cart_type == par_cart.type_cart.spirit_ice))
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.ice || cart_now.type == par_cart.type_cart.spirit_ice)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 6);
                        }

                        else if (cart.name == "cart derlorg potoc" && cart_type == par_cart.type_cart.derlorg)
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.derlorg)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 6);
                        }





                        if (cart.name == "cart poition st" && (cart_type == par_cart.type_cart.poition || cart_type == par_cart.type_cart.spirit_poition))
                        {
                            if (lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base[cart.name]++;
                            }
                            else
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart fire st" && (cart_type == par_cart.type_cart.fire || cart_type == par_cart.type_cart.spirit_fire))
                        {
                            if (lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base[cart.name]++;
                            }
                            else
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart rock st" && (cart_type == par_cart.type_cart.rock || cart_type == par_cart.type_cart.spirit_rock))
                        {
                            if (lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base[cart.name]++;
                            }
                            else
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart blood st" && (cart_type == par_cart.type_cart.blood || cart_type == par_cart.type_cart.spirit_blood))
                        {
                            if (lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base[cart.name]++;
                            }
                            else
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart energy st" && (cart_type == par_cart.type_cart.energy || cart_type == par_cart.type_cart.spirit_energy))
                        {
                            if (lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base[cart.name]++;
                            }
                            else
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart ice st" && (cart_type == par_cart.type_cart.ice || cart_type == par_cart.type_cart.spirit_ice))
                        {
                            if (lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base[cart.name]++;
                            }
                            else
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }


                        else if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                        {
                            if (lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base[cart.name]++;
                            }
                            else
                            {
                                lt_art_base.Add(cart.name, 1);
                            }
                            if (func.baza[st_cart_1].GetComponent<par_cart>().spetial)
                                lt_art_base[cart.name]++;

                        }


                        /*
                        else if (cart.name == "cart mast poition" && cart_type == par_cart.type_cart.poition)
                        {
                            if (!lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart mast fire" && cart_type == par_cart.type_cart.fire)
                        {
                            if (!lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart mast rock" && cart_type == par_cart.type_cart.rock)
                        {
                            if (!lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart mast blood" && cart_type == par_cart.type_cart.blood)
                        {
                            if (!lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart mast energy" && cart_type == par_cart.type_cart.energy)
                        {
                            if (!lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        else if (cart.name == "cart mast ice" && cart_type == par_cart.type_cart.ice)
                        {
                            if (!lt_art_base.ContainsKey(cart.name))
                            {
                                lt_art_base.Add(cart.name, 1);
                            }

                        }
                        */


                        else if (cart.q > 0)
                        {
                            if (bl_att_hod)
                            {

                            }
                        }
                        else if (cart.perez_now == 0)
                        {
                            if (cart.name == "cart dungalorg heard")
                            {
                                bl_art_boss_dungalorg = cart;
                                //dop_pow *= 4;
                                //mb.activate_art(cart, x_cl, y_cl);
                            }
                        }
                        if (cart.type == par_cart.type_cart.art_pow_rock && (cart_type == par_cart.type_cart.rock || cart_type == par_cart.type_cart.spirit_rock))
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_fire && (cart_type == par_cart.type_cart.fire || cart_type == par_cart.type_cart.spirit_fire))
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_blood && (cart_type == par_cart.type_cart.blood || cart_type == par_cart.type_cart.spirit_blood))
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_energy &&(cart_type == par_cart.type_cart.energy || cart_type == par_cart.type_cart.spirit_energy))
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_ice && (cart_type == par_cart.type_cart.ice || cart_type == par_cart.type_cart.spirit_ice))
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_poition && (cart_type == par_cart.type_cart.poition || cart_type == par_cart.type_cart.spirit_poition))
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_att_pow && cart_type == par_cart.type_cart.damage)
                        {
                            dop_pow += cart.lvl;
                            //mb.add_baf(par_baf.type_baf.bar, -1, 1);

                        }
                        else if (cart.type == par_cart.type_cart.art_pow_bomb && cart_type == par_cart.type_cart.sp_bomb)
                        {
                            dop_pow += cart.lvl;
                            //mb.add_baf(par_baf.type_baf.bar, -1, 1);

                        }
                        else if (cart.type == par_cart.type_cart.art_dych_pow && cart_type == par_cart.type_cart.sp_dych)
                        {
                            dop_pow += cart.lvl;
                            //mb.add_baf(par_baf.type_baf.bar, -1, 1);

                        }
                        //mb.activate_art(cart, mb.x, mb.y);

                        else if (cart.type == par_cart.type_cart.art_bar_pow && cart_type == par_cart.type_cart.sp_barier)
                        {
                            dop_pow += cart.lvl;
                            //mb.add_baf(par_baf.type_baf.bar, -1, 1);

                        }


                        else if (cart.type == par_cart.type_cart.art_rock_mast && (cart_type == par_cart.type_cart.rock || cart_type == par_cart.type_cart.spirit_rock))
                        {
                            dop_pow ++;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_fire_mast && (cart_type == par_cart.type_cart.fire || cart_type == par_cart.type_cart.spirit_fire))
                        {
                            dop_pow ++;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_blood_mast && (cart_type == par_cart.type_cart.blood || cart_type == par_cart.type_cart.spirit_blood))
                        {
                            dop_pow ++;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_energy_mast && (cart_type == par_cart.type_cart.energy || cart_type == par_cart.type_cart.spirit_energy))
                        {
                            dop_pow ++;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_ice_mast && (cart_type == par_cart.type_cart.ice || cart_type == par_cart.type_cart.spirit_ice))
                        {
                            dop_pow ++;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_poition_mast && (cart_type == par_cart.type_cart.poition || cart_type == par_cart.type_cart.spirit_poition))
                        {
                            dop_pow ++;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }

                        //mb.activate_art(cart, mb.x, mb.y);



                        else if (cart.type == par_cart.type_cart.art_pow || cart.type == par_cart.type_cart.art_seal_pow)
                        {
                            dop_pow++;
                            //mb.add_baf(par_baf.type_baf.bar, -1, 1);

                        }
                        else if (cart.name == "cart berserk st")
                        {
                            dop_pow += ((mb.hp_max - mb.hp) / 5);
                        }

                    }

                if (bl_cart_art_pow.Count > 0)
                {
                    foreach (par_cart_art art_pow in bl_cart_art_pow)
                    {
                        activate_art(art_pow, x_cl, y_cl);
                    }
                }

                foreach (GameObject gm_ch in mb.childs)
                {
                    par_mob mb_ch = gm_ch.GetComponent<par_mob>();
                    if ((gm_ch.name == "dych fire" && cart_type == par_cart.type_cart.fire) ||
                        (gm_ch.name == "dych blood" && cart_type == par_cart.type_cart.blood) ||
                        (gm_ch.name == "dych energy" && cart_type == par_cart.type_cart.energy) ||
                        (gm_ch.name == "dych ice" && cart_type == par_cart.type_cart.ice) ||
                        (gm_ch.name == "dych rock" && cart_type == par_cart.type_cart.rock) ||
                        (gm_ch.name == "dych poition" && cart_type == par_cart.type_cart.poition))
                    {
                        int cx = Mathf.Abs(x_cl - mb_ch.x);
                        int cy = Mathf.Abs(y_cl - mb_ch.y);
                        if (cx + cy <= mb_ch.distanse_attack)
                        {
                            dop_pow += mb_ch.hp_max;
                            atack_childs_now.Add(gm_ch);
                            if (mb_ch.x < x_cl)
                                mb_ch.simm(1);
                            else if (mb_ch.x > x_cl)
                                mb_ch.simm(-1);
                            //gm_ch.GetComponent<Animator>().Play("atack act");
                        }

                    }
                }


                if (gameObject.name == data.data_now.pl_name_now)
                {
                    if (cart_type == par_cart.type_cart.damage && data.data_now.cl_cart_att < 200)
                    {
                        data.data_now.cl_cart_att++;
                        if (data.data_now.cl_cart_att == 200)
                        {
                            data.data_now.lt_players.Add(data.players.atack);
                            data.give_achive("хорошая атака");

                        }
                    }
                    if (cart_type == par_cart.type_cart.sp_barier && data.data_now.cl_cart_bar < 100)
                    {
                        data.data_now.cl_cart_bar++;
                        if (data.data_now.cl_cart_bar == 100)
                        {
                            data.data_now.lt_players.Add(data.players.barier);
                            data.give_achive("хорошая защита");

                        }
                    }
                    if (cart_type == par_cart.type_cart.rock && data.data_now.cl_cart_rock < 200)
                    {
                        data.data_now.cl_cart_rock++;
                        if (data.data_now.cl_cart_rock == 200)
                        {
                            data.data_now.lt_players.Add(data.players.rock);
                            data.give_achive("сила камня");

                        }
                    }
                    if (cart_type == par_cart.type_cart.blood && data.data_now.cl_cart_blood < 200)
                    {
                        data.data_now.cl_cart_blood++;
                        if (data.data_now.cl_cart_blood == 200)
                        {
                            data.data_now.lt_players.Add(data.players.blood);
                            data.give_achive("сила крови");

                        }
                    }
                    if (cart_type == par_cart.type_cart.fire && data.data_now.cl_cart_fire < 200)
                    {
                        data.data_now.cl_cart_fire++;
                        if (data.data_now.cl_cart_fire == 200)
                        {
                            data.data_now.lt_players.Add(data.players.fire);
                            data.give_achive("сила огня");

                        }
                    }
                    if (cart_type == par_cart.type_cart.energy && data.data_now.cl_cart_energy < 200)
                    {
                        data.data_now.cl_cart_energy++;
                        if (data.data_now.cl_cart_energy == 200)
                        {
                            data.data_now.lt_players.Add(data.players.energy);
                            data.give_achive("сила электричества");
                            //data.data_now.lt_players.Add(data.players.energy);
                        }
                    }
                    if (cart_type == par_cart.type_cart.ice && data.data_now.cl_cart_ice < 200)
                    {
                        data.data_now.cl_cart_ice++;
                        if (data.data_now.cl_cart_ice == 200)
                        {
                            data.data_now.lt_players.Add(data.players.ice);
                            data.give_achive("сила льда");

                        }
                    }
                    if (cart_type == par_cart.type_cart.poition && data.data_now.cl_cart_poition < 200)
                    {
                        data.data_now.cl_cart_poition++;
                        if (data.data_now.cl_cart_poition == 200)
                        {
                            data.data_now.lt_players.Add(data.players.poition);
                            data.give_achive("сила яда");

                        }
                    }


                    if (data.data_now.pl_name_now == "berserk player")
                    {
                        dop_pow *=2;
                        mb.damage(1, "none");
                    }
                    else if (cart_type == par_cart.type_cart.damage && data.data_now.pl_name_now == "atack player")
                    {
                        //dop_pow *=2;
                        dop_pow ++;
                    }

                    else if (cart_type == par_cart.type_cart.sp_barier && data.data_now.pl_name_now == "barier player")
                    {
                        dop_pow++;
                    }
                    else if (cart_type == par_cart.type_cart.rock && data.data_now.pl_name_now == "rock player")
                    {
                        dop_pow++;
                    }
                    else if (cart_type == par_cart.type_cart.fire && data.data_now.pl_name_now == "fire player")
                    {
                        dop_pow++;
                    }
                    else if (cart_type == par_cart.type_cart.blood && data.data_now.pl_name_now == "blood player")
                    {
                        dop_pow++;
                    }
                    else if (cart_type == par_cart.type_cart.energy && data.data_now.pl_name_now == "energy player")
                    {
                        dop_pow++;
                    }
                    else if (cart_type == par_cart.type_cart.ice && data.data_now.pl_name_now == "ice player")
                    {
                        dop_pow++;
                    }
                    else if (cart_type == par_cart.type_cart.poition && data.data_now.pl_name_now == "poition player")
                    {
                        dop_pow++;
                    }



                    
                    else if (cart_type == par_cart.type_cart.derlorg && data.data_now.pl_name_now == "derlorg player")
                    {
                        dop_pow++;
                    }
                    
                }




                if ((cart_type == par_cart.type_cart.derlorg) &&  (lt_carts_active_now_base[0]=="cart derlorg"))
                {
                    pow_rez_seal += dop_pow;
                    
                }

                else
                {
                    pow_rez += dop_pow;
                }
            }

            List<string> lt_carts_speed = new List<string>(lt_carts_active_now_base);
            string cart_speed_base = null;
            foreach (string cart_speed in par_lt_speed)
            {
                Debug.Log(cart_speed + " del ");



                if (lt_carts_speed.Contains(cart_speed))
                {
                    lt_carts_speed.Remove(cart_speed);
                    cart_speed_base = cart_speed;
                    Debug.Log("speed cart(0) " + cart_speed_base);
                    break;
                }

                else
                {
                    par_cart.type_cart tp1 = par_cart.type_cart.bases;
                    bool bl_type = false;
                    bool bl_break = false;
                    try
                    {
                        foreach (string st_s in lt_carts_speed)
                        {
                            if (GameObject.Find(st_s).GetComponent<par_cart>().type.ToString() == cart_speed)
                            {
                                lt_carts_speed.Remove(st_s);
                                cart_speed_base = cart_speed;
                                Debug.Log("speed cart(0) " + cart_speed_base);
                                bl_break = true;
                                break;
                            }
                        }
                        if (bl_break)
                            break;
                        //tp1 = GameObject.Find("tp1").GetComponent<par_cart>().type;
                    }
                    catch (System.Exception)
                    {


                    }
                }
                if (cart_speed_base != null)
                {
                    Debug.Log("speed cart " + cart_speed_base);
                }
            }
            if (cart_speed_base != null)
            {
                bl_att_hod = false;
                par_lt_speed.Remove(cart_speed_base);

            }




            bool bl_d = false;
            if (lt_carts_active_now_base.Count > 1)
            {
                par_cart.type_cart cart0 = GameObject.Find(lt_carts_active_now_base[0]).GetComponent<par_cart>().type;
                if (cart0 == par_cart.type_cart.derlorg)
                    bl_d = true;
            }
            if (mb.lt_carts.Contains("cart dungalorg 1"))
            {
                pow_rez = 0;
                pow_rez_seal = 0;
                mb.remove_cart("cart dungalorg 1");
            }
            else
            {
                int i_ = 0;
                foreach (string st_d in mb.lt_carts)
                {
                    if (st_d == "cart dungalorg")
                    {
                        i_++;

                    }
                }
                for (int i = 0; i < i_; i++)
                {
                    if (bl_d && pow_rez_seal > 0)
                    {
                        pow_rez_seal--;
                        mb.remove_cart("cart dungalorg");
                    }
                    else if (pow_rez > 0)
                    {
                        pow_rez--;
                        mb.remove_cart("cart dungalorg");
                    }


                }
            }
            bool bl_att = false;
            bool bl_spirit = lt_carts_active_now_base.Contains("cart spirit(boss)")
                || lt_carts_active_now_base.Contains("cart spirit fire")
                || lt_carts_active_now_base.Contains("cart spirit blood")
                || lt_carts_active_now_base.Contains("cart spirit ice")
                || lt_carts_active_now_base.Contains("cart spirit rock")
                || lt_carts_active_now_base.Contains("cart spirit energy")
                || lt_carts_active_now_base.Contains("cart spirit poition");

            if (lt_carts_active_now_base[0] == "cart dungalorg(sleep)" && lt_carts_active_now_base.Count == 1)
            {
                if (mb_click != null)
                {
                    for (int i = 0; i < pow_rez; i++)
                    {
                        mb_click.spawn_cart("cart dungalorg", gameObject.transform.Find("carts active").gameObject);
                    }
                }
                bl_att = true;
                bl_att_hod = false;
                //func.nums_mob_wolk.Add(num);
                //speed_hod = true;
                //bl_att_hod = false;
                //anim.col_anim++;
                //func.next_hod();

            }
            else if (lt_carts_active_now_base[0] == "cart dungalorg(sleep) 1" && lt_carts_active_now_base.Count == 1)
            {
                Debug.Log(pow_rez + " rez pow");
                if (mb_click != null)
                {
                    for (int i = 0; i < pow_rez; i++)
                    {
                        mb_click.spawn_cart("cart dungalorg 1", gameObject.transform.Find("carts active").gameObject);
                    }

                }





                //mb.lt_carts_active_now.RemoveAt(0);
                bl_att = true;
                bl_att_hod = false;
                //func.nums_mob_wolk.Add(num);
                //speed_hod = true;

            }
            else if ((lt_carts_active_now_base[0] == "cart cuznec" || lt_carts_active_now_base[0] == "cart cuznec 1") && lt_carts_active_now.Count == 2)
            {
                int lv = GameObject.Find(lt_carts_active_now_base[1]).GetComponent<par_cart>().lvl;
                par_cart.type_cart cart0 = GameObject.Find(lt_carts_active_now_base[1]).GetComponent<par_cart>().type;
                bool bl_create = false;
                bool bl_el_cuzna = false;
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if (cart.type == par_cart.type_cart.art_cuzna && cart.q >= lv && !bl_create)
                    {
                        cart.q -= lv;
                        mb.activate_art(cart, x_cl, y_cl);
                        bl_create = true;
                        break;
                    }

                }
                //func.mob[x, y].GetComponent<par_mob>().add_baf(par_baf.type_baf.bar, -1, 1);
                if (bl_create)
                {
                    string cart_create = null;
                    string carts_cr = lt_carts_active_now[1];

                    if (lt_carts_active_now_base[0] == "cart cuznec" || lt_carts_active_now_base[0] == "cart cuznec 1")
                    {
                        if (carts_cr == "cart cuznec")
                            cart_create = "cart cuznec 1";

                        if (carts_cr == "cart damage")
                            cart_create = "cart damage 1";
                        if (carts_cr == "cart barier")
                            cart_create = "cart barier 1";

                        if (carts_cr == "cart bomb")
                            cart_create = "cart bomb 1";


                        if (carts_cr == "cart fire")
                            cart_create = "cart fire 1";
                        else if (carts_cr == "cart rock")
                            cart_create = "cart rock 1";
                        else if (carts_cr == "cart blood")
                            cart_create = "cart blood 1";
                        else if (carts_cr == "cart poition")
                            cart_create = "cart poition 1";
                        else if (carts_cr == "cart ice")
                            cart_create = "cart ice 1";
                        else if (carts_cr == "cart energy")
                            cart_create = "cart energy 1";

                        else if (carts_cr == "cart sp atack")
                            cart_create = "cart sp atack 1";

                        else if (carts_cr == "cart sp barier")
                            cart_create = "cart sp barier 1";
                        else if (carts_cr == "cart sp bomb")
                            cart_create = "cart sp bomb 1";
                        else if (carts_cr == "cart sp dych")
                            cart_create = "cart sp dych 1";

                        else if (carts_cr == "cart sp fire")
                            cart_create = "cart sp fire 1";
                        else if (carts_cr == "cart sp rock")
                            cart_create = "cart sp rock 1";
                        else if (carts_cr == "cart sp blood")
                            cart_create = "cart sp blood 1";
                        else if (carts_cr == "cart sp poition")
                            cart_create = "cart sp poition 1";
                        else if (carts_cr == "cart sp ice")
                            cart_create = "cart sp ice 1";
                        else if (carts_cr == "cart sp energy")
                            cart_create = "cart sp energy 1";


                        if (carts_cr == "cart act att")
                            cart_create = "cart act att 1";

                        if (carts_cr == "cart act bar")
                            cart_create = "cart act bar 1";
                        if (carts_cr == "cart act bomb")
                            cart_create = "cart act bomb 1";
                        if (carts_cr == "cart act dych")
                            cart_create = "cart act dych 1";

                        else if (carts_cr == "cart act fire")
                            cart_create = "cart act fire 1";
                        else if (carts_cr == "cart act rock")
                            cart_create = "cart act rock 1";
                        else if (carts_cr == "cart act blood")
                            cart_create = "cart act blood 1";
                        else if (carts_cr == "cart act poition")
                            cart_create = "cart act poition 1";
                        else if (carts_cr == "cart act ice")
                            cart_create = "cart act ice 1";
                        else if (carts_cr == "cart act energy")
                            cart_create = "cart act energy 1";

                        if (carts_cr == "cart speed att")
                            cart_create = "cart speed att 1";

                        if (carts_cr == "cart speed bar")
                            cart_create = "cart speed bar 1";
                        if (carts_cr == "cart speed bomb")
                            cart_create = "cart speed bomb 1";
                        if (carts_cr == "cart speed dych")
                            cart_create = "cart speed dych 1";

                        if (carts_cr == "cart speed fire")
                            cart_create = "cart speed fire 1";
                        else if (carts_cr == "cart speed rock")
                            cart_create = "cart speed rock 1";
                        else if (carts_cr == "cart speed blood")
                            cart_create = "cart speed blood 1";
                        else if (carts_cr == "cart speed poition")
                            cart_create = "cart speed poition 1";
                        else if (carts_cr == "cart speed ice")
                            cart_create = "cart speed ice 1";
                        else if (carts_cr == "cart speed energy")
                            cart_create = "cart speed energy 1";



                        if (carts_cr == "cart att pow")
                            cart_create = "cart att pow 1";

                        if (carts_cr == "cart bar pow")
                            cart_create = "cart bar pow 1";
                        if (carts_cr == "cart bomb pow")
                            cart_create = "cart bomb pow 1";
                        if (carts_cr == "cart dych pow")
                            cart_create = "cart dych pow 1";

                        else if (carts_cr == "cart fire pow")
                            cart_create = "cart fire pow 1";
                        else if (carts_cr == "cart poition pow")
                            cart_create = "cart poition pow 1";
                        else if (carts_cr == "cart blood pow")
                            cart_create = "cart blood pow 1";
                        else if (carts_cr == "cart rock pow")
                            cart_create = "cart rock pow 1";
                        else if (carts_cr == "cart ice pow")
                            cart_create = "cart ice pow 1";
                        else if (carts_cr == "cart energy pow")
                            cart_create = "cart energy pow 1";

                        if (carts_cr == "cart att cuzna")
                            cart_create = "cart att cuzna 1";

                        if (carts_cr == "cart bar cuzna")
                            cart_create = "cart bar cuzna 1";
                        if (carts_cr == "cart bomb cuzna")
                            cart_create = "cart bomb cuzna 1";
                        if (carts_cr == "cart dych cuzna")
                            cart_create = "cart dych cuzna 1";

                        else if (carts_cr == "cart fire cuzna")
                            cart_create = "cart fire cuzna 1";
                        else if (carts_cr == "cart poition cuzna")
                            cart_create = "cart poition cuzna 1";
                        else if (carts_cr == "cart blood cuzna")
                            cart_create = "cart blood cuzna 1";
                        else if (carts_cr == "cart rock cuzna")
                            cart_create = "cart rock cuzna 1";
                        else if (carts_cr == "cart ice cuzna")
                            cart_create = "cart ice cuzna 1";
                        else if (carts_cr == "cart energy cuzna")
                            cart_create = "cart energy cuzna 1";


                        if (carts_cr == "cart att bomb")
                            cart_create = "cart att bomb 1";

                        if (carts_cr == "cart bar bomb")
                            cart_create = "cart bar bomb 1";
                        if (carts_cr == "cart bomb bomb")
                            cart_create = "cart bomb bomb 1";
                        if (carts_cr == "cart dych bomb")
                            cart_create = "cart dych bomb 1";

                        else if (carts_cr == "cart fire bomb")
                            cart_create = "cart fire bomb 1";
                        else if (carts_cr == "cart poition bomb")
                            cart_create = "cart poition bomb 1";
                        else if (carts_cr == "cart blood bomb")
                            cart_create = "cart blood bomb 1";
                        else if (carts_cr == "cart rock bomb")
                            cart_create = "cart rock bomb 1";
                        else if (carts_cr == "cart ice bomb")
                            cart_create = "cart ice bomb 1";
                        else if (carts_cr == "cart energy bomb")
                            cart_create = "cart energy bomb 1";

                    }
                    if (lt_carts_active_now_base[0] == "cart cuznec 1" && cart_create == null)
                    {
                        if (carts_cr == "cart damage 1")
                            cart_create = "cart damage 2";

                        if (carts_cr == "cart barier 1")
                            cart_create = "cart barier 2";
                        if (carts_cr == "cart bomb 1")
                            cart_create = "cart bomb 2";
                        if (carts_cr == "cart dych 1")
                            cart_create = "cart dych 2";

                        if (carts_cr == "cart fire 1")
                            cart_create = "cart fire 2";
                        else if (carts_cr == "cart rock 1")
                            cart_create = "cart rock 2";
                        else if (carts_cr == "cart blood 1")
                            cart_create = "cart blood 2";
                        else if (carts_cr == "cart poition 1")
                            cart_create = "cart poition 2";
                        else if (carts_cr == "cart ice 1")
                            cart_create = "cart ice 2";
                        else if (carts_cr == "cart energy 1")
                            cart_create = "cart energy 2";



                        else if (carts_cr == "cart sp atack 1")
                            cart_create = "cart sp atack 2";

                        else if (carts_cr == "cart sp barier 1")
                            cart_create = "cart sp barier 2";
                        else if (carts_cr == "cart sp bomb 1")
                            cart_create = "cart sp bomb 2";
                        else if (carts_cr == "cart sp dych 1")
                            cart_create = "cart sp dych 2";

                        else if (carts_cr == "cart sp fire 1")
                            cart_create = "cart sp fire 2";
                        else if (carts_cr == "cart sp rock 1")
                            cart_create = "cart sp rock 2";
                        else if (carts_cr == "cart sp blood 1")
                            cart_create = "cart sp blood 2";
                        else if (carts_cr == "cart sp poition 1")
                            cart_create = "cart sp poition 2";
                        else if (carts_cr == "cart sp ice 1")
                            cart_create = "cart sp ice 2";
                        else if (carts_cr == "cart sp energy 1")
                            cart_create = "cart sp energy 2";


                        if (carts_cr == "cart act att 1")
                            cart_create = "cart act att 2";

                        if (carts_cr == "cart act bar 1")
                            cart_create = "cart act bar 2";
                        if (carts_cr == "cart act bomb 1")
                            cart_create = "cart act bomb 2";
                        if (carts_cr == "cart act dych 1")
                            cart_create = "cart act dych 2";

                        else if (carts_cr == "cart act fire 1")
                            cart_create = "cart act fire 2";
                        else if (carts_cr == "cart act rock 1")
                            cart_create = "cart act rock 2";
                        else if (carts_cr == "cart act blood 1")
                            cart_create = "cart act blood 2";
                        else if (carts_cr == "cart act poition 1")
                            cart_create = "cart act poition 2";
                        else if (carts_cr == "cart act ice 1")
                            cart_create = "cart act ice 2";
                        else if (carts_cr == "cart act energy 1")
                            cart_create = "cart act energy 2";

                        if (carts_cr == "cart speed att 1")
                            cart_create = "cart speed att 2";

                        if (carts_cr == "cart speed bar 1")
                            cart_create = "cart speed bar 2";
                        if (carts_cr == "cart speed bomb 1")
                            cart_create = "cart speed bomb 2";
                        if (carts_cr == "cart speed dych 1")
                            cart_create = "cart speed dych 2";

                        if (carts_cr == "cart speed fire 1")
                            cart_create = "cart speed fire 2";
                        else if (carts_cr == "cart speed rock 1")
                            cart_create = "cart speed rock 2";
                        else if (carts_cr == "cart speed blood 1")
                            cart_create = "cart speed blood 2";
                        else if (carts_cr == "cart speed poition 1")
                            cart_create = "cart speed poition 2";
                        else if (carts_cr == "cart speed ice 1")
                            cart_create = "cart speed ice 2";
                        else if (carts_cr == "cart speed energy 1")
                            cart_create = "cart speed energy 2";



                        if (carts_cr == "cart att pow 1")
                            cart_create = "cart att pow 2";

                        if (carts_cr == "cart bar pow 1")
                            cart_create = "cart bar pow 2";
                        if (carts_cr == "cart bomb pow 1")
                            cart_create = "cart bomb pow 2";
                        if (carts_cr == "cart dych pow 1")
                            cart_create = "cart dych pow 2";

                        else if (carts_cr == "cart fire pow 1")
                            cart_create = "cart fire pow 2";
                        else if (carts_cr == "cart poition pow 1")
                            cart_create = "cart poition pow 2";
                        else if (carts_cr == "cart blood pow 1")
                            cart_create = "cart blood pow 2";
                        else if (carts_cr == "cart rock pow 1")
                            cart_create = "cart rock pow 2";
                        else if (carts_cr == "cart ice pow 1")
                            cart_create = "cart ice pow 2";
                        else if (carts_cr == "cart energy pow 1")
                            cart_create = "cart energy pow 2";

                        if (carts_cr == "cart att cuzna 1")
                            cart_create = "cart att cuzna 2";

                        if (carts_cr == "cart bar cuzna 1")
                            cart_create = "cart bar cuzna 2";
                        if (carts_cr == "cart bomb cuzna 1")
                            cart_create = "cart bomb cuzna 2";
                        if (carts_cr == "cart dych cuzna 1")
                            cart_create = "cart dych cuzna 2";

                        else if (carts_cr == "cart fire cuzna 1")
                            cart_create = "cart fire cuzna 2";
                        else if (carts_cr == "cart poition cuzna 1")
                            cart_create = "cart poition cuzna 2";
                        else if (carts_cr == "cart blood cuzna 1")
                            cart_create = "cart blood cuzna 2";
                        else if (carts_cr == "cart rock cuzna 1")
                            cart_create = "cart rock cuzna 2";
                        else if (carts_cr == "cart ice cuzna 1")
                            cart_create = "cart ice cuzna 2";
                        else if (carts_cr == "cart energy cuzna 1")
                            cart_create = "cart energy cuzna 2";


                        if (carts_cr == "cart att bomb 1")
                            cart_create = "cart att bomb 2";

                        if (carts_cr == "cart bar bomb 1")
                            cart_create = "cart bar bomb 2";
                        if (carts_cr == "cart bomb bomb 1")
                            cart_create = "cart bomb bomb 2";
                        if (carts_cr == "cart dych bomb 1")
                            cart_create = "cart dych bomb 2";

                        else if (carts_cr == "cart fire bomb 1")
                            cart_create = "cart fire bomb 2";
                        else if (carts_cr == "cart poition bomb 1")
                            cart_create = "cart poition bomb 2";
                        else if (carts_cr == "cart blood bomb 1")
                            cart_create = "cart blood bomb 2";
                        else if (carts_cr == "cart rock bomb 1")
                            cart_create = "cart rock bomb 2";
                        else if (carts_cr == "cart ice bomb 1")
                            cart_create = "cart ice bomb 2";
                        else if (carts_cr == "cart energy bomb 1")
                            cart_create = "cart energy bomb 2";

                        else if (carts_cr == "cart fire heart")
                            cart_create = "cart spirit fire heart";
                        else if (carts_cr == "cart poition heart")
                            cart_create = "cart spirit poition heart";
                        else if (carts_cr == "cart blood heart")
                            cart_create = "cart spirit blood heart";
                        else if (carts_cr == "cart rock heart")
                            cart_create = "cart spirit rock heart";
                        else if (carts_cr == "cart ice heart")
                            cart_create = "cart spirit ice heart";
                        else if (carts_cr == "cart energy heart")
                            cart_create = "cart spirit energy heart";

                        else if (carts_cr == "cart mini mast fire")
                            cart_create = "cart mast fire";
                        else if (carts_cr == "cart mini mast poition")
                            cart_create = "cart mast poition";
                        else if (carts_cr == "cart mini mast blood")
                            cart_create = "cart mast blood";
                        else if (carts_cr == "cart mini mast rock")
                            cart_create = "cart mast rock";
                        else if (carts_cr == "cart mini mast ice")
                            cart_create = "cart mast ice";
                        else if (carts_cr == "cart mini mast energy")
                            cart_create = "cart mast energy";

                        else if (carts_cr == "cart berserk")
                            cart_create = "cart rage";
                        else if (carts_cr == "cart dop hill")
                            cart_create = "cart berserk boss";

                        else if (carts_cr == "cart mini spirit")
                            cart_create = "cart spirit";
                        else if (carts_cr == "cart mini shaman")
                            cart_create = "cart shaman boss";

                        else if (carts_cr == "cart universal")
                            cart_create = "cart universal 1";
                        else if (carts_cr == "cart elemental")
                            cart_create = "cart elemental 1";
                    }



                    // mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (cart_create != null)
                    {
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            if (cart.q > 0 && cart.q_max > 0 && !bl_el_cuzna)
                            {

                                if (cart.type == par_cart.type_cart.art_ice_cuzna && cart0 == par_cart.type_cart.ice)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_fire_cuzna && cart0 == par_cart.type_cart.fire)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_poition_cuzna && cart0 == par_cart.type_cart.poition)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_energy_cuzna && cart0 == par_cart.type_cart.energy)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_blood_cuzna && cart0 == par_cart.type_cart.blood)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_rock_cuzna && cart0 == par_cart.type_cart.rock)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_att_cuzna && cart0 == par_cart.type_cart.damage)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_bar_cuzna && cart0 == par_cart.type_cart.sp_barier)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_dych_cuzna && cart0 == par_cart.type_cart.sp_dych)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                                else if (cart.type == par_cart.type_cart.art_bomb_cuzna && cart0 == par_cart.type_cart.sp_bomb)
                                {
                                    //cart.q --;
                                    mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                    bl_el_cuzna = true;
                                    //break;
                                }
                            }

                        }

                        spawn_cart(cart_create, gameObject.transform.Find("carts active").gameObject);
                    }
                    bl_att = true;
                }
            }

            else if ((lt_carts_active_now_base[0] == "cart cuznec" || lt_carts_active_now_base[0] == "cart cuznec 1") && lt_carts_active_now.Count == 3 && lt_carts_active_now[1] == lt_carts_active_now[2])
            {

                //func.mob[x, y].GetComponent<par_mob>().add_baf(par_baf.type_baf.bar, -1, 1);

                string cart_create = null;
                string carts_cr = lt_carts_active_now_base[1];
                par_cart.type_cart cart0 = GameObject.Find(lt_carts_active_now[1]).GetComponent<par_cart>().type;

                bool bl_el_cuzna = false;


                if (lt_carts_active_now_base[0] == "cart cuznec" || lt_carts_active_now_base[0] == "cart cuznec 1")
                {
                    if (carts_cr == "cart cuznec")
                        cart_create = "cart cuznec 1";

                    if (carts_cr == "cart damage")
                        cart_create = "cart damage 1";
                    if (carts_cr == "cart barier")
                        cart_create = "cart barier 1";

                    if (carts_cr == "cart bomb")
                        cart_create = "cart bomb 1";


                    if (carts_cr == "cart fire")
                        cart_create = "cart fire 1";
                    else if (carts_cr == "cart rock")
                        cart_create = "cart rock 1";
                    else if (carts_cr == "cart blood")
                        cart_create = "cart blood 1";
                    else if (carts_cr == "cart poition")
                        cart_create = "cart poition 1";
                    else if (carts_cr == "cart ice")
                        cart_create = "cart ice 1";
                    else if (carts_cr == "cart energy")
                        cart_create = "cart energy 1";

                    else if (carts_cr == "cart sp atack")
                        cart_create = "cart sp atack 1";

                    else if (carts_cr == "cart sp barier")
                        cart_create = "cart sp barier 1";
                    else if (carts_cr == "cart sp bomb")
                        cart_create = "cart sp bomb 1";
                    else if (carts_cr == "cart sp dych")
                        cart_create = "cart sp dych 1";

                    else if (carts_cr == "cart sp fire")
                        cart_create = "cart sp fire 1";
                    else if (carts_cr == "cart sp rock")
                        cart_create = "cart sp rock 1";
                    else if (carts_cr == "cart sp blood")
                        cart_create = "cart sp blood 1";
                    else if (carts_cr == "cart sp poition")
                        cart_create = "cart sp poition 1";
                    else if (carts_cr == "cart sp ice")
                        cart_create = "cart sp ice 1";
                    else if (carts_cr == "cart sp energy")
                        cart_create = "cart sp energy 1";


                    if (carts_cr == "cart act att")
                        cart_create = "cart act att 1";

                    if (carts_cr == "cart act bar")
                        cart_create = "cart act bar 1";
                    if (carts_cr == "cart act bomb")
                        cart_create = "cart act bomb 1";
                    if (carts_cr == "cart act dych")
                        cart_create = "cart act dych 1";

                    else if (carts_cr == "cart act fire")
                        cart_create = "cart act fire 1";
                    else if (carts_cr == "cart act rock")
                        cart_create = "cart act rock 1";
                    else if (carts_cr == "cart act blood")
                        cart_create = "cart act blood 1";
                    else if (carts_cr == "cart act poition")
                        cart_create = "cart act poition 1";
                    else if (carts_cr == "cart act ice")
                        cart_create = "cart act ice 1";
                    else if (carts_cr == "cart act energy")
                        cart_create = "cart act energy 1";

                    if (carts_cr == "cart speed att")
                        cart_create = "cart speed att 1";

                    if (carts_cr == "cart speed bar")
                        cart_create = "cart speed bar 1";
                    if (carts_cr == "cart speed bomb")
                        cart_create = "cart speed bomb 1";
                    if (carts_cr == "cart speed dych")
                        cart_create = "cart speed dych 1";

                    if (carts_cr == "cart speed fire")
                        cart_create = "cart speed fire 1";
                    else if (carts_cr == "cart speed rock")
                        cart_create = "cart speed rock 1";
                    else if (carts_cr == "cart speed blood")
                        cart_create = "cart speed blood 1";
                    else if (carts_cr == "cart speed poition")
                        cart_create = "cart speed poition 1";
                    else if (carts_cr == "cart speed ice")
                        cart_create = "cart speed ice 1";
                    else if (carts_cr == "cart speed energy")
                        cart_create = "cart speed energy 1";



                    if (carts_cr == "cart att pow")
                        cart_create = "cart att pow 1";

                    if (carts_cr == "cart bar pow")
                        cart_create = "cart bar pow 1";
                    if (carts_cr == "cart bomb pow")
                        cart_create = "cart bomb pow 1";
                    if (carts_cr == "cart dych pow")
                        cart_create = "cart dych pow 1";

                    else if (carts_cr == "cart fire pow")
                        cart_create = "cart fire pow 1";
                    else if (carts_cr == "cart poition pow")
                        cart_create = "cart poition pow 1";
                    else if (carts_cr == "cart blood pow")
                        cart_create = "cart blood pow 1";
                    else if (carts_cr == "cart rock pow")
                        cart_create = "cart rock pow 1";
                    else if (carts_cr == "cart ice pow")
                        cart_create = "cart ice pow 1";
                    else if (carts_cr == "cart energy pow")
                        cart_create = "cart energy pow 1";

                    if (carts_cr == "cart att cuzna")
                        cart_create = "cart att cuzna 1";

                    if (carts_cr == "cart bar cuzna")
                        cart_create = "cart bar cuzna 1";
                    if (carts_cr == "cart bomb cuzna")
                        cart_create = "cart bomb cuzna 1";
                    if (carts_cr == "cart dych cuzna")
                        cart_create = "cart dych cuzna 1";

                    else if (carts_cr == "cart fire cuzna")
                        cart_create = "cart fire cuzna 1";
                    else if (carts_cr == "cart poition cuzna")
                        cart_create = "cart poition cuzna 1";
                    else if (carts_cr == "cart blood cuzna")
                        cart_create = "cart blood cuzna 1";
                    else if (carts_cr == "cart rock cuzna")
                        cart_create = "cart rock cuzna 1";
                    else if (carts_cr == "cart ice cuzna")
                        cart_create = "cart ice cuzna 1";
                    else if (carts_cr == "cart energy cuzna")
                        cart_create = "cart energy cuzna 1";


                    if (carts_cr == "cart att bomb")
                        cart_create = "cart att bomb 1";

                    if (carts_cr == "cart bar bomb")
                        cart_create = "cart bar bomb 1";
                    if (carts_cr == "cart bomb bomb")
                        cart_create = "cart bomb bomb 1";
                    if (carts_cr == "cart dych bomb")
                        cart_create = "cart dych bomb 1";

                    else if (carts_cr == "cart fire bomb")
                        cart_create = "cart fire bomb 1";
                    else if (carts_cr == "cart poition bomb")
                        cart_create = "cart poition bomb 1";
                    else if (carts_cr == "cart blood bomb")
                        cart_create = "cart blood bomb 1";
                    else if (carts_cr == "cart rock bomb")
                        cart_create = "cart rock bomb 1";
                    else if (carts_cr == "cart ice bomb")
                        cart_create = "cart ice bomb 1";
                    else if (carts_cr == "cart energy bomb")
                        cart_create = "cart energy bomb 1";

                }
                if (lt_carts_active_now_base[0] == "cart cuznec 1" && cart_create == null)
                {
                    if (carts_cr == "cart damage 1")
                        cart_create = "cart damage 2";

                    if (carts_cr == "cart barier 1")
                        cart_create = "cart barier 2";
                    if (carts_cr == "cart bomb 1")
                        cart_create = "cart bomb 2";
                    if (carts_cr == "cart dych 1")
                        cart_create = "cart dych 2";

                    if (carts_cr == "cart fire 1")
                        cart_create = "cart fire 2";
                    else if (carts_cr == "cart rock 1")
                        cart_create = "cart rock 2";
                    else if (carts_cr == "cart blood 1")
                        cart_create = "cart blood 2";
                    else if (carts_cr == "cart poition 1")
                        cart_create = "cart poition 2";
                    else if (carts_cr == "cart ice 1")
                        cart_create = "cart ice 2";
                    else if (carts_cr == "cart energy 1")
                        cart_create = "cart energy 2";



                    else if (carts_cr == "cart sp atack 1")
                        cart_create = "cart sp atack 2";

                    else if (carts_cr == "cart sp barier 1")
                        cart_create = "cart sp barier 2";
                    else if (carts_cr == "cart sp bomb 1")
                        cart_create = "cart sp bomb 2";
                    else if (carts_cr == "cart sp dych 1")
                        cart_create = "cart sp dych 2";

                    else if (carts_cr == "cart sp fire 1")
                        cart_create = "cart sp fire 2";
                    else if (carts_cr == "cart sp rock 1")
                        cart_create = "cart sp rock 2";
                    else if (carts_cr == "cart sp blood 1")
                        cart_create = "cart sp blood 2";
                    else if (carts_cr == "cart sp poition 1")
                        cart_create = "cart sp poition 2";
                    else if (carts_cr == "cart sp ice 1")
                        cart_create = "cart sp ice 2";
                    else if (carts_cr == "cart sp energy 1")
                        cart_create = "cart sp energy 2";


                    if (carts_cr == "cart act att 1")
                        cart_create = "cart act att 2";

                    if (carts_cr == "cart act bar 1")
                        cart_create = "cart act bar 2";
                    if (carts_cr == "cart act bomb 1")
                        cart_create = "cart act bomb 2";
                    if (carts_cr == "cart act dych 1")
                        cart_create = "cart act dych 2";

                    else if (carts_cr == "cart act fire 1")
                        cart_create = "cart act fire 2";
                    else if (carts_cr == "cart act rock 1")
                        cart_create = "cart act rock 2";
                    else if (carts_cr == "cart act blood 1")
                        cart_create = "cart act blood 2";
                    else if (carts_cr == "cart act poition 1")
                        cart_create = "cart act poition 2";
                    else if (carts_cr == "cart act ice 1")
                        cart_create = "cart act ice 2";
                    else if (carts_cr == "cart act energy 1")
                        cart_create = "cart act energy 2";

                    if (carts_cr == "cart speed att 1")
                        cart_create = "cart speed att 2";

                    if (carts_cr == "cart speed bar 1")
                        cart_create = "cart speed bar 2";
                    if (carts_cr == "cart speed bomb 1")
                        cart_create = "cart speed bomb 2";
                    if (carts_cr == "cart speed dych 1")
                        cart_create = "cart speed dych 2";

                    if (carts_cr == "cart speed fire 1")
                        cart_create = "cart speed fire 2";
                    else if (carts_cr == "cart speed rock 1")
                        cart_create = "cart speed rock 2";
                    else if (carts_cr == "cart speed blood 1")
                        cart_create = "cart speed blood 2";
                    else if (carts_cr == "cart speed poition 1")
                        cart_create = "cart speed poition 2";
                    else if (carts_cr == "cart speed ice 1")
                        cart_create = "cart speed ice 2";
                    else if (carts_cr == "cart speed energy 1")
                        cart_create = "cart speed energy 2";



                    if (carts_cr == "cart att pow 1")
                        cart_create = "cart att pow 2";

                    if (carts_cr == "cart bar pow 1")
                        cart_create = "cart bar pow 2";
                    if (carts_cr == "cart bomb pow 1")
                        cart_create = "cart bomb pow 2";
                    if (carts_cr == "cart dych pow 1")
                        cart_create = "cart dych pow 2";

                    else if (carts_cr == "cart fire pow 1")
                        cart_create = "cart fire pow 2";
                    else if (carts_cr == "cart poition pow 1")
                        cart_create = "cart poition pow 2";
                    else if (carts_cr == "cart blood pow 1")
                        cart_create = "cart blood pow 2";
                    else if (carts_cr == "cart rock pow 1")
                        cart_create = "cart rock pow 2";
                    else if (carts_cr == "cart ice pow 1")
                        cart_create = "cart ice pow 2";
                    else if (carts_cr == "cart energy pow 1")
                        cart_create = "cart energy pow 2";

                    if (carts_cr == "cart att cuzna 1")
                        cart_create = "cart att cuzna 2";

                    if (carts_cr == "cart bar cuzna 1")
                        cart_create = "cart bar cuzna 2";
                    if (carts_cr == "cart bomb cuzna 1")
                        cart_create = "cart bomb cuzna 2";
                    if (carts_cr == "cart dych cuzna 1")
                        cart_create = "cart dych cuzna 2";

                    else if (carts_cr == "cart fire cuzna 1")
                        cart_create = "cart fire cuzna 2";
                    else if (carts_cr == "cart poition cuzna 1")
                        cart_create = "cart poition cuzna 2";
                    else if (carts_cr == "cart blood cuzna 1")
                        cart_create = "cart blood cuzna 2";
                    else if (carts_cr == "cart rock cuzna 1")
                        cart_create = "cart rock cuzna 2";
                    else if (carts_cr == "cart ice cuzna 1")
                        cart_create = "cart ice cuzna 2";
                    else if (carts_cr == "cart energy cuzna 1")
                        cart_create = "cart energy cuzna 2";


                    if (carts_cr == "cart att bomb 1")
                        cart_create = "cart att bomb 2";

                    if (carts_cr == "cart bar bomb 1")
                        cart_create = "cart bar bomb 2";
                    if (carts_cr == "cart bomb bomb 1")
                        cart_create = "cart bomb bomb 2";
                    if (carts_cr == "cart dych bomb 1")
                        cart_create = "cart dych bomb 2";

                    else if (carts_cr == "cart fire bomb 1")
                        cart_create = "cart fire bomb 2";
                    else if (carts_cr == "cart poition bomb 1")
                        cart_create = "cart poition bomb 2";
                    else if (carts_cr == "cart blood bomb 1")
                        cart_create = "cart blood bomb 2";
                    else if (carts_cr == "cart rock bomb 1")
                        cart_create = "cart rock bomb 2";
                    else if (carts_cr == "cart ice bomb 1")
                        cart_create = "cart ice bomb 2";
                    else if (carts_cr == "cart energy bomb 1")
                        cart_create = "cart energy bomb 2";

                    else if (carts_cr == "cart fire heart")
                        cart_create = "cart spirit fire heart";
                    else if (carts_cr == "cart poition heart")
                        cart_create = "cart spirit poition heart";
                    else if (carts_cr == "cart blood heart")
                        cart_create = "cart spirit blood heart";
                    else if (carts_cr == "cart rock heart")
                        cart_create = "cart spirit rock heart";
                    else if (carts_cr == "cart ice heart")
                        cart_create = "cart spirit ice heart";
                    else if (carts_cr == "cart energy heart")
                        cart_create = "cart spirit energy heart";

                    else if (carts_cr == "cart mini mast fire")
                        cart_create = "cart mast fire";
                    else if (carts_cr == "cart mini mast poition")
                        cart_create = "cart mast poition";
                    else if (carts_cr == "cart mini mast blood")
                        cart_create = "cart mast blood";
                    else if (carts_cr == "cart mini mast rock")
                        cart_create = "cart mast rock";
                    else if (carts_cr == "cart mini mast ice")
                        cart_create = "cart mast ice";
                    else if (carts_cr == "cart mini mast energy")
                        cart_create = "cart mast energy";

                    else if (carts_cr == "cart berserk")
                        cart_create = "cart rage";
                    else if (carts_cr == "cart dop hill")
                        cart_create = "cart berserk boss";

                    else if (carts_cr == "cart mini spirit")
                        cart_create = "cart spirit";
                    else if (carts_cr == "cart mini shaman")
                        cart_create = "cart shaman boss";

                    else if (carts_cr == "cart universal")
                        cart_create = "cart universal 1";
                    else if (carts_cr == "cart elemental")
                        cart_create = "cart elemental 1";
                }


                // mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (cart_create != null)
                {
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {
                        if (cart.q > 0 && cart.q_max > 0 && !bl_el_cuzna)
                        {

                            if (cart.type == par_cart.type_cart.art_ice_cuzna && cart0 == par_cart.type_cart.ice)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_fire_cuzna && cart0 == par_cart.type_cart.fire)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_poition_cuzna && cart0 == par_cart.type_cart.poition)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_energy_cuzna && cart0 == par_cart.type_cart.energy)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_blood_cuzna && cart0 == par_cart.type_cart.blood)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_rock_cuzna && cart0 == par_cart.type_cart.rock)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_att_cuzna && cart0 == par_cart.type_cart.damage)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_bar_cuzna && cart0 == par_cart.type_cart.sp_barier)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_dych_cuzna && cart0 == par_cart.type_cart.sp_dych)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                            else if (cart.type == par_cart.type_cart.art_bomb_cuzna && cart0 == par_cart.type_cart.sp_bomb)
                            {
                                //cart.q --;
                                mb.activate_art(cart, -1, -1, lt_carts_active_now_base[0]);
                                bl_el_cuzna = true;
                                //break;
                            }
                        }

                    }

                    if (data.data_now.col_carts_cuzna < 20)
                    {
                        data.data_now.col_carts_cuzna++;
                        if (data.data_now.col_carts_cuzna == 20)
                        {
                            //data.data_now.lt_players.Add(data.players.blood);
                            data.give_achive("Кузнец");

                        }
                    }


                    spawn_cart(cart_create, gameObject.transform.Find("carts active").gameObject);
                    bl_att = true;
                }


            }
            else if (par_cart_shaman > 0 && !bl_spirit)
            {


                mb.effect_now = func.create_effect("spirit base", x_cl, y_cl, mb.gameObject, false, pow_rez+par_cart_shaman, lt_carts_active_now_base);
                bl_att = true;
            }
            else if ((lt_carts_active_now_base[0] == "cart dych" || lt_carts_active_now_base[0] == "cart dych 1" || lt_carts_active_now_base[0] == "cart dych 2") && lt_carts_active_now_base.Count >1)
            {

                //List<string> lt_st = new List<string>(lt_carts_active_now_base);
                //lt_st.Re
                mb.effect_now = func.create_effect("spawn dych", x_cl, y_cl, mb.gameObject, false, pow_rez-1, lt_carts_active_now_base);
                mb.effect_now.transform.Find("empty").gameObject.GetComponent<SpriteRenderer>().color = anim_color.static_colors[func.baza[lt_carts_active_now_base[1]].GetComponent<par_cart>().type];
                bl_att = true;
            }
            else if ((lt_carts_active_now_base[0] == "cart bomb"|| lt_carts_active_now_base[0] == "cart bomb 1" || lt_carts_active_now_base[0] == "cart bomb 2") && lt_carts_active_now_base.Count > 1)
            {

                if (func.nature[x_cl, y_cl]==null)
                {
                    mb.effect_now = func.create_effect(lt_carts_active_now_base[0], x_cl, y_cl, mb.gameObject, false, pow_rez-1, lt_carts_active_now_base);
                    mb.effect_activate = lt_carts_active_now_base[0];
                    mb.effect_now.GetComponent<SpriteRenderer>().color = anim_color.static_colors[func.baza[lt_carts_active_now_base[1]].GetComponent<par_cart>().type];
                    bl_att = true;

                    //mb.effect_now = func.create_effect(lt_carts_active_now_base[0], x_cl, y_cl, mb.gameObject, false, pow_rez, lt_carts_active_now_base);
                    //mb.effect_activate = lt_carts_active_now_base[0];


                    //mb.lt_carts_active_now.RemoveAt(0);


                    //bl_att = true;
                }

            }

            else if (lt_carts_active_now_base.Count == 1 && !bl_spirit)
            {
                //bool bl_first = true;
                par_cart.type_cart tpp = GameObject.Find(lt_carts_active_now_base[0]).GetComponent<par_cart>().type;
                //bool bl_sp=(lt_carts_active_now_base[0] == "cart dych" || lt_carts_active_now_base[0] == "cart dych 1" || lt_carts_active_now_base[0] == "cart dych 2")||
                //par_cart
                /*
                foreach (par_cart.cart_combinations.comb_cart ct in par_cart.cart_combinations.lt_comb)
                {
                    if (ct.name == lt_carts_active_now_base[0])
                    {
                        if (ct.next.Count == 0)
                            bl_first = true;

                        //now1 = ct;
                        //break;
                    }


                }
                */
                if (tpp!=par_cart.type_cart.sp_cuznec && tpp != par_cart.type_cart.sp_bomb && tpp != par_cart.type_cart.sp_dych && tpp != par_cart.type_cart.shaman_boss)
                {
                    int pw = GameObject.Find(lt_carts_active_now_base[0]).GetComponent<par_cart>().lvl;

                    int dop_pow_rez = 1;

                    string bl_dych = null;
                    par_cart cart_bl_dych = null;
                    //bool bl_art_potoc = false;
                    if (bl_dych == null)
                    {
                        int pow_res_bool = pow_rez;
                        par_cart.type_cart type_cart0 = GameObject.Find(lt_carts_active_now_base[0]).GetComponent<par_cart>().type;
                        if (type_cart0 == par_cart.type_cart.derlorg)
                            pow_res_bool = pow_rez_seal;
                        mb.effect_now = func.create_effect(lt_carts_active_now_base[0], x_cl, y_cl, mb.gameObject, false, pow_res_bool * dop_pow_rez, lt_carts_active_now_base);

                        Debug.Log(type_cart0);
                        //bl_art_potoc = (type_cart0 == par_cart.type_cart.damage || type_cart0 == par_cart.type_cart.sp_barier || type_cart0 == par_cart.type_cart.derlorg);
                    }
                    else
                    {
                        mb.effect_now = func.create_effect(bl_dych, x_cl, y_cl, mb.gameObject, false, pow_rez * dop_pow_rez, lt_carts_active_now_base);


                        //mb.activate_art(cart_bl_dych, x_cl, y_cl);
                    }

                    //Debug.Log(effect_now ==null);
                    //mb.effect_now.GetComponent<par_effect>().parent = ;
                    mb.effect_activate = lt_carts_active_now_base[0];
                    bl_att = true;
                }

                //func.next_hod();
            }


            else if (lt_carts_active_now_base[0] == "cart derlorg" || lt_carts_active_now_base[0] == "cart derlorg 1" || lt_carts_active_now_base[0] == "cart derlorg 2")
            {
                //Debug.Log("cart atack is activated");
                string nm = null;
                string cart1 = lt_carts_active_now_base[1];
                //par_baf.type_baf type_bf = par_baf.type_baf.bar;
                List<string> lt_ = new List<string>(lt_carts_active_now_base);
                lt_.RemoveAt(0);
                //nm = func.get_name_effect(lt_, null);

                //par_baf.type_baf type = GameObject.Find(cart1).GetComponent<par_cart>().type
                int lv = GameObject.Find(cart1).GetComponent<par_cart>().lvl;
                //int lv1 = GameObject.Find(cart1).GetComponent<par_cart>().lvl;
                mb.effect_now = func.create_effect(lt_carts_active_now_base[0], x_cl, y_cl, mb.gameObject, false, pow_rez_seal, lt_carts_active_now_base);
                mb.effect_now.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                mb.effect_now.GetComponent<par_effect>().st_par.Push("par pow");
                //Debug.Log(effect_now ==null);
                //mb.effect_now.GetComponent<par_effect>().parent = ;
                mb.effect_activate = lt_carts_active_now_base[0];
                bl_att = true;
                //anim.col_anim++;
                //func.next_hod();
            }


            else if (bl_spirit)
            {
                //Debug.Log("tuman");
                //par_cart cart1 = GameObject.Find(lt_carts_active_now_base[1]).GetComponent<par_cart>();


                mb.effect_now = func.create_effect("spirit base", x_cl, y_cl, mb.gameObject, false, pow_rez + par_cart_shaman, lt_carts_active_now_base);
                bl_att = true;
                //func.next_hod();
            }
            else if (lt_carts_active_now_base.Count > 1)
            {
                mb.effect_now = func.create_effect(func.get_name_effect(lt_carts_active_now_base, null), x_cl, y_cl, mb.gameObject, false, pow_rez, lt_carts_active_now_base);
                bl_att = true;
            }
            if (bl_att)
            {
                int pow_crystall = 0;
                foreach (string art in lt_art_base.Keys)
                {

                    if (art == "cart poition touch")
                    {
                        //bl_art = true;
                        func.create_effect("cart poition", x_cl, y_cl, mb.gameObject, false, lt_art_base[art], new List<string> { "cart poition" });
                    }
                    else if (art == "cart fire touch")
                    {
                        func.create_effect("cart fire", x_cl, y_cl, mb.gameObject, false, lt_art_base[art], new List<string> { "cart fire" });
                    }
                    else if (art == "cart rock touch")
                    {
                        func.create_effect("cart rock", x_cl, y_cl, mb.gameObject, false, lt_art_base[art], new List<string> { "cart rock" });
                    }
                    else if (art == "cart blood touch")
                    {
                        func.create_effect("cart blood", x_cl, y_cl, mb.gameObject, false, lt_art_base[art], new List<string> { "cart blood" });
                    }
                    else if (art == "cart energy touch")
                    {
                        func.create_effect("cart energy", x_cl, y_cl, mb.gameObject, false, lt_art_base[art], new List<string> { "cart energy" });
                    }
                    else if (art == "cart ice touch")
                    {
                        func.create_effect("cart ice", x_cl, y_cl, mb.gameObject, false, lt_art_base[art], new List<string> { "cart ice" });
                    }

                    else if (art == "cart mast" || art == "cart derlorg st")
                    {
                        foreach (par_cart_art cart_ in mb.lt_carts_art)
                        {
                            if (cart_.tp_perez == par_cart_art.type_perez.standart)
                                cart_.perez_now -= lt_art_base[art];
                            if (cart_.perez_now < 0)
                                cart_.perez_now = 0;


                        }

                    }

                    else if (art == "cart fire st")
                    {
                        foreach (par_cart_art cart_ in mb.lt_carts_art)
                        {
                            if (cart_.tp_perez == par_cart_art.type_perez.standart)
                                cart_.perez_now -= lt_art_base[art];
                            if (cart_.perez_now < 0)
                                cart_.perez_now = 0;


                        }
                    }
                    else if (art == "cart rock st")
                    {
                        foreach (par_cart_art cart_ in mb.lt_carts_art)
                        {
                            if (cart_.tp_perez == par_cart_art.type_perez.standart)
                                cart_.perez_now -= lt_art_base[art];
                            if (cart_.perez_now < 0)
                                cart_.perez_now = 0;


                        }

                    }
                    else if (art == "cart blood st")
                    {
                        foreach (par_cart_art cart_ in mb.lt_carts_art)
                        {
                            if (cart_.tp_perez == par_cart_art.type_perez.standart)
                                cart_.perez_now -= lt_art_base[art];
                            if (cart_.perez_now < 0)
                                cart_.perez_now = 0;


                        }

                    }
                    else if (art == "cart energy st")
                    {
                        foreach (par_cart_art cart_ in mb.lt_carts_art)
                        {
                            if (cart_.tp_perez == par_cart_art.type_perez.standart)
                                cart_.perez_now -= lt_art_base[art];
                            if (cart_.perez_now < 0)
                                cart_.perez_now = 0;


                        }

                    }
                    else if (art == "cart ice st")
                    {
                        foreach (par_cart_art cart_ in mb.lt_carts_art)
                        {
                            if (cart_.tp_perez == par_cart_art.type_perez.standart)
                                cart_.perez_now -= lt_art_base[art];
                            if (cart_.perez_now < 0)
                                cart_.perez_now = 0;


                        }

                    }
                    else if (art == "cart poition st")
                    {
                        foreach (par_cart_art cart_ in mb.lt_carts_art)
                        {
                            if (cart_.tp_perez == par_cart_art.type_perez.standart)
                                cart_.perez_now -= lt_art_base[art];
                            if (cart_.perez_now < 0)
                                cart_.perez_now = 0;


                        }

                    }
                }
                foreach (par_cart_art art in lt_art_rem)
                {
                    //if (art.name=="cart mini spirit poition" || art.name == "cart mini spirit fire" || art.name == "cart mini spirit rock" || art.name == "cart mini spirit blood" || art.name == "cart mini spirit energy" || art.name == "cart mini spirit ice")
                    //{
                        activate_art(art, x, y);
                    //}
                    //if(art.name=="cart mini shaman"|| art.name == "cart shaman boss")
                        //activate_art(art,x_cl,y_cl);
                    //else
                        //activate_art(art);
                }


                List<par_cart_art> art_first = new List<par_cart_art>();
                foreach (par_cart_art art in lt_carts_art)
                {
                    if (art.perez_now == 0)
                    {
                        if (art.type == par_cart.type_cart.art_blood_bomb && (lt_carts_active_now_base.Contains("cart blood") || lt_carts_active_now_base.Contains("cart blood 1") || lt_carts_active_now_base.Contains("cart blood 2") || lt_carts_active_now_base.Contains("cart spirit blood")))
                        {
                            pow_crystall += art.lvl;
                            //lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl - 1, y_cl, gameObject, false, pow_crystall, lt_carts_active_now_base));

                            activate_art(art, x_cl, y_cl);
                        }

                        else if (art.type == par_cart.type_cart.art_rock_bomb && (lt_carts_active_now_base.Contains("cart rock") || lt_carts_active_now_base.Contains("cart rock 1") || lt_carts_active_now_base.Contains("cart rock 2") || lt_carts_active_now_base.Contains("cart spirit rock")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_energy_bomb && (lt_carts_active_now_base.Contains("cart energy") || lt_carts_active_now_base.Contains("cart energy 1") || lt_carts_active_now_base.Contains("cart energy 2") || lt_carts_active_now_base.Contains("cart spirit energy")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_ice_bomb && (lt_carts_active_now_base.Contains("cart ice") || lt_carts_active_now_base.Contains("cart ice 1") || lt_carts_active_now_base.Contains("cart ice 2") || lt_carts_active_now_base.Contains("cart spirit ice")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_poition_bomb && (lt_carts_active_now_base.Contains("cart poition") || lt_carts_active_now_base.Contains("cart poition 1") || lt_carts_active_now_base.Contains("cart poition 2") || lt_carts_active_now_base.Contains("cart spirit poition")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_fire_bomb && (lt_carts_active_now_base.Contains("cart fire") || lt_carts_active_now_base.Contains("cart fire 1") || lt_carts_active_now_base.Contains("cart fire 2") || lt_carts_active_now_base.Contains("cart spirit fire")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }

                        else if (art.type == par_cart.type_cart.art_att_bomb && (lt_carts_active_now_base.Contains("cart damage") || lt_carts_active_now_base.Contains("cart damage 1") || lt_carts_active_now_base.Contains("cart damage 2") ))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }

                        else if (art.type == par_cart.type_cart.art_bar_bomb && (lt_carts_active_now_base.Contains("cart barier") || lt_carts_active_now_base.Contains("cart barier 1") || lt_carts_active_now_base.Contains("cart barier 2")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_dych_bomb && (lt_carts_active_now_base.Contains("cart dych") || lt_carts_active_now_base.Contains("cart dych 1") || lt_carts_active_now_base.Contains("cart dych 2")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_bomb_bomb && (lt_carts_active_now_base.Contains("cart bomb") || lt_carts_active_now_base.Contains("cart bomb 1") || lt_carts_active_now_base.Contains("cart bomb 2")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }




                        else if (art.type == par_cart.type_cart.art_seal_blood  &&(lt_carts_active_now_base.Contains("cart blood") || lt_carts_active_now_base.Contains("cart blood 1") || lt_carts_active_now_base.Contains("cart blood 2") || lt_carts_active_now_base.Contains("cart spirit blood")))
                        {
                            //pow_crystall += art.lvl;
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }

                        else if (art.type == par_cart.type_cart.art_seal_rock  && (lt_carts_active_now_base.Contains("cart rock") || lt_carts_active_now_base.Contains("cart rock 1") || lt_carts_active_now_base.Contains("cart rock 2") || lt_carts_active_now_base.Contains("cart spirit rock")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_seal_energy  && (lt_carts_active_now_base.Contains("cart energy") || lt_carts_active_now_base.Contains("cart energy 1") || lt_carts_active_now_base.Contains("cart energy 2") || lt_carts_active_now_base.Contains("cart spirit energy")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_seal_ice  && (lt_carts_active_now_base.Contains("cart ice") || lt_carts_active_now_base.Contains("cart ice 1") || lt_carts_active_now_base.Contains("cart ice 2") || lt_carts_active_now_base.Contains("cart spirit ice")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_seal_poition && (lt_carts_active_now_base.Contains("cart poition") || lt_carts_active_now_base.Contains("cart poition 1") || lt_carts_active_now_base.Contains("cart poition 2") || lt_carts_active_now_base.Contains("cart spirit poition")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_seal_fire  && (lt_carts_active_now_base.Contains("cart fire") || lt_carts_active_now_base.Contains("cart fire 1") || lt_carts_active_now_base.Contains("cart fire 2") || lt_carts_active_now_base.Contains("cart spirit fire")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }

                        else if (art.type == par_cart.type_cart.art_seal_att  && (lt_carts_active_now_base.Contains("cart damage") || lt_carts_active_now_base.Contains("cart damage 1") || lt_carts_active_now_base.Contains("cart damage 2")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }

                        else if (art.type == par_cart.type_cart.art_seal_bar && (lt_carts_active_now_base.Contains("cart barier") || lt_carts_active_now_base.Contains("cart barier 1") || lt_carts_active_now_base.Contains("cart barier 2")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }
                        /*
                        else if (art.type == par_cart.type_cart.art_dych_bomb && (lt_carts_active_now_base.Contains("cart dych") || lt_carts_active_now_base.Contains("cart dych 1") || lt_carts_active_now_base.Contains("cart dych 2")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }*/
                        else if (art.type == par_cart.type_cart.art_seal_bomb && (lt_carts_active_now_base.Contains("cart bomb") || lt_carts_active_now_base.Contains("cart bomb 1") || lt_carts_active_now_base.Contains("cart bomb 2")))
                        {
                            List<string> l = new List<string>();
                            //if()
                            l.Add("cart derlorg");
                            l.AddRange(lt_carts_active_now_base);
                            GameObject gm_s = func.create_effect("cart derlorg", x_cl, y_cl, mb.gameObject, false, 4, l);
                            lt_effect_now.Add(gm_s);
                            gm_s.GetComponent<par_effect>().st_par.Push((pow_rez).ToString());
                            gm_s.GetComponent<par_effect>().st_par.Push("par pow");
                            activate_art(art, x_cl, y_cl);
                        }


                        else if (art.name == "cart mast poition" && (lt_carts_active_now_base.Contains("cart poition") || lt_carts_active_now_base.Contains("cart poition 1") || lt_carts_active_now_base.Contains("cart poition 2") || lt_carts_active_now_base.Contains("cart spirit poition")))
                        {
                            if (art.perez_now == 0)
                            {
                                foreach (string cart in lt_carts_active_now)
                                {
                                    bool act = false;
                                    if (func.baza[cart].GetComponent<par_cart>().type==par_cart.type_cart.poition || func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.spirit_poition)
                                    {
                                        spawn_cart(cart, gameObject.transform.Find("carts active").gameObject);
                                        act = true;
                                    }
                                    if (act)
                                    {
                                        activate_art(art);

                                    }
                                }
                            }
                            else
                            {
                                if (lt_art_base.ContainsKey("cart mast"))
                                    art.perez_now -= (lt_art_base["cart mast"] + 1);
                                else
                                    art.perez_now--;
                                if (art.perez_now < 0)
                                    art.perez_now = 0;
                            }

                        }
                        else if (art.name == "cart mast fire" && (lt_carts_active_now_base.Contains("cart fire") || lt_carts_active_now_base.Contains("cart fire 1") || lt_carts_active_now_base.Contains("cart fire 2") || lt_carts_active_now_base.Contains("cart spirit fire")))
                        {
                            if (art.perez_now == 0)
                            {
                                bool act = false;
                                foreach (string cart in lt_carts_active_now)
                                {
                                    
                                    if (func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.fire || func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.spirit_fire)
                                    {
                                        spawn_cart(cart, gameObject.transform.Find("carts active").gameObject);
                                        act = true;
                                    }

                                }
                                if (act)
                                {
                                    activate_art(art);

                                }
                            }
                            else
                            {
                                if (lt_art_base.ContainsKey("cart mast"))
                                    art.perez_now -= (lt_art_base["cart mast"] + 1);
                                else
                                    art.perez_now--;
                                if (art.perez_now < 0)
                                    art.perez_now = 0;
                            }

                        }
                        else if (art.name == "cart mast rock" && (lt_carts_active_now_base.Contains("cart rock") || lt_carts_active_now_base.Contains("cart rock 1") || lt_carts_active_now_base.Contains("cart rock 2") || lt_carts_active_now_base.Contains("cart spirit rock")))
                        {
                            if (art.perez_now == 0)
                            {
                                bool act = false;
                                foreach (string cart in lt_carts_active_now)
                                {
                                    
                                    if (func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.rock || func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.spirit_rock)
                                    {
                                        spawn_cart(cart, gameObject.transform.Find("carts active").gameObject);
                                        act = true;
                                    }

                                }
                                if (act)
                                {
                                    activate_art(art);

                                }
                            }
                            else
                            {
                                if (lt_art_base.ContainsKey("cart mast"))
                                    art.perez_now -= (lt_art_base["cart mast"] + 1);
                                else
                                    art.perez_now--;
                                if (art.perez_now < 0)
                                    art.perez_now = 0;
                            }
                        }
                        else if (art.name == "cart mast blood" &&  (lt_carts_active_now_base.Contains("cart blood") || lt_carts_active_now_base.Contains("cart blood 1") || lt_carts_active_now_base.Contains("cart blood 2") || lt_carts_active_now_base.Contains("cart spirit blood")))
                        {
                            if (art.perez_now == 0)
                            {
                                bool act = false;
                                foreach (string cart in lt_carts_active_now)
                                {
                                    
                                    if (func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.blood || func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.spirit_blood)
                                    {
                                        spawn_cart(cart, gameObject.transform.Find("carts active").gameObject);
                                        act = true;
                                    }
                                }
                                if (act)
                                {
                                    activate_art(art);

                                }
                            }
                            else
                            {
                                if (lt_art_base.ContainsKey("cart mast"))
                                    art.perez_now -= (lt_art_base["cart mast"] + 1);
                                else
                                    art.perez_now--;
                                if (art.perez_now < 0)
                                    art.perez_now = 0;
                            }
                        }
                        else if (art.name == "cart mast energy" && (lt_carts_active_now_base.Contains("cart energy") || lt_carts_active_now_base.Contains("cart energy 1") || lt_carts_active_now_base.Contains("cart energy 2") || lt_carts_active_now_base.Contains("cart spirit energy")))
                        {
                            if (art.perez_now == 0)
                            {
                                bool act = false;
                                foreach (string cart in lt_carts_active_now)
                                {

                                    if (func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.energy || func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.spirit_energy)
                                    {
                                        spawn_cart(cart, gameObject.transform.Find("carts active").gameObject);
                                        act = true;
                                    }
                                }
                                if (act)
                                {
                                    activate_art(art);

                                }
                            }
                            else
                            {
                                if (lt_art_base.ContainsKey("cart mast"))
                                    art.perez_now -= (lt_art_base["cart mast"] + 1);
                                else
                                    art.perez_now--;
                                if (art.perez_now < 0)
                                    art.perez_now = 0;
                            }

                        }
                        else if (art.name == "cart mast ice" && (lt_carts_active_now_base.Contains("cart ice") || lt_carts_active_now_base.Contains("cart ice 1") || lt_carts_active_now_base.Contains("cart ice 2") || lt_carts_active_now_base.Contains("cart spirit ice")))
                        {
                            if (art.perez_now == 0)
                            {
                                bool act = false;
                                foreach (string cart in lt_carts_active_now)
                                {

                                    if (func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.ice || func.baza[cart].GetComponent<par_cart>().type == par_cart.type_cart.spirit_ice)
                                    {
                                        spawn_cart(cart, gameObject.transform.Find("carts active").gameObject);
                                        act = true;
                                    }
                                }
                                if (act)
                                {
                                    activate_art(art);

                                }
                            }
                            else
                            {
                                if (lt_art_base.ContainsKey("cart mast"))
                                    art.perez_now -= (lt_art_base["cart mast"]+1);
                                else
                                    art.perez_now--;
                                if (art.perez_now < 0)
                                    art.perez_now = 0;
                            }

                        }
                    }
                }
                if (pow_crystall > 0)
                {
                    par_effect eff = effect_now.GetComponent<par_effect>();
                    if (x_cl > 0)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl - 1, y_cl, gameObject, false, pow_crystall, lt_carts_active_now_base));
                    if (y_cl > 0)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl, y_cl - 1, gameObject, false, pow_crystall, lt_carts_active_now_base));
                    if (x_cl < func.size_global - 1)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl + 1, y_cl, gameObject, false, pow_crystall, lt_carts_active_now_base));
                    if (y_cl < func.size_global - 1)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl, y_cl + 1, gameObject, false, pow_crystall, lt_carts_active_now_base));
                }

                mb.gameObject.GetComponent<Animator>().SetBool("selected", false);
                mb.gameObject.GetComponent<Animator>().SetBool("select_now", false);

                mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                //anim.col_anim++;
                data.data_now.cl_carts_speed++;

                mb.lt_carts_active_now.Clear();
                //bool bl_art_boss_berserk = false;
                if (data.data_now.cl_carts_speed >= 4)
                {
                    data.give_achive("быстрее некуда");
                }
                if (col_sila >= 4)
                {
                    data.give_achive("тяжёлая рука");
                }

                Debug.Log("hod after atack " + bl_att_hod);
                if (!bl_att_hod)
                {
                    //timer_now = 0.1f;
                    //func.nums_mob_wolk.Add(num);
                    //func.nums_mob_wolk.Insert(func.nums_mob_wolk.IndexOf(num), num);
                    speed_hod = true;
                    
                }
                //func.next_hod();
                return true;
            }











            /*
            if (gameObject.name == "berserk player")
            {
                func.create_effect("berserk ef", x, y, mb.gameObject, true, 1, new List<string>());
                //dop_pow *=2;
                //mb.damage(1, "none");
            }
            */

        }
        return false;
        //func.test_message = "good";
    }


    public void wolk_mob()
    {

        int size = func.size_global;
        bool[,] bl_way = new bool[size, size];
        bool[,] bl_way_1 = new bool[size, size];

        //par_mob player = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
        par_mob player = null;
        List<GameObject> mb_lt_rd = new List<GameObject>();
        foreach (GameObject obj in lt_mob_vizited)
        {
            par_mob mb = obj.GetComponent<par_mob>();
            if (mb.friend != friend && !mb.nps)
            {
                mb_lt_rd.Add(obj);
            }





        }
        if (mb_lt_rd.Count > 0)
            player = mb_lt_rd[Random.Range(0, mb_lt_rd.Count - 1)].GetComponent<par_mob>();
        else if (parent != null)
        {
            player = parent.GetComponent<par_mob>();
        }

        if (player != null)
        {
            int xd = Mathf.Abs(x - player.x);
            int yd = Mathf.Abs(y - player.y);
            bool rd_wolk = true;

            //int dop_r = 2;
            if (xd + yd <= distanse_max && run_mob)
            {
                rd_wolk = false;
                int[,] way_;

                for (int i = y - distanse_max; i <= y + distanse_max; i++)
                {
                    if (i >= 0 && i < size)
                        for (int j = x - distanse_max; j <= x + distanse_max; j++)
                        {
                            if (j >= 0 && j < size)
                            {
                                bl_way[j, i] = (func.object_bool(j, i)) || (func.mob[j, i] == GameObject.Find(data.data_now.pl_name_now)) || (func.mob[j, i] == gameObject);
                                bl_way_1[j, i] = func.eath[j, i].active;
                            }
                            //if(func.way(x,y, distanse_max)[j,i]!=-1)

                            //Debug.Log(j+"  "+i+ "ways  " +bl_way[j,i]);
                        }
                }


                way_ = func.way_bool(bl_way, player.x, player.y, distanse_max);

                if (way_[x, y] != -1 && distanse_attack != -1)
                {
                    //int[,] way_ = func.way_bool(bl_way, player.x, player.y, distanse_max);
                    //Debug.Log("ways  " + way_[x, y]);
                    if (way_[x, y] > distanse_attack)
                    {
                        if (x > 0 && way_[x - 1, y] < way_[x, y] && way_[x - 1, y] != -1)
                            wolk(x - 1, y);
                        else if (x < func.size_global - 1 && way_[x + 1, y] < way_[x, y] && way_[x + 1, y] != -1)
                            wolk(x + 1, y);
                        else if (y > 0 && way_[x, y - 1] < way_[x, y] && way_[x, y - 1] != -1)
                            wolk(x, y - 1);
                        else if (y < func.size_global - 1 && way_[x, y + 1] < way_[x, y] && way_[x, y + 1] != -1)
                            wolk(x, y + 1);
                    }
                    else
                    {
                        wolk(x, y);
                    }
                }
                else if (distanse_attack != -1)
                {
                    way_ = func.way_bool(bl_way_1, player.x, player.y, distanse_max);
                    if (way_[x, y] > distanse_attack)
                    {
                        if (x > 0 && way_[x - 1, y] < way_[x, y] && way_[x - 1, y] != -1 && func.object_bool(x - 1, y))
                            wolk(x - 1, y);
                        else if (x < func.size_global - 1 && way_[x + 1, y] < way_[x, y] && way_[x + 1, y] != -1 && func.object_bool(x + 1, y))
                            wolk(x + 1, y);
                        else if (y > 0 && way_[x, y - 1] < way_[x, y] && way_[x, y - 1] != -1 && func.object_bool(x, y - 1))
                            wolk(x, y - 1);
                        else if (y < func.size_global - 1 && way_[x, y + 1] < way_[x, y] && way_[x, y + 1] != -1 && func.object_bool(x, y + 1))
                            wolk(x, y + 1);
                    }
                    else
                    {
                        wolk(x, y);
                    }
                }
            }
            else
            {
                rd_wolk = false;


                int[,] way_;
                /*
                List<object> lt = func.way_find_lt(func.mob, x, y, distanse_max);
                List<GameObject> mob_rd = new List<GameObject>();
                
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    if (!mb.friend && mb.num != num)
                        mob_rd.Add((GameObject)obj);
                }*/

                GameObject mb_end = null;
                par_mob mb_end_par = null;
                if (lt_mob_vizited.Count != 0)
                {
                    mb_end = func.rand_list(lt_mob_vizited);// mob_rd[Random.Range(0, mob_rd.Count)];
                    mb_end_par = mb_end.GetComponent<par_mob>();
                }
                if (mb_end_par != null)
                {
                    for (int i = y - distanse_max; i <= y + distanse_max; i++)
                    {
                        if (i >= 0 && i < size)
                            for (int j = x - distanse_max; j <= x + distanse_max; j++)
                            {
                                if (j >= 0 && j < size)
                                    //if(func.way(x,y, distanse_max)[j,i]!=-1)
                                    bl_way[j, i] = (func.object_bool(j, i)) || (func.mob[j, i] == mb_end) || (func.mob[j, i] == gameObject);
                                //Debug.Log(j+"  "+i+ "ways  " +bl_way[j,i]);
                            }
                    }

                    way_ = func.way_bool(bl_way, mb_end_par.x, mb_end_par.y, distanse_max);

                    if (way_[x, y] != -1 && distanse_attack != -1)
                    {
                        //int[,] way_ = func.way_bool(bl_way, player.x, player.y, distanse_max);
                        //Debug.Log("ways  " + way_[x, y]);
                        if (way_[x, y] > distanse_attack)
                        {
                            if (x > 0 && way_[x - 1, y] < way_[x, y] && way_[x - 1, y] != -1)
                                wolk(x - 1, y);
                            else if (x < func.size_global - 1 && way_[x + 1, y] < way_[x, y] && way_[x + 1, y] != -1)
                                wolk(x + 1, y);
                            else if (y > 0 && way_[x, y - 1] < way_[x, y] && way_[x, y - 1] != -1)
                                wolk(x, y - 1);
                            else if (y < func.size_global - 1 && way_[x, y + 1] < way_[x, y] && way_[x, y + 1] != -1)
                                wolk(x, y + 1);
                        }
                        else
                        {
                            wolk(x, y);
                        }
                    }
                }
                else
                {
                    for (int i = y - distanse_max; i <= y + distanse_max; i++)
                    {
                        if (i >= 0 && i < size)
                            for (int j = x - distanse_max; j <= x + distanse_max; j++)
                            {
                                if (j >= 0 && j < size)
                                    //if(func.way(x,y, distanse_max)[j,i]!=-1)
                                    bl_way[j, i] = (func.object_bool(j, i)) || (func.mob[j, i] == GameObject.Find(data.data_now.pl_name_now)) || (func.mob[j, i] == gameObject);
                                //Debug.Log(j+"  "+i+ "ways  " +bl_way[j,i]);
                            }
                    }


                    way_ = func.way_bool(bl_way, player.x, player.y, distanse_max);

                    if (x > 0 && way_[x - 1, y] > way_[x, y] && way_[x - 1, y] != -1)
                        wolk(x - 1, y);
                    else if (x < func.size_global - 1 && way_[x + 1, y] > way_[x, y] && way_[x + 1, y] != -1)
                        wolk(x + 1, y);
                    else if (y > 0 && way_[x, y - 1] > way_[x, y] && way_[x, y - 1] != -1)
                        wolk(x, y - 1);
                    else if (y < func.size_global - 1 && way_[x, y + 1] > way_[x, y] && way_[x, y + 1] != -1)
                        wolk(x, y + 1);
                    else
                    {
                        wolk(x, y);
                    }
                }

            }

            if (rd_wolk)
            {
                List<int> lt_rd = new List<int>() { 0, 1, 2, 3 };
                if (x == 0)
                    lt_rd.Remove(0);
                if (x == func.size_global - 1)
                    lt_rd.Remove(1);
                if (y == 0)
                    lt_rd.Remove(2);
                if (y == func.size_global - 1)
                    lt_rd.Remove(3);

                int a = lt_rd[Random.Range(0, lt_rd.Count)];
                if (a == 0)
                    wolk(x - 1, y);
                else if (a == 1)
                    wolk(x + 1, y);
                else if (a == 2)
                    wolk(x, y - 1);
                else if (a == 3)
                    wolk(x, y + 1);
                //func.next_hod();
            }
        }


        //func.next_hod();


    }
    /*
    public void create_effect(string nm)
    {
        GameObject gm = GameObject.Find(nm);
        parametrs par = cel.GetComponent<parametrs>();
        func.create_effect(gm, (parametrs)skill_mob_now, transform.position, cel.transform.position,new Vector2(par.x,par.y)) ;
    }
    */
    public void wolk_v2_bool()
    {

        Vector2 v2 = lt_wolk.Pop();
        int i = Mathf.Abs(x - (int)v2.x) + Mathf.Abs(y - (int)v2.y);
        //Debug.Log("wolk auto " + v2);
        if (i == 1 || i == 0)
        {
            if(num==0)
                ui.del_pricels(v2);
            if (wolk((int)v2.x, (int)v2.y))
            {

                if (lt_wolk.Count == 0 && num==0)
                    ui.clean_pricels("pricel");
                
            }
            else
            {
                if(num==0)
                    foreach (Vector2 v2_ in lt_wolk)
                    {
                        Debug.Log("del pricels " + v2_ + " " + num);
                        ui.del_pricels(v2_);
                    }
                lt_wolk.Clear();
            }

        }
        else
        {
            if(num==0)
                foreach (Vector2 v2_ in lt_wolk)
                {
                    ui.del_pricels(v2_);
                }
            lt_wolk.Clear();
        }
        if (lt_wolk.Count == 0 && action_after_wolk_now!=null)
        {
            Debug.Log("action after wolk is true");
            action_after_wolk_now.active = true;
        }

    }
    public void wolk_v2()
    {
        bool_func_wolk_v2 = true;
        if (anim.col_anim == 0)
        {
            wolk_v2_bool();
            bool_func_wolk_v2 = false;
        }
    }
    public void viz_block(int x_v, int y_v)
    {
        lt_v2_vizited.Add(new Vector2(x_v, y_v));
        if (friend)
            func.lt_v2_viz_friend[x_v, y_v].Add(gameObject);
        func.lt_v2_viz_mob[x_v, y_v].Add(gameObject);
        if (func.mob[x_v, y_v] != null && !(x_v == x && y_v == y))
        {
            par_mob mb_viz = func.mob[x_v, y_v].GetComponent<par_mob>();

            lt_mob_vizited.Add(func.mob[x_v, y_v]);
            mb_viz.lt_mob_vizited_in.Add(gameObject);

            int cc = Mathf.Abs((x_v - x)) + Mathf.Abs((y_v - y));
            if (cc <= mb_viz.distanse_max)
            {
                mb_viz.lt_mob_vizited.Add(gameObject);
                lt_mob_vizited_in.Add(func.mob[x_v, y_v]);
            }
        }
        if (friend && !(x_v == x && y_v == y))
        {

            if (func.nature[x_v, y_v] != null && !func.nature[x_v, y_v].active)
                func.nature[x_v, y_v].active = true;
            if (func.mob[x_v, y_v] != null && !func.mob[x_v, y_v].GetComponent<par_mob>().vizited)
                //func.mob[j, i].active = true;
                func.mob[x_v, y_v].GetComponent<par_mob>().viz(true);
            if (func.carts[x_v, y_v] != null && !func.carts[x_v, y_v].active)
                func.carts[x_v, y_v].active = true;
            if (!func.eath[x_v, y_v].active)
                func.eath[x_v, y_v].active = true;

        }


    }
    public void unviz_block(int x_v, int y_v)
    {
        lt_v2_vizited.Remove(new Vector2(x_v, y_v));
        if (friend)
            func.lt_v2_viz_friend[x_v, y_v].Remove(gameObject);
        func.lt_v2_viz_mob[x_v, y_v].Remove(gameObject);
        bool bl = false;

        if (func.mob[x_v, y_v] != null && !(x_v == x && y_v == y))
        {
            par_mob mb_viz = func.mob[x_v, y_v].GetComponent<par_mob>();

            lt_mob_vizited.Remove(func.mob[x_v, y_v]);
            mb_viz.lt_mob_vizited_in.Remove(gameObject);

            int cc = Mathf.Abs((x_v - x)) + Mathf.Abs((y_v - y));
            if (cc > mb_viz.distanse_max)
            {
                mb_viz.lt_mob_vizited.Remove(gameObject);
                lt_mob_vizited_in.Remove(func.mob[x_v, y_v]);
            }
        }

        if (friend && !(x_v == x && y_v == y) && func.lt_v2_viz_friend[x_v, y_v].Count == 0)
        {
            if (func.nature[x_v, y_v] != null && func.nature[x_v, y_v].active)
                func.nature[x_v, y_v].active = false;
            if (func.mob[x_v, y_v] != null && func.mob[x_v, y_v].GetComponent<par_mob>().vizited)
                //func.mob[j, i].active = true;
                func.mob[x_v, y_v].GetComponent<par_mob>().viz(false);
            if (func.carts[x_v, y_v] != null && func.carts[x_v, y_v].active)
                func.carts[x_v, y_v].active = false;
            if (func.eath[x_v, y_v].active)
                func.eath[x_v, y_v].active = false;
        }


    }
    public bool wolk(int x_wolk, int y_wolk)
    {
        return wolk(x_wolk, y_wolk, false);
    }
    public bool wolk(int x_wolk, int y_wolk, bool bl_wolk)
    {
        //Debug.Log("before mob coord" + num + " in " + x + " " + y);
        Debug.Log("wolk mob " + num + " in " + x_wolk + " " + y_wolk);
        if ((func.object_bool(x_wolk, y_wolk) || (x_wolk==x && y_wolk==y))|| bl_wolk)
        {
            //Debug.Log("wolk mob next");

            parametrs par = gameObject.GetComponent<parametrs>();


            foreach (GameObject gm in func.lt_v2_viz_mob[x, y])
            {
                if (gm != null)
                {
                    par_mob mb_viz = gm.GetComponent<par_mob>();
                    if (mb_viz.lt_mob_vizited.Contains(gameObject) && func.distance(x, y, mb_viz.x, mb_viz.y) > mb_viz.distanse_max && gameObject != gm)
                    {
                        mb_viz.lt_mob_vizited.Remove(gameObject);
                        lt_mob_vizited_in.Remove(gm);
                        if (mb_viz.friend && vizited)
                        {
                            viz(false);
                        }
                    }
                }

            }

            int x_b = par.x;
            int y_b = par.y;


            if (x_wolk > par.x)
                simm(1);
            else if (x_wolk < par.x)
                simm(-1);
            x = x_wolk;
            y = y_wolk;
            func.mob[x_b, y_b] = null;
            func.mob[x_wolk, y_wolk] = gameObject;
            //bl = func.recurs(bl, 0, 0);
            Dictionary<Vector2, List<GameObject>> lt_gm = new Dictionary<Vector2, List<GameObject>>();
            //List<GameObject>[,] gm_lt = new List<GameObject>[func.size_global, func.size_global];

            //bool 
            //if (friend)
            //{
            int[,] way_fr = func.way(x_b, y_b, distanse_max);



            int c_size = distanse_max * 2 + 1;
            int c_size_2 = distanse_max;

            int di = 1;
            int dj = 1;
            if (x_wolk < x_b)
                dj = -1;
            if (y_wolk < y_b)
                di = -1;

            bool i_br = false;

            int i_ = 0;
            bool i_bl = false;

            int c_way = Mathf.Abs((x_wolk - x_b)) + Mathf.Abs((y_wolk - y_b));
            bool[,] bl = new bool[func.size_global, func.size_global];
            for (int i = y_wolk - distanse_max; i <= y_wolk + distanse_max; i++)
            {
                if (i >= 0 && i < func.size_global)
                {
                    for (int j = x_wolk - distanse_max; j <= x_wolk + distanse_max; j++)
                    {
                        if (j >= 0 && j < func.size_global)
                        {
                            bl[j, i] = (func.nature[j, i] == null || func.nature[j, i].GetComponent<par_nat>().vizible);
                        }
                    }
                }
            }
            //c_way = Mathf.Abs((x_wolk-x_b)) + Mathf.Abs((y_wolk-y_b));
            int[,] way_fr_bl = func.way_bool_and(bl, x_wolk, y_wolk, distanse_max);


            for (int i = y_b - distanse_max * di; i != y_b + distanse_max * di + di; i += di)
            {
                //if (i >= 0 && i < func.size_global)
                //{



                for (int j = x_b - i_ * dj; j != x_b + i_ * dj + dj; j += dj)
                {
                    //if (j >= 0 && j < func.size_global)
                    //{
                    int cc = Mathf.Abs((x_wolk - j)) + Mathf.Abs((y_wolk - i));
                    bool bl_ji = (j >= 0 && j < func.size_global && i >= 0 && i < func.size_global);
                    int n_x = (x_b - j) + x_wolk;//n_x=10 x_b=5 x_wolk=6 =>>  j=1
                    int n_y = (y_b - i) + y_wolk;//n_y=26 y_b=25 y_wolk=25 =>>  i=24

                    bool bl_n = (n_x >= 0 && n_x < func.size_global && n_y >= 0 && n_y < func.size_global);
                    //if(Math.abs(x_wolk))

                    if (cc <= distanse_max)
                    {

                        if (bl_n && way_fr_bl[n_x, n_y] != -1 && !lt_v2_vizited.Contains(new Vector2(n_x, n_y)))
                        {
                            viz_block(n_x, n_y);
                        }
                        if (bl_ji && way_fr_bl[j, i] == -1 && lt_v2_vizited.Contains(new Vector2(j, i)))
                        {
                            unviz_block(j, i);
                        }
                        //if (j == x_b - i_ * dj)
                        //i_br = true;
                        //break;
                        //continue;
                    }

                    else
                    {


                        //if (n_x>=0 && n_x< func.size_global && n_y >= 0 && n_y < func.size_global)
                        //{
                        if (bl_n && way_fr_bl[n_x, n_y] != -1)
                        {
                            viz_block(n_x, n_y);
                        }


                        if (friend)
                        {
                        }



                        if (bl_ji)
                        {
                            unviz_block(j, i);

                        }
                        //}


                        /*
                        if (num == 0)
                        {
                            Debug.Log(j + " " + i + "    del:  " + n_x + " " + n_y + "   " + " " + bl_ji);
                            if (bl_ji)
                                Debug.Log(way_fr_bl[j, i]);
                            if (bl_n)
                                Debug.Log(way_fr_bl[n_x, n_y]);
                        }
                             */
                    }


                    //}
                }
                if (i_br)
                    break;
                //}
                if (!i_bl)
                {
                    i_++;
                    if (i_ == distanse_max)
                        i_bl = true;
                }

                else
                {
                    i_--;
                }
            }
            Debug.Log(lt_mob_vizited.Count);

            //if (num == 0)
                //GameObject.Find("bt up").GetComponent<Animator>().SetBool("opened", func.carts[x_wolk, y_wolk] != null);
            //}

            foreach (GameObject gm in func.lt_v2_viz_mob[x, y])
            {
                if (gm != null)
                {
                    par_mob mb_viz = gm.GetComponent<par_mob>();
                    Debug.Log("viz " + mb_viz.num + " " + func.lt_v2_viz_mob[x, y].Count);
                    if (!mb_viz.lt_mob_vizited.Contains(gameObject) && func.distance(x, y, mb_viz.x, mb_viz.y) <= mb_viz.distanse_max && gameObject != gm)
                    {
                        mb_viz.lt_mob_vizited.Add(gameObject);
                        lt_mob_vizited_in.Add(gm);
                        if (mb_viz.friend &&!vizited)
                        {
                            viz(true);
                        }

                    }
                }

            }


            //



            /*
            if (!friend && !nps)
            {
                par_mob pl = func.gm_wolk_friend.GetComponent<par_mob>();
                int c = func.way_bool_and(bl, pl.x, pl.y, pl.distanse_max)[x_wolk, y_wolk];
                if (c == -1 && vizited)
                {
                    viz(false);
                }
                else if (c != -1 && !vizited)
                {
                    viz(true);
                }
            }
            */
            //if (!(gameObject.transform.position.x < 0 || gameObject.transform.position.y < 0) && vizited)
            if (!(gameObject.transform.position.x < 0 || gameObject.transform.position.y < 0) )
            {
                if (!friend ||(friend && num==0))
                {
                    //time_wolk = 0;
                    time_wolk_now = 0;
                    d_v2_wolk = new Vector2(x_wolk - x_b, y_wolk - y_b) / time_wolk;
                    d_v2_wolk_end = new Vector2(x_wolk, y_wolk);
                    anim.col_anim++;
                }
                else
                {
                    time_wolk_now = 0;
                    //time_wolk = 0.05f;
                    d_v2_wolk = new Vector2(x_wolk - x_b, y_wolk - y_b) / time_wolk;
                    d_v2_wolk_end = new Vector2(x_wolk, y_wolk);
                    anim.col_anim++;
                }

            }
            else
            {
                gameObject.transform.position = new Vector3(x_wolk, y_wolk, 0);
            }
            //gameObject.GetComponent<SpriteRenderer>().sortingOrder = (6 - y_wolk + 1) * 2-1;
            //func.next_hod();

            /*

            if(gameObject.name == data.data_now.pl_name_now)
            {
                foreach (GameObject gm in lt_mob_vizited)
                {
                    par_mob mb = gm.GetComponent<par_mob>();
                    if (gm.name == "teacher" && func.distance(x, y, mb.x, mb.y) <= 2)
                        gm.GetComponent<par_dialog>().start_dialog("start");
                }
               
            }
            */
            return true;
        }
        else
        {
            return false;
            //func.next_hod();
        }
        //int x_r = func.x_r_now;
        //int y_r = func.y_r_now;
        //GameObject.Find("panel_deistvie").GetComponent<ui_deistvie>().updating();
    }

    public void update_viz()
    {
        Debug.Log("update viz " + num);
        bool[,] bl = new bool[func.size_global, func.size_global];
        for (int i = y - distanse_max; i <= y + distanse_max; i++)
        {
            if (i >= 0 && i < func.size_global)
            {
                for (int j = x - distanse_max; j <= x + distanse_max; j++)
                {
                    if (j >= 0 && j < func.size_global)
                    {
                        bl[j, i] = (func.nature[j, i] == null || func.nature[j, i].GetComponent<par_nat>().vizible);
                    }
                }
            }
        }
        int[,] way_fr_bl = func.way_bool_and(bl, x, y, distanse_max);

        foreach (Vector2 v2 in lt_v2_vizited)
        {
            int j = (int)v2.x;
            int i = (int)v2.y;
            if (friend)
                func.lt_v2_viz_friend[j, i].Remove(gameObject);
            func.lt_v2_viz_mob[j, i].Remove(gameObject);
            if (way_fr_bl[j, i] != -1 && func.lt_v2_viz_friend[j, i].Count == 0)
            {
                if (func.nature[j, i] != null && func.nature[j, i].active)
                    func.nature[j, i].active = false;
                if (func.mob[j, i] != null && func.mob[j, i] != gameObject && func.mob[j, i].GetComponent<par_mob>().vizited)
                    //func.mob[j, i].active = true;
                    func.mob[j, i].GetComponent<par_mob>().viz(false);
                if (func.carts[j, i] != null && func.carts[j, i].active)
                    func.carts[j, i].active = false;
                if (func.eath[j, i].active)
                    func.eath[j, i].active = false;
            }

        }
        foreach (GameObject gm in lt_mob_vizited)
        {
            if (gm != null)
            {
                par_mob mb_viz = gm.GetComponent<par_mob>();
                if (mb_viz.lt_mob_vizited_in.Contains(gameObject))
                {
                    mb_viz.lt_mob_vizited_in.Remove(gameObject);
                    //lt_mob_vizited_in.Remove(gm);
                }

            }

        }
        //can_atacked = false;
        lt_v2_vizited.Clear();
        lt_mob_vizited.Clear();
        //lt_mob_vizited.Clear();

        for (int i = y - distanse_max; i <= y + distanse_max; i++)
        {
            if (i >= 0 && i < func.size_global)
            {



                for (int j = x - distanse_max; j <= x + distanse_max; j++)
                {
                    if (j >= 0 && j < func.size_global)
                    {
                        int cc = 0;
                        //if(Math.abs(x_wolk))


                        int n_x = j;
                        int n_y = i;


                        if (way_fr_bl[n_x, n_y] != -1)
                        {
                            lt_v2_vizited.Add(new Vector2(n_x, n_y));
                            if (friend)
                                func.lt_v2_viz_friend[n_x, n_y].Add(gameObject);
                            func.lt_v2_viz_mob[n_x, n_y].Add(gameObject);
                            if (func.mob[n_x, n_y] != null && !(j == x && i == y))
                            {
                                par_mob mb_viz = func.mob[n_x, n_y].GetComponent<par_mob>();
                                lt_mob_vizited.Add(func.mob[n_x, n_y]);
                                mb_viz.lt_mob_vizited_in.Add(gameObject);
                                cc = Mathf.Abs((n_x - x)) + Mathf.Abs((n_y - y));
                                if (cc <= mb_viz.distanse_max && func.mob[n_x, n_y] != gameObject)
                                {
                                    mb_viz.lt_mob_vizited.Add(gameObject);
                                    lt_mob_vizited_in.Add(func.mob[n_x, n_y]);
                                }
                            }
                        }


                        if (friend)
                        {
                            if (way_fr_bl[n_x, n_y] != -1)
                            {
                                if (func.nature[n_x, n_y] != null && !func.nature[n_x, n_y].active)
                                    func.nature[n_x, n_y].active = true;
                                if (func.mob[n_x, n_y] != null && !func.mob[n_x, n_y].GetComponent<par_mob>().vizited)
                                    //func.mob[j, i].active = true;
                                    func.mob[n_x, n_y].GetComponent<par_mob>().viz(true);
                                if (func.carts[n_x, n_y] != null && !func.carts[n_x, n_y].active)
                                    func.carts[n_x, n_y].active = true;
                                if (!func.eath[n_x, n_y].active)
                                    func.eath[n_x, n_y].active = true;
                            }





                        }



                        /*
                        if (way_fr_bl[j, i] != -1)
                        {
                            if (lt_v2_vizited.Contains(new Vector2(j, i)))
                            {

                            }
                                lt_v2_vizited.Remove(new Vector2(j, i));
                            if (func.mob[j, i] != null && friend != func.mob[j, i].GetComponent<par_mob>().friend)
                            {
                                par_mob mb_viz = func.mob[j, i].GetComponent<par_mob>();
                                lt_mob_vizited.Remove(func.mob[j, i]);
                                cc = Mathf.Abs((j - x_wolk)) + Mathf.Abs((i - y_wolk));
                                if (cc > mb_viz.distanse_max && lt_v2_vizited.Contains(gameObject))
                                {
                                    mb_viz.lt_mob_vizited.Remove(gameObject);
                                }
                            }
                        }
                        */

                        //System.out.println(j + " " + i + "    del:  " + n_x + " " + n_y);



                    }
                }

            }

        }
        Debug.Log(lt_mob_vizited.Count);
    }
    public void attack(GameObject gm)
    {

    }
    void at(GameObject gm, parametrs pr)
    {

    }
    public void simm(int i)
    {
        gameObject.transform.localScale = new Vector3(i * Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        if(gameObject.transform.Find("Canvas hp")!=null)
            gameObject.transform.Find("Canvas hp").localScale = new Vector3(i * Mathf.Abs(gameObject.transform.Find("Canvas hp").localScale.x), gameObject.transform.Find("Canvas hp").localScale.y, gameObject.transform.Find("Canvas hp").localScale.z);
    }
    public void viz(bool bl)
    {
        foreach (Transform tr in gameObject.transform)
        {
            if (tr.gameObject.active != bl)
                tr.gameObject.active = bl;
        }
        //if((!gameObject.GetComponent<Animator>().enabled && bl)|| (gameObject.GetComponent<Animator>().enabled && !bl))
        //gameObject.GetComponent<Animator>().enabled = bl;
        Vector4 v4 = gameObject.GetComponent<SpriteRenderer>().color;
        if (bl)
        {
            v4.w = 1;
        }
        else
            v4.w = 0;
        gameObject.GetComponent<SpriteRenderer>().color = v4;

        vizited = bl;
    }
    public void damage(int dm, string type)
    {
        int dop_dm = 0;
        if (dm < 0)
            dm = 0;
        //func.create_tx_infa("[" + type_dm + "] " + dm, new Vector2(x + 1, y + 1), 2);
        func.test_message = "error wolk damage " + num.ToString() + " 5065";

        Debug.Log("damage  " + type + " " + dm + " " + num);

        func.test_message = "error wolk damage " + num.ToString() + " 5077";
        dm += dop_dm;


        if (dm > 0)
        {
            foreach (par_cart_art cart in lt_carts_art)
            {
                if (cart.q > 0 && cart.name == "cart berserk boss")
                {
                    int c = dm;
                    dm -= cart.q;
                    cart.q -= c;
                    if (cart.q < 0)
                        cart.q = 0;
                }
            }
        }
        if (dm < 0)
            dm = 0;
        hp -= dm;
        pr_damage(dm);
        func.test_message = "error wolk damage " + num.ToString() + " 5104";
        if (hp <= 0)
        {
            hp = 0;
            List<par_cart_art> cart_del_art = new List<par_cart_art>();

            foreach (par_cart_art cart in lt_carts_art)
            {
                if (cart.name == "cart respawn" && hp == 0)
                {
                    cart_del_art.Add(cart);
                    activate_art(cart, x, y);

                    //remove_cart("cart respawn");
                    hp = hp_max;
                    //if (gameObject.name == data.data_now.pl_name_now)
                    //gameObject.GetComponent<Animator>().SetInteger("hp", hp);
                    //return;
                }


            }
            int tt = cart_del_art.Count;
            for (int i = 0; i < tt; i++)
            {
                string st_name = cart_del_art[0].name;
                remove_cart_art(st_name);
                remove_cart(st_name);
                cart_del_art.RemoveAt(0);
            }

            if (data.data_now.pl_name_now == "berserk player")
            {
                par_mob pl = GameObject.Find("berserk player").GetComponent<par_mob>();
                func.create_effect("berserk ef", pl.x, pl.y, GameObject.Find("berserk player"), true, hp_max, new List<string>());
                //dop_pow *=2;
                //mb.damage(1, "none");
            }
        }
        func.test_message = "error wolk damage " + num.ToString() + " 5143";
        if (gameObject.name == data.data_now.pl_name_now)
            gameObject.GetComponent<Animator>().SetInteger("hp", hp);
        if (gameObject.transform.Find("Canvas hp")!=null && gameObject.transform.Find("Canvas hp").Find("hp bar") != null)
        {
            gameObject.transform.Find("Canvas hp").Find("hp bar").gameObject.GetComponent<Scrollbar>().size = ((float)hp / hp_max);
        }
        func.test_message = "error wolk damage " + num.ToString() + " 5150";

        int count_mast = lt_carts_art.ConvertAll(x => x.name).Where(x => (x == "cart mast")).Count();
        foreach (par_cart_art cart in lt_carts_art)
        {

            if (cart.perez_now != 0 && cart.tp_perez==par_cart_art.type_perez.damage)
            {
                if (cart.name == "cart berserk")
                {
                    cart.perez_now -= dm * (1 + count_mast);
                    if (cart.perez_now < 0)
                        cart.perez_now = 0;
                    if (cart.perez_now == 0)
                    {
                        activate_art(cart);
                    }
                }
                /*
                else if (cart.name == "cart berserk")
                {
                    cart.perez_now -= dm;
                    if (cart.perez_now == 0)
                    {
                        activate_art(cart);
                    }


                }*/
                else if (cart.name == "cart berserk boss")
                {
                    if (cart.q == 0)
                        cart.perez_now -= dm * (1 + count_mast);
                    if (cart.perez_now < 0)
                        cart.perez_now = 0;
                    if (cart.perez_now == 0)
                    {
                        activate_art(cart);
                        cart.q += (hp_max - hp) / 5;
                    }


                }

                if (cart.perez_now < 0)
                    cart.perez_now = 0;
                /*
                if(cart.perez_now == 0 && cart.name == "cart berserk st")
                {
                    activate_art(cart);
                }
                */
            }
        }
        if (hp <= 0)
        {
            hp = 0;
            dead();
            /*
            foreach (par_cart_art cart in lt_carts_art)
            {
                if (cart.name == "cart respawn")
                {
                    activate_art(cart, x, y);
                    //remove_cart_art("cart respawn");
                    //remove_cart("cart respawn");
                    hp = hp_max;
                    return;
                }
                    
                
            }
             */

            /*
            func.test_message = "error wolk damage " + num.ToString() + " 5189";
            if (gameObject.name != data.data_now.pl_name_now)
            {
                if (gameObject.GetInstanceID() < 0)
                {
                    int bl_boss = 1;
                    if (gameObject.name == "goblin boss" || gameObject.name == "goblin shaman")
                    {
                        bl_boss = 5;
                        data.give_achive("умелый");
                    }
                    if (!friend)
                        foreach (par_cart_art cart in lt_carts_art)
                        {
                            if (Random.Range(0, 25 / bl_boss) == 0)
                            {
                                GameObject gm_cart = func.drop(cart.name, x, y);
                                if (gm_cart != null)
                                    gm_cart.active = func.eath[x, y].active;
                            }

                        }
                    if (!friend)
                        foreach (string cart in lt_carts)
                        {
                            if (Random.Range(0, 10 / bl_boss) == 0)
                            {
                                GameObject gm_cart = func.drop(cart, x, y);
                                if (gm_cart != null)
                                    gm_cart.active = func.eath[x, y].active;
                            }
                            //func.drop(cart, x, y).active = func.eath[x, y].active;
                        }

                    if (gameObject.name == "derlorg" || gameObject.name == "derlorg shaman" || gameObject.name == "derlorg fire" || gameObject.name == "derlorg poition" || gameObject.name == "derlorg blood" || gameObject.name == "derlorg rock" || gameObject.name == "derlorg energy" || gameObject.name == "derlorg ice" || gameObject.name == "dungalorg")
                        data.data_now.cl_mob_dead_derlorg++;
                    if (data.data_now.cl_mob_dead_derlorg == 50)
                    {
                        //data.data_now.lt_players.Add(data.players.berserk);
                        data.data_now.lt_players.Add(data.players.derlorg);
                        data.give_achive("Истребитель");

                    }
                    if (!friend)
                        data.data_now.cl_mob_dead_now++;
                    if (data.data_now.cl_mob_dead_now == 100)
                    {
                        //data.data_now.lt_players.Add(data.players.berserk);
                        //data.data_now.lt_players.Add(data.players.derlorg);
                        data.give_achive("опытный");

                    }
                    func.test_message = "error wolk damage " + num.ToString() + " 5220";

                    distanse_max = 0;
                    update_viz();


                    foreach (GameObject gm in lt_mob_vizited_in)
                    {
                        gm.GetComponent<par_mob>().lt_mob_vizited.Remove(gameObject);
                    }
                    foreach (GameObject gm in lt_mob_vizited)
                    {
                        gm.GetComponent<par_mob>().lt_mob_vizited_in.Remove(gameObject);
                    }
                    func.lt_v2_viz_mob[x, y].Remove(gameObject);
                    Debug.Log("dead " + num);

                    func.mob_list[num] = null;
                    func.nums_mob.Remove(num);
                    func.nums_mob_wolk.Remove(num);
                    func.mob[x, y] = null;
                    if (parent != null)
                        parent.GetComponent<par_mob>().childs.Remove(gameObject);

                    if (friend)
                    {
                        func.col_friend--;
                        GameObject.Find("Main Camera").transform.parent = func.mob_list[0].transform;
                        GameObject.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
                    }

                }
                Destroy(gameObject);
                func.test_message = "error wolk damage " + num.ToString() + " 5241";



            }

            else
            {

                if (data.data_now.col_dead < 20)
                {
                    data.data_now.col_dead++;
                    if (data.data_now.col_dead == 20)
                    {
                        //data.data_now.lt_players.Add(data.players.berserk);
                        data.give_achive("не в первый раз");

                    }
                }
                data.save_data();


                if (friend)
                    friend = false;

                func.played = false;
                //GameObject.Find("EventS")
                //Time.timeScale = 0;
                anim.col_anim++;
                //func.hod_next_mob_art = false;
                //func.hod_next_mob = false;
                GameObject.Find("Canvas end").GetComponent<Animator>().SetBool("opened", true);
            }
*/

        }
        func.test_message = "good";
        //par_mob player = gameObject.Find("player");
    }
    public void dead()
    {
        //func.test_message = "error wolk damage " + num.ToString() + " 5189";
        if (gameObject.name != data.data_now.pl_name_now)
        {
            if (gameObject.GetInstanceID() < 0)
            {
                if((data.data_now.pl_name_now== "berserk player") &&!friend)
                    func.mob_list[0].GetComponent<par_mob>().regen(hp_max,"none");


                int bl_boss = 1;
                List<string> lt_el_mob = new List<string>();

                lt_el_mob.Add("org");
                lt_el_mob.Add("shaman");

                lt_el_mob.Add("half-fire spirit");
                lt_el_mob.Add("half-poition spirit");
                lt_el_mob.Add("half-energy spirit");
                lt_el_mob.Add("half-ice spirit");
                lt_el_mob.Add("half-blood spirit");
                lt_el_mob.Add("half-rock spirit");

                lt_el_mob.Add("fire defender 1");
                lt_el_mob.Add("poition defender 1");
                lt_el_mob.Add("energy defender 1");
                lt_el_mob.Add("ice defender 1");
                lt_el_mob.Add("blood defender 1");
                lt_el_mob.Add("rock defender 1");

                lt_el_mob.Add("org berserk");
                lt_el_mob.Add("goblin shaman");

                lt_el_mob.Add("fire elemental");
                lt_el_mob.Add("poition elemental");
                lt_el_mob.Add("energy elemental");
                lt_el_mob.Add("ice elemental");
                lt_el_mob.Add("blood elemental");
                lt_el_mob.Add("rock elemental");

                lt_el_mob.Add("damage master");
                
                lt_el_mob.Add("fire master");
                lt_el_mob.Add("poition master");
                lt_el_mob.Add("energy master");
                lt_el_mob.Add("ice master");
                lt_el_mob.Add("blood master");
                lt_el_mob.Add("rock master");
                

                if (lt_el_mob.Contains(gameObject.name)&& (func.num_lev%5==0))
                {
                    bl_boss = 5;
                    data.give_achive("умелый");
                }
                if (!friend && gameObject.name!= "dych")
                    foreach (par_cart_art cart in lt_carts_art)
                    {
                        if (Random.Range(0, 20 / bl_boss) == 0)
                        {
                            GameObject gm_cart = func.drop(cart.name, x, y);
                            if (gm_cart != null)
                                gm_cart.active = func.eath[x, y].active;
                        }

                    }
                if (!friend && gameObject.name != "dych")
                    foreach (string cart in lt_carts)
                    {
                        if (Random.Range(0, 10 / bl_boss) == 0)
                        {
                            GameObject gm_cart = func.drop(cart, x, y);
                            if (gm_cart != null)
                                gm_cart.active = func.eath[x, y].active;
                        }
                        //func.drop(cart, x, y).active = func.eath[x, y].active;
                    }
                Debug.Log((gameObject.name == "manecen") + " " + (x == 8 && y == 4) + " " + (data.data_now.lt_dialog.Contains("start") )+" "+( !data.data_now.lt_dialog.Contains("start 1")));
                if(gameObject.name=="manecen" &&(x==8 && y==4)&& data.data_now.lt_dialog.Contains("start") && !data.data_now.lt_dialog.Contains("start 1"))
                {
                    par_dialog.par_dialog_now.start_dialog("start 1");
                }
                if (gameObject.name == "manecen" && (x == 12 && y == 4) && data.data_now.lt_dialog.Contains("start 1") && !data.data_now.lt_dialog.Contains("start 2"))
                {
                    par_dialog.par_dialog_now.start_dialog("start 2");
                }
                if (gameObject.name == "derlorg" || gameObject.name == "derlorg shaman" || gameObject.name == "derlorg fire" || gameObject.name == "derlorg poition" || gameObject.name == "derlorg blood" || gameObject.name == "derlorg rock" || gameObject.name == "derlorg energy" || gameObject.name == "derlorg ice" || gameObject.name == "derlorg bomb" || gameObject.name == "derlorg necro" || gameObject.name == "dungalorg")
                    data.data_now.cl_mob_dead_derlorg++;
                if (data.data_now.cl_mob_dead_derlorg == 50)
                {
                    //data.data_now.lt_players.Add(data.players.berserk);
                    data.data_now.lt_players.Add(data.players.derlorg);
                    data.give_achive("Истребитель");

                }
                if (!friend)
                    data.data_now.cl_mob_dead_now++;
                if (data.data_now.cl_mob_dead_now == 100)
                {
                    //data.data_now.lt_players.Add(data.players.berserk);
                    //data.data_now.lt_players.Add(data.players.derlorg);
                    data.give_achive("опытный");

                }
                func.test_message = "error wolk damage " + num.ToString() + " 5220";
                /*
                data.data_now.cl_mob_dead_ber++;

                if (data.data_now.cl_mob_dead_ber == 50)
                {
                    data.data_now.lt_players.Add(data.players.berserk);
                    data.give_achive("до самой смерти");

                }
                */
                distanse_max = 0;
                update_viz();


                foreach (GameObject gm in lt_mob_vizited_in)
                {
                    gm.GetComponent<par_mob>().lt_mob_vizited.Remove(gameObject);
                    gm.GetComponent<par_mob>().lt_v2_vizited.Remove(new Vector2(x,y));
                }
                foreach (GameObject gm in lt_mob_vizited)
                {
                    gm.GetComponent<par_mob>().lt_mob_vizited_in.Remove(gameObject);
                }
                foreach (GameObject gm in childs)
                {
                    gm.GetComponent<par_mob>().parent = null;
                }
                func.lt_v2_viz_mob[x, y].Remove(gameObject);
                Debug.Log("dead " + num);

                func.mob_list[num] = null;
                func.nums_mob.Remove(num);
                func.nums_mob_wolk.Remove(num);
                func.mob[x, y] = null;
                if (parent != null)
                    parent.GetComponent<par_mob>().childs.Remove(gameObject);

                if (friend)
                {
                    func.col_friend--;
                    GameObject.Find("Main Camera").transform.parent = func.mob_list[0].transform;
                    GameObject.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
                }

            }
            Destroy(gameObject);
            func.test_message = "error wolk damage " + num.ToString() + " 5241";



        }

        else
        {

            if (data.data_now.col_dead < 20)
            {
                data.data_now.col_dead++;
                if (data.data_now.col_dead == 20)
                {
                    //data.data_now.lt_players.Add(data.players.berserk);
                    data.give_achive("не в первый раз");

                }
            }
            data.save_data();


            if (friend)
                friend = false;

            func.played = false;
            //GameObject.Find("EventS")
            //Time.timeScale = 0;
            anim.col_anim++;
            //func.hod_next_mob_art = false;
            //func.hod_next_mob = false;
            GameObject.Find("Canvas end").GetComponent<Animator>().SetBool("opened", true);
        }
    }
    public void pr_damage(int dm)
    {
        TMP_Text tx = Instantiate(GameObject.Find("ignoring").transform.Find("tx hp").gameObject, gameObject.transform.Find("Canvas hp")).GetComponent<TMP_Text>();
        tx.gameObject.transform.localScale = new Vector3(1, 1, 1);

        tx.text = "-" + dm;
        tx.gameObject.transform.localPosition = new Vector2(0, 0);
        //tx.gameObject.active = true;

        que_dm_tx.Enqueue(tx.gameObject);
    }
    public void pr_regen(int dm)
    {
        TMP_Text tx = Instantiate(GameObject.Find("ignoring").transform.Find("tx hp").gameObject, gameObject.transform.Find("Canvas hp")).GetComponent<TMP_Text>();
        tx.gameObject.transform.localScale = new Vector3(1, 1, 1);

        tx.text = "+" + dm;
        tx.gameObject.transform.localPosition = new Vector2(0, 0);
        //tx.gameObject.active = true;

        que_dm_tx.Enqueue(tx.gameObject);
    }
    public void regen(int hill, string type)
    {
        Debug.Log("regen  " + hill + " " + num);
        if (hp > 0)
            hp += hill;
        pr_regen(hill);
        if (gameObject.transform.Find("Canvas hp").Find("hp bar") != null)
        {
            gameObject.transform.Find("Canvas hp").Find("hp bar").gameObject.GetComponent<Scrollbar>().size = ((float)hp / hp_max);
        }
        /*
        TMP_Text tx = Instantiate(GameObject.Find("ignoring").transform.Find("tx hp").gameObject, gameObject.transform.Find("Canvas hp")).GetComponent<TMP_Text>();
        tx.gameObject.transform.localScale = new Vector3(1, 1, 1);
        tx.text = "+" + hill;
        tx.gameObject.transform.localPosition = new Vector2(0, 0);
        tx.gameObject.active = true;
        */
        if (gameObject.name == data.data_now.pl_name_now)
        {
            //data.data_now.cl_mob_dead_ber = 0;
            gameObject.GetComponent<Animator>().SetInteger("hp", hp);
        }

        if (hp > hp_max)
            hp = hp_max;
        //par_mob player = gameObject.Find("player");
    }
    void OnDestroy()
    {
        //Debug.Log("dead 1" + gameObject.name + " " + num + " " + gameObject.GetInstanceID());


        //Debug.Log("def");
    }
    void Awake()
    {
        hp = hp_max;
        
    }
    void Start()
    {
        if (!func.baza.ContainsKey(gameObject.name))
        {
            cel = GameObject.Find(data.data_now.pl_name_now);
        }


        func = GameObject.Find("func").GetComponent<func>();


        //Debug.Log("key " + gameObject.name);

        if (!func.baza.ContainsKey(gameObject.name))
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                //Debug.Log(tr.gameObject.name + " "+ (GameObject.Find(tr.gameObject.name)==null));
                string nm = tr.gameObject.name;
                if (!GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().art_cart_bl() && tr.gameObject.name != "cart gold")
                {
                    par_cart.type_cart cart_name = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (cart_name == par_cart.type_cart.damage && gameObject.name == "atack player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if ((cart_name == par_cart.type_cart.fire || cart_name == par_cart.type_cart.spirit_fire) && gameObject.name == "fire player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if ((cart_name == par_cart.type_cart.rock || cart_name == par_cart.type_cart.spirit_rock) && gameObject.name == "rock player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if ((cart_name == par_cart.type_cart.ice || cart_name == par_cart.type_cart.spirit_ice) && gameObject.name == "ice player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if ((cart_name == par_cart.type_cart.blood || cart_name == par_cart.type_cart.spirit_blood) && gameObject.name == "blood player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if ((cart_name == par_cart.type_cart.energy || cart_name == par_cart.type_cart.spirit_energy) && gameObject.name == "energy player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if ((cart_name == par_cart.type_cart.poition || cart_name == par_cart.type_cart.spirit_poition) && gameObject.name == "poition player")
                        carts_count_max.Add(tr.gameObject.name, 8);

                    else if (cart_name == par_cart.type_cart.sp_barier && gameObject.name == "barier player")
                        carts_count_max.Add(tr.gameObject.name, 4);

                    //else if (cart_name == par_cart.type_cart.damage && gameObject.name == "org berserk")
                        //carts_count_max.Add(tr.gameObject.name, 10);

                    else
                    {
                        int dop_inven = 0;
                        if (gameObject.name == "derlorg player")
                            dop_inven = 1;
                        //if (nm == "cart dungalorg(sleep)" || nm == "cart dungalorg")
                        //carts_count_max.Add(tr.gameObject.name, 12+2*dop_inven);
                        if (GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().spetial)
                            carts_count_max.Add(tr.gameObject.name, 3 - dop_inven);
                        else
                            carts_count_max.Add(tr.gameObject.name, 6 - dop_inven * 2);
                    }

                    //Debug.Log(carts_count_max[tr.gameObject.name] + " trtr  " + tr.gameObject.name + "       " + gameObject.name);
                }


            }

            func.baza.Add(gameObject.name, gameObject);
            if (gameObject.name == "base player")
            {
                add_cart_art("cart base st");
                //add_cart_art("cart elemental");
                //add_cart_art("cart dungalorg(boss)");
                //add_cart_art("cart dungalorg heard");
                //add_cart_art("cart berserk boss");
                //add_cart_art("cart metca");
                //add_cart_art("cart sp derlorg");
            }
            if (gameObject.name == "atack player")
            {
                //add_cart_art("cart sp poition");
                //add_cart_art("cart speed poition");
                add_cart_art("cart att st");
                //add_cart_art("cart sp crystall");
            }
            if (gameObject.name == "barier player")
            {
                //add_cart_art("cart base st");
                add_cart_art("cart bar st");
            }
            if (gameObject.name == "rock player")
            {
                //add_cart_art("cart base st");
                add_cart_art("cart rock st");
            }
            if (gameObject.name == "fire player")
            {
                //add_cart_art("cart base st");
                add_cart_art("cart fire st");
                //add_cart_art("cart sp barier");
                //add_cart_art("cart sp atack 1");
                //add_cart_art("cart fire pow 1");
            }
            if (gameObject.name == "blood player")
            {
                //add_cart_art("cart base st");
                add_cart_art("cart blood st");
            }
            if (gameObject.name == "energy player")
            {
                //add_cart_art("cart base st");
                add_cart_art("cart energy st");
                //add_cart_art("cart act energy");
                //add_cart_art("cart act energy");
                //add_cart_art("cart act energy");
            }
            if (gameObject.name == "ice player")
            {
                //add_cart_art("cart base st");
                add_cart_art("cart ice st");
            }
            if (gameObject.name == "poition player")
            {
                //add_cart_art("cart base st");
                add_cart_art("cart poition st");
            }
            if (gameObject.name == "berserk player")
            {
                //add_cart_art("cart base st");
                //add_cart_art("cart sp poition");
                //add_cart_art("cart speed poition");
                //add_cart_art("cart att st");
                //add_cart_art("cart sp crystall");

                add_cart_art("cart berserk st");
            }
            if (gameObject.name == "derlorg player")
            {
                //add_cart_art("cart sp crystall");
                //add_cart_art("cart base st");
                add_cart_art("cart derlorg st");
            }


            else if (gameObject.name == "goblin elit")
            {
                add_cart_art("cart sp atack");
                //add_cart_art("cart sp barier");

                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                //type_lt_attack.Add(type_atack_mob.damage);
                //type_lt_attack.Add(type_atack_mob.barier);
            }

            else if (gameObject.name == "defender 1")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart universal");
                //add_cart_art("cart sp barier");

                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.bomb, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.dych, 1));
                //type_lt_attack.Add(type_atack_mob.damage);
                //type_lt_attack.Add(type_atack_mob.barier);
            }
            else if (gameObject.name == "defender elemental")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart elemental");
                //add_cart_art("cart sp barier");

                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.bomb, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.dych, 1));
                //type_lt_attack.Add(type_atack_mob.damage);
                //type_lt_attack.Add(type_atack_mob.barier);
            }
            else if (gameObject.name == "defender barier")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp barier");
                //add_cart_art("cart sp barier");

                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 2));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.bomb, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.dych, 1));
                //type_lt_attack.Add(type_atack_mob.damage);
                //type_lt_attack.Add(type_atack_mob.barier);
            }

            else if (gameObject.name == "goblin" || gameObject.name == "defender")
            {
                add_cart_art("cart sp atack");
                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
            }

            //add_cart_art("cart sp rock");
            //add_cart_art("cart sp blood");
            else if (gameObject.name == "goblin hiller")
            {
                add_cart_art("cart sp barier");
                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                //type_priority_attack.Add(new );
                //add_cart_art("cart sp rock");
            }
            else if (gameObject.name == "shaman")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart mini spirit");
                add_cart_art("cart mini shaman");
                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                //type_priority_attack.Add(new );
                //add_cart_art("cart sp rock");
            }
            else if (gameObject.name == "org")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart dop hill");
                add_cart_art("cart berserk");
                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                //type_priority_attack.Add(new );
                //add_cart_art("cart sp rock");
            }


            if (gameObject.name == "org berserk")
            {
                add_cart_art("cart sp atack");
                //add_cart_art("cart sp atack");
                add_cart_art("cart berserk boss");
                add_cart_art("cart rage");

                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));

                //base_pr();
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.sp_boss, 2));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 0));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.crystall, 0));
                //Debug.Log("start priority " + contains_pr_at(priority_atack.type_atack.damage));
                //add_cart_art("cart sp barier 1");
                //add_cart_art("cart sp rock 1");
                //add_cart_art("cart berserk");
            }
            else if (gameObject.name == "goblin shaman")
            {
                /*
                add_cart_art("cart sp atack");
                add_cart_art("cart sp fire");
                add_cart_art("cart sp rock");
                add_cart_art("cart sp blood");
                add_cart_art("cart sp energy");
                add_cart_art("cart sp ice");
                add_cart_art("cart sp poition");

                */
                add_cart_art("cart sp atack");
                add_cart_art("cart shaman boss");
                add_cart_art("cart spirit");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.sp_boss, 2));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 2));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                //type_lt_attack.Add(type_atack_mob.damage);
                //type_lt_attack.Add(type_atack_mob.elemental);
                //add_cart_art("cart sp barier 1");
                //add_cart_art("cart sp rock 1");
                //add_cart_art("cart berserk");
            }
            else if (gameObject.name == "goblin blood elit")
            {
                add_cart_art("cart sp blood");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                //base_pr();
                //add_cart_art("cart berserk");
            }
            else if (gameObject.name == "goblin rock elit" )
            {
                add_cart_art("cart sp rock");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                //base_pr();
                //add_cart_art("cart act rock");
            }
            else if (gameObject.name == "goblin fire elit" )
            {
                add_cart_art("cart sp fire");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
                //base_pr();
                //add_cart_art("cart act fire");
            }
            else if (gameObject.name == "goblin energy elit")
            {
                add_cart_art("cart sp energy");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "goblin ice elit")
            {
                add_cart_art("cart sp ice");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "goblin poition elit")
            {
                add_cart_art("cart sp poition");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }



            else if ( gameObject.name == "defender blood")
            {
                add_cart_art("cart sp blood");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                //base_pr();
                //add_cart_art("cart berserk");
            }
            else if (gameObject.name == "defender rock")
            {
                add_cart_art("cart sp rock");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                //base_pr();
                //add_cart_art("cart act rock");
            }
            else if (gameObject.name == "defender fire")
            {
                add_cart_art("cart sp fire");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
                //base_pr();
                //add_cart_art("cart act fire");
            }
            else if (gameObject.name == "defender energy")
            {
                add_cart_art("cart sp energy");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if ( gameObject.name == "defender ice")
            {
                add_cart_art("cart sp ice");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if ( gameObject.name == "defender poition")
            {
                add_cart_art("cart sp poition");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }



            else if (gameObject.name == "defender necro")
            {
                add_cart_art("cart sp dych");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.dych, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "defender bomb")
            {
                add_cart_art("cart sp bomb");
                add_cart_art("cart sp atack");
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.damage, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.bomb, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }

            else if (gameObject.name == "rock defender 1")
            {
                add_cart_art("cart sp rock 1");
                add_cart_art("cart mini mast rock");
                add_cart_art("cart sp rock");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
            }
            else if (gameObject.name == "blood defender 1")
            {
                add_cart_art("cart sp blood 1");
                add_cart_art("cart mini mast blood");
                add_cart_art("cart sp blood");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
            }
            else if (gameObject.name == "fire defender 1")
            {
                add_cart_art("cart sp fire 1");
                add_cart_art("cart mini mast fire");
                add_cart_art("cart sp fire");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
            }
            else if (gameObject.name == "poition defender 1")
            {
                add_cart_art("cart sp poition 1");
                add_cart_art("cart mini mast poition");
                add_cart_art("cart sp poition");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
            }
            else if (gameObject.name == "energy defender 1")
            {
                add_cart_art("cart sp energy 1");
                add_cart_art("cart mini mast energy");
                add_cart_art("cart sp energy");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
            }
            else if (gameObject.name == "ice defender 1")
            {
                add_cart_art("cart sp ice 1");
                add_cart_art("cart mini mast ice");
                add_cart_art("cart sp ice");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
            }

            else if (gameObject.name == "half-rock spirit")
            {
                add_cart_art("cart mini spirit rock");
                add_cart_art("cart rock heart");
                add_cart_art("cart sp rock");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
            }
            else if (gameObject.name == "half-fire spirit")
            {
                add_cart_art("cart mini spirit fire");
                add_cart_art("cart fire heart");
                add_cart_art("cart sp fire");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
            }
            else if (gameObject.name == "half-poition spirit")
            {
                add_cart_art("cart mini spirit poition");
                add_cart_art("cart poition heart");
                add_cart_art("cart sp poition");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
            }
            else if (gameObject.name == "half-blood spirit")
            {
                add_cart_art("cart mini spirit blood");
                add_cart_art("cart blood heart");
                add_cart_art("cart sp blood");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
            }
            else if (gameObject.name == "half-energy spirit")
            {
                add_cart_art("cart mini spirit energy");
                add_cart_art("cart energy heart");
                add_cart_art("cart sp energy");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
            }
            else if (gameObject.name == "half-ice spirit")
            {
                add_cart_art("cart mini spirit ice");
                add_cart_art("cart ice heart");
                add_cart_art("cart sp ice");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
            }



            else if (gameObject.name == "rock master")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp rock");
                add_cart_art("cart sp rock 2");
                add_cart_art("cart mast rock");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
            }
            else if (gameObject.name == "blood master")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp blood");
                add_cart_art("cart sp blood 2");
                add_cart_art("cart mast blood");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
            }
            else if (gameObject.name == "fire master")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp fire");
                add_cart_art("cart sp fire 2");
                add_cart_art("cart mast fire");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
            }
            else if (gameObject.name == "poition master")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp poition");
                add_cart_art("cart sp poition 2");
                add_cart_art("cart mast poition");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
            }
            else if (gameObject.name == "energy master")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp energy");
                add_cart_art("cart sp energy 2");
                add_cart_art("cart mast energy");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
            }
            else if (gameObject.name == "ice master")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp ice");
                add_cart_art("cart sp ice 2");
                add_cart_art("cart mast ice");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
            }



            else if (gameObject.name == "rock elemental")
            {
                add_cart_art("cart sp spirit rock");
                add_cart_art("cart spirit rock heart");
                add_cart_art("cart sp rock");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
            }
            else if (gameObject.name == "fire elemental")
            {
                add_cart_art("cart sp spirit fire");
                add_cart_art("cart spirit fire heart");
                add_cart_art("cart sp fire");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
            }
            else if (gameObject.name == "poition elemental")
            {
                add_cart_art("cart sp spirit poition");
                add_cart_art("cart spirit poition heart");
                add_cart_art("cart sp poition");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
            }
            else if (gameObject.name == "blood elemental")
            {
                add_cart_art("cart sp spirit blood");
                add_cart_art("cart spirit blood heart");
                add_cart_art("cart sp blood");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
            }
            else if (gameObject.name == "energy elemental")
            {
                add_cart_art("cart sp spirit energy");
                add_cart_art("cart spirit energy heart");
                add_cart_art("cart sp energy");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
            }
            else if (gameObject.name == "ice elemental")
            {
                add_cart_art("cart sp spirit ice");
                add_cart_art("cart spirit ice heart");
                add_cart_art("cart sp ice");
                add_cart_art("cart sp atack");
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
            }



            else if (gameObject.name == "derlorg")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg shaman")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                //type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg fire")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp fire");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg blood")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp blood");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg poition")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp poition");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                //base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg ice")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp ice");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg energy")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp energy");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                base_pr();
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg rock")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp rock");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg bomb")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp bomb");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.bomb, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg necro")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp dych");
                //add_cart_art("cart sp barier");

                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.dych, 1));
                type_priority_attack.Add(new priority_atack(priority_atack.type_atack.seal, 1));
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "dungalorg")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart dungalorg(boss)");
                add_cart_art("cart dungalorg heard");
                //add_cart_art("cart act energy");
            }

        }
        if (gameObject.name == data.data_now.pl_name_now)
            gameObject.GetComponent<Animator>().SetInteger("hp", hp);

        gameObject.GetComponent<SpriteRenderer>().material = new Material(gameObject.GetComponent<SpriteRenderer>().material);
        if(time_wolk==-1)
            time_wolk = 0.15f;
    }
    public float timer_now = 0;
    //public static int num_hod_wait=-1;
    public Queue<GameObject> que_dm_tx = new Queue<GameObject>();
    
    public class action_after_wolk
    {
        public bool active = false;
        public System.Action f;
        public action_after_wolk(System.Action f)
        {
            //this.active = active;
            this.f = f;
        }
    }
    public class action_atack
    {
        public List<string> carts = new List<string>();
        public Vector2 v2;
        public System.Action action_began;
        public System.Action action_after;
        public action_atack(List<string> carts,Vector2 v2,bool bl=false)
        {
            this.carts = carts;
            this.v2 = v2;
            if(!bl)
                this.action_began = null;
            else
                this.action_began = () => {anim.next_dialog=true; };
        }
    }
    public action_after_wolk action_after_wolk_now = null;
    public Queue<action_atack> lt_atack_comb = new Queue<action_atack>();
    public action_atack atack_comb_now = null;
    public Queue<System.Func<func.status_next_hod>> actions_hod = new Queue<System.Func<func.status_next_hod>>();
    //public Material mat;
    public bool outline_mat = true;
    void Update()
    {
        //if (num == 0 && lt_carts_active_now.Count>0)
        //Debug.Log("ui bt " + lt_carts_active_now.Count);
        if (timer_now < 0.5f)
            timer_now += Time.deltaTime;
        if (que_dm_tx.Count > 0 && timer_now >= 0.5f)
        {
            que_dm_tx.Dequeue().active = true;
            timer_now = 0;
        }
        if (anim.col_anim == 0 && bool_func_wolk_v2)
        {
            wolk_v2_bool();
            bool_func_wolk_v2 = false;
        }

        if (time_wolk_now >= 0)
        {
            //Debug.Log("timer hod " + num);
            time_wolk_now += Time.deltaTime;
            Vector2 v2 = gameObject.transform.position;
            v2 += d_v2_wolk * Time.deltaTime;
            gameObject.transform.position = new Vector3(v2.x, v2.y, 0);
            if (time_wolk_now >= time_wolk)
            {
                gameObject.transform.position = new Vector3(d_v2_wolk_end.x, d_v2_wolk_end.y, 0);
                anim.col_anim--;
                time_wolk_now = -1;
                if(action_after_wolk_now!=null && action_after_wolk_now.active)
                {
                    Debug.Log("invoke action");
                    action_after_wolk_now.f.Invoke();
                    action_after_wolk_now = null;
                }
            }

        }
        if (outline_mat && gameObject.GetComponent<SpriteRenderer>().sprite!=null)
        {
            Texture v2_t = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_texture_w", v2_t.width);
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_texture_h", v2_t.height);
        }
    }
        
    // Update is called once per frame
}


