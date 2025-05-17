using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class data : MonoBehaviour
{
    public enum players
    {
        bases,
        atack,
        barier,
        fire,
        rock,
        blood,
        energy,
        ice,
        poition,
        berserk,
        derlorg
    }
    [Serializable]
    public class par_player
    {
        public string name;
        public string infa;
        public List<string> lt_art_st = new List<string>();
        public Dictionary<string,int> lt_carts_st = new Dictionary<string, int>();
        public bool opened = false;
        public players type;
    }
    // Start is called before the first frame update
    public const int version = 11;
    public static string path = "/data.dat";
    public static bool is_open = false;
    public static SaveData data_now;
    [Serializable]
    public class SaveData {
        
        public bool first_game=true;
        public int version_now = version;
        public List<players> lt_players = new List<players>();

        public Dictionary<string, string> lt_achives = new Dictionary<string, string>();
        public List<string> lt_achives_open = new List<string>();

        public List<string> cart_collection = new List<string>();

        public int cl_cart_att = 0;
        public int cl_cart_bar = 0;
        public int cl_cart_rock = 0;
        public int cl_cart_fire = 0;
        public int cl_cart_blood = 0;
        public int cl_cart_energy = 0;
        public int cl_cart_poition = 0;
        public int cl_cart_ice = 0;


        public int cl_mob_dead_derlorg = 0;
        public int cl_mob_dead_now = 0;

        public int cl_carts_speed = 0;

        public int col_carts_now = 0;
        public int col_carts_art = 0;
        public int col_carts_cuzna = 0;

        //public int cl_mob_dead_derlorg = 0;

        public int col_dead=0;

        public SaveData()
        {
            first_game = true;


            lt_achives.Add("умелый","дойдите до 5 этажа и убейте босса");

            //lt_achives.Add("умелый", "дойдите до 4 этажа и убейте босса");

            lt_achives.Add("опытный", "убейте 100 врагов");

            lt_achives.Add("хорошая атака", "Успешно примените 200 карт атаки\n\n(открывает персонажа: боевой маг)");
            lt_achives.Add("хорошая защита", "Успешно примените 100 карт барьера\n\n(открывает персонажа: барьерный маг)");

            lt_achives.Add("сила огня", "Примените успешно 100 огненных карт\n\n(открывает персонажа: огненный маг)");
            lt_achives.Add("сила яда", "Примените успешно 100 ядовитых карт\n\n(открывает персонажа: ядовитый маг)");
            lt_achives.Add("сила льда", "Примените успешно 100 ледяных карт\n\n(открывает персонажа: барьерный маг)");
            lt_achives.Add("сила электричества", "Примените успешно 100 электрических карт\n\n(открывает персонажа: электрический маг)");
            lt_achives.Add("сила камня", "Примените успешно 100 каменных карт\n\n(открывает персонажа: каменный маг)");
            lt_achives.Add("сила крови", "Примените успешно 100 кровавых карт\n\n(открывает персонажа: кровавый маг)");

            lt_achives.Add("Истребитель", "Убейте 50 дерлоргов\n\n(открывает персонажа: маг-дерлорг)");

            lt_achives.Add("живучий", "Получите 30 единиц максимального здоровья\n\n(открывает персонажа: маг-берсерк)");

            lt_achives.Add("Кузнец", "Объедините карты 50 раз\n\n(открывает карту: карта древней кузни)");
            lt_achives.Add("любитель артефактов", "Соберите 50 артефактов");
            lt_achives.Add("постоянный покупатель", "совершите 20 покупок");
            lt_achives.Add("большая колода", "Соберите 75 карт в инвентаре за одну игру\n\n(открывает карту: карта колоды)");

            lt_achives.Add("быстрее некуда", "Примените 4 или более раза карты за один ход\n\n(открывает карту: карта скорости)");

            lt_achives.Add("тяжёлая рука", "используйте 4 или более повторяющихся карт за одну атаку\n\n(открывает карту: карта силы)");

            lt_achives.Add("большой потенциал", "Создайте эффект мощности 16\n\n(открывает карту: карта мощи)");

            //lt_achives.Add("просто надоело", "убейте сами себя");
            lt_achives.Add("не в первый раз", "умрите 20 раз\n\n(открывает карту: карта воскрешения)");

            lt_achives.Add("коллекционер","Получите все виды карт");
            


            lt_players = new List<players>();
            lt_players.Add(players.bases);
            //lt_players.Add(players.atack);
            //lt_players.Add(players.barier);
            //lt_players.Add(players.fire);
            //lt_players.Add(players.rock);
            //lt_players.Add(players.energy);
            //lt_players.Add(players.blood);
            //lt_players.Add(players.poition);
            //lt_players.Add(players.ice);
            //lt_players.Add(players.berserk);
            //lt_players.Add(players.derlorg);
            player_now = players.bases;


            par_player par = new par_player();
            par.name = "маг-новичок";

            par.lt_carts_st.Add("cart damage", 8);
            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_art_st.Add("cart base st");
            par.infa = "маг, который недавно научился пользоваться картами\n \nНе обладает особыми свойствами";
            par.opened = true;
            par.type = players.bases;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "боевой маг";

            par.lt_carts_st.Add("cart damage", 12);
            //par.lt_carts_st.Add("cart barier", 3);
            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart att st");
            par.infa = "+1 к множителю мощности карты атаки\n \n+2 к месту в инвентаре для карты атаки любой мощности";
            par.type = players.atack;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "барьерный маг";

            par.lt_carts_st.Add("cart barier",5);
            par.lt_carts_st.Add("cart damage",6);
            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart bar st");
            par.infa = "+1 к множителю мощности карты защиты\n \n+1 к месту в инвентаре для карты защиты любой мощности";
            par.type = players.barier;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "огненный маг";

            //par.lt_carts_st.Add("cart barier",2);
            par.lt_carts_st.Add("cart damage",6);
            par.lt_carts_st.Add("cart fire",12);
            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart fire st");
            par.infa = "+1 к множителю мощности огненной карты\n \n+2 к месту в инвентаре для огненной карты любой мощности";
            par.type = players.fire;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "каменный маг";

            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_carts_st.Add("cart damage", 6);
            par.lt_carts_st.Add("cart rock",12);

            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart rock st");
            par.infa = "+1 к множителю мощности каменной карты\n \n+2 к месту в инвентаре для каменной карты любой мощности";
            par.type = players.rock;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "кровавый маг";

            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_carts_st.Add("cart damage", 6);
            par.lt_carts_st.Add("cart blood",12);

            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart blood st");
            par.infa = "+1 к множителю мощности кровавой карты\n \n+2 к месту в инвентаре для кровавой карты любой мощности";
            par.type = players.blood;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "электрический маг";

            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_carts_st.Add("cart damage", 6);
            par.lt_carts_st.Add("cart energy",12);

            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart energy st");
            par.infa = "+1 к множителю мощности электрической карты\n \n+2 к месту в инвентаре для электрической карты любой мощности";
            par.type = players.energy;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "ледяной маг";

            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_carts_st.Add("cart damage", 6);
            par.lt_carts_st.Add("cart ice",12);

            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart ice st");
            par.infa = "+1 к множителю мощности ледяной карты\n \n+2 к месту в инвентаре для ледяной карты любой мощности";
            par.type = players.ice;
            lt_par_pl.Add(par);



            par = new par_player();
            par.name = "ядовитый маг";

            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_carts_st.Add("cart damage", 6);
            par.lt_carts_st.Add("cart poition",12);

            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart poition st");
            par.infa = "+1 к множителю мощности ядовитой карты\n \n+2 к месту в инвентаре для ядовитой карты любой мощности";
            par.type = players.poition;
            lt_par_pl.Add(par);


            par = new par_player();
            par.name = "маг-берсерк";

            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_carts_st.Add("cart damage", 6);

            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart berserk st");
            par.infa = "После смерти врага увеличивает мощность применяемых карт на 1\n(длительность эффекта зависит от максимального запаса здоровья врага)\n \n+10 к масимальному запасу здоровья";
            par.type = players.berserk;
            lt_par_pl.Add(par);

            par = new par_player();
            par.name = "маг-дерлорг";

            //par.lt_carts_st.Add("cart barier", 2);
            par.lt_carts_st.Add("cart damage", 8);
            par.lt_carts_st.Add("cart derlorg", 12);

            //par.lt_art_st.Add("cart base st");
            par.lt_art_st.Add("cart derlorg st");
            par.infa = "+1 к мощности применяемых карт за каждые 4 карты дерлорга в инвентаре\n+2 к месту в инвентаре для обычных карт и +1 к месту в инвентаре для специальных карт\n+2 дополнительных слота для артефакта\n-1 к дальности атаки\n-5 к масимальному запасу здоровья";
            par.type = players.derlorg;
            lt_par_pl.Add(par);
        }

        public players player_now
        {
            get
            {
                return player_now_pr;
            }
            set
            {
                //Debug.Log("set "+value.ToString());
                if(value== players.bases)
                {
                    pl_name_now = "base player";
                }
                else if(value == players.atack)
                {
                    pl_name_now = "atack player";
                }
                else if (value == players.barier)
                {
                    pl_name_now = "barier player";
                }
                else if (value == players.blood)
                {
                    pl_name_now = "blood player";
                }
                else if (value == players.fire)
                {
                    pl_name_now = "fire player";
                }
                else if (value == players.rock)
                {
                    pl_name_now = "rock player";
                }
                else if (value == players.energy)
                {
                    pl_name_now = "energy player";
                }
                else if (value == players.ice)
                {
                    pl_name_now = "ice player";
                }
                else if (value == players.poition)
                {
                    pl_name_now = "poition player";
                }
                else if (value == players.berserk)
                {
                    pl_name_now = "berserk player";
                }
                else if (value == players.derlorg)
                {
                    pl_name_now = "derlorg player";
                }
                player_now_pr = value;
            }
        }
        private players player_now_pr = players.bases;
        public string pl_name_now = "base player";

        public List<par_player> lt_par_pl = new List<par_player>();
    }
    public static void give_achive(string name)
    {
        if (!data_now.lt_achives_open.Contains(name))
        {
            data_now.lt_achives_open.Add(name);
            GameObject gm = Instantiate(GameObject.Find("panels").transform.Find("ignoring").Find("im ach").gameObject, GameObject.Find("pn achives").transform);
            gm.transform.Find("achive").Find("ram name").Find("tx name").GetComponent<Text>().text = name;
            gm.transform.Find("achive").Find("ram infa").Find("tx infa").GetComponent<Text>().text = data_now.lt_achives[name];
            gm.active = true;
            save_data();
        }

    }
    public static void load_data()
    {
        //
        
        if (File.Exists(Application.persistentDataPath + path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream st = File.Open(Application.persistentDataPath + path,FileMode.Open);
            data_now = (SaveData)bf.Deserialize(st);
            st.Close();
            if (data_now.version_now != version)
            {
                SaveData data_now1 = new SaveData();
                foreach (string str in data_now1.lt_achives.Keys)
                {
                    if (!data_now.lt_achives.ContainsKey(str))
                        data_now.lt_achives.Add(str, data_now1.lt_achives[str]);
                }
                foreach (par_player pl1 in data_now1.lt_par_pl)
                {
                    bool bl = true;
                    foreach (par_player pl2 in data_now.lt_par_pl)
                    {
                        if (pl1.type == pl2.type)
                            bl = false;
                    }
                    if (bl)
                        data_now.lt_par_pl.Add(pl1);
                }
                save_data();
            }
            else
            {
                //data_now.col_carts_now = 0;
                //data_now.cl_mob_dead_derlorg = 0;
                //data_now.cl_carts_speed = 0;
            }

        }
        else
        {
            data_now = new SaveData();
            save_data();
        }
        //File.Delete(Application.persistentDataPath + path);
    }
    public static void save_data()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream st = File.Create(Application.persistentDataPath + path);
        bf.Serialize(st, data_now);
        st.Close();
    }
    void Start()
    {
        add.load_add();
        //load_data();
        //PlayerPrefs.DeleteAll();
        //Debug.Log
        
        if (!data_now.first_game)
        {
            GameObject.Find("Canvas obych").active = false;
            //GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
            //GameObject.Find("tx lvl").GetComponent<Animator>().enabled = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Canvas obych")==null)
        {
            //if(!GameObject.Find("Main Camera").GetComponent<Animator>().enabled)
                
            //GameObject.Find("tx lvl").GetComponent<Animator>().enabled = true;

        }

        if (data_now.first_game && GameObject.Find("pn6")!=null)
        {
            data_now.first_game = false;


            save_data();
        }
    }
}
