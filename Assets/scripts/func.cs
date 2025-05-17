using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static par_mob;


public class func : MonoBehaviour
{
    public enum type_lvl
    {
        bases, bases_1, fire, poition, energy, ice, rock, blood, derlorg, spider
    }

    //public Dictionary<string,GameObject> baza = new Dictionary<string ,GameObject>();
    // Start is called before the first frame update
    //public Texture2D im;
    public static string test_message = "good";



    public static float sz;
    public static int num_lev = 0;
    public static int num_lev_type = 0;
    public static type_lvl type_lvl_now = type_lvl.bases;

    public static int size_global = 30;

    public static Dictionary<string, GameObject> baza = new Dictionary<string, GameObject>();

    #region alg
    public static void destroy_childs(Transform tr)
    {
        Stack<GameObject> st = new Stack<GameObject>();
        foreach (Transform tr_base in tr)
        {
            st.Push(tr_base.gameObject);
        }
        while (st.Count > 0)
        {
            Destroy(st.Pop());
        }
    }
    public static T rand_list<T>(List<T> lt)
    {
        if (lt.Count > 0)
            return lt[Random.Range(0, lt.Count)];
        else
            return default(T);
    }
    public static GameObject rand_list(List<GameObject> lt)
    {
        return create(lt[Random.Range(0, lt.Count)].name, -1, -1);
    }
    public static int distance(int x1,int y1,int x2,int y2)
    {
        return distance(new Vector2(x1,y1),new Vector2(x2,y2));
    }
    public static int distance(Vector2 v1 ,Vector2 v2)
    {
        int xd = (int)Mathf.Abs((int)v1.x - (int)v2.x);
        int yd = (int)Mathf.Abs((int)v1.y - (int)v2.y);
        return xd + yd;
    }
    public static int[,] recurs(int[,] a, int j, int i)
    {
        a[j, i] = -1;
        //if(i==0)
        //Debug.Log("recs " + j + " " + i);
        if (i == size_global - 1 && j == size_global - 1)
            return a;
        else if (j == size_global - 1)
            return recurs(a, 0, i + 1);
        else
            return recurs(a, j + 1, i);

    }
    public static bool[,] recurs(bool[,] a, int j, int i)
    {
        a[j, i] = false;
        if (i == size_global - 1 && j == size_global - 1)
            return a;
        else if (j == size_global - 1)
            return recurs(a, 0, i + 1);
        else
            return recurs(a, j + 1, i);

    }
    public static int[,] way(int x_st, int y_st, int way_col)
    {
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        return a;
    }
    public static int[,] way_bool(bool[,] bl, int x_st, int y_st, int way_col)
    {
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && bl[j, i] && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        return a;
    }
    public static int[,] way_bool_and(bool[,] bl, int x_st, int y_st, int way_col)
    {
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && ((j > x_st_1 && a[j - 1, i] == q - 1 && bl[j - 1, i]) || (i > y_st_1 && a[j, i - 1] == q - 1 && bl[j, i - 1]) || (j < x_end_1 && a[j + 1, i] == q - 1 && bl[j + 1, i]) || (i < y_end_1 && a[j, i + 1] == q - 1 && bl[j, i + 1])))
                        a[j, i] = q;
                }
            }
        }
        return a;
    }
    public static int[,] way_find(object[,] bl, object obj, int x_st, int y_st, int way_col)
    {
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        for (int i = y_st_1; i <= y_end_1; i++)
        {
            for (int j = x_st_1; j <= x_end_1; j++)
            {
                if (!(a[j, i] != -1 && !bl[j, i].Equals(obj)))
                    a[j, i] = -1;
            }
        }


        return a;
    }
    public static int[,] way_find_gm(GameObject[,] bl, GameObject obj, int x_st, int y_st, int way_col)
    {
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        for (int i = y_st_1; i <= y_end_1; i++)
        {
            for (int j = x_st_1; j <= x_end_1; j++)
            {
                if (!(a[j, i] != -1 && !bl[j, i].name.Equals(obj.name)))
                    a[j, i] = -1;
            }
        }


        return a;
    }
    public static List<object> way_find_lt(object[,] bl, int x_st, int y_st, int way_col)
    {
        List<object> lt = new List<object>();
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        for (int i = y_st_1; i <= y_end_1; i++)
        {
            for (int j = x_st_1; j <= x_end_1; j++)
            {
                if (a[j, i] != -1 && bl[j, i] != null)
                    lt.Add(bl[j, i]);
            }
        }


        return lt;
    }

    public static List<object> way_find_lt(GameObject[,] bl, GameObject obj, int x_st, int y_st, int way_col)
    {
        List<object> lt = new List<object>();
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        for (int i = y_st_1; i <= y_end_1; i++)
        {
            for (int j = x_st_1; j <= x_end_1; j++)
            {
                if (a[j, i] != -1 && bl[j, i] != null && obj.name == bl[j, i].name)
                    lt.Add(bl[j, i]);
            }
        }


        return lt;
    }

    public static List<GameObject> way_find_lt_in_list(List<GameObject>[,] bl, string name, int x_st, int y_st, int way_col)
    {
        List<GameObject> lt = new List<GameObject>();
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;
        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && q <= way_col && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        for (int i = y_st_1; i <= y_end_1; i++)
        {
            for (int j = x_st_1; j <= x_end_1; j++)
            {
                if (a[j, i] != -1 && bl[j, i] != null)
                {
                    foreach (GameObject gm1 in bl[j, i])
                    {
                        if (name == gm1.name)
                        {
                            lt.Add(gm1);
                        }
                    }
                    
                }
                    
            }
        }


        return lt;
    }

    public static Stack<Vector2> way_wolk(int x_st, int y_st, int x_end, int y_end)
    {
        Stack<Vector2> lt = new Stack<Vector2>();
        int[,] a = new int[size_global, size_global];
        a = recurs(a, 0, 0);

        int way_col = Mathf.Abs(x_end - x_st) + Mathf.Abs(y_end - y_st);
        int x_st_1 = x_st - way_col;
        int y_st_1 = y_st - way_col;
        int x_end_1 = x_st + way_col;
        int y_end_1 = y_st + way_col;
        if (x_st_1 < 0)
            x_st_1 = 0;
        if (y_st_1 < 0)
            y_st_1 = 0;
        if (x_end_1 > size_global - 1)
            x_end_1 = size_global - 1;
        if (y_end_1 > size_global - 1)
            y_end_1 = size_global - 1;
        a[x_st, y_st] = 0;

        for (int q = 1; q < (way_col * 2 + 1) * (way_col * 2 + 1); q++)
        {
            for (int i = y_st_1; i <= y_end_1; i++)
            {
                for (int j = x_st_1; j <= x_end_1; j++)
                {
                    if (a[j, i] == -1 && object_bool(j, i) && ((j > x_st_1 && a[j - 1, i] == q - 1) || (i > y_st_1 && a[j, i - 1] == q - 1) || (j < x_end_1 && a[j + 1, i] == q - 1) || (i < y_end_1 && a[j, i + 1] == q - 1)))
                        a[j, i] = q;
                }
            }
        }
        int aa = way_col;
        int xx = x_end;
        int yy = y_end;
        lt.Push(new Vector2(xx, yy));
        for (int i = (way_col * 2 + 1) * (way_col * 2 + 1); i > 0; i--)
        {
            int x_ = x_end;
            int y_ = y_end;
            if (a[x_end - 1, y_end] < a[x_end, y_end] && a[x_end - 1, y_end] != -1)
                x_end--;
            else if (a[x_end + 1, y_end] < a[x_end, y_end] && a[x_end + 1, y_end] != -1)
                x_end++;
            else if (a[x_end, y_end - 1] < a[x_end, y_end] && a[x_end, y_end - 1] != -1)
                y_end--;
            else if (a[x_end, y_end + 1] < a[x_end, y_end] && a[x_end, y_end + 1] != -1)
                y_end++;
            if (!(x_end == x_st && y_end == y_st) && !(x_end == x_ && y_end == y_))
                lt.Push(new Vector2(x_end, y_end));
            else if (x_end == x_ && y_end == y_)
            {
                return new Stack<Vector2>();
            }
            else if (x_end == x_st && y_end == y_st)
                return lt;
        }


        return lt;
    }

    public static bool object_bool(int x, int y)
    {
        //Debug.Log((nature[x, y] == null));
        bool bl = ((nature[x, y] != null && nature[x, y].GetComponent<par_prise>() != null) || (mob[x, y] != null && mob[x, y].GetComponent<par_prise>() != null) || (eath[x, y] != null && eath[x, y].GetComponent<par_prise>() != null) || (carts[x, y] != null && carts[x, y].GetComponent<par_prise>() != null));

        return (nature[x, y] == null) && (mob[x, y] == null) && !bl;
    }
    #endregion







    public static void click(GameObject gm_click)
    {
        par_mob mb_click = gm_click.GetComponent<par_mob>();

        par_mob mb = mob_list[hod_mob_now_int].GetComponent<par_mob>();


        //Debug.Break();

        int x_cl = -1;
        int y_cl = -1;
        if (gm_click.GetComponent<par_eath>() != null)
        {
            x_cl = gm_click.GetComponent<par_eath>().x;
            y_cl = gm_click.GetComponent<par_eath>().y;
        }
        if (gm_click.GetComponent<par_nat>() != null)
        {
            x_cl = gm_click.GetComponent<par_nat>().x;
            y_cl = gm_click.GetComponent<par_nat>().y;
        }
        if (gm_click.GetComponent<par_mob>() != null)
        {
            x_cl = gm_click.GetComponent<par_mob>().x;
            y_cl = gm_click.GetComponent<par_mob>().y;
        }
        if (ui_move.bl_down)
        {

            if (anim.col_anim == 0 && mb.friend && stay_friend_wolk_now())
            {
                bool bl_r_att = false;
                if (mb.lt_carts_active_now.Count > 0)
                {
                    par_cart.type_cart dych = par_cart.type_cart.bases;
                    foreach (string cart_st in mb.lt_carts_active_now)
                    {
                        par_cart.type_cart type_cart = GameObject.Find(cart_st).GetComponent<par_cart>().type;
                        if (type_cart == par_cart.type_cart.fire || type_cart == par_cart.type_cart.blood || type_cart == par_cart.type_cart.poition || type_cart == par_cart.type_cart.ice || type_cart == par_cart.type_cart.rock || type_cart == par_cart.type_cart.energy)
                        {
                            dych = type_cart;
                            break;
                        }
                    }


                    foreach (GameObject gm_ch in mb.childs)
                    {
                        par_mob mb_ch = gm_ch.GetComponent<par_mob>();
                        if ((gm_ch.name == "dych fire" && dych == par_cart.type_cart.fire) || (gm_ch.name == "dych blood" && dych == par_cart.type_cart.blood) || (gm_ch.name == "dych energy" && dych == par_cart.type_cart.energy) || (gm_ch.name == "dych ice" && dych == par_cart.type_cart.ice) || (gm_ch.name == "dych rock" && dych == par_cart.type_cart.rock) || (gm_ch.name == "dych poition" && dych == par_cart.type_cart.poition))
                        {
                            int cx = Mathf.Abs(x_cl - mb_ch.x);
                            int cy = Mathf.Abs(y_cl - mb_ch.y);
                            if (cx + cy <= mb_ch.distanse_attack)
                            {
                                bl_r_att = true;
                                break;
                            }

                        }
                    }
                }

                int col_way = way(mb.x, mb.y, mb.distanse_attack)[x_cl, y_cl];
                ui_atack.atack_comb at = null;
                if (hod_mob_now_int == 0 && ui_atack.lt_cart_comb_selected.Count > 0 && (col_way != -1 || bl_r_att))
                {
                    foreach (ui_atack.atack_comb comb in ui_atack.lt_cart_comb_selected)
                    {
                        Debug.Log("auto comb " + comb.atack_type + "  " + (gm_click.GetComponent<par_mob>() != null));

                        if (comb.atack_type == ui_atack.type_atack.bases)
                            at = comb;
                        else if (gm_click.GetComponent<par_mob>() != null)
                        {
                            par_mob pr_mb = gm_click.GetComponent<par_mob>();
                            Debug.Log("auto comb 0 " + pr_mb.friend);
                            if (pr_mb.friend && comb.atack_type == ui_atack.type_atack.friend)
                                at = comb;
                            if (!pr_mb.friend && comb.atack_type == ui_atack.type_atack.mob)
                            {
                                at = comb;
                                Debug.Log("auto comb true");
                            }

                            if (comb.atack_type == ui_atack.type_atack.mob_friend)
                                at = comb;
                        }
                        else if (gm_click.GetComponent<par_eath>() != null)
                        {
                            if (comb.atack_type == ui_atack.type_atack.eath)
                                at = comb;
                        }
                        if (at != null)
                            break;

                    }
                }
                Debug.Log("auto comb 1 " + at);
                if (at != null)
                {
                    bool bl = true;
                    List<string> lt_st = new List<string>(mb.lt_carts);
                    foreach (string st in at.carts)
                    {
                        if (lt_st.Contains(st))
                            lt_st.Remove(st);
                        else
                        {
                            bl = false;
                            break;
                        }
                    }

                    if (bl && mb.proverka_atack(new List<string>(at.carts), true, true))
                    {
                        if (x_cl > mb.x)
                            mb.simm(1);
                        else if (x_cl < mb.x)
                            mb.simm(-1);

                        string type;
                        //Stack<string> stack = new Stack<string>(at.carts);
                        //while (stack.Count != 1)
                        //{
                        //stack.Pop();
                        //}
                        Queue<string> new_q = new Queue<string>(at.carts);
                        type = new_q.Dequeue();

                        mb.cel = gm_click;
                        mb.gameObject.GetComponent<Animator>().SetBool("atack", true);
                        if (!mb.gameObject.GetComponent<Animator>().GetBool("selected"))
                            mb.gameObject.GetComponent<Animator>().SetBool("selected", true);
                        else
                        {
                            mb.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                        }
                        anim.col_anim++;
                        mb.cart_active_now = type;
                        mb.lt_carts_active_now.Add(type);
                        mb.lt_carts_added_now.Add(type);
                        foreach (string st in new_q)
                        {
                            mb.lt_carts_added.Add(st);
                        }

                        //pr_mob.lt_carts_added.Add(type);


                        mb.remove_cart(type);

                        mb.atack_auto_comb = true;
                    }

                    //atact = true;
                    //next_hod();
                }

                else if (gm_click.GetComponent<par_nat>() != null && hod_mob_now_int == 0 && stay_friend_atack_now())
                {
                    par_mob pl = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                    if ((gm_click.name == "cave" || gm_click.name == "cave 1" || gm_click.name == "cave derlorg") && way(x_cl, y_cl, 1)[pl.x, pl.y] != -1)
                    {
                        type_lvl type_lvl_base = type_lvl_now;
                        if (gm_click.name == "cave derlorg")
                        {
                            type_lvl_now = type_lvl.derlorg;
                        }
                        else
                        {
                            if (num_lev < 5)
                                type_lvl_now = type_lvl.bases;
                            else
                                type_lvl_now = type_lvl.bases_1;
                        }
                        if (type_lvl_now != type_lvl_base)
                        {
                            GameObject.Find("func").GetComponent<AudioSource>().clip = music_lvl[type_lvl_now];
                            GameObject.Find("func").GetComponent<AudioSource>().Play();
                        }

                        generate_room();

                    }


                }
                else if (gm_click.GetComponent<par_prise>() != null)
                {
                    if (way(mb.x, mb.y, 1)[x_cl, y_cl] != -1)
                    {
                        if (gm_click.GetComponent<par_prise>().can_buy(mb.lt_carts))
                        {
                            gm_click.GetComponent<par_prise>().buy(mb);
                            //Debug.Log("del canvas");
                            //gm_click.transform.Find("Canvas price").gameObjectactive = false;
                            Destroy(gm_click.transform.Find("Canvas price").gameObject);
                            //mb_click.nps = false;
                            //mb_click.friend = true;
                            //Destroy(gm_click.transform.Find("Canvas hp").Find("pn prise").gameObject);
                        }
                    }

                }
                //Debug.Log(mb.lt_carts_active_now.Count+"  "+ mb.lt_carts_active_now[0]);
                else if (gm_click.GetComponent<par_eath>() != null && stay_friend_atack_now())
                {
                    //Debug.Break();
                    //par_mob pl = GameObject.Find("player").GetComponent<par_mob>();
                    par_mob pl = mb;

                    if (way(pl.x, pl.y, 1)[x_cl, y_cl] == 1)
                    {
                        pl.wolk(x_cl, y_cl);
                        next_hod();
                    }
                    else
                    {
                        //anim.col_anim++;

                        pl.lt_wolk = func.way_wolk(pl.x, pl.y, x_cl, y_cl);
                        //if (pl.lt_wolk.Count != 0)
                        if (pl.lt_wolk.Count != 0)
                        {
                            foreach (Vector2 v2 in pl.lt_wolk)
                            {
                                ui.create_pricel_wolk(v2);
                            }
                            pl.wolk_v2();
                            next_hod();
                        }

                        //}

                    }
                }






                else if (mb_click != null && mb_click.friend && hod_mob_now_int == mb_click.num && stay_friend_atack_now())
                {

                    mb_click.wolk(x_cl, y_cl);
                    next_hod();
                }

                else if (mb_click != null && gm_click.name == "nps derlorg" && way(mb.x, mb.y, 1)[x_cl, y_cl] != -1 && mb_click.lt_par.Count != 0)
                {
                    Debug.Log("derlorg nps");
                    Vector2 v2_par = (Vector2)gm_click.GetComponent<par_mob>().lt_par[0];
                    int x_par = (int)v2_par.x;
                    int y_par = (int)v2_par.y;
                    if (object_bool(x_par, y_par))
                    {
                        anim.col_anim++;
                        gm_click.GetComponent<Animator>().SetBool("act", true);
                    }
                }
                else if (way(mb.x, mb.y, mb.distanse_attack)[x_cl, y_cl] != -1 || bl_r_att)
                {
                    //Debug.Break();
                    if (x_cl > mb.x)
                        mb.simm(1);
                    else if (x_cl < mb.x)
                        mb.simm(-1);


                    if (mb.lt_carts_active_now.Count > 0)
                    {

                        List<string> lt1 = new List<string>(mb.lt_carts_active_now);
                        if (mb.atack_mob(gm_click))
                        {
                            anim.col_anim++;
                            mb.proverka_atack(new List<string>(lt1), true, true);
                        }

                    }



                }
            }
            else
            {
                par_mob pl = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                foreach (Vector2 v2 in pl.lt_wolk)
                {
                    ui.del_pricels(v2);
                }
                pl.lt_wolk.Clear();
                //ui.clean_pricels();
            }
        }




    }


    #region generate

    public static bool drop_bool(int x, int y)
    {
        //par_mob player = GameObject.Find("player").GetComponent<par_mob>();
        int x_min = -1;
        int y_min = -1;
        for (int i = y - 2; i < y + 2; i++)
        {
            if (i >= 0 && i < size_global)
            {
                for (int j = x - 2; j < x + 2; j++)
                {
                    if (j >= 0 && j < size_global)
                    {
                        int c = Mathf.Abs(x - j) + Mathf.Abs(y - i);
                        int c1 = -1;
                        if (x_min != -1 && y_min != -1)
                        {
                            c1 = Mathf.Abs(x - x_min) + Mathf.Abs(y - y_min);
                        }
                        if (carts[j, i] == null && nature[j, i] == null && (c < c1 || c1 == -1))
                        {
                            x_min = j;
                            y_min = i;
                        }
                    }
                }
            }
        }
        bool gm1 = false;
        if (x_min != -1 && y_min != -1)
        {
            gm1 = true;
        }

        return gm1;
        //gm1.GetComponent<par_eq>().col = col;
    }
    public static GameObject drop(string gm, int x, int y)
    {
        //par_mob player = GameObject.Find("player").GetComponent<par_mob>();
        int x_min = -1;
        int y_min = -1;
        for (int i = y - 2; i < y + 2; i++)
        {
            if (i >= 0 && i < size_global)
            {
                for (int j = x - 2; j < x + 2; j++)
                {
                    if (j >= 0 && j < size_global)
                    {
                        int c = Mathf.Abs(x - j) + Mathf.Abs(y - i);
                        int c1 = -1;
                        if (x_min != -1 && y_min != -1)
                        {
                            c1 = Mathf.Abs(x - x_min) + Mathf.Abs(y - y_min);
                        }
                        if (carts[j, i] == null && nature[j, i] == null && (c < c1 || c1 == -1))
                        {
                            x_min = j;
                            y_min = i;
                        }
                    }
                }
            }
        }
        GameObject gm1 = null;
        if (x_min != -1 && y_min != -1)
        {
            gm1 = create(gm, x_min, y_min);
        }

        return gm1;
        //gm1.GetComponent<par_eq>().col = col;
    }
    public static GameObject create(string gm)
    {
        return create(gm, -1, -1);
    }
    public static GameObject create_cart(int x, int y, par_cart.type_cart type)
    {
        int a1 = num_lev + Random.Range(0, 100);
        string st_lvl = "";

        /*
         
        эт потом
         
        if (a1 < 50)
            lvl = "";
        else if (lvl < 80)
            lvl = " 1";
        else
            lvl = " 2";
        */
        if (a1 < 100)
            st_lvl = "";
        else
            st_lvl = " 1";

        string gm = null;
        List<string> lt_rd = new List<string>();
        if (type == par_cart.type_cart.bases)
        {
            lt_rd.Add("cart damage" + st_lvl);
            //lt_rd.Add("cart barier");
        }
        else if (type == par_cart.type_cart.sp_barier)
        {
            lt_rd.Add("cart barier" + st_lvl);
            //lt_rd.Add("cart barier");
        }
        else if (type == par_cart.type_cart.sp_crystall)
        {
            lt_rd.Add("cart crystall" + st_lvl);
            //lt_rd.Add("cart barier");
        }
        else if (type == par_cart.type_cart.rock)
        {

            lt_rd.Add("cart rock" + st_lvl);
            // a1 = num_lev + num_lev_type * 4 - Random.Range(0, 15);
            //if (a1 / 4 < 0)
            // gm = "cart rock";
            //else
            //gm = "cart rock 1";
        }
        else if (type == par_cart.type_cart.blood)
        {
            lt_rd.Add("cart blood" + st_lvl);
            //gm = "cart blood";
        }
        else if (type == par_cart.type_cart.fire)
        {
            lt_rd.Add("cart fire" + st_lvl);
            //gm = "cart fire";

        }
        else if (type == par_cart.type_cart.energy)
        {
            lt_rd.Add("cart energy" + st_lvl);
            //gm = "cart energy";
        }
        else if (type == par_cart.type_cart.ice)
        {
            lt_rd.Add("cart ice" + st_lvl);
            //gm = "cart ice";
        }
        else if (type == par_cart.type_cart.poition)
        {
            lt_rd.Add("cart poition" + st_lvl);
            //gm = "cart energy";
        }
        else if (type == par_cart.type_cart.derlorg)
        {
            lt_rd.Add("cart derlorg");
            //gm = "cart energy";
        }
        if (type == par_cart.type_cart.gold)
        {
            lt_rd.Add("cart gold");
            //lt_rd.Add("cart barier");
        }

        gm = lt_rd[Random.Range(0, lt_rd.Count)];
        return create(gm, x, y);
    }
    public static GameObject create_cart_art(int x, int y, par_cart.type_cart type)
    {
        string gm = null;
        List<string> lt_rd = new List<string>();

        //int a1 = (num_lev + 1) / 5 + Random.Range(0, 21);
        int a1 = Random.Range(0, 100);
        int num_lev = 0;
        
        string st_lvl = "";

        /*
         
        эт потом
         
        if (a1 < 50)
            lvl = "";
        else if (lvl < 80)
            lvl = " 1";
        else
            lvl = " 2";
        */
        if (a1 < 75)
        {
            num_lev = 1;
            st_lvl = "";
        }
            
        else if(a1 < 95)
        {
            st_lvl = " 1";
            num_lev = 2;
        }

        else
        {
            st_lvl = " 2";
            num_lev = 4;
        }
            
        if (type == par_cart.type_cart.bases)
        {
            //int a_rd = Random.Range(0, 2);
            //for (int i = 0; i < 7; i++)
            //{
            lt_rd.Add("cart sp atack" + st_lvl);
            lt_rd.Add("cart att pow" + st_lvl);
            /*
            if (Random.Range(0, 8) != 0)
            {
                if (Random.Range(0, 2) == 0)
                    gm = "cart sp atack";
                else
                    gm = "cart sp barier";
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                    gm = "cart att pow";
                else
                    gm = "cart bar pow";
            }
             */

        }
        else if (type == par_cart.type_cart.sp_barier)
        {

            lt_rd.Add("cart sp barier" + st_lvl);
            lt_rd.Add("cart bar pow" + st_lvl);

        }
        else if (type == par_cart.type_cart.sp_crystall)
        {

            lt_rd.Add("cart sp crystall" + st_lvl);

        }
        else if (type == par_cart.type_cart.rock)
        {
            //int a_rd = Random.Range(0, 2);
            //for (int i = 0; i < 7; i++)
            //{
                lt_rd.Add("cart sp rock" + st_lvl);
                lt_rd.Add("cart act rock" + st_lvl);
                lt_rd.Add("cart speed rock" + st_lvl);
                lt_rd.Add("cart rock pow" + st_lvl);
            lt_rd.Add("cart rock cuzna" + st_lvl);
            lt_rd.Add("cart rock crystall" + st_lvl);
            //}
            if (num_lev == 2)
            {
                lt_rd.Add("cart potoc rock");
                lt_rd.Add("cart rock touch");
                lt_rd.Add("cart rock heart");
                lt_rd.Add("cart potoc rock");
                
            }
                


        }
        else if (type == par_cart.type_cart.blood)
        {
            for (int i = 0; i < 7; i++)
            {
                lt_rd.Add("cart sp blood" + st_lvl);
                lt_rd.Add("cart act blood" + st_lvl);
                lt_rd.Add("cart speed blood" + st_lvl);
                lt_rd.Add("cart blood pow" + st_lvl);
            }
            lt_rd.Add("cart potoc blood");
        }
        else if (type == par_cart.type_cart.fire)
        {
            for (int i = 0; i < 7; i++)
            {
                lt_rd.Add("cart sp fire" + st_lvl);
                lt_rd.Add("cart act fire" + st_lvl);
                lt_rd.Add("cart speed fire" + st_lvl);
                lt_rd.Add("cart fire pow" + st_lvl);
            }
            lt_rd.Add("cart potoc fire");

        }
        else if (type == par_cart.type_cart.energy)
        {
            for (int i = 0; i < 7; i++)
            {
                lt_rd.Add("cart sp energy" + st_lvl);
                lt_rd.Add("cart act energy" + st_lvl);
                lt_rd.Add("cart speed energy" + st_lvl);
                lt_rd.Add("cart energy pow" + st_lvl);
            }
            lt_rd.Add("cart potoc energy");

        }
        else if (type == par_cart.type_cart.ice)
        {

            for (int i = 0; i < 7; i++)
            {
                lt_rd.Add("cart sp ice" + st_lvl);
                lt_rd.Add("cart act ice" + st_lvl);
                lt_rd.Add("cart speed ice" + st_lvl);
                lt_rd.Add("cart ice pow" + st_lvl);
            }
            lt_rd.Add("cart potoc ice");

        }
        else if (type == par_cart.type_cart.poition)
        {
            for (int i = 0; i < 7; i++)
            {
                lt_rd.Add("cart sp poition" + st_lvl);
                lt_rd.Add("cart act poition" + st_lvl);
                lt_rd.Add("cart speed poition" + st_lvl);
                lt_rd.Add("cart poition pow" + st_lvl);
            }
            lt_rd.Add("cart potoc poition");
        }

        gm = lt_rd[Random.Range(0, lt_rd.Count)];
        return create(gm, x, y);
    }
    public static GameObject create_cart(int x, int y)
    {
        string gm = null;
        par_cart.type_cart type_cart = par_cart.type_cart.bases;

        //a = 14;
        int a = -1;
        if (Random.Range(0, 8) == 0)
        {
            type_cart = par_cart.type_cart.gold;
        }
        else
        {
            if (Random.Range(0, 4) == 0)
            {
                a = Random.Range(0, 2);
                if (a == 0)
                    type_cart = par_cart.type_cart.sp_barier;
                else
                    type_cart = par_cart.type_cart.sp_crystall;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                    type_cart = par_cart.type_cart.bases;
                else
                {
                    a = Random.Range(0, 6);
                    if (a == 0)
                        type_cart = par_cart.type_cart.fire;
                    else if (a == 1)
                        type_cart = par_cart.type_cart.blood;
                    else if (a == 2)
                        type_cart = par_cart.type_cart.poition;
                    else if (a == 3)
                        type_cart = par_cart.type_cart.energy;
                    else if (a == 4)
                        type_cart = par_cart.type_cart.ice;
                    else if (a == 5)
                        type_cart = par_cart.type_cart.rock;
                }

            }
        }

        if (Random.Range(0, 20) == 0 && type_cart != par_cart.type_cart.gold)
            return create_cart_art(x, y, type_cart);
        else
            return create_cart(x, y, type_cart);
        /*
        if (Random.Range(0, 3) == 0 &&false)
        {
            gm = "cart talon";
        }
        else
        {
            if (Random.Range(0, 3) == 0)
            {
                if(Random.Range(0, 2)==0)
                    gm = "cart damage";
                else
                    gm = "cart barier";
            }
            else if (Random.Range(0, 3) == 0 && a==18)
            {
                return create_cart_art(x, y);

            }
            else
            {
                int aa = a / 3;
                a1 = -4;
                if (aa == 0)
                {
                    if (a1 / 4 < 0 ||true)
                        gm = "cart rock";
                    else
                        gm = "cart rock 1";
                }
                else if (aa == 1)
                {
                    gm = "cart blood";
                }
                else if (aa == 2)
                    gm = "cart fire";
                else if (aa == 3)
                    gm = "cart ice";
                else if (aa == 4)
                    gm = "cart poition";
                else
                    gm = "cart energy";

            }

        }
        */

    }
    public static GameObject create_cart_art(int x, int y)
    {
        string gm = null;
        int a1 = num_lev + num_lev_type * 4 - Random.Range(0, 15);
        int a = Random.Range(0, 20);

        bool bl_leg = true;

        int var = Random.Range(0, 100);
        if (var >=95)
        {
            bl_leg = false;
            List<string> lt_rd = new List<string>();
            if (GameObject.Find("cart speed").GetComponent<par_cart_art>().open)
                lt_rd.Add("cart speed");
            if (!GameObject.Find("cart power").GetComponent<par_cart_art>().open)
                lt_rd.Add("cart power");
            if (GameObject.Find("cart respawn").GetComponent<par_cart_art>().open)
                lt_rd.Add("cart respawn");
            if (lt_rd.Count > 0)
                gm = rand_list<string>(lt_rd);
            else
                bl_leg = true;
        }
        else if (var >= 75)
        {
            bl_leg = false;
            List<string> lt_rd = new List<string>();
            if (GameObject.Find("cart inven").GetComponent<par_cart_art>().open)
                lt_rd.Add("cart inven");
            lt_rd.Add("cart mast");
            lt_rd.Add("cart elemental");
            lt_rd.Add("cart universal");
            gm = rand_list<string>(lt_rd);
        }
        if (bl_leg)
        {
            par_cart.type_cart type_cart = par_cart.type_cart.bases;

            //a = 14;
            a = -1;
            if (Random.Range(0, 4) == 0)
            {
                a = Random.Range(0, 2);
                if (a == 0)
                    type_cart = par_cart.type_cart.sp_barier;
                else
                    type_cart = par_cart.type_cart.sp_crystall;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                    type_cart = par_cart.type_cart.bases;
                else
                {
                    a = Random.Range(0, 6);
                    if (a == 0)
                        type_cart = par_cart.type_cart.fire;
                    else if (a == 1)
                        type_cart = par_cart.type_cart.blood;
                    else if (a == 2)
                        type_cart = par_cart.type_cart.poition;
                    else if (a == 3)
                        type_cart = par_cart.type_cart.energy;
                    else if (a == 4)
                        type_cart = par_cart.type_cart.ice;
                    else if (a == 5)
                        type_cart = par_cart.type_cart.rock;
                }

            }
            return create_cart_art(x, y, type_cart);



            /*
            if (Random.Range(0, 5) == 0)
            {
                if (Random.Range(0, 8) != 0)
                {
                    if (Random.Range(0, 2) == 0)
                        gm = "cart sp atack";
                    else
                        gm = "cart sp barier";
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                        gm = "cart att pow";
                    else
                        gm = "cart bar pow";
                }
            }
            if (a < 18)
            {
                int aa = a / 3;

                if (aa == 0)
                {
                    //int a_rd = Random.Range(0, 2);
                    int a_rd = Random.Range(0, 3);
                    a1 = -4;
                    if (a_rd == 0)
                    {
                        if (a1 / 4 < 0)
                            gm = "cart sp rock";
                        else
                            gm = "cart sp rock 1";
                    }
                    else if (a_rd == 1)
                    {
                        if (a1 / 4 < 0)
                            gm = "cart act rock";
                        else
                            gm = "cart act rock 1";
                    }
                    else
                    {
                        if (a1 / 4 < 0)
                            gm = "cart speed rock";
                        else
                            gm = "cart speed rock 1";
                    }
                }
                else if (aa == 1)
                {
                    int a_rd = Random.Range(0, 3);

                    if (a_rd == 0)
                    {
                        gm = "cart sp blood";
                    }
                    else if (a_rd == 1)
                    {
                        gm = "cart act blood";
                    }
                    else
                    {
                        gm = "cart speed blood";
                    }
                }
                else if (aa == 2)
                {
                    int a_rd = Random.Range(0, 3);

                    if (a_rd == 0)
                    {
                        gm = "cart sp fire";
                    }
                    else if (a_rd == 1)
                    {
                        gm = "cart act fire";
                    }
                    else
                    {
                        gm = "cart speed fire";
                    }


                }
                else if (aa == 3)
                {
                    int a_rd = Random.Range(0, 3);

                    if (a_rd == 0)
                    {
                        gm = "cart sp ice";
                    }
                    else if (a_rd == 1)
                    {
                        gm = "cart act ice";
                    }
                    else
                    {
                        gm = "cart speed ice";
                    }


                }
                else if (aa == 4)
                {
                    int a_rd = Random.Range(0, 3);

                    if (a_rd == 0)
                    {
                        gm = "cart sp poition";
                    }
                    else if (a_rd == 1)
                    {
                        gm = "cart act poition";
                    }
                    else
                    {
                        gm = "cart speed poition";
                    }


                }
                else
                {
                    int a_rd = Random.Range(0, 3);

                    if (a_rd == 0)
                    {
                        gm = "cart sp energy";
                    }
                    else if (a_rd == 1)
                    {
                        gm = "cart act energy";
                    }
                    else
                    {
                        gm = "cart speed energy";
                    }
                }
            }
            //gm = "cart sp rock";
            else if (a < 19)
            {
                int a_rd = Random.Range(0, 3);
                if (a_rd==0 && GameObject.Find("cart inven").GetComponent<par_cart_art>().open)
                    gm = "cart inven";
                else
                    a_rd = Random.Range(1, 3);
                if (a_rd == 1)
                    gm = "cart berserk";
                else
                    gm = "cart mast";
            }

            else
            {
                int a_rd = Random.Range(0, 11);
                if (a_rd == 0)
                    gm = "cart hp blood";
                else if (a_rd == 1)
                    gm = "cart hp rock";
                else if (a_rd == 2)
                    gm = "cart hp energy";
                else if (a_rd == 3)
                    gm = "cart hp fire";
                else if (a_rd == 4)
                    gm = "cart hp ice";
                else if (a_rd == 5)
                    gm = "cart hp poition";
                else
                    gm = "cart dop hill";
            }
            */
        }
        //gm = "cart power";

        return create(gm, x, y);
    }
    public static GameObject create(string gm_st, int x, int y)
    {
        //test_message = "error create";

        string name = gm_st;

        GameObject gm = Instantiate(baza[gm_st]);
        //Debug.Log("create gm " + gm_st);
        gm.name = name;
        if (x == -1 || y == -1)
        {
            //Debug.Log("error create " + gm.name);
            //UnityEditor.EditorApplication.isPlaying = false;
            return gm;
        }
        parametrs pr = gm.GetComponent<parametrs>();
        if (pr != null)
        {
            pr.x = x;
            pr.y = y;
        }

        gm.transform.position = new Vector2(x, y);
        //gm.GetComponent<SpriteRenderer>().sortingOrder = (8 - y)*2;
        if (gm.GetComponent<par_mob>() != null)
        {

            //Debug.Log("create mob 1 " + gm_st);
            for (int i = 1; i < size_global * size_global; i++)
            {
                if (mob_list[i] == null)
                {
                    //gm.GetComponent<par_mob>().num = i;
                    gm.GetComponent<par_mob>().num = i;
                    mob_list[i] = gm;
                    nums_mob.Add(i);
                    break;
                    //mob_list.Add(gm);
                }
            }

            par_mob mb = gm.GetComponent<par_mob>();
            mb.carts_count_max.Clear();
            foreach (string art in baza[gm_st].GetComponent<par_mob>().carts_count_max.Keys)
            {
                //mb.carts_count_max.Add(art, 5);

                if (GameObject.Find(art).GetComponent<par_cart>().spetial)
                    mb.carts_count_max.Add(art, 3);
                else
                    mb.carts_count_max.Add(art, 6);
            }

            while (mb.lt_carts_art.Count > 0)
            {
                string st = mb.lt_carts_art[0].name;
                mb.remove_cart_art(st);
                mb.remove_cart(st);
            }
            mb.lt_carts_art.Clear();

            /*
            
            while (gm.GetComponent<par_cart_art>() != null)
                DestroyImmediate(gm.GetComponent<par_cart_art>());
            while (gm.transform.Find("artefacts").transform.childCount > 0)
            {
                DestroyImmediate(gm.transform.Find("artefacts").transform.GetChild(0).gameObject);
            }
            */
            /*
            foreach (Transform tr in gm.transform.Find("artefacts").transform)
            {
                if (mb.num == 1)
                    Debug.Log(true + "  ttttttttttt dellll " + "  " + tr.gameObject.name);
                Destroy(tr.gameObject);
            }
            foreach (Transform tr in gm.transform.Find("artefacts"))
            {
                if (mb.num == 1)
                    Debug.Log(true + "  ttttttttttt dellll1111 " + "  " + tr.gameObject.name);
                Destroy(tr.gameObject);
            }
            */
            int aa = 0;
            aa = num_lev;
            /*
            if (num_lev < 10)
            {
                aa = num_lev / 5;
            }
            else
                aa = 5;
            */
            bool bl = true;

            bool bl1 = false;
            bool bl2 = false;

            //bl1 = true;
            //gm_st = "goblin rock elit";

            if (Random.Range(0, 100) - aa * 2 <= 0)
            {
                bl = false;
                bl1 = true;
                bl2 = true;
                //aa = 8;
                if (gm_st == "goblin")
                {
                    //bl = true;
                    mb.add_cart_art("cart dop hill");
                }
                else if (gm_st == "goblin elit")
                {
                    int a_rd = Random.Range(0, 5);
                    if (a_rd < 2 || a_rd == 4)
                        mb.add_cart_art("cart att pow");
                    if (a_rd >= 2)
                        mb.add_cart_art("cart bar pow");
                    //mb.add_cart_art("cart act rock 1");
                }
                else if (gm_st == "goblin hiller")
                {
                    bl = true;
                    //mb.add_cart_art("cart act blood");
                }

                else if (gm_st == "goblin blood elit")
                {
                    mb.add_cart_art("cart blood pow");
                    if (Random.Range(0, 5) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp atack");
                        mb.run_mob = true;
                        bl1 = false;
                    }

                    if (Random.Range(0, 10) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp barier");
                        bl1 = false;
                    }
                    bl2 = false;
                }

                else if (gm_st == "goblin ice elit")
                {
                    mb.add_cart_art("cart ice pow");
                    if (Random.Range(0, 5) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp atack");
                        mb.run_mob = true;
                        bl1 = false;
                    }

                    if (Random.Range(0, 10) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp barier");
                        bl1 = false;
                    }
                    bl2 = false;
                }
                else if (gm_st == "goblin poition elit")
                {
                    mb.add_cart_art("cart poition pow");
                    if (Random.Range(0, 5) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp atack");
                        mb.run_mob = true;
                        bl1 = false;
                    }

                    if (Random.Range(0, 10) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp barier");
                        bl1 = false;
                    }
                    bl2 = false;
                }

                else if (gm_st == "goblin rock elit")
                {
                    mb.add_cart_art("cart rock pow");
                    if (Random.Range(0, 5) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp atack");
                        bl1 = false;
                    }

                    if (Random.Range(0, 10) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp barier");
                        bl1 = false;
                    }
                    bl2 = false;
                }
                else if (gm_st == "goblin energy elit")
                {
                    mb.add_cart_art("cart energy pow");
                    if (Random.Range(0, 5) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp atack");
                        mb.run_mob = true;
                        bl1 = false;
                    }

                    if (Random.Range(0, 10) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp barier");
                        bl1 = false;
                    }
                    bl2 = false;
                }
                else if (gm_st == "goblin fire elit")
                {
                    mb.add_cart_art("cart fire pow");
                    if (Random.Range(0, 5) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp atack");
                        bl1 = false;
                    }

                    if (Random.Range(0, 10) <= aa / 5)
                    {
                        mb.add_cart_art("cart sp barier");
                        bl1 = false;
                    }
                    bl2 = false;
                }

                else if (gm_st == "goblin boss" || gm_st == "goblin shaman" || gm_st == "dungalorg")
                {
                    //mb.add_cart_art("cart mast");
                    //if(Random.Range(0,3)==0)
                    //mb.add_cart_art("cart berserk");
                    bl = true;
                }

                /*
                if (Random.Range(0, 5) == 0&&bl2)
                {
                    mb.add_cart_art("cart dop hill");
                    bl = false;
                }
                    */
                mb.hp = mb.hp_max;
            }





            if ((gm_st != "goblin blood elit" && gm_st != "goblin rock elit" && gm_st != "goblin fire elit" && gm_st != "goblin energy elit" && gm_st != "goblin ice elit" && gm_st != "goblin poition elit" &&
                 gm_st != "derlorg blood" && gm_st != "derlorg rock" && gm_st != "derlorg fire" && gm_st != "derlorg energy" && gm_st != "derlorg ice" && gm_st != "derlorg poition"
                && !mb.nps) && bl)
            {
                Destroy(gm.transform.Find("artefacts").gameObject);
            }


            if (bl1)
            {
                int rr = Random.Range(0, 5);
                if (gm_st == "goblin blood elit")
                {
                    if (rr < 2 || rr == 4)
                        mb.add_cart_art("cart act blood");
                    if (rr <= 4 && rr > 1)
                        mb.add_cart_art("cart speed blood");
                }
                else if (gm_st == "goblin rock elit")
                {
                    if (rr < 2 || rr == 4)
                        mb.add_cart_art("cart act rock");
                    if (rr <= 4 && rr > 1)
                        mb.add_cart_art("cart speed rock");
                }
                else if (gm_st == "goblin energy elit")
                {
                    if (rr < 2 || rr == 4)
                        mb.add_cart_art("cart act energy");
                    if (rr <= 4 && rr > 1)
                        mb.add_cart_art("cart speed energy");
                }
                else if (gm_st == "goblin fire elit")
                {
                    if (rr < 2 || rr == 4)
                        mb.add_cart_art("cart act fire");
                    if (rr <= 4 && rr > 1)
                        mb.add_cart_art("cart speed fire");
                }
                else if (gm_st == "goblin ice elit")
                {
                    if (rr < 2 || rr == 4)
                        mb.add_cart_art("cart act ice");
                    if (rr <= 4 && rr > 1)
                        mb.add_cart_art("cart speed ice");
                }
                else if (gm_st == "goblin poition elit")
                {
                    if (rr < 2 || rr == 4)
                        mb.add_cart_art("cart act poition");
                    if (rr <= 4 && rr > 1)
                        mb.add_cart_art("cart speed poition");
                }
            }

            //if()
            foreach (par_cart_art art in baza[gm_st].GetComponent<par_mob>().lt_carts_art)
            {
                //Debug.Log(gm.name  +"  "+ art.name);
                par_cart_art art1 = mb.add_cart_art(art.name);
                if (art.name == "cart shaman boss" && gm.name == "goblin shaman")
                {
                    art1.q = art1.q_max;
                }
            }
            //Debug.Log(nums_mob.Count + " lt " + mb.num);
            mob[x, y] = gm;

            if (mb.nps)
            {
                /*

                GameObject gm_pr = Instantiate(GameObject.Find("Canvas").transform.Find("panels").Find("ignoring").Find("pn prise").gameObject,gm.transform.Find("Canvas hp"));
                gm_pr.name = "pn prise";
                gm_pr.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                gm_pr.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0.75f, 0);
                

                par_prise pr_buy = gm.GetComponent<par_prise>();
                int col_rd = 0;
                string st_pr = null;
                if (Random.Range(0, 3) == 0)
                {

                    if (gm_st == "goblin fire naim")
                    {
                        
                        
                        int rd = Random.Range(0, 7);
                        if (rd ==0)
                        {
                            mb.add_cart_art("cart dop hill");
                            col_rd = 1;
                            st_pr = "cart dop hill";
                        }
                        else if (rd <3)
                        {
                            col_rd = 1;
                            st_pr = "cart sp fire";
                        }
                        else if (rd<5)
                        {
                            col_rd = 1;
                            st_pr = "cart act fire";
                        }
                        else
                        {
                            col_rd = 3+num_lev / 4;
                            if (col_rd > 5)
                                col_rd = 5;
                            st_pr = "cart fire";
                        }
                    }
                    else if (gm_st == "goblin rock naim")
                    {
                        int rd = Random.Range(0, 7);
                        if (rd == 0)
                        {
                            mb.add_cart_art("cart dop hill");
                            col_rd = 1;
                            st_pr = "cart dop hill";
                        }
                        else if (rd < 3)
                        {
                            col_rd = 1;
                            st_pr = "cart sp rock";
                        }
                        else if (rd < 5)
                        {
                            col_rd = 1;
                            st_pr = "cart act rock";
                        }
                        else
                        {
                            col_rd = 3 + num_lev / 4;
                            if (col_rd > 5)
                                col_rd = 5;
                            st_pr = "cart rock";
                        }
                    }
                    else if (gm_st == "goblin hp naim")
                    {
                        int rd = Random.Range(0, 7);
                        if (rd == 0)
                        {
                            mb.add_cart_art("cart dop hill");
                            col_rd = 1;
                            st_pr = "cart dop hill";
                        }
                        else if (rd < 3)
                        {
                            col_rd = 1;
                            st_pr = "cart sp blood";
                        }
                        else if (rd < 5)
                        {
                            col_rd = 1;
                            st_pr = "cart act blood";
                        }
                        else
                        {
                            col_rd = 3 + num_lev / 4;
                            if (col_rd > 5)
                                col_rd = 5;
                            st_pr = "cart blood";
                        }
                    }
                    else if (gm_st == "goblin energy naim")
                    {
                        int rd = Random.Range(0, 7);
                        if (rd == 0)
                        {
                            mb.add_cart_art("cart dop hill");
                            col_rd = 1;
                            st_pr = "cart dop hill";
                        }
                        else if (rd < 3)
                        {
                            col_rd = 1;
                            st_pr = "cart sp energy";
                        }
                        else if (rd < 5)
                        {
                            col_rd = 1;
                            st_pr = "cart act energy";
                        }
                        else
                        {
                            col_rd = 3 + num_lev / 4;
                            if (col_rd > 5)
                                col_rd = 5;
                            st_pr = "cart energy";
                        }
                    }

                    for (int i = 0; i < col_rd; i++)
                    {
                        pr_buy.lt_prise.Add(st_pr);
                    }
                }
                else
                {
                    col_rd = 7 + num_lev / 2;
                    st_pr = "cart talon";
                    if (col_rd > 10)
                        col_rd = 10;
                    for (int i = 0; i < col_rd; i++)
                    {
                        pr_buy.lt_prise.Add(st_pr);
                    }
                }

                gm_pr.transform.Find("im_price").gameObject.GetComponent<Image>().sprite = GameObject.Find(st_pr).GetComponent<SpriteRenderer>().sprite;
                gm_pr.transform.Find("im_price").Find("tx").gameObject.GetComponent<TMP_Text>().text = col_rd.ToString();

                */
            }
        }
        else if (gm.GetComponent<par_nat>() != null)
        {
            //Debug.Log("create nat 1 " + gm_st);
            par_nat p_n = gm.GetComponent<par_nat>();
            p_n.x = x;
            p_n.y = y;
            nature[x, y] = gm;
            if (gm.name == "stalact" || gm.name == "fakel" || gm.name == "camp")
            {
                Vector3 v3 = eath[x, y].transform.position;
                Destroy(eath[x, y]);
                eath[x, y] = Instantiate(baza[eath_now]);
                eath[x, y].name = eath_now;
                eath[x, y].transform.position = v3;
                eath[x, y].active = false;
                eath[x, y].GetComponent<par_eath>().x = x;
                eath[x, y].GetComponent<par_eath>().y = y;
                eath[x, y].GetComponent<SpriteRenderer>().sprite = eath[x, y].GetComponent<par_eath>().rd_im_st[0];
            }

        }
        else if (gm.GetComponent<par_eath>() != null)
        {
            //Debug.Log("create eath 1 " + gm_st);
            eath[x, y] = gm;
            gm.GetComponent<par_eath>().x = x;
            gm.GetComponent<par_eath>().y = y;
        }
        else if (gm.GetComponent<par_cart>() != null)
        {
            //Debug.Log("create cart 1 " + gm_st);
            //par_eq p_eq = gm.GetComponent<par_eq>();

            //gm.GetComponent<SpriteRenderer>().sortingOrder = 0;
            carts[x, y] = gm;
        }
        if (gm.GetComponent<par_mob>() == null)
            gm.active = false;
        else
            gm.GetComponent<par_mob>().viz(false);
        //test_message = "good";
        return gm;

    }
    /*
    void delete(int x,int y)
    {
        //Debug.Log("del");
        if (decor[x_r, y_r, x , y] != null)
        {

            int a = decor[x_r, y_r, x, y].Count;
            for (int i = 0; i <a ; i++)
            {
                //Debug.Log(decor[x_r, y_r, x, y][i].name);
                //Debug.Log("nulling");
                Destroy(decor[x_r, y_r, x, y][i]);
                //decor[x_r, y_r, x, y].RemoveAt(0);
            }
            decor[x_r, y_r, x, y]=null;
        }
        if(nature[x_r, y_r, x, y] != null)
            Destroy(nature[x_r, y_r, x, y]);
        nature[x_r, y_r, x, y] = null;
    }
    */
    public void generate_nature(int x, int y)
    {

    }
    public void generate_case(int x_r, int y_r, int x, int y)
    {

    }

    delegate void delegat(int xx, int yy, int x, int y);




    class class_way_generate
    {
        public List<Vector2> v2_list = new List<Vector2>();
        public int r;
        public class_way_generate(int r_)
        {
            r = r_;
        }
    }

    public static void generate_room()
    {
        //test_message = "error generate";


        Debug.Log("level" + num_lev + "  " + num_lev_type);
        List<GameObject> lt_fr = new List<GameObject>();
        if (!(num_lev == 0))
        {
            for (int i = 0; i < size_global; i++)
            {
                for (int j = 0; j < size_global; j++)
                {
                    if (mob[j, i] != null && !mob[j, i].GetComponent<par_mob>().friend)
                        Destroy(mob[j, i]);
                    else if (mob[j, i] != null && mob[j, i].GetComponent<par_mob>().friend)
                        lt_fr.Add(mob[j, i]);
                    if (nature[j, i] != null)
                        Destroy(nature[j, i]);
                    if (carts[j, i] != null)
                        Destroy(carts[j, i]);
                    if (eath[j, i] != null)
                        Destroy(eath[j, i]);
                    if (lt_lovyshka[j, i] != null)
                    {
                        foreach (GameObject gm in lt_lovyshka[j, i])
                        {
                            Destroy(gm);
                        }

                        lt_lovyshka[j, i].Clear();
                    }

                    mob[j, i] = null;
                    nature[j, i] = null;
                    carts[j, i] = null;
                    eath[j, i] = null;
                    mob_list[i * size_global + j] = null;
                }
            }
            nums_mob.Clear();
            nums_mob_wolk.Clear();
            //mob_list[0] = GameObject.Find("player");
            //nums_mob.Add(0);
            foreach (GameObject gm in lt_fr)
            {
                par_mob mbs = gm.GetComponent<par_mob>();
                nums_mob.Add(mbs.num);
                mob_list[mbs.num] = gm;
            }
        }
        else
            lt_fr.Add(GameObject.Find(data.data_now.pl_name_now));
        //cart_effects.Clear();
        //baza.Clear();
        //num_lev = 0;
        //num_lev_type = 0;



        int st_x, st_y;
        if (Random.Range(0, 2) == 0)
        {
            st_x = Random.Range(3, size_global - 3);
            st_y = Random.Range(0, 2) * (size_global - 6) + 3;
        }
        else
        {
            st_y = Random.Range(3, size_global - 3);
            st_x = Random.Range(0, 2) * (size_global - 6) + 3;
        }
        int x_end = -1, y_end = -1;
        for (int i = 0; i < 100; i++)
        {

            if (Random.Range(0, 2) == 0)
            {
                x_end = Random.Range(3, size_global - 3);
                y_end = Random.Range(0, 2) * (size_global - 6) + 3;
            }
            else
            {
                y_end = Random.Range(3, size_global - 3);
                x_end = Random.Range(0, 2) * (size_global - 6) + 3;
            }
            if (way(x_end, y_end, size_global)[st_x, st_y] == size_global - 1)
                break;
            else if (i == 99)
            {
                if (Random.Range(0, 2) == 0)
                {
                    st_x = Random.Range(3, size_global - 3);
                    st_y = Random.Range(0, 2) * (size_global - 6) + 3;
                }
                else
                {
                    st_y = Random.Range(3, size_global - 3);
                    st_x = Random.Range(0, 2) * (size_global - 6) + 3;
                }
                i = 0;
                //Debug.LogError("i==99");
            }

        }
        Debug.Log(st_x + "  " + st_y);
        Debug.Log(x_end + "  " + y_end);

        string facel_now = null;
        string camp_now = null;
        string decor_now = null;
        string cave_now = null;
        //type_lvl_now = type_lvl.derlorg;
        for (int i = 0; i < size_global; i++)
        {
            for (int j = 0; j < size_global; j++)
            {
                //nature[j, i] = null;
                if (type_lvl_now == type_lvl.bases)
                {
                    wall_now = "rock";
                    eath_now = "eath";
                    decor_now = "stalact";
                    facel_now = "fakel";
                    camp_now = "camp";
                    cave_now = "cave";
                    //wall_now = "wall";
                    //eath_now = "eath 1";
                    if (Random.Range(0, 100) != 0)
                        create(eath_now, j, i);
                    else
                    {
                        int rr = Random.Range(1, 3);
                        int[,] way_rd = way(j, i, rr + 1);
                        for (int i1 = i - rr; i1 <= i + rr; i1++)
                        {
                            if (i1 >= 0 && i1 < size_global)
                            {
                                for (int j1 = j - rr; j1 <= j + rr; j1++)
                                {
                                    if (j1 >= 0 && j1 < size_global)
                                    {
                                        if (way_rd[j1, i1] != -1)
                                            create("grass", j1, i1);
                                    }
                                }
                            }
                        }
                    }

                }
                else if (type_lvl_now == type_lvl.bases_1)
                {
                    wall_now = "wall";
                    eath_now = "eath 1";
                    facel_now = "fakel 1";
                    camp_now = "fakel 1";
                    decor_now = "colonna";
                    cave_now = "cave 1";
                    //wall_now = "rock derlorg";
                    //eath_now = "eath derlorg";
                    //facel_now = "fakel derlorg";
                    //camp_now = "camp derlorg";
                    //decor_now = "stalact";

                    create(eath_now, j, i);
                    //create("wall", j, i);
                }
                else if (type_lvl_now == type_lvl.derlorg)
                {

                    wall_now = "rock derlorg";
                    eath_now = "eath derlorg";
                    facel_now = "fakel derlorg";
                    camp_now = "camp derlorg";
                    decor_now = "stalact";
                    cave_now = "cave derlorg";

                    create(eath_now, j, i);
                    //create("wall", j, i);
                }
                create(wall_now, j, i);
                //Debug.Log(nature[j, i] == null);
            }
        }
        //nature[x_end, y_end] = null;
        //nature[st_x, st_y] = null;
        /*
        do
        {
            if (Random.Range(0, 2) == 0)
            {
                x_end = Random.Range(3, size_global - 3);
                y_end = Random.Range(0, 2) * (size_global - 6) + 3;
            }
            else
            {
                y_end = Random.Range(3, size_global - 3);
                x_end = Random.Range(0, 2) * (size_global - 6) + 3;
            }
        } while (way(x_end,y_end,15)[st_x,st_y]!=-1);
        */
        int ways_points = Random.Range(3, 6);
        //ways_points = 5;
        int range_way = 8;
        int dop_marging = 0;
        bool boss_lev = false;

        //num_lev = 18;

        if (((num_lev + 1) % 5 == 0 && num_lev != 0))
        {
            ways_points = 1;
            range_way = 15;
            dop_marging = 3;
            boss_lev = true;
        }

        List<Vector2> lt_points = new List<Vector2>();
        List<Vector2> lt_points_sp;
        lt_points.Add(new Vector2(st_x, st_y));
        lt_points.Add(new Vector2(x_end, y_end));
        //lt_points_sp.Add(new Vector2(st_x, st_y));
        //lt_points_sp.Add(new Vector2(x_end, y_end));


        while (lt_points.Count - 2 < ways_points)
        {
            int x_rd = Random.Range(3 + dop_marging, size_global - 3 - dop_marging);
            int y_rd = Random.Range(3 + dop_marging, size_global - 3 - dop_marging);

            int[,] mas_way = way(x_rd, y_rd, range_way);
            bool bl = true;
            foreach (Vector2 v2 in lt_points)
            {

                if (mas_way[(int)v2.x, (int)v2.y] != -1)
                {
                    bl = false;
                    break;
                }
            }
            if (bl)
            {
                //Debug.Log(x_rd + " " + y_rd);
                lt_points.Add(new Vector2(x_rd, y_rd));
            }
            //Debug.Log(lt_points.Count);
            //yield return new WaitForSeconds(0.1f);
        }
        lt_points_sp = new List<Vector2>(lt_points);
        //lt_points_sp.Remove(new Vector2(x_end, y_end));
        //lt_points_sp.Remove(new Vector2(st_x, st_y));
        List<Vector2> lt_points_now = new List<Vector2>();
        int[] type = new int[lt_points.Count];
        for (int i = 0; i < lt_points.Count; i++)
        {
            int a = Random.Range(0, 5);
            if (a < 2)
                type[i] = 1;
            else if (a < 4)
                type[i] = 2;
            else

                type[i] = 3;

        }
        int r = Random.Range(1, 3);
        r = 1;



        Stack<class_way_generate> v2_ways = new Stack<class_way_generate>();
        while (lt_points.Count > 1)
        {
            int a_st = Random.Range(0, lt_points.Count);
            if (boss_lev)
            {
                //if(lt_points.Count==3)
                a_st = 0;
            }

            //int a_end = Random.Range(0, lt_points.Count);
            /*
            while (a_st == a_end)
            {
                a_end = Random.Range(0, lt_points.Count);
            }


            */
            Vector2 v2 = lt_points[a_st];
            Vector2 v2_now = new Vector2(-1, -1);
            int ln = 0;

            int[,] way_st = way((int)v2.x, (int)v2.y, ln);

            if (!boss_lev)
            {
                foreach (Vector2 v2_ in lt_points)
                {
                    if ((ln == 0 || way_st[(int)v2_.x, (int)v2_.y] != -1) && v2_ != v2)
                    {
                        v2_now = v2_;
                        ln = way((int)v2_.x, (int)v2_.y, ln)[(int)v2.x, (int)v2.y];
                    }

                }
            }
            else
            {
                v2_now = lt_points[lt_points.Count - 1];
            }
            //Vector2 v2_now = lt_points[a_end];
            //v2_now = ;


            int x_now = (int)v2.x;
            int y_now = (int)v2.y;
            /*
            if (Random.Range(0, 3) == 0&& way(x_now,y_now,size_global/2)[(int)v2_now.x,(int)v2_now.y]!=-1&&false)
            {
                r = 0;
                Debug.Log("rd many  "+v2+"    "+v2_now);
                int[] type_ways = new int[4];
                if (v2.x > v2_now.x)
                {
                    type_ways[0] = (int)v2_now.x;
                    type_ways[1] = (int)v2.x;
                }
                else
                {
                    type_ways[0] = (int)v2.x;
                    type_ways[1] = (int)v2_now.x;
                }
                if (v2.y > v2_now.y)
                {
                    type_ways[2] = (int)v2_now.y;
                    type_ways[3] = (int)v2.y;
                }
                else
                {
                    type_ways[2] = (int)v2.y;
                    type_ways[3] = (int)v2_now.y;
                }
                nat[x_now, y_now] = false;

                for (int i = (int)v2.y-2; i <= (int)v2.y + 2; i++)
                {
                    if(i>0 && i < size_global - 1)
                    {
                        for (int j = (int)v2.x - 2; j <= (int)v2.x + 2; j++)
                        {
                            if (j> 0 && j < size_global - 1)
                            {
                                Debug.Log(j + " " + i + "  " + way(j, i, r)[x_now, y_now]);
                                if (way(j, i, r)[x_now, y_now] != -1)
                                {

                                    Destroy(nature[j, i]);
                                    nat[j, i] = false;
                                }

                            }
                        }
                    }
                }

                while (!(x_now==(int)v2_now.x && y_now == (int)v2_now.y))
                {
                    //left to right   down to up
                    List<int> lt_ways = new List<int>() { 1, 2, 3, 4 };
                    if (x_now == type_ways[0])
                        lt_ways.Remove(1);
                    if (x_now == type_ways[1])
                        lt_ways.Remove(2);
                    if (y_now == type_ways[2])
                        lt_ways.Remove(3);
                    if (y_now == type_ways[3])
                        lt_ways.Remove(4);

                    int a = lt_ways[Random.Range(0, lt_ways.Count)];
                    if (a == 1)
                    {
                        x_now--;
                    }
                    else if (a == 2)
                    {
                        x_now++;
                    }
                    else if (a == 3)
                    {
                        y_now--;
                    }
                    else if (a == 4)
                    {
                        y_now++;
                    }
                    Debug.Log(x_now + "  " + y_now);


                    for (int i = y_now - r; i <= y_now + r; i++)
                    {
                        if (i > 0 && i < size_global - 1)
                        {
                            for (int j = x_now - r; j <= x_now + r; j++)
                            {
                                if (j > 0 && j < size_global - 1)
                                {
                                    if (way(j, i, r)[x_now, y_now] != -1)
                                    {

                                        Destroy(nature[j, i]);
                                        nat[j, i] = true;
                                        //yield return new WaitForSeconds(0.1f);
                                    }
                                }
                            }
                        }
                    }
                }

                lt_points.Remove(v2);

            }
            */
            if (true)
            {
                //Debug.Log("rd one   " + v2 + "    " + v2_now);
                int[] type_ways = new int[4];
                if (v2.x > v2_now.x)
                {
                    type_ways[0] = (int)v2_now.x;
                    type_ways[1] = (int)v2.x;
                }
                else
                {
                    type_ways[0] = (int)v2.x;
                    type_ways[1] = (int)v2_now.x;
                }
                if (v2.y > v2_now.y)
                {
                    type_ways[2] = (int)v2_now.y;
                    type_ways[3] = (int)v2.y;
                }
                else
                {
                    type_ways[2] = (int)v2.y;
                    type_ways[3] = (int)v2_now.y;
                }
                //nat[x_now, y_now] = false;
                //Debug.Log(lt_points_sp.Contains(new Vector2(x_now, y_now))+"  case"+ new Vector2(x_now, y_now));
                r = 1 - Random.Range(0, 3) / 2;
                if (boss_lev)
                    r = 1;
                Vector2 v2_now_base = new Vector2(x_now, y_now);
                List<Vector2> lt_v2 = new List<Vector2>();

                v2_ways.Push(new class_way_generate(r));
                v2_ways.Peek().v2_list.Add(v2_now_base);
                while (!(x_now == (int)v2_now.x && y_now == (int)v2_now.y))
                {

                    //left to right   down to up
                    List<int> lt_ways = new List<int>() { 1, 2, 3, 4 };
                    if (x_now == type_ways[0] || v2.x < v2_now.x)
                        lt_ways.Remove(1);
                    if (x_now == type_ways[1] || v2_now.x <= v2.x)
                        lt_ways.Remove(2);
                    if (y_now == type_ways[2] || v2.y < v2_now.y)
                        lt_ways.Remove(3);
                    if (y_now == type_ways[3] || v2_now.y <= v2.y)
                        lt_ways.Remove(4);

                    int a = lt_ways[Random.Range(0, lt_ways.Count)];
                    if (a == 1)
                    {
                        x_now--;
                    }
                    if (a == 2)
                    {
                        x_now++;
                    }
                    if (a == 3)
                    {
                        y_now--;
                    }
                    if (a == 4)
                    {
                        y_now++;
                    }
                    //Debug.Log(x_now + "  " + y_now);

                    if (type_lvl_now == type_lvl.bases_1)
                    {
                        Debug.Log(new Vector2(x_now, y_now) + "  is way");
                        v2_ways.Peek().v2_list.Add(new Vector2(x_now, y_now));
                    }

                    lt_v2.Add(new Vector2(x_now, y_now));
                    int[,] ways = way(x_now, y_now, r);
                    for (int i = y_now - r; i <= y_now + r; i++)
                    {
                        if (i > 0 && i < size_global - 1)
                        {
                            for (int j = x_now - r; j <= x_now + r; j++)
                            {
                                if (j > 0 && j < size_global - 1)
                                {
                                    if (ways[j, i] != -1 && (nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes))
                                    {



                                        Destroy(nature[j, i]);
                                        nature[j, i] = null;
                                        //yield return new WaitForSeconds(0.1f);
                                    }
                                }
                            }
                        }
                    }



                }
                //Debug.Log(lt_points_sp.Contains(new Vector2(x_now, y_now)) + "  case" + new Vector2(x_now, y_now));

                lt_points.Remove(v2);


            }
        }





        bool bool_nps = true;
        foreach (Vector2 v2 in lt_points_sp)
        {
            r = 1;
            int x_sp = (int)v2.x;
            int y_sp = (int)v2.y;
            if (x_sp != st_x && x_sp != x_end && y_sp != st_y && y_sp != y_end)
            {
                if (((num_lev + 1) % 5 == 0 && num_lev != 0))
                {
                    r = 5;
                    int[,] way_ = way(x_sp, y_sp, r);
                    for (int i = y_sp - r; i <= y_sp + r; i++)
                    {
                        if (i > 0 && i < size_global - 1)
                        {
                            for (int j = x_sp - r; j <= x_sp + r; j++)
                            {
                                if (j > 0 && j < size_global - 1)
                                {
                                    if (way_[j, i] != -1 && nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                    {

                                        Destroy(nature[j, i]);
                                        nature[j, i] = null;

                                        //yield return new WaitForSeconds(0.1f);
                                    }
                                }
                            }
                        }
                    }
                    create(wall_now, x_sp - 1, y_sp - 1);
                    create(wall_now, x_sp + 1, y_sp - 1);
                    create(wall_now, x_sp - 1, y_sp + 1);
                    create(wall_now, x_sp + 1, y_sp + 1);

                    int a_boss = (num_lev + 1 + 5) / 10;
                    if (type_lvl_now == type_lvl.derlorg)
                    {
                        a_boss = (num_lev + 1) / 10;
                    }
                    //a_boss = 1;
                    // Debug.Log(a_boss+" boss");
                    //a = 0;
                    if (a_boss > 4)
                        a_boss = 4;
                    //a = 4;

                    //if (type_lvl_now != type_lvl.derlorg)
                    //{
                    while (a_boss > 0)
                    {
                        int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                        int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                        if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1)
                        {
                            if (object_bool(x_rd, y_rd))
                            {

                                if (a_boss > 0)
                                {
                                    if (type_lvl_now != type_lvl.derlorg)
                                    {

                                        //if (Random.Range(0, 2) == 0)
                                            //create("goblin boss", x_rd, y_rd);
                                        //else
                                            create("goblin shaman", x_rd, y_rd);

                                    }
                                    else
                                    {
                                        create("dungalorg", x_rd, y_rd);
                                    }
                                    a_boss--;
                                }
                            }
                        }
                    }
                    //}

                }
                else
                {
                    int a_rd = Random.Range(0, 5);
                    //a_rd = 3;
                    //a_rd = 0;
                    if (a_rd == 0)
                    {
                        r = 2;
                        int[,] way_ = way(x_sp, y_sp, r);
                        for (int i = y_sp - r; i <= y_sp + r; i++)
                        {
                            if (i > 0 && i < size_global - 1)
                            {
                                for (int j = x_sp - r; j <= x_sp + r; j++)
                                {
                                    if (j > 0 && j < size_global - 1)
                                    {
                                        if (way_[j, i] != -1 && nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                        {

                                            Destroy(nature[j, i]);
                                            nature[j, i] = null;

                                            //yield return new WaitForSeconds(0.1f);
                                        }
                                    }
                                }
                            }
                        }

                        if (Random.Range(0, 20) == 0)
                        {
                            create(facel_now, x_sp, y_sp);

                            List<string> lt_rd = new List<string>();
                            if (GameObject.Find("cart speed").GetComponent<par_cart_art>().open)
                                lt_rd.Add("cart speed");
                            if (GameObject.Find("cart pow").GetComponent<par_cart_art>().open)
                                lt_rd.Add("cart pow");
                            if (GameObject.Find("cart respawn").GetComponent<par_cart_art>().open)
                                lt_rd.Add("cart respawn");
                            for (int i = 0; i < 4; i++)
                            {
                                if (GameObject.Find("cart inven").GetComponent<par_cart_art>().open)
                                    lt_rd.Add("cart inven");
                                lt_rd.Add("cart mast");
                                lt_rd.Add("cart dop hill");
                                lt_rd.Add("cart berserk");
                                lt_rd.Add("cart potoc fire");
                                lt_rd.Add("cart potoc poition");
                                lt_rd.Add("cart potoc blood");
                                lt_rd.Add("cart potoc rock");
                                lt_rd.Add("cart potoc energy");
                                lt_rd.Add("cart potoc ice");
                            }

                            int a = 1 + Random.Range(0, 5) / 4;
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && way_[x_rd, y_rd] != -1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    string stt = lt_rd[Random.Range(0, lt_rd.Count)];
                                    lt_rd.Remove(stt);
                                    create(stt, x_rd, y_rd);

                                    a--;
                                    if (a == 0)
                                        break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }
                        }
                        else if (Random.Range(0, 4) == 0)
                        {
                            for (int i = y_sp - r; i <= y_sp + r; i++)
                            {
                                if (i > 0 && i < size_global - 1)
                                {
                                    for (int j = x_sp - r; j <= x_sp + r; j++)
                                    {
                                        if (j > 0 && j < size_global - 1)
                                        {

                                            if (way(j, i, r)[x_sp, y_sp] != -1)
                                            {
                                                Destroy(eath[j, i]);
                                                eath[j, i] = null;
                                                create("eath cuzna", j, i);
                                            }


                                        }
                                    }
                                }
                            }
                            create("cuzna", x_sp, y_sp);

                            int aa_ = Random.Range(1, 3);
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && object_bool(x_rd, y_rd) && way_[x_rd, y_rd] != -1 && carts[x_rd, y_rd] == null)
                                {
                                    create("fakel cuzna", x_rd, y_rd);
                                    aa_--;
                                    if (aa_ == 0)
                                        break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }

                            int a = Random.Range(2, 5);
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && way_[x_rd, y_rd] != -1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    if (GameObject.Find("cart cuzna").GetComponent<par_cart_art>().open && Random.Range(0, 20) == 0)
                                        create("cart cuzna", x_rd, y_rd);
                                    else
                                        create("cart cuznec", x_rd, y_rd);

                                    a--;
                                    if (a == 0)
                                        break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }
                        }
                        else
                        {


                            //Debug.Log("tryyyyyyyyyyyyyyyyyyyyyyyyyyy");




                            //create("case", x_sp, y_sp);
                            //nat[x_sp, y_sp] = true;


                            /*
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (c != r - 1) && nature[x_rd, y_rd] == null)
                                {

                                    create("fakel", x_rd, y_rd);
                                    break;
                                }
                            }
                            */
                            create(facel_now, x_sp, y_sp);
                            int cart_rd = Random.Range(0, 8);
                            par_cart.type_cart tp_cart = par_cart.type_cart.bases;
                            /*



                            if (cart_rd < 2)
                            {

                            }
                            else if (cart_rd < 3)
                            {
                                tp_cart = par_cart.type_cart.rock;
                            }
                            else if (cart_rd < 4)
                            {
                                tp_cart = par_cart.type_cart.blood;
                            }
                            else if (cart_rd < 5)
                            {
                                tp_cart = par_cart.type_cart.fire;
                            }
                            else if (cart_rd < 6)
                            {
                                tp_cart = par_cart.type_cart.ice;
                            }
                            else if (cart_rd < 7)
                            {
                                tp_cart = par_cart.type_cart.poition;
                            }
                            else if (cart_rd < 8)
                            {
                                tp_cart = par_cart.type_cart.energy;
                            }


                        */
                            int col_max = 0;

                            if (Random.Range(0, 4) == 0)
                            {
                                cart_rd = Random.Range(0, 2);
                                if (cart_rd == 0)
                                    tp_cart = par_cart.type_cart.sp_barier;
                                else
                                    tp_cart = par_cart.type_cart.sp_crystall;
                                col_max = 4;
                            }
                            else
                            {
                                if (Random.Range(0, 2) == 0)
                                    tp_cart = par_cart.type_cart.bases;
                                else
                                {
                                    cart_rd = Random.Range(0, 6);
                                    if (cart_rd == 0)
                                        tp_cart = par_cart.type_cart.fire;
                                    else if (cart_rd == 1)
                                        tp_cart = par_cart.type_cart.blood;
                                    else if (cart_rd == 2)
                                        tp_cart = par_cart.type_cart.poition;
                                    else if (cart_rd == 3)
                                        tp_cart = par_cart.type_cart.energy;
                                    else if (cart_rd == 4)
                                        tp_cart = par_cart.type_cart.ice;
                                    else if (cart_rd == 5)
                                        tp_cart = par_cart.type_cart.rock;
                                }
                                col_max = 8;
                            }
                            int a = Random.Range(col_max / 2, col_max + 1);
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    //create_cart_art(x_rd, y_rd);

                                    if (Random.Range(0, 25) == 0)
                                        create_cart_art(x_rd, y_rd, tp_cart);
                                    else
                                        create_cart(x_rd, y_rd, tp_cart);

                                    a--;
                                    if (a == 0)
                                        break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }
                        }


                        //int rd = Random.Range(0, 100);

                        /*
                        if (Random.Range(0, 4) != 0)
                        {
                            int a = Random.Range(4, 6);
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && way(x_rd, y_rd, r)[x_sp, y_sp] != -1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    create_cart(x_rd, y_rd);
                                    a--;
                                    if (a == 0)
                                        break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }
                        }
                        else
                        {
                            int a = 1;
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && way(x_rd, y_rd, r)[x_sp, y_sp] != -1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    create_cart_art(x_rd, y_rd);
                                    a--;
                                    if (a == 0)
                                        break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }


                        }
                        */
                        /*
                        int aa = 3;
                        for (int i = 0; i < 100; i++)
                        {
                            int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                            int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                            int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                            bool[] bl_n = new bool[4];

                            if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (way_find_lt(nature, GameObject.Find(wall_now), x_rd, y_rd, 1).Count != 0) && object_bool(x_rd, y_rd) && carts[x_rd, y_rd] == null)
                            {
                                create(decor_now, x_rd, y_rd);
                                aa--;
                                if (aa == 0)
                                    break;
                                //nat[x_rd, y_rd] = true;
                                //break;
                            }
                        }
                        */

                        /*
                        for (int i = y_sp - r; i <= y_sp + r; i++)
                        {
                            if (i > 0 && i < size_global - 1)
                            {
                                for (int j = x_sp - r; j <= x_sp + r; j++)
                                {
                                    if (j > 0 && j < size_global - 1)
                                    {
                                        if (way(j, i, r)[x_sp, y_sp] != -1 && nature[j, i] == null && Random.Range(0, 3) == 0)
                                        {

                                            create_cart(j, i);
                                            //yield return new WaitForSeconds(0.1f);
                                        }
                                    }
                                }
                            }
                        }
                        */
                        //r = 1;
                    }
                    else if ((a_rd == 1 || a_rd == 2) && bool_nps)
                    {
                        bool_nps = false;

                        if ((Random.Range(0, 10) == 0 && num_lev > 4) && type_lvl_now != type_lvl.derlorg)
                        {
                            r = 3;
                            int[,] way_ = way(x_sp, y_sp, r);
                            for (int i = y_sp - r; i <= y_sp + r; i++)
                            {
                                if (i > 0 && i < size_global - 1)
                                {
                                    for (int j = x_sp - r; j <= x_sp + r; j++)
                                    {
                                        if (j > 0 && j < size_global - 1)
                                        {
                                            if (way_[j, i] != -1)
                                            {
                                                Destroy(eath[j, i]);
                                                eath[j, i] = null;
                                                create("eath derlorg", j, i);
                                                if (nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                                {
                                                    Destroy(nature[j, i]);
                                                    nature[j, i] = null;
                                                }


                                                //yield return new WaitForSeconds(0.1f);
                                            }
                                        }
                                    }
                                }
                            }
                            while (true)
                            {
                                int x_rd = Random.Range(x_sp - 1, x_sp + 1 + 1);
                                int y_rd = Random.Range(y_sp - 1, y_sp + 1 + 1);
                                int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && c == 1)
                                {
                                    par_mob nps_mob = create("nps derlorg", x_rd, y_rd).GetComponent<par_mob>();
                                    nps_mob.lt_par.Add(new Vector2(x_sp, y_sp));
                                    break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }
                            //create("nps derlorg", x_sp, y_sp);

                        }
                        else
                        {


                            if (Random.Range(0, 7) != 0)
                            {
                                r = 3;
                                int[,] way_ = way(x_sp, y_sp, r);
                                for (int i = y_sp - r; i <= y_sp + r; i++)
                                {
                                    if (i > 0 && i < size_global - 1)
                                    {
                                        for (int j = x_sp - r; j <= x_sp + r; j++)
                                        {
                                            if (j > 0 && j < size_global - 1)
                                            {
                                                if (way_[j, i] != -1 && nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                                {

                                                    Destroy(nature[j, i]);
                                                    nature[j, i] = null;

                                                    //yield return new WaitForSeconds(0.1f);
                                                }
                                            }
                                        }
                                    }
                                }
                                par_cart.type_cart type_cart = par_cart.type_cart.bases;

                                int cart_rd = Random.Range(0, 6);
                                if (cart_rd == 0)
                                {
                                    type_cart = par_cart.type_cart.fire;
                                    create("nps fire", x_sp, y_sp);
                                }

                                else if (cart_rd == 1)
                                {
                                    type_cart = par_cart.type_cart.blood;
                                    create("nps blood", x_sp, y_sp);
                                }

                                else if (cart_rd == 2)
                                {
                                    type_cart = par_cart.type_cart.poition;
                                    create("nps poition", x_sp, y_sp);
                                }

                                else if (cart_rd == 3)
                                {
                                    type_cart = par_cart.type_cart.energy;
                                    create("nps energy", x_sp, y_sp);
                                }

                                else if (cart_rd == 4)
                                {
                                    type_cart = par_cart.type_cart.ice;
                                    create("nps ice", x_sp, y_sp);
                                }

                                else if (cart_rd == 5)
                                {
                                    type_cart = par_cart.type_cart.rock;
                                    create("nps rock", x_sp, y_sp);
                                }



                                int a_tovar = Random.Range(2, 5);
                                while (a_tovar > 0)
                                {
                                    int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                    int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                    int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                                    if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && c == 1 && carts[x_rd, y_rd] == null)
                                    {
                                        int a_rock = 0;
                                        int a_rock_h = 0;
                                        int a_rock_v = 0;
                                        //bool[,] bl_way = new bool[size_global, size_global];

                                        //int[,] bl_way = ;
                                        /*
                                        if (nature[x_rd - 1, y_rd]!=null)
                                            a_rock_h++;
                                        if (nature[x_rd + 1, y_rd]!= null)
                                            a_rock_h++;
                                        if (nature[x_rd , y_rd-1]!= null)
                                            a_rock_v++;
                                        if (nature[x_rd , y_rd+1]!= null)
                                            a_rock_v++;
                                        */


                                        //if (way_find_lt(carts, x_rd, y_rd, 2).Count==0 && (a_rock_v+a_rock_h>2 || (a_rock_h!=2 && a_rock_v!=2)))
                                        //{
                                        int col_price = 0;
                                        if (a_tovar == 1)
                                        {
                                            par_cart cart = create_cart(x_rd, y_rd, type_cart).GetComponent<par_cart>();
                                            if (cart.spetial)
                                                cart.col = 3;
                                            else
                                                cart.col = 6;
                                            col_price = cart.lvl;
                                        }
                                        else
                                        {
                                            par_cart cart = create_cart_art(x_rd, y_rd, type_cart).GetComponent<par_cart>();
                                            col_price = cart.lvl * 2;
                                        }
                                        eath[x_rd, y_rd].AddComponent<par_prise>();
                                        eath[x_rd, y_rd].GetComponent<par_prise>().add_price("cart gold", col_price, carts[x_rd, y_rd].GetComponent<par_cart>().col);

                                        a_tovar--;

                                        //}
                                        //Destroy(eath[x_rd, y_rd]);

                                        //nat[x_rd, y_rd] = true;
                                        //break;
                                    }
                                }
                            }
                            else
                            {
                                r = 2;
                                int[,] way_ = way(x_sp, y_sp, r);
                                for (int i = y_sp - r; i <= y_sp + r; i++)
                                {
                                    if (i > 0 && i < size_global - 1)
                                    {
                                        for (int j = x_sp - r; j <= x_sp + r; j++)
                                        {
                                            if (j > 0 && j < size_global - 1)
                                            {
                                                if (way_[j, i] != -1 && nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                                {

                                                    Destroy(nature[j, i]);
                                                    nature[j, i] = null;

                                                    //yield return new WaitForSeconds(0.1f);
                                                }
                                            }
                                        }
                                    }
                                }

                                create("nps berserk", x_sp, y_sp);
                                while (true)
                                {
                                    int x_rd = Random.Range(x_sp - 1, x_sp + 1 + 1);
                                    int y_rd = Random.Range(y_sp - 1, y_sp + 1 + 1);
                                    int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                                    if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && c == 1)
                                    {
                                        Destroy(eath[x_rd, y_rd]);
                                        create("eath crimzon", x_rd, y_rd);
                                        break;
                                        //nat[x_rd, y_rd] = true;
                                        //break;
                                    }
                                }
                            }

                        }

                    }
                    //else if (Random.Range(0, 5) != 0 )
                    else
                    {
                        r = 2;
                        int[,] way_ = way(x_sp, y_sp, r);
                        for (int i = y_sp - r; i <= y_sp + r; i++)
                        {
                            if (i > 0 && i < size_global - 1)
                            {
                                for (int j = x_sp - r; j <= x_sp + r; j++)
                                {
                                    if (j > 0 && j < size_global - 1)
                                    {
                                        if (way_[j, i] != -1 && nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                        {

                                            Destroy(nature[j, i]);
                                            nature[j, i] = null;
                                            //yield return new WaitForSeconds(0.1f);
                                        }
                                    }
                                }
                            }
                        }
                        int aa = 0;
                        if (num_lev < 7)
                            aa = num_lev / 2;
                        else
                            aa = 4;
                        int a = Random.Range(1 + aa / 2, 4 + aa);
                        //a = 8;
                        //a = 0;
                        while (a > 0)
                        {
                            int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                            int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                            if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1)
                            {
                                if (object_bool(x_rd, y_rd))
                                {

                                    if (type_lvl_now == type_lvl.bases)
                                    {
                                        if ((Random.Range(0, 100) < num_lev))
                                        {
                                            create("org", x_rd, y_rd);
                                        }

                                        if ((Random.Range(0, 100) < num_lev * 5))
                                        {
                                            int rdd = Random.Range(0, 6);
                                            if (rdd == 0)
                                                create("goblin rock elit", x_rd, y_rd);
                                            else if (rdd == 1)
                                                create("goblin fire elit", x_rd, y_rd);
                                            else if (rdd == 2)
                                                create("goblin blood elit", x_rd, y_rd);
                                            else if (rdd == 3)
                                                create("goblin ice elit", x_rd, y_rd);
                                            else if (rdd == 4)
                                                create("goblin poition elit", x_rd, y_rd);
                                            else
                                                create("goblin energy elit", x_rd, y_rd);
                                        }

                                        else
                                        {
                                            if (Random.Range(0, 6) == 0)
                                                create("goblin hiller", x_rd, y_rd);
                                            //create("derlorg shaman", x_rd, y_rd);
                                            else if (num_lev - Random.Range(1, 15) < 0)
                                            {

                                                create("goblin", x_rd, y_rd);
                                                //create("dungalorg", x_rd, y_rd);
                                            }
                                            else
                                            {
                                                create("goblin elit", x_rd, y_rd);
                                            }
                                        }

                                        //create("derlorg fire", x_rd, y_rd);
                                        //create("spider", x_rd, y_rd);
                                    }
                                    else if (type_lvl_now == type_lvl.bases_1)
                                    {
                                        create("org berserk", x_rd, y_rd);
                                        /*
                                        if ((Random.Range(0, 100) < (num_lev - 2) * 5 && num_lev < 25) || (Random.Range(0, 100) < 75 && num_lev >= 25))
                                        {
                                            int rdd = Random.Range(0, 6);
                                            if (rdd == 0)
                                                create("goblin rock elit", x_rd, y_rd);
                                            else if (rdd == 1)
                                                create("goblin fire elit", x_rd, y_rd);
                                            else if (rdd == 2)
                                                create("goblin blood elit", x_rd, y_rd);
                                            else if (rdd == 3)
                                                create("goblin ice elit", x_rd, y_rd);
                                            else if (rdd == 4)
                                                create("goblin poition elit", x_rd, y_rd);
                                            else
                                                create("goblin energy elit", x_rd, y_rd);
                                        }

                                        else
                                        {
                                            if (Random.Range(0, 6) == 0)
                                                create("goblin hiller", x_rd, y_rd);
                                            //create("derlorg shaman", x_rd, y_rd);
                                            else if (num_lev - Random.Range(1, 15) < 0)
                                            {

                                                create("defender", x_rd, y_rd);
                                                //create("dungalorg", x_rd, y_rd);
                                            }
                                            else
                                            {
                                                //create("defender 1", x_rd, y_rd);
                                            }
                                        }
                                        */
                                        //create("derlorg fire", x_rd, y_rd);
                                        //create("spider", x_rd, y_rd);
                                    }
                                    else if (type_lvl_now == type_lvl.derlorg)
                                    {
                                        if ((Random.Range(0, 100) < (num_lev - 12) * 5 && num_lev < 35) || (Random.Range(0, 100) < 75 && num_lev >= 35))
                                        {
                                            int rdd = Random.Range(0, 6);
                                            if (rdd == 0)
                                                create("derlorg rock", x_rd, y_rd);
                                            else if (rdd == 1)
                                                create("derlorg fire", x_rd, y_rd);
                                            else if (rdd == 2)
                                                create("derlorg blood", x_rd, y_rd);
                                            else if (rdd == 3)
                                                create("derlorg ice", x_rd, y_rd);
                                            else if (rdd == 4)
                                                create("derlorg poition", x_rd, y_rd);
                                            else
                                                create("derlorg energy", x_rd, y_rd);
                                        }
                                        else
                                        {
                                            if (Random.Range(0, 6) == 0)
                                                create("derlorg shaman", x_rd, y_rd);
                                            else
                                            {
                                                create("derlorg", x_rd, y_rd);
                                            }
                                        }

                                    }





                                    a--;
                                }
                            }
                        }
                        for (int i = 0; i < 100; i++)
                        {
                            int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                            int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                            int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);
                            if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (c != r - 1) && object_bool(x_rd, y_rd))
                            {

                                create(camp_now, x_rd, y_rd);
                                break;
                            }
                        }
                        aa = 3;
                        for (int i = 0; i < 100; i++)
                        {
                            int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                            int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                            int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                            bool[] bl_n = new bool[4];

                            if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (way_find_lt(nature, GameObject.Find(wall_now), x_rd, y_rd, 1).Count != 0) && object_bool(x_rd, y_rd) && carts[x_rd, y_rd] == null)
                            {
                                create(decor_now, x_rd, y_rd);
                                aa--;
                                if (aa == 0)
                                    break;
                                //nat[x_rd, y_rd] = true;
                                //break;
                            }
                        }
                        /*
                        for (int i = y_sp - r; i <= y_sp + r; i++)
                        {
                            if (i > 0 && i < size_global - 1)
                            {
                                for (int j = x_sp - r; j <= x_sp + r; j++)
                                {
                                    if (j > 0 && j < size_global - 1)
                                    {
                                        if (way(j, i, r)[x_sp, y_sp] != -1 && nature[j, i] == null && Random.Range(0, 6) == 0)
                                        {

                                            create_cart(j, i);
                                            //yield return new WaitForSeconds(0.1f);
                                        }
                                    }
                                }
                            }
                        }
                        */
                        if (Random.Range(0, 10) != 0)
                        {
                            int a1 = Random.Range(3, 6);
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && !(x_rd == x_sp && y_rd == y_sp) && way_[x_rd, y_rd] != -1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    create_cart(x_rd, y_rd);
                                    a1--;
                                    if (a1 == 0)
                                        break;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }
                        }
                        else
                        {
                            int a1 = 1;
                            while (a1 > 0)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && !(x_rd == x_sp && y_rd == y_sp) && way_[x_rd, y_rd] != -1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    create_cart_art(x_rd, y_rd);
                                    a1--;
                                    //nat[x_rd, y_rd] = true;
                                    //break;
                                }
                            }
                        }
                        //r = 1;
                    }
                    /*
                    else
                    {
                        r = 1;
                        int[,] way_ = way(x_sp, y_sp, r);

                        for (int i = y_sp - r; i <= y_sp + r; i++)
                        {
                            if (i > 0 && i < size_global - 1)
                            {
                                for (int j = x_sp - r; j <= x_sp + r; j++)
                                {
                                    if (j > 0 && j < size_global - 1)
                                    {
                                        if (way_[j, i] != -1 && nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                        {

                                            Destroy(nature[j, i]);
                                            nature[j, i] = null;
                                            //yield return new WaitForSeconds(0.1f);
                                        }
                                    }
                                }
                            }
                        }

                        for (int i = y_sp - r; i <= y_sp + r; i++)
                        {
                            if (i > 0 && i < size_global - 1)
                            {
                                for (int j = x_sp - r; j <= x_sp + r; j++)
                                {
                                    if (j > 0 && j < size_global - 1)
                                    {
                                        if (way_[j, i] != -1 && nature[j, i] == null && Random.Range(0, 8) == 0)
                                        {

                                            create_cart(j, i);
                                            //yield return new WaitForSeconds(0.1f);
                                        }
                                    }
                                }
                            }
                        }
                        //int aa = 4;
                    }
                    */
                }



                //int aa = Random.Range(1, 3);

            }
            else
            {
                //if ((num_lev + 1) % 4 == 0 && x_sp == x_end && y_sp == y_end)
                r = 2;
                int[,] way_1 = way(x_sp, y_sp, r + 1);
                for (int i = y_sp - r - 2; i <= y_sp + r + 2; i++)
                {
                    if (i > 0 && i < size_global - 1)
                    {
                        for (int j = x_sp - r - 2; j <= x_sp + r + 2; j++)
                        {
                            if (j > 0 && j < size_global - 1)
                            {
                                if (way_1[j, i] != -1 && nature[j, i] != null && !nature[j, i].GetComponent<par_nat>().undes)
                                {

                                    Destroy(nature[j, i]);
                                    nature[j, i] = null;
                                    //yield return new WaitForSeconds(0.1f);
                                }
                            }
                        }
                    }

                }
                if (x_sp == x_end && y_sp == y_end)
                {
                    //Debug.Log("num level " + num_lev);
                    if ((num_lev + 1) % 5 == 0 && num_lev != 0)
                    {
                        create("cave 1", x_end, y_end);
                    }
                    else
                    {
                        create(cave_now, x_end, y_end);
                    }



                    /*
                    if (((num_lev+1) % 4 == 0 ))
                    {
                        int aa = 0;
                        if (num_lev < 8)
                            aa = (num_lev+1) / 2;
                        else
                            aa = 4;
                        int a = Random.Range(2 + aa / 2, 4 + aa);
                        int a_boss = aa / 2;
                        //a_boss = 1;
                        // Debug.Log(a_boss+" boss");
                        //a = 0;
                        //a = 4;
                        while (a > 0)
                        {
                            int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                            int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                            if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1)
                            {
                                if (object_bool(x_rd, y_rd))
                                {
                                    if (a_boss > 0)
                                    {
                                        create("goblin boss", x_rd, y_rd);
                                        //Debug.Log(a_boss + " boss");
                                        a_boss--;
                                    }
                                    else
                                    {
                                        if ((Random.Range(0, 100) < 6 + num_lev * 2 && num_lev > 2 ))
                                        {
                                            int rdd = Random.Range(0, 6);
                                            if (rdd == 0)
                                                create("goblin rock elit", x_rd, y_rd);
                                            else if (rdd == 1)
                                                create("goblin fire elit", x_rd, y_rd);
                                            else if (rdd == 2)
                                                create("goblin blood elit", x_rd, y_rd);
                                            else if (rdd == 2)
                                                create("goblin ice elit", x_rd, y_rd);
                                            else if (rdd == 2)
                                                create("goblin poition elit", x_rd, y_rd);
                                            else
                                                create("goblin energy elit", x_rd, y_rd);
                                        }
                                        else
                                        {
                                            if (Random.Range(0, 4) == 0)
                                                create("goblin hiller", x_rd, y_rd);
                                            else if (num_lev + num_lev_type * 4 - Random.Range(1, 15) < 0)
                                            {
                                                create("goblin", x_rd, y_rd);
                                            }
                                            else
                                            {
                                                create("goblin elit", x_rd, y_rd);
                                            }
                                        }
                                    }



                                    a--;
                                }
                            }
                        }
                    }
                    else
                    {
                        int aa = 4;
                        aa = 1;
                        for (int i = 0; i < 100; i++)
                        {
                            int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                            int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                            int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                            bool[] bl_n = new bool[4];

                            if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (way_find_lt(nature, GameObject.Find("rock"), x_rd, y_rd, 1).Count != 0) && object_bool(x_rd, y_rd) && carts[x_rd, y_rd] == null)
                            {
                                //create("stalact", x_rd, y_rd);
                                create("goblin shaman", x_rd, y_rd);
                                aa--;
                                if (aa == 0)
                                    break;
                                //nat[x_rd, y_rd] = true;
                                //break;
                            }
                        }
                    }
                    */

                    int aa = 4;
                    //aa = 1;
                    for (int i = 0; i < 100; i++)
                    {
                        int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                        int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                        int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                        bool[] bl_n = new bool[4];

                        if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (way_find_lt(nature, GameObject.Find(wall_now), x_rd, y_rd, 1).Count != 0) && object_bool(x_rd, y_rd) && carts[x_rd, y_rd] == null)
                        {
                            create(decor_now, x_rd, y_rd);
                            //create("dungalorg", x_rd, y_rd);
                            aa--;
                            if (aa == 0)
                                break;
                            //nat[x_rd, y_rd] = true;
                            //break;
                        }
                    }
                }
                //r =1;
            }
        }
        if (type_lvl_now == type_lvl.bases_1)
        {
            foreach (class_way_generate lv_class_for in v2_ways)
            {
                List<Vector2> lv_v2_for = lv_class_for.v2_list;
                if (Random.Range(0, 2) == 0)
                {
                    //foreach (Vector2 v2_for in lv_v2_for)
                    //{
                    int cc = lv_v2_for.Count;
                    int c = (int)cc / 2;
                    //c = 1;

                    for (int i1 = 0; i1 < c; i1++)
                    {
                        if (lv_v2_for.Count == 0)
                            break;
                        Vector2 v2_ = lv_v2_for[Random.Range(0, lv_v2_for.Count)];
                        int x_v = (int)v2_.x;
                        int y_v = (int)v2_.y;

                        Dictionary<Vector2, Vector2> d_v2 = new Dictionary<Vector2, Vector2>();
                        for (int i = y_v - lv_class_for.r - 2; i <= y_v + lv_class_for.r + 2; i++)
                        {
                            if (i > 0 && i < size_global - 1)
                            {
                                //int j = ;
                                List<int> lt_j = new List<int>();
                                //lt_j.Add(x_v - (i - y_v - lv_class_for.r - 2));
                                //lt_j.Add(-(x_v - (i - y_v - lv_class_for.r - 2)));
                                int md_x = 0;
                                if (i - y_v != 0)
                                    md_x = (i - y_v) / Mathf.Abs(i - y_v);
                                lt_j.Add(x_v + md_x * (i - y_v) - lv_class_for.r - 2);
                                if (x_v + md_x * (i - y_v) - lv_class_for.r - 2 != x_v - md_x * (i - y_v) + lv_class_for.r + 2)
                                    lt_j.Add(x_v - md_x * (i - y_v) + lv_class_for.r + 2);
                                foreach (int j in lt_j)
                                {
                                    if (j > 0 && j < size_global - 1)
                                    {
                                        if (j - 1 > 0 && j + 1 < size_global - 1 && i - 1 > 0 && i + 1 < size_global - 1)
                                        {
                                            bool bl_for = true;
                                            if (!(nature[j - 1, i] != null && !nature[(int)j - 1, (int)i].GetComponent<par_nat>().undes))
                                                bl_for = false;
                                            if (!(nature[j + 1, i] != null && !nature[(int)j + 1, (int)i].GetComponent<par_nat>().undes))
                                                bl_for = false;
                                            if (!(nature[j, i - 1] != null && !nature[(int)j, (int)i - 1].GetComponent<par_nat>().undes))
                                                bl_for = false;
                                            if (!(nature[j, i + 1] != null && !nature[(int)j, (int)i + 1].GetComponent<par_nat>().undes))
                                                bl_for = false;

                                            //
                                            if (nature[j, i] != null && bl_for)
                                            {
                                                Debug.Log(v2_ + "  " + j + " " + i + "  set(0)   " + bl_for);

                                                //Destroy(nature[j, i]);
                                                //nature[j, i] = null;

                                                int x_d = 0;
                                                int y_d = 0;

                                                if (Random.Range(0, 2) == 0)
                                                {
                                                    if (j > x_v)
                                                        x_d = -1;
                                                    else
                                                    {
                                                        x_d = 1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (i > y_v)
                                                        y_d = -1;
                                                    else
                                                    {
                                                        y_d = 1;
                                                    }
                                                }
                                                d_v2.Add(new Vector2(j, i), new Vector2(j + x_d, i + y_d));



                                                //Destroy(nature[j + x_d, i + y_d]);
                                                //nature[j + x_d, i + y_d] = null;

                                                //create("setka", j + x_d, i + y_d);


                                            }

                                        }
                                    }
                                }

                            }
                        }
                        if (d_v2.Count > 0)
                        {
                            //Debug.Break();
                            List<Vector2> v2_keys = new List<Vector2>(d_v2.Keys);

                            Vector2 v_k = (Vector2)rand_list<Vector2>(v2_keys);
                            Vector2 v1 = d_v2[v_k];

                            Destroy(nature[(int)v_k.x, (int)v_k.y]);
                            nature[(int)v_k.x, (int)v_k.y] = null;

                            if (Random.Range(0, 3) != 0)
                            {
                                create("skelet", (int)v_k.x, (int)v_k.y);
                            }

                            Destroy(nature[(int)v1.x, (int)v1.y]);
                            nature[(int)v1.x, (int)v1.y] = null;

                            create("setka", (int)v1.x, (int)v1.y);

                            nature[(int)v_k.x - 1, (int)v_k.y].GetComponent<par_nat>().undes = true;
                            nature[(int)v_k.x + 1, (int)v_k.y].GetComponent<par_nat>().undes = true;
                            nature[(int)v_k.x, (int)v_k.y - 1].GetComponent<par_nat>().undes = true;
                            nature[(int)v_k.x, (int)v_k.y + 1].GetComponent<par_nat>().undes = true;

                            nature[(int)v1.x, (int)v1.y].GetComponent<par_nat>().undes = false;


                            Debug.Log(v_k + "  set   " + v1);
                        }

                        lv_v2_for.Remove(v2_);
                        //int x_rd = 
                    }



                    //}
                }
            }




        }





        //nat[st_x, st_y] = false;
        gm_wolk_friend = GameObject.Find(data.data_now.pl_name_now);

        Debug.Log("end rd");
        //if(false)
        //nums_mob_wolk.Add(0);

        ui.print_carts_tip(0);
        ui.print_carts_art(0);
        ui.update_pn_mob(0);



        if (!(num_lev == 0))
        {

            foreach (GameObject gm in lt_fr)
            {
                nums_mob_wolk.Add(gm.GetComponent<par_mob>().num);
            }
        }
        foreach (GameObject gm in lt_fr)
        {
            while (true)
            {
                int x_rd = Random.Range(0, 4) + st_x - 2;
                int y_rd = Random.Range(0, 4) + st_y - 2;
                //x_rd = st_x;
                //y_rd = st_y;
                if (object_bool(x_rd, y_rd))
                {
                    gm.GetComponent<par_mob>().x = x_rd;
                    gm.GetComponent<par_mob>().y = y_rd;
                    gm.GetComponent<par_mob>().wolk(x_rd, y_rd, true);
                    break;
                }
                //nums_mob_wolk.Add(gm_wolk_friend.GetComponent<par_mob>().num);

            }

        }

        foreach (int id in nums_mob_wolk)
        {
            mob_list[id].GetComponent<par_mob>().update_viz();
        }
        num_lev++;

        GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<Animator>().Play("down", -1, 0);
        GameObject.Find("tx lvl").GetComponent<Animator>().Play("invize", -1, 0);
        GameObject.Find("tx lvl").GetComponent<Text>().text = num_lev + " этаж";

        add.load_add();



        //test_message = "good";
        //GameObject.Find("player").GetComponent<par_mob>().wolk(st_x, st_y);

    }
    #endregion

    #region effects
    public static GameObject create_effect(string name, int x, int y)
    {
        GameObject gm = create_effect(name, x, y, null, true, 0, null);
        return gm;
    }
    public static GameObject create_effect(string name, int x, int y, GameObject gm_p)
    {
        GameObject gm = create_effect(name, x, y, gm_p, true, 0, null);
        return gm;
    }
    public static GameObject create_effect(string name, int x, int y, GameObject gm_p, int power, List<string> lt_carts)
    {
        GameObject gm = create_effect(name, x, y, gm_p, true, power, lt_carts);
        return gm;
    }
    public static GameObject create_effect(string name, int x, int y, GameObject gm_p, bool bl, int power, List<string> lt_carts)
    {
        GameObject gm = create_effect(name, x, y, gm_p, bl, power, lt_carts, null);
        return gm;
    }


    public static string get_name_effect(List<string> lt_carts, Stack<string> st_par)
    {
        string name = null;
        List<par_cart.type_cart> type_cart = new List<par_cart.type_cart>();

        /*
        foreach (string st in lt_carts)
        {
            type_cart.Add(GameObject.Find(st).GetComponent<par_cart>().type);
        }
        */
        foreach (string st_cart in lt_carts)
        {

            int pw = GameObject.Find(st_cart).GetComponent<par_cart>().lvl;
            //if()

            par_cart.type_cart cart_type = GameObject.Find(st_cart).GetComponent<par_cart>().type;
            //par_cart.type_cart cart_type1 = par_cart.type_cart.bases;

            if (type_cart.Count == 0)
                type_cart.Add(cart_type);
            else
            {
                //par_cart.type_cart pr_cart1 = type_cart[type_cart.Count - 1];
                //cart_type1 = pr_cart1;
                if (cart_type != type_cart[type_cart.Count - 1])
                {
                    type_cart.Add(cart_type);
                }
                else
                {
                    //pw += pr_cart1.lvl;
                }
            }
        }



        if (lt_carts.Count == 1)
        {
            name = lt_carts[0];
        }
        else if (type_cart[0] == par_cart.type_cart.damage)
        {
            if (type_cart[1] == par_cart.type_cart.rock)
            {
                name = "atack rock";
                //type_bf = par_baf.type_baf.bar_rock;
            }

            else if (type_cart[1] == par_cart.type_cart.fire)
            {
                name = "atack fire";
                //type_bf = par_baf.type_baf.bar_fire;
            }

            else if (type_cart[1] == par_cart.type_cart.energy)
            {
                //type_bf = par_baf.type_baf.bar_energy;
                name = "atack energy";
            }

            else if (type_cart[1] == par_cart.type_cart.blood)
            {
                //type_bf = par_baf.type_baf.bar_blood;
                name = "atack blood";
            }
            else if (type_cart[1] == par_cart.type_cart.ice)
            {
                //type_bf = par_baf.type_baf.bar_ice;
                name = "atack ice";
            }
            else if (type_cart[1] == par_cart.type_cart.poition)
            {
                //type_bf = par_baf.type_baf.bar_poition;
                name = "atack poition";
            }
        }
        else if (type_cart[0] == par_cart.type_cart.sp_barier)
        {
            if (type_cart[1] == par_cart.type_cart.rock)
            {
                name = "barier bases rock";
                //type_bf = par_baf.type_baf.bar_rock;
            }

            else if (type_cart[1] == par_cart.type_cart.fire)
            {
                name = "barier bases fire";
                //type_bf = par_baf.type_baf.bar_fire;
            }

            else if (type_cart[1] == par_cart.type_cart.energy)
            {
                //type_bf = par_baf.type_baf.bar_energy;
                name = "barier bases energy";
            }

            else if (type_cart[1] == par_cart.type_cart.blood)
            {
                //type_bf = par_baf.type_baf.bar_blood;
                name = "barier bases blood";
            }
            else if (type_cart[1] == par_cart.type_cart.ice)
            {
                //type_bf = par_baf.type_baf.bar_ice;
                name = "barier bases ice";
            }
            else if (type_cart[1] == par_cart.type_cart.poition)
            {
                //type_bf = par_baf.type_baf.bar_poition;
                name = "barier bases poition";
            }
        }
        /*
        else if (type_cart[0] == par_cart.type_cart.sp_barier)
        {
            if (type_cart[1] == par_cart.type_cart.rock)
            {
                name = "barier rock";
                //type_bf = par_baf.type_baf.bar_rock;
            }

            else if (type_cart[1] == par_cart.type_cart.fire)
            {
                name = "barier fire";
                //type_bf = par_baf.type_baf.bar_fire;
            }

            else if (type_cart[1] == par_cart.type_cart.energy)
            {
                //type_bf = par_baf.type_baf.bar_energy;
                name = "barier energy";
            }

            else if (type_cart[1] == par_cart.type_cart.blood)
            {
                //type_bf = par_baf.type_baf.bar_blood;
                name = "barier blood";
            }
            else if (type_cart[1] == par_cart.type_cart.ice)
            {
                //type_bf = par_baf.type_baf.bar_ice;
                name = "barier ice";
            }
            else if (type_cart[1] == par_cart.type_cart.poition)
            {
                //type_bf = par_baf.type_baf.bar_poition;
                name = "barier poition";
            }
        }
        */
        else if (type_cart[0] == par_cart.type_cart.derlorg)
        {
            if (type_cart[1] == par_cart.type_cart.damage)
            {
                name = "seal damage";
                //type_bf = par_baf.type_baf.bar_rock;
            }
            else if (type_cart[1] == par_cart.type_cart.sp_barier)
            {
                name = "seal barier";
                //type_bf = par_baf.type_baf.bar_rock;
            }
            else if (type_cart[1] == par_cart.type_cart.rock)
            {
                name = "seal rock";
                //type_bf = par_baf.type_baf.bar_rock;
            }

            else if (type_cart[1] == par_cart.type_cart.fire)
            {
                name = "seal fire";
                //type_bf = par_baf.type_baf.bar_fire;
            }

            else if (type_cart[1] == par_cart.type_cart.energy)
            {
                //type_bf = par_baf.type_baf.bar_energy;
                name = "seal energy";
            }

            else if (type_cart[1] == par_cart.type_cart.blood)
            {
                //type_bf = par_baf.type_baf.bar_blood;
                name = "seal blood";
            }
            else if (type_cart[1] == par_cart.type_cart.ice)
            {
                //type_bf = par_baf.type_baf.bar_ice;
                name = "seal ice";
            }
            else if (type_cart[1] == par_cart.type_cart.poition)
            {
                //type_bf = par_baf.type_baf.bar_poition;
                name = "seal poition";
            }
        }

        return name;
    }

    public static GameObject create_effect(string name, int x, int y, GameObject gm_p, bool bl, int power, List<string> lt_carts, Stack<string> st_par)
    {
        Debug.Log("create effect " + name);

        string name_ = cart_effects[name].name;
        if (name == "cart damage" || name == "cart damage 1" || name == "cart damage 2")
        {
            if (power <= 1)
            {
                name = "cart damage";
            }
            else if (power < 4)
            {
                name = "cart damage 1";
            }
            else
            {
                name = "cart damage 2";
            }
        }
        else if (name == "atack blood" || name == "atack blood 1" || name == "atack blood 2")
        {
            if (power <= 2)
            {
                name = "atack blood";
            }
            else if (power < 8)
            {
                name = "atack blood 1";
            }
            else
            {
                name = "atack blood 2";
            }
        }
        else if (name == "atack fire" || name == "atack fire 1" || name == "atack fire 2")
        {
            if (power <= 2)
            {
                name = "atack fire";
            }
            else if (power < 8)
            {
                name = "atack fire 1";
            }
            else
            {
                name = "atack fire 2";
            }
        }
        else if (name == "atack poition" || name == "atack poition 1" || name == "atack poition 2")
        {
            if (power <= 2)
            {
                name = "atack poition";
            }
            else if (power < 8)
            {
                name = "atack poition 1";
            }
            else
            {
                name = "atack poition 2";
            }
        }
        else if (name == "atack ice" || name == "atack ice 1" || name == "atack ice 2")
        {
            if (power <= 2)
            {
                name = "atack ice";
            }
            else if (power < 8)
            {
                name = "atack ice 1";
            }
            else
            {
                name = "atack ice 2";
            }
        }
        else if (name == "atack rock" || name == "atack rock 1" || name == "atack rock 2")
        {
            if (power <= 2)
            {
                name = "atack rock";
            }
            else if (power < 8)
            {
                name = "atack rock 1";
            }
            else
            {
                name = "atack rock 2";
            }
        }
        else if (name == "atack energy" || name == "atack energy 1" || name == "atack energy 2")
        {
            if (power <= 2)
            {
                name = "atack energy";
            }
            else if (power < 8)
            {
                name = "atack energy 1";
            }
            else
            {
                name = "atack energy 2";
            }
        }


        else if (name == "cart blood" || name == "cart blood 1" || name == "cart blood 2")
        {
            if (power <= 1)
            {
                name = "cart blood";
            }
            else if (power > 1 && power < 4)
            {
                name = "cart blood 1";
            }
            else if (power >= 4)
            {
                name = "cart blood 2";
            }
        }
        else if (name == "cart rock" || name == "cart rock 1")
        {
            if (power <= 1)
            {
                name = "cart rock";
            }
            if (power > 1)
            {
                name = "cart rock 1";
            }
        }
        else if (name == "cart fire" || name == "cart fire 1")
        {
            if (power <= 1)
            {
                name = "cart fire";
            }
            else if (power > 1)
            {
                name = "cart fire 1";
            }
        }
        //Debug.Log("create effect rez" + name);
        if (power >= 16 && gm_p.name == data.data_now.pl_name_now && name != "berserk ef")
            data.give_achive("большой потенциал");
        GameObject effect = Instantiate(cart_effects[name]);


        if (que_effects[x, y].Count == 0)
            effect.active = bl;
        else
            effect.active = false;

        //que_effects[x,y].
        effect.name = name_;
        par_effect par_ef = effect.GetComponent<par_effect>();
        par_ef.name_in_lt = name;
        par_ef.bl_que = true;
        par_ef.parent = gm_p;

        par_ef.cart_par = new List<string>(lt_carts);
        par_ef.power = power;
        par_ef.x = x;
        par_ef.y = y;
        effect.transform.position = new Vector3(x, y);
        anim.col_anim++;

        que_effects[x, y].Enqueue(effect);
        //cart_effects.
        return effect;
    }

    public static GameObject create_effect(string name, string cart, Vector3 v3, bool bl)
    {
        Debug.Log("spawn art " + name);
        //que_effects[1,2].
        string name_ = name;
        GameObject effect = Instantiate(GameObject.Find("effects").transform.Find(name).gameObject);
        if (!cart_spawn.lt_v3.ContainsKey(v3))
        {
            effect.active = bl;
            cart_spawn.lt_v3.Add(v3, new Queue<GameObject>());
            cart_spawn.lt_v3[v3].Enqueue(effect);
        }

        else
        {
            effect.active = false;
            cart_spawn.lt_v3[v3].Enqueue(effect);
        }
        foreach (Vector3 v3_ in cart_spawn.lt_v3.Keys)
        {
            Debug.Log(v3_ + " " + cart_spawn.lt_v3[v3_].Count);
        }

        effect.name = name_;
        par_effect par_ef = effect.GetComponent<par_effect>();
        //if()
        //par_ef.power = GameObject.Find(name).GetComponent<par_cart>().lvl;
        par_ef.cart_par.Add(cart);
        effect.transform.position = v3;
        anim.col_anim++;
        //cart_effects.
        return effect;
    }
    public static GameObject create_lovyshka(int x, int y, string name, GameObject parent, int time, int power)
    {
        return create_lovyshka(x, y, name, parent, time, power, false);
    }
    public static GameObject create_lovyshka(int x, int y, string name, GameObject parent, int time, int power,bool skip)
    {
        Debug.Log("create lovyshka");
        string name_ = name;

        GameObject lovyshka = null;
        foreach (GameObject gm in lt_lovyshka[x, y])
        {
            if (gm.name == name_)
            {
                lovyshka = gm;
                break;
            }
        }
        if (lovyshka != null)
        {
            par_lovyshka lov = lovyshka.GetComponent<par_lovyshka>();
            lov.time += time;
        }
        else
        {
            lovyshka = Instantiate(GameObject.Find("effects").transform.Find("lovyshka").Find(name).gameObject);
            lovyshka.name = name_;
            par_lovyshka lov = lovyshka.GetComponent<par_lovyshka>();
            lov.x = x;
            lov.y = y;
            lov.time = time;
            lov.power = power;
            lov.parent = parent;
            if (parent!=null && parent.GetComponent<par_mob>().num==0)
                lov.skip_first = true;
            else
                lov.skip_first = skip;


            lovyshka_now.Add(lovyshka);
            if (!lov.before_hod)
                lovyshka_after.Add(lovyshka);
            lovyshka.active = eath[x, y].active;
            lt_lovyshka[x, y].Add(lovyshka);
            lovyshka.transform.position = new Vector2(x, y);
        }


        return lovyshka;
    }
    #endregion
    public static bool stay_friend_atack_now()
    {
        return mob_list[hod_mob_now_int].GetComponent<par_mob>().lt_carts_active_now.Count == 0;
    }
    public static bool stay_friend_wolk_now()
    {
        return mob_list[hod_mob_now_int].GetComponent<par_mob>().lt_wolk.Count == 0;
    }
    public static par_mob mob_now()
    {
        return mob_list[hod_mob_now_int].GetComponent<par_mob>();
    }
    public static void next_hod_art_bool()
    {
        ////test_message = "error wolk mob";
        int num = 0;
        //Debug.Log("next hod art " + nums_mob_wolk.Count);
        ////test_message = "error wolk mob "+num;
        num = nums_mob_wolk[nums_mob_wolk.Count - 1];
        if (mob_list[num] != null)
        {
            par_mob pr_mb = mob_list[num].GetComponent<par_mob>();
            bool rt = false;

            if (pr_mb.lt_carts_art.Count > 0 && !pr_mb.speed_hod)
            {
                int a = 0;

                foreach (par_cart_art art in pr_mb.lt_carts_art)
                {
                    if (art.perez_now > 0 && art.name != "cart berserk" && art.name != "cart berserk boss")
                        art.perez_now--;
                    if (art.perez_now == 0 && art.name != "cart berserk" && art.name != "cart berserk boss")
                    {
                        //Debug.Log("activate art =" + art.name+"  "+num);
                        //pr_mb.activate_cart_art(art.name));

                        //bl = 
                        if (art.q < art.q_max && art.q_max > 0)
                        {
                            if (art.type_q_now == par_cart_art.type_q.standart)
                            {
                                art.q += art.q_sum;
                                art.perez_now = art.perez;
                            }
                            else if (art.q == 0)
                            {
                                art.q += art.q_sum;
                            }
                            if (art.q > art.q_max)
                                art.q = art.q_max;
                        }
                        else if (art.q_max == 0)
                            pr_mb.activate_art(art);
                        /*
                        if (art.name == "cart sp rock")
                             
                            bl = pr_mb.spawn_cart("cart rock", lt_art[a]);
                        else if (art.name == "cart sp blood")
                            bl = pr_mb.spawn_cart("cart blood", lt_art[a]);
                        else if (art.name == "cart sp rock 1")
                            bl = pr_mb.spawn_cart("cart rock 1", lt_art[a]);
                        */
                        //if (bl)
                        //art.perez_now = art.perez;
                    }
                    a++;
                }
            }

        }
        next_hod_art();
    }

    public static void next_hod_bool()
    {
        //

        int num = 0;
        //test_message = "error wolk mob " + num.ToString() + " 1859";



        num = nums_mob_wolk[nums_mob_wolk.Count - 1];
        hod_mob_now_int = num;
        Debug.Log("next hod " + num);
        if (num == 0)
            data.save_data();

        if (mob_list[num] != null)
        {


            par_mob pr_mob = mob_list[num].GetComponent<par_mob>();

            //Debug.Log("speed " + pr_mob.speed_now +"  "+(pr_mob.speed_hod));
            bool sp = false;
            bool sp_del = false;

            bool skip_hop = false;
            bool bl_at = true;
            bool bl_add_hod = false;

            bool bl_at_now = pr_mob.atacked;

            pr_mob.hod_now = true;
            pr_mob.atacked = false;

            if (pr_mob.friend)
            {
                GameObject gg = GameObject.Find("im baf");
                //gg.GetComponent<SprireRenderer>
                gg.GetComponent<Animator>().SetBool("energy_anim", (pr_mob.speed_now < pr_mob.speed) && (pr_mob.speed > 1));
                gg.GetComponent<Animator>().SetBool("ice_anim", (pr_mob.speed_now <= 0) && (pr_mob.speed <= 0));
            }
            //Debug.Log("speed hod " + pr_mob.speed_now);
            if (pr_mob.speed_now > 0)
            {


                pr_mob.speed_now--;
                if (pr_mob.speed > 0)
                    sp = true;
                /*
                foreach (int id_mob in nums_mob_wolk)
                {
                    Debug.Log(id_mob + " id ,speed = "+ mob_list[id_mob].GetComponent<par_mob>().speed+"   speed now= "+ mob_list[id_mob].GetComponent<par_mob>().speed_now);
                }
                */
                //if(num==0)
                //Debug.Log("  ind num "+num+"  "+ pr_mob.speed_now+"  ");



                //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);

                /*
                if (pr_mob.speed_now == 0 &&pr_mob.friend )
                {
                    gg.GetComponent<Animator>().SetBool("energy_anim", false);
                }
                */


                if (pr_mob.speed_now < pr_mob.speed - 1)
                {
                    //Debug.Log("  ind add " + num);
                    //nums_mob_wolk.Add(num); tyt

                    //nums_mob_wolk.Insert(nums_mob_wolk.Count, num);
                    //bl_add_hod = true;
                    /*
                    if (nums_mob_wolk.Count > 0)
                        nums_mob_wolk.Insert(nums_mob_wolk.Count, num);
                    else
                    {
                        nums_mob_wolk = new List<int>(nums_mob);
                        nums_mob_wolk.Insert(nums_mob_wolk.Count, num);
                    }
                    */

                    /*
                    foreach (int id_mob in nums_mob_wolk)
                    {
                        Debug.Log(id_mob + " id 1");
                    }
                    */
                    pr_mob.atacked = bl_at_now;
                }
                ////test_message = "error wolk mob " + num.ToString() + " 1898 " + (gg.GetComponent<Animator>()==null).ToString()+"]";
                ////test_message = "error wolk mob " + num.ToString() + " 1899 " + (gg.GetComponent<Animator>().GetBool("energy_anim")).ToString() + "]";



                //if(num==0)

            }
            else if (pr_mob.speed_now <= 0 && pr_mob.speed <= 0)
            {
                Debug.Log("ice skip");
                //sp = true;
                pr_mob.speed_now--;
                if (pr_mob.speed_now < pr_mob.speed)
                    sp = true;
                //pr_mob.speed_now++;
                GameObject gg = GameObject.Find("im baf");
                //if(num==0)
                //Debug.Log("  ind num "+num+"  "+ pr_mob.speed_now+"  ");

                /*
                if (pr_mob.speed_now == 0 &&pr_mob.friend )
                {
                    gg.GetComponent<Animator>().SetBool("energy_anim", false);
                }
                */
                skip_hop = true;
                if (pr_mob.friend)
                {
                    //gg.GetComponent<SprireRenderer>
                    gg.GetComponent<Animator>().SetBool("ice_anim", true);
                }
                //if(num==0)

            }
            else
            {

            }

            /*
                if (pr_mob.speed_now == pr_mob.speed - 1 ||(pr_mob.speed == 0))
                    sp = true;
                if (pr_mob.speed_now == 0)
                {
                    sp_del = true;
                    
                    pr_mob.speed_now = pr_mob.speed;
                    //if (num == 0)
                    //
                }*/



            //pr_mob.speed_now = pr_mob.speed;




            if (pr_mob.lt_bafs.Count > 0 && !pr_mob.speed_hod)
            {
                Stack<par_baf> st_bafs = new Stack<par_baf>();
                List<par_baf.type_baf> lt_tp_bf = new List<par_baf.type_baf>();
                foreach (par_baf bafs in pr_mob.lt_bafs)
                {
                    if (bafs.passive)
                    {
                        st_bafs.Push(bafs);
                        lt_tp_bf.Add(bafs.type);
                    }

                }
                int q = st_bafs.Count;
                for (int i = 0; i < q; i++)
                {

                    par_baf bf = st_bafs.Pop();
                    if (bf.before)
                    {

                        /*
if (bf.type == par_baf.type_baf.fire)
{
    pr_mob.damage(1,"none");
}

*/
                        /*
                        if (bf.type == par_baf.type_baf.energy  && bl_add_hod && bf.long_baf>1)
                    {


                            if (nums_mob_wolk.Count > 0)
                                nums_mob_wolk.Insert(nums_mob_wolk.Count, num);
                            else
                            {
                                nums_mob_wolk = new List<int>(nums_mob);
                                nums_mob_wolk.Insert(nums_mob_wolk.Count, num);
                            }
                        */




                        //gg.GetComponent<Animator>().SetBool("energy_anim", false);
                        //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);

                        /*
                            else if (bf.type == par_baf.type_baf.ice  && skip_hop)
                            {
                                if (num == 0)
                                {

                                    if (nums_mob_wolk.Count == 0)
                                        nums_mob_wolk = new List<int>(nums_mob);
                                    //if (sp)
                                    end_hod_all();
                                }

                                /*
                                if (bf.long_baf == 1)
                                {
                                    skip_hop = false;

                                }
                                else
                                */
                        /*
                        bf.skip_first = true;
                        //next_hod();
                        //next_hod_baf();
                        //return;
                        //gg.GetComponent<Animator>().SetBool("energy_anim", false);
                        //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                    }
            */
                        if (bf.type == par_baf.type_baf.seal)
                        {
                            bl_at = false;
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_damage)
                        {
                            create_effect("cart damage", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart damage" });
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_barier)
                        {
                            create_effect("cart barier", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart barier" });
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_fire)
                        {
                            create_effect("cart fire", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart fire" });
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_poition)
                        {
                            create_effect("cart poition", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart poition" });
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_ice)
                        {
                            create_effect("cart ice", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart ice" });
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_energy)
                        {
                            create_effect("cart energy", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart energy" });
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_rock)
                        {
                            create_effect("cart rock", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart rock" });
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_blood)
                        {
                            create_effect("cart blood", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart blood" });
                            //del = true;
                        }

                        else if (bf.type == par_baf.type_baf.rage && bf.long_baf != 1)
                        {
                            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
                            {
                                pr_mob.par_lt_sila.Add(tp.ToString());
                                pr_mob.par_lt_speed.Add(tp.ToString());
                            }
                            //create_effect("cart blood", pr_mob.x, pr_mob.y, mob_list[num], bf.power, new List<string> { "cart blood" });
                            //del = true;
                        }


                        if (pr_mob.hp <= 0)
                        {
                            timer_now = 0.1f;
                            next_hod();
                            return;
                        }
                        else if (bf.long_baf == 1)
                        {

                            lt_tp_bf.Remove(bf.type);
                            bool del = false;
                            /*
                            if (bf.type == par_baf.type_baf.fire)
                            {
                                del = true;
                            }
                            */


                            /*
                                if (bf.type == par_baf.type_baf.energy && sp)
                                {
                                        pr_mob.speed--;
                                    //if (pr_mob.speed_now>1 || pr_mob.speed ==0)
                                        //pr_mob.speed_now--;
                                        //pr_mob.speed_now--;
                                        mob_list[num].GetComponent<Animator>().speed = 1;
                                        del = true;
                                        GameObject gg = GameObject.Find("im baf");


                                    //gg.GetComponent<Animator>().SetBool("energy_anim", false);
                                    //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                                }
                                else if (bf.type == par_baf.type_baf.ice && sp )
                                {
                                    pr_mob.speed++;
                                    //pr_mob.speed_now++;
                                    mob_list[num].GetComponent<Animator>().speed = 1;

                                    mob_list[num].GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                                    del = true;
                                    GameObject gg = GameObject.Find("im baf");
                                    //gg.GetComponent<Animator>().SetBool("energy_anim", false);
                                    //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                                }
                            */

                            if (bf.type == par_baf.type_baf.seal ||
                                bf.type == par_baf.type_baf.seal_damage || bf.type == par_baf.type_baf.seal_barier ||
                                bf.type == par_baf.type_baf.seal_rock || bf.type == par_baf.type_baf.seal_blood ||
                                bf.type == par_baf.type_baf.seal_fire || bf.type == par_baf.type_baf.seal_poition ||
                                bf.type == par_baf.type_baf.seal_energy || bf.type == par_baf.type_baf.seal_ice

                                 || bf.type == par_baf.type_baf.rage
                                )
                            {
                                //bl_at = false;
                                del = true;
                            }
                            if (!lt_tp_bf.Contains(bf.type))
                            {
                                /*
                                    if (bf.type == par_baf.type_baf.energy && sp)
                                    {
                                        Destroy(mob_list[num].transform.Find("energy").gameObject);
                                    }
                                    else if (bf.type == par_baf.type_baf.ice && sp && !bf.skip_first)
                                    {
                                        Destroy(mob_list[num].transform.Find("ice").gameObject);
                                    }
                                */
                                if (bf.type == par_baf.type_baf.seal)
                                {
                                    Destroy(mob_list[num].transform.Find("seal").gameObject);
                                }

                                else if (bf.type == par_baf.type_baf.seal)
                                {
                                    Destroy(mob_list[num].transform.Find("seal").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_damage)
                                {
                                    Destroy(mob_list[num].transform.Find("seal damage baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_barier)
                                {
                                    Destroy(mob_list[num].transform.Find("seal barier baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_blood)
                                {
                                    Destroy(mob_list[num].transform.Find("seal blood baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_rock)
                                {
                                    Destroy(mob_list[num].transform.Find("seal rock baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_ice)
                                {
                                    Destroy(mob_list[num].transform.Find("seal ice baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_energy)
                                {
                                    Destroy(mob_list[num].transform.Find("seal energy baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_poition)
                                {
                                    Destroy(mob_list[num].transform.Find("seal poition baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.seal_fire)
                                {
                                    Destroy(mob_list[num].transform.Find("seal fire baf").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.rage)
                                {
                                    Destroy(mob_list[num].transform.Find("rage").gameObject);
                                }

                            }
                            if (del)
                                pr_mob.lt_bafs.Remove(bf);
                        }

                        if (sp)
                        {
                            if (bf.skip_first)
                                bf.skip_first = false;
                            else
                                bf.baf();
                        }
                    }
                }

            }
            if (num == 0)
                pr_mob.update_bufs();

            /*
            foreach (int id_mob in nums_mob_wolk)
            {
                Debug.Log(id_mob + " id 11 ,speed = " + mob_list[id_mob].GetComponent<par_mob>().speed + "   speed now= " + mob_list[id_mob].GetComponent<par_mob>().speed_now);
            }
            */
            if (skip_hop)
            {
                timer_now = 0.1f;
                next_hod();
                //next_hod_baf();
                return;
            }
            /*
            if (skip_hop)
            {
                if (num == 0 )
                {

                    if(nums_mob_wolk.Count == 0)
                        nums_mob_wolk = new List<int>(nums_mob);
                    if(sp)
                        lovyshka_after_hod();
                }

                next_hod();
                //next_hod_baf();
                return;
            }
            */
            if (mob_list[num].GetComponent<par_mob>().speed_hod)
            {
                //test_message = "error wolk mob " + num.ToString() + " 1989";
                nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                //if (num == 0 && nums_mob_wolk.Count == 0)
                //nums_mob_wolk = new List<int>(nums_mob);
            }
            mob_list[num].GetComponent<par_mob>().speed_hod = false;
            hod_mob_now_baf = num;
            hod_mob_now_baf_attack = bl_at;
            hod_mob_now_baf_sp = sp;

            next_hod_baf();
            /*


            if (mob_list[num].GetComponent<par_mob>().friend )
            {
                //test_message = "error wolk mob " + num.ToString() + " 1998";
                par_mob pr_pl = mob_list[0].GetComponent<par_mob>();
                GameObject.Find("bt up").GetComponent<Animator>().SetBool("opened", carts[pr_pl.x, pr_pl.y] != null && num == 0);


                bool[,] bl = new bool[size_global, size_global];
                //bl = func.recurs(bl, 0, 0);
                for (int i = pr_mob.y - pr_mob.distanse_max; i <= pr_mob.y + pr_mob.distanse_max; i++)
                {
                    if (i >= 0 && i < func.size_global)
                    {
                        for (int j = pr_mob.x - pr_mob.distanse_max; j <= pr_mob.x + pr_mob.distanse_max; j++)
                        {
                            if (j >= 0 && j < func.size_global)
                            {
                                bl[j, i] = (func.nature[j, i] == null || func.nature[j, i].GetComponent<par_nat>().vizible);
                            }
                        }
                    }
                }

                int[,] way_bl = way_bool_and(bl, pr_mob.x, pr_mob.y, pr_mob.distanse_max);
                for (int i = pr_mob.y - pr_mob.distanse_max; i <= pr_mob.y + pr_mob.distanse_max; i++)
                {
                    if (i >= 0 && i < func.size_global)
                    {
                        for (int j = pr_mob.x - pr_mob.distanse_max; j <= pr_mob.x + pr_mob.distanse_max; j++)
                        {
                            if (j >= 0 && j < func.size_global)
                            {
                                if (way_bl[j, i] != -1)
                                {
                                    if (func.nature[j, i] != null && !func.nature[j, i].active)
                                        func.nature[j, i].active = true;
                                    if (func.mob[j, i] != null && !func.mob[j, i].GetComponent<par_mob>().friend && func.mob[j, i].GetComponent<par_mob>().vizited)
                                        func.mob[j, i].GetComponent<par_mob>().viz(true);
                                    if (func.carts[j, i] != null && !func.carts[j, i].active)
                                        func.carts[j, i].active = true;
                                    if(!func.eath[j, i].active)
                                        func.eath[j, i].active = true;
                                }
                            }
                        }
                    }
                }
                //Debug.Log(mob_list[num].name + " baaaf -2");

                    

                if (mob_list[num].GetComponent<par_mob>().wolked)
                {
                    gm_wolk_friend = mob_list[num];
                    hod_mob_now_int = num;
                    hod_mob_now_friend = num;

                    if (mob_list[num].name == "dych fire" || mob_list[num].name == "dych blood" || mob_list[num].name == "dych energy" || mob_list[num].name == "dych ice" || mob_list[num].name == "dych rock" || mob_list[num].name == "dych poition")
                    {
                        //Debug.Log(mob_list[num].name + " baaaf -1");
                        timer_now = 0.1f;
                        next_hod();
                        return;
                    }

                    ui.print_carts_tip(num);
                    ui.print_carts_art(num);
                    ui.update_pn_mob(num);
                    //par_mob pl = GameObject.Find("player").GetComponent<par_mob>();
                    Debug.Log("start hod player " + num.ToString() + "  " + nums_mob_wolk.Count);
                    //GameObject.Find("Main Camera").transform.parent = mob_list[num].transform;
                    GameObject.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);

                    if (num == 0 && nums_mob_wolk.Count == 0)
                    {
                        nums_mob_wolk = new List<int>(nums_mob);
                        data.data_now.cl_carts_speed = 0;
                        lovyshka_after_hod();
   
                    }
                    if (pr_mob.lt_wolk.Count != 0)
                    {
                        if (col_friend > 1)
                            timer_now = -0.1f;
                        pr_mob.wolk_v2();
                        next_hod();
                    }
                }
                else
                {
                    timer_now = 0.1f;
                    next_hod();
                }
                //next_hod();
                //test_message = "good";
                return;
            }
            else if(!mob_list[num].GetComponent<par_mob>().friend &&!pr_mob.nps)
            {
                int dop_r = 0;
                bool bl_wolk_st = false;
                if (!pr_mob.bl_wolk)
                {
                    dop_r = 2;
                }
                bool[,] bl = new bool[size_global, size_global];
                //bl = func.recurs(bl, 0, 0);
                for (int i = pr_mob.y - pr_mob.distanse_max+dop_r; i <= pr_mob.y + pr_mob.distanse_max-dop_r; i++)
                {
                    if (i >= 0 && i < func.size_global)
                    {
                        for (int j = pr_mob.x - pr_mob.distanse_max+dop_r; j <= pr_mob.x + pr_mob.distanse_max-dop_r; j++)
                        {
                            if (j >= 0 && j < func.size_global)
                            {
                                bl[j, i] = (func.nature[j, i] == null || func.nature[j, i].GetComponent<par_nat>().vizible);
                            }
                        }
                    }
                }

                int[,] way_bl = way_bool_and(bl, pr_mob.x, pr_mob.y, pr_mob.distanse_max-dop_r);
                for (int i = pr_mob.y - pr_mob.distanse_max+dop_r; i <= pr_mob.y + pr_mob.distanse_max-dop_r; i++)
                {
                    if (i >= 0 && i < func.size_global)
                    {
                        for (int j = pr_mob.x - pr_mob.distanse_max+dop_r; j <= pr_mob.x + pr_mob.distanse_max-dop_r; j++)
                        {
                            if (j >= 0 && j < func.size_global)
                            {
                                if (way_bl[j, i] != -1)
                                {
                                    if (func.mob[j, i] != null &&( func.mob[j, i].GetComponent<par_mob>().friend|| func.mob[j, i].GetComponent<par_mob>().bl_wolk))
                                        bl_wolk_st = true;
                                }
                            }
                        }
                    }
                }



                data.data_now.cl_carts_speed = 0;
                //test_message = "error wolk mob " + num.ToString() + " 2086";
                //Debug.Log()
                if (!pr_mob.vizited)
                {
                    //Debug.Log("timer is 0.15 mob");
                    timer_now = 0.1f;
                }

                //Debug.Log(mob_list.Count+"  "+num);
                par_mob player = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                //par_mob pr_mob = mob_list[num].GetComponent<par_mob>();
                hod_mob_now_int = num;

                if (player.x > pr_mob.x)
                    pr_mob.simm(1);
                else if (player.x < pr_mob.x)
                    pr_mob.simm(-1);
                bool atack = false;


                //test_message = "error wolk mob " + num.ToString() + " 2106";
                if (!pr_mob.bl_wolk)
                {
                    if (bl_wolk_st)
                    {
                        mob_list[num].GetComponent<par_mob>().bl_wolk = true;
                        try
                        {
                            mob_list[num].GetComponent<Animator>().SetBool("wait", false);
                        }
                        catch
                        {

                        }
                    }

                    next_hod();
                    return;
                }
                

                if (pr_mob.vizited && bl_at)
                {
                    atack = atack_mob_auto(num);
                }
                if (!atack)
                {
                    pr_mob.wolk_mob();
                }

                //next_hod();
            }
            else
            {
                timer_now = 0.1f;
                next_hod();
                return;
            }
            */
        }

        else
        {
            timer_now = 0.1f;
            next_hod();
        }
        //test_message = "good";
    }
    public static void next_hod_baf_bool()
    {




        int num = hod_mob_now_baf;
        bool bl_at = hod_mob_now_baf_attack;
        bool sp = hod_mob_now_baf_sp;


        if (mob_list[num] != null)
        {
            par_mob pr_mob = mob_list[num].GetComponent<par_mob>();

            if (mob_list[num].GetComponent<par_mob>().friend)
            {
                //test_message = "error wolk mob " + num.ToString() + " 1998";
                par_mob pr_pl = mob_list[0].GetComponent<par_mob>();
                GameObject.Find("bt up").GetComponent<Animator>().SetBool("opened", carts[pr_pl.x, pr_pl.y] != null && num == 0);

                /*
                bool[,] bl = new bool[size_global, size_global];
                //bl = func.recurs(bl, 0, 0);
                for (int i = pr_mob.y - pr_mob.distanse_max; i <= pr_mob.y + pr_mob.distanse_max; i++)
                {
                    if (i >= 0 && i < func.size_global)
                    {
                        for (int j = pr_mob.x - pr_mob.distanse_max; j <= pr_mob.x + pr_mob.distanse_max; j++)
                        {
                            if (j >= 0 && j < func.size_global)
                            {
                                bl[j, i] = (func.nature[j, i] == null || func.nature[j, i].GetComponent<par_nat>().vizible);
                            }
                        }
                    }
                }

                int[,] way_bl = way_bool_and(bl, pr_mob.x, pr_mob.y, pr_mob.distanse_max);
                for (int i = pr_mob.y - pr_mob.distanse_max; i <= pr_mob.y + pr_mob.distanse_max; i++)
                {
                    if (i >= 0 && i < func.size_global)
                    {
                        for (int j = pr_mob.x - pr_mob.distanse_max; j <= pr_mob.x + pr_mob.distanse_max; j++)
                        {
                            if (j >= 0 && j < func.size_global)
                            {
                                if (way_bl[j, i] != -1)
                                {
                                    if (func.nature[j, i] != null && !func.nature[j, i].active)
                                        func.nature[j, i].active = true;
                                    if (func.mob[j, i] != null && !func.mob[j, i].GetComponent<par_mob>().friend && !func.mob[j, i].GetComponent<par_mob>().vizited)
                                        func.mob[j, i].GetComponent<par_mob>().viz(true);
                                    if (func.carts[j, i] != null && !func.carts[j, i].active)
                                        func.carts[j, i].active = true;
                                    if (!func.eath[j, i].active)
                                        func.eath[j, i].active = true;
                                }
                            }
                        }
                    }
                }
                */
                //Debug.Log(mob_list[num].name + " baaaf -2");



                if (mob_list[num].GetComponent<par_mob>().wolked)
                {
                    gm_wolk_friend = mob_list[num];
                    //hod_mob_now_int = num;
                    hod_mob_now_friend = num;

                    if (mob_list[num].name == "dych fire" || mob_list[num].name == "dych blood" || mob_list[num].name == "dych energy" || mob_list[num].name == "dych ice" || mob_list[num].name == "dych rock" || mob_list[num].name == "dych poition" || pr_mob.friend_type == par_mob.type_friend.disactive)
                    {
                        //Debug.Log(mob_list[num].name + " baaaf -1");
                        timer_now = 0.1f;
                        next_hod();
                        return;
                    }

                    bool atack = false;
                    if (pr_mob.friend_type == par_mob.type_friend.auto)
                    {
                        if (bl_at)
                        {
                            atack = atack_mob_auto(num);
                        }


                        if (!atack && pr_mob.vizited)
                        {
                            pr_mob.wolk_mob();
                        }
                        else
                        {
                            timer_now = 0.1f;
                            next_hod();
                        }
                    }

                    /*
                    if (pr_mob.vizited && bl_at)
                    {
                        atack = atack_mob_auto(num);
                    }
                    if (!atack && pr_mob.vizited)
                    {
                        pr_mob.wolk_mob();
                    }
                    */


                    //par_mob pl = GameObject.Find("player").GetComponent<par_mob>();
                    Debug.Log("start hod friend " + num.ToString() + "  " + nums_mob_wolk.Count);
                    if (pr_mob.friend_type == par_mob.type_friend.active)
                    {
                        ui.print_carts_tip(num);
                        ui.print_carts_art(num);
                        ui.update_pn_mob(num);
                        GameObject.Find("Main Camera").transform.parent = mob_list[num].transform;
                        GameObject.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
                    }


                    if (num == 0)
                    {
                        if (nums_mob_wolk.Count == 0)
                            nums_mob_wolk = new List<int>(nums_mob);

                        if (sp)
                        {
                            //data.data_now.cl_carts_speed = 0;
                            //end_hod_all();
                        }


                    }
                    if (pr_mob.lt_wolk.Count != 0)
                    {
                        if (col_friend > 1)
                            timer_now = -0.1f;
                        pr_mob.wolk_v2();
                        next_hod();
                    }
                }
                else
                {
                    timer_now = 0.1f;
                    next_hod();
                }
                //next_hod();
                //test_message = "good";
                return;
            }
            else if (!mob_list[num].GetComponent<par_mob>().friend && !pr_mob.nps)
            {
                int dop_r = 0;
                bool bl_wolk_st = false;
                if (!pr_mob.bl_wolk)
                {
                    dop_r = 2;
                }
                if (mob_list[num].name == "dych fire" || mob_list[num].name == "dych blood" || mob_list[num].name == "dych energy" || mob_list[num].name == "dych ice" || mob_list[num].name == "dych rock" || mob_list[num].name == "dych poition")
                {
                    //Debug.Log(mob_list[num].name + " baaaf -1");
                    timer_now = 0.1f;
                    next_hod();
                    return;
                }
                bool[,] bl = new bool[size_global, size_global];
                //float c = Mathf.Abs()
                //bl = func.recurs(bl, 0, 0);
                if (pr_mob.vizited)
                {
                    for (int i = pr_mob.y - pr_mob.distanse_max + dop_r; i <= pr_mob.y + pr_mob.distanse_max - dop_r; i++)
                    {
                        if (i >= 0 && i < func.size_global)
                        {
                            for (int j = pr_mob.x - pr_mob.distanse_max + dop_r; j <= pr_mob.x + pr_mob.distanse_max - dop_r; j++)
                            {
                                if (j >= 0 && j < func.size_global)
                                {
                                    bl[j, i] = (func.nature[j, i] == null || func.nature[j, i].GetComponent<par_nat>().vizible);
                                }
                            }
                        }
                    }

                    int[,] way_bl = way_bool_and(bl, pr_mob.x, pr_mob.y, pr_mob.distanse_max - dop_r);
                    for (int i = pr_mob.y - pr_mob.distanse_max + dop_r; i <= pr_mob.y + pr_mob.distanse_max - dop_r; i++)
                    {
                        if (i >= 0 && i < func.size_global)
                        {
                            for (int j = pr_mob.x - pr_mob.distanse_max + dop_r; j <= pr_mob.x + pr_mob.distanse_max - dop_r; j++)
                            {
                                if (j >= 0 && j < func.size_global)
                                {
                                    if (way_bl[j, i] != -1)
                                    {
                                        if (func.mob[j, i] != null && (func.mob[j, i].GetComponent<par_mob>().friend || func.mob[j, i].GetComponent<par_mob>().bl_wolk))
                                            bl_wolk_st = true;
                                    }
                                }
                            }
                        }
                    }
                }




                data.data_now.cl_carts_speed = 0;
                //test_message = "error wolk mob " + num.ToString() + " 2086";
                //Debug.Log()
                if (!pr_mob.vizited)
                {
                    //Debug.Log("timer is 0.15 mob");
                    timer_now = 0.1f;
                }

                //Debug.Log(mob_list.Count+"  "+num);
                par_mob player = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                //par_mob pr_mob = mob_list[num].GetComponent<par_mob>();
                //hod_mob_now_int = num;

                if (player.x > pr_mob.x)
                    pr_mob.simm(1);
                else if (player.x < pr_mob.x)
                    pr_mob.simm(-1);
                bool atack = false;


                //test_message = "error wolk mob " + num.ToString() + " 2106";
                if (!pr_mob.bl_wolk)
                {
                    if (bl_wolk_st)
                    {
                        mob_list[num].GetComponent<par_mob>().bl_wolk = true;
                        try
                        {
                            mob_list[num].GetComponent<Animator>().SetBool("wait", false);
                        }
                        catch
                        {

                        }
                    }
                    timer_now = 0.1f;
                    next_hod();
                    return;
                }


                if (pr_mob.vizited && bl_at)
                {
                    atack = atack_mob_auto(num);
                }
                if (!atack && pr_mob.vizited)
                {
                    pr_mob.wolk_mob();
                }
                else
                {
                    timer_now = 0.1f;
                    next_hod();
                }
                //next_hod();
            }
            else
            {
                timer_now = 0.1f;
                next_hod();
                return;
            }
        }
        else
        {
            timer_now = 0.1f;
            next_hod();
            //return;
        }

    }
    public class atack_cart
    {
        public Queue<string> que_cart = new Queue<string>();
        public GameObject cel;
        public atack_cart(Queue<string> que,GameObject cel)
        {
            que_cart = que;
            this.cel = cel;
        }
        public atack_cart(string cart1, GameObject cel)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue(cart1);
            que_cart = q;
            this.cel = cel;
        }
        public atack_cart(string cart1,string cart2, GameObject cel)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue(cart1);
            q.Enqueue(cart2);
            que_cart = q;
            this.cel = cel;
        }
        public atack_cart(string cart1, string cart2,string cart3, GameObject cel)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue(cart1);
            q.Enqueue(cart2);
            q.Enqueue(cart3);
            que_cart = q;
            this.cel = cel;
        }
    }

    public static List<atack_cart> base_proverka_atack(par_mob mb_now)
    {
        List<atack_cart> lt_at = new List<atack_cart>();
        List<GameObject> lt_gm = new List<GameObject>();

        foreach (Vector2 v2 in mb_now.lt_v2_vizited)
        {
           
            List<atack_cart> lt_at_now = new List<atack_cart>();
            par_mob mb = null;
            int v_x = (int)v2.x;
            int v_y = (int)v2.y;
            if (func.mob[v_x, v_y] != null)
                mb = func.mob[v_x, v_y].GetComponent<par_mob>();
            if (distance(v_x,v_y,mb_now.x,mb_now.y) <= mb_now.distanse_min)
            {
                if (mb!=null && mb.friend!=mb_now.friend)
                {
                    if (mb_now.type_lt_attack.Contains(type_atack_mob.damage))
                    {
                        if (mb_now.lt_carts.Contains("cart damage"))
                            lt_at_now.Add(new atack_cart("cart damage", mob[v_x,v_y]));
                        if (mb_now.lt_carts.Contains("cart damage 1"))
                            lt_at_now.Add(new atack_cart("cart damage 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart damage 2"))
                            lt_at_now.Add(new atack_cart("cart damage 2", mob[v_x, v_y]));
                    }
                    if (mb_now.type_lt_attack.Contains(type_atack_mob.rock) || mb_now.type_lt_attack.Contains(type_atack_mob.elemental))
                    {
                        if (mb_now.lt_carts.Contains("cart rock"))
                            lt_at_now.Add(new atack_cart("cart rock", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart rock 1"))
                            lt_at_now.Add(new atack_cart("cart rock 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart rock 2"))
                            lt_at_now.Add(new atack_cart("cart rock 2", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart spirit rock"))
                            lt_at_now.Add(new atack_cart("cart spirit rock", mob[v_x, v_y]));
                    }
                    if (mb_now.type_lt_attack.Contains(type_atack_mob.ice) || mb_now.type_lt_attack.Contains(type_atack_mob.elemental))
                    {
                        if (mb_now.lt_carts.Contains("cart ice"))
                            lt_at_now.Add(new atack_cart("cart ice", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart ice 1"))
                            lt_at_now.Add(new atack_cart("cart ice 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart ice 2"))
                            lt_at_now.Add(new atack_cart("cart ice 2", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart spirit ice"))
                            lt_at_now.Add(new atack_cart("cart spirit ice", mob[v_x, v_y]));
                    }
                    if (mb_now.type_lt_attack.Contains(type_atack_mob.poition) || mb_now.type_lt_attack.Contains(type_atack_mob.elemental))
                    {
                        if (mb_now.lt_carts.Contains("cart poition"))
                            lt_at_now.Add(new atack_cart("cart poition", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart poition 1"))
                            lt_at_now.Add(new atack_cart("cart poition 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart poition 2"))
                            lt_at_now.Add(new atack_cart("cart poition 2", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart spirit poition"))
                            lt_at_now.Add(new atack_cart("cart spirit poition", mob[v_x, v_y]));
                    }
                    if (mb_now.type_lt_attack.Contains(type_atack_mob.fire) || mb_now.type_lt_attack.Contains(type_atack_mob.elemental))
                    {
                        if (mb_now.lt_carts.Contains("cart fire"))
                            lt_at_now.Add(new atack_cart("cart fire", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart fire 1"))
                            lt_at_now.Add(new atack_cart("cart fire 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart fire 2"))
                            lt_at_now.Add(new atack_cart("cart fire 2", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart spirit fire"))
                            lt_at_now.Add(new atack_cart("cart spirit fire", mob[v_x, v_y]));
                    }



                    if (mb_now.type_lt_attack.Contains(type_atack_mob.crystall))
                    {
                        List<atack_cart> lt_at_now_cr = new List<atack_cart>();
                        List<string> lt_c_cr = new List<string>();
                        lt_c_cr.Add("cart crystall");
                        lt_c_cr.Add("cart crystall 1");
                        lt_c_cr.Add("cart crystall 2");
                        foreach (string st_cr in lt_c_cr)
                        {
                            if (mb_now.lt_carts.Contains(st_cr))
                            {
                                foreach (atack_cart at in lt_at_now)
                                {
                                    Queue<string> q = new Queue<string>();
                                    q.Enqueue(st_cr);
                                    foreach (string st in at.que_cart)
                                    {
                                        q.Enqueue(st);
                                    }

                                    if (!lt_gm.Contains(eath[v_x, v_y]))
                                    {
                                        lt_at_now_cr.Add(new atack_cart(q, eath[v_x, v_y]));
                                        lt_gm.Add(eath[v_x, v_y]);
                                    }
                                    if (v_x > 0 && nature[v_x - 1, v_y] != null && !lt_gm.Contains(eath[v_x - 1, v_y]) && distance(v_x - 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                    {
                                        lt_at_now_cr.Add(new atack_cart(q, eath[v_x - 1, v_y]));
                                        lt_gm.Add(eath[v_x - 1, v_y]);
                                    }
                                    if (v_x < size_global - 1 && nature[v_x + 1, v_y] != null && !lt_gm.Contains(eath[v_x + 1, v_y]) && distance(v_x + 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                    {
                                        lt_at_now_cr.Add(new atack_cart(q, eath[v_x + 1, v_y]));
                                        lt_gm.Add(eath[v_x + 1, v_y]);
                                    }
                                    if (v_y > 0 && nature[v_x, v_y - 1] != null && !lt_gm.Contains(eath[v_x, v_y - 1]) && distance(v_x, v_y - 1, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                    {
                                        lt_at_now_cr.Add(new atack_cart(q, eath[v_x, v_y - 1]));
                                        lt_gm.Add(eath[v_x, v_y - 1]);
                                    }
                                    if (v_y < size_global - 1 && nature[v_x, v_y + 1] != null && !lt_gm.Contains(eath[v_x, v_y + 1]) && distance(v_x, v_y + 1, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                    {
                                        lt_at_now_cr.Add(new atack_cart(q, eath[v_x, v_y + 1]));
                                        lt_gm.Add(eath[v_x, v_y + 1]);
                                    }
                                }
                            }
                        }
                        lt_at_now.AddRange(lt_at_now_cr);
                    }
                }
                else if (mb != null && mb.friend == mb_now.friend)
                {
                    if (mb_now.type_lt_attack.Contains(type_atack_mob.barier))
                    {
                        if (mb_now.lt_carts.Contains("cart barier"))
                            lt_at_now.Add(new atack_cart("cart barier", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart barier 1"))
                            lt_at_now.Add(new atack_cart("cart barier 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart barier 2"))
                            lt_at_now.Add(new atack_cart("cart barier 2", mob[v_x, v_y]));
                    }
                    if ((mb_now.type_lt_attack.Contains(type_atack_mob.blood) || mb_now.type_lt_attack.Contains(type_atack_mob.elemental)) && mb.hp<mb.hp_max)
                    {
                        if (mb_now.lt_carts.Contains("cart blood"))
                            lt_at_now.Add(new atack_cart("cart blood", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart blood 1"))
                            lt_at_now.Add(new atack_cart("cart blood 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart blood 2"))
                            lt_at_now.Add(new atack_cart("cart blood 2", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart spirit blood"))
                            lt_at_now.Add(new atack_cart("cart spirit blood", mob[v_x, v_y]));
                    }
                    if ((mb_now.type_lt_attack.Contains(type_atack_mob.blood) || mb_now.type_lt_attack.Contains(type_atack_mob.elemental)) && mb.hp < mb.hp_max)
                    {
                        if (mb_now.lt_carts.Contains("cart blood"))
                            lt_at_now.Add(new atack_cart("cart blood", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart blood 1"))
                            lt_at_now.Add(new atack_cart("cart blood 1", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart blood 2"))
                            lt_at_now.Add(new atack_cart("cart blood 2", mob[v_x, v_y]));
                        if (mb_now.lt_carts.Contains("cart spirit blood"))
                            lt_at_now.Add(new atack_cart("cart spirit blood", mob[v_x, v_y]));
                    }
                }

            }


            lt_at.AddRange(lt_at_now);
        }

        return lt_at;
    }

    #region atack_mob
    public static bool atack_mob_auto(int num)
    {
        Debug.Log("atack mob auto " + num);
        bool atact = false;
        par_mob pr_mob = mob_list[num].GetComponent<par_mob>();
        par_mob player = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();

        Queue<string> st_carts = new Queue<string>();

        GameObject cel = null;


        List<atack_cart> lt_rd_atack = new List<atack_cart>();

        if (mob_list[num].name == "goblin hiller")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            bool bl_cent = true;
            if (lt.Count > 0 && pr_mob.lt_carts.Contains("cart barier"))
            {
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                bl_cent = false;
                            bl = false;
                        }

                    }
                    if (!mb.friend && bl && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now.Add(obj);
                }
                Debug.Log("hiller atack");

            }
            if (lt_now.Count > 0)
            {
                /*
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                //pr_mob.gameObject.GetComponent<Animator>().SetBool("perehod", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);
                 */

                //atact = true;

                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            /*
            else if (pr_mob.lt_carts.Contains("cart damage") && way(pr_mob.x, pr_mob.y, pr_mob.distanse_min)[player.x, player.y] != -1)
            {
                string type = "cart damage";
                pr_mob.cel = player.gameObject;
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;


                //next_hod();
            }
            */

        }
        if (mob_list[num].name == "goblin" || mob_list[num].name == "defender")
        {
            List<GameObject> lt = new List<GameObject>();
            foreach (GameObject gm in pr_mob.lt_mob_vizited)
            {
                par_mob mb  = gm.GetComponent<par_mob>();
                int v_x = mb.x;
                int v_y = mb.y;
                Vector2 v2 = new Vector2(v_x, v_y);
                if (distance(v2, new Vector2(pr_mob.x, pr_mob.y)) <= pr_mob.distanse_min && mb.friend!= pr_mob.friend)
                {
                    if (pr_mob.lt_carts.Contains("cart damage"))
                    {
                        lt.Add(gm);
                    }


                }



            }

            //List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            //List<object> lt_now = new List<object>();
            GameObject c = rand_list<GameObject>(lt);
            if (pr_mob.lt_carts.Contains("cart damage") && c!=null)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = player.gameObject;
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart damage");
                cel = player.gameObject;
                atact = true;
                //next_hod();
            }

        }
        if (mob_list[num].name == "goblin elit")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_att = new List<object>();
            bool bl_cent = true;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;

                    if (pr_mob.lt_carts.Contains("cart barier"))
                    {
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.bar)
                            {
                                if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                    bl_cent = false;
                                bl = false;
                            }

                        }
                        if (!mb.friend && bl)
                            lt_now.Add(obj);
                    }
                    if (pr_mob.lt_carts.Contains("cart damage"))
                    {
                        if (mb.friend)
                        {
                            lt_now_att.Add(obj);
                            lt_now_att.Add(obj);
                        }
                        //lt_now.Add(obj);
                    }
                }
                //Debug.Log("hiller atack");

            }
            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            int rd = -1;
            if (lt_now_att.Count == 0)
            {
                lt_rd.Remove(0);
                lt_rd.Remove(1);
            }

            if (lt_now.Count == 0)
                lt_rd.Remove(2);

            if (lt_rd.Count > 0)
                rd = lt_rd[Random.Range(0, lt_rd.Count)];


            if (rd == 0 || rd == 1)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else if (rd == 2)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }

            /*
            if (pr_mob.lt_carts.Contains("cart damage") && way(pr_mob.x, pr_mob.y, pr_mob.distanse_min)[player.x, player.y] != -1)
            {
                string type = "cart damage";
                pr_mob.cel = player.gameObject;
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                //next_hod();
            }
            else if (pr_mob.lt_carts.Contains("cart barier") && bl_cent)
            {
                string type = "cart barier";
                pr_mob.cel = mob_list[num];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                //next_hod();
            }
            else if (lt_now.Count > 0)
            {
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                //next_hod();
            }
            */
        }
        if (mob_list[num].name == "org berserk")
        {


            List<GameObject> lt = pr_mob.lt_mob_vizited;

            //List<object> lt_now = new List<object>();
            List<GameObject> lt_now_rage = new List<GameObject>();
            List<GameObject> lt_now_att = new List<GameObject>();

            bool bl_cent = true;

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
            //Debug.Log("true dist -2 " + pr_mob.lt_carts_art.Count);


            
            if (lt.Count > 0)
            {
                foreach (GameObject obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    if (distance(mb.x, mb.y, pr_mob.x, pr_mob.y) <= pr_mob.distanse_min)
                    {
                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        else if (!mb.friend && (pr_mob.lt_carts.Contains("cart rage(boss)")))
                            lt_now_rage.Add(obj);
                    }

                }

            }
            lt_now_rage.Add(pr_mob.gameObject);

            List<int> lt_rd = new List<int>() {0,1 };
            if (lt_now_att.Count == 0)
            {
                lt_rd.Remove(0);
                lt_rd.Remove(1);
            }
                
            if (lt_now_rage.Count == 0)
                lt_rd.Remove(1);
            int rd = -1;
            if (lt_rd.Count > 0)
                rd = lt_rd[Random.Range(0, lt_rd.Count)];


            if (rd == 0)
            {
                //type = "cart damage";
                st_carts.Enqueue("cart crystall");
                st_carts.Enqueue("cart damage");
                cel = rand_list<GameObject>(lt_now_att);
                atact = true;
                //next_hod();
            }
            else if (rd == 1)
            {
                //type = "cart damage";
                //st_carts.Enqueue(type_1);
                st_carts.Enqueue("cart rage(boss)");
                cel = rand_list<GameObject>(lt_now_rage);

                lt_rd_atack.Add(new atack_cart("cart rage(boss)", cel));
                atact = true;
                //next_hod();
            }
            
        }


        if (mob_list[num].name == "goblin shaman")
        {
            List<GameObject> lt_now = new List<GameObject>();
            List<GameObject> lt_now_att = new List<GameObject>();
            List<GameObject> lt_now_att_dm = new List<GameObject>();
            List<GameObject> lt_now_att_spirit = new List<GameObject>();
            List<GameObject> lt_now_baf = new List<GameObject>();
            List<GameObject> lt_now_baf_spirit = new List<GameObject>();

            //List<object> lt_now_dych = new List<object>();





            bool bl_cent = true;

            //List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            //Debug.Log("true dist -2 " + pr_mob.lt_carts_art.Count);
            List<GameObject> lt_cel = new List<GameObject>(pr_mob.lt_mob_vizited);
            lt_cel.Add(pr_mob.gameObject);


                foreach (GameObject obj in lt_cel)
                {
                    bool bl_energy = true;


                par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                if(distance(mb.x,mb.y,pr_mob.x,pr_mob.y)<= pr_mob.distanse_min)
                {
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.energy)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl_energy = false;
                        }

                    }

                    if (mb.friend && pr_mob.lt_carts.Contains("cart damage"))
                        lt_now_att_dm.Add(obj);
                    if (mb.friend && (pr_mob.lt_carts.Contains("cart fire") || pr_mob.lt_carts.Contains("cart ice") || pr_mob.lt_carts.Contains("cart poition") || pr_mob.lt_carts.Contains("cart rock")))
                    {
                        lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart spirit(boss)"))
                        {
                            lt_now_att_spirit.Add(obj);
                        }
                    }
                    else if (!mb.friend && ((pr_mob.lt_carts.Contains("cart energy") && bl_energy) || (pr_mob.lt_carts.Contains("cart blood") && mb.hp < mb.hp_max)))
                    {
                        lt_now_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart spirit(boss)"))
                        {
                            lt_now_baf_spirit.Add(obj);
                        }
                    }
                }


                }

            




            List<int> lt_rd = null;
            lt_rd = new List<int>() { 0, 1, 2, 3, 4 };
            if (lt_now_att.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_att_spirit.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_baf.Count == 0)
                lt_rd.Remove(2);
            if (lt_now_baf_spirit.Count == 0)
                lt_rd.Remove(3);
            if (lt_now_att_dm.Count == 0)
                lt_rd.Remove(4);
            int rd = -1;
            if (lt_rd.Count > 0)
                rd = lt_rd[Random.Range(0, lt_rd.Count)];
            if (rd != -1)
            {
                if (rd == 0)
                {
                    //pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                    cel = rand_list<GameObject>(lt_now_att);
                }
                else if (rd == 1)
                {
                    //pr_mob.cel = (GameObject)lt_now_att_tuman[Random.Range(0, lt_now_att_tuman.Count)];
                    cel = rand_list<GameObject>(lt_now_att_spirit);
                    //cel = (GameObject)lt_now_att_s[Random.Range(0, lt_now_att_tuman.Count)];
                }
                else if (rd == 2)
                {
                    //pr_mob.cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                    //cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                    cel = rand_list<GameObject>(lt_now_baf);
                }
                else if (rd == 3)
                {
                    //pr_mob.cel = (GameObject)lt_now_baf_tuman[Random.Range(0, lt_now_baf_tuman.Count)];
                    //cel = (GameObject)lt_now_baf_tuman[Random.Range(0, lt_now_baf_tuman.Count)];
                    cel = rand_list<GameObject>(lt_now_baf_spirit);
                }
                else if (rd == 4)
                {
                    //pr_mob.cel = (GameObject)lt_now_att_dm[Random.Range(0, lt_now_att_dm.Count)];
                    //cel = (GameObject)lt_now_att_dm[Random.Range(0, lt_now_att_dm.Count)];
                    cel = rand_list<GameObject>(lt_now_att_dm);
                }



                //bool bl_comb = false;

                string type = null;
                string type_1 = null;
                if (rd == 4)
                {

                    List<string> lt_el = new List<string>();
                    if (pr_mob.lt_carts.Contains("cart energy"))
                        lt_el.Add("cart energy");
                    if (pr_mob.lt_carts.Contains("cart ice"))
                        lt_el.Add("cart ice");
                    if (pr_mob.lt_carts.Contains("cart fire"))
                        lt_el.Add("cart fire");
                    if (pr_mob.lt_carts.Contains("cart poition"))
                        lt_el.Add("cart poition");
                    if (pr_mob.lt_carts.Contains("cart blood"))
                        lt_el.Add("cart blood");
                    if (pr_mob.lt_carts.Contains("cart rock"))
                        lt_el.Add("cart rock");

                    if (lt_el.Count > 0)
                        type = rand_list<string>(lt_el);

                    //type = cart_dych;
                }
                else if (rd == 0 || rd == 1)
                {
                    List<string> lt_el = new List<string>();
                    if (pr_mob.lt_carts.Contains("cart ice"))
                        lt_el.Add("cart ice");
                    if (pr_mob.lt_carts.Contains("cart fire"))
                        lt_el.Add("cart fire");
                    if (pr_mob.lt_carts.Contains("cart poition"))
                        lt_el.Add("cart poition");
                    if (pr_mob.lt_carts.Contains("cart rock"))
                        lt_el.Add("cart rock");
                    if (lt_el.Count > 0)
                        type = rand_list<string>(lt_el);
                }
                else if (rd == 2 || rd == 3)
                {
                    bool bl_energy = true;


                    par_mob mb = cel.GetComponent<par_mob>();
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.energy)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl_energy = false;
                        }

                    }

                    List<string> lt_el = new List<string>();
                    if (!mb.friend)
                    {
                        if (pr_mob.lt_carts.Contains("cart blood") && mb.hp < mb.hp_max)
                            lt_el.Add("cart blood");
                        if (pr_mob.lt_carts.Contains("cart energy") && bl_energy)
                            lt_el.Add("cart energy");
                    }
                    if (lt_el.Count > 0)
                        type = rand_list<string>(lt_el);
                }
                if (rd == 1 || rd == 3)
                {
                    type_1 = "cart spirit(boss)";
                }
                else if (rd == 4)
                {
                    type_1 = "cart damage";
                }



                if (type_1 != null && type != null)
                {
                    st_carts.Enqueue(type_1);
                    st_carts.Enqueue(type);
                }
                else if (type != null)
                {
                    //pr_mob.cart_active_now = type;
                    //pr_mob.lt_carts_active_now.Add(type);
                    //pr_mob.remove_cart(type);


                    st_carts.Enqueue(type);
                    //cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                    //atact = true;
                }
                if (type_1 != null || type != null)
                {
                    atact = true;
                }



                /*
                foreach (par_cart_art art in pr_mob.lt_carts_art)
                {
                    if (art.q > 0 && ((par_cart)art).type == par_cart.type_cart.art_boss_shaman)
                    {
                        bl_art = true;
                        break;
                    }

                }


                List<object> lt_eath = way_find_lt(eath, pr_mob.x, pr_mob.y, pr_mob.distanse_min);

                string cart_dych = null;
                List<object> lt_now_eath = new List<object>();


                if (bl_art && (pr_mob.lt_carts.Contains("cart fire") || pr_mob.lt_carts.Contains("cart ice") || pr_mob.lt_carts.Contains("cart poition") || pr_mob.lt_carts.Contains("cart rock") || pr_mob.lt_carts.Contains("cart blood") || pr_mob.lt_carts.Contains("cart energy")))
                {
                    foreach (object gm_eath in lt_eath)
                    {
                        par_eath p_e = ((GameObject)gm_eath).GetComponent<par_eath>();
                        if (object_bool(p_e.x, p_e.y))
                        {
                            lt_now_eath.Add(gm_eath);
                        }
                    }
                    List<string> lt_el = new List<string>();
                    if (pr_mob.lt_carts.Contains("cart energy"))
                        lt_el.Add("cart energy");
                    if (pr_mob.lt_carts.Contains("cart ice"))
                        lt_el.Add("cart ice");
                    if (pr_mob.lt_carts.Contains("cart fire"))
                        lt_el.Add("cart fire");
                    if (pr_mob.lt_carts.Contains("cart poition"))
                        lt_el.Add("cart poition");
                    if (pr_mob.lt_carts.Contains("cart blood"))
                        lt_el.Add("cart blood");
                    if (pr_mob.lt_carts.Contains("cart rock"))
                        lt_el.Add("cart rock");
                    if (lt_el.Count > 0)
                        cart_dych = lt_el[Random.Range(0, lt_el.Count)];

                }


                List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
                Dictionary<GameObject, List<object>> d_dych = new Dictionary<GameObject, List<object>>();
                List<string> lt_name_dych = new List<string> { "dych fire", "dych blood", "dych energy", "dych ice", "dych rock", "dych poition" };

                foreach (GameObject gm_dych in pr_mob.childs)
                {
                    if (gm_dych.name == "dych fire" || gm_dych.name == "dych blood" || gm_dych.name == "dych energy" || gm_dych.name == "dych ice" || gm_dych.name == "dych rock" || gm_dych.name == "dych poition")
                    {
                        par_mob pr_dych = gm_dych.GetComponent<par_mob>();
                        List<object> lt_dych = way_find_lt(mob, pr_dych.x, pr_dych.y, pr_dych.distanse_min);
                        //List<object> eath_dych_lt = way_find_lt(eath, pr_dych.x, pr_dych.y, pr_dych.distanse_min);

                        if (bl_art)
                        {
                            //List<object> eath_dych_lt = way_find_lt(eath, pr_dych.x, pr_dych.y, pr_dych.distanse_min);
                            if (cart_dych == "cart fire" && gm_dych.name == "dych fire")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (cart_dych == "cart poition" && gm_dych.name == "dych poition")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (cart_dych == "cart blood" && gm_dych.name == "dych blood")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (cart_dych == "cart energy" && gm_dych.name == "dych energy")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (cart_dych == "cart ice" && gm_dych.name == "dych ice")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (cart_dych == "cart rock" && gm_dych.name == "dych rock")
                                d_dych.Add(gm_dych, lt_dych);
                        }
                        else if (lt_dych.Count > 0)
                        {
                            if (pr_mob.lt_carts.Contains("cart fire") && gm_dych.name == "dych fire")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (pr_mob.lt_carts.Contains("cart poition") && gm_dych.name == "dych poition")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (pr_mob.lt_carts.Contains("cart ice") && gm_dych.name == "dych ice")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (pr_mob.lt_carts.Contains("cart rock") && gm_dych.name == "dych rock")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (pr_mob.lt_carts.Contains("cart blood") && gm_dych.name == "dych blood")
                                d_dych.Add(gm_dych, lt_dych);
                            else if (pr_mob.lt_carts.Contains("cart energy") && gm_dych.name == "dych energy")
                                d_dych.Add(gm_dych, lt_dych);
                        }
                    }

                }

                List<object> lt_now = new List<object>();
                List<object> lt_now_att = new List<object>();
                List<object> lt_now_att_dm = new List<object>();
                List<object> lt_now_att_tuman = new List<object>();
                List<object> lt_now_baf = new List<object>();
                List<object> lt_now_baf_tuman = new List<object>();

                //List<object> lt_now_dych = new List<object>();





                bool bl_cent = true;


                //Debug.Log("true dist -2 " + pr_mob.lt_carts_art.Count);
                if (lt.Count > 0)
                {


                    foreach (object obj in lt)
                    {
                        bool bl_energy = true;


                        par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.energy)
                            {
                                //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                //bl_cent = false;
                                bl_energy = false;
                            }

                        }

                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att_dm.Add(obj);
                        if (mb.friend && (pr_mob.lt_carts.Contains("cart fire") || pr_mob.lt_carts.Contains("cart ice") || pr_mob.lt_carts.Contains("cart poition") || pr_mob.lt_carts.Contains("cart rock")))
                        {
                            lt_now_att.Add(obj);
                            if (pr_mob.lt_carts.Contains("cart tuman(boss)"))
                            {
                                lt_now_att_tuman.Add(obj);
                            }
                        }
                        else if (!lt_name_dych.Contains(((GameObject)obj).name) && !mb.friend && ((pr_mob.lt_carts.Contains("cart energy") && bl_energy) || (pr_mob.lt_carts.Contains("cart blood") && mb.hp < mb.hp_max)))
                        {
                            lt_now_baf.Add(obj);
                            if (pr_mob.lt_carts.Contains("cart tuman(boss)"))
                            {
                                lt_now_baf_tuman.Add(obj);
                            }
                        }

                    }

                }
                bool bl_att_dych = false;
                if (d_dych.Count > 0)
                {
                    //KeyValuePair<GameObject, List<object>> gm_dych = d_dych.Keys[Random.Range(0, d_dych.Count)];
                    List<GameObject> dych_lt = new List<GameObject>(d_dych.Keys);

                    while (dych_lt.Count > 0)
                    {
                        bl_att_dych = false;
                        GameObject gm_dych = dych_lt[Random.Range(0, dych_lt.Count)];
                        List<object> gm_dych_lt = d_dych[gm_dych];

                        par_mob pr_dych = gm_dych.GetComponent<par_mob>();
                        List<object> eath_dych_lt = way_find_lt(eath, pr_dych.x, pr_dych.y, pr_dych.distanse_min);




                        List<object> lt_att_0 = new List<object>();
                        List<object> lt_att_1 = new List<object>();
                        List<object> lt_att_2 = new List<object>();
                        List<object> lt_att_3 = new List<object>();

                        foreach (object gm_eath in eath_dych_lt)
                        {
                            par_eath p_e = ((GameObject)gm_eath).GetComponent<par_eath>();
                            if (object_bool(p_e.x, p_e.y))
                            {
                                lt_att_3.Add(gm_eath);
                            }
                        }
                        //Debug.Log(gm_dych.name + gm_dych_lt);
                        List<List<object>> rd_dych = null;
                        if (bl_art && Random.Range(0, 2) == 0 && lt_att_3.Count != 0)
                        {
                            rd_dych = new List<List<object>>() { lt_att_3 };
                        }
                        else
                        {
                            if (gm_dych_lt != null)
                            {
                                foreach (object obj in gm_dych_lt)
                                {
                                    bool bl_energy = true;


                                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                                    foreach (par_baf baf in mb.lt_bafs)
                                    {
                                        if (baf.type == par_baf.type_baf.energy)
                                        {
                                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                            //bl_cent = false;
                                            bl_energy = false;
                                        }

                                    }

                                    if (!lt_name_dych.Contains(((GameObject)obj).name) && !mb.friend)
                                    {
                                        if ((gm_dych.name == "dych blood" && mb.hp < mb.hp_max) || (bl_energy && gm_dych.name == "dych energy"))
                                        {
                                            if (pr_mob.lt_carts.Contains("cart tuman(boss)"))
                                                lt_att_0.Add(obj);
                                            lt_att_2.Add(obj);
                                        }

                                    }
                                    else if (((GameObject)obj).GetComponent<par_mob>().friend)
                                    {
                                        if (pr_mob.lt_carts.Contains("cart damage"))
                                            lt_att_1.Add(obj);
                                        if (gm_dych.name == "dych fire" || gm_dych.name == "dych poition" || gm_dych.name == "dych rock" || gm_dych.name == "dych ice")
                                        {
                                            if (pr_mob.lt_carts.Contains("cart tuman(boss)"))
                                                lt_att_0.Add(obj);

                                            lt_att_2.Add(obj);
                                        }
                                    }

                                }
                            }


                            rd_dych = new List<List<object>> { lt_att_0, lt_att_1, lt_att_2, lt_att_3 };
                            if (lt_att_0.Count == 0)
                                rd_dych.Remove(lt_att_0);
                            if (lt_att_1.Count == 0)
                                rd_dych.Remove(lt_att_1);
                            if (lt_att_2.Count == 0)
                                rd_dych.Remove(lt_att_2);
                            if (!(bl_art && lt_att_3.Count != 0))
                                rd_dych.Remove(lt_att_3);
                        }



                        List<object> int_rd = null;
                        if (rd_dych.Count > 0)
                        {
                            int_rd = rd_dych[Random.Range(0, rd_dych.Count)];
                        }
                        if (int_rd != null)
                        {
                            GameObject gm_rd = (GameObject)int_rd[Random.Range(0, int_rd.Count)];

                            string type = null;
                            string type_1 = null;
                            if (int_rd == lt_att_0)
                            {
                                type_1 = "cart tuman(boss)";
                            }
                            else if (int_rd == lt_att_1)
                            {
                                type_1 = "cart damage";

                            }
                            else if (int_rd == lt_att_2)
                            {

                            }
                            if (gm_dych.name == "dych fire")
                                type = "cart fire";
                            else if (gm_dych.name == "dych poition")
                                type = "cart poition";
                            else if (gm_dych.name == "dych rock")
                                type = "cart rock";
                            else if (gm_dych.name == "dych blood")
                                type = "cart blood";
                            else if (gm_dych.name == "dych energy")
                                type = "cart energy";
                            else if (gm_dych.name == "dych ice")
                                type = "cart ice";


                            bl_att_dych = true;

                            //pr_mob.cel = gm_rd;
                            cel = gm_rd;



                            if (int_rd != lt_att_2 && int_rd != lt_att_3)
                            {

                                st_carts.Enqueue(type_1);
                                st_carts.Enqueue(type);
                                //cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                                //atact = true;
                            }
                            else
                            {
                                //pr_mob.cart_active_now = type;
                                //pr_mob.lt_carts_active_now.Add(type);
                                //pr_mob.remove_cart(type);

                                st_carts.Enqueue(type);
                                //cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                                //atact = true;
                            }

                            atact = true;
                        }
                        if (bl_att_dych)
                            break;
                        else
                            dych_lt.Remove(gm_dych);
                    }




                }
                if (!bl_att_dych)
                {
                    List<int> lt_rd = null;
                    if (bl_art && Random.Range(0, 2) == 0 && lt_now_eath.Count != 0)//тут Random.Range(0, 2) == 0
                    {
                        lt_rd = new List<int>() { 5 };
                    }
                    else
                    {
                        lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5 };
                        if (lt_now_att.Count == 0)
                            lt_rd.Remove(0);
                        if (lt_now_att_tuman.Count == 0)
                            lt_rd.Remove(1);
                        if (lt_now_baf.Count == 0)
                            lt_rd.Remove(2);
                        if (lt_now_baf_tuman.Count == 0)
                            lt_rd.Remove(3);
                        if (lt_now_att_dm.Count == 0)
                            lt_rd.Remove(4);
                        if (!(bl_art && lt_now_eath.Count != 0))
                            lt_rd.Remove(5);
                    }

                    int rd = -1;
                    if (lt_rd.Count > 0)
                        rd = lt_rd[Random.Range(0, lt_rd.Count)];
                    if (rd != -1)
                    {
                        if (rd == 0)
                        {
                            //pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                            cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                        }
                        else if (rd == 1)
                        {
                            //pr_mob.cel = (GameObject)lt_now_att_tuman[Random.Range(0, lt_now_att_tuman.Count)];
                            cel = (GameObject)lt_now_att_tuman[Random.Range(0, lt_now_att_tuman.Count)];
                        }
                        else if (rd == 2)
                        {
                            //pr_mob.cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                            cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                        }
                        else if (rd == 3)
                        {
                            //pr_mob.cel = (GameObject)lt_now_baf_tuman[Random.Range(0, lt_now_baf_tuman.Count)];
                            cel = (GameObject)lt_now_baf_tuman[Random.Range(0, lt_now_baf_tuman.Count)];
                        }
                        else if (rd == 4)
                        {
                            //pr_mob.cel = (GameObject)lt_now_att_dm[Random.Range(0, lt_now_att_dm.Count)];
                            cel = (GameObject)lt_now_att_dm[Random.Range(0, lt_now_att_dm.Count)];
                        }
                        else if (rd == 5)
                        {
                            //pr_mob.cel = (GameObject)lt_now_eath[Random.Range(0, lt_now_eath.Count)];
                            cel = (GameObject)lt_now_eath[Random.Range(0, lt_now_eath.Count)];
                        }



                        //bool bl_comb = false;

                        string type = null;
                        string type_1 = null;
                        if (rd == 5)
                        {
                            type = cart_dych;
                        }
                        else if (rd == 4)
                        {

                            List<string> lt_el = new List<string>();
                            if (pr_mob.lt_carts.Contains("cart energy"))
                                lt_el.Add("cart energy");
                            if (pr_mob.lt_carts.Contains("cart ice"))
                                lt_el.Add("cart ice");
                            if (pr_mob.lt_carts.Contains("cart fire"))
                                lt_el.Add("cart fire");
                            if (pr_mob.lt_carts.Contains("cart poition"))
                                lt_el.Add("cart poition");
                            if (pr_mob.lt_carts.Contains("cart blood"))
                                lt_el.Add("cart blood");
                            if (pr_mob.lt_carts.Contains("cart rock"))
                                lt_el.Add("cart rock");

                            if (lt_el.Count > 0)
                                type = lt_el[Random.Range(0, lt_el.Count)];

                            //type = cart_dych;
                        }
                        else if (rd == 0 || rd == 1)
                        {
                            List<string> lt_el = new List<string>();
                            if (pr_mob.lt_carts.Contains("cart ice"))
                                lt_el.Add("cart ice");
                            if (pr_mob.lt_carts.Contains("cart fire"))
                                lt_el.Add("cart fire");
                            if (pr_mob.lt_carts.Contains("cart poition"))
                                lt_el.Add("cart poition");
                            if (pr_mob.lt_carts.Contains("cart rock"))
                                lt_el.Add("cart rock");
                            if (lt_el.Count > 0)
                                type = lt_el[Random.Range(0, lt_el.Count)];
                        }
                        else if (rd == 2 || rd == 3)
                        {
                            bool bl_energy = true;


                            par_mob mb = pr_mob.cel.GetComponent<par_mob>();
                            foreach (par_baf baf in mb.lt_bafs)
                            {
                                if (baf.type == par_baf.type_baf.energy)
                                {
                                    //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                    //bl_cent = false;
                                    bl_energy = false;
                                }

                            }

                            List<string> lt_el = new List<string>();
                            if (!mb.friend)
                            {
                                if (pr_mob.lt_carts.Contains("cart blood") && mb.hp < mb.hp_max)
                                    lt_el.Add("cart blood");
                                if (pr_mob.lt_carts.Contains("cart energy") && bl_energy)
                                    lt_el.Add("cart energy");
                            }
                            if (lt_el.Count > 0)
                                type = lt_el[Random.Range(0, lt_el.Count)];
                        }
                        if (rd == 1 || rd == 3)
                        {
                            type_1 = "cart tuman(boss)";
                        }
                        else if (rd == 4)
                        {
                            type_1 = "cart damage";
                        }



                        if (type_1 != null && type != null)
                        {
                            st_carts.Enqueue(type_1);
                            st_carts.Enqueue(type);
                        }
                        else if (type != null)
                        {
                            pr_mob.cart_active_now = type;
                            pr_mob.lt_carts_active_now.Add(type);
                            pr_mob.remove_cart(type);


                            st_carts.Enqueue(type);
                            //cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                            //atact = true;
                        }
                        if (type_1 != null || type != null)
                        {
                            atact = true;
                        }
                    }

                }

                */
            }
        }


        if (mob_list[num].name == "goblin blood elit" || mob_list[num].name == "blood defender")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_bar_base = new List<object>();
            List<object> lt_now_att_base = new List<object>();
            //bool bl_cent = true;
            int count_friend = 0;
            if (lt.Count > 0)
            {

                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    if (mb.friend)
                        count_friend++;
                    if (pr_mob.lt_carts.Contains("cart blood"))
                    {
                        if (!mb.friend && mb.hp < mb.hp_max)
                            lt_now.Add(obj);
                        bl = true;
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.bar_blood)
                            {
                                //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                //bl_cent = false;
                                bl = false;
                            }

                        }
                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now_att.Add(obj);
                        //else if (mb.friend && pr_mob.lt_carts.Contains("cart barier") && !bl )
                        //lt_now_bar.Add(obj);

                    }
                    bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl = false;
                        }

                    }
                    if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                        lt_now_bar_base.Add(obj);
                    if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now_att_base.Add(obj);

                }
                //Debug.Log("hiller atack");

            }
            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(2);
            int a_rd = -1;

            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
            if (a_rd == 0)
            {
                /*
                string type = "cart blood";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                //st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart blood");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart blood");

                pr_mob.remove_cart(type);
                atact = true;
                */

                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart blood");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart blood");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart blood");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else
            {
                lt_rd = new List<int>() { 0, 1 };
                if (lt_now_bar_base.Count == 0)
                    lt_rd.Remove(0);
                if (lt_now_att_base.Count == 0)
                    lt_rd.Remove(1);
                if (lt_rd.Count > 0)
                    a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
                if (a_rd == 0)
                {
                    /*
                    string type = "cart barier";
                    pr_mob.cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */
                    st_carts.Enqueue("cart barier");
                    cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    atact = true;
                    //next_hod();
                }
                else if (a_rd == 1)
                {
                    /*
                    string type = "cart damage";
                    pr_mob.cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */

                    st_carts.Enqueue("cart damage");
                    cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    atact = true;
                    //next_hod();
                }
                /*
                else if(pr_mob.hp < pr_mob.hp_max && pr_mob.lt_carts.Contains("cart blood"))
                {
                    string type = "cart blood";
                    pr_mob.cel = mob_list[num];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);
                    pr_mob.remove_cart(type);

                    atact = true;
                    //next_hod();
                }
                */
            }

        }
        if (mob_list[num].name == "goblin energy elit" || mob_list[num].name == "energy defender")
        {
            /*
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            if (lt.Count > 0 && pr_mob.lt_carts.Contains("cart energy"))
            {
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    if (!mb.friend && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now.Add(obj);
                }
                //Debug.Log("hiller atack");

            }
            bool bl = false;
            foreach (par_baf baf in pr_mob.lt_bafs)
            {
                if (baf.type == par_baf.type_baf.energy)
                    bl = true;
            }
            if (lt_now.Count > 0 && bl)
            {
                string type = "cart energy";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                next_hod();
            }
            else if ( pr_mob.lt_carts.Contains("cart energy") && !bl)
            {
                Debug.Log("cel energy "+ mob_list[num].name);
                string type = "cart energy";
                pr_mob.cel = mob_list[num];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                next_hod();
            }
            */



            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_bar_base = new List<object>();
            List<object> lt_now_att_base = new List<object>();
            //bool bl_cent = true;
            int count_friend = 0;
            int count_mob = 0;
            if (lt.Count > 0)
            {

                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;

                    if (mb.friend)
                        count_friend++;
                    else
                        count_mob++;
                    if (pr_mob.lt_carts.Contains("cart energy"))
                    {
                        bl = true;
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.energy)
                            {
                                //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                //bl_cent = false;
                                bl = false;
                            }

                        }

                        if (!mb.friend && bl && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now.Add(obj);

                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now_att.Add(obj);
                        //else if (mb.friend && pr_mob.lt_carts.Contains("cart barier") && !bl && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        //lt_now_bar.Add(obj);

                    }
                    bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl = false;
                        }

                    }
                    if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                        lt_now_bar_base.Add(obj);
                    if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now_att_base.Add(obj);

                }
                //Debug.Log("hiller atack");

            }
            if (count_friend == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(2);

            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            bool bl_en = false;
            foreach (par_baf baf in pr_mob.lt_bafs)
            {
                if (baf.type == par_baf.type_baf.energy)
                    bl_en = true;
            }
            if (!bl_en && pr_mob.lt_carts.Contains("cart energy") && count_mob>0)
            {
                /*
                string type = "cart energy";
                pr_mob.cel = mob_list[num];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart energy");
                cel = mob_list[num];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 0 && count_mob>0)
            {
                /*
                string type = "cart energy";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                //st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart energy");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart energy");

                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart energy");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart energy");

                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart energy");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else
            {
                lt_rd = new List<int>() { 0, 1 };
                if (lt_now_bar_base.Count == 0)
                    lt_rd.Remove(0);
                if (lt_now_att_base.Count == 0)
                    lt_rd.Remove(1);
                if (lt_rd.Count > 0)
                    a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
                if (a_rd == 0)
                {
                    /*
                    string type = "cart barier";
                    pr_mob.cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */
                    st_carts.Enqueue("cart barier");
                    cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    atact = true;
                    //next_hod();
                }
                else if (a_rd == 1)
                {
                    /*
                    string type = "cart damage";
                    pr_mob.cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */

                    st_carts.Enqueue("cart damage");
                    cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    atact = true;
                    //next_hod();
                }

            }



        }
        if (mob_list[num].name == "goblin ice elit")
        {
            /*
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            if (pr_mob.lt_carts.Contains("cart fire") && way(pr_mob.x, pr_mob.y, pr_mob.distanse_min)[player.x, player.y] != -1)
            {
                string type = "cart fire";
                pr_mob.cel = player.gameObject;
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                next_hod();
            }
            */


            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_bar_base = new List<object>();
            List<object> lt_now_att_base = new List<object>();
            //bool bl_cent = true;
            int count_friend = 0;
            if (lt.Count > 0)
            {

                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    if (mb.friend)
                        count_friend++;
                    if (pr_mob.lt_carts.Contains("cart ice"))
                    {
                        if (mb.friend && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now.Add(obj);
                        bl = true;
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.bar_fire)
                            {
                                //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                //bl_cent = false;
                                bl = false;
                            }

                        }
                        if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                            lt_now_bar.Add(obj);
                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now_att.Add(obj);
                    }
                    bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl = false;
                        }

                    }
                    if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                        lt_now_bar_base.Add(obj);
                    if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now_att_base.Add(obj);

                }
                //Debug.Log("hiller atack");

            }
            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(2);

            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
            if (a_rd == 0)
            {
                /*
                string type = "cart ice";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                //st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart ice");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart ice");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart ice");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart ice");

                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart ice");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else
            {
                lt_rd = new List<int>() { 0, 1 };
                if (lt_now_bar_base.Count == 0)
                    lt_rd.Remove(0);
                if (lt_now_att_base.Count == 0)
                    lt_rd.Remove(1);

                if (lt_rd.Count > 0)
                    a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
                if (a_rd == 0)
                {

                    /*
                    string type = "cart barier";
                    pr_mob.cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    pr_mob.remove_cart(type);

                    atact = true;

                    st_carts.Enqueue("cart damage");
                    cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    atact = true;
                    */
                    //next_hod();
                    st_carts.Enqueue("cart barier");
                    cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    atact = true;
                }

                else if (a_rd == 1)
                {
                    /*
                    string type = "cart damage";
                    pr_mob.cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */

                    st_carts.Enqueue("cart damage");
                    cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    atact = true;
                    //next_hod();
                }

            }


        }
        if (mob_list[num].name == "goblin poition elit")
        {
            /*
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            if (pr_mob.lt_carts.Contains("cart fire") && way(pr_mob.x, pr_mob.y, pr_mob.distanse_min)[player.x, player.y] != -1)
            {
                string type = "cart fire";
                pr_mob.cel = player.gameObject;
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                next_hod();
            }
            */


            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_bar_base = new List<object>();
            List<object> lt_now_att_base = new List<object>();
            //bool bl_cent = true;
            int count_friend = 0;
            if (lt.Count > 0)
            {

                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    if (mb.friend)
                        count_friend++;
                    if (pr_mob.lt_carts.Contains("cart poition"))
                    {
                        if (mb.friend && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now.Add(obj);
                        bl = true;
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.bar_fire)
                            {
                                //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                //bl_cent = false;
                                bl = false;
                            }

                        }
                        if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                            lt_now_bar.Add(obj);
                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now_att.Add(obj);
                    }
                    bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl = false;
                        }

                    }
                    if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                        lt_now_bar_base.Add(obj);
                    if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now_att_base.Add(obj);

                }
                //Debug.Log("hiller atack");

            }
            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(2);

            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
            if (a_rd == 0)
            {
                /*
                string type = "cart poition";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                //st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart poition");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart poition");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart poition");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart poition");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart poition");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else
            {
                lt_rd = new List<int>() { 0, 1 };
                if (lt_now_bar_base.Count == 0)
                    lt_rd.Remove(0);
                if (lt_now_att_base.Count == 0)
                    lt_rd.Remove(1);

                if (lt_rd.Count > 0)
                    a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
                if (a_rd == 0)
                {
                    /*
                    string type = "cart barier";
                    pr_mob.cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */
                    st_carts.Enqueue("cart barier");
                    cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    atact = true;
                    //next_hod();
                }
                else if (a_rd == 1)
                {
                    /*
                    string type = "cart damage";
                    pr_mob.cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */

                    st_carts.Enqueue("cart damage");
                    cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    atact = true;
                    //next_hod();
                }

            }


        }



        if (mob_list[num].name == "goblin fire elit")
        {


            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_bar_base = new List<object>();
            List<object> lt_now_att_base = new List<object>();
            //bool bl_cent = true;
            int count_friend = 0;
            if (lt.Count > 0)
            {

                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    if (mb.friend)
                        count_friend++;
                    if (pr_mob.lt_carts.Contains("cart fire"))
                    {
                        if (mb.friend && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now.Add(obj);
                        bl = true;
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.bar_fire)
                            {
                                //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                //bl_cent = false;
                                bl = false;
                            }

                        }
                        if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                            lt_now_bar.Add(obj);
                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now_att.Add(obj);
                    }
                    bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl = false;
                        }

                    }
                    if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                        lt_now_bar_base.Add(obj);
                    if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now_att_base.Add(obj);

                }
                //Debug.Log("hiller atack");

            }
            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(2);

            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
            if (a_rd == 0)
            {
                /*
                string type = "cart fire";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                //st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart fire");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart fire");

                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart fire");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart fire");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart fire");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else
            {
                lt_rd = new List<int>() { 0, 1 };
                if (lt_now_bar_base.Count == 0)
                    lt_rd.Remove(0);
                if (lt_now_att_base.Count == 0)
                    lt_rd.Remove(1);

                if (lt_rd.Count > 0)
                    a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
                if (a_rd == 0)
                {
                    /*
                    string type = "cart barier";
                    pr_mob.cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */
                    st_carts.Enqueue("cart barier");
                    cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    atact = true;
                    //next_hod();
                }
                else if (a_rd == 1)
                {
                    /*
                    string type = "cart damage";
                    pr_mob.cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */
                    st_carts.Enqueue("cart damage");
                    cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    atact = true;
                    //next_hod();
                }

            }


        }
        if (mob_list[num].name == "goblin rock elit")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_bar_base = new List<object>();
            List<object> lt_now_att_base = new List<object>();
            //bool bl_cent = true;
            int count_friend = 0;
            if (lt.Count > 0)
            {

                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    if (mb.friend)
                        count_friend++;
                    if (pr_mob.lt_carts.Contains("cart rock"))
                    {
                        if (mb.friend && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now.Add(obj);
                        bl = true;
                        foreach (par_baf baf in mb.lt_bafs)
                        {
                            if (baf.type == par_baf.type_baf.bar_rock)
                            {
                                //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                //bl_cent = false;
                                bl = false;
                            }

                        }
                        if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                            lt_now_bar.Add(obj);
                        if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                            lt_now_att.Add(obj);
                    }
                    bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            //if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                            //bl_cent = false;
                            bl = false;
                        }

                    }
                    if (!mb.friend && pr_mob.lt_carts.Contains("cart barier") && bl)
                        lt_now_bar_base.Add(obj);
                    if (mb.friend && pr_mob.lt_carts.Contains("cart damage") && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                        lt_now_att_base.Add(obj);

                }
                //Debug.Log("hiller atack");

            }
            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(2);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
            if (a_rd == 0)
            {
                /*
                string type = "cart rock";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                //st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart rock");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart rock");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart rock");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart rock");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart rock");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else
            {
                lt_rd = new List<int>() { 0, 1 };
                if (lt_now_bar_base.Count == 0)
                    lt_rd.Remove(0);
                if (lt_now_att_base.Count == 0)
                    lt_rd.Remove(1);
                if (lt_rd.Count > 0)
                    a_rd = lt_rd[Random.Range(0, lt_rd.Count)];
                if (a_rd == 0)
                {
                    /*
                    string type = "cart barier";
                    pr_mob.cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */

                    st_carts.Enqueue("cart barier");
                    cel = (GameObject)lt_now_bar_base[Random.Range(0, lt_now_bar_base.Count)];
                    atact = true;
                    //next_hod();
                }
                else if (a_rd == 1)
                {
                    /*
                    string type = "cart damage";
                    pr_mob.cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                    if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                        pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                    else
                    {
                        pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                    }
                    anim.col_anim++;
                    pr_mob.cart_active_now = type;
                    pr_mob.lt_carts_active_now.Add(type);

                    //pr_mob.lt_carts_added_now.Add(type);
                    //pr_mob.lt_carts_added.Add(type);
                    //pr_mob.lt_carts_added.Add("cart blood");

                    pr_mob.remove_cart(type);

                    atact = true;
                    */

                    st_carts.Enqueue("cart damage");
                    cel = (GameObject)lt_now_att_base[Random.Range(0, lt_now_att_base.Count)];
                    atact = true;
                    //next_hod();
                }

            }





        }
        if (mob_list[num].name == "derlorg")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now1 = new List<object>();
            bool bl_cent = true;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.seal)
                        {
                            if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                bl_cent = false;
                            bl = false;
                        }

                    }
                    if (mb.friend && !(mb.x == pr_mob.x && mb.y == pr_mob.y))
                    {
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now.Add(obj);
                        if (bl && pr_mob.lt_carts.Contains("cart derlorg"))
                        {
                            lt_now1.Add(obj);
                        }
                    }

                }
                //Debug.Log("hiller atack");

            }

            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now1.Count == 0)
                lt_rd.Remove(1);
            if (lt_now.Count == 0 || lt_now1.Count == 0)
                lt_rd.Remove(2);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now1[Random.Range(0, lt_now1.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now1[Random.Range(0, lt_now1.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart damage");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }

            /*
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            if (pr_mob.lt_carts.Contains("cart rock 1") && way(pr_mob.x, pr_mob.y, pr_mob.distanse_min)[player.x, player.y] != -1)
            {
                string type = "cart rock 1";
                pr_mob.cel = player.gameObject;
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                next_hod();
            }
            else if (pr_mob.lt_carts.Contains("cart rock") && way(pr_mob.x, pr_mob.y, pr_mob.distanse_min)[player.x, player.y] != -1)
            {
                string type = "cart rock";
                pr_mob.cel = player.gameObject;
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                next_hod();
            }
            */
        }
        if (mob_list[num].name == "derlorg shaman")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now1 = new List<object>();
            List<object> lt_now2 = new List<object>();
            bool bl_cent = true;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    bool bl_bar = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.seal)
                        {
                            if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                bl_cent = false;
                            bl = false;
                        }
                        else if (baf.type == par_baf.type_baf.bar)
                        {
                            if (mb.x == pr_mob.x && mb.y == pr_mob.y)
                                bl_cent = false;
                            bl_bar = false;
                        }

                    }
                    if (!mb.friend && bl_bar && pr_mob.lt_carts.Contains("cart barier"))
                    {
                        lt_now.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now2.Add(obj);
                    }

                    if (mb.friend && bl && pr_mob.lt_carts.Contains("cart derlorg"))
                    {
                        lt_now1.Add(obj);
                    }


                }
                //Debug.Log("hiller atack");

            }

            List<int> lt_rd = new List<int>() { 0, 1, 2 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now1.Count == 0)
                lt_rd.Remove(1);
            if (lt_now2.Count == 0)
                lt_rd.Remove(2);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now1[Random.Range(0, lt_now1.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now1[Random.Range(0, lt_now1.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now2[Random.Range(0, lt_now2.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart barier");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now2[Random.Range(0, lt_now2.Count)];
                atact = true;
                //next_hod();
            }


        }
        if (mob_list[num].name == "derlorg energy")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now_seal = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_att_baf = new List<object>();
            List<object> lt_now_att_seal = new List<object>();

            List<object> lt_now_bar = new List<object>();
            //List<object> lt_now_bar_baf = new List<object>();
            List<object> lt_now_bar_seal = new List<object>();

            List<object> lt_now_baf = new List<object>();
            List<object> lt_now_baf_seal = new List<object>();
            bool bl_cent = true;
            int count_friend = 0;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    bool bl_bar = true;
                    bool bl_bar_seal = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.energy)
                        {
                            bl = false;
                        }
                        else if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = false;
                        }
                        else if (baf.type == par_baf.type_baf.seal_barier)
                        {
                            bl_bar_seal = false;
                        }
                    }
                    if (mb.friend)
                    {
                        count_friend++;
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart energy") && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_att_seal.Add(obj);
                    }
                    else if (bl)
                    {
                        if (bl)
                        {
                            if (pr_mob.lt_carts.Contains("cart energy"))
                                lt_now_baf.Add(obj);
                            if (pr_mob.lt_carts.Contains("cart energy") && pr_mob.lt_carts.Contains("cart derlorg"))
                                lt_now_baf_seal.Add(obj);
                        }


                        if (pr_mob.lt_carts.Contains("cart barier") && bl_bar && bl_bar_seal)
                            lt_now_bar.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart derlorg") && bl_bar && bl_bar_seal)
                            lt_now_bar_seal.Add(obj);
                    }


                }
                //Debug.Log("hiller atack");

            }
            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5 };
            if (lt_now_seal.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_baf.Count == 0)
                lt_rd.Remove(2);
            if (lt_now_att_baf.Count == 0)
                lt_rd.Remove(3);
            if (lt_now_att_seal.Count == 0)
                lt_rd.Remove(4);
            if (lt_now_baf_seal.Count == 0)
                lt_rd.Remove(5);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                atact = true;
            }
            else if (a_rd == 1)
            {
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
            }
            else if (a_rd == 2)
            {
                st_carts.Enqueue("cart energy");
                cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                atact = true;
            }
            else if (a_rd == 3)
            {
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart energy");
                cel = (GameObject)lt_now_att_baf[Random.Range(0, lt_now_att_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 4)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 5)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart energy");
                cel = (GameObject)lt_now_baf_seal[Random.Range(0, lt_now_baf_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 6)
            {
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
            }
            else if (a_rd == 8)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar_seal[Random.Range(0, lt_now_bar_seal.Count)];
                atact = true;
            }
        }
        if (mob_list[num].name == "derlorg blood")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now_seal = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_att_baf = new List<object>();
            List<object> lt_now_att_seal = new List<object>();

            List<object> lt_now_bar = new List<object>();
            //List<object> lt_now_bar_baf = new List<object>();
            List<object> lt_now_bar_seal = new List<object>();

            List<object> lt_now_baf = new List<object>();
            List<object> lt_now_baf_seal = new List<object>();
            bool bl_cent = true;
            int count_friend = 0;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {


                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();

                    bool bl_bar = true;
                    bool bl_bar_seal = true;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = false;
                        }
                        else if (baf.type == par_baf.type_baf.seal_barier)
                        {
                            bl_bar_seal = false;
                        }

                    }


                    if (mb.friend)
                    {
                        count_friend++;
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart blood") && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_att_seal.Add(obj);
                    }
                    else
                    {
                        if (mb.hp < mb.hp_max)
                        {
                            if (pr_mob.lt_carts.Contains("cart blood"))
                                lt_now_baf.Add(obj);
                            if (pr_mob.lt_carts.Contains("cart blood") && pr_mob.lt_carts.Contains("cart derlorg"))
                                lt_now_baf_seal.Add(obj);
                        }

                        if (pr_mob.lt_carts.Contains("cart barier") && bl_bar && bl_bar_seal)
                            lt_now_bar.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart derlorg") && bl_bar && bl_bar_seal)
                            lt_now_bar_seal.Add(obj);
                    }


                }
                //Debug.Log("hiller atack");

            }

            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }
            List<int> lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 8 };
            if (lt_now_seal.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_baf.Count == 0)
                lt_rd.Remove(2);
            if (lt_now_att_baf.Count == 0)
                lt_rd.Remove(3);
            if (lt_now_att_seal.Count == 0)
                lt_rd.Remove(4);
            if (lt_now_baf_seal.Count == 0)
                lt_rd.Remove(5);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(6);
            if (lt_now_bar_seal.Count == 0)
                lt_rd.Remove(8);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                atact = true;
            }
            else if (a_rd == 1)
            {
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
            }
            else if (a_rd == 2)
            {
                st_carts.Enqueue("cart blood");
                cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                atact = true;
            }
            else if (a_rd == 3)
            {
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart blood");
                cel = (GameObject)lt_now_att_baf[Random.Range(0, lt_now_att_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 4)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 5)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart blood");
                cel = (GameObject)lt_now_baf_seal[Random.Range(0, lt_now_baf_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 6)
            {
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
            }
            else if (a_rd == 8)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar_seal[Random.Range(0, lt_now_bar_seal.Count)];
                atact = true;
            }
        }
        if (mob_list[num].name == "derlorg fire")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now_seal = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_att_baf = new List<object>();
            List<object> lt_now_att_seal = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_bar_baf = new List<object>();
            List<object> lt_now_bar_seal = new List<object>();
            List<object> lt_now_baf = new List<object>();
            List<object> lt_now_baf_seal = new List<object>();
            bool bl_cent = true;

            int count_friend = 0;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    bool bl_bar = true;
                    bool bl_bar_seal = true;
                    bool bl_bar_baf = true;
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = false;
                        }
                        else if (baf.type == par_baf.type_baf.bar_fire)
                        {
                            bl_bar_baf = false;
                        }
                        else if (baf.type == par_baf.type_baf.seal_barier)
                        {
                            bl_bar_seal = false;
                        }

                    }


                    if (mb.friend)
                    {
                        count_friend++;
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart fire") && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_att_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart fire"))
                            lt_now_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart fire") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_baf_seal.Add(obj);
                    }
                    else
                    {
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart fire") && bl_bar_baf)
                            lt_now_bar_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart seal") && bl_bar_seal && bl_bar)
                            lt_now_bar_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && bl_bar_seal && bl_bar)
                            lt_now_bar.Add(obj);
                    }


                }
                //Debug.Log("hiller atack");

            }

            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }

            List<int> lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            if (lt_now_seal.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_baf.Count == 0)
                lt_rd.Remove(2);
            if (lt_now_att_baf.Count == 0)
                lt_rd.Remove(3);
            if (lt_now_att_seal.Count == 0)
                lt_rd.Remove(4);
            if (lt_now_baf_seal.Count == 0)
                lt_rd.Remove(5);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(6);
            if (lt_now_bar_baf.Count == 0)
                lt_rd.Remove(7);
            if (lt_now_bar_seal.Count == 0)
                lt_rd.Remove(8);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                atact = true;
            }
            else if (a_rd == 1)
            {
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
            }
            else if (a_rd == 2)
            {
                st_carts.Enqueue("cart fire");
                cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                atact = true;
            }
            else if (a_rd == 3)
            {
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart fire");
                cel = (GameObject)lt_now_att_baf[Random.Range(0, lt_now_att_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 4)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 5)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart fire");
                cel = (GameObject)lt_now_baf_seal[Random.Range(0, lt_now_baf_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 6)
            {
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
            }
            else if (a_rd == 7)
            {
                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart fire");
                cel = (GameObject)lt_now_bar_baf[Random.Range(0, lt_now_bar_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 8)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar_seal[Random.Range(0, lt_now_bar_seal.Count)];
                atact = true;
            }
        }
        if (mob_list[num].name == "derlorg ice")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now_seal = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_att_baf = new List<object>();
            List<object> lt_now_att_seal = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_bar_baf = new List<object>();
            List<object> lt_now_bar_seal = new List<object>();
            List<object> lt_now_baf = new List<object>();
            List<object> lt_now_baf_seal = new List<object>();
            bool bl_cent = true;

            int count_friend = 0;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    bool bl_bar = true;
                    bool bl_bar_seal = true;
                    bool bl_bar_baf = true;
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();

                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = false;
                        }
                        else if (baf.type == par_baf.type_baf.bar_ice)
                        {
                            bl_bar_baf = false;
                        }
                        else if (baf.type == par_baf.type_baf.seal_barier)
                        {
                            bl_bar_seal = false;
                        }

                    }


                    if (mb.friend)
                    {
                        count_friend++;
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart ice") && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_att_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart ice"))
                            lt_now_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart ice") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_baf_seal.Add(obj);
                    }
                    else
                    {
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart ice") && bl_bar_baf)
                            lt_now_bar_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart seal") && bl_bar_seal && bl_bar)
                            lt_now_bar_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && bl_bar_seal && bl_bar)
                            lt_now_bar.Add(obj);
                    }


                }
                //Debug.Log("hiller atack");

            }

            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }

            List<int> lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            if (lt_now_seal.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_baf.Count == 0)
                lt_rd.Remove(2);
            if (lt_now_att_baf.Count == 0)
                lt_rd.Remove(3);
            if (lt_now_att_seal.Count == 0)
                lt_rd.Remove(4);
            if (lt_now_baf_seal.Count == 0)
                lt_rd.Remove(5);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(6);
            if (lt_now_bar_baf.Count == 0)
                lt_rd.Remove(7);
            if (lt_now_bar_seal.Count == 0)
                lt_rd.Remove(8);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                atact = true;
            }
            else if (a_rd == 1)
            {
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
            }
            else if (a_rd == 2)
            {
                st_carts.Enqueue("cart ice");
                cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                atact = true;
            }
            else if (a_rd == 3)
            {
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart ice");
                cel = (GameObject)lt_now_att_baf[Random.Range(0, lt_now_att_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 4)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 5)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart ice");
                cel = (GameObject)lt_now_baf_seal[Random.Range(0, lt_now_baf_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 6)
            {
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
            }
            else if (a_rd == 7)
            {
                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart ice");
                cel = (GameObject)lt_now_bar_baf[Random.Range(0, lt_now_bar_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 8)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar_seal[Random.Range(0, lt_now_bar_seal.Count)];
                atact = true;
            }
        }
        if (mob_list[num].name == "derlorg poition")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now_seal = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_att_baf = new List<object>();
            List<object> lt_now_att_seal = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_bar_baf = new List<object>();
            List<object> lt_now_bar_seal = new List<object>();
            List<object> lt_now_baf = new List<object>();
            List<object> lt_now_baf_seal = new List<object>();
            bool bl_cent = true;

            int count_friend = 0;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    bool bl_bar = true;
                    bool bl_bar_seal = true;
                    bool bl_bar_baf = true;
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = false;
                        }
                        else if (baf.type == par_baf.type_baf.bar_poition)
                        {
                            bl_bar_baf = false;
                        }
                        else if (baf.type == par_baf.type_baf.seal_barier)
                        {
                            bl_bar_seal = false;
                        }

                    }


                    if (mb.friend)
                    {
                        count_friend++;
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart poition") && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_att_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart poition"))
                            lt_now_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart poition") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_baf_seal.Add(obj);
                    }
                    else
                    {
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart poition") && bl_bar_baf)
                            lt_now_bar_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart seal") && bl_bar_seal && bl_bar)
                            lt_now_bar_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && bl_bar_seal && bl_bar)
                            lt_now_bar.Add(obj);
                    }


                }
                //Debug.Log("hiller atack");

            }

            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }

            List<int> lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            if (lt_now_seal.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_baf.Count == 0)
                lt_rd.Remove(2);
            if (lt_now_att_baf.Count == 0)
                lt_rd.Remove(3);
            if (lt_now_att_seal.Count == 0)
                lt_rd.Remove(4);
            if (lt_now_baf_seal.Count == 0)
                lt_rd.Remove(5);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(6);
            if (lt_now_bar_baf.Count == 0)
                lt_rd.Remove(7);
            if (lt_now_bar_seal.Count == 0)
                lt_rd.Remove(8);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                atact = true;
            }
            else if (a_rd == 1)
            {
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
            }
            else if (a_rd == 2)
            {
                st_carts.Enqueue("cart poition");
                cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                atact = true;
            }
            else if (a_rd == 3)
            {
                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart poition");
                cel = (GameObject)lt_now_att_baf[Random.Range(0, lt_now_att_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 4)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 5)
            {
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart poition");
                cel = (GameObject)lt_now_baf_seal[Random.Range(0, lt_now_baf_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 6)
            {
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 7)
            {


                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart poition");
                cel = (GameObject)lt_now_bar_baf[Random.Range(0, lt_now_bar_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 8)
            {

                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar_seal[Random.Range(0, lt_now_bar_seal.Count)];
                atact = true;
                //next_hod();
            }
        }
        if (mob_list[num].name == "derlorg rock")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now_seal = new List<object>();
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_att_baf = new List<object>();
            List<object> lt_now_att_seal = new List<object>();
            List<object> lt_now_bar = new List<object>();
            List<object> lt_now_bar_baf = new List<object>();
            List<object> lt_now_bar_seal = new List<object>();
            List<object> lt_now_baf = new List<object>();
            List<object> lt_now_baf_seal = new List<object>();
            bool bl_cent = true;

            int count_friend = 0;
            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    bool bl_bar = true;
                    bool bl_bar_seal = true;
                    bool bl_bar_baf = true;
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = false;
                        }
                        else if (baf.type == par_baf.type_baf.bar_rock)
                        {
                            bl_bar_baf = false;
                        }
                        else if (baf.type == par_baf.type_baf.seal_barier)
                        {
                            bl_bar_seal = false;
                        }

                    }


                    if (mb.friend)
                    {
                        count_friend++;
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart rock") && pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_att_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart rock"))
                            lt_now_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart rock") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_baf_seal.Add(obj);
                    }
                    else
                    {
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart rock") && bl_bar_baf)
                            lt_now_bar_baf.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && pr_mob.lt_carts.Contains("cart seal") && bl_bar_seal && bl_bar)
                            lt_now_bar_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart barier") && bl_bar_seal && bl_bar)
                            lt_now_bar.Add(obj);
                    }


                }
                //Debug.Log("hiller atack");

            }

            if (count_friend == 0 && Random.Range(0, 2) == 0)
            {
                return false;
            }

            List<int> lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            if (lt_now_seal.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_att.Count == 0)
                lt_rd.Remove(1);
            if (lt_now_baf.Count == 0)
                lt_rd.Remove(2);
            if (lt_now_att_baf.Count == 0)
                lt_rd.Remove(3);
            if (lt_now_att_seal.Count == 0)
                lt_rd.Remove(4);
            if (lt_now_baf_seal.Count == 0)
                lt_rd.Remove(5);
            if (lt_now_bar.Count == 0)
                lt_rd.Remove(6);
            if (lt_now_bar_baf.Count == 0)
                lt_rd.Remove(7);
            if (lt_now_bar_seal.Count == 0)
                lt_rd.Remove(8);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;

                */
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart rock";
                pr_mob.cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart rock");
                cel = (GameObject)lt_now_baf[Random.Range(0, lt_now_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 3)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att_baf[Random.Range(0, lt_now_att_baf.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart rock");

                pr_mob.remove_cart(type);


                atact = true;
                */

                st_carts.Enqueue("cart damage");
                st_carts.Enqueue("cart rock");
                cel = (GameObject)lt_now_att_baf[Random.Range(0, lt_now_att_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 4)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart damage");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 5)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now_baf_seal[Random.Range(0, lt_now_baf_seal.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart rock");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart rock");
                cel = (GameObject)lt_now_baf_seal[Random.Range(0, lt_now_baf_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 6)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                //pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                //pr_mob.lt_carts_added.Add("cart fire");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar[Random.Range(0, lt_now_bar.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 7)
            {
                /*
                string type = "cart barier";
                pr_mob.cel = (GameObject)lt_now_bar_baf[Random.Range(0, lt_now_bar_baf.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                pr_mob.lt_carts_added.Add("cart rock");
                //pr_mob.lt_carts_added.Add("cart fire");

                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart barier");
                st_carts.Enqueue("cart rock");
                cel = (GameObject)lt_now_bar_baf[Random.Range(0, lt_now_bar_baf.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 8)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now_bar_seal[Random.Range(0, lt_now_bar_seal.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                pr_mob.lt_carts_added.Add("cart barier");
                //pr_mob.lt_carts_added.Add("cart fire");

                pr_mob.remove_cart(type);

                atact = true;
                */

                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart barier");
                cel = (GameObject)lt_now_bar_seal[Random.Range(0, lt_now_bar_seal.Count)];
                atact = true;
                //next_hod();
            }
        }

        if (mob_list[num].name == "dungalorg")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now_att = new List<object>();
            List<object> lt_now_seal = new List<object>();
            List<object> lt_now_d = new List<object>();
            List<object> lt_now_d1 = new List<object>();
            List<object> lt_now_d_seal = new List<object>();
            List<object> lt_now_att_seal = new List<object>();
            bool bl_cent = true;

            bool bl_art_d_seal = false;
            foreach (par_cart_art art in pr_mob.lt_carts_art)
            {
                if (art.name == "cart dungalorg heard" && art.perez_now == 0)
                    bl_art_d_seal = true;

            }

            if (lt.Count > 0)
            {
                foreach (object obj in lt)
                {
                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();
                    bool bl = true;
                    if (mb.friend)
                    {
                        if (pr_mob.lt_carts.Contains("cart damage"))
                            lt_now_att.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart damage") && pr_mob.lt_carts.Contains("cart derlorg"))
                            lt_now_att_seal.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart dungalorg(sleep)"))
                            lt_now_d.Add(obj);
                        if (pr_mob.lt_carts.Contains("cart dungalorg(sleep) 1"))
                            lt_now_d1.Add(obj);

                    }


                }
                if (pr_mob.lt_carts.Contains("cart dungalorg(sleep)") && pr_mob.lt_carts.Contains("cart derlorg") && bl_art_d_seal)
                    lt_now_d_seal.Add(mob_list[num]);
                //Debug.Log("hiller atack");

            }

            List<int> lt_rd = new List<int>() { 0, 1, 2, 3, 4, 5 };
            if (Random.Range(0, 3) == 0 && (lt_now_d.Count != 0 || lt_now_d1.Count != 0))
            {
                lt_rd = new List<int>() { 3, 4 };
                if (lt_now_d.Count == 0)
                    lt_rd.Remove(3);
                if (lt_now_d1.Count == 0)
                    lt_rd.Remove(4);
            }
            else
            {
                if (lt_now_att.Count == 0)
                    lt_rd.Remove(0);
                if (lt_now_seal.Count == 0)
                    lt_rd.Remove(1);
                if (lt_now_att_seal.Count == 0)
                    lt_rd.Remove(2);
                if (lt_now_d.Count == 0)
                    lt_rd.Remove(3);
                if (lt_now_d1.Count == 0)
                    lt_rd.Remove(4);
                if (lt_now_d_seal.Count == 0)
                    lt_rd.Remove(5);
            }

            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                /*
                string type = "cart damage";
                pr_mob.cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att[Random.Range(0, lt_now_att.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 1)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_seal[Random.Range(0, lt_now_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 2)
            {
                /*
                string type = "cart derlorg";
                pr_mob.cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart damage");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart derlorg");
                st_carts.Enqueue("cart damage");
                cel = (GameObject)lt_now_att_seal[Random.Range(0, lt_now_att_seal.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 3)
            {
                /*
                string type = "cart dungalorg(sleep)";
                pr_mob.cel = (GameObject)lt_now_d[Random.Range(0, lt_now_d.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                //pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                //pr_mob.lt_carts_added.Add("cart damage");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart dungalorg(sleep)");
                cel = (GameObject)lt_now_d[Random.Range(0, lt_now_d.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 4)
            {
                /*
                string type = "cart dungalorg(sleep) 1";
                pr_mob.cel = (GameObject)lt_now_d1[Random.Range(0, lt_now_d1.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                //pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                //pr_mob.lt_carts_added.Add("cart damage");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart dungalorg(sleep) 1");
                cel = (GameObject)lt_now_d1[Random.Range(0, lt_now_d1.Count)];
                atact = true;
                //next_hod();
            }
            else if (a_rd == 5)
            {
                /*
                string type = "cart dungalorg(sleep)";
                pr_mob.cel = (GameObject)lt_now_d_seal[Random.Range(0, lt_now_d_seal.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);

                pr_mob.lt_carts_added_now.Add(type);
                //pr_mob.lt_carts_added.Add(type);
                pr_mob.lt_carts_added.Add("cart derlorg");

                pr_mob.remove_cart(type);

                atact = true;
                */
                st_carts.Enqueue("cart dungalorg(sleep)");
                st_carts.Enqueue("cart derlorg");
                cel = (GameObject)lt_now_d_seal[Random.Range(0, lt_now_d_seal.Count)];
                atact = true;
                //next_hod();
            }
        }



        else if (mob_list[num].name == "spider (spawned)")
        {
            //List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            bool bl_cent = true;
            if (pr_mob.lt_mob_vizited.Count > 0)
            {
                foreach (GameObject obj in pr_mob.lt_mob_vizited)
                {
                    par_mob mb = obj.GetComponent<par_mob>();
                    if (Vector2.Distance(new Vector2(mb.x, mb.y), new Vector2(pr_mob.x, pr_mob.y)) <= pr_mob.distanse_min)
                    {

                        if (mb.friend != pr_mob.friend && pr_mob.lt_carts.Contains("cart spider poition"))
                        {
                            lt_now.Add(obj);
                        }

                    }



                }
                //Debug.Log("hiller atack");

            }

            List<int> lt_rd = new List<int>() { 0 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                /*
                string type = "cart spider poition";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);
                */
                st_carts.Enqueue("cart spider poition");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }



        }
        else if (mob_list[num].name == "spider")
        {
            //List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_min);
            List<object> lt_now = new List<object>();
            List<object> lt_now_1 = new List<object>();
            bool bl_cent = true;
            if (pr_mob.lt_mob_vizited.Count > 0)
            {
                foreach (Vector2 v2 in pr_mob.lt_v2_vizited)
                {
                    par_mob mb = null;
                    int v_x = (int)v2.x;
                    int v_y = (int)v2.y;
                    if (func.mob[v_x, v_y] != null)
                        mb = func.mob[v_x, v_y].GetComponent<par_mob>();
                    if (Vector2.Distance(v2, new Vector2(pr_mob.x, pr_mob.y)) <= pr_mob.distanse_min)
                    {
                        if (object_bool(v_x, v_y) && pr_mob.lt_carts.Contains("cart spider"))
                        {
                            lt_now.Add(func.eath[v_x, v_y]);
                        }


                    }



                }
                foreach (GameObject gm in pr_mob.lt_mob_vizited)
                {
                    par_mob mb = gm.GetComponent<par_mob>();
                    if ((mb.friend == pr_mob.friend) && gm.name == "spider (spawned)" && pr_mob.lt_carts.Contains("cart spider"))
                    {
                        lt_now_1.Add(gm);
                    }
                }

                //Debug.Log("hiller atack");

            }

            List<int> lt_rd = new List<int>() { 0, 1 };
            if (lt_now.Count == 0)
                lt_rd.Remove(0);
            if (lt_now_1.Count == 0)
                lt_rd.Remove(1);
            int a_rd = -1;
            if (lt_rd.Count > 0)
                a_rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (a_rd == 0)
            {
                /*
                string type = "cart spider";
                pr_mob.cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
                if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                    pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
                else
                {
                    pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
                }
                anim.col_anim++;
                pr_mob.cart_active_now = type;
                pr_mob.lt_carts_active_now.Add(type);
                pr_mob.remove_cart(type);
                */
                st_carts.Enqueue("cart spider");
                cel = (GameObject)lt_now[Random.Range(0, lt_now.Count)];
                atact = true;
                //next_hod();
            }
            else
            {
                st_carts.Enqueue("cart spider");
                cel = (GameObject)lt_now_1[Random.Range(0, lt_now_1.Count)];
                atact = true;
            }


        }



        if (atact)
        {
            string type = st_carts.Dequeue();
            pr_mob.cel = cel;

            List<string> lt_rez = new List<string>(pr_mob.lt_carts);


            pr_mob.gameObject.GetComponent<Animator>().SetBool("atack", true);
            //pr_mob.gameObject.GetComponent<Animator>().SetBool("perehod", true);
            if (!pr_mob.gameObject.GetComponent<Animator>().GetBool("selected"))
                pr_mob.gameObject.GetComponent<Animator>().SetBool("selected", true);
            else
            {
                pr_mob.gameObject.GetComponent<Animator>().Play("atack", -1, 0);
            }
            anim.col_anim++;

            pr_mob.cart_active_now = type;

            int sila = pr_mob.max_sila(type);
            bool bl_add = false;
            Debug.Log("sila " + type + "   =" + sila);
            lt_rez.Remove(type);
            if (sila > 0)
            {
                int rr = Random.Range(0, sila + 1);
                for (int i = 0; i < rr; i++)
                {
                    if (lt_rez.Contains(type))
                    {
                        if (!bl_add)
                        {
                            bl_add = true;
                            pr_mob.lt_carts_added_now.Add(type);
                        }

                        pr_mob.lt_carts_added.Add(type);
                        lt_rez.Remove(type);
                    }
                    else
                        break;
                }

            }

            pr_mob.lt_carts_active_now.Add(type);

            if (st_carts.Count > 0)
            {
                if (!bl_add)
                {
                    bl_add = true;
                    pr_mob.lt_carts_added_now.Add(type);
                }
                foreach (string st in st_carts)
                {
                    pr_mob.lt_carts_added.Add(st);
                    sila = pr_mob.max_sila(st);
                    Debug.Log("sila " + st + "   =" + sila);
                    if (sila > 0)
                    {
                        int rr = Random.Range(0, sila + 1);
                        Debug.Log("rr= " + rr);
                        for (int i = 0; i < rr; i++)
                        {
                            if (lt_rez.Contains(st))
                            {
                                pr_mob.lt_carts_added.Add(st);
                                lt_rez.Remove(st);
                            }
                            else
                                break;
                        }

                    }

                }
            }
            pr_mob.remove_cart(type);

        }
        /*      
              if (false)
              {
                  List<GameObject> lt_friends = new List<GameObject>();
                  List<GameObject> lt_no_friends = new List<GameObject>();
                  List<GameObject> lt_empty = new List<GameObject>();

                  foreach (Vector2 v2 in pr_mob.lt_v2_vizited)
                  {
                      par_mob mb = null;
                      int v_x = (int)v2.x;
                      int v_y = (int)v2.y;
                      if (func.mob[v_x, v_y] != null)
                          mb = func.mob[v_x, v_y].GetComponent<par_mob>();

                      if (Vector2.Distance(v2, new Vector2(pr_mob.x, pr_mob.y)) <= pr_mob.distanse_min)
                      {
                          if (mb != null)
                          {
                              if (mb.friend == pr_mob.friend)
                                  lt_friends.Add(func.mob[v_x, v_y]);
                              else
                                  lt_no_friends.Add(func.mob[v_x, v_y]);
                          }
                          else
                              lt_empty.Add(func.eath[v_x, v_y]);
                      }



                  }


                  Dictionary<string, List<GameObject>> lt_rd_attack = new Dictionary<string, List<GameObject>>();

                  if (lt_no_friends.Count > 0)
                  {
                      if (pr_mob.type_lt_attack.Contains(type_atack_mob.damage))
                      {
                          if (pr_mob.lt_carts.Contains("cart damage"))
                              lt_rd_attack.Add("cart damage");
                          if (pr_mob.lt_carts.Contains("cart damage 1"))
                              lt_rd_attack.Add("cart damage 1");
                          if (pr_mob.lt_carts.Contains("cart damage 2"))
                              lt_rd_attack.Add("cart damage 2");

                          if (pr_mob.type_lt_attack.Contains(type_atack_mob.blood) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                          {
                              if (pr_mob.lt_carts.Contains("cart blood"))
                                  lt_rd_attack.Add("cart blood");
                              if (pr_mob.lt_carts.Contains("cart blood 1"))
                                  lt_rd_attack.Add("cart blood 1");
                              if (pr_mob.lt_carts.Contains("cart blood 2"))
                                  lt_rd_attack.Add("cart blood 2");
                          }
                          if (pr_mob.type_lt_attack.Contains(type_atack_mob.rock) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                          {
                              if (pr_mob.lt_carts.Contains("cart rock"))
                                  lt_rd_attack.Add("cart rock");
                              if (pr_mob.lt_carts.Contains("cart rock 1"))
                                  lt_rd_attack.Add("cart rock 1");
                              if (pr_mob.lt_carts.Contains("cart rock 2"))
                                  lt_rd_attack.Add("cart rock 2");
                          }
                          if (pr_mob.type_lt_attack.Contains(type_atack_mob.ice) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                          {
                              if (pr_mob.lt_carts.Contains("cart ice"))
                                  lt_rd_attack.Add("cart ice");
                              if (pr_mob.lt_carts.Contains("cart ice 1"))
                                  lt_rd_attack.Add("cart ice 1");
                              if (pr_mob.lt_carts.Contains("cart ice 2"))
                                  lt_rd_attack.Add("cart ice 2");
                          }
                          if (pr_mob.type_lt_attack.Contains(type_atack_mob.energy) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                          {
                              if (pr_mob.lt_carts.Contains("cart ice"))
                                  lt_rd_attack.Add("cart ice");
                              if (pr_mob.lt_carts.Contains("cart ice 1"))
                                  lt_rd_attack.Add("cart ice 1");
                              if (pr_mob.lt_carts.Contains("cart ice 2"))
                                  lt_rd_attack.Add("cart ice 2");
                          }
                      }
                      if (pr_mob.type_lt_attack.Contains(type_atack_mob.blood) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                      {
                          if (pr_mob.lt_carts.Contains("cart blood"))
                              lt_rd_attack.Add("cart blood");
                          if (pr_mob.lt_carts.Contains("cart blood 1"))
                              lt_rd_attack.Add("cart blood 1");
                          if (pr_mob.lt_carts.Contains("cart blood 2"))
                              lt_rd_attack.Add("cart blood 2");
                      }
                      if (pr_mob.type_lt_attack.Contains(type_atack_mob.rock) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                      {
                          if (pr_mob.lt_carts.Contains("cart rock"))
                              lt_rd_attack.Add("cart rock");
                          if (pr_mob.lt_carts.Contains("cart rock 1"))
                              lt_rd_attack.Add("cart rock 1");
                          if (pr_mob.lt_carts.Contains("cart rock 2"))
                              lt_rd_attack.Add("cart rock 2");
                      }
                      if (pr_mob.type_lt_attack.Contains(type_atack_mob.ice) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                      {
                          if (pr_mob.lt_carts.Contains("cart ice"))
                              lt_rd_attack.Add("cart ice");
                          if (pr_mob.lt_carts.Contains("cart ice 1"))
                              lt_rd_attack.Add("cart ice 1");
                          if (pr_mob.lt_carts.Contains("cart ice 2"))
                              lt_rd_attack.Add("cart ice 2");
                      }
                      if (pr_mob.type_lt_attack.Contains(type_atack_mob.energy) || pr_mob.type_lt_attack.Contains(type_atack_mob.elemental))
                      {
                          if (pr_mob.lt_carts.Contains("cart ice"))
                              lt_rd_attack.Add("cart ice");
                          if (pr_mob.lt_carts.Contains("cart ice 1"))
                              lt_rd_attack.Add("cart ice 1");
                          if (pr_mob.lt_carts.Contains("cart ice 2"))
                              lt_rd_attack.Add("cart ice 2");
                      }
                  }
              }
      */



        return atact;
    }
    #endregion



    public void baf_after_hod(bool bl_skip)
    {

        if (mob_list[hod_mob_now_int] != null)
        {
            par_mob pr_mob = mob_list[hod_mob_now_int].GetComponent<par_mob>();
            if (pr_mob.lt_bafs.Count > 0 && !pr_mob.speed_hod)
            {
                Stack<par_baf> st_bafs = new Stack<par_baf>();
                List<par_baf.type_baf> lt_tp_bf = new List<par_baf.type_baf>();
                foreach (par_baf bafs in pr_mob.lt_bafs)
                {
                    if (bafs.passive)
                    {
                        st_bafs.Push(bafs);
                        lt_tp_bf.Add(bafs.type);
                    }

                }
                int q = st_bafs.Count;
                for (int i = 0; i < q; i++)
                {
                    par_baf bf = st_bafs.Pop();
                    if (!bf.before)
                    {
                        if (bf.skip_first)
                        {
                            bf.skip_first = false;
                            continue;
                        }
                        if (bf.type == par_baf.type_baf.fire || bf.type == par_baf.type_baf.poition)
                        {
                            //Debug.Log("true poition");
                            pr_mob.damage(bf.power, "none");
                        }
                        if (pr_mob.hp <= 0)
                        {
                            //next_hod();
                            //return;
                            break;
                        }
                        /*
                        else if (bf.type == par_baf.type_baf.berserk)
                        {
                            lt_tp_bf.Remove(bf.type);
                            pr_mob.lt_bafs.Remove(bf);
                        }
                        */
                        else if (bf.long_baf == 1)
                        {

                            lt_tp_bf.Remove(bf.type);
                            bool del = false;


                            if (bf.type == par_baf.type_baf.energy && bl_skip)
                            {
                                pr_mob.speed--;
                                //if (pr_mob.speed_now>1 || pr_mob.speed ==0)
                                //pr_mob.speed_now--;
                                //pr_mob.speed_now--;
                                mob_list[hod_mob_now_int].GetComponent<Animator>().speed = 1;
                                del = true;
                                GameObject gg = GameObject.Find("im baf");


                                //gg.GetComponent<Animator>().SetBool("energy_anim", false);
                                //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                            }
                            else if (bf.type == par_baf.type_baf.ice && bl_skip)
                            {
                                pr_mob.speed++;
                                //pr_mob.speed_now++;
                                mob_list[hod_mob_now_int].GetComponent<Animator>().speed = 1;

                                mob_list[hod_mob_now_int].GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                                del = true;
                                GameObject gg = GameObject.Find("im baf");
                                //gg.GetComponent<Animator>().SetBool("energy_anim", false);
                                //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                            }
                            if (bf.type == par_baf.type_baf.fire || bf.type == par_baf.type_baf.poition || bf.type == par_baf.type_baf.berserk)
                            {
                                del = true;
                            }
                            if (!lt_tp_bf.Contains(bf.type))
                            {
                                if (bf.type == par_baf.type_baf.energy && bl_skip)
                                {
                                    Destroy(mob_list[hod_mob_now_int].transform.Find("energy").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.ice && bl_skip)
                                {
                                    Destroy(mob_list[hod_mob_now_int].transform.Find("ice").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.fire)
                                {
                                    Destroy(mob_list[hod_mob_now_int].transform.Find("fire").gameObject);
                                }
                                else if (bf.type == par_baf.type_baf.poition)
                                {
                                    Destroy(mob_list[hod_mob_now_int].transform.Find("poition").gameObject);
                                }

                            }
                            if (del)
                                pr_mob.lt_bafs.Remove(bf);
                        }
                        if (bl_skip)
                            bf.baf();
                    }

                }
                if (hod_mob_now_int == 0)
                    pr_mob.update_bufs();
            }
        }
    }
    public static void end_hod_all()
    {
        lovyshka_after_hod();
    }
    public static void lovyshka_after_hod()
    {
        Debug.Log("lov after");
        List<Vector2> lt_ef = new List<Vector2>();
        Stack<GameObject> gm_del = new Stack<GameObject>();
        foreach (GameObject gm in lovyshka_after)
        {
            Debug.Log(gm.name);
            par_lovyshka lov = gm.GetComponent<par_lovyshka>();
            if (lov.skip_first)
            {
                lov.skip_first = false;
                continue;
            }
            GameObject gg = lov.activate();
            if (gg != null)
            {
                if (!lt_ef.Contains(new Vector2(lov.x, lov.y)))
                    lt_ef.Add(new Vector2(lov.x, lov.y));
                else
                {
                    gg.active = false;
                }
            }



            lov.update();
            if (lov.time == 0)
            {
                gm_del.Push(gm);
            }
        }
        while (gm_del.Count > 0)
        {
            gm_del.Pop().GetComponent<par_lovyshka>().del();
        }


    }

    public static void func_next_hod()
    {


        if (mob_list[hod_mob_now_int] != null)
        {
            par_mob mb_now = mob_list[hod_mob_now_int].GetComponent<par_mob>();
            //Debug.Log("is hod " + hod_mob_now_int + " " + mb_now.speed_now);
            Debug.Log("is hod " + nums_mob_wolk.Count + " " + nums_mob.Count);

            //if 
            //return;
            bool sp = false;
            if (mb_now.speed_now <= 0)
            {
                nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                if (mb_now.speed > 0)
                {
                    mb_now.speed_now = mb_now.speed;
                    sp = true;
                }

                else if (mb_now.speed_now < mb_now.speed)
                {
                    mb_now.speed_now = 1;
                    sp = true;
                }

                if (nums_mob_wolk.Count == 0)
                {
                    nums_mob_wolk = new List<int>(nums_mob);
                    end_hod_all();
                }

                mb_now.par_lt_sila = new List<string>(mb_now.par_lt_sila_base);
                mb_now.par_lt_speed = new List<string>(mb_now.par_lt_speed_base);
                data.data_now.cl_carts_speed = 0;
            }


            if (mob_list[hod_mob_now_int] != null)
            {
                mb_now.hod_now = false;
            }
            //if (hod_mob_now_int == 0)
            //Debug.Log("is sp=" + sp);
            //if (mb_now.speed_now <= 0)
            GameObject.Find("func").GetComponent<func>().baf_after_hod(sp);
        }



        timer_now = 0;
        //anim.hod_next_anim = false;
        hod_next_mob_art = false;

        next_hod_art_bool();
    }
    public static void next_hod()
    {





        //if(mb_now.speed_now > 0)    






        hod_next_mob_art = true;



        //Debug.Log(mob_list[hod_mob_now_int].name + "  baaaaf 0 "+ anim.col_anim+"   "+ timer_now);
        if (anim.col_anim == 0 && timer_now == 0.1f)
        {

            func_next_hod();
        }

    }
    public static void next_hod_art()
    {
        hod_next_mob = true;
        if (anim.col_anim == 0)
        {
            timer_now = 0;
            //anim.hod_next_anim = false;
            hod_next_mob = false;

            next_hod_bool();

        }
    }
    public static void next_hod_baf()
    {
        hod_next_mob_baf = true;
        if (anim.col_anim == 0)
        {
            timer_now = 0;
            //anim.hod_next_anim = false;
            hod_next_mob_baf = false;

            next_hod_baf_bool();

        }
    }



    public static void clean_func()
    {
        for (int i = 0; i < size_global; i++)
        {
            for (int j = 0; j < size_global; j++)
            {
                if (mob[j, i] != null && mob[j, i].name != data.data_now.pl_name_now)
                    Destroy(mob[j, i]);
                if (nature[j, i] != null)
                    Destroy(nature[j, i]);
                if (carts[j, i] != null)
                    Destroy(carts[j, i]);
                if (eath[j, i] != null)
                    Destroy(eath[j, i]);
                mob[j, i] = null;
                nature[j, i] = null;
                carts[j, i] = null;
                eath[j, i] = null;
                mob_list[i * size_global + j] = null;
            }
        }
        hod_mob_now_int = 0;
        hod_next_mob = false;
        nums_mob.Clear();
        nums_mob_wolk.Clear();
        cart_effects.Clear();
        baza.Clear();
        //ui.cart_sprite.Clear();
        num_lev = 0;
        num_lev_type = 0;
    }

    public static GameObject[,] mob;
    public static GameObject[,] nature;
    public static GameObject[,] carts;
    public static GameObject[,] eath;
    public static Queue<GameObject>[,] que_effects;
    public static List<GameObject>[,] lt_lovyshka;
    public static List<GameObject>[,] lt_v2_viz_friend;

    public static List<GameObject> lovyshka_now;
    public static List<GameObject> lovyshka_after;

    public static string wall_now = "rock";
    public static string eath_now = "eath";
    //public func.room rt = new func.room();

    //public static List<GameObject>[,,,] decor = new List<GameObject>[5, 5, 7, 7];
    public static GameObject[] mob_list;
    public static List<int> nums_mob = new List<int>();
    public static List<int> nums_mob_wolk = new List<int>();
    public static Dictionary<string, GameObject> cart_effects = new Dictionary<string, GameObject>();
    public static GameObject gm_wolk_friend;


    public static bool hod_next_mob = false;
    public static bool hod_next_mob_art = false;
    public static bool hod_next_mob_baf = false;


    public static bool played = true;
    public static bool hod_mob_now_baf_attack = true;
    public static bool hod_mob_now_baf_sp = true;
    public static int hod_mob_now_baf = 0;
    public static int hod_mob_now_int = 0;
    public static int hod_mob_now_friend = 0;
    public static int col_friend = 1;

    public static float timer_now = 0;

    public static Dictionary<par_baf.type_baf, Sprite> bf_sp = new Dictionary<par_baf.type_baf, Sprite>();
    public List<Sprite> sp_lt_bf = new List<Sprite>();

    public static Dictionary<type_lvl, AudioClip> music_lvl = new Dictionary<type_lvl, AudioClip>();
    public List<AudioClip> music_lvl_lt = new List<AudioClip>();
    void Start()
    {


        played = true;

        if (bf_sp.Count == 0)
        {
            bf_sp.Add(par_baf.type_baf.fire, sp_lt_bf[0]);
            bf_sp.Add(par_baf.type_baf.energy, sp_lt_bf[1]);
            bf_sp.Add(par_baf.type_baf.ice, sp_lt_bf[2]);
            bf_sp.Add(par_baf.type_baf.poition, sp_lt_bf[3]);
            bf_sp.Add(par_baf.type_baf.seal, sp_lt_bf[4]);

            bf_sp.Add(par_baf.type_baf.bar, sp_lt_bf[5]);

            bf_sp.Add(par_baf.type_baf.blood_bar, sp_lt_bf[6]);
            bf_sp.Add(par_baf.type_baf.energy_bar, sp_lt_bf[7]);
            bf_sp.Add(par_baf.type_baf.fire_bar, sp_lt_bf[8]);
            bf_sp.Add(par_baf.type_baf.ice_bar, sp_lt_bf[9]);
            bf_sp.Add(par_baf.type_baf.poition_bar, sp_lt_bf[10]);
            bf_sp.Add(par_baf.type_baf.rock_bar, sp_lt_bf[11]);


            bf_sp.Add(par_baf.type_baf.rage, sp_lt_bf[12]);

            bf_sp.Add(par_baf.type_baf.seal_damage, sp_lt_bf[13]);
            bf_sp.Add(par_baf.type_baf.seal_barier, sp_lt_bf[14]);
            bf_sp.Add(par_baf.type_baf.seal_blood, sp_lt_bf[15]);
            bf_sp.Add(par_baf.type_baf.seal_rock, sp_lt_bf[16]);
            bf_sp.Add(par_baf.type_baf.seal_fire, sp_lt_bf[17]);
            bf_sp.Add(par_baf.type_baf.seal_poition, sp_lt_bf[18]);
            bf_sp.Add(par_baf.type_baf.seal_ice, sp_lt_bf[19]);
            bf_sp.Add(par_baf.type_baf.seal_energy, sp_lt_bf[20]);

            bf_sp.Add(par_baf.type_baf.berserk, sp_lt_bf[21]);

            bf_sp.Add(par_baf.type_baf.bar_rock, sp_lt_bf[22]);
            bf_sp.Add(par_baf.type_baf.bar_fire, sp_lt_bf[23]);
            bf_sp.Add(par_baf.type_baf.bar_energy, sp_lt_bf[24]);
            bf_sp.Add(par_baf.type_baf.bar_blood, sp_lt_bf[25]);
            bf_sp.Add(par_baf.type_baf.bar_ice, sp_lt_bf[26]);
            bf_sp.Add(par_baf.type_baf.bar_poition, sp_lt_bf[27]);


        }
        if (music_lvl.Count == 0)
        {
            music_lvl.Add(type_lvl.bases, music_lvl_lt[0]);
            music_lvl.Add(type_lvl.bases_1, music_lvl_lt[0]);
            music_lvl.Add(type_lvl.derlorg, music_lvl_lt[1]);
            music_lvl.Add(type_lvl.spider, music_lvl_lt[2]);
        }


        //mob_list = new GameObject[size_global*size_global];
        mob = new GameObject[size_global, size_global];
        nature = new GameObject[size_global, size_global];
        carts = new GameObject[size_global, size_global];
        eath = new GameObject[size_global, size_global];
        mob_list = new GameObject[size_global * size_global];
        que_effects = new Queue<GameObject>[size_global, size_global];
        lt_lovyshka = new List<GameObject>[size_global, size_global];
        lt_v2_viz_friend = new List<GameObject>[size_global, size_global];
        for (int i = 0; i < size_global; i++)
        {
            for (int j = 0; j < size_global; j++)
            {
                lt_v2_viz_friend[j, i] = new List<GameObject>();
                que_effects[j, i] = new Queue<GameObject>();
                lt_lovyshka[j, i] = new List<GameObject>();
                lovyshka_now = new List<GameObject>();
                lovyshka_after = new List<GameObject>();
            }
        }

        Transform tr = GameObject.Find("effects").transform;

        cart_effects.Add("cart rock", tr.Find("rock ef").gameObject);
        cart_effects.Add("cart rock 1", tr.Find("rock 1 ef").gameObject);

        cart_effects.Add("cart blood", tr.Find("blood ef").gameObject);
        cart_effects.Add("cart blood 1", tr.Find("blood 1 ef").gameObject);
        cart_effects.Add("cart blood 2", tr.Find("blood 2 ef").gameObject);

        cart_effects.Add("cart fire", tr.Find("fire ef").gameObject);
        cart_effects.Add("cart fire 1", tr.Find("fire 1 ef").gameObject);

        cart_effects.Add("cart energy", tr.Find("energy ef").gameObject);
        cart_effects.Add("cart energy 1", tr.Find("energy ef").gameObject);

        cart_effects.Add("cart ice", tr.Find("ice ef").gameObject);
        cart_effects.Add("cart ice 1", tr.Find("ice ef").gameObject);

        cart_effects.Add("cart poition", tr.Find("poition ef").gameObject);
        cart_effects.Add("cart poition 1", tr.Find("poition ef").gameObject);

        cart_effects.Add("cart spider", tr.Find("spawn spider").gameObject);
        cart_effects.Add("cart spider poition", tr.Find("spider poition ef").gameObject);

        cart_effects.Add("atack energy", tr.Find("atack energy").gameObject);
        cart_effects.Add("atack fire", tr.Find("atack fire").gameObject);
        cart_effects.Add("atack rock", tr.Find("atack rock").gameObject);
        cart_effects.Add("atack blood", tr.Find("atack blood").gameObject);
        cart_effects.Add("atack ice", tr.Find("atack ice").gameObject);
        cart_effects.Add("atack poition", tr.Find("atack poition").gameObject);

        cart_effects.Add("atack energy 1", tr.Find("atack energy 1").gameObject);
        cart_effects.Add("atack fire 1", tr.Find("atack fire 1").gameObject);
        cart_effects.Add("atack rock 1", tr.Find("atack rock 1").gameObject);
        cart_effects.Add("atack blood 1", tr.Find("atack blood 1").gameObject);
        cart_effects.Add("atack ice 1", tr.Find("atack ice 1").gameObject);
        cart_effects.Add("atack poition 1", tr.Find("atack poition 1").gameObject);

        cart_effects.Add("atack energy 2", tr.Find("atack energy 2").gameObject);
        cart_effects.Add("atack fire 2", tr.Find("atack fire 2").gameObject);
        cart_effects.Add("atack rock 2", tr.Find("atack rock 2").gameObject);
        cart_effects.Add("atack blood 2", tr.Find("atack blood 2").gameObject);
        cart_effects.Add("atack ice 2", tr.Find("atack ice 2").gameObject);
        cart_effects.Add("atack poition 2", tr.Find("atack poition 2").gameObject);

        cart_effects.Add("cart damage", tr.Find("atack base").gameObject);
        cart_effects.Add("cart damage 1", tr.Find("atack base 1").gameObject);
        cart_effects.Add("cart damage 2", tr.Find("atack base 2").gameObject);
        cart_effects.Add("cart barier", tr.Find("bases bar ef").gameObject);
        cart_effects.Add("cart barier 1", tr.Find("bases bar ef").gameObject);

        cart_effects.Add("cart derlorg", tr.Find("derlorg ef").gameObject);

        cart_effects.Add("cart metca(boss)", tr.Find("metca ef").gameObject);
        /*
        cart_effects.Add("tuman energy", tr.Find("tuman energy").gameObject);
        cart_effects.Add("tuman fire", tr.Find("tuman fire").gameObject);
        cart_effects.Add("tuman rock", tr.Find("tuman rock").gameObject);
        cart_effects.Add("tuman blood", tr.Find("tuman blood").gameObject);
        cart_effects.Add("tuman ice", tr.Find("tuman ice").gameObject);
        cart_effects.Add("tuman poition", tr.Find("tuman poition").gameObject);


        cart_effects.Add("spawn dych energy", tr.Find("spawn dych energy").gameObject);
        cart_effects.Add("spawn dych fire", tr.Find("spawn dych fire").gameObject);
        cart_effects.Add("spawn dych rock", tr.Find("spawn dych rock").gameObject);
        cart_effects.Add("spawn dych blood", tr.Find("spawn dych blood").gameObject);
        cart_effects.Add("spawn dych ice", tr.Find("spawn dych ice").gameObject);
        cart_effects.Add("spawn dych poition", tr.Find("spawn dych poition").gameObject);

                */

        //cart_effects.Add("cart spirit energy", tr.Find("spirit energy ef").gameObject);
        cart_effects.Add("cart spirit fire", tr.Find("spirit fire ef").gameObject);
        //cart_effects.Add("cart spirit rock", tr.Find("spirit rock ef").gameObject);
        cart_effects.Add("cart spirit blood", tr.Find("spirit blood ef").gameObject);
        cart_effects.Add("cart spirit ice", tr.Find("spirit ice ef").gameObject);
        cart_effects.Add("cart spirit poition", tr.Find("spirit poition ef").gameObject);

        cart_effects.Add("barier bases energy", tr.Find("bases energy bar ef").gameObject);
        cart_effects.Add("barier bases fire", tr.Find("bases fire bar ef").gameObject);
        cart_effects.Add("barier bases rock", tr.Find("bases rock bar ef").gameObject);
        cart_effects.Add("barier bases blood", tr.Find("bases blood bar ef").gameObject);
        cart_effects.Add("barier bases ice", tr.Find("bases ice bar ef").gameObject);
        cart_effects.Add("barier bases poition", tr.Find("bases poition bar ef").gameObject);

        cart_effects.Add("barier energy", tr.Find("energy bar ef").gameObject);
        cart_effects.Add("barier fire", tr.Find("fire bar ef").gameObject);
        cart_effects.Add("barier rock", tr.Find("rock bar ef").gameObject);
        cart_effects.Add("barier blood", tr.Find("blood bar ef").gameObject);
        cart_effects.Add("barier ice", tr.Find("ice bar ef").gameObject);
        cart_effects.Add("barier poition", tr.Find("poition bar ef").gameObject);

        cart_effects.Add("seal energy", tr.Find("seal energy").gameObject);
        cart_effects.Add("seal fire", tr.Find("seal fire").gameObject);
        cart_effects.Add("seal rock", tr.Find("seal rock").gameObject);
        cart_effects.Add("seal blood", tr.Find("seal blood").gameObject);
        cart_effects.Add("seal ice", tr.Find("seal ice").gameObject);
        cart_effects.Add("seal poition", tr.Find("seal poition").gameObject);
        cart_effects.Add("seal damage", tr.Find("seal damage").gameObject);
        cart_effects.Add("seal barier", tr.Find("seal barier").gameObject);


        //cart_effects.Add("create crystall", tr.Find("create crystall").gameObject);
        cart_effects.Add("cart crystall", tr.Find("crystall create").gameObject);
        //cart_effects.Add("cart crystall 1", tr.Find("create crystall").gameObject);
        cart_effects.Add("crystall destroy", tr.Find("crystall destroy").gameObject);

        cart_effects.Add("cart rage(boss)", tr.Find("rage ef").gameObject);

        cart_effects.Add("berserk ef", tr.Find("berserk ef").gameObject);
        cart_effects.Add("create cave derlorg", tr.Find("create cave derlorg").gameObject);
        //cart_effects.Add("cart blood", tr.Find("rock ef").gameObject;
        //mob_list[0] = GameObject.Find("player");
        //Debug.Log("func");
        par_cart.cart_combinations.collections();
        /*
        foreach (par_cart.cart_combinations.comb_cart ss in par_cart.cart_combinations.lt_comb)
        {
            foreach (string name in ss.next.Keys)
            {
                Debug.Log(ss.name+" "+name);
            }
            
        }
        */

        num_lev = 0;
        num_lev_type = 0;
        mob_list[0] = GameObject.Find(data.data_now.pl_name_now);
        nums_mob.Add(0);
        nums_mob_wolk.Add(0);

        type_lvl_now = type_lvl.bases_1;

        generate_room();
        GameObject.Find("func").GetComponent<AudioSource>().clip = music_lvl[type_lvl_now];
        GameObject.Find("func").GetComponent<AudioSource>().Play();

    }
    public float aaa = 0;
    public float aaa1 = 0;


    public List<int> lt1 = new List<int>();
    public List<int> lt2 = new List<int>();
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(col_friend);
        lt1 = new List<int>(nums_mob);
        lt2 = new List<int>(nums_mob_wolk);
        //Debug.Log(hod_mob_now_friend);
        //aaa = Time.time-aaa1;
        //if(hod_mob_now_int!=0)
        //aaa += Time.deltaTime;
        /*
        foreach (par_cart.cart_combinations.comb_cart ss in par_cart.cart_combinations.lt_comb)
        {
            //Debug.Log(ss.name+" "+ss.next.Count);

        }
        */
        //Debug.Log(" aaart " + hod_next_mob_art + "   ");
        if (played)
        {
            if (timer_now < 0.1f)
            {
                timer_now += Time.deltaTime;
                //Debug.Log(timer_now);
                if (timer_now > 0.1f)
                    timer_now = 0.1f;
            }

            if (anim.col_anim == 0 && hod_next_mob_art && timer_now == 0.1f)
            {

                func_next_hod();
                /*
                baf_after_hod();
                
                if (mob_list[hod_mob_now_int] != null)
                {
                    mob_list[hod_mob_now_int].GetComponent<par_mob>().hod_now = false;
                }
                
                timer_now = 0;
                //anim.hod_next_anim = false;
                hod_next_mob_art = false;

                next_hod_art_bool();
                */
            }
            if (anim.col_anim == 0 && hod_next_mob)
            {
                timer_now = 0;
                //anim.hod_next_anim = false;
                hod_next_mob = false;

                next_hod_bool();

            }
            if (anim.col_anim == 0 && hod_next_mob_baf)
            {
                timer_now = 0;
                //anim.hod_next_anim = false;
                hod_next_mob_baf = false;

                next_hod_baf_bool();

            }
        }

        //Debug.Log(Input.touchCount);




    }
}


