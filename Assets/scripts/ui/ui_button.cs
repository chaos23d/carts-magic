using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class ui_button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public ui ui;

    public int num = -1;
    public string type;

    public float timer = 0;
    public bool down = false;
    public bool isCart = false;
    void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("CLICK");
            down = true;
            timer = 0;
        

    }
    void OnPointerUp(PointerEventData eventData)
    {
            down = false;
            if (GameObject.Find("pn infa")!=null && GameObject.Find("pn infa").GetComponent<Animator>().GetBool("opened"))
                opened_closed(GameObject.Find("pn infa"));
        

    }
    public bool friend()
    {
        return func.mob_list[func.hod_mob_now_int].GetComponent<par_mob>().friend;
    }
    public GameObject friend_now()
    {
        return func.mob_list[func.hod_mob_now_int];
    }
    public void per_menu_inf()
    {
        SceneManager.LoadScene("infa game");

    }
    public void per_menu_pl()
    {
        SceneManager.LoadScene("menu player");

    }
    public void per_game()
    {
        SceneManager.LoadScene("Scene");

    }
    public void per_achive()
    {
        SceneManager.LoadScene("achives");

    }
    public void per_back()
    {
        SceneManager.LoadScene("menu");

    }
    public void active_anim_cam()
    {
        GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
        GameObject.Find("tx lvl").GetComponent<Animator>().enabled = true;
    }
    public void activate_settings_friend()
    {
        bool bl = true;

        if (type == "false")
            type = "true";
        else
            type = "false";


        if (type == "false")
            bl = false;

        GameObject.Find("panels").transform.Find("pn infa mob").gameObject.active = bl;
        Image bt_im = GameObject.Find("bt friend settings").GetComponent<Image>();

        if (bl)
        {
            Time.timeScale = 0;
            Color cl = bt_im.color;
            cl.a = 1;
            bt_im.color = cl;
        }

        else
        {
            Time.timeScale = 1;
            Color cl = bt_im.color;
            cl.a = 0.4f;
            bt_im.color = cl;

            GameObject.Find("panels").transform.Find("Image").Find("bt cart art").gameObject.active = true;
            GameObject.Find("panels").transform.Find("Image").Find("bt cart tip").gameObject.active = true;

        }
        GameObject.Find("Main Camera").transform.parent = func.mob_list[func.hod_mob_now_friend].transform;
        GameObject.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
        
    }

    public void select_player()
    {
        if(data.data_now.lt_players.Contains((data.players)System.Enum.Parse( typeof( data.players),type)))
        {
            //GameObject.Find("Image pl").GetComponent<Animator>().SetBool("opened", true);
            Debug.Log("uu " + type);
            data.data_now.player_now= (data.players)System.Enum.Parse(typeof(data.players), type);
            GameObject.Find("Image pl").GetComponent<Animator>().SetBool("opened", false);
        }
    }
    public void select_type_atack(string par)
    {
        ui_atack.type_atack type = ui_atack.type_atack.bases;
        if(par == "bases")
        {
            type = ui_atack.type_atack.bases;
            GameObject.Find("tx atack").GetComponent<Text>().text = "комбинаци€ примен€етс€ при нажатии на любую клетку";
        }
            
        else if (par == "mob")
        {
            type = ui_atack.type_atack.mob;
            GameObject.Find("tx atack").GetComponent<Text>().text = "комбинаци€ примен€етс€ при нажатии на врага";
        }
            
        else if (par == "friend")
        {
            type = ui_atack.type_atack.friend;
            GameObject.Find("tx atack").GetComponent<Text>().text = "комбинаци€ примен€етс€ при нажатии на союзника";
        }
            
        else if (par == "mob_friend")
        {
            type = ui_atack.type_atack.mob_friend;
            GameObject.Find("tx atack").GetComponent<Text>().text = "комбинаци€ примен€етс€ при нажатии на любое сущесво";
        }
            
        else if (par == "eath")
        {
            type = ui_atack.type_atack.eath;
           
            GameObject.Find("tx atack").GetComponent<Text>().text = "комбинаци€ примен€етс€ при нажатии на свободную клетку";
        }
        ui_atack.last_type_atack = type;
        GameObject.Find("bt type atack").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        ui_atack.atack_comb_now.atack_type = type;
    }
    public void add_cart_atack()
    {
        ui_atack.atack_comb_now.carts.Enqueue(type);
        if (GameObject.Find("Scroll carts atack").transform.Find("Viewport").Find("Content carts atack").childCount > 0)
            Instantiate(GameObject.Find("ignoring").transform.Find("napr").gameObject, GameObject.Find("Scroll carts atack").transform.Find("Viewport").Find("Content carts atack"));
        GameObject gm_cart =Instantiate(GameObject.Find("ignoring").transform.Find("rama cart").gameObject, GameObject.Find("Scroll carts atack").transform.Find("Viewport").Find("Content carts atack"));
        gm_cart.transform.Find("cart dop").gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        
    }
    public void del_cart_atack()
    {
        if (ui_atack.atack_comb_now.carts.Count > 0)
        {
            ui_atack.atack_comb_now.carts.Dequeue();
            Transform tr = GameObject.Find("Scroll carts atack").transform.Find("Viewport").Find("Content carts atack");
            int c = ui_atack.atack_comb_now.carts.Count;
            Destroy(tr.GetChild(tr.childCount-1).gameObject);
            Debug.Log(ui_atack.atack_comb_now.carts.Count);
            if(c!=0)
            {
                Destroy(tr.GetChild(tr.childCount - 2).gameObject);
            }
        }
        
    }
    public void add_atack()
    {
        ui_atack.lt_cart_comb.Add(ui_atack.atack_comb_now);

        GameObject gm = Instantiate(GameObject.Find("ignoring").transform.Find("pn comb carts").gameObject, GameObject.Find("pn atack").transform.Find("Viewport").Find("Content"));
        gm.transform.Find("Image").Find("ic atack").GetComponent<Image>().sprite = GameObject.Find("bt type atack").GetComponent<Image>().sprite;
        gm.GetComponent<ui_button>().num = ui_atack.lt_cart_comb.Count - 1;
        foreach (string st in ui_atack.atack_comb_now.carts)
        {
            GameObject gg =Instantiate(GameObject.Find("ignoring").transform.Find("cart icon atack").gameObject, gm.transform.Find("mini pn comb carts"));
            gg.GetComponent<Image>().sprite = GameObject.Find(st).GetComponent<SpriteRenderer>().sprite;
            
        }
        ui_atack.lt_cart_comb_ui.Add(gm);
    }
    public void del_atack()
    {
        
        List<GameObject> lt_rem = new List<GameObject>();
        foreach (ui_atack.atack_comb at in ui_atack.lt_cart_comb_selected)
        {
            lt_rem.Add(ui_atack.lt_cart_comb_ui[ui_atack.lt_cart_comb.IndexOf(at)]);
            //Debug.Log(ui_atack.lt_cart_comb_selected.IndexOf(at));
            //ui_atack.lt_cart_comb_ui.Add(gm);
            //GameObject gg = Instantiate(GameObject.Find("ignoring").transform.Find("cart icon atack").gameObject, gm.transform.Find("mini pn comb carts"));
            //gg.GetComponent<Image>().sprite = GameObject.Find(st).GetComponent<SpriteRenderer>().sprite;
        }
        foreach (GameObject g in lt_rem)
        {
            ui_atack.lt_cart_comb_ui.Remove(g);
            Destroy(g);
        }

        ui_atack.lt_cart_comb.RemoveAll(x=>ui_atack.lt_cart_comb_selected.Contains(x));
        ui_atack.lt_cart_comb_selected.Clear();

        for (int i = 0; i < ui_atack.lt_cart_comb.Count; i++)
        {
            ui_atack.lt_cart_comb_ui[i].GetComponent<ui_button>().num = i;
        }
    }
    public void select_atack()
    {
        ui_atack.atack_comb comb = ui_atack.lt_cart_comb[num];
        if (ui_atack.lt_cart_comb_selected.Contains(comb))
        {
            gameObject.GetComponent<Outline>().enabled = false;
            ui_atack.lt_cart_comb_selected.Remove(comb);
        }
        else
        {
            bool bl = true;
            foreach(ui_atack.atack_comb comb_now in ui_atack.lt_cart_comb_selected)
            {
                if (comb_now.atack_type == comb.atack_type)
                    bl = false;
                if (comb_now.atack_type == ui_atack.type_atack.bases || comb.atack_type == ui_atack.type_atack.bases)
                    bl = false;
                if ((comb_now.atack_type == ui_atack.type_atack.mob || comb_now.atack_type == ui_atack.type_atack.friend) && comb.atack_type == ui_atack.type_atack.mob_friend)
                    bl = false;
                if ((comb.atack_type == ui_atack.type_atack.mob || comb.atack_type == ui_atack.type_atack.friend) && comb_now.atack_type == ui_atack.type_atack.mob_friend)
                    bl = false;
            }
            if (bl)
            {
                Debug.Log("true selected");
                ui_atack.lt_cart_comb_selected.Add(comb);
                gameObject.GetComponent<Outline>().enabled = true;
            }
            

        }
        
    }
    public void ui_cart()
    {
        if (friend())
        {
            par_mob mb = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
            int col = 0;
            foreach (string st in mb.lt_carts)
            {
                if (st== func.carts[mb.x, mb.y].GetComponent<par_cart>().name)
                    col++;
            }
            //Debug.Log(func.carts[mb.x, mb.y].GetComponent<par_cart>().art_cart_bl()+"   button");
            if(!mb.carts_count_max.ContainsKey(func.carts[mb.x, mb.y].name) || mb.carts_count_max[func.carts[mb.x, mb.y].GetComponent<par_cart>().name] > col)
            {

                mb.add_cart(func.carts[mb.x, mb.y].GetComponent<par_cart>());

                if (func.carts[mb.x, mb.y].GetComponent<par_cart_art>() != null && func.carts[mb.x, mb.y].GetComponent<par_cart_art>().first)
                {
                    if (data.data_now.col_carts_art < 50)
                    {
                        data.data_now.col_carts_art++;
                        if (data.data_now.col_carts_art == 50)
                            data.give_achive("любитель артефактов");
                    }
                }
                if(func.carts[mb.x, mb.y].GetComponent<par_cart>().col == 0)
                {
                    Destroy(func.carts[mb.x, mb.y]);
                    func.carts[mb.x, mb.y] = null;
                }

                opened_closed(gameObject);
            }

        }

    }
    public void next_friend()
    {
        int q = GameObject.Find("Main Camera").transform.parent.gameObject.GetComponent<par_mob>().num;
        q++;
        Debug.Log("buttobn "+q);
        for (int i = 0; i < func.size_global * func.size_global; i++)
        {
            if (func.mob_list[q] != null && func.mob_list[q].GetComponent<par_mob>().friend)
                break;
            if (q < func.size_global * func.size_global - 1)
                q++;
            else
                q = 0;
        }
        Debug.Log("buttobn 1 "+q);
        GameObject mb = func.mob_list[q];

        GameObject.Find("Main Camera").transform.parent = mb.transform;
        GameObject.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);

        if (mb.GetComponent<par_mob>().num != 0)
        {
            GameObject.Find("panels").transform.Find("pn infa mob").Find("infa mob").Find("Dropdown").gameObject.active = true;

            TMP_Dropdown dr = GameObject.Find("panels").transform.Find("pn infa mob").Find("infa mob").Find("Dropdown").gameObject.GetComponent<TMP_Dropdown>();
            par_mob pr_mb = mb.GetComponent<par_mob>();
            if (pr_mb.friend_type == par_mob.type_friend.auto)
            {
                dr.value=0;
            }
            else if (pr_mb.friend_type == par_mob.type_friend.active)
            {
                dr.value = 1;
            }
            else if (pr_mb.friend_type == par_mob.type_friend.disactive)
            {
                dr.value = 2;
            }
        }
        else
        {
            GameObject.Find("panels").transform.Find("pn infa mob").Find("infa mob").Find("Dropdown").gameObject.active = false;
        }
       

        GameObject par = GameObject.Find("panels").transform.Find("Image").gameObject;
        int nm = func.hod_mob_now_friend;
        if (nm != q && par.transform.Find("bt cart art").gameObject.active && par.transform.Find("bt cart tip").gameObject.active)
        {
            par.transform.Find("bt cart art").gameObject.active = false;
            par.transform.Find("bt cart tip").gameObject.active = false;
            GameObject.Find("Scroll View art").GetComponent<Animator>().SetBool("opened", false);
            GameObject.Find("Scroll View tip").GetComponent<Animator>().SetBool("opened",false);
        }
        else if( nm == q &&!par.transform.Find("bt cart art").gameObject.active && !par.transform.Find("bt cart tip").gameObject.active)
        {
            par.transform.Find("bt cart art").gameObject.active = true;
            par.transform.Find("bt cart tip").gameObject.active = true;
        }

        ui.update_pn_mob(q);
    }
    public void update_pn_mob()
    {
        ui.update_pn_mob(0);
    }

    public void simm_y(GameObject gm)
    {
        Vector3 v3 = gm.transform.localScale;
        v3.y = -v3.y;
        gm.transform.localScale = v3;
    }
    public void equip()
    {
        if (friend() && type!=null && type!="")
        {
            par_mob mb = friend_now().GetComponent<par_mob>();
            //par_mob pr_mob = func.mob_list[num].GetComponent<par_mob>();
            Dictionary<string, int> lt_cart = new Dictionary<string, int>();
            foreach (string st in mb.lt_carts)
            {
                if (!lt_cart.ContainsKey(st))
                    lt_cart.Add(st, 1);
                else
                    lt_cart[st]++;
            }
            /*
            if(type== "cart sp derlorg" || type == "cart derlorg act" || type== "cart derlorg speed"|| type== "cart derlorg potoc" || type== "cart derlorg pow")
            {
                foreach (Transform tr in GameObject.Find("list carts").transform)
                {
                    if (mb.carts_count_max.ContainsKey(tr.gameObject.name))
                    {
                        if (mb.carts_count_max[tr.gameObject.name] < 2)
                            return;
                    }
                }
            }
            */
            
            if (mb.col_art > mb.lt_carts_art.Count)
            {
                //mb.add_cart(func.carts[mb.x, mb.y].GetComponent<par_cart>());
                mb.remove_cart(type);
                mb.add_cart_art(type);
                //func.carts[mb.x, mb.y] = null;
                opened_closed(GameObject.Find("bt drop"));
                opened_closed(gameObject);
            }
            
        }

    }
    public void uiequip()
    {
        if (friend() && type!=null && type!="")
        {
            //Debug.Log("click" + type);
            par_mob mb = friend_now().GetComponent<par_mob>();


            Dictionary<string, int> lt_cart = new Dictionary<string, int>();
            foreach (string st in mb.lt_carts)
            {
                if (!lt_cart.ContainsKey(st))
                    lt_cart.Add(st, 1);
                else
                    lt_cart[st]++;
            }
            if(!(type== "cart mast" && mb.col_art == mb.lt_carts_art.Count) && !(type == "cart inven" && mb.col_art == mb.lt_carts_art.Count+1))
            {
                mb.remove_cart_art(type);
                //mb.add_cart(type);
                //func.carts[mb.x, mb.y] = null;
                opened_closed(gameObject);
            }
            //mb.add_cart(func.carts[mb.x, mb.y].GetComponent<par_cart>());

            //ui.print_carts_art(mb.num);
            //ui.print_carts_art(mb.num);

        }

    }
    public void drop()
    {
        if (friend() && type != null && type != "")
        {
            Debug.Log("click" + type);
            par_mob mb = friend_now().GetComponent<par_mob>();

            if (func.drop_bool(mb.x,mb.y))
            {
                //Debug.Log(func.carts[mb.x, mb.y].GetComponent<par_cart>().art_cart_bl() + "   button");
                //mb.add_cart(func.carts[mb.x, mb.y].GetComponent<par_cart>());



                GameObject gm_cart= func.drop(type, mb.x, mb.y);
                if (gm_cart.GetComponent<par_cart_art>() != null)
                    gm_cart.GetComponent<par_cart_art>().first = false;
                gm_cart.active = true;
                mb.remove_cart(type);
                //mb.add_cart(type);
                //func.carts[mb.x, mb.y] = null;
                
                opened_closed(gameObject);
                opened_closed(GameObject.Find("bt equip"));

                GameObject gold_nps = null;

                if (func.mob[mb.x - 1, mb.y] != null && func.mob[mb.x - 1, mb.y].name == "nps gold goblin")
                    gold_nps = func.mob[mb.x - 1, mb.y];
                else if (func.mob[mb.x + 1, mb.y] != null && func.mob[mb.x + 1, mb.y].name == "nps gold goblin")
                    gold_nps = func.mob[mb.x + 1, mb.y];
                else if (func.mob[mb.x, mb.y - 1] != null && func.mob[mb.x, mb.y - 1].name == "nps gold goblin")
                    gold_nps = func.mob[mb.x, mb.y - 1];
                else if (func.mob[mb.x, mb.y + 1] != null && func.mob[mb.x, mb.y + 1].name == "nps gold goblin")
                    gold_nps = func.mob[mb.x, mb.y + 1];

                if (gold_nps!=null)
                {
                    gold_nps.GetComponent<par_mob>().spawn_cart(type, func.eath[mb.x,mb.y]);
                    for (int i = 0; i < func.baza[type].gameObject.GetComponent<par_cart>().lvl; i++)
                    {
                        mb.GetComponent<par_mob>().spawn_cart("cart gold", func.eath[mb.x, mb.y]);
                    }
                    Destroy(gm_cart);
                    func.carts[mb.x, mb.y] = null;

                    //anim.col_anim++;
                    //bers_nps.GetComponent<Animator>().SetBool("act", true);
                }
                else
                {
                    GameObject.Find("bt up").GetComponent<Animator>().SetBool("opened", true);
                }

            }
            //mb.add_cart(func.carts[mb.x, mb.y].GetComponent<par_cart>());

            //ui.print_carts_art(mb.num);
            //ui.print_carts_art(mb.num);

        }

    }
    public void opened_closed_bl (GameObject gm)
    {
        if (timer < 0.75f && type != null && type != "")
            opened_closed(gm);
    }
    public void opened_closed(GameObject gm)
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();


        //Debug.Log("naaaame " + gm.name);
        //Debug.Log("art scr " + GameObject.Find("Scroll View art").GetComponent<Animator>().GetBool("opened"));
        //Debug.Log("tip scr " + GameObject.Find("Scroll View tip").GetComponent<Animator>().GetBool("opened"));
        //Debug.Log(gameObject.transform.parent.parent.gameObject.name == "Content art");
        if (gm.name != "pn infa" && gm.name != "bt stop")
        {
            if (gameObject.transform.parent != null && gameObject.transform.parent.gameObject.name == "Content tip")
            {
                //Debug.Log("true cont tip");
                Vector2 v2 = gameObject.transform.position;
                gm.transform.position = v2;
                Debug.Log(v2 + " v2");
                v2 = gm.GetComponent<RectTransform>().anchoredPosition;
                Debug.Log(v2 + " v2_0");
                if (gm.name == "bt equip")
                    v2.y += 12.5f;
                else if (gm.name == "bt drop")
                    v2.y -= 12.5f;
                gm.GetComponent<RectTransform>().anchoredPosition = v2;
                Debug.Log(v2+" v2_1");

            }
                
            else if (gameObject.transform.parent.parent != null && gameObject.transform.parent.parent.gameObject.name == "Content art")
                gm.transform.position = gameObject.transform.position;
            if (gm.name == "bt equip" || gm.name == "bt unequip" || gm.name == "bt drop")
                gm.GetComponent<ui_button>().type = type;
        }
        else if (gm.name == "pn infa")
        {

            par_cart cart = GameObject.Find(type).GetComponent<par_cart>();
            par_mob mob = func.mob_now();
            if (cart is par_cart_art)
            {
                if (((par_cart_art)cart).perez != 0)
                {
                    gm.transform.Find("pn art").Find("perez").gameObject.active = true;
                    gm.transform.Find("pn art").Find("perez max").gameObject.active = true;
                }
                else
                {
                    gm.transform.Find("pn art").Find("perez").gameObject.active = false;
                    gm.transform.Find("pn art").Find("perez max").gameObject.active = false;
                }
                if (((par_cart_art)cart).q_max != 0)
                {
                    //gm.transform.Find("pn art q").Find("q").gameObject.active = true;
                    gm.transform.Find("pn art q").Find("q max").gameObject.active = true;
                }
                else
                {
                    //gm.transform.Find("pn art q").Find("q").gameObject.active = false;
                    gm.transform.Find("pn art q").Find("q max").gameObject.active = false;
                }

                

                if (gameObject.transform.parent.gameObject.name == "Content tip")
                {
                    if (((par_cart_art)cart).q != 0)
                    {
                        //gm.transform.Find("pn art q").Find("q").gameObject.active = true;
                        gm.transform.Find("pn art q").Find("q").gameObject.active = true;
                    }
                    else
                    {
                        //gm.transform.Find("pn art q").Find("q").gameObject.active = false;
                        gm.transform.Find("pn art q").Find("q").gameObject.active = false;
                    }
                    //tyt
                    gm.transform.Find("pn art q").Find("q").gameObject.GetComponent<Text>().text = "зар€д\n" + ((par_cart_art)cart).q.ToString();
                    gm.transform.Find("pn art q").Find("q max").gameObject.GetComponent<Text>().text = "max зар€д\n" + ((par_cart_art)cart).q_max.ToString();

                    gm.transform.Find("pn art").Find("perez").gameObject.GetComponent<Text>().text = "перезар€дка\n" + ((par_cart_art)cart).perez.ToString();
                    gm.transform.Find("pn art").Find("perez max").gameObject.GetComponent<Text>().text = "max перезар€дка\n" + ((par_cart_art)cart).perez.ToString();


                }

                else if (gameObject.transform.parent.parent != null && gameObject.transform.parent.parent.gameObject.name == "Content art")
                {
                    //par_mob mob = func.mob_now();
                    if (mob.lt_carts_art[num].q != 0)
                    {
                        gm.transform.Find("pn art q").Find("q").gameObject.active = true;
                        //gm.transform.Find("pn art q").Find("q max").gameObject.active = true;
                    }
                    else
                    {
                        gm.transform.Find("pn art q").Find("q").gameObject.active = false;
                        //gm.transform.Find("pn art q").Find("q max").gameObject.active = false;
                    }
                    if (mob.friend)
                    {
                        gm.transform.Find("pn art").Find("perez").gameObject.GetComponent<Text>().text = "перезар€дка\n" + mob.lt_carts_art[num].perez_now.ToString();
                        gm.transform.Find("pn art").Find("perez max").gameObject.GetComponent<Text>().text = "max перезар€дка\n" + mob.lt_carts_art[num].perez.ToString();

                        gm.transform.Find("pn art q").Find("q").gameObject.GetComponent<Text>().text = "зар€д\n" + mob.lt_carts_art[num].q.ToString(); ;
                        gm.transform.Find("pn art q").Find("q max").gameObject.GetComponent<Text>().text = "max зар€д\n" + mob.lt_carts_art[num].q_max.ToString() ;
                        //Debug.Log(mob.lt_carts_art[num].q_max.ToString() + "   q maaaax");
                        //Debug.Log(mob.lt_carts_art[num].q.ToString() + "   q "+ ((par_cart_art)cart).q);
                    }

                }
                else if (SceneManager.GetActiveScene().name == "menu player")
                {
                    gm.transform.Find("pn art").Find("perez").gameObject.GetComponent<Text>().text = "перезар€дка\n" + 0;
                    gm.transform.Find("pn art").Find("perez max").gameObject.GetComponent<Text>().text = "max перезар€дка\n" + ((par_cart_art)cart).perez.ToString();
                }
            }
            else
            {
                gm.transform.Find("pn art").Find("perez").gameObject.active = false;
                gm.transform.Find("pn art").Find("perez max").gameObject.active = false;

                gm.transform.Find("pn art q").Find("q").gameObject.active = false;
                gm.transform.Find("pn art q").Find("q max").gameObject.active = false;
            }
            gm.transform.Find("name").gameObject.GetComponent<Text>().text = cart.name_real + "\n" + "(мощность " + cart.lvl + ")";
            //string st_color = "";
            //foreach (char item in cart.infa)
            //{

            //}
            string[] stt = cart.infa.Split('|');
            gm.transform.Find("infa").gameObject.GetComponent<Text>().text = "";
            string st1 = "";
            foreach (string st in stt)
            {
                st1 += st + "\n";

            }
            gm.transform.Find("infa").gameObject.GetComponent<Text>().text = st1;
            //gm.transform.Find("infa").gameObject.GetComponent<Text>().text = cart.infa;
            LayoutRebuilder.ForceRebuildLayoutImmediate(gm.GetComponent<RectTransform>());
        }
        
        if (gm.name == "Scroll View art" && GameObject.Find("Scroll View tip").GetComponent<Animator>().GetBool("opened") && !gm.GetComponent<Animator>().GetBool("opened"))
        {
            Debug.Log("tip 1 ");
            GameObject.Find("Scroll View tip").GetComponent<Animator>().SetBool("opened", false);
        }
        else if (gm.name == "Scroll View tip" && GameObject.Find("Scroll View art").GetComponent<Animator>().GetBool("opened") && !gm.GetComponent<Animator>().GetBool("opened"))
        {
            Debug.Log("art 1 ");
            GameObject.Find("Scroll View art").GetComponent<Animator>().SetBool("opened", false);
        }
        gm.GetComponent<Animator>().SetBool("opened", !gm.GetComponent<Animator>().GetBool("opened"));
        if(gm.name== "Scroll View art" || gm.name == "Scroll View tip")
        {
            GameObject.Find("bt equip").GetComponent<Animator>().SetBool("opened", false);
            GameObject.Find("bt unequip").GetComponent<Animator>().SetBool("opened", false);
            GameObject.Find("bt drop").GetComponent<Animator>().SetBool("opened", false);
        }
    }
    public void stop_cart()
    {
        foreach (Transform tr in GameObject.Find(data.data_now.pl_name_now).transform.Find("carts active"))
        {
            Destroy(tr.gameObject);
        }
        par_mob mb = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
        foreach (string st in mb.lt_carts_active_now)
        {
            mb.add_cart(st);
        }
        mb.lt_carts_active_now.Clear();
        mb.lt_carts_active_now_ui.Clear();
        //mb.par_lt_sila = mb.par_lt_sila_save;
        ui.clean_pricels();

        //anim.col_anim--;
        GameObject.Find(data.data_now.pl_name_now).GetComponent<Animator>().SetBool("atack", false);
        GameObject.Find(data.data_now.pl_name_now).GetComponent<Animator>().SetBool("selected", false);
        GameObject.Find(data.data_now.pl_name_now).GetComponent<Animator>().SetBool("perehod", false);
        opened_closed(gameObject);
    }
    public void select_cart()
    {
        if (friend() && timer < 0.75f&& anim.col_anim==0 )
        {
            par_mob mob = func.mob_list[func.hod_mob_now_int].GetComponent<par_mob>();
            bool atack = true;
            foreach (par_baf baf in mob.lt_bafs)
            {
                if (baf.type == par_baf.type_baf.seal)
                    atack = false;
            }


            if (GameObject.Find(type).GetComponent<par_cart>().art_cart_bl() && mob.lt_carts_active_now.Count == 0)
            {
                opened_closed(GameObject.Find("bt equip"));
                opened_closed(GameObject.Find("bt drop"));
            }
            else if (type!="cart gold" && !mob.selected_anim && atack)
            {

                List<string> lt_other = new List<string>(mob.lt_carts_active_now);
                lt_other.Add(type);
                //now1 = par_cart.cart_combinations.lt_comb[6];

                //Debug.Log(now.name + " " + type+" "+ now.next.ContainsKey(type)+" " );
                if(mob.proverka_atack(lt_other,true,false))
                {
                    mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                        //Debug.Log(true);
                    }
                    mob.gameObject.GetComponent<Animator>().SetBool("select_now", true);
                    mob.gameObject.GetComponent<Animator>().SetBool("perehod", false);
                    mob.selected_anim = true;

                    int[,] way_now = func.way(mob.x, mob.y, mob.distanse_attack);
                    for (int i = mob.y - mob.distanse_attack; i <= mob.y + mob.distanse_attack; i++)
                    {
                        if (i > 0 && i < func.size_global - 1)
                        {
                            for (int j = mob.x - mob.distanse_attack; j <= mob.x + mob.distanse_attack; j++)
                            {
                                if (j > 0 && j < func.size_global - 1)
                                {
                                    if (way_now[j, i] != -1)
                                    {// 
                                        if ((func.baza[lt_other[0]].GetComponent<par_cart>().type==par_cart.type_cart.damage) && ( (func.nature[j, i] != null && func.nature[j, i].active && func.nature[j, i].GetComponent<par_nat>().dam) || (func.mob[j, i] != null && func.mob[j, i].GetComponent<par_mob>().vizited) ) )
                                        {
                                            ui.create_pricel(func.eath[j, i].transform.position);
                                        }

                                    }
                                }
                            }
                        }
                    }


                    mob.cart_active_now = type;
                    mob.lt_carts_active_now.Add(type);
                    mob.lt_carts_active_now_ui.Add(type);
                    mob.remove_cart(type);
                }
            }


            if (mob.lt_carts_active_now.Count > 0 && func.mob_list[func.hod_mob_now_int].name == data.data_now.pl_name_now && !GameObject.Find("bt stop").GetComponent<Animator>().GetBool("opened"))
            {
                opened_closed(GameObject.Find("bt stop"));
            }
        }

    }
    public void restart()
    {
        func.clean_func();
        SceneManager.LoadScene("menu player");

    }
    public void next_dialog(ui_anim_text tx_anim)
    {
        par_dialog dialog_now = par_dialog.par_dialog_now;

        if (tx_anim.end)
        {
            if (dialog_now.lt_dialog[dialog_now.type_now].Count - 1 > dialog_now.id_now)
            {
                dialog_now.id_now++;
                dialog_now.start_dialog();
            }
            else
            {
                par_dialog.end_dialog();
                //GameObject.Find("canvases").transform.Find("Canvas dialog").gameObject.SetActive(false);
            }
        }
        else
        {
            tx_anim.end_text();
        }
        
    }
    void Start()
    {
        ui = GameObject.Find("Main Camera").GetComponent<ui>();

    }

    // Update is called once per frame
    void locate_pn_infa()
    {
        int const_d_size = 50;

        RectTransform ap = GameObject.Find("pn infa").GetComponent<RectTransform>();
        Vector2 v2 = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 v2_m = Input.mousePosition;
        Vector2 v2_c = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta;
        float f = GameObject.Find("Canvas").GetComponent<Canvas>().scaleFactor;

        v2_m.x = v2_m.x/f ;
        v2_m.y = v2_m.y /f;

        //v2.y = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().rect.height + ap.sizeDelta.y / 2;
        //  += ;
        if (v2_m.x < v2_c.x / 2)
        {
            if (v2_m.y+ap.sizeDelta.y > v2_c.y)
                v2.y = v2_m.y - (v2_m.y + ap.sizeDelta.y - v2_c.y);
            else
                v2.y = v2_m.y;


            if (v2_m.x + ap.sizeDelta.x > v2_c.x)
                v2.x = v2_m.x - (v2_m.x + ap.sizeDelta.x - v2_c.x);
            else
                v2.x = v2_m.x+const_d_size;
        }
        else
        {
            if (v2_m.y + ap.sizeDelta.y > v2_c.y)
                v2.y = v2_m.y - (v2_m.y + ap.sizeDelta.y - v2_c.y);
            else
                v2.y = v2_m.y;


            if (v2_m.x- ap.sizeDelta.x <0)
                v2.x = v2_m.x - (v2_m.x - ap.sizeDelta.x);
            else
                v2.x = v2_m.x - ap.sizeDelta.x-const_d_size;
        }
        
        //Debug.Log(v2 + "  111  "+v2_m+"   "+v2_c+"    "+ap.sizeDelta);
        GameObject.Find("pn infa").GetComponent<RectTransform>().anchoredPosition = v2;
    }
    void FixedUpdate()
    {
        
        //locate_pn_infa();
        //Debug.Log(Input.mousePosition);
        if (down && timer < 0.75f && isCart)
        {
            
            timer += Time.fixedDeltaTime;

            if (timer >= 0.75f)
            {
                opened_closed(GameObject.Find("pn infa"));
                locate_pn_infa();
            }
            //if (gameObject.transform.parent.gameObject.name == "Content tip" && timer >= 1)
            //{
                //opened_closed(GameObject.Find("pn infa"));
                /*
                RectTransform ap = GameObject.Find("pn infa").GetComponent<RectTransform>();
                Vector2 v2 = gameObject.GetComponent<RectTransform>().anchoredPosition;
                Debug.Log(v2 + "  0");
                v2.y = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().rect.height + ap.sizeDelta.y/2;
                //  += ;
                if (v2.x + ap.sizeDelta.x / 2 >= gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().rect.width)
                    v2.x = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().rect.width-ap.sizeDelta.x / 2;
                else if(v2.x - ap.sizeDelta.x / 2<0)
                    v2.x = ap.sizeDelta.x / 2;
                //Debug.Log(v2 + "  1  "+ ap.sizeDelta.y);
                GameObject.Find("pn infa").GetComponent<RectTransform>().anchoredPosition =  v2;
                */
                //locate_pn_infa();
            //}
            //else if ((gameObject.transform.parent.parent != null && gameObject.transform.parent.parent.gameObject.name == "Content art") && timer >= 1)
            //{
                //opened_closed(GameObject.Find("pn infa"));
                /*
                RectTransform ap = GameObject.Find("pn infa").GetComponent<RectTransform>();
                Vector2 v2 = new Vector2(0, 0);
                //v2.y = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().rect.height - v2.y;
                

                v2.x = gameObject.transform.parent.parent.parent.gameObject.GetComponent<RectTransform>().rect.width + ap.sizeDelta.x/2;
                v2.y = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height + gameObject.transform.parent.localPosition.y- 50+GameObject.Find("Scroll View tip").GetComponent<RectTransform>().rect.height;
                Debug.Log(v2 + "  0");
                if (v2.y + ap.sizeDelta.y / 2 >= GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height)
                    v2.y = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height - ap.sizeDelta.y / 2;
                else if (v2.y - ap.sizeDelta.y / 2 < 0)
                    v2.y = ap.sizeDelta.y / 2;
                //v2 = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
                //Debug.Log(v2 + "  1 "  + (gameObject.transform.position- GameObject.Find("Canvas").transform.position));
                GameObject.Find("pn infa").GetComponent<RectTransform>().anchoredPosition = v2;
                */
               // locate_pn_infa();
            //}
        }
            
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown(eventData);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        OnPointerUp(eventData);
    }
}
