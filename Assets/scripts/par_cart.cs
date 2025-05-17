using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class par_cart : MonoBehaviour
{
    public static class cart_combinations
    {

        public static void Dictionary_in_atack()
        {

        }
        public class comb_cart 
        {
            public string name;
            public comb_cart first;
            public Dictionary <string,comb_cart> next = new Dictionary<string, comb_cart>();
            public int position;
        }
        private static void add(string st)
        {
            string[] lt_cart = st.Split("-");
            int i_pos = 0;
            List<comb_cart> start = new List<comb_cart>();
            List<string> lt_e = new List<string> { "cart rock", "cart energy", "cart blood", "cart fire", "cart ice", "cart poition" };
            List<string> lt_e_d = new List<string> { "cart spirit rock", "cart spirit energy", "cart spirit blood", "cart spirit fire", "cart spirit ice", "cart spirit poition" };
            List<comb_cart> cart_now=new List<comb_cart>();
            foreach (string cart in lt_cart)
            {
                List<string> lt_add = new List<string>();
                if (cart.Contains("[n]"))
                {
                    string cart_base = cart.Replace("[n]", "");
                    for (int i = 0; i < 4; i++)
                    {
                        string st_dop = "";
                        if (i != 0)
                            st_dop = " "+i.ToString();
                        if (cart.Contains("[e]"))
                        {
                            foreach (string st_e in lt_e)
                            {
                                lt_add.Add(st_e + st_dop);
                            }
                        }
                        else
                        {
                            lt_add.Add(cart_base + st_dop);
                        }
                    }

                }
                else if (cart.Contains("[e]"))
                {
                    foreach (string st_e in lt_e)
                    {
                        lt_add.Add(st_e);
                    }
                }
                else if (cart.Contains("[e_d]"))
                {
                    foreach (string st_e in lt_e_d)
                    {
                        lt_add.Add(st_e);
                    }
                }
                else
                {
                    lt_add.Add(cart);
                }
                List<comb_cart> cart_now_add = new List<comb_cart>();
                foreach (string cart_dop in lt_add)
                {
                    comb_cart c_cart = new comb_cart();
                    c_cart.name = cart_dop;
                    c_cart.position = i_pos;
                    
                    if (i_pos == 0)
                    {
                        start.Add(c_cart);
                        lt_comb.Add(c_cart);
                        c_cart.first = c_cart;
                        cart_now_add = start;

                    }
                    else
                    {
                        cart_now_add.Add(c_cart);
                        foreach (comb_cart ct in cart_now)
                        {

                            //comb_cart cart_now = ct;
                            //for (int i = 0; i < i_pos-1; i++)
                            //{
                            //cart_now = cart_now.next;
                            //}
                            //ct.before = cart_now;
                            c_cart.first = ct.first;
                            ct.next.Add(cart_dop, c_cart);
                        }
                        
                    }
                }
                cart_now = cart_now_add;

                i_pos++;
            }
        }
        public static void print()
        {
            foreach (comb_cart cart in lt_comb)
            {
                Debug.Log("");
                Debug.Log("");
                //Debug.Log(cart.name);
                print(cart,new List<string>() );
            }
        }
        public static void print(comb_cart cart_now,List<string> lt)
        {
            List<string> lt1 = new List<string>(lt);
            lt1.Add(cart_now.name);
            if (cart_now.next.Count > 0)
            {
                foreach (comb_cart cart_1 in cart_now.next.Values)
                {
                    //Debug.Log(cart_1.name);

                    print(cart_1,lt1);
                }
            }
            
            else
            {
                Debug.Log("");
                Debug.Log("");
                foreach (string st in lt1)
                {
                    Debug.Log(st);
                }
            }
            
        }
        public static void collections()
        {
            string command;
            command = "cart damage[n]";
            add(command);
            command = "cart barier[n]";
            add(command);
            command = "cart [e][n]";
            add(command);

            command = "cart crystall[n]-cart [e][n]";
            add(command);
            command = "cart crystall[n]-cart [e_d]";
            add(command);

            command = "cart crystall[n]-cart damage[n]";
            add(command);
            command = "cart crystall[n]-cart damage[n]-cart [e][n]";
            add(command);
            command = "cart crystall[n]-cart barier[n]";
            add(command);
            command = "cart crystall[n]-cart barier[n]-cart [e][n]";
            add(command);


            command = "cart spider[n]";
            add(command);
            command = "cart spider poition[n]";
            add(command);
            command = "cart derlorg[n]";
            add(command);
            command = "cart dungalorg(sleep)[n]";
            add(command);
            command = "cart rage(boss)";
            add(command);
            command = "cart damage[n]-cart [e][n]";
            //command = "cart damage[n]-";
            add(command);
            command = "cart barier[n]-cart [e][n]";
            add(command);
            command = "cart spirit(boss)-cart [e][n]";
            add(command);

            command = "cart derlorg[n]-cart [e][n]";
            add(command);
            command = "cart derlorg[n]-cart damage[n]";
            add(command);
            command = "cart derlorg[n]-cart barier[n]";
            add(command);

            command = "cart dungalorg(sleep)-cart derlorg[n]";
            add(command);

            command = "cart [e_d]";
            add(command);

            for (int i = 0; i < 3; i++)
            {
                string st_dop = "";
                if (i != 0)
                    st_dop = " " + i.ToString();
                command = "cart cuznec"+st_dop+"-cart damage"+st_dop + "-cart damage" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart barier" + st_dop + "-cart barier" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart poition" + st_dop + "-cart poition" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart fire" + st_dop + "-cart fire" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart blood" + st_dop + "-cart blood" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart rock" + st_dop + "-cart rock" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart energy" + st_dop + "-cart energy" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart ice" + st_dop + "-cart ice" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart crystall" + st_dop + "-cart crystall" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart derlorg" + st_dop + "-cart derlorg" + st_dop;
                add(command);

                if (i < 2)
                {
                    command = "cart cuznec" + st_dop + "-cart cuznec" + st_dop + "-cart cuznec" + st_dop;
                    add(command);
                }



                command = "cart cuznec" + st_dop + "-cart att pow" + st_dop + "-cart att pow" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart bar pow" + st_dop + "-cart bar pow" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart poition pow" + st_dop + "-cart poition pow" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart fire pow" + st_dop + "-cart fire pow" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart blood pow" + st_dop + "-cart blood pow" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart rock pow" + st_dop + "-cart rock pow" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart energy pow" + st_dop + "-cart energy pow" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart ice pow" + st_dop + "-cart ice pow" + st_dop;
                add(command);

                command = "cart cuznec" + st_dop + "-cart sp atack" + st_dop + "-cart sp atack" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp barier" + st_dop + "-cart sp barier" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp poition" + st_dop + "-cart sp poition" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp fire" + st_dop + "-cart sp fire" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp blood" + st_dop + "-cart sp blood" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp rock" + st_dop + "-cart sp rock" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp energy" + st_dop + "-cart sp energy" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp ice" + st_dop + "-cart sp ice" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp crystall" + st_dop + "-cart sp crystall" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart sp derlord" + st_dop + "-cart sp derlord" + st_dop;
                add(command);


                //command = "cart cuznec" + st_dop + "-cart sp atack" + st_dop + "-cart sp atack" + st_dop;
                //add(command);
                //command = "cart cuznec" + st_dop + "-cart sp barier" + st_dop + "-cart sp barier" + st_dop;
                //add(command);
                command = "cart cuznec" + st_dop + "-cart act poition" + st_dop + "-cart act poition" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart act fire" + st_dop + "-cart act fire" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart act blood" + st_dop + "-cart act blood" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart act rock" + st_dop + "-cart act rock" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart act energy" + st_dop + "-cart act energy" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart act ice" + st_dop + "-cart act ice" + st_dop;
                add(command);
                /*
                command = "cart cuznec" + st_dop + "-cart act crystall" + st_dop + "-cart act crystall" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart act derlord" + st_dop + "-cart act derlord" + st_dop;
                add(command);
                */


                command = "cart cuznec" + st_dop + "-cart speed poition" + st_dop + "-cart speed poition" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart speed fire" + st_dop + "-cart speed fire" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart speed blood" + st_dop + "-cart speed blood" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart speed rock" + st_dop + "-cart speed rock" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart speed energy" + st_dop + "-cart speed energy" + st_dop;
                add(command);
                command = "cart cuznec" + st_dop + "-cart speed ice" + st_dop + "-cart speed ice" + st_dop;
                add(command);
            }

            
            //print();
        }
        public static List<comb_cart> lt_comb = new List<comb_cart>();//кароч. Это содержит стартовую точку карт
        public static Dictionary<comb_cart,string> lt_comb_if;
    }

    public int lvl = 1;
    public type_cart type;
    public string name = null;
    public string name_real = null;
    public int col = 1;
    public string infa;
    public bool sum_comb_other = true;
    public bool spetial = false;

    public bool open = true;
    public enum type_cart
    {
        
        bases,

        rock,
        fire,
        poition,
        blood,
        energy,
        ice,
        derlorg,
        sp_barier,
        damage,
        sp_du,
        sp_comb,
        sp_pricel,

        art_rock_sp,
        art_rock_act,

        art_blood_sp,
        art_blood_act,

        art_energy_act,
        art_energy_sp,

        art_fire_sp,
        art_fire_act,

        art_ice_sp,
        art_ice_act,

        art_poition_sp,
        art_poition_act,

        art_berserk,
        art_hp_max,

        art_potoc_blood,
        art_potoc_fire,
        art_potoc_rock,
        art_potoc_energy,
        art_potoc_ice,
        art_potoc_poition,

        art_speed_blood,
        art_speed_fire,
        art_speed_rock,
        art_speed_energy,
        art_speed_ice,
        art_speed_poition,

        art_mast,

        art_att_sp,
        art_bar_sp,
        art_att_pow,
        art_bar_pow,

        art_base_st,
        art_att_st,
        art_bar_st,
        art_rock_st,
        art_fire_st,
        art_blood_st,
        art_energy_st,
        art_ice_st,
        art_poition_st,
        art_berserk_st,

        art_dop_carts,
        art_pow,
        art_speed,
        art_respawn,

        gold,
        sp_cuznec,
        sp_crystall,

        art_pow_blood,
        art_pow_fire,
        art_pow_energy,
        art_pow_poition,
        art_pow_rock,
        art_pow_ice,

        art_crystall_sp,
        art_pow_crystall,
        art_cuzna,

        art_derlorg_sp,

        art_spirit_ice,
        art_spirit_poition,
        art_spirit_energy,

        art_spirit_rock,
        art_spirit_blood,
        art_spirit_fire,


        art_boss_shaman,
        art_boss_shaman_tuman,
        shaman_boss,
        art_boss_berserk,
        art_boss_berserk_rage,
        rage_boss,

        art_derlorg_glass,

        dungalorg,
        dungalorg_sleep,


        art_boss_dungalorg_heard,
        art_boss_dungalorg,

        art_boss_kaitalorg_heard,
        art_boss_kaitalorg,

        art_derlorg_st,


        sp_spider,
        poition_spider,
        art_spider_sp,
        art_spider_spawned_sp,


        art_touch_blood,
        art_touch_fire,
        art_touch_energy,
        art_touch_poition,
        art_touch_rock,
        art_touch_ice,

        art_elemental,
        art_universal,

        art_fire_cuzna,
        art_poition_cuzna,
        art_ice_cuzna,
        art_energy_cuzna,
        art_blood_cuzna,
        art_rock_cuzna,

        spirit_rock,
        spirit_fire,
        spirit_poition,
        spirit_blood,
        spirit_energy,
        spirit_ice,

        art_fire_crystall,
        art_poition_crystall,
        art_ice_crystall,
        art_energy_crystall,
        art_blood_crystall,
        art_rock_crystall,

        art_fire_heart,
        art_poition_heart,
        art_ice_heart,
        art_energy_heart,
        art_blood_heart,
        art_rock_heart,

        art_fire_mast,
        art_poition_mast,
        art_ice_mast,
        art_energy_mast,
        art_blood_mast,
        art_rock_mast,

        art_sila
    }

    public static List<type_cart> get_types_tip()
    {
        List<type_cart> lt = new List<type_cart>();
        lt.Add(type_cart.damage);
        lt.Add(type_cart.sp_barier);
        lt.Add(type_cart.sp_crystall);

        lt.Add(type_cart.rock);
        lt.Add(type_cart.blood);
        lt.Add(type_cart.ice);
        lt.Add(type_cart.energy);
        lt.Add(type_cart.fire);
        lt.Add(type_cart.poition);

        lt.Add(type_cart.spirit_rock);
        lt.Add(type_cart.spirit_blood);
        lt.Add(type_cart.spirit_ice);
        lt.Add(type_cart.spirit_energy);
        lt.Add(type_cart.spirit_fire);
        lt.Add(type_cart.spirit_poition);

        lt.Add(type_cart.derlorg);

        lt.Add(type_cart.sp_spider);
        lt.Add(type_cart.poition_spider);


        lt.Add(type_cart.shaman_boss);
        lt.Add(type_cart.rage_boss);

        return lt;
    }
    public bool sp_cart_bl()
    {
        return (type == type_cart.sp_barier ||
            type == type_cart.sp_du ||
            type == type_cart.sp_comb ||
            type == type_cart.damage ||
            type == type_cart.sp_pricel||
            type == type_cart.shaman_boss ||
            type == type_cart.derlorg
            );
    }
    public bool hard_cart_bl()
    {
        return type == type_cart.sp_cuznec || type == type_cart.dungalorg_sleep;
    }
    public bool art_cart_bl()
    {
        return (type == type_cart.art_blood_sp ||
            type == type_cart.art_blood_act ||
            type == type_cart.art_rock_sp ||
            type == type_cart.art_rock_act ||
            type == type_cart.art_fire_sp ||
            type == type_cart.art_fire_act ||
            type == type_cart.art_energy_sp ||
            type == type_cart.art_energy_act ||
            type == type_cart.art_ice_sp ||
            type == type_cart.art_ice_act ||
            type == type_cart.art_poition_sp ||
            type == type_cart.art_poition_act ||

            type == type_cart.art_potoc_blood ||
            type == type_cart.art_potoc_fire ||
            type == type_cart.art_potoc_rock ||
            type == type_cart.art_potoc_energy ||
            type == type_cart.art_potoc_ice ||
            type == type_cart.art_potoc_poition ||

            type == type_cart.art_speed_blood ||
            type == type_cart.art_speed_fire ||
            type == type_cart.art_speed_rock ||
            type == type_cart.art_speed_energy ||
            type == type_cart.art_speed_ice ||
            type == type_cart.art_speed_poition ||

            type == type_cart.art_pow_blood ||
            type == type_cart.art_pow_fire ||
            type == type_cart.art_pow_rock ||
            type == type_cart.art_pow_energy ||
            type == type_cart.art_pow_ice ||
            type == type_cart.art_pow_poition ||

            type == type_cart.art_blood_cuzna ||
            type == type_cart.art_rock_cuzna ||
            type == type_cart.art_ice_cuzna ||
            type == type_cart.art_energy_cuzna ||
            type == type_cart.art_poition_cuzna ||
            type == type_cart.art_fire_cuzna ||

            type == type_cart.art_blood_crystall ||
            type == type_cart.art_rock_crystall ||
            type == type_cart.art_ice_crystall ||
            type == type_cart.art_energy_crystall ||
            type == type_cart.art_poition_crystall ||
            type == type_cart.art_fire_crystall ||


            type == type_cart.art_att_sp ||
            type == type_cart.art_bar_sp ||

            type == type_cart.art_crystall_sp ||

            type == type_cart.art_att_pow ||
            type == type_cart.art_bar_pow ||

            type == type_cart.art_dop_carts ||
            type == type_cart.art_pow ||
            type == type_cart.art_speed ||
            type == type_cart.art_respawn ||

            type == type_cart.art_base_st ||
            type == type_cart.art_att_st ||
            type == type_cart.art_bar_st ||
            type == type_cart.art_fire_st ||
            type == type_cart.art_poition_st ||
            type == type_cart.art_ice_st ||
            type == type_cart.art_blood_st ||
            type == type_cart.art_energy_st ||
            type == type_cart.art_rock_st ||
            type == type_cart.art_berserk_st ||


            type == type_cart.art_berserk ||
            type == type_cart.art_hp_max ||
            type == type_cart.art_mast||
            type == type_cart.art_elemental||
            type == type_cart.art_universal ||
            type == type_cart.art_touch_rock ||

            type == type_cart.art_derlorg_sp ||

            type ==type_cart.art_spirit_fire||
            type == type_cart.art_spirit_rock ||
            type == type_cart.art_spirit_blood ||
            type == type_cart.art_spirit_energy||
            type == type_cart.art_spirit_poition ||
            type == type_cart.art_spirit_ice||

            type == type_cart.art_touch_blood ||
            type == type_cart.art_touch_energy ||
            type == type_cart.art_touch_fire ||
            type == type_cart.art_touch_ice ||
            type == type_cart.art_touch_poition ||
            type == type_cart.art_touch_rock ||

            type == type_cart.art_boss_shaman||
            type == type_cart.art_boss_shaman_tuman ||
            type == type_cart.art_boss_berserk ||
            type == type_cart.art_boss_berserk_rage||
            type == type_cart.art_boss_dungalorg ||
            type == type_cart.art_boss_dungalorg_heard||

            type==type_cart.art_derlorg_st

            );
    }
    public void Start()
    {
        if(SceneManager.GetActiveScene().name == "Scene")
        {
            
            if (gameObject.name == "cart speed" && data.data_now.lt_achives_open.Contains("быстрее некуда"))
                open = true;
            if (gameObject.name == "cart power" && data.data_now.lt_achives_open.Contains("большой потенциал"))
                open = true;
            if (gameObject.name == "cart respawn" && data.data_now.lt_achives_open.Contains("не в первый раз"))
                open = true;
            if (gameObject.name == "cart inven" && data.data_now.lt_achives_open.Contains("большая колода"))
                open = true;
            if (gameObject.name == "cart cuzna" && data.data_now.lt_achives_open.Contains("Кузнец"))
                open = true;

            func func = GameObject.Find("func").GetComponent<func>();
            if (!func.baza.ContainsKey(gameObject.name))
            {
                //Debug.Log(gameObject.name);
                func.baza.Add(gameObject.name, gameObject);
                name = gameObject.name;
                #region typ
                if (name == "cart damage")
                    infa = "наносит 1 урона живому существу| |можно использовать для уничтожения препятствий| |позволяет комбинировать с картой-элементом для усиления атаки";
                if (name == "cart damage 1")
                    infa = "наносит 2 урона живому существу| |можно использовать для уничтожения препятствий| |позволяет комбинировать с картой-элементом для усиления атаки";
                if (name == "cart damage 2")
                    infa = "наносит 4 урона живому существу| |можно использовать для уничтожения препятствий| |позволяет комбинировать с картой-элементом для усиления атаки";
                if (name == "cart barier")
                    infa = "создаёт барьер, защищающий от 2 урона| |позволяет комбинировать с картой-элементом для усиления защиты";
                if (name == "cart barier 1")
                    infa = "создаёт барьер, защищающий от 4 урона| |позволяет комбинировать с картой-элементом для усиления защиты";
                if (name == "cart barier 2")
                    infa = "создаёт барьер, защищающий от 4 урона| |позволяет комбинировать с картой-элементом для усиления защиты";
                if (name == "cart fire")
                    infa = "карта элемента огня| |поджигает живое существо на 2 хода";
                if (name == "cart fire 1")
                    infa = "карта элемента огня| |поджигает живое существо 2 раза на 2 хода";
                if (name == "cart fire 2")
                    infa = "карта элемента огня| |поджигает живое существо 4 раза на 2 хода";
                if (name == "cart poition")
                    infa = "карта элемента яда| |отравляет живое существо на 3 хода";
                if (name == "cart poition 1")
                    infa = "карта элемента яда| |отравляет живое существо на 6 ходов";
                if (name == "cart poition 2")
                    infa = "карта элемента яда| |отравляет живое существо на 12 ходов";
                if (name == "cart ice")
                    infa = "карта элемента льда| |замедляет живое существо на 2 хода";
                if (name == "cart ice 1")
                    infa = "карта элемента льда| |замедляет живое существо на 4 хода";
                if (name == "cart ice 2")
                    infa = "карта элемента льда| |замедляет живое существо на 8 ходов";
                if (name == "cart rock")
                    infa = "карта элемента земли| |наносит 1 урон";
                if (name == "cart rock 1")
                    infa = "карта элемента земли| |наносит 2 урона";
                if (name == "cart rock 2")
                    infa = "карта элемента земли| |наносит 4 урона";
                if (name == "cart energy")
                    infa = "карта элемента электричества| |ускоряет живое существо на 2 хода";
                if (name == "cart energy 1")
                    infa = "карта элемента электричества| |ускоряет живое существо на 4 хода";
                if (name == "cart energy 2")
                    infa = "карта элемента электричества| |ускоряет живое существо на 8 хода";
                if (name == "cart blood")
                    infa = "карта элемента крови| |исцеляет живое существо на 1";
                if (name == "cart blood 1")
                    infa = "карта элемента крови| |исцеляет живое существо на 2";
                if (name == "cart blood 2")
                    infa = "карта элемента крови| |исцеляет живое существо на 4";
                if (name == "cart crystall")
                    infa = "позволяет комбинировать с картами для создания взрывного кристалла|кристалл взрывается через ход, создавая магический эффект от карт| с удвоенной мощностью в центре и обычной мощностью в радиусе 1";
                if (name == "cart crystall 1")
                    infa = "позволяет комбинировать с картами для создания взрывного кристалла|кристалл взрывается через ход, создавая магический эффект от карт| с удвоенной мощностью в центре и обычной мощностью в радиусе 1";
                if (name == "cart crystall 2")
                    infa = "позволяет комбинировать с картами для создания взрывного кристалла|кристалл взрывается через ход, создавая магический эффект от карт| с удвоенной мощностью в центре и обычной мощностью в радиусе 1";
                if (name == "cart spider")
                    infa = "при применении создаёт союзного паука с здоровьем 1| |позволяет применять эту карту на союзного паука для его усиления";
                if (name == "cart gold")
                    infa = "карта, состоящая из золота| |не обладает особыми свойствами, но хорошо ценится как местная валюта";
                if (name == "cart cuznec")
                    infa = "позволяет комбинировать с 2 одинаковыми картами мощности 1| для объединения в более мощную карту мощности 2(касается всех видов карт)";


                if (name == "cart spider")
                    infa = "при применении на свободную клетку создаёт союзного паука|при применении на существующего(призванного) паука усиливает его";
                if (name == "cart spider poition")
                    infa = "наносит 1 урона живому существу";

                #endregion


                #region art_st
                if (name == "cart base st")
                    infa = "создаёт карту атаки мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)";
                if (name == "cart att st")
                    infa = "создаёт 2 карты атаки мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для карты атаки мощности 1(пассивный)| |применение карты атаки уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                if (name == "cart bar st")
                    infa = "создаёт карту атаки и карту защиты мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для карты защиты мощности 1(пассивный)| |применение карты защиты уменьшает перезарядку всех артефактов на 2(но не менее 1)(пассивный)";
                if (name == "cart fire st")
                    infa = "создаёт огненную карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для огненной карты мощности 1(пассивный)| |применение огненной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                if (name == "cart poition st")
                    infa = "создаёт ядовитую карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для ядовитой карты мощности 1(пассивный)| |применение ядовитой карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                if (name == "cart rock st")
                    infa = "создаёт каменную карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для каменной карты мощности 1(пассивный)| |применение каменной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                if (name == "cart blood st")
                    infa = "создаёт кровавую карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для кровавой карты мощности 1(пассивный)| |применение кровавой карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                if (name == "cart ice st")
                    infa = "создаёт ледяную карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для ледяной карты мощности 1(пассивный)| |применение ледяной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                if (name == "cart energy st")
                    infa = "создаёт электрическую карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для электрической карты мощности 1(пассивный)| |применение электрической карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                if (name == "cart berserk st")
                    infa = "создаёт карту атаки мощности 1 и добавляет в инвентарь| |+1 к мощности применения карты за каждые 5 единиц отсутствующего здоровья(пассивный)";
                if (name == "cart derlorg st")
                    infa = "создаёт карту дерлорга и карту атаки мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для карты дерлорга мощности 1(пассивный)| |применение обычной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)| |применение сцециальной карты уменьшает перезарядку всех артефактов на 2(но не менее 1)(пассивный)";

                #endregion

                if (name == "cart sp atack")
                    infa = "создаёт карту атаки мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)";
                if (name == "cart sp atack 1")
                    infa = "создаёт карту атаки мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для карты атаки мощности 2(пассивный)";
                if (name == "cart sp barier")
                    infa = "создаёт карту защиты мощности 1 и добавляет в инвентарь| |+1 к месту в инвентаре для карты защиты мощности 1(пассивный)";
                if (name == "cart sp barier 1")
                    infa = "создаёт карту защиты мощности 2 и добавляет в инвентарь| |+1 к месту в инвентаре для карты защиты мощности 2(пассивный)";

                if (name == "cart sp spider")
                    infa = "создаёт карту паучего кокона мощности 1 и добавляет в инвентарь| |+1 к месту в инвентаре для карты паучего кокона мощности 1(пассивный)";
                if (name == "cart sp spider(spawned)")
                    infa = "создаёт карту паучей кислоты и добавляет в инвентарь| |+2 к месту в инвентаре для карты паучей кислоты(пассивный)|+1 к мощности карты паучей кислоты за каждый дополнительный такой же артефакт";

                if (name == "cart att pow")
                    infa = "+1 к мощности карты атаки";
                if (name == "cart att pow 1")
                    infa = "+2 к мощности карты атаки";
                if (name == "cart bar pow")
                    infa = "+1 к мощности карты защиты";
                if (name == "cart bar pow 1")
                    infa = "+2 к мощности карты защиты";


                #region art_sp
                if (name == "cart sp fire")
                    infa = "создаёт огненную карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart sp poition")
                    infa = "создаёт ядовитую карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)";
                if (name == "cart sp rock")
                    infa = "создаёт каменную карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)";
                if (name == "cart sp blood")
                    infa = "создаёт кровавую карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)";
                if (name == "cart sp ice")
                    infa = "создаёт ледяную карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)";
                if (name == "cart sp energy")
                    infa = "создаёт электрическую карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)";

                if (name == "cart sp fire 1")
                    infa = "создаёт огненную карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart sp poition 1")
                    infa = "создаёт ядовитую карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)";
                if (name == "cart sp rock 1")
                    infa = "создаёт каменную карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)";
                if (name == "cart sp blood 1")
                    infa = "создаёт кровавую карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)";
                if (name == "cart sp ice 1")
                    infa = "создаёт ледяную карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)";
                if (name == "cart sp energy 1")
                    infa = "создаёт электрическую карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)";

                if (name == "cart sp fire 2")
                    infa = "создаёт огненную карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart sp poition 2")
                    infa = "создаёт ядовитую карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";
                if (name == "cart sp rock 2")
                    infa = "создаёт каменную карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";
                if (name == "cart sp blood 2")
                    infa = "создаёт кровавую карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";
                if (name == "cart sp ice 2")
                    infa = "создаёт ледяную карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";
                if (name == "cart sp energy 2")
                    infa = "создаёт электрическую карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";

                #endregion
                #region art_act
                if (name == "cart act fire")
                    infa = "+1 к силе применения огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart act poition")
                    infa = "+1 к силе применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)";
                if (name == "cart act rock")
                    infa = "+1 к силе применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)";
                if (name == "cart act blood")
                    infa = "+1 к силе применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)";
                if (name == "cart act ice")
                    infa = "+1 к силе применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)";
                if (name == "cart act energy")
                    infa = "+1 к силе применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)";

                if (name == "cart act fire 1")
                    infa = "+2 к силе применения карты| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart act poition 1")
                    infa = "+2 к силе применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)";
                if (name == "cart act rock 1")
                    infa = "+2 к силе применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)";
                if (name == "cart act blood 1")
                    infa = "+2 к силе применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)";
                if (name == "cart act ice 1")
                    infa = "+2 к силе применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)";
                if (name == "cart act energy 1")
                    infa = "+2 к силе применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)";

                if (name == "cart act fire 2")
                    infa = "+4 к силе применения карты| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart act poition 2")
                    infa = "+4 к силе применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";
                if (name == "cart act rock 2")
                    infa = "+4 к силе применения каменной карты| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";
                if (name == "cart act blood 2")
                    infa = "+4 к силе применения кровавой карты| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";
                if (name == "cart act ice 2")
                    infa = "+4 к силе применения ледяной карты| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";
                if (name == "cart act energy 2")
                    infa = "+4 к силе применения электрической карты| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";
                #endregion
                #region art_speed

                if (name == "cart speed fire")
                    infa = "+1 к скорости применения огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1";
                if (name == "cart speed poition")
                    infa = "+1 к скорости применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1";
                if (name == "cart speed rock")
                    infa = "+1 к скорости применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1";
                if (name == "cart speed blood")
                    infa = "+1 к скорости применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1";
                if (name == "cart speed ice")
                    infa = "+1 к скорости применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1";
                if (name == "cart speed energy")
                    infa = "+1 к скорости применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1";

                if (name == "cart speed fire 1")
                    infa = "+2 к скорости применения огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1 и 2";
                if (name == "cart speed poition 1")
                    infa = "+2 к скорости применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2";
                if (name == "cart speed rock 1")
                    infa = "+2 к скорости применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1 и 2";
                if (name == "cart speed blood 1")
                    infa = "+2 к скорости применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2";
                if (name == "cart speed ice 1")
                    infa = "+2 к скорости применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2";
                if (name == "cart speed energy 1")
                    infa = "+2 к скорости применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1 и 2";

                if (name == "cart speed fire 2")
                    infa = "+4 к скорости применения огненной карты| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart speed poition 2")
                    infa = "+4 к скорости применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";
                if (name == "cart speed rock 2")
                    infa = "+4 к скорости применения каменной карты| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";
                if (name == "cart speed blood 2")
                    infa = "+4 к скорости применения кровавой карты| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";
                if (name == "cart speed ice 2")
                    infa = "+4 к скорости применения ледяной карты| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";
                if (name == "cart speed energy 2")
                    infa = "+4 к скорости применения электрической карты| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";
                #endregion

                #region art_pow
                if (name == "cart fire pow")
                    infa = "+1 к мощности огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart poition pow")
                    infa = "+1 к мощности ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)";
                if (name == "cart rock pow")
                    infa = "+1 к мощности каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)";
                if (name == "cart blood pow")
                    infa = "+1 к мощности кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)";
                if (name == "cart ice pow")
                    infa = "+1 к мощности ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)";
                if (name == "cart energy pow")
                    infa = "+1 к мощности электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)";

                if (name == "cart fire pow 1")
                    infa = "+2 к мощности огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart poition pow 1")
                    infa = "+2 к мощности ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)";
                if (name == "cart rock pow 1")
                    infa = "+2 к мощности каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)";
                if (name == "cart blood pow 1")
                    infa = "+2 к мощности кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)";
                if (name == "cart ice pow 1")
                    infa = "+2 к мощности ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)";
                if (name == "cart energy pow 1")
                    infa = "+2 к мощности электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)";

                if (name == "cart fire pow 2")
                    infa = "+4 к мощности огненной карты| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart poition pow 2")
                    infa = "+4 к мощности ядовитой карты| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";
                if (name == "cart rock pow 2")
                    infa = "+4 к мощности каменной карты| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";
                if (name == "cart blood pow 2")
                    infa = "+4 к мощности кровавой карты| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";
                if (name == "cart ice pow 2")
                    infa = "+4 к мощности ледяной карты| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";
                if (name == "cart energy pow 2")
                    infa = "+4 к мощности электрической карты| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";
                #endregion
                #region art_cuzna
                if (name == "cart fire cuzna")
                    infa = "сохраняет карту кузнеца при объединении обычных огненных карт| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart poition cuzna")
                    infa = "сохраняет карту кузнеца при объединении обычных ядовитых карт| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)";
                if (name == "cart rock cuzna")
                    infa = "сохраняет карту кузнеца при объединении обычных каменных карт| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)";
                if (name == "cart blood cuzna")
                    infa = "сохраняет карту кузнеца при объединении обычных кровавых карт| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)";
                if (name == "cart ice cuzna")
                    infa = "сохраняет карту кузнеца при объединении обычных ледяных карт| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)";
                if (name == "cart energy cuzna")
                    infa = "сохраняет карту кузнеца при объединении обычных электрических карт| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)";


                if (name == "cart fire cuzna 1")
                    infa = "сохраняет карту кузнеца при объединении обычных огненных карт| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart poition cuzna 1")
                    infa = "сохраняет карту кузнеца при объединении обычных ядовитых карт| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)";
                if (name == "cart rock cuzna 1")
                    infa = "сохраняет карту кузнеца при объединении обычных каменных карт| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)";
                if (name == "cart blood cuzna 1")
                    infa = "сохраняет карту кузнеца при объединении обычных кровавых карт| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)";
                if (name == "cart ice cuzna 1")
                    infa = "сохраняет карту кузнеца при объединении обычных ледяных карт| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)";
                if (name == "cart energy cuzna 1")
                    infa = "сохраняет карту кузнеца при объединении обычных электрических карт| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)";

                if (name == "cart fire cuzna 2")
                    infa = "сохраняет карту кузнеца при объединении обычных огненных карт| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart poition cuzna 2")
                    infa = "сохраняет карту кузнеца при объединении обычных ядовитых карт| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";
                if (name == "cart rock cuzna 2")
                    infa = "сохраняет карту кузнеца при объединении обычных каменных карт| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";
                if (name == "cart blood cuzna 2")
                    infa = "сохраняет карту кузнеца при объединении обычных кровавых карт| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";
                if (name == "cart ice cuzna 2")
                    infa = "сохраняет карту кузнеца при объединении обычных ледяных карт| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";
                if (name == "cart energy cuzna 2")
                    infa = "сохраняет карту кузнеца при объединении обычных электрических карт| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";
                #endregion
                #region art_crystall
                if (name == "cart fire crystall")
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart poition crystall")
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart ice crystall")
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart energy crystall")
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart blood crystall")
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";
                if (name == "cart rock crystall")
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)";


                if (name == "cart fire crystall 1")
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart poition crystall 1")
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart ice crystall 1")
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart energy crystall 1")
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart blood crystall 1")
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart rock crystall 1")
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";

                if (name == "cart fire crystall 2")
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart poition crystall 2")
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart ice crystall 2")
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart energy crystall 2")
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart blood crystall 2")
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart rock crystall 2")
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                #endregion

                if (name == "cart fire touch")
                    infa = "при применении карт дополнительно применяет| огненную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                if (name == "cart poition touch")
                    infa = "при применении карт дополнительно применяет| ядовитую карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                if (name == "cart rock touch")
                    infa = "при применении карт дополнительно применяет| каменную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                if (name == "cart blood touch")
                    infa = "при применении карт дополнительно применяет| каменную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                if (name == "cart ice touch")
                    infa = "при применении карт дополнительно применяет| ледяную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                if (name == "cart energy touch")
                    infa = "при применении карт дополнительно применяет| электрическую карту мощности 1(+1 за каждый дополнительный такой же артефакт)";



                if (name == "cart sp crystall")
                    infa = "создаёт карту кристалла мощности 1 и добавляет в инвентарь| |+1 к месту в инвентаре для карты кристалла мощности 1(пассивный)";
                if (name == "cart sp crystall 1")
                    infa = "создаёт карту кристалла мощности 2 и добавляет в инвентарь| |+1 к месту в инвентаре для карты кристалла мощности 2(пассивный)";



                if (name == "cart berserk")
                    infa = "даёт эффект ярости(2 хода за каждые 5 единиц отсутствующего здоровья):| |+1 к скорости применения всех карт|+1 к силе применения всех карт|+1 к мощности применения всех карт| |перезарядка происходит только за счёт полученного урона";
                if (name == "cart dop hill")
                    infa = "+5 к максимальному запасу здоровья";

                if (name == "cart mini spirit")
                    infa = "карта-элемент наделяется силой души при применении";

                if (name == "cart inven")
                    infa = "+2 места в инвентаре для обычных карт и +1 место в инвентаре для специальных карт| |+2 дополнительных слота для артефакта| |можно использовать не более 2 таких карт";
                if (name == "cart mast")
                    infa = "применение обычной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)| |применение сцециальной карты уменьшает перезарядку всех артефактов на 2(но не менее 1)(пассивный)| |+1 дополнительный слот для артефакта| |можно использовать не более 4 таких карт";
                if (name == "cart universal")
                    infa = "создаёт 2 карты атаки , карту защиты и карту кристалла|мощности 1 и добавляет в инвентарь";
                if (name == "cart elemental")
                    infa = "создаёт карту каждого элемента мощности 1 и добавляет в инвентарь";


                if (name == "cart potoc fire")
                    infa = "дополнительно поглощает полученный магический эффект в виде огненных карт| |+1 к мощности огненной карты за каждые 4 единицы мощности огненных карт в инвентаре";
                if (name == "cart potoc poition")
                    infa = "дополнительно поглощает полученный магический эффект в виде ядовитых карт| |+1 к мощности ядовитой карты за каждые 4 единицы мощности ядовитых карт в инвентаре";
                if (name == "cart potoc rock")
                    infa = "дополнительно поглощает полученный магический эффект в виде каменных карт| |+1 к мощности каменной карты за каждые 4 единицы мощности каменных карт в инвентаре";
                if (name == "cart potoc blood")
                    infa = "дополнительно поглощает полученный магический эффект в виде кровавых карт| |+1 к мощности кровавой карты за каждые 4 единицы мощности кровавых карт в инвентаре";
                if (name == "cart potoc ice")
                    infa = "дополнительно поглощает полученный магический эффект в виде ледяных карт| |+1 к мощности ледяной карты за каждые 4 единицы мощности ледяных карт в инвентаре";
                if (name == "cart potoc energy")
                    infa = "дополнительно поглощает полученный магический эффект в виде электрических карт| |+1 к мощности электрической карты за каждые 4 единицы мощности электрических карт в инвентаре";

                if (name == "cart fire heart")
                    infa = "поглощает огонь или огненную атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение";
                if (name == "cart poition heart")
                    infa = "поглощает яд или ядовитую атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение";
                if (name == "cart ice heart")
                    infa = "поглощает лёд или ледяную атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение";
                if (name == "cart rock heart")
                    infa = "поглощает камни или каменную атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение";
                if (name == "cart blood heart")
                    infa = "дополнительно поглощает кровь|для восстановления здоровья(в размере двойной мощности эффекта)| |поглощает кровавую атаку для восстановления здоровья(в размере мощности эффекта)| |тратит заряд на одно поглощение";
                if (name == "cart energy heart")
                    infa = "дополнительно поглощает электричество|для восстановления здоровья(в размере двойной мощности эффекта)| |поглощает кровавую атаку для восстановления здоровья(в размере мощности эффекта)| |тратит заряд на одно поглощение";

                if (name == "cart spirit fire heart")
                    infa = "поглощает огонь или огненную атаку для восстановления здоровья|(в размере мощности эффекта)| |+5 к максимальному запасу здоровья";
                if (name == "cart spirit poition heart")
                    infa = "поглощает яд или ядовитую атаку для восстановления здоровья|(в размере мощности эффекта)| |+5 к максимальному запасу здоровья";
                if (name == "cart spirit ice heart")
                    infa = "поглощает лёд или ледяную атаку для восстановления здоровья|(в размере мощности эффекта)| |++5 к максимальному запасу здоровья";
                if (name == "cart spirit rock heart")
                    infa = "поглощает камни или каменную атаку для восстановления здоровья|(в размере мощности эффекта)| |+5 к максимальному запасу здоровья";
                if (name == "cart spirit blood heart")
                    infa = "дополнительно поглощает кровь|для восстановления здоровья(в размере двойной мощности эффекта)| |поглощает кровавую атаку для восстановления здоровья(в размере мощности эффекта)| |+5 к максимальному запасу здоровья";
                if (name == "cart spirit energy heart")
                    infa = "дополнительно поглощает электричество|для восстановления здоровья(в размере двойной мощности эффекта)| |поглощает кровавую атаку для восстановления здоровья(в размере мощности эффекта)| |+5 к максимальному запасу здоровья";

                if (name == "cart speed")
                    infa = "+1 к скорости применения всех карт";
                if (name == "cart power")
                    infa = "+1 к силе применения всех карт";
                if (name == "cart respawn")
                    infa = "воcкрешает при получении смертельного урона| |после воскрешения артефакт уничтожается";


                if (name == "cart shaman boss")
                    infa = "при применении карты-элемента создаёт руну|(1 ход за каждую единицу мощности)| |руна создаёт карту элемента в радиусе 3 от неё|и добавляет в инвентарь создателю|+1 к множителю мощности карты элемента, при применении в радиусе руны|(если природа руны и какой-то карты совпадает)";
                if (name == "cart spirit")
                    infa = "создаёт карту духа и добавляет в инвентарь| |+2 к длительности эффекта духа";

                if (name == "cart spirit(boss)")
                    infa = "позволяет комбинировать вместе с картой-элементом|для создания магического эффекта духа определённой природы|(4 хода за каждую единицу мощности)";


                if (name == "cart berserk boss")
                    infa = "создаёт защиту тела(1 заряд за каждые 5 единиц отсутствующего здоровья)|тратит заряд в размере полученного урона| |+10 к максимальному запасу здоровья(пассивный)| |перезарядка происходит только за счёт полученного урона";
                if (name == "cart rage")
                    infa = "создаёт карту ярости и добавляет в инвентарь";

                if (name == "cart rage(boss)")
                    infa = "даёт эффект ярости на 2 хода:| |+1 к скорости применения всех карт|+1 к силе применения всех карт|+1 к мощности применения всех карт";


                if (name == "cart mini spirit fire")
                    infa = "огненная карта наделяется силой духовного огня при применении";
                if (name == "cart spirit blood")
                    infa = "кровавая карта наделяется силой духовной крови при применении";
                if (name == "cart mini spirit poition")
                    infa = "ядовитая карта наделяется силой духовного яда при применении";
                if (name == "cart mini spirit energy")
                    infa = "электрическая карта наделяется силой духовного электричества при применении";
                if (name == "cart mini spirit ice")
                    infa = "ледяная карта наделяется силой духовного льда при применении";
                if (name == "cart mini spirit rock")
                    infa = "каменная карта наделяется силой духовного камня при применении";

                if (name == "cart sp spirit fire")
                    infa = "создаёт карту духовного огня и добавляет в инвентарь| |+2 к длительности поджога от огненной карты мощности 1";
                if (name == "cart sp spirit blood")
                    infa = "создаёт карту духовной крови и добавляет в инвентарь| |+1 к восстановлению здоровья от кровавой карты мощности 1";
                if (name == "cart sp spirit poition")
                    infa = "создаёт карту духовного яда и добавляет в инвентарь| |+1 к наносимому урону от ядовитой карты мощности 1";
                if (name == "cart sp spirit energy")
                    infa = "создаёт карту духовного электричества и добавляет в инвентарь | |+2 к длительности эффекта от электрической карты мощности 1";
                if (name == "cart sp spirit ice")
                    infa = "создаёт карту духовного льда и добавляет в инвентарь| |+2 к длительности эффекта от ледяной карты мощности 1";
                if (name == "cart sp spirit rock")
                    infa = "создаёт карту духовного камня и добавляет в инвентарь| |+1 к наносимому урону от каменной карты мощности 1";



                if (name == "cart spirit fire")
                    infa = "создаёт пламя духа на 4 хода";
                if (name == "cart spirit blood")
                    infa = "создаёт кровь духа на 4 хода";
                if (name == "cart spirit poition")
                    infa = "создаёт яд духа на 4 хода";
                if (name == "cart spirit energy")
                    infa = "создаёт электричество духа на 4 хода";
                if (name == "cart spirit ice")
                    infa = "создаёт лёд духа на 4 хода";
                if (name == "cart spirit rock")
                    infa = "создаёт камни духа на 4 хода";



                if (name == "cart mini mast fire")
                    infa = "сохраняет огненные карты при применении| |-1 к перезарядке артефакта при применении огненной карты| |+1 дополнительный слот для артефакта";
                if (name == "cart mini mast poition")
                    infa = "сохраняет ядовитые карты при применении| |-1 к перезарядке артефакта при применении ядовитой карты| |+1 дополнительный слот для артефакта";
                if (name == "cart mini mast ice")
                    infa = "сохраняет ледяные карты при применении| |-1 к перезарядке артефакта при применении ледяной карты| |+1 дополнительный слот для артефакта";
                if (name == "cart mini mast energy")
                    infa = "сохраняет электрические карты при применении| |-1 к перезарядке артефакта при применении электрической карты| |+1 дополнительный слот для артефакта";
                if (name == "cart mini mast rock")
                    infa = "сохраняет каменные карты при применении| |-1 к перезарядке артефакта при применении каменной карты| |+1 дополнительный слот для артефакта";
                if (name == "cart mini mast blood")
                    infa = "сохраняет кровавые карты при применении| |-1 к перезарядке артефакта при применении кровавой карты| |+1 дополнительный слот для артефакта";


                if (name == "cart mast fire")
                    infa = "сохраняет огненные карты при применении| |-1 к перезарядке артефакта при применении огненной карты| |+1 к мощности огненной карты(пассивный)| |+1 к скорости применения огненной карты(пассивный)| |+1 к силе применения огненной карты(пассивный)| |+1 дополнительный слот для артефакта(пассивный)";
                if (name == "cart mast poition")
                    infa = "сохраняет ядовитые карты при применении| |-1 к перезарядке артефакта при применении ядовитой карты| |+1 к мощности ядовитой карты(пассивный)| |+1 к скорости применения ядовитой карты(пассивный)| |+1 к силе применения ядовитой карты(пассивный)| |+1 дополнительный слот для артефакта(пассивный)";
                if (name == "cart mast ice")
                    infa = "сохраняет ледяные карты при применении| |-1 к перезарядке артефакта при применении ледяной карты| |+1 к мощности ледяной карты(пассивный)| |+1 к скорости применения ледяной карты(пассивный)| |+1 к силе применения ледяной карты(пассивный)| |+1 дополнительный слот для артефакта(пассивный)";
                if (name == "cart mast energy")
                    infa = "сохраняет электрические карты при применении| |-1 к перезарядке артефакта при применении электрической карты| |+1 к мощности электрической карты(пассивный)| |+1 к скорости применения электрической карты(пассивный)| |+1 к силе применения электрической карты(пассивный)| |+1 дополнительный слот для артефакта(пассивный)";
                if (name == "cart mast rock")
                    infa = "сохраняет каменные карты при применении| |-1 к перезарядке артефакта при применении каменной карты| |+1 к мощности каменной карты(пассивный)| |+1 к скорости применения каменной карты(пассивный)| |+1 к силе применения каменной карты(пассивный)| |+1 дополнительный слот для артефакта(пассивный)";
                if (name == "cart mast blood")
                    infa = "сохраняет кровавых карты при применении| |-1 к перезарядке артефакта при применении кровавой карты| |+1 к мощности кровавой карты(пассивный)| |+1 к скорости применения кровавой карты(пассивный)| |+1 к силе применения кровавой карты(пассивный)| |+1 дополнительный слот для артефакта(пассивный)";

                if (name == "cart derlorg")
                    infa = "карта с проклятой силой| |при применении на живом существе создаёт проклятье на 4 хода|проклятье не позволяет применять карты| |позволяет комбинировать с другими картами для создания определённых проклятий";
                if (name == "cart sp derlorg")
                    infa = "создаёт карту дерлорга мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для карты дерлорга мощности 1(пассивный)| |-2 к максимальному запасу здоровья";

                if (name == "cart dungalorg(sleep)")
                    infa = "сейчас карта спит| |позволяет применять на живом существе для добавления активной версии карты| |не тратит ход при применении";
                if (name == "cart dungalorg")
                    infa = "поглощает 1 мощности применяемых карт| |после поглощения карта уничтожается";

                if (name == "cart dungalorg(sleep) 1")
                    infa = "сейчас карта спит| |позволяет применять на живом существе для добавления активной версии карты| |не тратит ход при применении";
                if (name == "cart dungalorg 1")
                    infa = "поглощает всю мощность применяемых карт| |после поглощения карта уничтожается| |имеет выше приоритет над обычной картой пожирателя";

                if (name == "cart dungalorg heard")
                    infa = "позволяет комбинировать обычную карту пожирателя с картой дерлорга|для получения карты великого пожирателя| |дополнительно поглощает полученный магический эффект в виде карт пожирателя(пассивный)";
                if (name == "cart dungalorg(boss)")
                    infa = "создаёт карту дерлорга и карту пожирателя и добавляет в инвентарь| |при получении активной карты пожирателя получаете спящую карту пожирателя(пассивный)| |поглощает полученное обычное проклятие в виде карты дерлорга(пассивный)";
            }
        }


    }
}
    
