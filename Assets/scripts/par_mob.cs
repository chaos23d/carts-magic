using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class par_mob : parametrs, IPointerClickHandler, interfase_click
{
    // Start is called before the first frame update
    //public int col_skills;
    //public GameObject ray

    public enum type_friend
    {
        none,
        auto,
        active,
        disactive
    }
    public enum type_atack_mob
    {
        none,
        damage,barier,crystall,
        blood,rock,ice,energy,fire,poition,
        elemental,
        spider,
        seal
    }
    
    [Header("mob parametrs")]
    public List<string> par_lt_sila = new List<string>();
    public List<string> par_lt_sila_base = new List<string>();
    public List<string> par_lt_speed = new List<string>();
    public List<string> par_lt_speed_base = new List<string>();
    [Header("base parametrs")]

    public bool friendly = false;
    public type_friend friend_type= type_friend.none;
    public bool nps = false;
    public int distanse_min = 1;
    public int distanse_max = 1;
    public int distanse_attack = 3;
    public bool vizited = false;
    //public List<string> lt_mob_speed_par = new List<string>();
    //public List<string> lt_mob_speed_par_base = new List<string>();
    //public List<string> lt_mob_sila_par = new List<string>();
    //public List<string> lt_mob_sila_par_base = new List<string>();

    public List<type_atack_mob> type_lt_attack = new List<type_atack_mob>();
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

    public float time_wolk = -1;
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
    public void add_baf(par_baf.type_baf baf, int time, int power)
    {
        Debug.Log("add baf " + baf + " " + time + " " + power);
        List<par_baf.type_baf> lt_baf_pow_time = new List<par_baf.type_baf> {



            par_baf.type_baf.seal_damage, par_baf.type_baf.seal_barier,
            par_baf.type_baf.seal_fire,
            par_baf.type_baf.seal_rock,
            par_baf.type_baf.seal_poition,
            par_baf.type_baf.seal_blood,
            par_baf.type_baf.seal_energy,
            par_baf.type_baf.seal_ice };
        List<par_baf.type_baf> lt_baf_barier = new List<par_baf.type_baf> { par_baf.type_baf.bar,
            par_baf.type_baf.bar_fire,
            par_baf.type_baf.bar_rock,
            par_baf.type_baf.bar_poition,
            par_baf.type_baf.bar_blood,
            par_baf.type_baf.bar_energy,
            par_baf.type_baf.bar_ice };

        par_baf bf_first = null;
        foreach (par_baf pr_baf in lt_bafs)
        {
            if (pr_baf.type == baf && (!lt_baf_pow_time.Contains(pr_baf.type)) || (lt_baf_pow_time.Contains(pr_baf.type) && pr_baf.power == power))
                bf_first = pr_baf;
        }
        bool bl = true;
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

        if (bf_first == null)
        {
            bl = false;
            GameObject gm_bf = null;
            if (baf == par_baf.type_baf.fire)
            {
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("fire").gameObject, gameObject.transform);
                gm_bf.name = "fire";
            }
            else if (baf == par_baf.type_baf.rage)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
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
                /*
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                */



                //bf_first = new_baf;
                //speed++;
                //speed_now++;
                //gm_bf = Instantiate(GameObject.Find("effects").transform.Find("poition").gameObject, gameObject.transform);
                //gm_bf.name = "poition";

                //gameObject.GetComponent<Animator>().speed = 1.5f;
            }



            else if (baf == par_baf.type_baf.energy)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.before = false;
                //new_baf.skip_first = true;
                lt_bafs.Add(new_baf);
                speed++;
                speed_now++;
                //speed_now++;
                //speed_now = 1;


                /*
                if (speed > 1 && hod_now)
                {
                    //speed_now = 2;
                    speed_now = 1;

                    if (func.nums_mob_wolk.Count > 0)
                        func.nums_mob_wolk.Insert(func.nums_mob_wolk.Count, num);
                    else
                    {
                        func.nums_mob_wolk = new List<int>(func.nums_mob);
                        func.nums_mob_wolk.Insert(func.nums_mob_wolk.Count, num);
                    }
                    //if (!hod_now)
                    //speed_now--;
                }
                else if (!hod_now)
                    new_baf.skip_first = true;
                */

                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("energy").gameObject, gameObject.transform);
                gm_bf.name = "energy";

                gameObject.GetComponent<Animator>().speed = 1.5f;
                //gameObject.GetComponent<Animator>().speed = 5;
            }
            else if (baf == par_baf.type_baf.poition)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
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
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.before = false;
                lt_bafs.Add(new_baf);
                speed--;
                //speed_now--;
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
                par_baf new_baf = gameObject.AddComponent<par_baf>();
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

            else if (baf == par_baf.type_baf.seal_damage)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal damage baf").gameObject, gameObject.transform);
                gm_bf.name = "seal damage baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }
            else if (baf == par_baf.type_baf.seal_barier)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal barier baf").gameObject, gameObject.transform);
                gm_bf.name = "seal barier baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }
            else if (baf == par_baf.type_baf.seal_fire)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal fire baf").gameObject, gameObject.transform);
                gm_bf.name = "seal fire baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }
            else if (baf == par_baf.type_baf.seal_poition)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal poition baf").gameObject, gameObject.transform);
                
                gm_bf.name = "seal poition baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }
            else if (baf == par_baf.type_baf.seal_rock)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal rock baf").gameObject, gameObject.transform);
                gm_bf.name = "seal rock baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }
            else if (baf == par_baf.type_baf.seal_blood)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal blood baf").gameObject, gameObject.transform);
                gm_bf.name = "seal blood baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }
            else if (baf == par_baf.type_baf.seal_energy)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal energy baf").gameObject, gameObject.transform);
                gm_bf.name = "seal energy baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }
            else if (baf == par_baf.type_baf.seal_ice)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                new_baf.before = true;
                lt_bafs.Add(new_baf);
                //speed--;
                //speed_now--;
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("seal ice baf").gameObject, gameObject.transform);
                gm_bf.name = "seal ice baf";

                //gameObject.GetComponent<Animator>().speed = 0.5f;
                //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.75f, 1, 1, 1);
            }


            else if (baf == par_baf.type_baf.bar)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases").gameObject, gameObject.transform);
                gm_bf.name = "bar bases";
            }


            else if (baf == par_baf.type_baf.bar_energy)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases energy").gameObject, gameObject.transform);
                gm_bf.name = "bar bases energy";
            }
            else if (baf == par_baf.type_baf.bar_rock)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases rock").gameObject, gameObject.transform);
                gm_bf.name = "bar bases rock";
            }
            else if (baf == par_baf.type_baf.bar_blood)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases blood").gameObject, gameObject.transform);
                gm_bf.name = "bar bases blood";
            }
            else if (baf == par_baf.type_baf.bar_fire)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases fire").gameObject, gameObject.transform);
                gm_bf.name = "bar bases fire";
            }
            else if (baf == par_baf.type_baf.bar_ice)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
                new_baf.long_baf = time;
                new_baf.type = baf;
                new_baf.power = power;
                lt_bafs.Add(new_baf);
                gm_bf = Instantiate(GameObject.Find("effects").transform.Find("bar bases ice").gameObject, gameObject.transform);
                gm_bf.name = "bar bases ice";
            }
            else if (baf == par_baf.type_baf.bar_poition)
            {
                par_baf new_baf = gameObject.AddComponent<par_baf>();
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
            }

        }
        if (baf == par_baf.type_baf.fire)
        {
            par_baf new_baf = gameObject.AddComponent<par_baf>();
            new_baf.long_baf = time;
            new_baf.type = baf;
            new_baf.before = false;
            new_baf.power = power;
            lt_bafs.Add(new_baf);
        }
        else if (baf == par_baf.type_baf.berserk)
        {
            par_baf new_baf = gameObject.AddComponent<par_baf>();
            new_baf.long_baf = time;
            new_baf.power = power;
            new_baf.skip_first = true;
            //new_baf.time = time;
            new_baf.type = baf;
            new_baf.before = false;
            lt_bafs.Add(new_baf);
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
        GameObject pn_bf = GameObject.Find("pn bufs");
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
    public bool activate_art(par_cart_art cart, int x, int y,object par)
    {
        Stack<object> st_par = new Stack<object>();
        st_par.Push(par);
        return activate_art(cart, x, y, st_par);
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


            else if (cart.name == "cart sp atack")
                bl = spawn_cart("cart damage", lt_art[a]);
            else if (cart.name == "cart sp barier")
                bl = spawn_cart("cart barier", lt_art[a]);

            else if (cart.name == "cart sp atack 1")
                bl = spawn_cart("cart damage 1", lt_art[a]);
            else if (cart.name == "cart sp barier 1")
                bl = spawn_cart("cart barier 1", lt_art[a]);

            else if (cart.name == "cart sp crystall")
                bl = spawn_cart("cart crystall", lt_art[a]);
            else if (cart.name == "cart sp crystall 1")
                bl = spawn_cart("cart crystall 1", lt_art[a]);

            else if (cart.name == "cart sp derlorg")
                bl = spawn_cart("cart derlorg", lt_art[a]);

            else if (cart.name == "cart sp spider")
                bl = spawn_cart("cart spider", lt_art[a]);
            else if (cart.name == "cart sp spider(spawned)")
                bl = spawn_cart("cart spider poition", lt_art[a]);

            else if (cart.name == "cart elemental")
            {
                bool bl1 = false;
                bl1 =  spawn_cart("cart ice", lt_art[a]) || bl1;
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
                bl1 = bl1 || spawn_cart("cart damage", lt_art[a]);
                bl1 = bl1 || spawn_cart("cart damage", lt_art[a]);
                bl1 = bl1 || spawn_cart("cart barier", lt_art[a]);
                bl1 = bl1 || spawn_cart("cart crystall", lt_art[a]);
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
                func.create_effect(func.get_name_effect(new List<string> {"cart rage(boss)"}, null), this.x, this.y, gameObject, false, (hp_max-hp)/5, new List<string> { "cart rage(boss)" });
            }


            else if (cart.type == par_cart.type_cart.art_blood_cuzna ||
                cart.type == par_cart.type_cart.art_ice_cuzna ||
                cart.type == par_cart.type_cart.art_energy_cuzna ||
                cart.type == par_cart.type_cart.art_fire_cuzna ||
                cart.type == par_cart.type_cart.art_poition_cuzna ||
                cart.type == par_cart.type_cart.art_rock_cuzna)
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

        Debug.Log(cart.name + " " + cart.q+" "+bl+" "+bl_dop);
        if (bl && bl_dop)
        {
            Debug.Log("q and " + cart.type_q_now);
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
        par_cart_art art1 = GameObject.Find(cart).GetComponent<par_cart_art>();
        art.type = art1.type;
        art.perez = art1.perez;
        art.perez_now = art1.perez;
        art.q = art1.q;
        art.q_max = art1.q_max;
        art.q_sum = art1.q_sum;
        art.type_q_now = art1.type_q_now;
        art.name = cart;
        art.lvl = art1.lvl;
        art.infa = art1.infa;

        string[] a_ = new string[3];

        a_[0] = "";
        a_[1] = "1";
        a_[2] = "2";

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
        ui.print_carts_art(num);

        ad_cart_art_active(cart);

        string aa = "";
        if (art.lvl > 1)
            aa = " " + (art.lvl - 1).ToString();
        if (art1.type == par_cart.type_cart.art_rock_sp || art1.type == par_cart.type_cart.art_rock_act || art1.type == par_cart.type_cart.art_speed_rock || art1.type == par_cart.type_cart.art_pow_rock)
        {
            //Debug.Log("ttttttttttt"+aa);
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart rock" + a_[i]] += 2;
            }
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
        else if (art1.type == par_cart.type_cart.art_blood_sp || art1.type == par_cart.type_cart.art_blood_act || art1.type == par_cart.type_cart.art_speed_blood || art1.type == par_cart.type_cart.art_pow_blood)
        {
            //carts_count_max["cart blood" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart blood" + a_[i]] += 2;
            }
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
                    par_lt_speed_base.Add(par_cart.type_cart.blood.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_fire_sp || art1.type == par_cart.type_cart.art_fire_act || art1.type == par_cart.type_cart.art_speed_fire || art1.type == par_cart.type_cart.art_pow_fire)
        {
            //carts_count_max["cart fire" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart fire" + a_[i]] += 2;
            }
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
        else if (art1.type == par_cart.type_cart.art_energy_sp || art1.type == par_cart.type_cart.art_energy_act || art1.type == par_cart.type_cart.art_speed_energy || art1.type == par_cart.type_cart.art_pow_energy)
        {
            //carts_count_max["cart energy" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart energy" + a_[i]] += 2;
            }
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
        else if (art1.type == par_cart.type_cart.art_ice_sp || art1.type == par_cart.type_cart.art_ice_act || art1.type == par_cart.type_cart.art_speed_ice || art1.type == par_cart.type_cart.art_pow_ice)
        {
            //carts_count_max["cart ice" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart ice" + a_[i]] += 2;
            }
            for (int i = 0; i < ((par_cart)art1).lvl; i++)
            {
                if (art1.type == par_cart.type_cart.art_ice_act)
                {
                    par_lt_sila_base.Add(par_cart.type_cart.ice.ToString());
                    par_lt_sila_base.Add(par_cart.type_cart.spirit_ice.ToString());
                }
                else if (art1.type == par_cart.type_cart.art_speed_ice)
                {
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_ice.ToString());
                    par_lt_speed_base.Add(par_cart.type_cart.spirit_ice.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_poition_sp || art1.type == par_cart.type_cart.art_poition_act || art1.type == par_cart.type_cart.art_speed_poition || art1.type == par_cart.type_cart.art_pow_poition)
        {
            //carts_count_max["cart poition" + aa] += 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart poition" + a_[i]] += 2;
            }
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
        else if (art1.type == par_cart.type_cart.art_crystall_sp)
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


        else if (art1.type == par_cart.type_cart.art_base_st)
        {
            carts_count_max["cart damage"] += 2;
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
            hp_max -= 2;
            if (hp > hp_max)
                hp = hp_max;
        }

        else if (cart == "cart dop hill" || cart== "cart spirit fire heart" || cart == "cart spirit poition heart" || cart == "cart spirit energy heart" || cart == "cart spirit ice heart"|| cart == "cart spirit rock heart"|| cart == "cart spirit blood heart")
        {
            hp_max += 5;
        }
        else if (cart == "cart berserk boss")
        {
            hp_max += 10;
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
        else if (cart == "cart mast")
        {
            col_art++;
        }
        else if (cart == "cart inven")
        {
            col_art += 2;

            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(tr.gameObject.name))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.sp_crystall || type == par_cart.type_cart.sp_barier || type == par_cart.type_cart.sp_cuznec || nm == "cart dungalorg(sleep) 1" || nm == "cart dungalorg 1")
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
        else if (cart == "cart power")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_sila_base.Add(tp.ToString());
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
        a_[1] = "1";
        a_[2] = "2";

        if (art1.type == par_cart.type_cart.art_rock_sp || art1.type == par_cart.type_cart.art_rock_act || art1.type == par_cart.type_cart.art_speed_rock || art1.type == par_cart.type_cart.art_pow_rock)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart rock" + a_[i]] -= 2;
            }
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
        else if (art1.type == par_cart.type_cart.art_blood_sp || art1.type == par_cart.type_cart.art_blood_act || art1.type == par_cart.type_cart.art_speed_blood || art1.type == par_cart.type_cart.art_pow_blood)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart blood" + a_[i]] -= 2;
            }
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
                    par_lt_speed_base.Remove(par_cart.type_cart.blood.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_fire_sp || art1.type == par_cart.type_cart.art_fire_act || art1.type == par_cart.type_cart.art_speed_fire || art1.type == par_cart.type_cart.art_pow_fire)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart fire" + a_[i]] -= 2;
            }
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
        else if (art1.type == par_cart.type_cart.art_energy_sp || art1.type == par_cart.type_cart.art_energy_act || art1.type == par_cart.type_cart.art_speed_energy || art1.type == par_cart.type_cart.art_pow_energy)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart energy" + a_[i]] -= 2;
            }
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
        else if (art1.type == par_cart.type_cart.art_ice_sp || art1.type == par_cart.type_cart.art_ice_act || art1.type == par_cart.type_cart.art_speed_ice || art1.type == par_cart.type_cart.art_pow_ice)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart ice" + a_[i]] -= 2;
            }
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
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_ice.ToString());
                    par_lt_speed_base.Remove(par_cart.type_cart.spirit_ice.ToString());
                }
            }
        }
        else if (art1.type == par_cart.type_cart.art_poition_sp || art1.type == par_cart.type_cart.art_poition_act || art1.type == par_cart.type_cart.art_speed_poition || art1.type == par_cart.type_cart.art_pow_poition)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart poition" + a_[i]] -= 2;
            }
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

        else if (art1.type == par_cart.type_cart.art_crystall_sp)
        {
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart crystall" + a_[i]] += 2;
            }
            //carts_count_max["cart crystall" + aa]--;
        }


        else if (art1.type == par_cart.type_cart.art_att_sp)
        {
            //carts_count_max["cart damage" + aa] -= 2;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart damage" + a_[i]] += 2;
            }
        }
        else if (art1.type == par_cart.type_cart.art_bar_sp)
        {
            //carts_count_max["cart barier" + aa]--;
            int a_f = ((par_cart)art1).lvl - (((par_cart)art1).lvl / 4);
            for (int i = 0; i < a_f; i++)
            {
                carts_count_max["cart barier" + a_[i]] += 2;
            }
        }
        else if (art1.type == par_cart.type_cart.art_base_st)
        {
            carts_count_max["cart damage"] -= 2;
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
            carts_count_max["cart derlorg"] += 2;
            hp_max += 2;
        }


        else if (cart == "cart dop hill" || cart == "cart spirit fire heart" || cart == "cart spirit poition heart" || cart == "cart spirit energy heart" || cart == "cart spirit ice heart" || cart == "cart spirit rock heart" || cart == "cart spirit blood heart")
        {
            hp_max -= 5;
            if (hp > hp_max)
                hp = hp_max;
        }
        else if (cart == "cart berserk boss")
        {
            hp_max -= 10;
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
        else if (cart == "cart mast")
        {
            col_art--;
        }
        else if (cart == "cart inven")
        {
            col_art -= 2;

            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                string nm = tr.gameObject.name;
                if (carts_count_max.ContainsKey(tr.gameObject.name))
                {
                    par_cart.type_cart type = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (type == par_cart.type_cart.sp_crystall || type == par_cart.type_cart.sp_barier || type == par_cart.type_cart.sp_cuznec || nm == "cart dungalorg(sleep) 1" || nm == "cart dungalorg 1")
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
        else if (cart == "cart power")
        {
            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
            {
                par_lt_sila_base.Remove(tp.ToString());
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
            if (func.hod_mob_now_int == num && friend)
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
        if (func.hod_mob_now_int == num && friend)
        {
            ui.print_carts_tip(num);
        }
    }
    public void ad_cart_active()
    {
        //string stt = null;
        //ad_cart_active(cart_active_now,lt_carts_active_now);
        if(friend && friend_type==type_friend.active && !atack_auto_comb)
            ad_cart_active(cart_active_now, lt_carts_active_now_ui);
        else
            ad_cart_active(cart_active_now, lt_carts_active_now);
    }

    public void ad_cart_active(string name,List<string> lt_carts_ad)
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
        {/*
            if (i == 0)
            {
                tr.localPosition = new Vector3(0, 0, 0);
                i += 3;
                i1 += 3;
                y_e = (i1 - 1) % 4;
            }
            else
            {
                if (i / 4 + 1 <= i1 / 4)
                    tr.localPosition = new Vector3(i / 4 * 0.28f, -0.75f + (i % 4) * 0.5f, 0);
                else
                    tr.localPosition = new Vector3(i / 4 * 0.28f, -0.25f * y_e + (i % 4) * 0.5f, 0);
            }
            i++;
            */
            //if (i == 4)
            //x_sm++;

            if (i == y_[x_now_])
            {
                i = 0;
                x_now_++;
            }
            tr.localPosition = new Vector3(x_now_ * 0.28f, -0.25f * (y_[x_now_]-1) + i * 0.5f, 0);

            i++;
        }
        gameObject.GetComponent<Animator>().SetBool("select_now", false);
        gameObject.GetComponent<Animator>().SetBool("perehod", true);
        selected_anim = false;
        cart_active_now = null;
        if ((!friend || friend_type==type_friend.auto || atack_auto_comb) && !nps)
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
        //skill_mob_now = null;
        //func.hod_next_mob = true;

    }
    public void activate_nps()
    {
        if (gameObject.name == "nps berserk")
        {
            par_mob pl = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
            pl.hp_max += func.carts[pl.x, pl.y].GetComponent<par_cart>().lvl;
            pl.hp += func.carts[pl.x, pl.y].GetComponent<par_cart>().lvl;
            Destroy(func.carts[pl.x, pl.y]);
            func.carts[pl.x, pl.y] = null;
        }
        if (gameObject.name == "nps derlorg")
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
    public bool proverka_atack(List<string> lt_carts_now,bool final,bool activate)
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
            if(lt_carts_now.Count==2 || (lt_carts_now.Count == 3 && lt_carts_now[1]== lt_carts_now[2]))
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
                if (now.next.Count > 0)
                    for (int i = 1; i < lt_carts_active_now_base.Count; i++)
                    {
                        if (now.next.ContainsKey(lt_carts_active_now_base[i]))
                            now = now.next[lt_carts_active_now_base[i]];
                        else bl1 = false;
                    }
                if (now != null && bl1 )
                //(lt_carts_active_now_base.Count == 0 || now.next.ContainsKey(lt_carts_active_now_base[lt_carts_active_now_base.Count-1])
                {
                    bl = true;
                    break;
                }
                //now1 = ct;
                //break;
            }


        }
        Debug.Log(bl + " bl two ");
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
            else
            {
                foreach (string st in lt_carts_sila_base)
                {
                    par_lt_sila.Remove(st);
                }
            }

        }
        Debug.Log(bl+" "+ lt_carts_sila.Count);
        return bl;
    }
    public int max_sila(object cart)
    {
        int col = 0;
        string cart_st = cart.ToString();//либо тип enum либо тип string
       
        foreach (string cart_sila in par_lt_sila)
        {
            //Debug.Log(cart_sila + " del " + lt_carts_sila.Count);



            if (cart_st==cart_sila)
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

            bool bl_art_boss_berserk = false;
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
                    pow_baf ++;
                    //break;
                }
            }
            List<string> lt_carts_active_now_base = new List<string>();
            Dictionary<string, int> lt_art_base = new Dictionary<string, int>();

            int par_cart_shaman = 0;
            Dictionary<par_cart.type_cart, bool> dic_el = new Dictionary<par_cart.type_cart, bool>();
            dic_el.Add(par_cart.type_cart.blood, lt_carts_active_now.ConvertAll(new System.Converter<string,par_cart.type_cart>(n =>
            {
                return GameObject.Find(n).GetComponent<par_cart>().type;
            })).Contains(par_cart.type_cart.blood));

            dic_el.Add(par_cart.type_cart.poition, lt_carts_active_now.ConvertAll(new System.Converter<string, par_cart.type_cart>(n =>
            {
                return GameObject.Find(n).GetComponent<par_cart>().type;
            })).Contains(par_cart.type_cart.poition));

            dic_el.Add(par_cart.type_cart.rock, lt_carts_active_now.ConvertAll(new System.Converter<string, par_cart.type_cart>(n =>
            {
                return GameObject.Find(n).GetComponent<par_cart>().type;
            })).Contains(par_cart.type_cart.rock));

            dic_el.Add(par_cart.type_cart.fire, lt_carts_active_now.ConvertAll(new System.Converter<string, par_cart.type_cart>(n =>
            {
                return GameObject.Find(n).GetComponent<par_cart>().type;
            })).Contains(par_cart.type_cart.fire));

            dic_el.Add(par_cart.type_cart.energy, lt_carts_active_now.ConvertAll(new System.Converter<string, par_cart.type_cart>(n =>
            {
                return GameObject.Find(n).GetComponent<par_cart>().type;
            })).Contains(par_cart.type_cart.energy));

            dic_el.Add(par_cart.type_cart.ice, lt_carts_active_now.ConvertAll(new System.Converter<string, par_cart.type_cart>(n =>
            {
                return GameObject.Find(n).GetComponent<par_cart>().type;
            })).Contains(par_cart.type_cart.ice));

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
                
                
                else if (cart.name == "cart mini spirit blood" && dic_el[par_cart.type_cart.blood])
                {
                    //par_cart_shaman.Add(cart);
                    par_cart_shaman++;
                }
                else if (cart.name == "cart mini spirit rock" && dic_el[par_cart.type_cart.rock])
                {
                    par_cart_shaman++;
                    //cart_shaman.Add(cart);
                }
                else if (cart.name == "cart mini spirit ice" && dic_el[par_cart.type_cart.ice])
                {
                    par_cart_shaman++;
                    //cart_shaman.Add(cart);
                }
                else if (cart.name == "cart mini spirit energy" && dic_el[par_cart.type_cart.energy])
                {
                    par_cart_shaman++;
                    //cart_shaman.Add(cart);
                }
                else if (cart.name == "cart mini spirit fire" && dic_el[par_cart.type_cart.fire])
                {
                    par_cart_shaman++;
                    //cart_shaman.Add(cart);
                }
                else if (cart.name == "cart mini spirit poition" && dic_el[par_cart.type_cart.poition])
                {
                    par_cart_shaman++;
                    //cart_shaman.Add(cart);
                }
                else if (cart.name == "cart mini spirit" && (
                    dic_el[par_cart.type_cart.poition] ||
                    dic_el[par_cart.type_cart.fire] ||
                    dic_el[par_cart.type_cart.rock] ||
                    dic_el[par_cart.type_cart.blood] ||
                    dic_el[par_cart.type_cart.ice] ||
                    dic_el[par_cart.type_cart.energy]))
                {
                    par_cart_shaman++;
                    //cart_shaman.Add(cart);
                }

                if (bl_art)
                {
                    if (lt_art_base.ContainsKey(cart.name))
                    {
                        lt_art_base[cart.name]++;
                    }
                    else
                    {



                        lt_art_base.Add(cart.name,1);
                    }
                }

            }

            foreach (string st_cart_1 in mb.lt_carts_active_now)
            {
                string st_cart = st_cart_1;
                int pw = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
                //if()

                par_cart.type_cart cart_type = GameObject.Find(st_cart).GetComponent<par_cart>().type;
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
                    pw = pw * 4 * par_cart_shaman;
                }

                bool cart_spetial = GameObject.Find(st_cart).GetComponent<par_cart>().spetial;
                par_cart.type_cart cart_type1 = par_cart.type_cart.bases;

                

                if (lt_carts_active_now_base.Count == 0)
                    lt_carts_active_now_base.Add(st_cart);
                else
                {
                    par_cart pr_cart1 = GameObject.Find(lt_carts_active_now_base[lt_carts_active_now_base.Count - 1]).GetComponent<par_cart>();
                    cart_type1 = pr_cart1.type;
                    if (cart_type != cart_type1)
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

                if (cart_type == par_cart.type_cart.fire)
                {
                    List<GameObject> lt_t = func.way_find_lt_in_list(func.lt_lovyshka, "run fire", x, y, 3);
                    dop_pow += lt_t.Count;
                }
                else if (cart_type == par_cart.type_cart.poition)
                {
                    List<GameObject> lt_t = func.way_find_lt_in_list(func.lt_lovyshka, "run poition", x, y, 3);
                    dop_pow += lt_t.Count;
                }
                else if (cart_type == par_cart.type_cart.ice)
                {
                    List<GameObject> lt_t = func.way_find_lt_in_list(func.lt_lovyshka, "run ice", x, y, 3);
                    dop_pow += lt_t.Count;
                }
                else if (cart_type == par_cart.type_cart.energy)
                {
                    List<GameObject> lt_t = func.way_find_lt_in_list(func.lt_lovyshka, "run energy", x, y, 3);
                    dop_pow += lt_t.Count;
                }
                else if (cart_type == par_cart.type_cart.blood)
                {
                    List<GameObject> lt_t = func.way_find_lt_in_list(func.lt_lovyshka, "run blood", x, y, 3);
                    dop_pow += lt_t.Count;
                }
                else if (cart_type == par_cart.type_cart.rock)
                {
                    List<GameObject> lt_t = func.way_find_lt_in_list(func.lt_lovyshka, "run rock", x, y, 3);
                    dop_pow += lt_t.Count;
                }
                dop_pow += pow_baf;

                List<par_cart_art> bl_cart_art_pow = new List<par_cart_art>();
                if (mb.lt_carts_active_now[0] != "cart cuznec")
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {
                        /*
                        if (cart.type == par_cart.type_cart.art_boss_shaman &&
                        (cart_type == par_cart.type_cart.rock || cart_type == par_cart.type_cart.blood || cart_type == par_cart.type_cart.fire || cart_type == par_cart.type_cart.poition || cart_type == par_cart.type_cart.ice || cart_type == par_cart.type_cart.energy))
                        {
                            dop_pow += 1;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        */
                        if (cart.name == "cart potoc poition" && cart_type == par_cart.type_cart.poition)
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.poition)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 4);
                        }
                        else if (cart.name == "cart potoc fire" && cart_type == par_cart.type_cart.fire)
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.fire)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 4);
                        }
                        else if (cart.name == "cart potoc rock" && cart_type == par_cart.type_cart.rock)
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.rock)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 4);
                        }
                        else if (cart.name == "cart potoc blood" && cart_type == par_cart.type_cart.blood)
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.blood)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 4);
                        }
                        else if (cart.name == "cart potoc energy" && cart_type == par_cart.type_cart.energy)
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.energy)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 4);
                        }
                        else if (cart.name == "cart potoc ice" && cart_type == par_cart.type_cart.ice)
                        {
                            int pow_art = 0;
                            foreach (string st in lt_carts)
                            {
                                par_cart cart_now = GameObject.Find(st).GetComponent<par_cart>();
                                if (cart_now.type == par_cart.type_cart.poition)
                                    pow_art += cart_now.lvl;
                            }
                            dop_pow += (pow_art / 4);
                        }







                        if (cart.name == "cart poition st" && cart_type == par_cart.type_cart.poition)
                        {
                            foreach (par_cart_art cart_ in mb.lt_carts_art)
                            {
                                if (cart.perez_now > 1)
                                    cart.perez_now--;
                                if (cart.perez_now < 0)
                                    cart.perez_now = 0;
                            }

                        }
                        else if (cart.name == "cart fire st" && cart_type == par_cart.type_cart.fire)
                        {
                            foreach (par_cart_art cart_ in mb.lt_carts_art)
                            {
                                if (cart.perez_now > 1)
                                    cart.perez_now--;
                                if (cart.perez_now < 0)
                                    cart.perez_now = 0;
                            }
                        }
                        else if (cart.name == "cart rock st" && cart_type == par_cart.type_cart.rock)
                        {
                            foreach (par_cart_art cart_ in mb.lt_carts_art)
                            {
                                if (cart.perez_now > 1)
                                    cart.perez_now--;
                                if (cart.perez_now < 0)
                                    cart.perez_now = 0;
                            }
                        }
                        else if (cart.name == "cart blood st" && cart_type == par_cart.type_cart.blood)
                        {
                            foreach (par_cart_art cart_ in mb.lt_carts_art)
                            {
                                if (cart.perez_now > 1)
                                    cart.perez_now--;
                                if (cart.perez_now < 0)
                                    cart.perez_now = 0;
                            }
                        }
                        else if (cart.name == "cart energy st" && cart_type == par_cart.type_cart.energy)
                        {
                            foreach (par_cart_art cart_ in mb.lt_carts_art)
                            {
                                if (cart.perez_now > 1)
                                    cart.perez_now--;
                                if (cart.perez_now < 0)
                                    cart.perez_now = 0;
                            }
                        }
                        else if (cart.name == "cart ice st" && cart_type == par_cart.type_cart.ice)
                        {
                            foreach (par_cart_art cart_ in mb.lt_carts_art)
                            {
                                if (cart.perez_now > 1)
                                    cart.perez_now--;
                                if (cart.perez_now < 0)
                                    cart.perez_now = 0;
                            }
                        }


                        else if (cart.name == "cart mast" || cart.name == "cart derlorg st") {
                            foreach (par_cart_art cart_ in mb.lt_carts_art)
                            {
                                if (cart_spetial)
                                {
                                    if (cart.perez_now > 1)
                                        cart.perez_now -= 2;
                                    if (cart.perez_now < 0)
                                        cart.perez_now = 0;
                                }
                                else
                                {
                                    if (cart.perez_now > 1)
                                        cart.perez_now--;
                                    if (cart.perez_now < 0)
                                        cart.perez_now = 0;
                                }
                            }

                        } 


                        else if (cart.q > 0)
                        {
                            if (bl_att_hod)
                            {
                                /*
                                if (cart.type == par_cart.type_cart.art_speed_rock && cart_type == par_cart.type_cart.rock)
                                {
                                    //mb.activate_art(cart, mb.x, mb.y);
                                    art_att_hod.Enqueue(cart);
                                }
                                
                                //else if (cart.name == "cart speed rock 1" && mb.lt_carts_active_now[0] == "cart rock 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_fire && cart_type == par_cart.type_cart.fire)
                                {
                                    //mb.activate_art(cart, mb.x, mb.y);
                                    art_att_hod.Enqueue(cart);
                                }
                                //else if (cart.name == "cart speed fire 1" && mb.lt_carts_active_now[0] == "cart fire 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_energy && cart_type == par_cart.type_cart.energy)
                                {
                                    //mb.activate_art(cart, mb.x, mb.y);
                                    art_att_hod.Enqueue(cart);
                                }
                                //else if (cart.name == "cart speed energy 1" && mb.lt_carts_active_now[0] == "cart energy 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_blood && cart_type == par_cart.type_cart.blood)
                                {
                                    //mb.activate_art(cart, mb.x, mb.y);
                                    art_att_hod.Enqueue(cart);
                                }
                                //else if (cart.name == "cart speed blood 1" && mb.lt_carts_active_now[0] == "cart blood 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_ice && cart_type == par_cart.type_cart.ice)
                                {
                                    //mb.activate_art(cart, mb.x, mb.y);
                                    art_att_hod.Enqueue(cart);
                                }
                                //else if (cart.name == "cart speed ice 1" && mb.lt_carts_active_now[0] == "cart ice 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_poition && cart_type == par_cart.type_cart.poition)
                                {
                                    //mb.activate_art(cart, mb.x, mb.y);
                                    art_att_hod.Enqueue(cart);
                                }
                                //else if (cart.name == "cart speed poition 1" && mb.lt_carts_active_now[0] == "cart poition 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                else if (cart.name == "cart speed")
                                {
                                    //mb.activate_art(cart, mb.x, mb.y);
                                    //art_att_hod.Enqueue(cart);
                                }
                                */
                                //lt.Add(cart.type);
                            }
                        }
                        else if (cart.perez_now == 0)
                        {


                            /*
                            else if (cart.name == "cart act rock 1" && cart_type == par_cart.type_cart.poition)
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }


                            else if (cart.name == "cart act fire 1" && st_cart == "cart fire 1")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }

                            else if (cart.name == "cart act energy" && st_cart == "cart energy")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }
                            else if (cart.name == "cart act energy 1" && st_cart == "cart energy 1")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }

                            else if (cart.name == "cart act blood" && st_cart == "cart blood")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }
                            else if (cart.name == "cart act blood 1" && st_cart == "cart blood 1")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }

                            else if (cart.name == "cart act ice" && st_cart == "cart ice")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }
                            else if (cart.name == "cart act ice 1" && st_cart == "cart ice 1")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }

                            else if (cart.name == "cart act poition" && st_cart == "cart poition")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }

                            else if (cart.name == "cart act poition 1" && st_cart == "cart poition 1")
                            {
                                dop_pow *= 2;
                                mb.activate_art(cart, x_cl, y_cl);
                            }
                            */
                            /*
                            if (cart.name == "cart berserk")
                            {
                                dop_pow += ((mb.hp_max - mb.hp) / 5 + 1) * pow_base;
                                mb.activate_art(cart, x_cl, y_cl);
                            }
                            */
                            /*
                            if (cart.name == "cart power")
                            {
                                dop_pow += pow_base;

                                bl_cart_art_pow.Add(cart);
                                //mb.activate_art(cart, x_cl, y_cl);
                            }
                            */
/*
                            else if (cart.name == "cart berserk boss")
                            {
                                bl_art_boss_berserk = true;
                                //dop_pow *= 4;
                                //mb.activate_art(cart, x_cl, y_cl);
                            }
*/
                            if (cart.name == "cart dungalorg heard")
                            {
                                bl_art_boss_dungalorg = cart;
                                //dop_pow *= 4;
                                //mb.activate_art(cart, x_cl, y_cl);
                            }




                            //else if (pw == cart.lvl)
                            else
                            {
                                //int pow_lvl = cart.lvl - pw;

                                //if (pow_lvl >= 0)
                                //{
                                /*
                                int pow_lvl = (int)Mathf.Pow(2, cart.lvl) - 1;
                                if (cart.type == par_cart.type_cart.art_rock_act && cart_type == par_cart.type_cart.rock)
                                {
                                    dop_pow += pow_base * pow_lvl;
                                    mb.activate_art(cart, x_cl, y_cl);
                                }
                                else if (cart.type == par_cart.type_cart.art_fire_act && cart_type == par_cart.type_cart.fire)
                                {
                                    dop_pow += pow_base * pow_lvl;
                                    mb.activate_art(cart, x_cl, y_cl);
                                }
                                else if (cart.type == par_cart.type_cart.art_blood_act && cart_type == par_cart.type_cart.blood)
                                {
                                    dop_pow += pow_base * pow_lvl;
                                    mb.activate_art(cart, x_cl, y_cl);
                                }
                                else if (cart.type == par_cart.type_cart.art_energy_act && cart_type == par_cart.type_cart.energy)
                                {
                                    dop_pow += pow_base * pow_lvl;
                                    mb.activate_art(cart, x_cl, y_cl);
                                }
                                else if (cart.type == par_cart.type_cart.art_ice_act && cart_type == par_cart.type_cart.ice)
                                {
                                    dop_pow += pow_base * pow_lvl;
                                    mb.activate_art(cart, x_cl, y_cl);
                                }
                                else if (cart.type == par_cart.type_cart.art_poition_act && cart_type == par_cart.type_cart.poition)
                                {
                                    dop_pow += pow_base * pow_lvl;
                                    mb.activate_art(cart, x_cl, y_cl);
                                }
                                */
                                //}

                            }
                        }
                        if (cart.type == par_cart.type_cart.art_pow_rock && cart_type == par_cart.type_cart.rock)
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_fire && cart_type == par_cart.type_cart.fire)
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_blood && cart_type == par_cart.type_cart.blood)
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_energy && cart_type == par_cart.type_cart.energy)
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_ice && cart_type == par_cart.type_cart.ice)
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_pow_poition && cart_type == par_cart.type_cart.poition)
                        {
                            dop_pow += cart.lvl;
                            //mb.activate_art(cart, x_cl, y_cl);
                        }
                        else if (cart.type == par_cart.type_cart.art_att_pow && cart_type == par_cart.type_cart.damage)
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
                if (art_att_hod.Count != 0)
                {

                    /*
                    par_cart_art art1 = null;
                    par_cart_art art2 = null;
                    foreach (par_cart_art art_now in art_att_hod)
                    {
                        if (art_now.name == "cart speed" && art2==null)
                            art2 = art_now;
                        else if (art1 == null)
                            art1 = art_now;
                    }
                    if(art2!=null)
                        mb.activate_art(art2, mb.x, mb.y);
                    else
                        mb.activate_art(art1, mb.x, mb.y);

                    */
                    //bl_att_hod = false;
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
                    if (cart_type == par_cart.type_cart.rock && data.data_now.cl_cart_rock < 100)
                    {
                        data.data_now.cl_cart_rock++;
                        if (data.data_now.cl_cart_rock == 100)
                        {
                            data.data_now.lt_players.Add(data.players.rock);
                            data.give_achive("сила камня");

                        }
                    }
                    if (cart_type == par_cart.type_cart.blood && data.data_now.cl_cart_blood < 100)
                    {
                        data.data_now.cl_cart_blood++;
                        if (data.data_now.cl_cart_blood == 100)
                        {
                            data.data_now.lt_players.Add(data.players.blood);
                            data.give_achive("сила крови");

                        }
                    }
                    if (cart_type == par_cart.type_cart.fire && data.data_now.cl_cart_fire < 100)
                    {
                        data.data_now.cl_cart_fire++;
                        if (data.data_now.cl_cart_fire == 100)
                        {
                            data.data_now.lt_players.Add(data.players.fire);
                            data.give_achive("сила огня");

                        }
                    }
                    if (cart_type == par_cart.type_cart.energy && data.data_now.cl_cart_energy < 100)
                    {
                        data.data_now.cl_cart_energy++;
                        if (data.data_now.cl_cart_energy == 100)
                        {
                            data.data_now.lt_players.Add(data.players.energy);
                            data.give_achive("сила электричества");
                            //data.data_now.lt_players.Add(data.players.energy);
                        }
                    }
                    if (cart_type == par_cart.type_cart.ice && data.data_now.cl_cart_ice < 100)
                    {
                        data.data_now.cl_cart_ice++;
                        if (data.data_now.cl_cart_ice == 100)
                        {
                            data.data_now.lt_players.Add(data.players.ice);
                            data.give_achive("сила льда");

                        }
                    }
                    if (cart_type == par_cart.type_cart.poition && data.data_now.cl_cart_poition < 100)
                    {
                        data.data_now.cl_cart_poition++;
                        if (data.data_now.cl_cart_poition == 100)
                        {
                            data.data_now.lt_players.Add(data.players.poition);
                            data.give_achive("сила яда");

                        }
                    }


                    if (data.data_now.pl_name_now == "berserk player")
                    {
                        //dop_pow *=2;
                        //mb.damage(1, "none");
                    }
                    else if (cart_type == par_cart.type_cart.damage && data.data_now.pl_name_now == "atack player")
                    {
                        //dop_pow *=2;
                        dop_pow += pow_base;
                    }

                    else if (cart_type == par_cart.type_cart.sp_barier && data.data_now.pl_name_now == "barier player")
                    {
                        dop_pow += pow_base;
                    }
                    else if (cart_type == par_cart.type_cart.rock && data.data_now.pl_name_now == "rock player")
                    {
                        dop_pow += pow_base;
                    }
                    else if (cart_type == par_cart.type_cart.fire && data.data_now.pl_name_now == "fire player")
                    {
                        dop_pow += pow_base;
                    }
                    else if (cart_type == par_cart.type_cart.blood && data.data_now.pl_name_now == "blood player")
                    {
                        dop_pow += pow_base;
                    }
                    else if (cart_type == par_cart.type_cart.energy && data.data_now.pl_name_now == "energy player")
                    {
                        dop_pow += pow_base;
                    }
                    else if (cart_type == par_cart.type_cart.ice && data.data_now.pl_name_now == "ice player")
                    {
                        dop_pow += pow_base;
                    }
                    else if (cart_type == par_cart.type_cart.poition && data.data_now.pl_name_now == "poition player")
                    {
                        dop_pow += pow_base;
                    }



                    else if (data.data_now.pl_name_now == "derlorg player")
                    {
                        int i_ = 0;
                        foreach (string st_d in mb.lt_carts)
                        {
                            if (st_d == "cart derlorg")
                            {
                                i_++;

                            }
                        }
                        dop_pow += i_ / 4;
                    }
                }




                if (cart_type != par_cart.type_cart.derlorg)
                {
                    pow_rez += dop_pow;
                }

                else
                {
                    pow_rez_seal += dop_pow;
                }
            }

            List<string> lt_carts_speed = new List<string>(lt_carts_active_now_base);
            string cart_speed_base = null;
            foreach (string cart_speed in par_lt_speed)
            {
                //Debug.Log(cart_sila + " del " + lt_carts_sila.Count);



                if (lt_carts_speed.Contains(cart_speed))
                {
                    lt_carts_speed.Remove(cart_speed);
                    cart_speed_base = cart_speed;
                }

                else
                {
                    par_cart.type_cart tp1 = par_cart.type_cart.bases;
                    bool bl_type = false;
                    try
                    {
                        foreach (string st_s in lt_carts_speed)
                        {
                            if (GameObject.Find(st_s).GetComponent<par_cart>().type.ToString() == cart_speed)
                            {
                                lt_carts_speed.Remove(st_s);
                                cart_speed_base = cart_speed;
                                break;
                            }
                        }
                        //tp1 = GameObject.Find("tp1").GetComponent<par_cart>().type;
                    }
                    catch (System.Exception)
                    {


                    }
                }
                if (cart_speed_base != null)
                {

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

            /*
            if (pow_rez >= 8 && gameObject.name == data.data_now.pl_name_now)
                data.give_achive("большой потенциал");
            */
            bool bl_att = false;

            if (lt_carts_active_now_base[0] == "cart crystall")
            {
                if (func.nature[x_cl, y_cl] == null)
                {

                    mb.effect_now = func.create_effect(lt_carts_active_now_base[0], x_cl, y_cl, mb.gameObject, false, pow_rez, lt_carts_active_now_base);


                    List<string> lt = new List<string>();
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {
                        /*
                        if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                        {
                            //foreach (par_cart_art cart1 in lt_carts_art)
                            //{
                            foreach (par_cart_art cart1 in mb.lt_carts_art)
                            {
                                if (cart1.perez_now > 1)
                                    cart1.perez_now -= 2;
                                if (cart1.perez_now < 0)
                                    cart1.perez_now = 0;
                            }
                            //}
                        }
                        */
                        if (cart.perez_now == 0)
                        {
                            /*
                            else if (!lt.Contains(cart.name))
                            {
                                if (cart.name == "cart speed")
                                    mb.activate_art(cart, mb.x, mb.y);
                                lt.Add(cart.name);
                            }

                            */


                        }
                    }
                    //mb.effect_now.GetComponent<par_effect>().st_par.Push("crystall");
                    //Debug.Log(effect_now ==null);
                    //mb.effect_now.GetComponent<par_effect>().parent = ;
                    mb.effect_activate = lt_carts_active_now_base[0];


                    //mb.lt_carts_active_now.RemoveAt(0);
                    

                    bl_att = true;
                    //anim.col_anim++;
                    //func.next_hod();
                }

            }
            else if (lt_carts_active_now_base[0] == "cart dungalorg(sleep)" && lt_carts_active_now_base.Count == 1)
            {

                //func.mob[x, y].GetComponent<par_mob>().add_baf(par_baf.type_baf.bar, -1, 1);

                //string cart_create = null;
                //string carts_cr = mb.lt_carts_active_now[0];



                // mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (mb_click != null)
                {
                    for (int i = 0; i < pow_rez; i++)
                    {
                        mb_click.spawn_cart("cart dungalorg", gameObject.transform.Find("carts active").gameObject);
                    }
                    /*
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {

                        if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                        {
                            //foreach (par_cart_art cart1 in lt_carts_art)
                            //{
                            foreach (par_cart_art cart1 in mb.lt_carts_art)
                            {
                                if (cart1.perez_now > 1)
                                    cart1.perez_now--;
                                if (cart1.perez_now < 0)
                                    cart1.perez_now = 0;
                            }
                            //}
                        }

                    }
                    */
                }





                //mb.lt_carts_active_now.RemoveAt(0);
                bl_att = true;
                func.nums_mob_wolk.Add(num);
                speed_hod = true;
                //bl_att_hod = false;
                //anim.col_anim++;
                //func.next_hod();

            }
            else if (lt_carts_active_now_base[0] == "cart dungalorg(sleep) 1" && lt_carts_active_now_base.Count == 1)
            {
                Debug.Log(pow_rez + " rez pow");
                //func.mob[x, y].GetComponent<par_mob>().add_baf(par_baf.type_baf.bar, -1, 1);

                //string cart_create = null;
                //string carts_cr = mb.lt_carts_active_now[0];



                // mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (mb_click != null)
                {
                    for (int i = 0; i < pow_rez; i++)
                    {
                        mb_click.spawn_cart("cart dungalorg 1", gameObject.transform.Find("carts active").gameObject);
                    }
                    /*
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {
                        if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                        {
                            //foreach (par_cart_art cart1 in lt_carts_art)
                            //{
                            foreach (par_cart_art cart1 in mb.lt_carts_art)
                            {
                                if (cart1.perez_now > 1)
                                    cart1.perez_now -= 2;
                                if (cart1.perez_now < 0)
                                    cart1.perez_now = 0;
                            }
                            //}
                        }

                    }*/
                }





                //mb.lt_carts_active_now.RemoveAt(0);
                bl_att = true;
                func.nums_mob_wolk.Add(num);
                speed_hod = true;
                //bl_att_hod = false;
                //anim.col_anim++;
                //func.next_hod();

            }
            else if (lt_carts_active_now_base.Count == 1)
            {
                bool bl_first = false;
                //par_cart
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
                if (bl_first)
                {
                    int pw = GameObject.Find(lt_carts_active_now_base[0]).GetComponent<par_cart>().lvl;
                    /*
                    if (mb.lt_carts_active_now[0] == "cart barier" || mb.lt_carts_active_now[0] == "cart barier 1")
                    {
                        if (mb_click != null)
                        {
                            //if (data.data_now.pl_name_now == "barier player")
                            //dop_pow_mn *= 2;
                            mb_click.add_baf(par_baf.type_baf.bar, -1, pow_rez * 3);

                        }
                    }
                    */
                    int dop_pow_rez = 1;
                    if (bl_art_boss_berserk)
                        dop_pow_rez = 4;

                    string bl_dych = null;
                    par_cart cart_bl_dych = null;
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {
                        /*
                        if (cart.q > 0)
                        {
                            par_cart.type_cart type_el = GameObject.Find(lt_carts_active_now_base[0]).GetComponent<par_cart>().type;
                            if (cart.name == "cart shaman boss" && func.object_bool(x_cl, y_cl))
                            {
                                if (type_el == par_cart.type_cart.fire)
                                    bl_dych = "spawn dych fire";
                                if (type_el == par_cart.type_cart.poition)
                                    bl_dych = "spawn dych poition";
                                if (type_el == par_cart.type_cart.ice)
                                    bl_dych = "spawn dych ice";
                                if (type_el == par_cart.type_cart.energy)
                                    bl_dych = "spawn dych energy";
                                if (type_el == par_cart.type_cart.blood)
                                    bl_dych = "spawn dych blood";
                                if (type_el == par_cart.type_cart.rock)
                                    bl_dych = "spawn dych rock";
                                //dop_pow *= 4;
                                //bl_dych = type_el;
                                cart_bl_dych = cart;

                                Stack<object> st = new Stack<object>();
                                st.Push(1);
                                st.Push("par -q");

                                mb.activate_art(cart, x_cl, y_cl, st);
                                break;
                            }

                        }
                        */
                    }
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




                    //Debug.Log("aaaaaaaccccccttt 0 " + num + " ");

                    //mb.lt_carts_active_now.RemoveAt(0);

                    //anim.col_anim++;
                    bl_att = true;
                }

                //func.next_hod();
            }
            else if (lt_carts_active_now_base[0] == "cart damage" || lt_carts_active_now_base[0] == "cart damage 1")
            {
                //Debug.Log("cart atack is activated");
                string nm = null;
                string cart1 = lt_carts_active_now_base[1];
                //par_baf.type_baf type_bf = par_baf.type_baf.bar;
                nm = func.get_name_effect(lt_carts_active_now_base, null);
                /*
                if (cart1 == "cart rock" || cart1 == "cart rock 1")
                {
                    nm = "atack rock";
                    //type_bf = par_baf.type_baf.bar_rock;
                }

                else if (cart1 == "cart fire" || cart1 == "cart fire 1")
                {
                    nm = "atack fire";
                    //type_bf = par_baf.type_baf.bar_fire;
                }

                else if (cart1 == "cart energy" || cart1 == "cart energy 1")
                {
                    //type_bf = par_baf.type_baf.bar_energy;
                    nm = "atack energy";
                }

                else if (cart1 == "cart blood" || cart1 == "cart blood 1")
                {
                    //type_bf = par_baf.type_baf.bar_blood;
                    nm = "atack blood";
                }
                else if (cart1 == "cart ice" || cart1 == "cart ice 1")
                {
                    //type_bf = par_baf.type_baf.bar_ice;
                    nm = "atack ice";
                }
                else if (cart1 == "cart poition" || cart1 == "cart poition 1")
                {
                    //type_bf = par_baf.type_baf.bar_poition;
                    nm = "atack poition";
                }
                */
                //par_baf.type_baf type = GameObject.Find(cart1).GetComponent<par_cart>().type
                int lv = GameObject.Find(cart1).GetComponent<par_cart>().lvl;
                //int lv1 = GameObject.Find(cart1).GetComponent<par_cart>().lvl;

                int dop_pow_rez = 1;
                if (bl_art_boss_berserk)
                    dop_pow_rez = 4;
                mb.effect_now = func.create_effect(nm, x_cl, y_cl, mb.gameObject, false, pow_rez * dop_pow_rez, lt_carts_active_now_base);
                //Debug.Log(effect_now ==null);
                //mb.effect_now.GetComponent<par_effect>().parent = ;
                mb.effect_activate = nm;

                //mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                //Debug.Log("aaaaaaaccccccttt 0 " + num + " ");

                List<par_cart.type_cart> lt = new List<par_cart.type_cart>();
                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);

                bl_att = true;
                //anim.col_anim++;
                //func.next_hod();
            }
            else if (lt_carts_active_now_base[0] == "cart barier" || lt_carts_active_now_base[0] == "cart barier 1")
            {
                string nm = null;
                string cart1 = lt_carts_active_now_base[1];
                //par_baf.type_baf type_bf = par_baf.type_baf.bar;
                /*
                if (cart1 == "cart rock" || cart1 == "cart rock 1")
                {
                    nm = "barier rock";
                    //type_bf = par_baf.type_baf.bar_rock;
                }

                else if (cart1 == "cart fire" || cart1 == "cart fire 1")
                {
                    nm = "barier fire";
                    //type_bf = par_baf.type_baf.bar_fire;
                }

                else if (cart1 == "cart energy" || cart1 == "cart energy 1")
                {
                    //type_bf = par_baf.type_baf.bar_energy;
                    nm = "barier energy";
                }

                else if (cart1 == "cart blood" || cart1 == "cart blood 1")
                {
                    //type_bf = par_baf.type_baf.bar_blood;
                    nm = "barier blood";
                }
                else if (cart1 == "cart ice" || cart1 == "cart ice 1")
                {
                    //type_bf = par_baf.type_baf.bar_ice;
                    nm = "barier ice";
                }
                else if (cart1 == "cart poition" || cart1 == "cart poition 1")
                {
                    //type_bf = par_baf.type_baf.bar_poition;
                    nm = "barier poition";
                }
                */

                nm = func.get_name_effect(lt_carts_active_now_base, null);
                //par_baf.type_baf type = GameObject.Find(cart1).GetComponent<par_cart>().type
                int lv = GameObject.Find(cart1).GetComponent<par_cart>().lvl;
                //int lv1 = GameObject.Find(cart1).GetComponent<par_cart>().lvl;

                int dop_pow_rez = 1;
                if (bl_art_boss_berserk)
                    dop_pow_rez = 4;
                mb.effect_now = func.create_effect(nm, x_cl, y_cl, mb.gameObject, false, pow_rez * dop_pow_rez, lt_carts_active_now_base);
                /*
                if (mb_click != null)
                {

                    int pw = GameObject.Find(mb.lt_carts_active_now[0]).GetComponent<par_cart>().lvl;
                    int pw1 = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().lvl;




                    //func.mob[x, y].GetComponent<par_mob>().add_baf(par_baf.type_baf.bar, -1, 1);
                    if (pow_rez >= 8 && gameObject.name == data.data_now.pl_name_now)
                        data.give_achive("большой потенциал");
                    pow_rez *= 3;
                    if (mb.lt_carts_active_now[1] == "cart rock" || mb.lt_carts_active_now[1] == "cart rock 1")
                        mb_click.add_baf(par_baf.type_baf.bar_rock, -1, pow_rez);
                    else if (mb.lt_carts_active_now[1] == "cart blood" || mb.lt_carts_active_now[1] == "cart blood 1")
                        mb_click.add_baf(par_baf.type_baf.bar_blood, -1, pow_rez);
                    else if (mb.lt_carts_active_now[1] == "cart fire" || mb.lt_carts_active_now[1] == "cart fire 1")
                        mb_click.add_baf(par_baf.type_baf.bar_fire, -1, pow_rez);
                    else if (mb.lt_carts_active_now[1] == "cart energy" || mb.lt_carts_active_now[1] == "cart energy 1")
                        mb_click.add_baf(par_baf.type_baf.bar_energy, -1, pow_rez);
                    else if (mb.lt_carts_active_now[1] == "cart ice" || mb.lt_carts_active_now[1] == "cart ice 1")
                        mb_click.add_baf(par_baf.type_baf.bar_ice, -1, pow_rez);
                    else if (mb.lt_carts_active_now[1] == "cart poition" || mb.lt_carts_active_now[1] == "cart poition 1")
                        mb_click.add_baf(par_baf.type_baf.bar_poition, -1, pow_rez);

                }
                */



                /*
                List<par_cart.type_cart> lt = new List<par_cart.type_cart>();
                foreach (par_cart_art cart in mb.lt_carts_art)
                {

                    if (cart.perez_now == 0 || cart.q > 0)
                    {

                        if (cart.name == "cart berserk boss")
                        {
                            //dop_pow *= 4;
                            Stack<object> st = new Stack<object>();
                            st.Push(pow_rez);
                            st.Push("par_cart berserk boss");
                            mb.activate_art(cart, x_cl, y_cl, st);
                            //Debug.Break();
                        }
                        else if (!lt.Contains(cart.type))
                        {
                            par_cart.type_cart cart_type_now = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().type;

                            if (cart.type == par_cart.type_cart.art_speed_rock && cart_type_now == par_cart.type_cart.rock)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed rock 1" && mb.lt_carts_active_now[0] == "cart rock 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_fire && cart_type_now == par_cart.type_cart.fire)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed fire 1" && mb.lt_carts_active_now[0] == "cart fire 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_energy && cart_type_now == par_cart.type_cart.energy)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed energy 1" && mb.lt_carts_active_now[0] == "cart energy 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_blood && cart_type_now == par_cart.type_cart.blood)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed blood 1" && mb.lt_carts_active_now[0] == "cart blood 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_ice && cart_type_now == par_cart.type_cart.ice)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed ice 1" && mb.lt_carts_active_now[0] == "cart ice 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_poition && cart_type_now == par_cart.type_cart.poition)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed poition 1" && mb.lt_carts_active_now[0] == "cart poition 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            else if (cart.name == "cart speed")
                                mb.activate_art(cart, mb.x, mb.y);
                            lt.Add(cart.type);
                        }


                    }
                }
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    bool bl1 = (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock") ||
                        (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock 1") ||

                        (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood") ||
                        (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood 1") ||

                        (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire") ||
                        (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire 1") ||

                        (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice") ||
                        (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice 1") ||

                        (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy") ||
                        (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy 1") ||

                        (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition") ||
                        (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition 1") ||

                        (cart.name == "cart bar st")


                        ;
                    if (cart.perez_now == 0)
                    {

                    }
                    if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                    {
                        foreach (par_cart_art cart11 in mb.lt_carts_art)
                        {
                            if (cart11.perez_now > 1)
                                cart11.perez_now -= 3;
                            if (cart11.perez_now < 0)
                                cart11.perez_now = 0;
                        }
                    }
                    else if (bl1)
                    {
                        //foreach (par_cart_art cart1 in lt_carts_art)
                        //{
                        foreach (par_cart_art cart11 in mb.lt_carts_art)
                        {
                            if (cart11.perez_now > 1)
                                cart11.perez_now--;
                        }
                    }
                }
                */
                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);
                bl_att = true;
                //anim.col_anim++;
                //func.next_hod();
            }

            else if (lt_carts_active_now_base[0] == "cart derlorg" || lt_carts_active_now_base[0] == "cart derlorg 1")
            {
                //Debug.Log("cart atack is activated");
                string nm = null;
                string cart1 = lt_carts_active_now_base[1];
                //par_baf.type_baf type_bf = par_baf.type_baf.bar;
                nm = func.get_name_effect(lt_carts_active_now_base, null);

                //par_baf.type_baf type = GameObject.Find(cart1).GetComponent<par_cart>().type
                int lv = GameObject.Find(cart1).GetComponent<par_cart>().lvl;
                //int lv1 = GameObject.Find(cart1).GetComponent<par_cart>().lvl;

                int dop_pow_rez = 1;
                if (bl_art_boss_berserk)
                    dop_pow_rez = 4;
                mb.effect_now = func.create_effect(nm, x_cl, y_cl, mb.gameObject, false, pow_rez_seal * dop_pow_rez, lt_carts_active_now_base);
                mb.effect_now.GetComponent<par_effect>().st_par.Push((pow_rez * dop_pow_rez).ToString());
                mb.effect_now.GetComponent<par_effect>().st_par.Push("par pow");
                //Debug.Log(effect_now ==null);
                //mb.effect_now.GetComponent<par_effect>().parent = ;
                mb.effect_activate = nm;

                //mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                //Debug.Log("aaaaaaaccccccttt 0 " + num + " ");
                /*
                List<par_cart.type_cart> lt = new List<par_cart.type_cart>();
                foreach (par_cart_art cart in mb.lt_carts_art)
                {

                    if (cart.perez_now == 0 || cart.q > 0)
                    {
                        if (cart.name == "cart berserk boss")
                        {
                            //dop_pow *= 4;
                            Stack<object> st = new Stack<object>();
                            st.Push(pow_rez);
                            st.Push("par pow berserk boss");
                            st.Push(pow_rez_seal);
                            st.Push("par_cart berserk boss");
                            mb.activate_art(cart, x_cl, y_cl, st);
                            //Debug.Break();
                        }


                        /*
                        if (cart.name == "cart att bar")
                        {
                            mb.add_baf(type_bf, -1, lv + 1);

                        }
                        //mb.activate_art(cart, mb.x, mb.y);

                        else if (cart.name == "cart bar att")
                        {
                            int pow_bar = 0;

                            foreach (par_baf bf in mb.lt_bafs)
                            {
                                if (bf.type == type_bf)
                                {
                                    pow_bar += bf.power;

                                }
                                //mb.activate_art(cart, mb.x, mb.y);
                            }
                            if (pow_bar > 0)
                            {
                                create_effect(nm, x_cl, y_cl, mb.gameObject, false, pow_bar);
                            }
                        }
                        
                        else if (!lt.Contains(cart.type))
                        {
                            par_cart.type_cart cart_type_now = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().type;

                            if (cart.type == par_cart.type_cart.art_speed_rock && cart_type_now == par_cart.type_cart.rock)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed rock 1" && mb.lt_carts_active_now[0] == "cart rock 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_fire && cart_type_now == par_cart.type_cart.fire)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed fire 1" && mb.lt_carts_active_now[0] == "cart fire 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_energy && cart_type_now == par_cart.type_cart.energy)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed energy 1" && mb.lt_carts_active_now[0] == "cart energy 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_blood && cart_type_now == par_cart.type_cart.blood)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed blood 1" && mb.lt_carts_active_now[0] == "cart blood 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_ice && cart_type_now == par_cart.type_cart.ice)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed ice 1" && mb.lt_carts_active_now[0] == "cart ice 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            if (cart.type == par_cart.type_cart.art_speed_poition && cart_type_now == par_cart.type_cart.poition)
                                mb.activate_art(cart, mb.x, mb.y);
                            //else if (cart.name == "cart speed poition 1" && mb.lt_carts_active_now[0] == "cart poition 1")
                            //mb.activate_art(cart, mb.x, mb.y);

                            else if (cart.name == "cart speed")
                                mb.activate_art(cart, mb.x, mb.y);
                            lt.Add(cart.type);
                        }


                    }
                }
                */
                /*
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    bool bl1 = (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock") ||
                        (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock 1") ||

                        (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood") ||
                        (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood 1") ||

                        (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire") ||
                        (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire 1") ||

                        (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice") ||
                        (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice 1") ||

                        (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy") ||
                        (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy 1") ||

                        (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition") ||
                        (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition 1") ||

                        (cart.name == "cart att st")
                        ;

                    if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                    {
                        //foreach (par_cart_art cart1 in lt_carts_art)
                        //{
                        par_cart.type_cart cart_type_now = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().type;
                        foreach (par_cart_art cart11 in mb.lt_carts_art)
                        {

                            if (cart_type_now == par_cart.type_cart.sp_barier || cart_type_now == par_cart.type_cart.sp_crystall)
                            {

                                if (cart11.perez_now > 1)
                                    cart11.perez_now -= 3;
                                if (cart11.perez_now < 1)
                                    cart11.perez_now = 0;
                            }
                            else
                            {

                                if (cart11.perez_now > 1)
                                    cart11.perez_now -= 2;
                                if (cart11.perez_now < 1)
                                    cart11.perez_now = 0;
                            }
                        }
                    }
                    else if (bl1)
                    {
                        //foreach (par_cart_art cart1 in lt_carts_art)
                        //{
                        foreach (par_cart_art cart11 in mb.lt_carts_art)
                        {
                            if (cart11.perez_now > 1)
                                cart11.perez_now--;
                        }
                    }
                }
                */
                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);
                bl_att = true;
                //anim.col_anim++;
                //func.next_hod();
            }
            else if (lt_carts_active_now_base[0] == "cart dungalorg(sleep)" && lt_carts_active_now_base[1] == "cart derlorg" && bl_art_boss_dungalorg != null)
            {
                mb.activate_art(bl_art_boss_dungalorg, x_cl, y_cl);
                //int lv = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().lvl;
                /*
                bool bl_create = false;
                foreach (par_cart_art cart in mb.lt_carts_art)
                {
                    if(cart.type==par_cart.type_cart.art_cuzna && cart.q >= lv)
                    {
                        cart.q-= lv;
                        mb.activate_art(cart,x_cl,y_cl);
                        bl_create = true;
                        break;
                    }
                        
                }
                */
                //func.mob[x, y].GetComponent<par_mob>().add_baf(par_baf.type_baf.bar, -1, 1);
                string cart_create = null;
                string carts_cr = lt_carts_active_now_base[1];

                cart_create = "cart dungalorg(sleep) 1";
                // mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (cart_create != null)
                {
                    spawn_cart(cart_create, gameObject.transform.Find("carts active").gameObject);


                }

                /*
                List<string> lt = new List<string>();
                foreach (par_cart_art cart in mb.lt_carts_art)
                {

                    
                    if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                    {
                        //foreach (par_cart_art cart1 in lt_carts_art)
                        //{
                        foreach (par_cart_art cart11 in mb.lt_carts_art)
                        {
                            if (cart11.perez_now > 1)
                                cart11.perez_now -= 2;
                            if (cart11.perez_now < 1)
                                cart11.perez_now = 0;
                        }
                    }
                    else if (!lt.Contains(cart.name) && cart.perez_now == 0)
                    {
                        if (cart.name == "cart speed")
                            mb.activate_art(cart, mb.x, mb.y);
                        lt.Add(cart.name);
                    }
                }
                */


                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);
                bl_att = true;
                //mb.lt_carts_active_now.RemoveAt(0);
                //anim.col_anim++;
                //func.next_hod();

            }
            else if (lt_carts_active_now_base[0] == "cart cuznec" && lt_carts_active_now.Count == 2)
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


                    if (carts_cr == "cart damage")
                        cart_create = "cart damage 1";
                    if (carts_cr == "cart barier")
                        cart_create = "cart barier 1";
                    if (carts_cr == "cart sp atack")
                        cart_create = "cart sp atack 1";
                    if (carts_cr == "cart sp barier")
                        cart_create = "cart sp barier 1";

                    if (carts_cr == "cart att pow")
                        cart_create = "cart att pow 1";
                    if (carts_cr == "cart bar pow")
                        cart_create = "cart bar pow 1";

                    if (carts_cr == "cart crystall")
                        cart_create = "cart crystall 1";
                    if (carts_cr == "cart sp crystall")
                        cart_create = "cart sp crystall 1";


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
                                    mb.activate_art(cart,-1,-1, lt_carts_active_now_base[0]);
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
                            }

                        }

                        spawn_cart(cart_create, gameObject.transform.Find("carts active").gameObject);

                        /*
                        List<par_cart.type_cart> lt = new List<par_cart.type_cart>();
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {

                            if (cart.perez_now == 0 || cart.q > 0)
                            {
                                if (!lt.Contains(cart.type))
                                {
                                    par_cart.type_cart cart_type_now = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().type;

                                    if (cart.type == par_cart.type_cart.art_speed_rock && cart_type_now == par_cart.type_cart.rock)
                                        mb.activate_art(cart, mb.x, mb.y);
                                    //else if (cart.name == "cart speed rock 1" && mb.lt_carts_active_now[0] == "cart rock 1")
                                    //mb.activate_art(cart, mb.x, mb.y);

                                    if (cart.type == par_cart.type_cart.art_speed_fire && cart_type_now == par_cart.type_cart.fire)
                                        mb.activate_art(cart, mb.x, mb.y);
                                    //else if (cart.name == "cart speed fire 1" && mb.lt_carts_active_now[0] == "cart fire 1")
                                    //mb.activate_art(cart, mb.x, mb.y);

                                    if (cart.type == par_cart.type_cart.art_speed_energy && cart_type_now == par_cart.type_cart.energy)
                                        mb.activate_art(cart, mb.x, mb.y);
                                    //else if (cart.name == "cart speed energy 1" && mb.lt_carts_active_now[0] == "cart energy 1")
                                    //mb.activate_art(cart, mb.x, mb.y);

                                    if (cart.type == par_cart.type_cart.art_speed_blood && cart_type_now == par_cart.type_cart.blood)
                                        mb.activate_art(cart, mb.x, mb.y);
                                    //else if (cart.name == "cart speed blood 1" && mb.lt_carts_active_now[0] == "cart blood 1")
                                    //mb.activate_art(cart, mb.x, mb.y);

                                    if (cart.type == par_cart.type_cart.art_speed_ice && cart_type_now == par_cart.type_cart.ice)
                                        mb.activate_art(cart, mb.x, mb.y);
                                    //else if (cart.name == "cart speed ice 1" && mb.lt_carts_active_now[0] == "cart ice 1")
                                    //mb.activate_art(cart, mb.x, mb.y);

                                    if (cart.type == par_cart.type_cart.art_speed_poition && cart_type_now == par_cart.type_cart.poition)
                                        mb.activate_art(cart, mb.x, mb.y);
                                    //else if (cart.name == "cart speed poition 1" && mb.lt_carts_active_now[0] == "cart poition 1")
                                    //mb.activate_art(cart, mb.x, mb.y);

                                    else if (cart.name == "cart speed")
                                        mb.activate_art(cart, mb.x, mb.y);
                                    lt.Add(cart.type);
                                }


                            }
                        }
                        foreach (par_cart_art cart in mb.lt_carts_art)
                        {
                            bool bl1 = (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock") ||
                                (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock 1") ||

                                (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood") ||
                                (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood 1") ||

                                (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire") ||
                                (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire 1") ||

                                (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice") ||
                                (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice 1") ||

                                (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy") ||
                                (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy 1") ||

                                (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition") ||
                                (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition 1");

                            if (cart.perez_now == 0)
                            {

                            }
                            if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                            {
                                par_cart.type_cart cart_type_now = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().type;
                                foreach (par_cart_art cart1 in mb.lt_carts_art)
                                {
                                    if (cart_type_now == par_cart.type_cart.sp_barier || cart_type_now == par_cart.type_cart.sp_crystall)
                                    {
                                        if (cart1.perez_now > 1)
                                            cart1.perez_now -= 3;
                                        if (cart1.perez_now < 0)
                                            cart1.perez_now = 0;
                                    }
                                    else
                                    {
                                        if (cart1.perez_now > 1)
                                            cart1.perez_now -= 2;
                                        if (cart1.perez_now < 0)
                                            cart1.perez_now = 0;
                                    }
                                }
                            }
                            else if (bl1)
                            {
                                //foreach (par_cart_art cart1 in lt_carts_art)
                                //{
                                foreach (par_cart_art cart11 in mb.lt_carts_art)
                                {
                                    if (cart11.perez_now > 1)
                                        cart11.perez_now--;
                                }
                            }
                        }
                        */

                    }
                    bl_att = true;
                }
                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);
                
                //mb.lt_carts_active_now.RemoveAt(0);
                //anim.col_anim++;
                //func.next_hod();

            }

            else if (lt_carts_active_now_base[0] == "cart cuznec" && lt_carts_active_now.Count == 3 && lt_carts_active_now[1] == lt_carts_active_now[2])
            {

                //func.mob[x, y].GetComponent<par_mob>().add_baf(par_baf.type_baf.bar, -1, 1);

                string cart_create = null;
                string carts_cr = lt_carts_active_now_base[1];
                par_cart.type_cart cart0 = GameObject.Find(lt_carts_active_now[1]).GetComponent<par_cart>().type;

                bool bl_el_cuzna = false;
                

                if (carts_cr == "cart damage")
                    cart_create = "cart damage 1";
                if (carts_cr == "cart barier")
                    cart_create = "cart barier 1";
                if (carts_cr == "cart sp atack")
                    cart_create = "cart sp atack 1";
                if (carts_cr == "cart sp barier")
                    cart_create = "cart sp barier 1";

                if (carts_cr == "cart att pow")
                    cart_create = "cart att pow 1";
                if (carts_cr == "cart bar pow")
                    cart_create = "cart bar pow 1";

                if (carts_cr == "cart crystall")
                    cart_create = "cart crystall 1";
                if (carts_cr == "cart sp crystall")
                    cart_create = "cart sp crystall 1";


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
                        }

                    }

                    if (data.data_now.col_carts_cuzna < 50)
                    {
                        data.data_now.col_carts_cuzna++;
                        if (data.data_now.col_carts_cuzna == 50)
                        {
                            //data.data_now.lt_players.Add(data.players.blood);
                            data.give_achive("Кузнец");

                        }
                    }


                    spawn_cart(cart_create, gameObject.transform.Find("carts active").gameObject);

                    /*
                    List<par_cart.type_cart> lt = new List<par_cart.type_cart>();
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {

                        if (cart.perez_now == 0 || cart.q > 0)
                        {
                            if (!lt.Contains(cart.type))
                            {
                                par_cart.type_cart cart_type_now = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().type;

                                if (cart.type == par_cart.type_cart.art_speed_rock && cart_type_now == par_cart.type_cart.rock)
                                    mb.activate_art(cart, mb.x, mb.y);
                                //else if (cart.name == "cart speed rock 1" && mb.lt_carts_active_now[0] == "cart rock 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_fire && cart_type_now == par_cart.type_cart.fire)
                                    mb.activate_art(cart, mb.x, mb.y);
                                //else if (cart.name == "cart speed fire 1" && mb.lt_carts_active_now[0] == "cart fire 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_energy && cart_type_now == par_cart.type_cart.energy)
                                    mb.activate_art(cart, mb.x, mb.y);
                                //else if (cart.name == "cart speed energy 1" && mb.lt_carts_active_now[0] == "cart energy 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_blood && cart_type_now == par_cart.type_cart.blood)
                                    mb.activate_art(cart, mb.x, mb.y);
                                //else if (cart.name == "cart speed blood 1" && mb.lt_carts_active_now[0] == "cart blood 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_ice && cart_type_now == par_cart.type_cart.ice)
                                    mb.activate_art(cart, mb.x, mb.y);
                                //else if (cart.name == "cart speed ice 1" && mb.lt_carts_active_now[0] == "cart ice 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                if (cart.type == par_cart.type_cart.art_speed_poition && cart_type_now == par_cart.type_cart.poition)
                                    mb.activate_art(cart, mb.x, mb.y);
                                //else if (cart.name == "cart speed poition 1" && mb.lt_carts_active_now[0] == "cart poition 1")
                                //mb.activate_art(cart, mb.x, mb.y);

                                else if (cart.name == "cart speed")
                                    mb.activate_art(cart, mb.x, mb.y);
                                lt.Add(cart.type);
                            }


                        }
                    }
                    foreach (par_cart_art cart in mb.lt_carts_art)
                    {
                        bool bl1 = (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock") ||
                            (cart.name == "cart rock st" && mb.lt_carts_active_now[1] == "cart rock 1") ||

                            (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood") ||
                            (cart.name == "cart blood st" && mb.lt_carts_active_now[1] == "cart blood 1") ||

                            (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire") ||
                            (cart.name == "cart fire st" && mb.lt_carts_active_now[1] == "cart fire 1") ||

                            (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice") ||
                            (cart.name == "cart ice st" && mb.lt_carts_active_now[1] == "cart ice 1") ||

                            (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy") ||
                            (cart.name == "cart energy st" && mb.lt_carts_active_now[1] == "cart energy 1") ||

                            (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition") ||
                            (cart.name == "cart poition st" && mb.lt_carts_active_now[1] == "cart poition 1");

                        if (cart.perez_now == 0)
                        {

                        }
                        if (cart.name == "cart mast" || cart.name == "cart derlorg st")
                        {
                            par_cart.type_cart cart_type_now = GameObject.Find(mb.lt_carts_active_now[1]).GetComponent<par_cart>().type;
                            foreach (par_cart_art cart1 in mb.lt_carts_art)
                            {
                                if (cart_type_now == par_cart.type_cart.sp_barier || cart_type_now == par_cart.type_cart.sp_crystall)
                                {
                                    if (cart1.perez_now > 1)
                                        cart1.perez_now -= 6;
                                    if (cart1.perez_now < 0)
                                        cart1.perez_now = 0;
                                }
                                else
                                {
                                    if (cart1.perez_now > 1)
                                        cart1.perez_now -= 4;
                                    if (cart1.perez_now < 0)
                                        cart1.perez_now = 0;
                                }
                            }
                        }
                        else if (bl1)
                        {
                            //foreach (par_cart_art cart1 in lt_carts_art)
                            //{
                            foreach (par_cart_art cart11 in mb.lt_carts_art)
                            {
                                if (cart11.perez_now > 1)
                                    cart11.perez_now -= 2;
                                if (cart11.perez_now < 0)
                                    cart11.perez_now = 0;
                            }
                        }
                    }
                    */
                    bl_att = true;
                }
                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);
                
                //anim.col_anim++;
                //func.next_hod();

            }

            else if (lt_carts_active_now_base[0] == "cart spirit(boss)")
            {
                Debug.Log("tuman");
                par_cart cart1 = GameObject.Find(lt_carts_active_now_base[1]).GetComponent<par_cart>();
                string ef = null;
                if (cart1.type == par_cart.type_cart.blood)
                {
                    ef = "cart spirit blood";
                }
                else if (cart1.type == par_cart.type_cart.ice)
                {
                    ef = "cart spirit ice";
                }
                else if (cart1.type == par_cart.type_cart.fire)
                {
                    ef = "cart spirit fire";
                }
                else if (cart1.type == par_cart.type_cart.poition)
                {
                    ef = "cart spirit poition";
                }
                else if (cart1.type == par_cart.type_cart.rock)
                {
                    ef = "cart spirit rock";
                }
                else if (cart1.type == par_cart.type_cart.energy)
                {
                    ef = "cart spirit energy";
                }


                mb.effect_now = func.create_effect(ef, x_cl, y_cl, mb.gameObject, false, pow_rez*2, lt_carts_active_now_base);


                List<par_cart.type_cart> lt = new List<par_cart.type_cart>();
                
                
                //mb.effect_now = func.create_effect(ef, x_cl, y_cl, mb.gameObject, false, pow_rez, mb.lt_carts_active_now);
                //mb.lt_carts_active_now.RemoveAt(0);
                //mb.lt_carts_active_now.RemoveAt(0);
                bl_att = true;
                //func.next_hod();
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
                    
                }
                foreach (par_cart_art art in lt_carts_art)
                {
                    if (art.perez_now == 0)
                    {
                        if (art.type == par_cart.type_cart.art_blood_crystall && (lt_carts_active_now_base.Contains("cart blood") || lt_carts_active_now_base.Contains("cart blood 1") || lt_carts_active_now_base.Contains("cart blood 2") || lt_carts_active_now_base.Contains("cart spirit blood")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art,x_cl,y_cl);
                        }
                            
                        else if (art.type == par_cart.type_cart.art_rock_crystall && (lt_carts_active_now_base.Contains("cart rock") || lt_carts_active_now_base.Contains("cart rock 1") || lt_carts_active_now_base.Contains("cart rock 2") || lt_carts_active_now_base.Contains("cart spirit rock")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_energy_crystall && (lt_carts_active_now_base.Contains("cart energy") || lt_carts_active_now_base.Contains("cart energy 1") || lt_carts_active_now_base.Contains("cart energy 2") || lt_carts_active_now_base.Contains("cart spirit energy")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_ice_crystall && (lt_carts_active_now_base.Contains("cart ice") || lt_carts_active_now_base.Contains("cart ice 1") || lt_carts_active_now_base.Contains("cart ice 2") || lt_carts_active_now_base.Contains("cart spirit ice")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_poition_crystall && (lt_carts_active_now_base.Contains("cart poition") || lt_carts_active_now_base.Contains("cart poition 1") || lt_carts_active_now_base.Contains("cart poition 2") || lt_carts_active_now_base.Contains("cart spirit poition")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                        else if (art.type == par_cart.type_cart.art_fire_crystall && (lt_carts_active_now_base.Contains("cart fire") || lt_carts_active_now_base.Contains("cart fire 1") || lt_carts_active_now_base.Contains("cart fire 2") || lt_carts_active_now_base.Contains("cart spirit fire")))
                        {
                            pow_crystall += art.lvl;
                            activate_art(art, x_cl, y_cl);
                        }
                    }
                }
                if (pow_crystall > 0)
                {
                    par_effect eff = effect_now.GetComponent<par_effect>();
                    if(x_cl>0)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl - 1, y_cl, parent, false, pow_crystall, lt_carts_active_now_base));
                    if (y_cl > 0)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl, y_cl-1, parent, false, pow_crystall, lt_carts_active_now_base));
                    if (x_cl < func.size_global-1)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl + 1, y_cl, parent, false, pow_crystall, lt_carts_active_now_base));
                    if (y_cl < func.size_global - 1)
                        lt_effect_now.Add(func.create_effect(eff.name_in_lt, x_cl, y_cl+1, parent, false, pow_crystall, lt_carts_active_now_base));
                }

                mb.gameObject.GetComponent<Animator>().SetBool("selected", false);
                mb.gameObject.GetComponent<Animator>().SetBool("select_now", false);

                mb.gameObject.GetComponent<Animator>().SetBool("atack", true);

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
                if (bl_att_hod)
                    func.next_hod();
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
            if (mb.friend != friend)
            {
                mb_lt_rd.Add(obj);
            }





        }
        if (mb_lt_rd.Count > 0)
            player = mb_lt_rd[Random.Range(0, mb_lt_rd.Count-1)].GetComponent<par_mob>();
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

                if (way_[x, y] != -1 && distanse_min != -1)
                {
                    //int[,] way_ = func.way_bool(bl_way, player.x, player.y, distanse_max);
                    //Debug.Log("ways  " + way_[x, y]);
                    if (way_[x, y] > distanse_min)
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
                else if (distanse_min != -1)
                {
                    way_ = func.way_bool(bl_way_1, player.x, player.y, distanse_max);
                    if (way_[x, y] > distanse_min)
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

                List<object> lt = func.way_find_lt(func.mob, x, y, distanse_max);
                List<GameObject> mob_rd = new List<GameObject>();
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    if (!mb.friend && mb.num != num)
                        mob_rd.Add((GameObject)obj);
                }

                GameObject mb_end = null;
                par_mob mb_end_par = null;
                if (mob_rd.Count != 0)
                {
                    mb_end = mob_rd[Random.Range(0, mob_rd.Count)];
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

                    if (way_[x, y] != -1 && distanse_min != -1)
                    {
                        //int[,] way_ = func.way_bool(bl_way, player.x, player.y, distanse_max);
                        //Debug.Log("ways  " + way_[x, y]);
                        if (way_[x, y] > distanse_min)
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
        

        func.next_hod();


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
        if (i == 1)
        {
            ui.del_pricels(v2);
            if (wolk((int)v2.x, (int)v2.y))
            {

                if (lt_wolk.Count == 0)
                    ui.clean_pricels("pricel");
            }
            else
            {
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
            foreach (Vector2 v2_ in lt_wolk)
            {
                ui.del_pricels(v2_);
            }
            lt_wolk.Clear();
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
        /*
        Vector2 v2 = lt_wolk.Pop();
        int i = Mathf.Abs(x - (int)v2.x) + Mathf.Abs(y - (int)v2.y);
        //Debug.Log("wolk auto " + v2);
        if (i == 1)
        {
            ui.del_pricels(v2);
            if (wolk((int)v2.x, (int)v2.y))
            {
                
                if (lt_wolk.Count == 0)
                    ui.clean_pricels("pricel");
            }
            else
            {
                foreach (Vector2 v2_ in lt_wolk)
                {
                    Debug.Log("del pricels " + v2_+" "+num);
                    ui.del_pricels(v2_);
                }
                lt_wolk.Clear();
            }

        }
        else
        {
            foreach (Vector2 v2_ in lt_wolk)
            {
                ui.del_pricels(v2_);
            }
            lt_wolk.Clear();
        }
        */
    }
    public void viz_block(int x_v,int y_v)
    {
        lt_v2_vizited.Add(new Vector2(x_v, y_v));
        if(friend)
        func.lt_v2_viz_friend[x_v, y_v].Add(gameObject);
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
        if (friend && !(x_v==x && y_v== y))
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
        if(friend)
        func.lt_v2_viz_friend[x_v, y_v].Remove(gameObject);
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

        if (friend && !(x_v == x && y_v == y) && func.lt_v2_viz_friend[x_v, y_v].Count==0 )
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
        //Debug.Log("wolk mob "+num+" in "+x_wolk+" "+y_wolk);
        if (func.object_bool(x_wolk, y_wolk) || bl_wolk)
        {
            //Debug.Log("wolk mob next");

            parametrs par = gameObject.GetComponent<parametrs>();
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
            for (int i = y_wolk - distanse_max; i <= y_wolk + distanse_max ; i++)
            {
                if (i >= 0 && i < func.size_global)
                {
                    for (int j = x_wolk - distanse_max ; j <= x_wolk + distanse_max; j++)
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

                    if (cc <= distanse_max )
                    {

                        if(bl_n && way_fr_bl[n_x, n_y] != -1 && !lt_v2_vizited.Contains(new Vector2(n_x, n_y)))
                        {
                            viz_block(n_x, n_y);
                        }
                        if (bl_ji  && way_fr_bl[j, i] ==-1 && lt_v2_vizited.Contains(new Vector2(j, i)))
                        {
                            unviz_block(j, i);
                        }
                        //if (j == x_b - i_ * dj)
                        //i_br = true;
                        //break;
                        //continue;
                    }
                    
                    /*
                    if (bl_ji&& way_fr_bl[j, i] <= distanse_max && way_fr_bl[j, i]!=-1)
                    {
                        //if (j == x_b - i_ * dj)
                        //i_br = true;
                        //break;
                        continue;
                    }
                    */
                    else 
                            {
                                

                                //if (n_x>=0 && n_x< func.size_global && n_y >= 0 && n_y < func.size_global)
                                //{
                                    if (bl_n&& way_fr_bl[n_x, n_y] != -1)
                                    {
                            viz_block(n_x, n_y);
                            /*
                                        lt_v2_vizited.Add(new Vector2(n_x, n_y));
                                        if (func.mob[n_x, n_y] != null)
                                        {
                                            par_mob mb_viz = func.mob[n_x, n_y].GetComponent<par_mob>();
                                            lt_mob_vizited.Add(func.mob[n_x, n_y]);

                                            cc = Mathf.Abs((n_x - x_wolk)) + Mathf.Abs((n_y - y_wolk));
                                            if (cc <= mb_viz.distanse_max)
                                            {
                                                mb_viz.lt_mob_vizited.Add(gameObject);
                                            }
                                        }
                            */
                                    }


                                    if (friend)
                        {/*
                                        if (bl_n &&way_fr_bl[n_x, n_y] != -1)
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


                                        
                                        if (bl_ji)
                                        {
                                            if (func.nature[j, i] != null && func.nature[j, i].active)
                                                func.nature[j, i].active = false;
                                            if (func.mob[j, i] != null && func.mob[j, i].GetComponent<par_mob>().vizited)
                                                //func.mob[j, i].active = true;
                                                func.mob[j, i].GetComponent<par_mob>().viz(false);
                                            if (func.carts[j, i] != null && func.carts[j, i].active)
                                                func.carts[j, i].active = false;
                                            if (func.eath[j, i].active)
                                                func.eath[j, i].active = false;
                                        }
                                        */
                        }



                                    if (bl_ji )
                                    {
                            unviz_block(j, i);
                            /*
                                        lt_v2_vizited.Remove(new Vector2(j, i));
                                        if (func.mob[j, i] != null && friend != func.mob[j, i].GetComponent<par_mob>().friend)
                                        {
                                            par_mob mb_viz = func.mob[j, i].GetComponent<par_mob>();
                                            lt_mob_vizited.Remove(func.mob[j, i]);
                                            cc = Mathf.Abs((j - x_wolk)) + Mathf.Abs((i - y_wolk));
                                            if (cc > mb_viz.distanse_max)
                                            {
                                                mb_viz.lt_mob_vizited.Remove(gameObject);
                                            }
                                        }
                            */
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

            /*
            for (int i = y_b- distanse_max*di; i != y_b +distanse_max*di +di; i+=di)
            {
                if (i >=0 &&  i < func.size_global)
                {
                    for (int j = x_b; j != x_wolk+ dj; j += dj)
                    {
                        if (j >= 0 && j < func.size_global)
                        {
                            if (way_fr[j, i] == -1)
                            {
                                if (j == x_b)
                                    i_br = true;
                                break;
                            }
                                
                            else
                            {

                            }
                        }
                    }
                    if (i_br)
                        break;
                }

            }

            for()

                for (int i = y_b - distanse_max; i <= y_b + distanse_max; i++)
                {
                    if (i >= 0 && i < func.size_global)
                    {
                        for (int j = x_b - distanse_max; j <= x_b + distanse_max; j++)
                        {
                            if (j >= 0 && j < func.size_global)
                            {
                                if (way_fr[j, i] != -1)
                                {

                                    //gm_lt[j, i] = new List<GameObject>();
                                    List<GameObject> lt_gm1 = new List<GameObject>();
                                    if (func.nature[j, i] != null && func.nature[j, i].active)
                                        lt_gm1.Add(func.nature[j, i]);
                                    if (func.mob[j, i] != null && !func.mob[j, i].GetComponent<par_mob>().friend && func.mob[j, i].GetComponent<par_mob>().vizited)
                                        lt_gm1.Add(func.mob[j, i]);
                                    if (func.carts[j, i] != null && func.carts[j, i].active)
                                        lt_gm1.Add(func.carts[j, i]);
                                    if (func.eath[j, i].active)
                                        lt_gm1.Add(func.eath[j, i]);
                                    if (lt_gm1.Count > 0)
                                        lt_gm.Add(new Vector2(j, i), lt_gm1);

                                }
                            }
                        }
                    }
                }
            //}
            //Debug.Log("wolk mob nex 1 count "+lt_gm.Count);

            func.mob[par.x, par.y] = null;

            if (x_wolk > par.x)
                simm(1);
            else if (x_wolk < par.x)
                simm(-1);

            par.x = x_wolk;
            par.y = y_wolk;

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
            //&& !(x_wolk == x_b && y_wolk == y_b)
            //if (friend)
            //{

            //bl = func.recurs(bl, 0, 0);


            int[,] way_fr_bl = func.way_bool_and(bl, x_wolk, y_wolk, distanse_max);
            for (int i = y_wolk - distanse_max; i <= y_wolk + distanse_max; i++)
            {
                if (i >= 0 && i < func.size_global)
                {
                    for (int j = x_wolk - distanse_max; j <= x_wolk + distanse_max; j++)
                    {
                        if (j >= 0 && j < func.size_global)
                        {
                            if (way_fr_bl[j, i] != -1)
                            {
                                if (lt_gm.ContainsKey(new Vector2(j, i)))
                                {
                                    lt_gm.Remove(new Vector2(j, i));
                                    if (!lt_mob_vizited.Contains(func.mob[j, i]))
                                    {
                                        lt_mob_vizited.Add(func.mob[j, i]);
                                        par_mob mb_viz = func.mob[j, i].GetComponent<par_mob>();
                                        int c_viz = Mathf.Abs(x - mb_viz.x) + Mathf.Abs(y - mb_viz.y);
                                        if (c_viz<= mb_viz.distanse_max && !mb_viz.lt_mob_vizited.Contains(gameObject))
                                        {
                                            mb_viz.lt_mob_vizited.Add(gameObject);
                                        }
                                    }
                                        
                                }

                                else
                                {
                                    if (func.nature[j, i] != null && !func.nature[j, i].active)
                                        func.nature[j, i].active = true;
                                    if (func.mob[j, i] != null && !func.mob[j, i].GetComponent<par_mob>().friend && !func.mob[j, i].GetComponent<par_mob>().vizited)
                                        //func.mob[j, i].active = true;
                                        func.mob[j, i].GetComponent<par_mob>().viz(true);
                                    if (func.carts[j, i] != null && !func.carts[j, i].active)
                                        func.carts[j, i].active = true;
                                    if (!func.eath[j, i].active)
                                        func.eath[j, i].active = true;
                                }
                                /*
                                if (func.nature[j, i] != null && !func.nature[j, i].active)
                                    func.nature[j, i].active = true;
                                if (func.mob[j, i] != null && !func.mob[j, i].GetComponent<par_mob>().friend && !func.mob[j, i].GetComponent<par_mob>().vizited)
                                    //func.mob[j, i].active = true;
                                    func.mob[j, i].GetComponent<par_mob>().viz(true);
                                if (func.carts[j, i] != null && !func.carts[j, i].active)
                                    func.carts[j, i].active = true;
                                if(!func.eath[j, i].active)
                                    func.eath[j, i].active = true;
                                */
            /*

                            }
                        }
                    }
                }
            }
            */
            //Debug.Log("wolk mob nex 2 count " + lt_gm.Count);

            /*
            foreach (List<GameObject> lt in lt_gm.Values)
            {
                foreach (GameObject gm in lt)
                {
                    /*
                    if (func.nature[j, i] != null && func.nature[j, i].active)
                        func.nature[j, i].active = false;
                    if (func.mob[j, i] != null && !func.mob[j, i].GetComponent<par_mob>().friend && func.mob[j, i].GetComponent<par_mob>().vizited)
                        func.mob[j, i].GetComponent<par_mob>().viz(false);
                    if (func.carts[j, i] != null && func.carts[j, i].active)
                        func.carts[j, i].active = false;
                    if (func.eath[j, i].active)
                        func.eath[j, i].active = false;
                    */
            /*
                    if (gm.GetComponent<par_mob>() != null)
                    {
                        par_mob mb_viz = gm.GetComponent<par_mob>();
                        if (lt_mob_vizited.Contains(gm))
                        {
                            lt_mob_vizited.Remove(gm);
                            
                            int c_viz = Mathf.Abs(x - mb_viz.x) + Mathf.Abs(y - mb_viz.y);
                            if (c_viz > mb_viz.distanse_max && mb_viz.lt_mob_vizited.Contains(gameObject))
                            {
                                mb_viz.lt_mob_vizited.Add(gameObject);
                            }
                        }
                            
                        gm.GetComponent<par_mob>().viz(false);
                    }

                    else
                        gm.active = false;
                }
        
            }
    */
            if (num == 0)
                GameObject.Find("bt up").GetComponent<Animator>().SetBool("opened", func.carts[x_wolk, y_wolk] != null);
            //}




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
            if (!(gameObject.transform.position.x < 0 || gameObject.transform.position.y < 0) && vizited)
            {
                time_wolk = 0;
                d_v2_wolk = new Vector2(x_wolk - x_b, y_wolk - y_b) / 0.15f;
                d_v2_wolk_end = new Vector2(x_wolk, y_wolk);
                anim.col_anim++;
            }
            else
            {
                gameObject.transform.position = new Vector3(x_wolk, y_wolk, 0);
            }
            //gameObject.GetComponent<SpriteRenderer>().sortingOrder = (6 - y_wolk + 1) * 2-1;
            //func.next_hod();
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
            if(friend)
                func.lt_v2_viz_friend[j, i].Remove(gameObject);
            if (way_fr_bl[j, i] != -1 && func.lt_v2_viz_friend[j, i].Count==0)
            {
                if (func.nature[j, i] != null && func.nature[j, i].active)
                    func.nature[j, i].active = false;
                if (func.mob[j, i] != null && func.mob[j, i] != gameObject &&func.mob[j, i].GetComponent<par_mob>().vizited)
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
                if (mb_viz.lt_mob_vizited.Contains(gameObject))
                {
                    mb_viz.lt_mob_vizited.Remove(gameObject);
                }
                    
            }

        }
        lt_v2_vizited.Clear();
        lt_mob_vizited.Clear();

        for (int i = y - distanse_max ; i <= y + distanse_max  ; i ++)
        {
            if (i >= 0 && i < func.size_global)
            {



                for (int j = x - distanse_max; j <=x+ distanse_max; j ++)
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
                            if (func.mob[n_x, n_y] != null && !(j == x && i == y))
                            {
                                par_mob mb_viz = func.mob[n_x, n_y].GetComponent<par_mob>();
                                lt_mob_vizited.Add(func.mob[n_x, n_y]);

                                cc = Mathf.Abs((n_x - x)) + Mathf.Abs((n_y - y));
                                if (cc <= mb_viz.distanse_max && func.mob[n_x, n_y]!=gameObject)
                                {
                                    mb_viz.lt_mob_vizited.Add(gameObject);
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
                if (cart.q>0 &&  cart.name == "cart berserk boss")
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
                if (cart.name == "cart respawn")
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
        if (gameObject.transform.Find("Canvas hp").Find("hp bar") != null)
        {
            gameObject.transform.Find("Canvas hp").Find("hp bar").gameObject.GetComponent<Scrollbar>().size = ((float)hp / hp_max);
        }
        func.test_message = "error wolk damage " + num.ToString() + " 5150";
        foreach (par_cart_art cart in lt_carts_art)
        {
            
            if (cart.perez_now != 0)
            {
                
                    
                if (cart.name == "cart berserk")
                {
                    cart.perez_now -= dm;
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
                    if(cart.q==0)
                        cart.perez_now -= dm;
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
                    if(!friend)
                    foreach (par_cart_art cart in lt_carts_art)
                    {
                        if (Random.Range(0, 25 / bl_boss) == 0)
                        {
                            GameObject gm_cart = func.drop(cart.name, x, y);
                            if (gm_cart != null)
                                gm_cart.active = func.eath[x, y].active;
                        }

                    }
                    if(!friend)
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
                    }
                    foreach (GameObject gm in lt_mob_vizited)
                    {
                        gm.GetComponent<par_mob>().lt_mob_vizited_in.Remove(gameObject);
                    }
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
        func.test_message = "good";
        //par_mob player = gameObject.Find("player");
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
    void Start()
    {
        if (!func.baza.ContainsKey(gameObject.name))
        {
            cel = GameObject.Find(data.data_now.pl_name_now);
        }


        func = GameObject.Find("func").GetComponent<func>();


        //Debug.Log("key " + carts_count_max.Count);
    }
    void Awake()
    {
        hp = hp_max;
        if (!func.baza.ContainsKey(gameObject.name))
        {
            foreach (Transform tr in GameObject.Find("list carts").transform)
            {
                Debug.Log(tr.gameObject.name + " "+ (GameObject.Find(tr.gameObject.name)==null));
                string nm = tr.gameObject.name;
                if (!GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().art_cart_bl() && tr.gameObject.name != "cart gold")
                {
                    par_cart.type_cart cart_name = GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().type;
                    if (cart_name == par_cart.type_cart.damage && gameObject.name == "atack player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if (cart_name == par_cart.type_cart.fire && gameObject.name == "fire player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if (cart_name == par_cart.type_cart.rock && gameObject.name == "rock player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if (cart_name == par_cart.type_cart.ice && gameObject.name == "ice player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if (cart_name == par_cart.type_cart.blood && gameObject.name == "blood player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if (cart_name == par_cart.type_cart.energy && gameObject.name == "energy player")
                        carts_count_max.Add(tr.gameObject.name, 8);
                    else if (cart_name == par_cart.type_cart.poition && gameObject.name == "poition player")
                        carts_count_max.Add(tr.gameObject.name, 8);

                    else if (cart_name == par_cart.type_cart.sp_barier && gameObject.name == "barier player")
                        carts_count_max.Add(tr.gameObject.name, 4);

                    else if (cart_name == par_cart.type_cart.damage && gameObject.name == "goblin boss")
                        carts_count_max.Add(tr.gameObject.name, 10);

                    else
                    {
                        int dop_inven = 0;
                        if (gameObject.name == "derlorg player")
                            dop_inven = 1;
                        //if (nm == "cart dungalorg(sleep)" || nm == "cart dungalorg")
                        //carts_count_max.Add(tr.gameObject.name, 12+2*dop_inven);
                        if (GameObject.Find(tr.gameObject.name).GetComponent<par_cart>().spetial)
                            carts_count_max.Add(tr.gameObject.name, 3 + dop_inven);
                        else
                            carts_count_max.Add(tr.gameObject.name, 6 + dop_inven * 2);
                    }

                    //Debug.Log(carts_count_max[tr.gameObject.name] + " trtr  " + tr.gameObject.name + "       " + gameObject.name);
                }


            }

            func.baza.Add(gameObject.name, gameObject);
            if (gameObject.name == "base player")
            {
                add_cart_art("cart base st");
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
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.barier);
            }



            else if (gameObject.name == "goblin")
            {
                add_cart_art("cart sp atack");
                type_lt_attack.Add(type_atack_mob.damage);
            }
                
            //add_cart_art("cart sp rock");
            //add_cart_art("cart sp blood");
            else if (gameObject.name == "goblin hiller")
            {
                add_cart_art("cart sp barier");
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart sp rock");
            }

            if (gameObject.name == "org berserk")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp atack");
                add_cart_art("cart berserk boss");
                add_cart_art("cart rage");

                type_lt_attack.Add(type_atack_mob.damage);
                //add_cart_art("cart sp barier 1");
                //add_cart_art("cart sp rock 1");
                //add_cart_art("cart berserk");
            }
            else if (gameObject.name == "goblin shaman")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart sp fire");
                add_cart_art("cart sp rock");
                add_cart_art("cart sp blood");
                add_cart_art("cart sp energy");
                add_cart_art("cart sp ice");
                add_cart_art("cart sp poition");

                add_cart_art("cart shaman boss");
                add_cart_art("cart spirit");

                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.elemental);
                //add_cart_art("cart sp barier 1");
                //add_cart_art("cart sp rock 1");
                //add_cart_art("cart berserk");
            }
            else if (gameObject.name == "goblin blood elit")
            {
                add_cart_art("cart sp blood");
                type_lt_attack.Add(type_atack_mob.blood);
                //add_cart_art("cart berserk");
            }
            else if (gameObject.name == "goblin rock elit")
            {
                add_cart_art("cart sp rock");
                type_lt_attack.Add(type_atack_mob.rock);
                //add_cart_art("cart act rock");
            }
            else if (gameObject.name == "goblin fire elit")
            {
                add_cart_art("cart sp fire");
                type_lt_attack.Add(type_atack_mob.fire);
                //add_cart_art("cart act fire");
            }
            else if (gameObject.name == "goblin energy elit")
            {
                add_cart_art("cart sp energy");
                type_lt_attack.Add(type_atack_mob.energy);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "goblin ice elit")
            {
                add_cart_art("cart sp ice");
                type_lt_attack.Add(type_atack_mob.ice);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "goblin poition elit")
            {
                add_cart_art("cart sp poition");
                type_lt_attack.Add(type_atack_mob.poition);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.damage);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg shaman")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg fire")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp fire");
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.fire);
                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg blood")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp blood");
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.blood);
                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg poition")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp poition");
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.poition);
                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg ice")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp ice");
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.ice);
                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg energy")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp energy");
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.energy);
                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "derlorg rock")
            {
                add_cart_art("cart sp derlorg");
                add_cart_art("cart sp atack");
                add_cart_art("cart sp rock");
                add_cart_art("cart sp barier");

                type_lt_attack.Add(type_atack_mob.seal);
                type_lt_attack.Add(type_atack_mob.rock);
                type_lt_attack.Add(type_atack_mob.damage);
                type_lt_attack.Add(type_atack_mob.barier);
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "dungalorg")
            {
                add_cart_art("cart sp atack");
                add_cart_art("cart dungalorg(boss)");
                add_cart_art("cart dungalorg heard");
                //add_cart_art("cart act energy");
            }

            else if (gameObject.name == "spider (spawned)")
            {
                add_cart_art("cart sp spider(spawned)");
                //add_cart_art("cart act energy");
            }
            else if (gameObject.name == "spider (spawned)")
            {
                add_cart_art("cart sp spider");
                //add_cart_art("cart act energy");
            }

        }
        if (gameObject.name == data.data_now.pl_name_now)
            gameObject.GetComponent<Animator>().SetInteger("hp", hp);
    }
    public float timer_now = 0;
    public Queue<GameObject> que_dm_tx = new Queue<GameObject>();
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

        if (time_wolk >= 0)
        {
            Debug.Log("timer hod " + num);
            time_wolk += Time.deltaTime;
            Vector2 v2 = gameObject.transform.position;
            v2 += d_v2_wolk * Time.deltaTime;
            gameObject.transform.position = new Vector3(v2.x, v2.y, 0);
            if (time_wolk >= 0.15f)
            {
                gameObject.transform.position = new Vector3(d_v2_wolk_end.x, d_v2_wolk_end.y, 0);
                anim.col_anim--;
                time_wolk = -1;
            }

        }


    }
    // Update is called once per frame
}


