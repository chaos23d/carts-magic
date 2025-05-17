using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public  class ui : MonoBehaviour
{
    public static Dictionary<string, Sprite> cart_sprite = new Dictionary<string, Sprite>();

    public static List<GameObject> lt_pricel = new List<GameObject>();
    public static Dictionary<Vector2, List<GameObject>> lt_pricel_wolk = new Dictionary<Vector2, List<GameObject>>();
    public static Dictionary<Vector2, int> lt_pricel_col = new Dictionary<Vector2, int>();

    public void create_pricel(Vector2 v2)
    {
        GameObject gm = Instantiate(GameObject.Find("pricel"));
        gm.name = "pricel";
        //Debug.Log(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(v2));
        gm.transform.position = v2;
        //Vector3 sz_min = GameObject.Find("player").GetComponent<SpriteRenderer>().bounds.min;
        //Vector3 sz_max = GameObject.Find("player").GetComponent<SpriteRenderer>().bounds.max;

        //Vector3 s_min = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(sz_min);
        //Vector3 s_max= GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(sz_max);
        //Debug.Log(sz_max +"  "+ sz_min);
        //gm.GetComponent<RectTransform>().sizeDelta = (s_max - s_min)*32/100;
        //gm.GetComponent<RectTransform>().rect.height = sz;
        lt_pricel.Add(gm);
    }
    public static void create_pricel_wolk(Vector2 v2)
    {
        
        //Debug.Log("create pricel " + v2);
        GameObject gm = Instantiate(GameObject.Find("pricel wolk"));
        gm.name = "pricel wolk";
        //Debug.Log(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(v2));
        gm.transform.position = v2;
        lt_pricel.Add(gm);
        if (!lt_pricel_wolk.ContainsKey(v2))
        {
            lt_pricel_wolk.Add(v2, new List<GameObject>());
            lt_pricel_col.Add(v2, 1);
        }
        else
            lt_pricel_col[v2]++;
        lt_pricel_wolk[v2].Add(gm);
        //}

    }
    public static void del_pricels(Vector2 v2)
    {
        GameObject gm= lt_pricel_wolk[v2][0];
        lt_pricel_wolk[v2].Remove(gm);
        //Debug.Log("del pricels ui " + lt_pricel_col[v2]+"  "+gm.transform.position);
        lt_pricel.Remove(gm);
        
        Destroy(gm);
        
        if (lt_pricel_col[v2] == 1)
        {
            
            
            lt_pricel_wolk.Remove(v2);
            lt_pricel_col.Remove(v2);
        }
        else
        {
            //lt_pricel_wolk[v2]
            lt_pricel_col[v2]--;
        }
    }
    public static void clean_pricels()
    {
        clean_pricels(null);
    }
    public static void clean_pricels(string name)
    {
        foreach (GameObject gm in lt_pricel)
        {
            if(name==gm.name || name==null)
                Destroy(gm);
        }
        lt_pricel.Clear();
    }
    void Start()
    {

    }
    public static void print_carts_tip(int num)
    {
        Debug.Log("update pn  " + num);

        par_mob pr_mob = func.mob_list[num].GetComponent<par_mob>();
        Dictionary<string, int> lt_cart = new Dictionary<string, int>();
        foreach (string st in pr_mob.lt_carts)
        {
            if (!lt_cart.ContainsKey(st))
                lt_cart.Add(st, 1);
            else
                lt_cart[st]++;
        }
        foreach (Transform tr in GameObject.Find("Content tip").transform)
        {
            Destroy(tr.gameObject);
        }
        foreach (string st in cart_sprite.Keys)
        {
            //Debug.Log(st + " maaaap " + lt_cart.ContainsKey(st));
            if (lt_cart.ContainsKey(st))
            {

                GameObject gm = Instantiate(GameObject.Find("ignoring").transform.Find("cart").gameObject, GameObject.Find("Content tip").transform);
                gm.GetComponent<Image>().sprite = cart_sprite[st];
                gm.GetComponent<ui_button>().type = st;
                //string st_dop = "";
                gm.transform.Find("tx max").gameObject.active = (pr_mob.carts_count_max.ContainsKey(st) && lt_cart[st] == pr_mob.carts_count_max[st]);
                 
                gm.transform.Find("tx").GetComponent<TMP_Text>().text = lt_cart[st].ToString();
            }
        }
    }
    public static void print_carts_art(int num)
    {
        if (func.mob_list!= null && func.mob_list[num]!=null)
        {
            //
            par_mob pr_mob = func.mob_list[num].GetComponent<par_mob>();
            List<par_cart_art> lt_cart = new List<par_cart_art>(pr_mob.lt_carts_art);
            foreach (Transform tr in GameObject.Find("Content art").transform)
            {
                Destroy(tr.gameObject);
            }
            int i = 1;
            //Debug.Log("print art  "+ pr_mob.col_art);
            for (int i1 = 0; i1 <  pr_mob.col_art; i1++)
            {
                GameObject gm = Instantiate(GameObject.Find("Scroll View art").transform.Find("Viewport").Find("art").gameObject, GameObject.Find("Content art").transform);
                gm.transform.Find("im").gameObject.GetComponent<ui_button>().num = i - 1;
                gm.name = "art" + i;
                
                
                if (i<=lt_cart.Count)
                {
                    string st = lt_cart[i - 1].name;
                    gm.transform.Find("im").GetComponent<Image>().sprite = cart_sprite[st];
                    gm.transform.Find("im").GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                    gm.transform.Find("im").GetComponent<ui_button>().type = st;
                }
                gm.active = true;
                i++;
            }
            /*
            foreach (par_cart_art st1 in lt_cart)
            {
                

            }
            */
        }

    }
    public static void update_pn_players()
    {
        foreach (Transform tr in GameObject.Find("pn players").transform)
        {
            if (tr.gameObject.active && tr.Find("bt icon").gameObject.active)
            {
                tr.gameObject.GetComponent<Image>().color = new Vector4(0.3f, 0.3f, 0.3f, 1);
                tr.Find("bt icon").gameObject.gameObject.GetComponent<Image>().color = new Vector4(0.3f, 0.3f, 0.3f, 1);
            }
        }
        foreach (Transform tr in GameObject.Find("infa art").transform)
        {
            if (tr.gameObject.name!="Text art")
            {
                Destroy(tr.gameObject);
            }
        }
        foreach (Transform tr in GameObject.Find("infa st").transform)
        {
            if (tr.gameObject.name != "Text cart")
            {
                Destroy(tr.gameObject);
            }
        }
        foreach (data.players pl in data.data_now.lt_players)
        {
            string nm = null;
            if (pl == data.players.bases)
            {
                nm = "icon bases";
            }
            else if (pl == data.players.atack)
            {
                nm = "icon att";
            }
            else if (pl == data.players.barier)
            {
                nm = "icon bar";
            }
            else if (pl == data.players.fire)
            {
                nm = "icon fire";
            }
            else if (pl == data.players.blood)
            {
                nm = "icon blood";
            }
            else if (pl == data.players.poition)
            {
                nm = "icon poition";
            }
            else if (pl == data.players.rock)
            {
                nm = "icon rock";
            }
            else if (pl == data.players.ice)
            {
                nm = "icon ice";
            }
            else if (pl == data.players.energy)
            {
                nm = "icon energy";
            }
            else if (pl == data.players.berserk)
            {
                nm = "icon berserk";
            }
            else if (pl == data.players.derlorg)
            {
                nm = "icon derlorg";
            }
            //Debug.Log("ui true");
            if (pl == data.data_now.player_now)
            {
                //Debug.Log("ui true "+ pl.ToString());
                GameObject.Find(nm).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                
                GameObject.Find("Image pl").transform.Find("Image").gameObject.GetComponent<Animator>().Play(pl.ToString());
                foreach (data.par_player pl1 in data.data_now.lt_par_pl)
                {
                    if (pl1.type == pl)
                    {
                        GameObject.Find("name pl").GetComponent<Text>().text = pl1.name;
                        GameObject.Find("Text infa").GetComponent<Text>().text = pl1.infa;
                        foreach (string st in pl1.lt_art_st)
                        {
                            GameObject obj = Instantiate(GameObject.Find("bt cart(base)"),GameObject.Find("infa").transform.Find("pn carts st").Find("infa art"));
                            obj.GetComponent<ui_button>().type = st;
                            obj.name = "bt cart";
                            obj.GetComponent<Image>().sprite = cart_sprite[st];
                            //продолжить тут
                        }
                        foreach (string st in pl1.lt_carts_st.Keys)
                        {
                            GameObject obj = Instantiate(GameObject.Find("bt cart(base)"), GameObject.Find("infa").transform.Find("pn carts st").Find("infa st"));
                            obj.GetComponent<ui_button>().type = st;
                            obj.name = "bt cart";
                            obj.GetComponent<Image>().sprite = cart_sprite[st];
                            obj.transform.Find("tx").gameObject.GetComponent<TMP_Text>().text = pl1.lt_carts_st[st].ToString();
                            //продолжить тут
                        }

                        LayoutRebuilder.ForceRebuildLayoutImmediate(GameObject.Find("infa").transform.Find("pn carts st").gameObject.GetComponent<RectTransform>());
                        
                    }
                }
            }
                
            GameObject.Find(nm).transform.Find("bt icon").gameObject.GetComponent<Image>().color = new Vector4(1, 1,1, 1);
            
        }


    }
    public static void update_pn_mob(int i)
    {
        

        GameObject pn = GameObject.Find("panels").transform.Find("pn infa mob").Find("infa mob").gameObject;
        par_mob mb = func.mob_list[i].GetComponent<par_mob>();
        pn.transform.Find("tx dist atc 1").gameObject.GetComponent<Text>().text = mb.distanse_attack.ToString();
        pn.transform.Find("tx dist rad 1").gameObject.GetComponent<Text>().text = mb.distanse_max.ToString();
        pn.transform.Find("tx speed 1").gameObject.GetComponent<Text>().text = mb.speed.ToString();
        

        
        pn.transform.Find("bt next").gameObject.active = (func.col_friend > 1);
        /*
        pn.transform.Find("tg wolk").gameObject.GetComponent<ui_toogle>().num = i;
        pn.transform.Find("tg wolk").gameObject.active = false;
        pn.transform.Find("tg wolk").gameObject.GetComponent<Toggle>().isOn = !mb.wolked;

        pn.transform.Find("tg wolk").gameObject.active = (i != 0);
        
        */

    }
    // Update is called once per frame
}
