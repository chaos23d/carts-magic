using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class par_cart : MonoBehaviour
{
    public static class cart_combinations
    {
        public class comb_cart 
        {
            public string name;
            public comb_cart first;
            public Dictionary <string,comb_cart> next = new Dictionary<string, comb_cart>();
            public int position;
        }

        public static void add(string st)
        {
            List<comb_cart> lt_c = get_comb(st);
            lt_comb.AddRange(lt_c);
            //List<List<string>> l_l_s = get_comb(lt_c);
            //lt_comb_map.Add(st, l_l_s);
        }
        public static List<comb_cart> get_comb(string st)
        {
            List<comb_cart> lt = new List<comb_cart>();

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
                    for (int i = 0; i < 3; i++)
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
                        lt.Add(c_cart);
                        c_cart.first = c_cart;
                        cart_now_add = start;

                    }
                    else
                    {
                        cart_now_add.Add(c_cart);
                        foreach (comb_cart ct in cart_now)
                        {
                            c_cart.first = ct.first;
                            ct.next.Add(cart_dop, c_cart);
                        }
                        
                    }
                }
                cart_now = cart_now_add;

                i_pos++;
            }
            return lt;
        }
        public static void print(List<comb_cart> lt_now)
        {
            foreach (comb_cart cart in lt_now)
            {
                Debug.Log("");
                Debug.Log("");
                //Debug.Log(cart.name);
                print(cart,new List<string>() );
            }
        }
        public static void print(comb_cart cart_now, List<string> lt)
        {
            List<string> lt1 = new List<string>(lt);
            lt1.Add(cart_now.name);
            if (cart_now.next.Count > 0)
            {
                foreach (comb_cart cart_1 in cart_now.next.Values)
                {
                    //Debug.Log(cart_1.name);

                    print(cart_1, lt1);
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
        public static bool equal(List<string> lt,string st)
        {
           

            List<comb_cart> lt_comb = get_comb(st);
            List<List<string>> lt_comb_st = get_comb(lt_comb);

            foreach (List<string> lt1 in lt_comb_st)
            {
                bool bl = false;
                if (lt.Count==lt1.Count)
                {
                    for (int i = 0;i< lt1.Count; i++)
                    {
                        bl = lt[i].Equals(lt1[i]);
                    }
                }
                if (bl)
                    return true;
            }
            return false;
        }
        public static List<List<string>> get_comb_now(string st)
        {
            return get_comb(get_comb(st));
        }
        public static List<List<string>> get_comb(List<comb_cart> lt)
        {
            List < List<string> > lt_rez = new List<List<string>>();
            foreach (comb_cart cart in lt)
            {
                get_comb(cart, lt_rez,new List<string>());
                //Debug.Log(l.Count+" in comb");
                //lt_rezlt_rez.Add(l);
            }
            return lt_rez;
        }
        public static void get_comb(comb_cart cart_now, List<List<string>> lt, List<string> lt_)
        {
            List<string> lt1 = new List<string>(lt_);
            lt1.Add(cart_now.name);
            //Debug.Log(cart_now.name + " in comb now");
            if (cart_now.next.Count > 0)
            {
                List<string> lt2 = new List<string>();
                foreach (comb_cart cart_1 in cart_now.next.Values)
                {
                    //Debug.Log(cart_1.name);

                   get_comb(cart_1, lt,lt1);
                }
                //return null;
            }

            else
            {
                lt.Add(lt1);
            }

        }
        /*
        public static List<string> get_comb(comb_cart cart_now, List<string> lt)
        {
            List<string> lt1 = new List<string>(lt);
            lt1.Add(cart_now.name);
            Debug.Log(cart_now.name + " in comb now");
            if (cart_now.next.Count > 0)
            {
                List<string> lt2 = new List<string>();
                foreach (comb_cart cart_1 in cart_now.next.Values)
                {
                    //Debug.Log(cart_1.name);

                    return get_comb(cart_1, lt1);
                }
                return null;
            }

            else
            {
                return lt1;
            }

        }
        */

        public static void collections()
        {
            string command;

            List<string> lt_comm= new List<string>();
            lt_comm.Add("cart damage 0");
            lt_comm.Add("cart damage[n]");
            lt_comm.Add("cart barier[n]");
            lt_comm.Add("cart [e][n]");
            lt_comm.Add("cart [e_d]");

            lt_comm.Add("cart damage[n]-cart [e][n]");
            lt_comm.Add("cart barier[n]-cart damage[n]");
            lt_comm.Add("cart barier[n]-cart damage[n]-cart [e][n]");

            //lt_comm.Add("cart damage[n]-cart [e_d]");
            //lt_comm.Add("cart barier[n]-cart [e_d]");

            lt_comm.Add("cart derlorg[n]");

            lt_comm.Add("cart rage(boss)");
            List<string> lt_comm1 = new List<string>();
            foreach (string st in lt_comm)
            {
                lt_comm1.Add("cart bomb[n]-"+st);

                lt_comm1.Add("cart dych[n]-" + st);

                //lt_comm1.Add("cart bomb[n]-cart dych[n]-" + st);
                //lt_comm1.Add("cart dych[n]-cart bomb[n]-" + st);
                if (st!= "cart derlorg[n]")
                {
                    lt_comm1.Add("cart derlorg[n]-" + st);
                    lt_comm1.Add("cart derlorg[n]-cart bomb[n]-" + st);

                    lt_comm1.Add("cart bomb[n]-cart derlorg[n]-" + st);
                    lt_comm1.Add("cart dych[n]-cart derlorg[n]-" + st);

                    //lt_comm1.Add("cart derlorg[n]-cart bomb[n]-cart dych[n]-" + st);

                    //lt_comm1.Add("cart dych[n]-cart derlorg[n]-cart bomb[n]-" + st);
                    //lt_comm1.Add("cart dych[n]-cart bomb[n]-cart derlorg[n]-" + st);
                    //lt_comm1.Add("cart derlorg[n]-cart dych[n]-cart bomb-" + st);

                    //lt_comm1.Add("cart bomb[n]-cart dych[n]-cart derlorg[n]-" + st);
                }


                lt_comm1.Add("cart spirit(boss)-" + st);
                lt_comm1.Add("cart spirit(boss)-cart bomb[n]-" + st);

                lt_comm1.Add("cart spirit(boss)-cart dych[n]-" + st);
                //lt_comm1.Add("cart spirit(boss)-cart bomb[n]-cart dych[n]" + st);
                //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart bomb[n]" + st);
                if (st != "cart derlorg[n]")
                {
                    lt_comm1.Add("cart spirit(boss)-cart derlorg[n]-" + st);
                    lt_comm1.Add("cart spirit(boss)-cart derlorg[n]-cart bomb[n]-" + st);

                    lt_comm1.Add("cart spirit(boss)-cart bomb[n]-cart derlorg[n]-" + st);
                    lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart derlorg[n]-" + st);

                    //lt_comm1.Add("cart spirit(boss)-cart derlorg[n]-cart bomb[n]-cart dych[n]-" + st);

                    //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart derlorg[n]-cart bomb[n]-" + st);
                    //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart bomb[n]-cart derlorg[n]-" + st);
                    //lt_comm1.Add("cart derlorg[n]-cart dych[n]-cart bomb-" + st);

                    //lt_comm1.Add("cart spirit(boss)-cart bomb[n]-cart dych[n]-cart derlorg[n]-" + st);
                }

                lt_comm1.Add("cart bomb[n]-cart spirit(boss)-" + st);
                //lt_comm1.Add("cart bomb[n]-cart spirit(boss)-cart dych[n]-" + st);
                if (st != "cart derlorg[n]")
                {
                    lt_comm1.Add("cart derlorg[n]-cart spirit(boss)-" + st);
                    lt_comm1.Add("cart derlorg[n]-cart spirit(boss)-cart bomb[n]-" + st);

                    lt_comm1.Add("cart bomb[n]-cart spirit(boss)-cart derlorg[n]-" + st);
                    //lt_comm1.Add("cart dych[n]-cart spirit(boss)-cart derlorg[n]-" + st);

                    lt_comm1.Add("cart derlorg[n]-cart spirit(boss)-cart bomb[n]-cart dych[n]-" + st);

                    //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart derlorg[n]-cart bomb[n]-" + st);
                    //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart bomb[n]-cart derlorg[n]-" + st);
                    //lt_comm1.Add("cart derlorg[n]-cart dych[n]-cart bomb-" + st);

                    //lt_comm1.Add("cart bomb[n]-cart spirit(boss)-cart dych[n]-cart derlorg[n]-" + st);
                }

                if (st != "cart derlorg[n]")
                {
                    //lt_comm1.Add("cart spirit(boss)-cart derlorg[n]-" + st);
                    lt_comm1.Add("cart derlorg[n]-cart bomb[n]-cart spirit(boss)-" + st);

                    lt_comm1.Add("cart bomb[n]-cart derlorg[n]-cart spirit(boss)-" + st);
                    //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart derlorg[n]-" + st);

                    lt_comm1.Add("cart derlorg[n]-cart bomb[n]-cart spirit(boss)-cart dych[n]-" + st);

                    //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart derlorg[n]-cart bomb[n]-" + st);
                    //lt_comm1.Add("cart spirit(boss)-cart dych[n]-cart bomb[n]-cart derlorg[n]-" + st);
                    //lt_comm1.Add("cart derlorg[n]-cart dych[n]-cart bomb-" + st);

                    //lt_comm1.Add("cart spirit(boss)-cart bomb[n]-cart dych[n]-cart derlorg[n]-" + st);
                }
            }

            
            foreach (string st in lt_comm)
            {
                add(st);
            }
            foreach (string st in lt_comm1)
            {
                add(st);
            }
            //lt_comm.Add("cart crystall[n]-cart [e][n]");
            /*
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
            add(command);
            command = "cart damage[n]-cart [e_d]";
            add(command);
            command = "cart barier[n]-cart [e][n]";
            add(command);
            command = "cart spirit(boss)-cart [e][n]";
            add(command);

            command = "cart derlorg[n]-cart [e][n]";
            add(command);
            command = "cart derlorg[n]-cart [e_d]";
            add(command);
            command = "cart derlorg[n]-cart damage[n]";
            add(command);
            command = "cart derlorg[n]-cart barier[n]";
            add(command);
            command = "cart derlorg[n]-cart damage[n]-cart [e][n]";
            add(command);
            command = "cart derlorg[n]-cart barier[n]-cart [e][n]";
            add(command);


            command = "cart derlorg[n]-cart crystall[n]-cart damage[n]";
            add(command);
            command = "cart crystall[n]-cart derlorg[n]-cart damage[n]";
            add(command);
            command = "cart derlorg[n]-cart crystall[n]-cart barier[n]";
            add(command);
            command = "cart crystall[n]-cart derlorg[n]-cart barier[n]";
            add(command);
            command = "cart crystall[n]-cart derlorg[n]-cart [e][n]";
            add(command);
            command = "cart crystall[n]-cart derlorg[n]-cart [e_d]";
            add(command);

            command = "cart dungalorg(sleep)-cart derlorg[n]";
            add(command);

            command = "cart [e_d]";
            add(command);
            */
            for (int j = 0; j < 2; j++)
            {
                string st_dop_cuz = "";
                if (j != 0)
                    st_dop_cuz = " " + j.ToString();

                for (int i = 0; i < j+1; i++)
                {
                    string st_dop = "";
                    if (i != 0)
                        st_dop = " " + i.ToString();
                    command = "cart cuznec" + st_dop_cuz + "-cart damage" + st_dop + "-cart damage" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart barier" + st_dop + "-cart barier" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart dych" + st_dop + "-cart dych" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart bomb" + st_dop + "-cart bomb" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart poition" + st_dop + "-cart poition" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart fire" + st_dop + "-cart fire" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart blood" + st_dop + "-cart blood" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart rock" + st_dop + "-cart rock" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart energy" + st_dop + "-cart energy" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart ice" + st_dop + "-cart ice" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart bomb" + st_dop + "-cart bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart derlorg" + st_dop + "-cart derlorg" + st_dop;
                    add(command);

                    if (i < 1)
                    {
                        command = "cart cuznec" + st_dop + "-cart cuznec" + st_dop + "-cart cuznec" + st_dop;
                        add(command);
                    }



                    command = "cart cuznec" + st_dop_cuz + "-cart att pow" + st_dop + "-cart att pow" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart bar pow" + st_dop + "-cart bar pow" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart dych pow" + st_dop + "-cart dych pow" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart bomb pow" + st_dop + "-cart bomb pow" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart poition pow" + st_dop + "-cart poition pow" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart fire pow" + st_dop + "-cart fire pow" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart blood pow" + st_dop + "-cart blood pow" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart rock pow" + st_dop + "-cart rock pow" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart energy pow" + st_dop + "-cart energy pow" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart ice pow" + st_dop + "-cart ice pow" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart sp atack" + st_dop + "-cart sp atack" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart sp barier" + st_dop + "-cart sp barier" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp bomb" + st_dop + "-cart sp bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp dych" + st_dop + "-cart sp dych" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart sp poition" + st_dop + "-cart sp poition" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp fire" + st_dop + "-cart sp fire" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp blood" + st_dop + "-cart sp blood" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp rock" + st_dop + "-cart sp rock" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp energy" + st_dop + "-cart sp energy" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp ice" + st_dop + "-cart sp ice" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp bomb" + st_dop + "-cart sp bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart sp derlord" + st_dop + "-cart sp derlord" + st_dop;
                    add(command);


                    //command = "cart cuznec" + st_dop + "-cart sp atack" + st_dop + "-cart sp atack" + st_dop;
                    //add(command);
                    //command = "cart cuznec" + st_dop + "-cart sp barier" + st_dop + "-cart sp barier" + st_dop;
                    //add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act att" + st_dop + "-cart act att" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart act bar" + st_dop + "-cart act bar" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act dych" + st_dop + "-cart act dych" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act bomb" + st_dop + "-cart act bomb" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart act poition" + st_dop + "-cart act poition" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act fire" + st_dop + "-cart act fire" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act blood" + st_dop + "-cart act blood" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act rock" + st_dop + "-cart act rock" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act energy" + st_dop + "-cart act energy" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart act ice" + st_dop + "-cart act ice" + st_dop;
                    add(command);
                    /*
                    command = "cart cuznec" + st_dop + "-cart act crystall" + st_dop + "-cart act crystall" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop + "-cart act derlord" + st_dop + "-cart act derlord" + st_dop;
                    add(command);
                    */
                    command = "cart cuznec" + st_dop_cuz + "-cart speed att" + st_dop + "-cart speed att" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart speed bar" + st_dop + "-cart speed bar" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart speed dych" + st_dop + "-cart speed dych" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart speed bomb" + st_dop + "-cart speed bomb" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart speed poition" + st_dop + "-cart speed poition" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart speed fire" + st_dop + "-cart speed fire" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart speed blood" + st_dop + "-cart speed blood" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart speed rock" + st_dop + "-cart speed rock" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart speed energy" + st_dop + "-cart speed energy" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart speed ice" + st_dop + "-cart speed ice" + st_dop;
                    add(command);


                    command = "cart cuznec" + st_dop_cuz + "-cart att cuzna" + st_dop + "-cart att cuzna" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart bar cuzna" + st_dop + "-cart bar cuzna" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart dych cuzna" + st_dop + "-cart dych cuzna" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart bomb cuzna" + st_dop + "-cart bomb cuzna" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart poition cuzna" + st_dop + "-cart poition cuzna" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart fire cuzna" + st_dop + "-cart fire cuzna" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart blood cuzna" + st_dop + "-cart blood cuzna" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart rock cuzna" + st_dop + "-cart rock cuzna" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart energy cuzna" + st_dop + "-cart energy cuzna" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart ice cuzna" + st_dop + "-cart ice cuzna" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart att bomb" + st_dop + "-cart att bomb" + st_dop;
                    add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart bar bomb" + st_dop + "-cart bar bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart dych bomb" + st_dop + "-cart dych bomb" + st_dop;
                    add(command);
                    //command = "cart cuznec" + st_dop + "-cart bomb bomb" + st_dop + "-cart bomb bomb" + st_dop;
                    //add(command);

                    command = "cart cuznec" + st_dop_cuz + "-cart poition bomb" + st_dop + "-cart poition bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart fire bomb" + st_dop + "-cart fire bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart blood bomb" + st_dop + "-cart blood bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart rock bomb" + st_dop + "-cart rock bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart energy bomb" + st_dop + "-cart energy bomb" + st_dop;
                    add(command);
                    command = "cart cuznec" + st_dop_cuz + "-cart ice bomb" + st_dop + "-cart ice bomb" + st_dop;
                    add(command);




                    //command = "cart cuznec" + st_dop + "-cart bomb bomb" + st_dop + "-cart bomb bomb" + st_dop;
                    //add(command);
                    if (i == 1)
                    {
                        command = "cart cuznec" + st_dop_cuz + "-cart mini mast poition" + st_dop + "-cart mini mast poition" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini mast fire" + st_dop + "-cart mini mast fire" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini mast blood" + st_dop + "-cart mini mast blood" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini mast rock" + st_dop + "-cart mini mast rock" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini mast energy" + st_dop + "-cart mini mast energy" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini mast ice" + st_dop + "-cart mini mast ice" + st_dop;
                        add(command);



                        command = "cart cuznec" + st_dop_cuz + "-cart mini spirit poition" + st_dop + "-cart mini spirit poition" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini spirit fire" + st_dop + "-cart mini spirit fire" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini spirit blood" + st_dop + "-cart mini spirit blood" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini spirit rock" + st_dop + "-cart mini spirit rock" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini spirit energy" + st_dop + "-cart mini spirit energy" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart mini spirit ice" + st_dop + "-cart mini spirit ice" + st_dop;
                        add(command);


                        command = "cart cuznec" + st_dop_cuz + "-cart poition heart" + st_dop + "-cart poition heart" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart fire heart" + st_dop + "-cart fire heart" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart blood heart" + st_dop + "-cart blood heart" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart rock heart" + st_dop + "-cart rock heart" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart energy heart" + st_dop + "-cart energy heart" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart ice heart" + st_dop + "-cart ice heart" + st_dop;
                        add(command);

                        command = "cart cuznec" + st_dop_cuz + "-cart elemental" + st_dop + "-cart elemental" + st_dop;
                        add(command);
                        command = "cart cuznec" + st_dop_cuz + "-cart universal" + st_dop + "-cart universal" + st_dop;
                        add(command);
                    }

                }
            }
            

            
            //print();
        }
        public static List<comb_cart> lt_comb = new List<comb_cart>();//кароч. Это содержит стартовую точку карт
        public static Dictionary<string, List<List<string>>> lt_comb_map = new Dictionary<string, List<List<string>>>();
        public static Dictionary<par_mob.priority_atack.type_atack, List<string>> lt_comb_type_atack = new Dictionary<par_mob.priority_atack.type_atack, List<string>>();
        //public static Dictionary<par_mob.priority_atack.type_atack,List<List>>
        //public static Dictionary<comb_cart,string> lt_comb_if;
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
        //sp_du,
        //sp_comb,
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
        art_att_pow,
        art_speed_att,
        art_att_act,
        art_att_cuzna,
        art_att_bomb,

        art_bar_sp,
        art_bar_pow,
        art_speed_bar,
        art_bar_act,
        art_bar_cuzna,
        art_bar_bomb,

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
        sp_bomb,

        art_pow_blood,
        art_pow_fire,
        art_pow_energy,
        art_pow_poition,
        art_pow_rock,
        art_pow_ice,

        art_bomb_sp,
        art_pow_bomb,
        art_speed_bomb,
        art_bomb_act,
        art_bomb_cuzna,
        art_bomb_bomb,

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


        sp_dych,

        art_dych_sp,
        art_dych_pow,
        art_speed_dych,
        art_dych_act,
        art_dych_bomb,
        art_dych_cuzna,

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

        art_fire_bomb,
        art_poition_bomb,
        art_ice_bomb,
        art_energy_bomb,
        art_blood_bomb,
        art_rock_bomb,

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

        art_att_mast,

        art_bar_mast,
        art_bomb_mast,
        art_dych_mast,

        art_sila,

        art_mini_spirit,
        art_mini_shaman,

        art_brilliant,


        art_seal_sila,
        art_seal_speed,
        art_seal_pow,
        art_seal_potoc,
        art_seal_att,
        art_seal_bar,
        art_seal_bomb,
        art_seal_fire,
        art_seal_poition,
        art_seal_ice,
        art_seal_energy,
        art_seal_rock,
        art_seal_blood,
    }

    public static List<type_cart> get_types_tip()
    {
        List<type_cart> lt = new List<type_cart>();
        lt.Add(type_cart.damage);
        lt.Add(type_cart.sp_barier);
        lt.Add(type_cart.sp_bomb);
        lt.Add(type_cart.sp_dych);

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

        //lt.Add(type_cart.sp_dych);
        //lt.Add(type_cart.poition_spider);


        lt.Add(type_cart.shaman_boss);
        lt.Add(type_cart.rage_boss);

        return lt;
    }
    public bool art_cart_bl()
    {

        return (this is par_cart_art);
    }
    public void Start()
    {
        if(SceneManager.GetActiveScene().name == "Scene")
        {
            //Debug.Log("START " + gameObject.name);
            
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
            if (gameObject.name == "cart brilliant" && data.data_now.lt_achives_open.Contains("постоянный покупатель"))
                open = true;

            func func = GameObject.Find("func").GetComponent<func>();
            if (!func.baza.ContainsKey(gameObject.name))
            {
                //Debug.Log(gameObject.name);
                func.baza.Add(gameObject.name, gameObject);
                name = gameObject.name;
                #region typ

                if (name == "cart damage 0")
                {
                    infa = "наносит 1 урона живому существу| |можно использовать для уничтожения препятствий";
                    type = type_cart.damage;
                }

                if (name == "cart damage")
                {
                    infa = "наносит 2 урона живому существу| |можно использовать для уничтожения препятствий| |позволяет комбинировать с картой-элементом для усиления атаки| |карта может быть улучшена";
                    type = type_cart.damage;
                }
                if (name == "cart damage 1")
                {
                    infa = "наносит 4 урона живому существу| |можно использовать для уничтожения препятствий| |позволяет комбинировать с картой-элементом для усиления атаки| |карта может быть улучшена";
                    type= type_cart.damage;
                }
                if (name == "cart damage 2")
                {
                    infa = "наносит 8 урона живому существу| |можно использовать для уничтожения препятствий| |позволяет комбинировать с картой-элементом для усиления атаки";
                    type=type_cart.damage;
                }
                if (name == "cart barier")
                {
                    infa = "создаёт барьер, защищающий от 2 урона карты атаки| |позволяет комбинировать с картой-элементом для усиления защиты| |карта может быть улучшена";
                    type = type_cart.sp_barier;
                }
                if (name == "cart barier 1")
                {
                    infa = "создаёт барьер, защищающий от 4 урона карты атаки| |позволяет комбинировать с картой-элементом для усиления защиты| |карта может быть улучшена";
                    type = type_cart.sp_barier;
                }
                if (name == "cart barier 2")
                {
                    infa = "создаёт барьер, защищающий от 8 урона карты атаки| |позволяет комбинировать с картой-элементом для усиления защиты";
                    type = type_cart.sp_barier;
                }

                if (name == "cart fire")
                {
                    infa = "карта элемента огня| |поджигает живое существо на 2 хода| |карта может быть улучшена";
                    type = type_cart.fire;
                }
                if (name == "cart fire 1")
                {
                    infa = "карта элемента огня| |поджигает живое существо 2 раза на 2 хода| |карта может быть улучшена";
                    type = type_cart.fire;
                }
                if (name == "cart fire 2")
                {
                    infa = "карта элемента огня| |поджигает живое существо 4 раза на 2 хода";
                    type = type_cart.fire;
                }
                if (name == "cart poition")
                {
                    infa = "карта элемента яда| |отравляет живое существо на 3 хода| |карта может быть улучшена";
                    type = type_cart.poition;
                }
                if (name == "cart poition 1")
                {
                    infa = "карта элемента яда| |отравляет живое существо на 6 ходов| |карта может быть улучшена";
                    type = type_cart.poition;
                }
                if (name == "cart poition 2")
                {
                    infa = "карта элемента яда| |отравляет живое существо на 12 ходов";
                    type= type_cart.poition;
                }
                if (name == "cart ice")
                {
                    infa = "карта элемента льда| |замедляет живое существо на 2 хода| |карта может быть улучшена";
                    type = type_cart.ice;
                }
                if (name == "cart ice 1")
                {
                    infa = "карта элемента льда| |замедляет живое существо на 4 хода| |карта может быть улучшена";
                    type=type_cart.ice;
                }
                if (name == "cart ice 2")
                {
                    infa = "карта элемента льда| |замедляет живое существо на 8 ходов";
                    type = type_cart.ice;
                }
                if (name == "cart rock")
                {
                    infa = "карта элемента земли| |наносит 1 урон| |карта может быть улучшена";
                    type = type_cart.rock;
                }
                if (name == "cart rock 1")
                {
                    infa = "карта элемента земли| |наносит 2 урона| |карта может быть улучшена";
                    type = type_cart.rock;
                }
                    
                if (name == "cart rock 2")
                {
                    infa = "карта элемента земли| |наносит 4 урона";
                    type = type_cart.rock;
                }
                    
                if (name == "cart energy")
                {
                    infa = "карта элемента электричества| |ускоряет живое существо на 2 хода| |карта может быть улучшена";
                    type = type_cart.energy;
                }
                   
                if (name == "cart energy 1")
                {
                    infa = "карта элемента электричества| |ускоряет живое существо на 4 хода| |карта может быть улучшена";
                    type = type_cart.energy;
                }
                    
                if (name == "cart energy 2")
                {
                    infa = "карта элемента электричества| |ускоряет живое существо на 8 хода";
                    type = type_cart.energy;
                }
                if (name == "cart blood")
                {
                    infa = "карта элемента крови| |исцеляет живое существо на 1| |карта может быть улучшена";
                    type = type_cart.blood;
                }
                if (name == "cart blood 1")
                {
                    infa = "карта элемента крови| |исцеляет живое существо на 2| |карта может быть улучшена";
                    type = type_cart.blood;
                }
                if (name == "cart blood 2")
                {
                    infa = "карта элемента крови| |исцеляет живое существо на 4";
                    type = type_cart.blood;
                }

                if (name == "cart bomb")
                {
                    infa = "позволяет комбинировать с картами для создания магической бомбы мощности 1| |бомба взрывается через ход, создавая магический эффект от карт| с удвоенной мощностью в центре и обычной мощностью в радиусе 1| |мощность бомбы усиливается при использовании более мощных карт| |карта может быть улучшена";
                    type= type_cart.sp_bomb;
                }
                if (name == "cart bomb 1")
                {
                    infa = "позволяет комбинировать с картами для создания магической бомбы мощности 2| |бомба взрываются через ход, создавая магический эффект от карт| с удвоенной мощностью в центре и обычной мощностью в радиусе 1| |мощность бомбы усиливается при использовании более мощных карт| |карта может быть улучшена";
                    type = type_cart.sp_bomb;
                }
                if (name == "cart bomb 2")
                {
                    infa = "позволяет комбинировать с картами для создания магической бомбы мощности 4| |бомба взрываются через ход, создавая магический эффект от карт| с удвоенной мощностью в центре и обычной мощностью в радиусе 1| |мощность бомбы усиливается при использовании более мощных карт";
                    type=type_cart.sp_bomb;
                }

                if (name == "cart dych")
                {
                    infa = "позволяет комбинировать с картами для призыва союзного духа|на 20 ходов со здоровьем 2| на свободной клетке с силой применённых карт| |карта может быть улучшена";
                    type = type_cart.sp_dych;
                }
                if (name == "cart dych 1")
                {
                    infa = "позволяет комбинировать с картами для призыва союзного духа|на 20 ходов со здоровьем 4| на свободной клетке с силой применённых карт| |карта может быть улучшена";
                    type = type_cart.sp_dych;

                }
                if (name == "cart dych 2")
                {
                    infa = "позволяет комбинировать с картами для призыва союзного духа|на 20 ходов со здоровьем 8| на свободной клетке с силой применённых карт| |карта может быть улучшена";
                    type = type_cart.sp_dych;
                }


                if (name == "cart gold")
                {
                    infa = "карта, состоящая из золота| |не обладает особыми свойствами, но хорошо ценится как местная валюта";
                    type = type_cart.gold;
                }

                if (name == "cart cuznec")
                {
                    infa = "позволяет комбинировать с 2 одинаковыми картами мощности 1| для объединения в более мощную карту(касается всех видов карт)| |карта может быть улучшена";
                    type = type_cart.sp_cuznec;
                }
                if (name == "cart cuznec 1")
                {
                    infa = "позволяет комбинировать с 2 одинаковыми картами мощности 1 или 2| для объединения в более мощную карту(касается всех видов карт)";
                    type = type_cart.sp_cuznec;
                }


                #endregion


                #region art_st
                if (name == "cart base st")
                {
                    infa = "создаёт карту атаки мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для карты атаки мощности 1(пассивный)";
                    type = type_cart.art_base_st;
                }
                if (name == "cart att st")
                {
                    infa = "создаёт 2 карты атаки мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для карты атаки мощности 1(пассивный)| |применение карты атаки уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                    type= type_cart.art_att_st;
                }
                if (name == "cart bar st")
                {
                    infa = "создаёт карту атаки и карту защиты мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для карты защиты мощности 1(пассивный)| |применение карты защиты уменьшает перезарядку всех артефактов на 2(но не менее 1)(пассивный)";
                    type = type_cart.art_bar_st;
                }
                if (name == "cart fire st")
                {
                    infa = "создаёт огненную карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для огненной карты мощности 1(пассивный)| |применение огненной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                    type=type_cart.art_fire_st;
                }
                if (name == "cart poition st")
                {
                    infa = "создаёт ядовитую карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для ядовитой карты мощности 1(пассивный)| |применение ядовитой карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                    type = type_cart.art_poition_st;
                }
                if (name == "cart rock st")
                {
                    infa = "создаёт каменную карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для каменной карты мощности 1(пассивный)| |применение каменной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                    type = type_cart.art_rock_st;
                }
                if (name == "cart blood st")
                {
                    infa = "создаёт кровавую карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для кровавой карты мощности 1(пассивный)| |применение кровавой карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                    type = type_cart.art_blood_st;

                }
                if (name == "cart ice st")
                {
                    infa = "создаёт ледяную карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для ледяной карты мощности 1(пассивный)| |применение ледяной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                    type = type_cart.art_ice_st;

                }
                if (name == "cart energy st")
                {
                    infa = "создаёт электрическую карту мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для электрической карты мощности 1(пассивный)| |применение электрической карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)";
                    type = type_cart.art_energy_st;

                }
                if (name == "cart berserk st")
                {
                    infa = "создаёт карту атаки мощности 1 и добавляет в инвентарь| |+1 к мощности применения карты за каждые 5 единиц отсутствующего здоровья(пассивный)";
                    type = type_cart.art_berserk_st;
                }
                if (name == "cart derlorg st")
                {
                    infa = "создаёт карту дерлорга и карту атаки мощности 1 и добавляет в инвентарь| |+4 к месту в инвентаре для карты дерлорга мощности 1(пассивный)| |применение обычной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)| |применение специальной карты уменьшает перезарядку всех артефактов на 2(но не менее 1)(пассивный)";
                    type = type_cart.art_derlorg_st;

                }

                #endregion

                #region art_att
                if (name == "cart sp atack")
                {
                    infa = "создаёт карту атаки мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_sp;
                }
                if (name == "cart sp atack 1")
                {
                    infa = "создаёт карту атаки мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для карты атаки мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_sp;
                }
                if (name == "cart sp atack 2")
                {
                    infa = "создаёт карту атаки мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для карты атаки любой мощности(пассивный)";
                    type = type_cart.art_att_sp;
                }


                if (name == "cart att pow")
                {
                    infa = "+1 к мощности карты атаки| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_pow;

                }
                if (name == "cart att pow 1")
                {
                    infa = "+2 к мощности карты атаки| |+2 к месту в инвентаре для карты атаки мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_pow;

                }
                if (name == "cart att pow 2")
                {
                    infa = "+4 к мощности карты атаки| |+2 к месту в инвентаре для карты атаки любой мощности(пассивный)";
                    type = type_cart.art_att_pow;
                }


                if (name == "cart act att")
                {
                    infa = "+1 к силе применения карты атаки| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_act;
                }
                if (name == "cart act att 1")
                {
                    infa = "+2 к силе применения карты атаки| |+2 к месту в инвентаре для карты атаки мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_act;
                }
                if (name == "cart act att 2")
                {
                    infa = "+4 к силе применения карты атаки| |+2 к месту в инвентаре для карты атаки любой мощности(пассивный)";
                    type = type_cart.art_att_act;
                }



                if (name == "cart speed att")
                {
                    infa = "+1 к скорости применения карты атаки| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_att;
                }
                if (name == "cart speed att 1")
                {
                    infa = "+2 к скорости применения карты атаки| |+2 к месту в инвентаре для карты атаки мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_att;
                }
                if (name == "cart speed att 2")
                {
                    infa = "+4 к скорости применения карты атаки| |+2 к месту в инвентаре для карты атаки любой мощности(пассивный)";
                    type = type_cart.art_speed_att;
                }

                if (name == "cart att cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт атаки| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_cuzna;
                }
                if (name == "cart att cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт атаки| |+2 к месту в инвентаре для карты атаки мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_cuzna;
                }
                if (name == "cart att cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт атаки| |+2 к месту в инвентаре для карты атаки любой мощности(пассивный)";
                    type = type_cart.art_att_cuzna;
                }


                if (name == "cart att bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты атаки| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для карты атаки мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_bomb;
                }
                if (name == "cart att bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты атаки| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для карты атаки мощности 1 м 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_att_bomb;
                }
                if (name == "cart att bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты атаки| на соседние клетки с мощностью 4| |+2 к месту в инвентаре для карты атаки любой мощности(пассивный)";
                    type = type_cart.art_att_bomb;
                }
                #endregion
                #region art_bar
                if (name == "cart sp barier")
                {
                    infa = "создаёт карту барьера мощности 1 и добавляет в инвентарь| |+1 к месту в инвентаре для карты барьера мощности 1(пассивный)| |карта может быть улучшена";
                    type=type_cart.art_bar_sp;
                }
                if (name == "cart sp barier 1")
                {
                    infa = "создаёт карту барьера мощности 2 и добавляет в инвентарь| |+1 к месту в инвентаре для карты барьера мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_sp;

                }
                if (name == "cart sp barier 2")
                {
                    infa = "создаёт карту барьера мощности 4 и добавляет в инвентарь| |+1 к месту в инвентаре для карты барьера любой мощности(пассивный)";
                    type = type_cart.art_bar_sp;

                }

                if (name == "cart bar pow")
                {
                    infa = "+1 к мощности карты барьера| |+1 к месту в инвентаре для карты барьера мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_pow;
                }
                if (name == "cart bar pow 1")
                {
                    infa = "+2 к мощности карты барьера| |+1 к месту в инвентаре для карты барьера мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_pow;

                }
                if (name == "cart bar pow 2")
                {
                    infa = "+4 к мощности карты барьера| |+1 к месту в инвентаре для карты барьера любой мощности(пассивный)";
                    type = type_cart.art_bar_pow;

                }


                if (name == "cart act bar")
                {
                    infa = "+1 к силе применения карты барьера| |+1 к месту в инвентаре для карты барьера мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_act;
                }
                if (name == "cart act bar 1")
                {
                    infa = "+2 к силе применения карты барьера| |+1 к месту в инвентаре для карты барьера мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_act;
                }
                if (name == "cart act bar 2")
                {
                    infa = "+4 к силе применения карты барьера| |+1 к месту в инвентаре для карты барьера любой мощности(пассивный)";
                    type = type_cart.art_bar_act;
                }



                if (name == "cart speed bar")
                {
                    infa = "+1 к скорости применения карты барьера| |+1 к месту в инвентаре для карты барьера мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_bar;
                }
                if (name == "cart speed bar 1")
                {
                    infa = "+2 к скорости применения карты барьера| |+1 к месту в инвентаре для карты барьера мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_bar;
                }
                if (name == "cart speed bar 2")
                {
                    infa = "+4 к скорости применения карты барьера| |+1 к месту в инвентаре для карты барьера любой мощности(пассивный)";
                    type = type_cart.art_speed_bar;
                }

                if (name == "cart bar cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт барьера| |+1 к месту в инвентаре для карты барьера мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_cuzna;
                }
                if (name == "cart bar cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт барьера| |+1 к месту в инвентаре для карты барьера мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_cuzna;
                }
                if (name == "cart bar cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт барьера| |+1 к месту в инвентаре для карты барьера любой мощности(пассивный)";
                    type = type_cart.art_bar_cuzna;
                }


                if (name == "cart bar bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты барьера| на соседние клетки с мощностью 1| |+1 к месту в инвентаре для карты барьера мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_bomb;
                }
                if (name == "cart bar bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты барьера| на соседние клетки с мощностью 2| |+1 к месту в инвентаре для карты барьера мощности 1 м 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bar_bomb;
                }
                if (name == "cart bar bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты барьера| на соседние клетки с мощностью 4| |+1 к месту в инвентаре для карты барьера любой мощности(пассивный)";
                    type = type_cart.art_bar_bomb;
                }
                #endregion

                #region art_sp
                if (name == "cart sp fire")
                {
                    infa = "создаёт огненную карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type=type_cart.art_fire_sp;
                }
                if (name == "cart sp poition")
                {
                    infa = "создаёт ядовитую карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)| |карта может быть улучшена";
                    type= type_cart.art_poition_sp;
                }
                if (name == "cart sp rock")
                {
                    infa = "создаёт каменную карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_rock_sp;
                }
                if (name == "cart sp blood")
                {
                    infa = "создаёт кровавую карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_blood_sp;

                }
                if (name == "cart sp ice")
                {
                    infa = "создаёт ледяную карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_ice_sp;

                }
                if (name == "cart sp energy")
                {
                    infa = "создаёт электрическую карту мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_energy_sp;

                }

                if (name == "cart sp fire 1")
                {
                    infa = "создаёт огненную карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_fire_sp;

                }
                if (name == "cart sp poition 1")
                {
                    infa = "создаёт ядовитую карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_poition_sp;
                }
                if (name == "cart sp rock 1")
                {
                    infa = "создаёт каменную карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_rock_sp;

                }
                if (name == "cart sp blood 1")
                {
                    infa = "создаёт кровавую карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_blood_sp;

                }
                if (name == "cart sp ice 1")
                {
                    infa = "создаёт ледяную карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_ice_sp;

                }
                if (name == "cart sp energy 1")
                {
                    infa = "создаёт электрическую карту мощности 2 и добавляет в инвентарь| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_energy_sp;

                }

                if (name == "cart sp fire 2")
                {
                    infa = "создаёт огненную карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_fire_sp;

                }
                if (name == "cart sp poition 2")
                {
                    infa = "создаёт ядовитую карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";
                    type = type_cart.art_poition_sp;

                }
                if (name == "cart sp rock 2")
                {
                    infa = "создаёт каменную карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";
                    type = type_cart.art_rock_sp;

                }
                if (name == "cart sp blood 2")
                {
                    infa = "создаёт кровавую карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";
                    type = type_cart.art_blood_sp;

                }
                if (name == "cart sp ice 2")
                {
                    infa = "создаёт ледяную карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";
                    type = type_cart.art_ice_sp;

                }
                if (name == "cart sp energy 2")
                {
                    infa = "создаёт электрическую карту мощности 4 и добавляет в инвентарь| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";
                    type = type_cart.art_energy_sp;

                }

                #endregion
                #region art_act
                if (name == "cart act fire")
                {
                    infa = "+1 к силе применения огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type=type_cart.art_fire_act;
                }
                if (name == "cart act poition")
                {
                    infa = "+1 к силе применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_poition_act;

                }
                if (name == "cart act rock")
                {
                    infa = "+1 к силе применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_rock_act;

                }
                if (name == "cart act blood")
                {
                    infa = "+1 к силе применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_blood_act;

                }
                if (name == "cart act ice")
                {
                    infa = "+1 к силе применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_ice_act;

                }
                if (name == "cart act energy")
                {
                    infa = "+1 к силе применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_energy_act;

                }

                if (name == "cart act fire 1")
                {
                    infa = "+2 к силе применения огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_fire_act;
                }
                if (name == "cart act poition 1")
                {
                    infa = "+2 к силе применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_poition_act;

                }
                if (name == "cart act rock 1")
                {
                    infa = "+2 к силе применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_rock_act;

                }
                if (name == "cart act blood 1")
                {
                    infa = "+2 к силе применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_blood_act;

                }
                if (name == "cart act ice 1")
                {
                    infa = "+2 к силе применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_ice_act;

                }
                if (name == "cart act energy 1")
                {
                    infa = "+2 к силе применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_energy_act;

                }

                if (name == "cart act fire 2")
                {

                    infa = "+4 к силе применения карты| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_fire_act;

                }
                if (name == "cart act poition 2")
                {
                    infa = "+4 к силе применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";

                    type = type_cart.art_poition_act;

                }
                if (name == "cart act rock 2")
                {
                    infa = "+4 к силе применения каменной карты| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";

                    type = type_cart.art_rock_act;

                }
                if (name == "cart act blood 2")
                {
                    infa = "+4 к силе применения кровавой карты| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";

                    type = type_cart.art_blood_act;

                }
                if (name == "cart act ice 2")
                {
                    infa = "+4 к силе применения ледяной карты| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";

                    type = type_cart.art_ice_act;

                }
                if (name == "cart act energy 2")
                {
                    infa = "+4 к силе применения электрической карты| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";

                    type = type_cart.art_energy_act;

                }
                #endregion
                #region art_speed

                if (name == "cart speed fire")
                {
                    infa = "+1 к скорости применения огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_fire;
                }
                if (name == "cart speed poition")
                {
                    infa = "+1 к скорости применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_poition;

                }
                if (name == "cart speed rock")
                {
                    infa = "+1 к скорости применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_rock;

                }
                if (name == "cart speed blood")
                {
                    infa = "+1 к скорости применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_blood;

                }
                if (name == "cart speed ice")
                {
                    infa = "+1 к скорости применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_ice;

                }
                if (name == "cart speed energy")
                {
                    infa = "+1 к скорости применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_energy;

                }

                if (name == "cart speed fire 1")
                {
                    infa = "+2 к скорости применения огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_fire;
                }
                if (name == "cart speed poition 1")
                {
                    infa = "+2 к скорости применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_poition;

                }
                if (name == "cart speed rock 1")
                {
                    infa = "+2 к скорости применения каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_rock;

                }
                if (name == "cart speed blood 1")
                {
                    infa = "+2 к скорости применения кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_blood;

                }
                if (name == "cart speed ice 1")
                {
                    infa = "+2 к скорости применения ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_ice;

                }
                if (name == "cart speed energy 1")
                {
                    infa = "+2 к скорости применения электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_speed_energy;

                }

                if (name == "cart speed fire 2")
                {

                    infa = "+4 к скорости применения карты| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_speed_fire;

                }
                if (name == "cart speed poition 2")
                {
                    infa = "+4 к скорости применения ядовитой карты| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";

                    type = type_cart.art_speed_poition;

                }
                if (name == "cart speed rock 2")
                {
                    infa = "+4 к скорости применения каменной карты| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";

                    type = type_cart.art_speed_rock;

                }
                if (name == "cart speed blood 2")
                {
                    infa = "+4 к скорости применения кровавой карты| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";

                    type = type_cart.art_speed_blood;

                }
                if (name == "cart speed ice 2")
                {
                    infa = "+4 к скорости применения ледяной карты| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";

                    type = type_cart.art_speed_ice;

                }
                if (name == "cart speed energy 2")
                {
                    infa = "+4 к скорости применения электрической карты| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";

                    type = type_cart.art_speed_energy;

                }
                #endregion

                #region art_pow
                if (name == "cart fire pow")
                {
                    infa = "+1 к мощности огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_fire;

                }
                if (name == "cart poition pow")
                {
                    infa = "+1 к мощности ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_poition;

                }
                if (name == "cart rock pow")
                {
                    infa = "+1 к мощности каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_rock;

                }
                if (name == "cart blood pow")
                {
                    infa = "+1 к мощности кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_blood;

                }
                if (name == "cart ice pow")
                {
                    infa = "+1 к мощности ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_ice;

                }
                if (name == "cart energy pow")
                {
                    infa = "+1 к мощности электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_energy;

                }


                if (name == "cart fire pow 1")
                {
                    infa = "+2 к мощности огненной карты| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_fire;

                }
                if (name == "cart poition pow 1")
                {
                    infa = "+2 к мощности ядовитой карты| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_poition;

                }
                if (name == "cart rock pow 1")
                {
                    infa = "+2 к мощности каменной карты| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_rock;

                }
                if (name == "cart blood pow 1")
                {
                    infa = "+2 к мощности кровавой карты| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_blood;

                }
                if (name == "cart ice pow 1")
                {
                    infa = "+2 к мощности ледяной карты| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_ice;

                }
                if (name == "cart energy pow 1")
                {
                    infa = "+2 к мощности электрической карты| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_energy;

                }



                if (name == "cart fire pow 2")
                {
                    infa = "+4 к мощности огненной карты| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";

                    type = type_cart.art_pow_fire;

                }
                if (name == "cart poition pow 2")
                {
                    infa = "+4 к мощности ядовитой карты| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";

                    type = type_cart.art_pow_poition;

                }
                if (name == "cart rock pow 2")
                {
                    infa = "+4 к мощности каменной карты| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";

                    type = type_cart.art_pow_rock;

                }
                if (name == "cart blood pow 2")
                {
                    infa = "+4 к мощности кровавой карты| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";

                    type = type_cart.art_pow_blood;

                }
                if (name == "cart ice pow 2")
                {
                    infa = "+4 к мощности ледяной карты| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";

                    type = type_cart.art_pow_ice;

                }
                if (name == "cart energy pow 2")
                {
                    infa = "+4 к мощности электрической карты| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";

                    type = type_cart.art_pow_energy;

                }
                #endregion
                #region art_cuzna
                if (name == "cart fire cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных огненных карт| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type=type_cart.art_fire_cuzna;
                }
                if (name == "cart poition cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных ядовитых карт| |+2 к месту в инвентаре для ядовитой карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_poition_cuzna;
                }
                if (name == "cart rock cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных каменных карт| |+2 к месту в инвентаре для каменной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_rock_cuzna;

                }
                if (name == "cart blood cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных кровавых карт| |+2 к месту в инвентаре для кровавой карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_blood_cuzna;

                }
                if (name == "cart ice cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных ледяных карт| |+2 к месту в инвентаре для ледяной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_ice_cuzna;

                }
                if (name == "cart energy cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных электрических карт| |+2 к месту в инвентаре для электрической карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_energy_cuzna;

                }


                if (name == "cart fire cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных огненных карт| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_fire_cuzna;
                }
                if (name == "cart poition cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных ядовитых карт| |+2 к месту в инвентаре для ядовитой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_poition_cuzna;
                }
                if (name == "cart rock cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных каменных карт| |+2 к месту в инвентаре для каменной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_rock_cuzna;

                }
                if (name == "cart blood cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных кровавых карт| |+2 к месту в инвентаре для кровавой карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_blood_cuzna;

                }
                if (name == "cart ice cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных ледяных карт| |+2 к месту в инвентаре для ледяной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_ice_cuzna;

                }
                if (name == "cart energy cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных электрических карт| |+2 к месту в инвентаре для электрической карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_energy_cuzna;

                }


                if (name == "cart fire cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных огненных карт| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_fire_cuzna;
                }
                if (name == "cart poition cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных ядовитых карт| |+2 к месту в инвентаре для ядовитой карты любой мощности(пассивный)";
                    type = type_cart.art_poition_cuzna;
                }
                if (name == "cart rock cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных каменных карт| |+2 к месту в инвентаре для каменной карты любой мощности(пассивный)";
                    type = type_cart.art_rock_cuzna;

                }
                if (name == "cart blood cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных кровавых карт| |+2 к месту в инвентаре для кровавой карты любой мощности(пассивный)";
                    type = type_cart.art_blood_cuzna;

                }
                if (name == "cart ice cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных ледяных карт| |+2 к месту в инвентаре для ледяной карты любой мощности(пассивный)";
                    type = type_cart.art_ice_cuzna;

                }
                if (name == "cart energy cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных электрических карт| |+2 к месту в инвентаре для электрической карты любой мощности(пассивный)";
                    type = type_cart.art_energy_cuzna;

                }

                /*
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
                */
                #endregion
                #region art_bomb
                if (name == "cart fire bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_fire_bomb;
                }
                if (name == "cart poition bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_poition_bomb;

                }
                if (name == "cart ice bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_ice_bomb;

                }
                if (name == "cart energy bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_energy_bomb;

                }
                if (name == "cart blood bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_blood_bomb;

                }
                if (name == "cart rock bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 1| |+2 к месту в инвентаре для огненной карты мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_rock_bomb;


                }


                if (name == "cart fire bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_fire_bomb;
                }
                if (name == "cart poition bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_poition_bomb;

                }
                if (name == "cart ice bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_ice_bomb;

                }
                if (name == "cart energy bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_energy_bomb;

                }
                if (name == "cart blood bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_blood_bomb;

                }
                if (name == "cart rock bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_rock_bomb;


                }


                if (name == "cart fire bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 4| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_fire_bomb;
                }
                if (name == "cart poition bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 4| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_poition_bomb;

                }
                if (name == "cart ice bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 4| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_ice_bomb;

                }
                if (name == "cart energy bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 4| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_energy_bomb;

                }
                if (name == "cart blood bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 4| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_blood_bomb;

                }
                if (name == "cart rock bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 4| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                    type = type_cart.art_rock_bomb;


                }
                /*
                if (name == "cart fire bomb 1")
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart poition bomb 1")
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart ice bomb 1")
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart energy bomb 1")
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart blood bomb 1")
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";
                if (name == "cart rock bomb 1")
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты мощности 1 и 2(пассивный)";

                if (name == "cart fire bomb 2")
                    infa = "распространяет магический эффект, созданный с помощью огненной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart poition bomb 2")
                    infa = "распространяет магический эффект, созданный с помощью ядовитой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart ice bomb 2")
                    infa = "распространяет магический эффект, созданный с помощью ледяной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart energy bomb 2")
                    infa = "распространяет магический эффект, созданный с помощью электрической карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart blood bomb 2")
                    infa = "распространяет магический эффект, созданный с помощью кровавой карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                if (name == "cart rock bomb 2")
                    infa = "распространяет магический эффект, созданный с помощью каменной карты| на соседние клетки с мощностью 2| |+2 к месту в инвентаре для огненной карты любой мощности(пассивный)";
                */
                #endregion
                #region art_touch
                if (name == "cart fire touch")
                {
                    infa = "при применении карт дополнительно применяет| огненную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                    type = type_cart.art_touch_fire;
                }
                if (name == "cart poition touch")
                {
                    infa = "при применении карт дополнительно применяет| ядовитую карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                    type = type_cart.art_touch_poition;

                }
                if (name == "cart rock touch")
                {
                    infa = "при применении карт дополнительно применяет| каменную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                    type = type_cart.art_touch_rock;

                }
                if (name == "cart blood touch")
                {
                    infa = "при применении карт дополнительно применяет| каменную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                    type = type_cart.art_touch_blood;

                }
                if (name == "cart ice touch")
                {
                    infa = "при применении карт дополнительно применяет| ледяную карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                    type = type_cart.art_touch_ice;

                }
                if (name == "cart energy touch")
                {
                    infa = "при применении карт дополнительно применяет| электрическую карту мощности 1(+1 за каждый дополнительный такой же артефакт)";
                    type = type_cart.art_touch_energy;

                }

                #endregion
                #region art_bomb
                if (name == "cart sp bomb")
                {
                    infa = "создаёт карту взыва мощности 1 и добавляет в инвентарь| |+1 к месту в инвентаре для карты взрыва мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_sp;

                }
                if (name == "cart sp bomb 1")
                {
                    infa = "создаёт карту взрыва мощности 2 и добавляет в инвентарь| |+1 к месту в инвентаре для карты взрыва мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_sp;

                }
                if (name == "cart sp bomb 2")
                {
                    infa = "создаёт карту взрыва мощности 4 и добавляет в инвентарь| |+1 к месту в инвентаре для карты взрыва любой мощности(пассивный)";
                    type = type_cart.art_bomb_sp;

                }
                if (name == "cart act bomb")
                {
                    infa = "+1 к силе применения карты взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_act;
                }
                if (name == "cart act bomb 1")
                {
                    infa = "+2 к силе применения карты взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_act;
                }
                if (name == "cart act bomb 2")
                {
                    infa = "+4 к силе применения карты взрыва| |+1 к месту в инвентаре для карты взрыва любой мощности(пассивный)";
                    type = type_cart.art_bomb_act;
                }

                if (name == "cart speed bomb")
                {
                    infa = "+1 к скорости применения карты взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_bomb;
                }
                if (name == "cart speed bomb 1")
                {
                    infa = "+2 к скорости применения карты взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_bomb;
                }
                if (name == "cart speed bomb 2")
                {
                    infa = "+4 к скорости применения карты взрыва| |+1 к месту в инвентаре для карты взрыва любой мощности(пассивный)";
                    type = type_cart.art_speed_bomb;
                }

                if (name == "cart bomb pow")
                {
                    infa = "+1 к мощности карты взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_bomb;

                }
                if (name == "cart bomb pow 1")
                {
                    infa = "+2 к мощности карты взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_pow_bomb;

                }
                if (name == "cart bomb pow 2")
                {
                    infa = "+4 к мощности карты взрыва| |+1 к месту в инвентаре для карты взрыва любой мощности(пассивный)";
                    type = type_cart.art_pow_bomb;
                }

                if (name == "cart bomb cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_cuzna;
                }
                if (name == "cart bomb cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт взрыва| |+1 к месту в инвентаре для карты взрыва мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_cuzna;
                }
                if (name == "cart bomb cuzna 2")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт взрыва| |+1 к месту в инвентаре для карты взрыва любой мощности(пассивный)";
                    type = type_cart.art_bomb_cuzna;
                }

                if (name == "cart bomb bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты взрыва| на соседние клетки с мощностью 1| |+1 к месту в инвентаре для карты взрыва мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_bomb;
                }
                if (name == "cart bomb bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты взрыва| на соседние клетки с мощностью 2| |+1 к месту в инвентаре для карты взрыва мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_bomb;
                }
                if (name == "cart bomb bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты взрыва| на соседние клетки с мощностью 4| |+1 к месту в инвентаре для карты взрыва любой мощности(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_bomb_bomb;
                }
                #endregion


                #region art_dych
                if (name == "cart sp dych")
                {
                    infa = "создаёт карту некроманта мощности 1 и добавляет в инвентарь| |+1 к месту в инвентаре для карты некроманта мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_sp;

                }
                if (name == "cart sp dych 1")
                {
                    infa = "создаёт карту некроманта мощности 2 и добавляет в инвентарь| |+1 к месту в инвентаре для карты некроманта мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_sp;

                }
                if (name == "cart sp dych 2")
                {
                    infa = "создаёт карту некроманта мощности 4 и добавляет в инвентарь| |+1 к месту в инвентаре для карты некроманта  любой мощности(пассивный)";
                    type = type_cart.art_dych_sp;
                }

                if (name == "cart act dych")
                {
                    infa = "+1 к силе применения карты некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_act;
                }
                if (name == "cart act dych 1")
                {
                    infa = "+2 к силе применения карты некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_act;
                }
                if (name == "cart act dych 2")
                {
                    infa = "+4 к силе применения карты некроманта| |+1 к месту в инвентаре для карты некроманта любо мощности(пассивный)";
                    type = type_cart.art_dych_act;
                }

                if (name == "cart speed dych")
                {
                    infa = "+1 к скорости применения карты некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_dych;
                }
                if (name == "cart speed dych 1")
                {
                    infa = "+2 к скорости применения карты некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_speed_dych;
                }
                if (name == "cart speed dych 2")
                {
                    infa = "+4 к скорости применения карты некроманта| |+1 к месту в инвентаре для карты некроманта любой мощности(пассивный)";
                    type = type_cart.art_speed_dych;
                }

                if (name == "cart dych pow")
                {
                    infa = "+1 к мощности карты некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_dych_pow;

                }
                if (name == "cart dych pow 1")
                {
                    infa = "+2 к мощности карты некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1 и 2(пассивный)| |карта может быть улучшена";

                    type = type_cart.art_dych_pow;

                }
                if (name == "cart dych pow 2")
                {
                    infa = "+4 к мощности карты некроманта| |+1 к месту в инвентаре для карты некроманта любой мощности(пассивный)";

                    type = type_cart.art_dych_pow;

                }

                if (name == "cart dych cuzna")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_cuzna;
                }
                if (name == "cart dych cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт некроманта| |+1 к месту в инвентаре для карты некроманта мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_cuzna;
                }
                if (name == "cart dych cuzna 1")
                {
                    infa = "сохраняет карту кузнеца при объединении обычных карт некроманта| |+1 к месту в инвентаре для карты некроманта любой мощности(пассивный)";
                    type = type_cart.art_dych_cuzna;
                }

                if (name == "cart dych bomb")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты некроманта| на соседние клетки с мощностью 1| |+1 к месту в инвентаре для карты некроманта мощности 1(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_bomb;
                }

                if (name == "cart dych bomb 1")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты некроманта| на соседние клетки с мощностью 2| |+1 к месту в инвентаре для карты некроманта мощности 1 и 2(пассивный)| |карта может быть улучшена";
                    type = type_cart.art_dych_bomb;
                }

                if (name == "cart dych bomb 2")
                {
                    infa = "распространяет магический эффект, созданный с помощью карты некроманта| на соседние клетки с мощностью 4| |+1 к месту в инвентаре для карты некроманта любой мощности(пассивный)";
                    type = type_cart.art_dych_bomb;
                }

                #endregion
                if (name == "cart berserk")
                {
                    infa = "даёт эффект ярости на 2 хода:| |+1 к скорости применения всех карт|+1 к силе применения всех карт|+1 к мощности применения всех карт| |перезарядка происходит только за счёт полученного урона| |карта может быть улучшена";
                    type = type_cart.art_berserk;
                }
                if (name == "cart dop hill")
                {
                    infa = "+10 к максимальному запасу здоровья| |карта может быть улучшена";
                    type = type_cart.art_hp_max;

                }

                if (name == "cart mini spirit")
                {
                    infa = "карта(или комбинация карт) создают магические частицы при применении|(если карта(или комбинация карт) не создают магические частицы)| |+1 к мощности создания магических частиц|(если карта(или комбинация карт) создают магические частицы)| |карта может быть улучшена";
                    type = type_cart.art_mini_spirit;
                }
                if (name == "cart mini shaman")
                {
                    infa = "создаёт магический эффект, который был получен|от заклинателя в сторону заклинателя с мощностью 1| |карта может быть улучшена";
                    type = type_cart.art_mini_shaman;
                }

                if (name == "cart inven")
                {
                    infa = "+2 места в инвентаре для обычных карт и +1 место в инвентаре для специальных карт| |+2 дополнительных слота для артефакта";
                    type = type_cart.art_dop_carts;
                }
                if (name == "cart mast")
                {
                    infa = "применение обычной карты уменьшает перезарядку всех артефактов на 1(но не менее 1)(пассивный)| |применение специальной карты уменьшает перезарядку всех артефактов на 2| |дополнительно ускоряет перезарядку артефакта на 1";
                    type = type_cart.art_mast;
                }
                if (name == "cart universal")
                {
                    infa = "создаёт карту защиты, карту некроманта и карту взрыва|мощности 1 и добавляет в инвентарь| |карта может быть улучшена";
                    type = type_cart.art_universal;
                }
                if (name == "cart elemental")
                {
                    infa = "создаёт карту каждого элемента мощности 1 и добавляет в инвентарь| |карта может быть улучшена";
                    type = type_cart.art_elemental;
                }

                if (name == "cart universal 1")
                {
                    infa = "создаёт карту защиты, карту некроманта и карту взрыва|мощности 2 и добавляет в инвентарь";
                    type = type_cart.art_universal;
                }
                if (name == "cart elemental 1")
                {
                    infa = "создаёт карту каждого элемента мощности 2 и добавляет в инвентарь";
                    type = type_cart.art_elemental;
                }


                if (name == "cart brilliant")
                {
                    infa = "уменьшает стоимость покупки на 3 золотые карты| |(перезарядка происходит при переходе этажей)";
                    type = type_cart.art_brilliant;
                }


                if (name == "cart potoc fire")
                {
                    infa = "дополнительно поглощает полученный магический эффект в виде огненных карт| |+1 к мощности огненной карты за каждые 6 единицы мощности огненных карт в инвентаре";
                    type = type_cart.art_potoc_fire;
                }
                if (name == "cart potoc poition")
                {
                    infa = "дополнительно поглощает полученный магический эффект в виде ядовитых карт| |+1 к мощности ядовитой карты за каждые 6 единицы мощности ядовитых карт в инвентаре";
                    type = type_cart.art_potoc_poition;
                }
                if (name == "cart potoc rock")
                {
                    infa = "дополнительно поглощает полученный магический эффект в виде каменных карт| |+1 к мощности каменной карты за каждые 6 единицы мощности каменных карт в инвентаре";
                    type = type_cart.art_potoc_rock;

                }
                if (name == "cart potoc blood")
                {
                    infa = "дополнительно поглощает полученный магический эффект в виде кровавых карт| |+1 к мощности кровавой карты за каждые 6 единицы мощности кровавых карт в инвентаре";
                    type = type_cart.art_potoc_blood;

                }
                if (name == "cart potoc ice")
                {
                    infa = "дополнительно поглощает полученный магический эффект в виде ледяных карт| |+1 к мощности ледяной карты за каждые 6 единицы мощности ледяных карт в инвентаре";
                    type = type_cart.art_potoc_ice;

                }
                if (name == "cart potoc energy")
                {
                    infa = "дополнительно поглощает полученный магический эффект в виде электрических карт| |+1 к мощности электрической карты за каждые 6 единицы мощности электрических карт в инвентаре";
                    type = type_cart.art_potoc_energy;

                }

                if (name == "cart fire heart")
                {
                    infa = "поглощает огонь или огненную атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение| |карта может быть улучшена";
                    type = type_cart.art_fire_heart;
                }
                if (name == "cart poition heart")
                {
                    infa = "поглощает яд или ядовитую атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение| |карта может быть улучшена";
                    type = type_cart.art_poition_heart;

                }
                if (name == "cart ice heart")
                {
                    infa = "поглощает лёд или ледяную атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение| |карта может быть улучшена";
                    type= type_cart.art_ice_heart;
                }
                if (name == "cart rock heart")
                {
                    infa = "поглощает камни или каменную атаку для восстановления здоровья|(в размере мощности эффекта)| |тратит заряд на одно поглощение| |карта может быть улучшена";
                    type = type_cart.art_rock_heart;

                }
                if (name == "cart blood heart")
                {
                    infa = "дополнительно поглощает кровь|для восстановления здоровья(в размере мощности эффекта)| |поглощает кровавую атаку для восстановления здоровья(в размере мощности эффекта)| |тратит заряд на одно поглощение| |карта может быть улучшена";
                    type = type_cart.art_blood_heart;

                }
                if (name == "cart energy heart")
                {
                    infa = "дополнительно поглощает электричество|для восстановления здоровья(в размере мощности эффекта)| |поглощает кровавую атаку для восстановления здоровья(в размере мощности эффекта)| |тратит заряд на одно поглощение| |карта может быть улучшена";
                    type = type_cart.art_energy_heart;

                }

                if (name == "cart spirit fire heart")
                {
                    infa = "поглощает огонь или огненную атаку для восстановления здоровья|(в размере мощности эффекта)| |+10 к максимальному запасу здоровья";
                    type = type_cart.art_fire_heart;

                }
                if (name == "cart spirit poition heart")
                {
                    infa = "поглощает яд или ядовитую атаку для восстановления здоровья|(в размере мощности эффекта)| |+10 к максимальному запасу здоровья";
                    type = type_cart.art_poition_heart;

                }
                if (name == "cart spirit ice heart")
                {
                    infa = "поглощает лёд или ледяную атаку для восстановления здоровья|(в размере мощности эффекта)| |+10 к максимальному запасу здоровья";
                    type = type_cart.art_ice_heart;

                }
                if (name == "cart spirit rock heart")
                {
                    infa = "поглощает камни или каменную атаку для восстановления здоровья|(в размере мощности эффекта)| |+10 к максимальному запасу здоровья";

                    type = type_cart.art_rock_heart;

                }
                if (name == "cart spirit blood heart")
                {
                    infa = "дополнительно поглощает кровь|для восстановления здоровья(в размере двойной мощности эффекта)| |поглощает кровавую атаку для восстановления здоровья(в размере мощности эффекта)| |+10 к максимальному запасу здоровья";
                    type = type_cart.art_blood_heart;

                }
                if (name == "cart spirit energy heart")
                {
                    infa = "дополнительно поглощает электричество|для восстановления здоровья(в размере двойной мощности эффекта)| |поглощает электрическую атаку для восстановления здоровья(в размере мощности эффекта)| |+10 к максимальному запасу здоровья";
                    type = type_cart.art_energy_heart;

                }


                if (name == "cart cuzna")
                {
                    infa = "позволяет использовать только 1 карту вместо 2 при объединении карт| |тратит заряд в размере мощности улучшаемой карты";
                    type = type_cart.art_cuzna;
                }

                if (name == "cart speed")
                {
                    infa = "+1 к скорости применения всех карт";
                    type = type_cart.art_speed;
                }
                if (name == "cart pow")
                {
                    infa = "+1 к мощности применения всех карт";
                    type = type_cart.art_pow;
                }
                if (name == "cart sila")
                {
                    infa = "+1 к силе применения всех карт";
                    type = type_cart.art_sila;

                }
                if (name == "cart respawn")
                {
                    infa = "воскрешает при получении смертельного урона|, полностью восстанавливая здоровье| |после воскрешения артефакт уничтожается";

                    type = type_cart.art_respawn;
                }


                if (name == "cart shaman boss")
                {
                    infa = "создаёт магический эффект,| который был получен от заклинателя, на самого заклинателя|мощности 1(+1 за каждые 5 единиц отсутствующего здоровья)";
                    type = type_cart.art_boss_shaman_tuman;
                }
                if (name == "cart spirit")
                {
                    infa = "создаёт карту магических частиц и добавляет в инвентарь| |+8 к длительности эффекта магических частиц";
                    type = type_cart.art_boss_shaman;

                }

                if (name == "cart spirit(boss)")
                {
                    infa = "позволяет комбинировать вместе с картами|для создания магических частиц от применённых карт|(2 хода за каждую единицу мощности)";
                    type = type_cart.shaman_boss;

                }


                if (name == "cart berserk boss")
                {
                    infa = "создаёт защиту тела(1 заряд за каждые 5 единиц отсутствующего здоровья)|тратит заряд в размере полученного урона| |+20 к максимальному запасу здоровья(пассивный)| |перезарядка происходит только за счёт полученного урона";
                    type = type_cart.art_boss_berserk;
                }
                if (name == "cart rage")
                {
                    infa = "создаёт карту ярости и добавляет в инвентарь";
                    type = type_cart.art_boss_berserk_rage;

                }

                if (name == "cart rage(boss)")
                {
                    infa = "даёт эффект ярости на 2 хода:| |+1 к скорости применения всех карт|+1 к силе применения всех карт|+1 к мощности применения всех карт";
                    type = type_cart.rage_boss;

                }


                if (name == "cart mini spirit fire")
                {
                    infa = "огненная карта создаёт огненные частицы при применении| |карта может быть улучшена";
                    type = type_cart.art_spirit_fire;

                }
                if (name == "cart mini spirit blood")
                {
                    infa = "кровавая карта создаёт кровавые частицы при применении| |карта может быть улучшена";
                    type = type_cart.art_spirit_blood;
                }
                if (name == "cart mini spirit poition")
                {
                    infa = "ядовитая карта создаёт ядовитые частицы при применении| |карта может быть улучшена";
                    type = type_cart.art_spirit_poition;

                }
                if (name == "cart mini spirit energy")
                {
                    infa = "электрическая карта создаёт электрические частицы при применении| |карта может быть улучшена";
                    type = type_cart.art_spirit_energy;

                }
                if (name == "cart mini spirit ice")
                {
                    infa = "ледяная карта создаёт ледяные частицы при применении| |карта может быть улучшена";
                    type = type_cart.art_spirit_ice;

                }
                if (name == "cart mini spirit rock")
                {
                    infa = "каменная карта создаёт каменные частицы при применении| |карта может быть улучшена";
                    type = type_cart.art_spirit_rock;

                }

                if (name == "cart sp spirit fire")
                {
                    infa = "создаёт карту частиц огня и добавляет в инвентарь| |+2 к длительности поджога от огненной карты за каждую единицу мощности";
                    type = type_cart.art_spirit_fire;

                }
                if (name == "cart sp spirit blood")
                {
                    infa = "создаёт карту частиц крови и добавляет в инвентарь| |+1 к восстановлению здоровья от кровавой карты за каждую единицу мощности";
                    type = type_cart.art_spirit_blood;

                }
                if (name == "cart sp spirit poition")
                {
                    infa = "создаёт карту частиц яда и добавляет в инвентарь| |+1 к наносимому урону от ядовитой карты за каждую единицу мощности";
                    type = type_cart.art_spirit_poition;

                }
                if (name == "cart sp spirit energy")
                {
                    infa = "создаёт карту частиц электричества и добавляет в инвентарь | |+2 к длительности эффекта от электрической карты за каждую единицу мощности";
                    type = type_cart.art_spirit_energy;

                }
                if (name == "cart sp spirit ice")
                {
                    infa = "создаёт карту частиц льда и добавляет в инвентарь| |+2 к длительности эффекта от ледяной карты за каждую единицу мощности";
                    type = type_cart.art_spirit_ice;

                }
                if (name == "cart sp spirit rock")
                {
                    infa = "создаёт карту частиц камня и добавляет в инвентарь| |+1 к наносимому урону от каменной карты за каждую единицу мощности";
                    type = type_cart.art_spirit_rock;

                }



                if (name == "cart spirit fire")
                {
                    infa = "создаёт огненные частицы на 8 ходов";
                    type = type_cart.spirit_fire;
                }
                if (name == "cart spirit blood")
                {
                    infa = "создаёт кровавые частицы на 8 ходов";
                    type = type_cart.spirit_blood;
                }
                if (name == "cart spirit poition")
                {
                    infa = "создаёт ядовитые частицы на 8 ходов";
                    type = type_cart.spirit_rock;

                }
                if (name == "cart spirit energy")
                {
                    infa = "создаёт электрические частицы на 8 ходов";
                    type = type_cart.spirit_energy;

                }
                if (name == "cart spirit ice")
                {
                    infa = "создаёт ледяные частицы на 8 ходов";
                    type = type_cart.spirit_ice;

                }
                if (name == "cart spirit rock")
                {
                    infa = "создаёт каменные частицы на 8 ходов";
                    type = type_cart.spirit_rock;

                }



                if (name == "cart mini mast fire")
                {
                    infa = "+1 к мощности огненной карты| |+1 к скорости применения огненной карты| |+1 к силе применения огненной карты| |карта может быть улучшена";
                    type = type_cart.art_fire_mast;
                }
                if (name == "cart mini mast poition")
                {
                    infa = "+1 к мощности ядовитой карты| |+1 к скорости применения ядовитой карты| |+1 к силе применения ядовитой карты| |карта может быть улучшена";
                    type = type_cart.art_poition_mast;

                }
                if (name == "cart mini mast ice")
                {
                    infa = "+1 к мощности ледяной карты| |+1 к скорости применения ледяной карты| |+1 к силе применения ледяной карты| |карта может быть улучшена";
                    type = type_cart.art_ice_mast;

                }
                if (name == "cart mini mast energy")
                {
                    infa = "+1 к мощности электрической карты| |+1 к скорости применения электрической карты| |+1 к силе применения электрической карты| |карта может быть улучшена";
                    type = type_cart.art_energy_mast;

                }
                if (name == "cart mini mast rock")
                {
                    infa = "+1 к мощности каменной карты| |+1 к скорости применения каменной карты| |+1 к силе применения каменной карты| |карта может быть улучшена";
                    type = type_cart.art_rock_mast;

                }
                if (name == "cart mini mast blood")
                {
                    infa = "+1 к мощности кровавой карты| |+1 к скорости применения кровавой карты| |+1 к силе применения кровавой карты| |карта может быть улучшена";
                    type = type_cart.art_blood_mast;

                }


                if (name == "cart mini mast att")
                {
                    infa = "+1 к мощности боевой карты(пассивный)| |+1 к скорости применения боевой карты(пассивный)| |+1 к силе применения боевой карты(пассивный)";
                    type = type_cart.art_att_mast;
                }


                if (name == "cart mini mast bar")
                {
                    infa = "+1 к мощности барьерной карты(пассивный)| |+1 к скорости применения барьерной карты(пассивный)| |+1 к силе применения барьерной карты(пассивный)";
                    type = type_cart.art_bar_mast;
                }
                if (name == "cart mini mast dych")
                {
                    infa = "+1 к мощности карты некроманта(пассивный)| |+1 к скорости применения карты некроманта(пассивный)| |+1 к силе применения карты некроманта(пассивный)";
                    type = type_cart.art_dych_mast;

                }
                if (name == "cart mini mast bomb")
                {
                    infa = "+1 к мощности взрывной карты(пассивный)| |+1 к скорости применения взрывной карты(пассивный)| |+1 к силе применения взрывной карты(пассивный)";
                    type = type_cart.art_bomb_mast;

                }


                if (name == "cart mast fire")
                {
                    infa = "сохраняет огненные карты применении| |+1 к мощности огненной карты(пассивный)| |+1 к скорости применения огненной карты(пассивный)| |+1 к силе применения огненной карты(пассивный)| |(перезарядка происходит только при применении огненных карт)";
                    type = type_cart.art_fire_mast;

                }
                if (name == "cart mast poition")
                {
                    infa = "сохраняет ядовитые карты при применении| |+1 к мощности ядовитой карты(пассивный)| |+1 к скорости применения ядовитой карты(пассивный)| |+1 к силе применения ядовитой карты(пассивный)| |(перезарядка происходит только при применении ядовитых карт)";
                    type = type_cart.art_poition_mast;

                }
                if (name == "cart mast ice")
                {
                    infa = "сохраняет ледяные карты при применении| |+1 к мощности ледяной карты(пассивный)| |+1 к скорости применения ледяной карты(пассивный)| |+1 к силе применения ледяной карты(пассивный)| |(перезарядка происходит только при применении ледяных карт)";
                    type = type_cart.art_ice_mast;

                }
                if (name == "cart mast energy")
                {
                    infa = "сохраняет электрические карты при применении| |+1 к мощности электрической карты(пассивный)| |+1 к скорости применения электрической карты(пассивный)| |+1 к силе применения электрической карты(пассивный)| |(перезарядка происходит только при применении электрических карт)";
                    type = type_cart.art_energy_mast;

                }
                if (name == "cart mast rock")
                {
                    infa = "сохраняет каменные карты при применении| |+1 к мощности каменной карты(пассивный)| |+1 к скорости применения каменной карты(пассивный)| |+1 к силе применения каменной карты(пассивный)| |(перезарядка происходит только при применении каменных карт)";
                    type = type_cart.art_rock_mast;

                }
                if (name == "cart mast blood")
                {
                    infa = "сохраняет кровавых карты при применении| |+1 к мощности кровавой карты(пассивный)| |+1 к скорости применения кровавой карты(пассивный)| |+1 к силе применения кровавой карты(пассивный)| |(перезарядка происходит только при применении кровавой карт)";
                    type = type_cart.art_blood_mast;

                }

                if (name == "cart mast att")
                {
                    infa = "сохраняет боевые карты при применении| |+1 к мощности боевой карты(пассивный)| |+1 к скорости применения боевой карты(пассивный)| |+1 к силе применения боевой карты(пассивный)";
                    type = type_cart.art_att_mast;
                }


                if (name == "cart mast bar")
                {
                    infa = "сохраняет барьерные карты при применении| |+1 к мощности барьерной карты(пассивный)| |+1 к скорости применения барьерной карты(пассивный)| |+1 к силе применения барьерной карты(пассивный)";
                    type = type_cart.art_bar_mast;
                }
                if (name == "cart mast dych")
                {
                    infa = "сохраняет карты некроманта при применении| |+1 к мощности карты некроманта(пассивный)| |+1 к скорости применения карты некроманта(пассивный)| |+1 к силе применения карты некроманта(пассивный)";
                    type = type_cart.art_dych_mast;

                }
                if (name == "cart mast bomb")
                {
                    infa = "сохраняет взрывные карты при применении| |+1 к мощности взрывной карты(пассивный)| |+1 к скорости применения взрывной карты(пассивный)| |+1 к силе применения взрывной карты(пассивный)";
                    type = type_cart.art_bomb_mast;

                }





                if (name == "cart derlorg")
                {
                    infa = "карта с проклятой силой| |при применении на живом существе создаёт проклятье на 4 хода| |проклятье не позволяет применять карты| |позволяет комбинировать с другими картами для создания определённых проклятий";
                    type = type_cart.derlorg;
                }

                if (name == "cart sp derlorg")
                {
                    infa = "создаёт карту дерлорга мощности 1 и добавляет в инвентарь| |+2 к месту в инвентаре для карты дерлорга мощности 1(пассивный)| |-2 места в инвентаре для обычных карт и -1 место в инвентаре для специальных карт";
                    type = type_cart.art_derlorg_sp;
                }
                if (name == "cart derlorg act")
                {
                    infa = "+1 к силе применения карты| |-2 места в инвентаре для обычных карт и -1 место в инвентаре для специальных карт";
                    type = type_cart.art_seal_sila;
                }
                if (name == "cart derlorg speed")
                {
                    infa = "+1 к скорости применения карты| |-2 места в инвентаре для обычных карт и -1 место в инвентаре для специальных карт";
                    type = type_cart.art_seal_speed;
                }
                if (name == "cart derlorg potoc")
                {
                    infa = "дополнительно поглощает полученный магический эффект в виде карт дерлорга| |+1 к мощности карты дерлорга за каждые 6 единицы мощности карт дерлорга в инвентаре| |-2 места в инвентаре для обычных карт и -1 место в инвентаре для специальных карт";
                    type = type_cart.art_seal_potoc;
                }

                if (name == "cart derlorg pow")
                {
                    infa = "+1 к мощности применения всех карт| |-2 места в инвентаре для обычных карт и -1 место в инвентаре для специальных карт";
                    type = type_cart.art_seal_pow;
                }

                if (name == "cart derlorg att")
                {
                    infa = "при применении карты атаки дополнительно создаёт проклятие от применённых карт| |-4 места в инвентаре для карты атаки";
                    type = type_cart.art_seal_att;
                }
                if (name == "cart derlorg bar")
                {
                    infa = "при применении карты барьера дополнительно создаёт проклятие от применённых карт| |-2 места в инвентаре для карты барьера";
                    type = type_cart.art_seal_bar;
                }
                if (name == "cart derlorg bomb")
                {
                    infa = "при применении карты взрыва дополнительно создаёт проклятие от применённых карт| |-2 места в инвентаре для карты взрыва";
                    type = type_cart.art_seal_bomb;
                }
                if (name == "cart derlorg fire")
                {
                    infa = "при применении огненной карты дополнительно создаёт проклятие от применённых карт| |-4 места в инвентаре для огненной карты";
                    type = type_cart.art_seal_fire;
                }
                if (name == "cart derlorg poition")
                {
                    infa = "при применении ядовитой карты дополнительно создаёт проклятие от применённых карт| |-4 места в инвентаре для ядовитой карты";
                    type = type_cart.art_seal_poition;
                }
                if (name == "cart derlorg ice")
                {
                    infa = "при применении ледяной карты дополнительно создаёт проклятие от применённых карт| |-4 места в инвентаре для ледяной карты";
                    type = type_cart.art_seal_ice;
                }
                if (name == "cart derlorg energy")
                {
                    infa = "при применении электрической карты дополнительно создаёт проклятие от применённых карт| |-4 места в инвентаре для электрической карты";
                    type = type_cart.art_seal_ice;
                }
                if (name == "cart derlorg rock")
                {
                    infa = "при применении каменной карты дополнительно создаёт проклятие от применённых карт| |-4 места в инвентаре для каменной карты";
                    type = type_cart.art_seal_rock;
                }
                if (name == "cart derlorg blood")
                {
                    infa = "при применении кровавой карты дополнительно создаёт проклятие от применённых карт| |-4 места в инвентаре для кровавой карты";
                    type = type_cart.art_seal_blood;
                }

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
    
