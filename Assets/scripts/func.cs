using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static par_mob;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using static par_cart;


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

    public const int size_global = 30;

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
    /*
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
    */
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

                int col_way = distance(mb.x, mb.y, x_cl, y_cl);// way(mb.x, mb.y, mb.distanse_attack)[x_cl, y_cl];
                ui_atack.atack_comb at = null;
                if (hod_mob_now_int == 0 && ui_atack.lt_cart_comb_selected.Count > 0&&mb.lt_carts_active_now.Count==0 && (col_way <=mb.distanse_attack || bl_r_att))
                {
                    foreach (ui_atack.atack_comb comb in ui_atack.lt_cart_comb_selected)
                    {
                        Debug.Log("auto comb " + comb.atack_type + "  " + (gm_click.GetComponent<par_mob>() != null));

                        if (comb.atack_type == ui_atack.type_atack.bases)
                            at = comb;
                        else if (gm_click.GetComponent<par_mob>() != null)
                        {
                            par_mob pr_mb = gm_click.GetComponent<par_mob>();
                            //Debug.Log("auto comb 0 " + pr_mb.friend);
                            if (!pr_mb.nps)
                            {
                                if (pr_mb.friend && comb.atack_type == ui_atack.type_atack.friend)
                                    at = comb;
                                if (!pr_mb.friend && comb.atack_type == ui_atack.type_atack.mob)
                                {
                                    at = comb;
                                    //Debug.Log("auto comb true");
                                }
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

                    if (bl && mb.proverka_atack(new List<string>(at.carts), true, false))
                    {
                        if (x_cl > mb.x)
                            mb.simm(1);
                        else if (x_cl < mb.x)
                            mb.simm(-1);

                        string type;
                        mb.proverka_atack(new List<string>(at.carts), true, true);
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
                    if ((gm_click.name == "cave" || gm_click.name == "cave 1" || gm_click.name == "cave derlorg" || gm_click.name == "fire portal" || gm_click.name == "ice portal" || gm_click.name == "poition portal" || gm_click.name == "blood portal" || gm_click.name == "rock portal" || gm_click.name == "energy portal") && distance(x_cl, y_cl, pl.x, pl.y) ==1)
                    {
                        type_lvl type_lvl_base = type_lvl_now;
                        if (gm_click.name == "cave derlorg")
                        {
                            if (type_lvl_now != type_lvl.derlorg)
                                num_lev--;
                            type_lvl_now = type_lvl.derlorg;
                            
                        }
                        else
                        {
                            foreach (type_lvl lvl in cave_location.Keys)
                            {
                                if (cave_location[lvl]==gm_click.name)
                                {

                                    type_lvl_now = lvl;
                                    break;
                                }
                            }
                            /*
                            if (num_lev < 5)
                                type_lvl_now = type_lvl.bases;
                            else
                                type_lvl_now = type_lvl.bases_1;
                            */
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
                    if (distance(mb.x, mb.y,x_cl, y_cl) ==1)
                    {
                        par_prise pr = gm_click.GetComponent<par_prise>();
                        List<string> lt_buy = new List<string>(pr.lt_price);

                        if (pr.type == par_prise.type_price.gold || pr.type == par_prise.type_price.obmen)
                        {
                            foreach (par_cart_art art in mb.lt_carts_art)
                            {
                                if (art.name == "cart brilliant" && art.q > 0 && lt_buy.Contains("cart gold"))
                                {
                                    lt_buy.Remove("cart gold");
                                    //art.q--;
                                }
                            }

                            if (gm_click.GetComponent<par_prise>().can_buy(lt_buy, mb.lt_carts))
                            {
                                if (data.data_now.cl_shop < 20)
                                    data.data_now.cl_shop++;
                                if (data.data_now.cl_shop >= 20)
                                    data.give_achive("постоянный покупатель");
                                data.save_data();
                                //lt_buy = new List<string>(mb.lt_carts);

                                
                                    foreach (par_cart_art art in mb.lt_carts_art)
                                    {
                                        if (art.name == "cart brilliant" && art.q > 0 && pr.lt_price.Contains("cart gold"))
                                        {
                                            if(pr.type == par_prise.type_price.gold)
                                                pr.lt_price.Remove("cart gold");
                                            art.q--;
                                        }
                                    }
                                


                                gm_click.GetComponent<par_prise>().buy(mb);
                                //Debug.Log("del canvas");
                                //gm_click.transform.Find("Canvas price").gameObjectactive = false;
                                if(pr.type == par_prise.type_price.gold)
                                    Destroy(gm_click.transform.Find("Canvas price").gameObject);
                                else
                                {
                                    anim.col_anim++;
                                    mob[x_cl,y_cl].GetComponent<Animator>().SetBool("act", true);
                                }
                                //mb_click.nps = false;
                                //mb_click.friend = true;
                                //Destroy(gm_click.transform.Find("Canvas hp").Find("pn prise").gameObject);
                            }


                        }
                        else if(pr.type == par_prise.type_price.hp && int.Parse(pr.lt_price[0])<mb.hp_max)
                        {
                            if (data.data_now.cl_shop < 20)
                                data.data_now.cl_shop++;
                            if (data.data_now.cl_shop >= 20)
                                data.give_achive("постоянный покупатель");
                            data.save_data();
                            //lt_buy = new List<string>(mb.lt_carts);





                            gm_click.GetComponent<par_prise>().buy(mb);
                            //Debug.Log("del canvas");
                            //gm_click.transform.Find("Canvas price").gameObjectactive = false;
                            Destroy(gm_click.transform.Find("Canvas price").gameObject);
                        }

                    }

                }
                //Debug.Log(mb.lt_carts_active_now.Count+"  "+ mb.lt_carts_active_now[0]);
                else if (gm_click.GetComponent<par_eath>() != null && stay_friend_atack_now() && mob_list[hod_mob_now_int].GetComponent<par_mob>().friend_type==par_mob.type_friend.active)
                {
                    //Debug.Break();
                    //par_mob pl = GameObject.Find("player").GetComponent<par_mob>();
                    par_mob pl = mb;

                    if (distance(pl.x, pl.y, x_cl, y_cl) == 1)
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

                else if (mb_click != null && gm_click.name == "nps derlorg portal" && distance(mb.x, mb.y, x_cl, y_cl) ==1 && mb_click.lt_par.Count != 0)
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
        int a1 = num_lev-10 + Random.Range(0, 90);
        string st_lvl = "";

        
         
         
        if (a1 < 75)
            st_lvl = "";
        else if (a1 < 95)
            st_lvl = " 1";
        else
            st_lvl = " 2";
        

        string gm = null;
        List<string> lt_rd = new List<string>();
        if (type == par_cart.type_cart.damage)
        {
            lt_rd.Add("cart damage" + st_lvl);
            //lt_rd.Add("cart barier");
        }
        else if (type == par_cart.type_cart.sp_barier)
        {
            lt_rd.Add("cart barier" + st_lvl);
            //lt_rd.Add("cart barier");
        }
        else if (type == par_cart.type_cart.sp_bomb)
        {
            lt_rd.Add("cart bomb" + st_lvl);
            //lt_rd.Add("cart barier");
        }
        else if (type == par_cart.type_cart.sp_dych)
        {
            lt_rd.Add("cart dych" + st_lvl);
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

        gm = rand_list(lt_rd);
        return create(gm, x, y);
    }
    public static GameObject create_cart_art(int x, int y, par_cart.type_cart type)
    {
        return create_cart_art(x, y, type, -1);
    }
    public static GameObject create_cart_art(int x, int y, par_cart.type_cart type,int random_lvl)
    {
        string gm = null;
        List<string> lt_rd = new List<string>();

        //int a1 = (num_lev + 1) / 5 + Random.Range(0, 21);
        int a1 = Random.Range(0, 90)+num_lev-10;
        int num_lev_rd = 0;
        
        string st_lvl = "";

        Debug.Log(random_lvl);
        if ((a1 < 75 && random_lvl==-1) || random_lvl==1)
        {
            num_lev_rd = 1;
            st_lvl = "";
        }
            
        else if((a1 < 95 && random_lvl == -1) || random_lvl == 2)
        {
            st_lvl = " 1";
            num_lev_rd = 2;
        }

        else if((a1 >=95 && random_lvl == -1) ||random_lvl == 4)
        {
            st_lvl = " 2";
            num_lev_rd = 4;
        }
            
        if (type == par_cart.type_cart.damage)
        {
            lt_rd.Add("cart sp atack" + st_lvl);
            lt_rd.Add("cart act att" + st_lvl);
            lt_rd.Add("cart speed att" + st_lvl);
            lt_rd.Add("cart att pow" + st_lvl);
            lt_rd.Add("cart att cuzna" + st_lvl);
            lt_rd.Add("cart att bomb" + st_lvl);
        }
        else if (type == par_cart.type_cart.sp_barier)
        {
            lt_rd.Add("cart sp barier" + st_lvl);
            lt_rd.Add("cart act bar" + st_lvl);
            lt_rd.Add("cart speed bar" + st_lvl);
            lt_rd.Add("cart bar pow" + st_lvl);
            lt_rd.Add("cart bar cuzna" + st_lvl);
            lt_rd.Add("cart bar bomb" + st_lvl);

        }
        else if (type == par_cart.type_cart.sp_bomb)
        {

            lt_rd.Add("cart sp bomb" + st_lvl);
            lt_rd.Add("cart act bomb" + st_lvl);
            lt_rd.Add("cart speed bomb" + st_lvl);
            lt_rd.Add("cart bomb pow" + st_lvl);
            lt_rd.Add("cart bomb cuzna" + st_lvl);
            //lt_rd.Add("cart bomb bomb" + st_lvl);

        }

        else if (type == par_cart.type_cart.sp_dych)
        {

            lt_rd.Add("cart sp dych" + st_lvl);
            lt_rd.Add("cart act dych" + st_lvl);
            lt_rd.Add("cart speed dych" + st_lvl);
            lt_rd.Add("cart dych pow" + st_lvl);
            lt_rd.Add("cart dych cuzna" + st_lvl);
            lt_rd.Add("cart dych bomb" + st_lvl);

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
            lt_rd.Add("cart rock bomb" + st_lvl);
            //}
            if (num_lev_rd == 2)
            {
                lt_rd.Add("cart potoc rock");
                lt_rd.Add("cart rock touch");
                lt_rd.Add("cart rock heart");
                lt_rd.Add("cart mini spirit rock");
                lt_rd.Add("cart mini mast rock");

            }
                


        }
        else if (type == par_cart.type_cart.blood)
        {
                lt_rd.Add("cart sp blood" + st_lvl);
                lt_rd.Add("cart act blood" + st_lvl);
                lt_rd.Add("cart speed blood" + st_lvl);
                lt_rd.Add("cart blood pow" + st_lvl);

            lt_rd.Add("cart blood cuzna" + st_lvl);
            lt_rd.Add("cart blood bomb" + st_lvl);

            if (num_lev_rd == 2)
            {
                lt_rd.Add("cart potoc blood");
                lt_rd.Add("cart blood touch");
                lt_rd.Add("cart blood heart");
                lt_rd.Add("cart mini spirit blood");
                lt_rd.Add("cart mini mast blood");

            }
        }
        else if (type == par_cart.type_cart.fire)
        {
                lt_rd.Add("cart sp fire" + st_lvl);
                lt_rd.Add("cart act fire" + st_lvl);
                lt_rd.Add("cart speed fire" + st_lvl);
                lt_rd.Add("cart fire pow" + st_lvl);
            lt_rd.Add("cart fire cuzna" + st_lvl);
            lt_rd.Add("cart fire bomb" + st_lvl);
            if (num_lev_rd == 2)
            {
                lt_rd.Add("cart potoc fire");
                lt_rd.Add("cart fire touch");
                lt_rd.Add("cart fire heart");
                lt_rd.Add("cart mini spirit fire");
                lt_rd.Add("cart mini mast fire");

            }
           

        }
        else if (type == par_cart.type_cart.energy)
        {
                lt_rd.Add("cart sp energy" + st_lvl);
                lt_rd.Add("cart act energy" + st_lvl);
                lt_rd.Add("cart speed energy" + st_lvl);
                lt_rd.Add("cart energy pow" + st_lvl);
            lt_rd.Add("cart energy cuzna" + st_lvl);
            lt_rd.Add("cart energy bomb" + st_lvl);
            if (num_lev_rd == 2)
            {
                lt_rd.Add("cart potoc energy");
                lt_rd.Add("cart energy touch");
                lt_rd.Add("cart energy heart");
                lt_rd.Add("cart mini spirit energy");
                lt_rd.Add("cart mini mast energy");

            }

        }
        else if (type == par_cart.type_cart.ice)
        {


                lt_rd.Add("cart sp ice" + st_lvl);
                lt_rd.Add("cart act ice" + st_lvl);
                lt_rd.Add("cart speed ice" + st_lvl);
                lt_rd.Add("cart ice pow" + st_lvl);
            lt_rd.Add("cart ice cuzna" + st_lvl);
            lt_rd.Add("cart ice bomb" + st_lvl);

            if (num_lev_rd == 2)
            {
                lt_rd.Add("cart potoc ice");
                lt_rd.Add("cart ice touch");
                lt_rd.Add("cart ice heart");
                lt_rd.Add("cart mini spirit ice");
                lt_rd.Add("cart mini mast ice");

            }

        }
        else if (type == par_cart.type_cart.poition)
        {
                lt_rd.Add("cart sp poition" + st_lvl);
                lt_rd.Add("cart act poition" + st_lvl);
                lt_rd.Add("cart speed poition" + st_lvl);
                lt_rd.Add("cart poition pow" + st_lvl);
            lt_rd.Add("cart poition cuzna" + st_lvl);
            lt_rd.Add("cart poition bomb" + st_lvl);
            if (num_lev_rd == 2)
            {
                lt_rd.Add("cart potoc poition");
                lt_rd.Add("cart poition touch");
                lt_rd.Add("cart poition heart");
                lt_rd.Add("cart mini spirit poition");
                lt_rd.Add("cart mini mast poition");

            }
        }
        else if(type == par_cart.type_cart.derlorg)
        {
            for (int i = 0; i < 2; i++)
            {
                lt_rd.Add("cart sp derlorg");
                lt_rd.Add("cart derlorg act");
                lt_rd.Add("cart derlorg speed");
                lt_rd.Add("cart derlorg potoc");
                lt_rd.Add("cart derlorg pow");
            }


            lt_rd.Add("cart derlorg att");
            lt_rd.Add("cart derlorg bar");
            lt_rd.Add("cart derlorg bomb");
            lt_rd.Add("cart derlorg fire");
            lt_rd.Add("cart derlorg poition");
            lt_rd.Add("cart derlorg ice");
            lt_rd.Add("cart derlorg energy");
            lt_rd.Add("cart derlorg rock");
            lt_rd.Add("cart derlorg blood");
        }

        else if (type == par_cart.type_cart.bases)
        {
            bool bl_leg = true;
            if (num_lev_rd == 4)
            {
                if (GameObject.Find("cart speed").GetComponent<par_cart_art>().open)
                    lt_rd.Add("cart speed");
                if (GameObject.Find("cart pow").GetComponent<par_cart_art>().open)
                    lt_rd.Add("cart pow");
                if (GameObject.Find("cart sila").GetComponent<par_cart_art>().open)
                    lt_rd.Add("cart sila");
                if (GameObject.Find("cart respawn").GetComponent<par_cart_art>().open)
                    lt_rd.Add("cart respawn");

                lt_rd.Add("cart elemental 1");
                lt_rd.Add("cart universal 1");
                bl_leg = lt_rd.Count>0;
            }
            if (num_lev_rd == 2 || (num_lev_rd == 4 && !bl_leg))
            {
                if (GameObject.Find("cart inven").GetComponent<par_cart_art>().open)
                    lt_rd.Add("cart inven");
                lt_rd.Add("cart mast");
                lt_rd.Add("cart elemental");
                lt_rd.Add("cart universal");
                lt_rd.Add("cart berserk");
                lt_rd.Add("cart dop hill");
                lt_rd.Add("cart mini spirit");
                lt_rd.Add("cart mini shaman");

                if (GameObject.Find("cart brilliant").GetComponent<par_cart_art>().open)
                    lt_rd.Add("cart brilliant");
            }
            //Debug.Log(lt_rd.Count +"  "+num_lev_rd+ " count bases");

        }
        if (lt_rd.Count > 0)
        {
            gm = lt_rd[Random.Range(0, lt_rd.Count)];
            return create(gm, x, y);
        }
        else
            return create_cart_art(x, y);

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
                a = Random.Range(0, 3);
                if (a == 0)
                    type_cart = par_cart.type_cart.sp_barier;
                else if(a==1)
                    type_cart = par_cart.type_cart.sp_bomb;
                else if (a == 2)
                    type_cart = par_cart.type_cart.sp_dych;
            }
            else
            {
                    a = Random.Range(0, 7);
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
                else if (a == 6)
                    type_cart = par_cart.type_cart.damage;


            }
        }

        if (Random.Range(0, 50) == 0 && type_cart != par_cart.type_cart.gold)
            return create_cart_art(x, y, type_cart);
        else
            return create_cart(x, y, type_cart);
    }
    public static GameObject create_cart_art(int x, int y)
    {
        return create_cart_art(x, y, -1);
    }
    public static GameObject create_cart_art(int x, int y, int lvl)
    {
        string gm = null;
        //int a1 = num_lev + num_lev_type * 4 - Random.Range(0, 15);
        //int a = Random.Range(0, 20);

        bool bl_leg = true;
        List<par_cart.type_cart> lt_rd = new List<par_cart.type_cart>() {par_cart.type_cart.bases,par_cart.type_cart.damage,par_cart.type_cart.sp_barier,par_cart.type_cart.sp_dych,par_cart.type_cart.sp_bomb,
            par_cart.type_cart.fire,par_cart.type_cart.poition,par_cart.type_cart.blood,par_cart.type_cart.rock,par_cart.type_cart.energy,par_cart.type_cart.ice };
        int i_c = lt_rd.Count;
        if (type_lvl_now == type_lvl.fire)
        {

            for (int i = 0; i < i_c; i++)
            {
                lt_rd.Add(par_cart.type_cart.fire);
            }
            
        }
            
        if (type_lvl_now == type_lvl.poition)
        {
            for (int i = 0; i < i_c; i++)
            {
                lt_rd.Add(par_cart.type_cart.poition);
            }

        }
        if (type_lvl_now == type_lvl.blood)
        {
            for (int i = 0; i < i_c; i++)
            {
                lt_rd.Add(par_cart.type_cart.blood);
            }

        }
        if (type_lvl_now == type_lvl.rock)
        {
            for (int i = 0; i < i_c; i++)
            {
                lt_rd.Add(par_cart.type_cart.rock);
            }

        }
        if (type_lvl_now == type_lvl.energy)
        {
            for (int i = 0; i < i_c; i++)
            {
                lt_rd.Add(par_cart.type_cart.energy);
            }

        }
        if (type_lvl_now == type_lvl.ice)
        {
            for (int i = 0; i < i_c; i++)
            {
                lt_rd.Add(par_cart.type_cart.ice);
            }

        }

        return create_cart_art(x, y, rand_list(lt_rd),lvl);
        /*
int var = Random.Range(0, 100);
if (var >=95)
{
    bl_leg = false;
    List<string> lt_rd = new List<string>();
    if (GameObject.Find("cart speed").GetComponent<par_cart_art>().open)
        lt_rd.Add("cart speed");
    if (GameObject.Find("cart power").GetComponent<par_cart_art>().open)
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
    lt_rd.Add("cart berserk");
    lt_rd.Add("cart dop hill");
    lt_rd.Add("cart mini spirit");
    lt_rd.Add("cart mini shaman");
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
            a = Random.Range(0, 7);
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
            else if (a == 6)
                type_cart = par_cart.type_cart.damage;
        }

    }
    return create_cart_art(x, y, type_cart);
}
//gm = "cart power";
*/
        //return create(gm, x, y);
    }
    public static GameObject create(string gm_st, int x, int y)
    {
        test_message = "error create "+gm_st;

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
            //Debug.Log("start priority " + baza[gm_st].GetComponent<par_mob>().contains_pr_at(priority_atack.type_atack.damage));
            //Debug.Log("start 1 priority " + gm.GetComponent<par_mob>().contains_pr_at(priority_atack.type_atack.damage));

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
                mb.carts_count_max.Add(art, baza[gm_st].GetComponent<par_mob>().carts_count_max[art]);
                /*
                if (GameObject.Find(art).GetComponent<par_cart>().spetial)
                    mb.carts_count_max.Add(art, 3);
                else
                    mb.carts_count_max.Add(art, 6);
                */
            }

            while (mb.lt_carts_art.Count > 0)
            {
                string st = mb.lt_carts_art[0].name;
                mb.remove_cart_art(st);
                mb.remove_cart(st);
            }
            mb.lt_carts_art.Clear();
            mb.type_priority_attack = new List<priority_atack>(baza[gm_st].GetComponent<par_mob>().type_priority_attack);
            bool bl = true;

            if(Random.Range(0, 100) <= num_lev &&
                (gm_st=="derlorg" ||
                gm_st == "derlorg shaman" ||
                gm_st == "derlorg fire" || gm_st == "derlorg poition" || gm_st == "derlorg blood" || gm_st == "derlorg rock" || gm_st == "derlorg energy" || gm_st == "derlorg ice" ||
                gm_st == "derlorg bomb" ))
            {

                int dop_rd = Random.Range(0,6);
                if (dop_rd == 0)
                {
                    mb.add_cart_art("cart derlorg act");
                }
                else if (dop_rd == 1)
                {
                    mb.add_cart_art("cart derlorg speed");
                }
                else if (dop_rd == 2)
                {
                    mb.add_cart_art("cart derlorg pow");
                }
                else
                {
                    if (gm_st == "derlorg")
                    {
                        mb.add_cart_art("cart derlorg att");
                    }
                    else if (gm_st == "derlorg shaman")
                    {
                        mb.add_cart_art("cart derlorg bar");
                    }
                    else if (gm_st == "derlorg bomb")
                    {
                        mb.add_cart_art("cart derlorg bomb");
                    }
                    else if (gm_st == "derlorg fire")
                    {
                        mb.add_cart_art("cart derlorg fire");
                    }
                    else if (gm_st == "derlorg poition")
                    {
                        mb.add_cart_art("cart derlorg poition");
                    }
                    else if (gm_st == "derlorg rock")
                    {
                        mb.add_cart_art("cart derlorg rock");
                    }
                    else if (gm_st == "derlorg blood")
                    {
                        mb.add_cart_art("cart derlorg blood");
                    }
                    else if (gm_st == "derlorg energy")
                    {
                        mb.add_cart_art("cart derlorg energy");
                    }
                    else if (gm_st == "derlorg ice")
                    {
                        mb.add_cart_art("cart derlorg ice");
                    }
                }

            }
            else if (Random.Range(0, 100) <= num_lev * 2)
            {
                bl = false;
                //aa = 8;
                //bool bl_def = true;
                if (gm_st== "defender 1"||gm_st== "defender elemental" || gm_st == "goblin hiller" || gm_st == "goblin" || gm_st == "dych")
                {
                    bl = true;
                    //mb.add_cart_art("cart dop hill");
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

                else if (gm_st == "defender")
                {
                    //bl = true;
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart att pow");
                    lt_rd_art.Add("cart act att");
                    lt_rd_art.Add("cart speed att");
                    //lt_rd_art.Add("cart att pow");
                    lt_rd_art.Add("cart att bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));

                    //mb.add_cart_art("cart act blood");
                }
                else if (gm_st == "defender barier")
                {
                    //bl = true;
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart bar pow");
                    lt_rd_art.Add("cart act bar");
                    lt_rd_art.Add("cart speed bar");
                    //lt_rd_art.Add("cart bar pow");
                    lt_rd_art.Add("cart bar bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));

                    //mb.add_cart_art("cart act blood");
                }

                else if (gm_st == "defender blood")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart blood pow");
                    lt_rd_art.Add("cart act blood");
                    lt_rd_art.Add("cart speed blood");
                    //lt_rd_art.Add("cart blood pow");
                    lt_rd_art.Add("cart blood bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }

                else if (gm_st == "defender ice")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart ice pow");
                    lt_rd_art.Add("cart act ice");
                    lt_rd_art.Add("cart speed ice");
                    //lt_rd_art.Add("cart ice pow");
                    lt_rd_art.Add("cart ice bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }
                else if (gm_st == "defender poition")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart poition pow");
                    lt_rd_art.Add("cart act poition");
                    lt_rd_art.Add("cart speed poition");
                    //lt_rd_art.Add("cart poition pow");
                    lt_rd_art.Add("cart poition bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }

                else if (gm_st == "defender rock")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart rock pow");
                    lt_rd_art.Add("cart act rock");
                    lt_rd_art.Add("cart speed rock");
                    //lt_rd_art.Add("cart rock pow");
                    lt_rd_art.Add("cart rock bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }

                else if (gm_st == "defender energy")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart energy pow");
                    lt_rd_art.Add("cart act energy");
                    lt_rd_art.Add("cart speed energy");
                    //lt_rd_art.Add("cart energy pow");
                    lt_rd_art.Add("cart energy bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }
                else if (gm_st == "defender fire")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart fire pow");
                    lt_rd_art.Add("cart act fire");
                    lt_rd_art.Add("cart speed fire");
                    //lt_rd_art.Add("cart fire pow");
                    lt_rd_art.Add("cart fire bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }
                else if (gm_st == "defender necro")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart dych pow");
                    lt_rd_art.Add("cart act dych");
                    lt_rd_art.Add("cart speed dych");
                    //lt_rd_art.Add("cart dych pow");
                    lt_rd_art.Add("cart dych bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }
                else if (gm_st == "defender bomb")
                {
                    List<string> lt_rd_art = new List<string>();
                    lt_rd_art.Add("cart bomb pow");
                    lt_rd_art.Add("cart act bomb");
                    lt_rd_art.Add("cart speed bomb");
                    lt_rd_art.Add("cart bomb pow");
                    //lt_rd_art.Add("cart dych bomb");

                    mb.add_cart_art(rand_list(lt_rd_art));
                }

                

                /*
                if (Random.Range(0, 5) == 0&&bl2)
                {
                    mb.add_cart_art("cart dop hill");
                    bl = false;
                }
                    */
                
            }
            if (gm_st == "org berserk" || gm_st == "goblin shaman")
            {
                if (Random.Range(0, 2) == 0)
                {
                    mb.add_cart_art("cart elemental");
                    for (int i = 0; i < 2; i++)
                    {
                        mb.add_cart("cart rock");
                        mb.add_cart("cart blood");
                        mb.add_cart("cart fire");
                        mb.add_cart("cart poition");
                        mb.add_cart("cart energy");
                        mb.add_cart("cart ice");
                    }
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.ice, 1));
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.fire, 1));
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.energy, 1));
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.poition, 1));

                }

                else
                {
                    mb.add_cart_art("cart universal");
                    for (int i = 0; i < 1; i++)
                    {
                        mb.add_cart("cart bomb");
                        mb.add_cart("cart dych");
                        mb.add_cart("cart barier");
                    }
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.bomb, 1));
                    mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.dych, 1));
                }
                bl = true;
            }

            else if (gm_st == "defender barier" || gm_st == "defender bomb" || gm_st == "defender necro" || gm_st == "org" || gm_st == "shaman")
            {
                //bl = true;
                //string st_art_sp = null;
                if (type_lvl_now == type_lvl.blood)
                {
                    mb.add_cart_art("cart sp blood");
                    //mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.blood, 1));
                }
                else if (type_lvl_now == type_lvl.rock)
                {
                    mb.add_cart_art("cart sp rock");
                    //mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.rock, 1));
                }
                if (type_lvl_now == type_lvl.fire)
                {
                    mb.add_cart_art("cart sp fire");
                    //mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                }
                if (type_lvl_now == type_lvl.ice)
                {
                    mb.add_cart_art("cart sp ice");
                    //mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                }
                if (type_lvl_now == type_lvl.poition)
                {
                    mb.add_cart_art("cart sp poition");
                    //mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                }
                if (type_lvl_now == type_lvl.energy)
                {
                    mb.add_cart_art("cart sp energy");
                    //mb.type_priority_attack.Add(new priority_atack(priority_atack.type_atack.barier, 1));
                }
                //mb.add_cart_art("cart act blood");
            }



            if(Random.Range(0,2)==0 && gm_st != "dych")
            {
                mb.add_cart("cart gold");
            }



            if (!mb.nps && bl && gm.transform.Find("artefacts")!=null)
            {
                Destroy(gm.transform.Find("artefacts").gameObject);
            }


            //Debug.Log(nums_mob.Count + " lt " + mb.num);
            foreach (par_cart_art art in baza[gm_st].GetComponent<par_mob>().lt_carts_art)
            {
                //Debug.Log(gm.name  +"  "+ art.name);
                par_cart_art art1 = mb.add_cart_art(art.name);
            }
            mb.hp = mb.hp_max;

            mob[x, y] = gm;

        }
        else if (gm.GetComponent<par_nat>() != null)
        {
            //Debug.Log("create nat 1 " + gm_st);
            par_nat p_n = gm.GetComponent<par_nat>();
            p_n.x = x;
            p_n.y = y;
            nature[x, y] = gm;
            /*
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
            */
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
    public static List<string> get_bosses()
    {
        List<string> lt_el_mob = new List<string>();
        lt_el_mob.Add("org berserk");
        lt_el_mob.Add("goblin shaman");

        lt_el_mob.Add("fire elemental");
        lt_el_mob.Add("poition elemental");
        lt_el_mob.Add("energy elemental");
        lt_el_mob.Add("ice elemental");
        lt_el_mob.Add("blood elemental");
        lt_el_mob.Add("rock elemental");

        lt_el_mob.Add("fire master");
        lt_el_mob.Add("poition master");
        lt_el_mob.Add("energy master");
        lt_el_mob.Add("ice master");
        lt_el_mob.Add("blood master");
        lt_el_mob.Add("rock master");
        return lt_el_mob;
    }
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

    public static void clear_room(List<GameObject> lt_fr)
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
                lt_v2_viz_mob[j, i] = new List<GameObject>();
                lt_v2_viz_friend[j, i] = new List<GameObject>();
            }
        }
        nums_mob.Clear();
        nums_mob_wolk.Clear();
        foreach (GameObject gm in lt_fr)
        {
            par_mob mbs = gm.GetComponent<par_mob>();
            nums_mob.Add(mbs.num);
            mob_list[mbs.num] = gm;
        }
        //mob_list[0] = GameObject.Find("player");
        //nums_mob.Add(0);

    }
    public static void generate_room_obych()
    {
        List<GameObject> lt_fr = new List<GameObject>();
        if (!(num_lev == 0))
        {
            clear_room(lt_fr);

        }
        else
            lt_fr.Add(GameObject.Find(data.data_now.pl_name_now));

        int st_x = 5;
        int st_y = 5;
        par_mob pr_teacher = null;
        for (int i = 0; i < size_global; i++)
        {
            for (int j = 0; j < size_global; j++)
            {
                string st_eath = "";
                if (Random.Range(0, 2) == 0)
                    st_eath = "grass start 1";
                else
                    st_eath = "grass start 2";

                if(i==5 && (j>=5 && j<=20))
                    st_eath = "eath start";
                if ((j == 9 ||j==13)&& (i >= 2 && i < 5))
                    st_eath = "eath start";
                if(j>=5 && j<15 && i>=12 && i<=22 )
                    st_eath = "eath start";
                create(st_eath, j, i);

                string st_nature = null;
                if(j==8 || j == 10 || j == 12 || j == 14)
                {
                    if(i<=4 && i >= 2)
                    {
                        st_nature = "wood wall v";
                    }
                }
                if ((j == 9||j==13) && i == 1)
                    st_nature = "wood wall h";
                if ((j == 8 || j==10 || j == 12 || j == 14) && i == 1)
                    st_nature = "wood wall hv";

                if (j >= 5 && j <= 15 && (i == 12 || i == 22))
                    st_nature = "wood wall h";
                if ((j == 5 || j == 15) && i >= 12 && i <= 22)
                    st_nature = "wood wall v";
                if ((j == 5 || j == 15) && (i == 12 || i == 22))
                    st_nature = "wood wall hv";

                if (j==9 && i==2)
                    st_nature = "manecen";

                if (j == 13 && i == 2)
                    st_nature = "manecen";

                if (j == 25 && i == 7)
                    st_nature = "teacher";

                if(j%3==0 && i%3==0 && ((j<=3 || j>= size_global-1 - 3) &&( i <= 3 || i >= size_global - 1 - 3)))
                    st_nature = "cyst";
                if (st_nature != null)
                {
                    GameObject gm= create(st_nature, j, i);
                    if (st_nature == "teacher")
                    {
                        gm.GetComponent<par_mob>().simm(-1);
                        pr_teacher = gm.GetComponent<par_mob>();
                    }
                        
                }
                    

            }
        }
        end_generate_room(lt_fr,st_x,st_y);
        priority_hod = pr_teacher.num;
        pr_teacher.action_after_wolk_now = new action_after_wolk(() => 
        {
            priority_hod = -1;
            next_hod();
            pr_teacher.gameObject.GetComponent<par_dialog>().start_dialog("start");
        }
        );
        pr_teacher.lt_wolk = func.way_wolk(pr_teacher.x, pr_teacher.y, 5, 7);
        pr_teacher.wolk_v2();
        //pr_teacher.time_wolk = -4;
        next_hod();
        Debug.Break();
        /*
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
        */
    }
    public static void generate_room()
    {
        //test_message = "error generate";


        Debug.Log("level" + num_lev + "  " + num_lev_type);
        List<GameObject> lt_fr = new List<GameObject>();
        if (!(num_lev == 0))
        {
            clear_room(lt_fr);

        }
        else
            lt_fr.Add(GameObject.Find(data.data_now.pl_name_now));
        //cart_effects.Clear();
        //baza.Clear();
        //num_lev = 0;
        //num_lev_type = 0;



        int st_x, st_y;
        int x_end = -1, y_end = -1;
        while (true)
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
            if (distance(st_x,st_y,x_end,y_end)   == size_global - 1)
                break;
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
                if (Random.Range(0, 100) != 0 || (grass_location[type_lvl_now]==null))
                    create(eath_location[type_lvl_now], j, i);
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
                                        create(grass_location[type_lvl_now], j1, i1);
                                }
                            }
                        }
                    }
                }

                create(rock_location[type_lvl_now], j, i);
                //Debug.Log(nature[j, i] == null);
            }
        }
        int ways_points = Random.Range(3, 6);
        int range_way = 8;
        int dop_marging = 0;
        bool boss_lev = false;

        if (((num_lev + 1) % 5 == 0 && num_lev != 0) )
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

        while (lt_points.Count - 2 < ways_points)
        {
            int x_rd = Random.Range(3 + dop_marging, size_global - 3 - dop_marging);
            int y_rd = Random.Range(3 + dop_marging, size_global - 3 - dop_marging);

            int[,] mas_way = way(x_rd, y_rd, range_way);
            bool bl = true;
            foreach (Vector2 v2 in lt_points)
            {

                if (mas_way[(int)v2.x, (int)v2.y] != -1 && mas_way[(int)v2.x, (int)v2.y] >3)
                {
                    bl = false;
                    break;
                }
            }
            if (bl)
            {
                lt_points.Add(new Vector2(x_rd, y_rd));
            }

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
                        ln = way_st[(int)v2_.x, (int)v2_.y];
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





        int col_nps = 0;
        foreach (Vector2 v2 in lt_points_sp)
        {
            r = 1;
            int x_sp = (int)v2.x;
            int y_sp = (int)v2.y;
            if (!(x_sp == st_x && y_sp == st_y) && !(x_sp == x_end && y_sp == y_end))
            {
                if (boss_lev )
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
                    int a_boss = 1;
                    if ((num_lev + 1) >= 15)
                        a_boss = (num_lev + 1 - 5) / 5;
                    //a_boss = 4;
                    // Debug.Log(a_boss+" boss");
                    //a = 0;
                    if (a_boss > 6)
                        a_boss = 6;

                    if (  !(type_lvl_now==type_lvl.derlorg || (type_lvl_now != type_lvl.derlorg && (num_lev+1==25))))
                    {
                        create(rock_location[type_lvl_now], x_sp - 1, y_sp - 1);
                        create(rock_location[type_lvl_now], x_sp + 1, y_sp - 1);
                        create(rock_location[type_lvl_now], x_sp - 1, y_sp + 1);
                        create(rock_location[type_lvl_now], x_sp + 1, y_sp + 1);
                    }
                    else
                    {
                        a_boss = 0;
                        if(type_lvl_now == type_lvl.derlorg)
                        {
                            create("nps derlorg final", x_sp, y_sp+2);
                            create("heart derlorg", x_sp, y_sp);
                        }
                        else if (type_lvl_now != type_lvl.derlorg && (num_lev + 1 == 25))
                        {
                            create("nps defender final", x_sp, y_sp+2);
                            create("temple", x_sp, y_sp);
                        }
                    }


                    //a = 4;

                    //if (type_lvl_now != type_lvl.derlorg)
                    //{
                    while (a_boss > 0)
                    {
                        int x_rd = Random.Range(x_sp - 2, x_sp + 2 + 1);
                        int y_rd = Random.Range(y_sp - 2, y_sp + 2 + 1);
                        if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1)
                        {
                            if (object_bool(x_rd, y_rd))
                            {

                                if (a_boss > 0)
                                {
                                    if(num_lev + 1 == 5 )
                                    {
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

                                        create(rand_list(lt_el_mob), x_rd, y_rd);
                                    }
                                    else if (type_lvl_now ==type_lvl.bases || type_lvl_now==type_lvl.bases_1)
                                    {
                                        List<string> lt_el_mob = new List<string>();
                                        lt_el_mob.Add("org berserk");
                                        lt_el_mob.Add("goblin shaman");

                                        lt_el_mob.Add("fire elemental");
                                        lt_el_mob.Add("poition elemental");
                                        lt_el_mob.Add("energy elemental");
                                        lt_el_mob.Add("ice elemental");
                                        lt_el_mob.Add("blood elemental");
                                        lt_el_mob.Add("rock elemental");

                                        //lt_el_mob.Add("damage master");
                                        
                                        lt_el_mob.Add("fire master");
                                        lt_el_mob.Add("poition master");
                                        lt_el_mob.Add("energy master");
                                        lt_el_mob.Add("ice master");
                                        lt_el_mob.Add("blood master");
                                        lt_el_mob.Add("rock master");
                                        
                                        create(rand_list(lt_el_mob), x_rd, y_rd);

                                    }
                                    else if (type_lvl_now == type_lvl.fire)
                                    {
                                        List<string> lt_el_mob = new List<string>();
                                        lt_el_mob.Add("org berserk");
                                        lt_el_mob.Add("goblin shaman");

                                        lt_el_mob.Add("fire elemental");
                                        lt_el_mob.Add("fire elemental");
                                        lt_el_mob.Add("fire elemental");

                                        lt_el_mob.Add("fire master");
                                        lt_el_mob.Add("fire master");
                                        lt_el_mob.Add("fire master");


                                        create(rand_list(lt_el_mob), x_rd, y_rd);

                                    }
                                    else if (type_lvl_now == type_lvl.poition)
                                    {
                                        List<string> lt_el_mob = new List<string>();
                                        lt_el_mob.Add("org berserk");
                                        lt_el_mob.Add("goblin shaman");

                                        lt_el_mob.Add("poition elemental");
                                        lt_el_mob.Add("poition elemental");
                                        lt_el_mob.Add("poition elemental");

                                        lt_el_mob.Add("poition master");
                                        lt_el_mob.Add("poition master");
                                        lt_el_mob.Add("poition master");

                                        create(rand_list(lt_el_mob), x_rd, y_rd);

                                    }
                                    else if (type_lvl_now == type_lvl.blood)
                                    {
                                        List<string> lt_el_mob = new List<string>();
                                        lt_el_mob.Add("org berserk");
                                        lt_el_mob.Add("goblin shaman");

                                        lt_el_mob.Add("blood elemental");
                                        lt_el_mob.Add("blood elemental");
                                        lt_el_mob.Add("blood elemental");

                                        lt_el_mob.Add("blood master");
                                        lt_el_mob.Add("blood master");
                                        lt_el_mob.Add("blood master");


                                        create(rand_list(lt_el_mob), x_rd, y_rd);

                                    }
                                    else if (type_lvl_now == type_lvl.rock)
                                    {
                                        List<string> lt_el_mob = new List<string>();
                                        lt_el_mob.Add("org berserk");
                                        lt_el_mob.Add("goblin shaman");

                                        lt_el_mob.Add("rock elemental");
                                        lt_el_mob.Add("rock elemental");
                                        lt_el_mob.Add("rock elemental");

                                        lt_el_mob.Add("rock master");
                                        lt_el_mob.Add("rock master");
                                        lt_el_mob.Add("rock master");


                                        create(rand_list(lt_el_mob), x_rd, y_rd);

                                    }
                                    else if (type_lvl_now == type_lvl.ice)
                                    {
                                        List<string> lt_el_mob = new List<string>();
                                        lt_el_mob.Add("org berserk");
                                        lt_el_mob.Add("goblin shaman");

                                        lt_el_mob.Add("ice elemental");
                                        lt_el_mob.Add("ice elemental");
                                        lt_el_mob.Add("ice elemental");

                                        lt_el_mob.Add("ice master");
                                        lt_el_mob.Add("ice master");
                                        lt_el_mob.Add("ice master");


                                        create(rand_list(lt_el_mob), x_rd, y_rd);

                                    }
                                    else if (type_lvl_now == type_lvl.energy)
                                    {
                                        List<string> lt_el_mob = new List<string>();
                                        lt_el_mob.Add("org berserk");
                                        lt_el_mob.Add("goblin shaman");

                                        lt_el_mob.Add("energy elemental");
                                        lt_el_mob.Add("energy elemental");
                                        lt_el_mob.Add("energy elemental");

                                        lt_el_mob.Add("energy master");
                                        lt_el_mob.Add("energy master");
                                        lt_el_mob.Add("energy master");


                                        create(rand_list(lt_el_mob), x_rd, y_rd);

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
                    if (col_nps==0)
                        a_rd = 1;
                    //a_rd = 3;
                    //a_rd = 0;
                    if (a_rd == 0 )
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

                        if (Random.Range(0, 20) == 0 )
                        {
                            create(facel_location[type_lvl_now], x_sp, y_sp);

                            //List<string> lt_rd = ;
                            /*
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
                            */
                            //int a = 2 + Random.Range(0, 6) / 3;
                            int a_type = Random.Range(0, 3) ;
                            int a_ = a_type + (a_type / 2) + 1;
                            a_type =a_ ;
                            Debug.Log(a_type);
                            int a = 4 / a_type;
                            for (int i = 0; i < 100; i++)
                            {
                                int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                                int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                                if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && way_[x_rd, y_rd] != -1 && nature[x_rd, y_rd] == null && carts[x_rd, y_rd] == null)
                                {
                                    //string stt = lt_rd[Random.Range(0, lt_rd.Count)];
                                    //lt_rd.Remove(stt);
                                    if(a_type>1 && Random.Range(0, 2) == 0)
                                    {
                                        create_cart_art(x_rd, y_rd,par_cart.type_cart.bases, a_type);
                                    }
                                    else
                                        create_cart_art(x_rd, y_rd,a_type);

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

                            int a = Random.Range(4,7 );
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
                            create(facel_location[type_lvl_now], x_sp, y_sp);
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
                                cart_rd = Random.Range(0, 3);
                                if (cart_rd == 0)
                                    tp_cart = par_cart.type_cart.sp_barier;
                                if (cart_rd == 1)
                                    tp_cart = par_cart.type_cart.sp_dych;
                                else
                                    tp_cart = par_cart.type_cart.sp_bomb;
                                col_max = 4;
                            }
                            else
                            {
                                if (Random.Range(0, 2) == 0)
                                    tp_cart = par_cart.type_cart.damage;
                                else
                                {
                                    cart_rd = Random.Range(0, 6);
                                    if (cart_rd == 0 || (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.fire))
                                        tp_cart = par_cart.type_cart.fire;
                                    else if (cart_rd == 1 ||(Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.blood))
                                        tp_cart = par_cart.type_cart.blood;
                                    else if (cart_rd == 2 || (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.poition))
                                        tp_cart = par_cart.type_cart.poition;
                                    else if (cart_rd == 3 || (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.energy))
                                        tp_cart = par_cart.type_cart.energy;
                                    else if (cart_rd == 4 || (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.ice))
                                        tp_cart = par_cart.type_cart.ice;
                                    else if (cart_rd == 5 || (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.rock))
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

                    }
                    else if (a_rd == 1 &&( (col_nps<1 && num_lev<2) || (col_nps <2 && num_lev >= 2)))
                    {
                        if (col_nps==1)
                        {
                            List<int> lt_rd = new List<int> { 0, 1, 2, 3, 4 };
                            
                            if (!(num_lev >= 15 && (num_lev % 5 == 0) && type_lvl_now != type_lvl.derlorg))
                                lt_rd.Remove(0);
                            if ( !(  (num_lev >= 5) && type_lvl_now != type_lvl.derlorg)  )
                                lt_rd.Remove(1);

                            int rd1 = rand_list(lt_rd);

                            if (rd1==0)
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
                                        par_mob nps_mob = create("nps derlorg portal", x_rd, y_rd).GetComponent<par_mob>();
                                        nps_mob.lt_par.Add(new Vector2(x_sp, y_sp));
                                        break;
                                        //nat[x_rd, y_rd] = true;
                                        //break;
                                    }
                                }
                                //create("nps derlorg", x_sp, y_sp);

                            }
                            else if (rd1==1)
                            {
                                //bool_nps = false;
                                par_mob nps_mob = create("nps derlorg", x_sp, y_sp).GetComponent<par_mob>();
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

                                int a_tovar = Random.Range(3, 5);
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

                                        int col_price = 0;
                                        /*if (a_tovar == 2)
                                        {
                                            string cart_create = "cart sp derlorg";


                                            par_cart cart = create(cart_create, x_rd, y_rd).GetComponent<par_cart>();
                                            col_price = cart.lvl * 3;
                                        }
                                        else */
                                        if (a_tovar == 1)
                                        {
                                            par_cart cart = create_cart(x_rd, y_rd, type_cart.derlorg).GetComponent<par_cart>();
                                            cart.col = 6;
                                            col_price = cart.lvl;
                                        }
                                        else
                                        {
                                            par_cart cart = create_cart_art(x_rd, y_rd, type_cart.derlorg).GetComponent<par_cart>();
                                            col_price = cart.lvl * 3;
                                        }
                                        eath[x_rd, y_rd].AddComponent<par_prise>();
                                        eath[x_rd, y_rd].GetComponent<par_prise>().add_price(null, col_price, carts[x_rd, y_rd].GetComponent<par_cart>().col, par_prise.type_price.hp);

                                        a_tovar--;

                                        //}
                                        //Destroy(eath[x_rd, y_rd]);

                                        //nat[x_rd, y_rd] = true;
                                        //break;
                                    }
                                }

                            }
                            else if (rd1==2)
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


                                create("nps baron", x_sp, y_sp);

                                int a_tovar = Random.Range(2, 5);
                                a_tovar = 4;
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

                                        //if (way_find_lt(carts, x_rd, y_rd, 2).Count==0 && (a_rock_v+a_rock_h>2 || (a_rock_h!=2 && a_rock_v!=2)))
                                        //{
                                        int col_price = 0;
                                        int col_lvl = 2;
                                        if (a_tovar == 1)
                                            col_lvl = 4;
                                        else
                                        {
                                            col_lvl = (Random.Range(0, 4) / 4) * 2 + 2;
                                        }
                                        Debug.Log("to create " + col_lvl);
                                        par_cart cart = create_cart_art(x_rd, y_rd, type_cart.bases, col_lvl).GetComponent<par_cart>();
                                        col_price = cart.lvl * 3;

                                        eath[x_rd, y_rd].AddComponent<par_prise>();
                                        eath[x_rd, y_rd].GetComponent<par_prise>().add_price("cart gold", col_price, carts[x_rd, y_rd].GetComponent<par_cart>().col, par_prise.type_price.gold);

                                        a_tovar--;


                                    }
                                }





                            }
                            else if(rd1==3)
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
                                eath[x_sp, y_sp].AddComponent<par_prise>();
                                eath[x_sp, y_sp].GetComponent<par_prise>().add_price("cart gold", 1, 1, par_prise.type_price.obmen);

                            }
                            else if (rd1 == 4)
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

                                create("nps gold goblin", x_sp, y_sp);
                                //eath[x_sp, y_sp].AddComponent<par_prise>();
                                //eath[x_sp, y_sp].GetComponent<par_prise>().add_price("cart gold", 1, 1, par_prise.type_price.obmen);

                            }
                        }

                        
                        else
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

                                int cart_rd = Random.Range(0, 10);
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

                                else if (cart_rd == 6)
                                {
                                    type_cart = par_cart.type_cart.damage;
                                    create("nps damage", x_sp, y_sp);
                                }
                                else if (cart_rd == 7)
                                {
                                    type_cart = par_cart.type_cart.sp_barier;
                                    create("nps barier", x_sp, y_sp);
                                }
                                else if (cart_rd == 8)
                                {
                                    type_cart = par_cart.type_cart.sp_bomb;
                                    create("nps bomb", x_sp, y_sp);
                                }
                                else if (cart_rd == 9)
                                {
                                    type_cart = par_cart.type_cart.sp_dych;
                                    create("nps dych", x_sp, y_sp);
                                }


                                int a_tovar = Random.Range(3, 5);
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

                                        int col_price = 0;
                                        if (a_tovar == 2)
                                        {
                                            string cart_create = null;
                                            if (type_cart == par_cart.type_cart.fire)
                                            {
                                                //type_cart = par_cart.type_cart.fire;
                                                //create("nps fire", x_sp, y_sp);
                                                cart_create = "cart sp fire";
                                            }

                                            else if (type_cart == par_cart.type_cart.blood)
                                            {
                                                cart_create = "cart sp blood";
                                                //create("nps blood", x_sp, y_sp);
                                            }

                                            else if (type_cart == par_cart.type_cart.poition)
                                            {
                                                cart_create = "cart sp poition";
                                            }

                                            else if (type_cart == par_cart.type_cart.energy)
                                            {
                                                cart_create = "cart sp energy";
                                            }

                                            else if (type_cart == par_cart.type_cart.ice)
                                            {
                                                cart_create = "cart sp ice";
                                            }

                                            else if (type_cart == par_cart.type_cart.rock)
                                            {
                                                cart_create = "cart sp rock";
                                            }

                                            else if (type_cart == par_cart.type_cart.damage)
                                            {
                                                cart_create = "cart sp atack";
                                            }
                                            else if (type_cart == par_cart.type_cart.sp_barier)
                                            {
                                                cart_create = "cart sp barier";
                                            }
                                            else if (type_cart == par_cart.type_cart.sp_bomb)
                                            {
                                                cart_create = "cart sp bomb";
                                            }
                                            else if (type_cart == par_cart.type_cart.sp_dych)
                                            {
                                                cart_create = "cart sp dych";
                                            }

                                            par_cart cart = create(cart_create, x_rd, y_rd).GetComponent<par_cart>();
                                            col_price = cart.lvl * 3;
                                        }
                                        else if (a_tovar == 1)
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
                                            col_price = cart.lvl * 3;
                                        }
                                        eath[x_rd, y_rd].AddComponent<par_prise>();
                                        eath[x_rd, y_rd].GetComponent<par_prise>().add_price("cart gold", col_price, carts[x_rd, y_rd].GetComponent<par_cart>().col, par_prise.type_price.gold);

                                        a_tovar--;

                                        //}
                                        //Destroy(eath[x_rd, y_rd]);

                                        //nat[x_rd, y_rd] = true;
                                        //break;
                                    }
                                }
                            



                        }
                        col_nps++;



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
                        if (num_lev < 20)
                            aa = num_lev / 4;
                        else
                            aa = 5;
                        int a = Random.Range(2 + aa / 2, 3 + aa);
                        //a = 6;
                        bool rd_sp_mob = (Random.Range(0, 100) < num_lev && num_lev >15 && num_lev<25)|| (Random.Range(0, 100) < 25 && num_lev >= 25);
                        bool rd_bl_sp = (Random.Range(0, 2) == 0);
                        //a = 6;
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
                                        
                                        if (rd_sp_mob)
                                        {
                                            if(rd_bl_sp)
                                                create("org", x_rd, y_rd);
                                            else
                                                create("shaman", x_rd, y_rd);
                                        }

                                    else if ((Random.Range(0, 100) < (num_lev+1) * 5) && num_lev>1 )
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
                                        if (Random.Range(0, 5) == 0 && num_lev!=0)
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
                                    else if (type_lvl_now != type_lvl.derlorg)
                                    {
                                        bool rd = ((Random.Range(0, 100) < num_lev+15 && num_lev < 35) || (Random.Range(0, 100) < 50 && num_lev >= 35)) && (num_lev >= 10);
                                        //rd = true;
                                        if (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.rock)
                                        {

                                            if (rd)
                                            {
                                                if (Random.Range(0, 2) == 0)
                                                    create("rock defender 1", x_rd, y_rd);
                                                else
                                                    create("half-rock spirit", x_rd, y_rd);
                                            }
                                            else
                                                create("defender rock", x_rd, y_rd);
                                        }

                                        else if (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.fire)
                                        {

                                            if (rd)
                                            {
                                                if (Random.Range(0, 2) == 0)
                                                    create("fire defender 1", x_rd, y_rd);
                                                else
                                                    create("half-fire spirit", x_rd, y_rd);
                                            }
                                            else
                                                create("defender fire", x_rd, y_rd);
                                        }
                                        else if (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.blood)
                                        {

                                            if (rd)
                                            {
                                                if (Random.Range(0, 2) == 0)
                                                    create("blood defender 1", x_rd, y_rd);
                                                else
                                                    create("half-blood spirit", x_rd, y_rd);
                                            }
                                            else
                                                create("defender blood", x_rd, y_rd);
                                        }
                                        else if (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.ice)
                                        {

                                            if (rd)
                                            {
                                                if (Random.Range(0, 2) == 0)
                                                    create("ice defender 1", x_rd, y_rd);
                                                else
                                                    create("half-ice spirit", x_rd, y_rd);
                                            }
                                            else
                                                create("defender ice", x_rd, y_rd);
                                        }
                                        else if (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.poition)
                                        {

                                            if (rd)
                                            {
                                                if (Random.Range(0, 2) == 0)
                                                    create("poition defender 1", x_rd, y_rd);
                                                else
                                                    create("half-poition spirit", x_rd, y_rd);
                                            }
                                            else
                                                create("defender poition", x_rd, y_rd);
                                        }
                                        else if (Random.Range(0, 2) == 0 && type_lvl_now == type_lvl.energy)
                                        {

                                            if (rd)
                                            {
                                                if (Random.Range(0, 2) == 0)
                                                    create("energy defender 1", x_rd, y_rd);
                                                else
                                                    create("half-energy spirit", x_rd, y_rd);
                                            }
                                            else
                                                create("defender energy", x_rd, y_rd);
                                        }
                                        else if ((Random.Range(0, 100) < num_lev && num_lev < 10) || ((Random.Range(0, 100) < 10 && num_lev >= 10)))
                                        {
                                            if (Random.Range(0, 2) == 0)
                                            {
                                                create("defender 1", x_rd, y_rd);
                                            }
                                            else
                                            {
                                                create("defender elemental", x_rd, y_rd);
                                            }
                                        }
                                        else if((Random.Range(0, 100) < num_lev * 2 && num_lev < 25) || (Random.Range(0, 100) < 50 && num_lev >= 25))
                                        {
                                            if (rd)
                                            {
                                                int rdd = Random.Range(0, 14);
                                                if (rdd == 0)
                                                {
                                                    if (Random.Range(0, 2) == 0)
                                                        create("rock defender 1", x_rd, y_rd);
                                                    else
                                                        create("half-rock spirit", x_rd, y_rd);
                                                }
                                                //create("defender rock", x_rd, y_rd);
                                                else if (rdd == 1)
                                                {
                                                    if (Random.Range(0, 2) == 0)
                                                        create("fire defender 1", x_rd, y_rd);
                                                    else
                                                        create("half-fire spirit", x_rd, y_rd);
                                                }
                                                //create("defender fire", x_rd, y_rd);
                                                else if (rdd == 2)
                                                {
                                                    if (Random.Range(0, 2) == 0)
                                                        create("blood defender 1", x_rd, y_rd);
                                                    else
                                                        create("half-blood spirit", x_rd, y_rd);
                                                }
                                                //create("defender blood", x_rd, y_rd);
                                                else if (rdd == 3)
                                                {
                                                    if (Random.Range(0, 2) == 0)
                                                        create("ice defender 1", x_rd, y_rd);
                                                    else
                                                        create("half-ice spirit", x_rd, y_rd);
                                                }
                                                //create("defender ice", x_rd, y_rd);
                                                else if (rdd == 4)
                                                {
                                                    if (Random.Range(0, 2) == 0)
                                                        create("poition defender 1", x_rd, y_rd);
                                                    else
                                                        create("half-poition spirit", x_rd, y_rd);
                                                }
                                                //create("defender poition", x_rd, y_rd);
                                                else if (rdd == 5)
                                                {
                                                    if (Random.Range(0, 2) == 0)
                                                        create("energy defender 1", x_rd, y_rd);
                                                    else
                                                        create("half-energy spirit", x_rd, y_rd);
                                                }
                                                else if (rdd == 6)
                                                {
                                                    create("org", x_rd, y_rd);
                                                }
                                                else if (rdd == 7)
                                                {
                                                    create("shaman", x_rd, y_rd);
                                                }
                                                else
                                                {
                                                    rdd = (rdd - 8) / 2;
                                                    if (rdd == 0)
                                                        create("defender necro", x_rd, y_rd);
                                                    else if (rdd == 1)
                                                        create("defender bomb", x_rd, y_rd);
                                                    else if (rdd == 2)
                                                        create("defender barier", x_rd, y_rd);
                                                }
                                            }
                                            else
                                            {
                                                int rdd = Random.Range(0, 12);
                                                if (rdd == 0)
                                                {
                                                    create("defender rock", x_rd, y_rd);
                                                }
                                                //create("defender rock", x_rd, y_rd);
                                                else if (rdd == 1)
                                                {
                                                    create("defender fire", x_rd, y_rd);
                                                }
                                                //create("defender fire", x_rd, y_rd);
                                                else if (rdd == 2)
                                                {
                                                    create("defender blood", x_rd, y_rd);
                                                }
                                                //create("defender blood", x_rd, y_rd);
                                                else if (rdd == 3)
                                                {
                                                    create("defender ice", x_rd, y_rd);
                                                }
                                                //create("defender ice", x_rd, y_rd);
                                                else if (rdd == 4)
                                                {
                                                    create("defender poition", x_rd, y_rd);
                                                }
                                                //create("defender poition", x_rd, y_rd);
                                                else if (rdd == 5)
                                                {
                                                    create("defender energy", x_rd, y_rd);
                                                }
                                                else
                                                {
                                                    rdd = (rdd - 6) / 2;
                                                    if (rdd == 0)
                                                        create("defender necro", x_rd, y_rd);
                                                    else if (rdd == 1)
                                                        create("defender bomb", x_rd, y_rd);
                                                    else if (rdd == 2)
                                                        create("defender barier", x_rd, y_rd);
                                                }
                                            }
                                        }


                                        else
                                        {
                                            create("defender", x_rd, y_rd);
                                        }

                                    }
                                    else if (type_lvl_now == type_lvl.derlorg)
                                    {
                                        if (Random.Range(0, 100) < 50)
                                        {
                                            if(Random.Range(0, 100) < 50)
                                            {
                                                int rdd = Random.Range(0, 3);
                                                if (rdd == 0)
                                                    create("derlorg shaman", x_rd, y_rd);
                                                if (rdd == 1)
                                                    create("derlorg necro", x_rd, y_rd);
                                                else if (rdd == 2)
                                                    create("derlorg bomb", x_rd, y_rd);
                                            }
                                            else
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

                                        }
                                        else
                                        {
                                            
                                                create("derlorg", x_rd, y_rd);
                                            
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

                                create(camp_location[type_lvl_now], x_rd, y_rd);
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

                            if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (way_find_lt(nature, GameObject.Find(rock_location[type_lvl_now]), x_rd, y_rd, 1).Count != 0) && object_bool(x_rd, y_rd) && carts[x_rd, y_rd] == null)
                            {
                                create(stalact_location[type_lvl_now], x_rd, y_rd);
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
                    //Debug.Log("end  " + x_end + " " + y_end+" "+ camp_location[type_lvl.bases]);
                    if ((num_lev + 1) % 5 == 0 && num_lev != 0)
                    {
                        if((num_lev + 1)==5)
                            create("cave 1", x_end, y_end);
                        else
                        {
                            List<string> lt_rd = new List<string>();
                            if (type_lvl_now != type_lvl.fire)
                                lt_rd.Add(cave_location[type_lvl.fire]);
                            if (type_lvl_now != type_lvl.ice)
                                lt_rd.Add(cave_location[type_lvl.ice]);
                            if (type_lvl_now != type_lvl.energy)
                                lt_rd.Add(cave_location[type_lvl.energy]);
                            if (type_lvl_now != type_lvl.poition)
                                lt_rd.Add(cave_location[type_lvl.poition]);
                            if (type_lvl_now != type_lvl.blood)
                                lt_rd.Add(cave_location[type_lvl.blood]);
                            if (type_lvl_now != type_lvl.rock)
                                lt_rd.Add(cave_location[type_lvl.rock]);
                            create(rand_list(lt_rd), x_end, y_end);
                        }
                    }
                    else
                    {
                        create(cave_location[type_lvl_now], x_end, y_end);
                    }
                    
                    

                    int aa = 4;
                    //aa = 1;
                    for (int i = 0; i < 100; i++)
                    {
                        int x_rd = Random.Range(x_sp - r, x_sp + r + 1);
                        int y_rd = Random.Range(y_sp - r, y_sp + r + 1);
                        int c = Mathf.Abs(x_rd - x_sp) + Mathf.Abs(y_rd - y_sp);

                        bool[] bl_n = new bool[4];

                        if (x_rd > 0 && x_rd < size_global - 1 && y_rd > 0 && y_rd < size_global - 1 && (way_find_lt(nature, GameObject.Find(rock_location[type_lvl_now]), x_rd, y_rd, 1).Count != 0) && object_bool(x_rd, y_rd) && carts[x_rd, y_rd] == null)
                        {
                            Debug.Log("decor create " + stalact_location[type_lvl_now]);
                            create(stalact_location[type_lvl_now], x_rd, y_rd);
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

        end_generate_room(lt_fr, st_x, st_y);



        //nat[st_x, st_y] = false;




        //test_message = "good";
        //GameObject.Find("player").GetComponent<par_mob>().wolk(st_x, st_y);

    }
    public static void end_generate_room(List<GameObject> lt_fr,int st_x,int st_y)
    {
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
                bool bl = gm.GetComponent<par_mob>().num == 0;
                


                if (object_bool(x_rd, y_rd) &&( (bl&&(x_rd == st_x && y_rd == st_y)) ||(!bl && !(x_rd==st_x && y_rd==st_y))  ))
                {
                    gm.GetComponent<par_mob>().x = x_rd;
                    gm.GetComponent<par_mob>().y = y_rd;
                    gm.GetComponent<par_mob>().wolk(x_rd, y_rd, true);
                    break;
                }
                //nums_mob_wolk.Add(gm_wolk_friend.GetComponent<par_mob>().num);

            }

        }

        foreach (int id in nums_mob)
        {
            mob_list[id].GetComponent<par_mob>().update_viz();
        }



        num_lev++;
        //num_lev = 20;
        //type_lvl_now=

        GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<Animator>().Play("down", -1, 0);
        GameObject.Find("tx lvl").GetComponent<Animator>().Play("invize", -1, 0);
        GameObject.Find("tx lvl").GetComponent<Text>().text = num_lev + " этаж";

        int count_mast = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().lt_carts_art.ConvertAll(x => x.name).Where(x => (x == "cart mast")).Count();
        foreach (par_cart_art art in GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().lt_carts_art)
        {
            if (art.tp_perez == par_cart_art.type_perez.perehod_lvl)
            {
                art.perez_now -= (1 + count_mast);
                if (art.perez_now < 0)
                    art.perez = 0;
                if (art.perez_now == 0 && art.name == "cart brilliant")
                    GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().activate_art(art);
            }
        }

        add.load_add();
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
        string name = "";
        //List<par_cart.type_cart> type_cart = new List<par_cart.type_cart>();

        //List<List<string>> lt_lt_st = par_cart.cart_combinations.get_comb_now(st_comb);

            for (int i = 0; i < lt_carts.Count; i++)
            {
            if ((i == 0)&&( lt_carts[i] == "cart bomb" || lt_carts[i] == "cart bomb 1" || lt_carts[i] == "cart bomb 2" || lt_carts[i] == "cart derlorg" || lt_carts[i] == "cart derlorg 1" || lt_carts[i] == "cart derlorg 2"))
            {
                    return lt_carts[i];
            }
            else if (lt_carts.Contains("cart spirit(boss)")
                || lt_carts.Contains("cart spirit fire")
                || lt_carts.Contains("cart spirit blood")
                || lt_carts.Contains("cart spirit ice")
                || lt_carts.Contains("cart spirit rock")
                || lt_carts.Contains("cart spirit energy")
                || lt_carts.Contains("cart spirit poition"))
            {
                return "spirit base";
            }
            else if(lt_carts[i] == "cart dych" || lt_carts[i] == "cart dych 1" || lt_carts[i] == "cart dych 2")
            {
                return "spawn dych";
            }
            name += lt_carts[i];
                if (i < lt_carts.Count - 1)
                    name += "-";
            }



        return name;
    }

    public static GameObject create_effect(string name, int x, int y, GameObject gm_p, bool bl, int power, List<string> lt_carts, Stack<string> st_par)
    {
        Debug.Log("create effect " + name);

        string name_ = cart_effects[name];

        if (name_ == "atack base")
        {
            if (power <= 1)
            {
                name_ = "atack base";
            }
            else if (power < 4)
            {
                name_ = "atack base 1";
            }
            else
            {
                name_ = "atack base 2";
            }
        }
        else if (name_ == "atack blood")
        {
            if (power <= 1)
            {
                name_ = "atack blood";
            }
            else if (power < 4)
            {
                name_ = "atack blood 1";
            }
            else
            {
                name_ = "atack blood 2";
            }
        }
        else if (name_ == "atack fire")
        {
            if (power <= 1)
            {
                name_ = "atack fire";
            }
            else if (power < 4)
            {
                name_ = "atack fire 1";
            }
            else
            {
                name_ = "atack fire 2";
            }
        }
        else if (name_ == "atack poition")
        {
            if (power <= 1)
            {
                name_ = "atack poition";
            }
            else if (power < 4)
            {
                name_ = "atack poition 1";
            }
            else
            {
                name_ = "atack poition 2";
            }
        }
        else if (name_ == "atack ice")
        {
            if (power <= 1)
            {
                name_ = "atack ice";
            }
            else if (power < 4)
            {
                name_ = "atack ice 1";
            }
            else
            {
                name_ = "atack ice 2";
            }
        }
        else if (name_ == "atack rock")
        {
            if (power <= 1)
            {
                name_ = "atack rock";
            }
            else if (power < 4)
            {
                name_ = "atack rock 1";
            }
            else
            {
                name_ = "atack rock 2";
            }
        }
        else if (name_ == "atack energy" )
        {
            if (power <= 1)
            {
                name_ = "atack energy";
            }
            else if (power < 4)
            {
                name_ = "atack energy 1";
            }
            else
            {
                name_ = "atack energy 2";
            }
        }


        else if (name_ == "blood ef")
        {
            if (power <= 1)
            {
                name_ = "blood ef";
            }
            else if (power< 4)
            {
                name_ = "blood 1 ef";
            }
            else
            {
                name_ = "blood 2 ef";
            }
        }
        else if (name_ == "rock ef")
        {
            if (power <= 1)
            {
                name_ = "rock ef";
            }
            if (power > 1)
            {
                name_ = "rock 1 ef";
            }
        }
        else if (name_ == "fire ef")
        {
            if (power <= 1)
            {
                name_ = "fire ef";
            }
            else if (power > 1)
            {
                name_ = "fire 1 ef";
            }
        }
        //Debug.Log("create effect rez" + name);
        if (power >= 16 && gm_p.name == data.data_now.pl_name_now && name_ != "berserk ef")
            data.give_achive("большой потенциал");
        GameObject effect = Instantiate(GameObject.Find("effects").transform.Find(name_).gameObject);


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
        //Debug.Break();
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
        Debug.Log("create lovyshka ["+name+"]");
        string name_ = name;

        GameObject lovyshka = null;


        if(name!="spirit base" && name != "lov bomb")
        {
            foreach (GameObject gm in lt_lovyshka[x, y])
            {
                if (gm.name == name_)
                {
                    lovyshka = gm;
                    break;
                }
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
    public static void set_anim_color(GameObject gm,List<string> lt_st)
    {
        anim_color cl = gm.GetComponent<anim_color>();
        Queue<par_cart.type_cart> lt_que = new Queue<par_cart.type_cart>();
        par_cart.type_cart typ0 = GameObject.Find(lt_st[0]).GetComponent<par_cart>().type;
        lt_que.Enqueue(typ0);
        lt_que.Enqueue(typ0);
        for (int i = 0; i < lt_st.Count; i++)
        {
            lt_que.Enqueue(GameObject.Find(lt_st[i]).GetComponent<par_cart>().type);
        }
        cl.lt_colors = lt_que;
        cl.init(typ0);
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
        test_message = "wolk mob "+num;
        num = nums_mob_wolk[nums_mob_wolk.Count - 1];
        if (mob_list[num] != null)
        {
            par_mob pr_mb = mob_list[num].GetComponent<par_mob>();
            bool rt = false;

            if (pr_mb.lt_carts_art.Count > 0 && !pr_mb.speed_hod)
            {
                int a = 0;
                List<string> lt_st_art = pr_mb.lt_carts_art.ConvertAll(x=>x.name);
                foreach (par_cart_art art in pr_mb.lt_carts_art)
                {
                    if (art.perez_now > 0 && art.tp_perez== par_cart_art.type_perez.standart)
                    {
                        art.perez_now--;
                        if(lt_st_art.Contains("cart mast"))
                        {
                            art.perez_now -= lt_st_art.Where(x =>( x == "cart mast")).Count();
                        }
                    }
                    if (art.perez_now < 0)
                        art.perez_now = 0;
                    if (art.perez_now == 0 && art.tp_perez == par_cart_art.type_perez.standart)
                    {
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

            if (pr_mob.friend && pr_mob.friend_type==par_mob.type_friend.active)
            {
                GameObject gg = GameObject.Find("canvases").transform.Find("Canvas").Find("im baf").gameObject;
                //gg.GetComponent<SprireRenderer>
                gg.GetComponent<Animator>().SetBool("energy_anim", (pr_mob.speed_now < pr_mob.speed) && (pr_mob.speed > 1));
                gg.GetComponent<Animator>().SetBool("ice_anim", (pr_mob.speed_now <= 0) && (pr_mob.speed <= 0));
            }
            //Debug.Log("speed hod " + pr_mob.speed_now);
            if (pr_mob.speed_now > 0)
            {

                sp = true;

            }
            else if (pr_mob.speed_now <= 0 && pr_mob.speed <= 0)
            {
                Debug.Log("ice skip");
                //sp = true;
                //pr_mob.speed_now--;
                if (pr_mob.speed_now < pr_mob.speed)
                    sp = true;
                //pr_mob.speed_now++;
                GameObject gg = GameObject.Find("im baf");

                skip_hop = true;
                if (pr_mob.friend && pr_mob.friend_type==par_mob.type_friend.active)
                {
                    //gg.GetComponent<SprireRenderer>
                    gg.GetComponent<Animator>().SetBool("ice_anim", true);
                }
                //if(num==0)

            }



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
                        if (bf.type == par_baf.type_baf.seal)
                        {
                            bl_at = false;
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.seal_spetial)
                        {
                            List<string> lt_c = new List<string>(bf.lt_cart);
                            string cart0 = lt_c[0];
                            lt_c.RemoveAt(0);
                            create_effect(get_name_effect(lt_c,null), pr_mob.x, pr_mob.y, mob_list[num], bf.power, lt_c);
                            //del = true;
                        }
                        else if (bf.type == par_baf.type_baf.rage && bf.long_baf != 1)
                        {
                            foreach (par_cart.type_cart tp in par_cart.get_types_tip())
                            {
                                pr_mob.par_lt_sila.Add(tp.ToString());
                                pr_mob.par_lt_speed.Add(tp.ToString());
                            }
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


                            if (bf.type == par_baf.type_baf.seal || bf.type == par_baf.type_baf.seal_spetial || bf.type == par_baf.type_baf.rage
                                )
                            {
                                del = true;
                            }
                            if (!lt_tp_bf.Contains(bf.type))
                            {

                                Destroy(bf.gm_anim);

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

            
            if (mob_list[num].GetComponent<par_mob>().speed_hod)
            {
                mob_list[num].GetComponent<par_mob>().speed_hod = false;
                //test_message = "error wolk mob " + num.ToString() + " 1989";
                //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                //if (num == 0 && nums_mob_wolk.Count == 0)
                //nums_mob_wolk = new List<int>(nums_mob);
            }
            //mob_list[num].GetComponent<par_mob>().par_lt_sila_save = new List<string>(mob_list[num].GetComponent<par_mob>().par_lt_sila);
            //mob_list[num].GetComponent<par_mob>().speed_hod = false;//поменять при необходимости
            hod_mob_now_baf = num;
            hod_mob_now_baf_attack = bl_at;
            hod_mob_now_baf_sp = sp;

            next_hod_baf();

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
                if(num==0)
                    GameObject.Find("canvases").transform.Find("Canvas").Find("bt up").gameObject.GetComponent<Animator>().SetBool("opened", carts[pr_pl.x, pr_pl.y] != null);

                if (mob_list[num].GetComponent<par_mob>().wolked)
                {
                    gm_wolk_friend = mob_list[num];
                    //hod_mob_now_int = num;
                    hod_mob_now_friend = num;
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

                    Debug.Log("start hod friend " + num.ToString() + "  " + nums_mob_wolk.Count);
                    Debug.Log(pr_mob.carts_count_max["cart damage"] + " count damage " + pr_mob.lt_carts_art[0].type);
                    if (pr_mob.friend_type == par_mob.type_friend.active )
                    {
                        GameObject.Find("canvases").transform.Find("Canvas").Find("bt up").gameObject.GetComponent<Animator>().SetBool("opened", carts[pr_pl.x, pr_pl.y] != null&& num == 0);
                        //ui.print_carts_tip(num);
                        ui.print_carts_art(num);
                        ui.update_pn_mob(num);
                        GameObject.Find("Main Camera").transform.parent = mob_list[num].transform;
                        if(!GameObject.Find("canvases").transform.Find("Canvas dialog").gameObject.activeSelf)
                            GameObject.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);

                        //if(num != 0)
                    }

                    if (pr_mob.friend_type == par_mob.type_friend.disactive)
                    {
                        timer_now = 0.1f;
                        next_hod();
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
                    foreach (GameObject gm in pr_mob.lt_mob_vizited)
                    {
                        if ((gm.GetComponent<par_mob>().friend && (distance(pr_mob.x, pr_mob.y, gm.GetComponent<par_mob>().x, gm.GetComponent<par_mob>().y) <= pr_mob.distanse_max - 2)) || gm.GetComponent<par_mob>().bl_wolk)
                            bl_wolk_st = true;
                    }
                }
                data.data_now.cl_carts_speed = 0;
                if (!pr_mob.vizited)
                {
                    //Debug.Log("timer is 0.15 mob");
                    timer_now = 0.1f;
                }

                //Debug.Log(mob_list.Count+"  "+num);
                par_mob player = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                if (player.x > pr_mob.x)
                    pr_mob.simm(1);
                else if (player.x < pr_mob.x)
                    pr_mob.simm(-1);
                bool atack = false;
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
                else if(!pr_mob.vizited ||!bl_at)
                {
                    timer_now = 0.1f;
                    next_hod();
                }
                //next_hod();
            }
            else
            {
                if (pr_mob.lt_wolk.Count != 0)
                {
                    if (col_friend > 1)
                        timer_now = -0.1f;
                    pr_mob.wolk_v2();
                    next_hod();
                }
                else
                {
                    timer_now = 0.1f;
                    next_hod();
                    return;
                }

            }
            Debug.Log("error with sh");
        }
        else
        {
            timer_now = 0.1f;
            next_hod();
            //return;
        }

    }

    #region atack_mob
    public class atack_cart
    {
        public Queue<string> que_cart = new Queue<string>();
        public GameObject cel;
        public int priority=0;

        public atack_cart(Queue<string> que,GameObject cel,int pr)
        {
            que_cart = que;
            priority = pr;
            this.cel = cel;
        }
        public atack_cart(string cart1, GameObject cel,int pr)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue(cart1);
            que_cart = q;
            priority = pr;
            this.cel = cel;
        }
        public atack_cart(string cart1,string cart2, GameObject cel, int pr)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue(cart1);
            q.Enqueue(cart2);
            que_cart = q;
            priority = pr;
            this.cel = cel;
        }
        public atack_cart(string cart1, string cart2,string cart3, GameObject cel, int pr)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue(cart1);
            q.Enqueue(cart2);
            q.Enqueue(cart3);
            que_cart = q;
            priority = pr;
            this.cel = cel;
        }
    }
    public static List<atack_cart> add_list_attack(List<string> lt_st,par_mob mb_now,GameObject cel,int pr)
    {
        List<atack_cart> lt_at = new List<atack_cart>();
        //Debug.Log("proverka atack");
        foreach (string st in lt_st)
        {

            //List<List<string>> lt_lt = par_cart.cart_combinations.lt_comb_map[st];
            List<List<string>> lt_lt = par_cart.cart_combinations.get_comb_now(st);

            foreach (List<string> lt1 in lt_lt)
            {
                //Debug.Log("comb");
                bool bl_eq = true;
                foreach (string lt2 in lt1)
                {
                    //Debug.Log(lt2);
                    if (!mb_now.lt_carts.Contains(lt2))
                    {
                        bl_eq = false;
                        break;
                    }
                }
                if (bl_eq)
                {
                    lt_at.Add(new atack_cart(new Queue<string>(lt1), cel, pr));
                }
            }
        }
        return lt_at;

    }

    public static List<List<atack_cart>> base_proverka_atack(par_mob mb_now)
    {
        Debug.Log("base proverka atack");
        //Debug.Log("cont " + mb_now.contains_pr_at(priority_atack.type_atack.damage));
        Dictionary<string,List<atack_cart>> lt_at_d = new Dictionary<string,List<atack_cart>>();
        List<List<atack_cart>> lt_at = new List<List<atack_cart>>();
        List<GameObject> lt_gm = new List<GameObject>();

        HashSet<priority_atack.type_atack> lt_con_att = new HashSet<priority_atack.type_atack>();
        foreach (priority_atack.type_atack att in System.Enum.GetValues(typeof(priority_atack.type_atack)))
        {
            if (mb_now.contains_pr_at(att))
            {
                lt_con_att.Add(att);
            }
        }


        List<GameObject> lt_g = new List<GameObject>(mb_now.lt_mob_vizited);
        lt_g.Add(mb_now.gameObject);

        Dictionary<priority_atack.type_atack, List<string>> lt_map_att = new Dictionary<priority_atack.type_atack, List<string>>();

        foreach (GameObject gm in lt_g)
        {

            //List<List<atack_cart>> lt_at_now = new List<List<atack_cart>>();
            par_mob mb = gm.GetComponent<par_mob>();
            int v_x = mb.x;
            int v_y = mb.y;
            int pr = -1;




            if (distance(v_x, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
            {
                if (mb.friend != mb_now.friend && !mb.nps)
                {
                    if (lt_con_att.Contains(priority_atack.type_atack.damage) && mb_now.contains_cart_type(par_cart.type_cart.damage))
                    {
                        
                        pr = mb_now.get_pr_at(priority_atack.type_atack.damage);
                        List<string> lt_st = new List<string>();
                        //lt_st.Add("cart damage");
                        
                        lt_st.Add("cart damage[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]");
                        lt_st.Add("cart damage[n]-cart [e][n]");
                        lt_st.Add("cart damage[n]-cart [e_d]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart [e][n]");
                        lt_st.Add("cart derlorg[n]-cart damage[n]");
                        lt_st.Add("cart derlorg[n]-cart bomb[n]-cart damage[n]");
                        lt_st.Add("cart bomb[n]-cart derlorg[n]-cart damage[n]");
                        
                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y],pr));
                        //lt_at.Add(new atack_cart(new Queue<string>(new List<string>() { "cart damage"}), mob[v_x, v_y], pr));
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.rock) && mb_now.contains_cart_type(par_cart.type_cart.rock))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.rock);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart rock[n]");
                        //lt_st.Add("cart rock[n]");
                        lt_st.Add("cart damage[n]-cart rock[n]");
                        lt_st.Add("cart bomb[n]-cart rock[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart rock[n]");
                        lt_st.Add("cart derlorg[n]-cart rock[n]");

                        lt_st.Add("cart spirit rock");
                        lt_st.Add("cart damage[n]-cart spirit rock");
                        lt_st.Add("cart bomb[n]-cart spirit rock");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart spirit rock");
                        lt_st.Add("cart derlorg[n]-cart spirit rock");

                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.fire) && mb_now.contains_cart_type(par_cart.type_cart.fire))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.fire);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart fire[n]");
                        lt_st.Add("cart damage[n]-cart fire[n]");
                        lt_st.Add("cart bomb[n]-cart fire[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart fire[n]");
                        lt_st.Add("cart derlorg[n]-cart fire[n]");

                        lt_st.Add("cart spirit fire");
                        lt_st.Add("cart damage[n]-cart spirit fire");
                        lt_st.Add("cart bomb[n]-cart spirit fire");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart spirit fire");
                        lt_st.Add("cart derlorg[n]-cart spirit fire");

                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.ice) && mb_now.contains_cart_type(par_cart.type_cart.ice))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.ice);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart ice[n]");
                        lt_st.Add("cart damage[n]-cart ice[n]");
                        lt_st.Add("cart bomb[n]-cart ice[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart ice[n]");
                        lt_st.Add("cart derlorg[n]-cart ice[n]");

                        lt_st.Add("cart spirit ice");
                        lt_st.Add("cart damage[n]-cart spirit ice");
                        lt_st.Add("cart bomb[n]-cart spirit ice");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart spirit ice");
                        lt_st.Add("cart derlorg[n]-cart spirit ice");

                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.poition) && mb_now.contains_cart_type(par_cart.type_cart.poition))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.poition);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart poition[n]");
                        lt_st.Add("cart damage[n]-cart poition[n]");
                        lt_st.Add("cart bomb[n]-cart poition[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart poition[n]");
                        lt_st.Add("cart derlorg[n]-cart poition[n]");

                        lt_st.Add("cart spirit poition");
                        lt_st.Add("cart damage[n]-cart spirit poition");
                        lt_st.Add("cart bomb[n]-cart spirit poition");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart spirit poition");
                        lt_st.Add("cart derlorg[n]-cart spirit poition");

                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.blood) && mb_now.contains_cart_type(par_cart.type_cart.blood))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.blood);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart damage[n]-cart blood[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart blood[n]");

                        lt_st.Add("cart damage[n]-cart spirit blood");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart spirit blood");

                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.energy) && mb_now.contains_cart_type(par_cart.type_cart.energy))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.energy);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart damage[n]-cart energy[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart energy[n]");

                        lt_st.Add("cart damage[n]-cart spirit energy");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart spirit energy");

                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    /*
                    if (lt_con_att.Contains(priority_atack.type_atack.bomb) && mb_now.contains_cart_type(par_cart.type_cart.damage))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.bomb);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart bomb[n]-cart damage[n]");
                        lt_st.Add("cart bomb[n]-cart damage[n]-cart [e][n]");

                        lt_st.Add("cart bomb[n]-cart fire[n]");
                        lt_st.Add("cart bomb[n]-cart poition[n]");
                        lt_st.Add("cart bomb[n]-cart ice[n]");
                        lt_st.Add("cart bomb[n]-cart rock[n]");

                        lt_st.Add("cart bomb[n]-cart derlorg[n]");

                        //List<atack_cart> lt_=
                        if (v_x > 0 && nature[v_x - 1, v_y] != null && distance(v_x - 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x-1, v_y], pr));
                        }
                        if (v_x <size_global-1 && nature[v_x + 1, v_y] != null && distance(v_x + 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x + 1, v_y], pr));
                        }
                        if (v_y > 0 && nature[v_x, v_y-1] != null && distance(v_x, v_y-1, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x, v_y-1], pr));
                        }
                        if (v_y <size_global-1 && nature[v_x, v_y +1] != null && distance(v_x, v_y + 1, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x, v_y + 1], pr));
                        }
                        lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x, v_y], pr));
                    }
                    */
                    /*
                    if (mb_now.contains_pr_at(priority_atack.type_atack.damage))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.damage);

                        List<atack_cart> lt_at_now_dm = new List<atack_cart>();
                        List<string> lt_c_dm = new List<string>();
                        lt_c_dm.Add("cart damage");
                        lt_c_dm.Add("cart damage 1");
                        lt_c_dm.Add("cart damage 2");
                        foreach (string st_dm in lt_c_dm)
                        {
                            if (mb_now.lt_carts.Contains(st_dm))
                            {
                                lt_at_now_dm.Add(new atack_cart(st_dm, mob[v_x, v_y], pr));
                                foreach (atack_cart at in lt_at_now)
                                {
                                    Queue<string> q = new Queue<string>();
                                    q.Enqueue(st_dm);
                                    foreach (string st in at.que_cart)
                                    {
                                        q.Enqueue(st);
                                    }
                                    lt_at_now_dm.Add(new atack_cart(q, mob[v_x, v_y], pr));
                                    //lt_gm.Add(eath[v_x, v_y]);

                                }
                            }
                            lt_at_now.AddRange(lt_at_now_dm);
                        }



                    }
                    */
                }
                else if (mb != null && mb.friend == mb_now.friend)
                {
                                        
                    bool bl_bar = false;
                    bool bl_energy = false;
                    bool bl_at_mob = false;
                    foreach (par_baf baf in mb.lt_bafs)
                    {

                        if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_rock)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_blood)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_fire)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_energy)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_poition)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_ice)
                        {
                            bl_bar = true;
                        }

                        else if (baf.type == par_baf.type_baf.energy)
                        {
                            bl_energy = true;
                        }
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.energy) && mb_now.contains_cart_type(par_cart.type_cart.energy))
                    {
                       
                        bl_at_mob = mb.lt_mob_vizited.ConvertAll(x =>
                        {
                            return x.GetComponent<par_mob>();
                        }).Where(m =>
                        {
                            return func.distance(m.x, m.y, mb.x, mb.y) <= mb.distanse_attack;
                        }).ToList().ConvertAll(x =>
                        {
                            return x.friend;
                        }).ToList().Contains(!mb_now.friend);
                        Debug.Log("contains energy "+bl_at_mob);
                    }




                    if (lt_con_att.Contains(priority_atack.type_atack.blood) && mb_now.contains_cart_type(par_cart.type_cart.blood) && mb.hp<mb.hp_max)
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.blood);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart blood[n]");
                        lt_st.Add("cart bomb[n]-cart blood[n]");
                        lt_st.Add("cart derlorg[n]-cart blood[n]");

                        lt_st.Add("cart spirit blood");
                        lt_st.Add("cart bomb[n]-cart spirit blood");
                        lt_st.Add("cart derlorg[n]-cart spirit blood");

                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.energy) && mb_now.contains_cart_type(par_cart.type_cart.energy) && !bl_energy && bl_at_mob )
                    {

                       
                            pr = mb_now.get_pr_at(priority_atack.type_atack.energy);
                            List<string> lt_st = new List<string>();
                            lt_st.Add("cart energy[n]");
                            lt_st.Add("cart bomb[n]-cart energy[n]");
                            lt_st.Add("cart derlorg[n]-cart energy[n]");

                            lt_st.Add("cart spirit energy");
                            lt_st.Add("cart bomb[n]-cart spirit energy");
                            lt_st.Add("cart derlorg[n]-cart spirit energy");

                            lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                        

                    }
                    if (lt_con_att.Contains(priority_atack.type_atack.barier) && mb_now.contains_cart_type(par_cart.type_cart.sp_barier) && !bl_bar)
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.barier);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart barier[n]");
                        lt_st.Add("cart barier[n]-cart [e][n]");
                        lt_st.Add("cart bomb[n]-cart barier[n]-cart [e][n]");
                        lt_st.Add("cart derlorg[n]-cart barier[n]");
                        lt_st.Add("cart derlorg[n]-cart barier[n]-cart [e][n]");

                        lt_st.Add("cart barier[n]-cart [e_d]");
                        lt_st.Add("cart bomb[n]-cart barier[n]-cart [e_d]");
                        lt_st.Add("cart derlorg[n]-cart barier[n]-cart [e_d]");
                        lt_at.Add(add_list_attack(lt_st, mb_now, mob[v_x, v_y], pr));
                    }
                    /*
                    if (lt_con_att.Contains(priority_atack.type_atack.bomb))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.bomb);
                        List<string> lt_st = new List<string>();
                        lt_st.Add("cart bomb[n]-cart barier[n]");
                        lt_st.Add("cart bomb[n]-cart barier[n]-cart [e][n]");

                        if(bl_energy && bl_at_mob)
                            lt_st.Add("cart bomb[n]-cart energy[n]");
                        if (mb.hp>mb.hp_max)
                            lt_st.Add("cart bomb[n]-cart blood[n]");

                        //lt_st.Add("cart bomb[n]-cart derlorg[n]");
                        if (bl_bar)
                        {
                            lt_st.Add("cart derlorg[n]-cart bomb[n]-cart barier[n]");
                            lt_st.Add("cart bomb[n]-cart derlorg[n]-cart barier[n]");
                        }
                        
                        //List<atack_cart> lt_=
                        if (v_x > 0 && nature[v_x - 1, v_y] != null && distance(v_x - 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x - 1, v_y], pr));
                        }
                        if (v_x < size_global - 1 && nature[v_x + 1, v_y] != null && distance(v_x + 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x + 1, v_y], pr));
                        }
                        if (v_y > 0 && nature[v_x, v_y - 1] != null && distance(v_x, v_y - 1, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x, v_y - 1], pr));
                        }
                        if (v_y < size_global - 1 && nature[v_x, v_y + 1] != null && distance(v_x, v_y + 1, mb_now.x, mb_now.y) <= mb_now.distanse_attack)
                        {
                            lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x, v_y + 1], pr));
                        }
                        lt_at.AddRange(add_list_attack(lt_st, mb_now, eath[v_x, v_y], pr));
                    }
                    */
                    /*
                    bool bl_bar = false;
                    bool bl_energy = false;
                    foreach (par_baf baf in mb.lt_bafs)
                    {

                        if (baf.type == par_baf.type_baf.bar)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_rock)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_blood)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_fire)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_energy)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_poition)
                        {
                            bl_bar = true;
                        }
                        else if (baf.type == par_baf.type_baf.bar_ice)
                        {
                            bl_bar = true;
                        }

                        else if (baf.type == par_baf.type_baf.energy)
                        {
                            bl_energy = true;
                        }
                    }

                    if (mb_now.contains_pr_at(priority_atack.type_atack.blood) && mb.hp < mb.hp_max)
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.blood);
                        if (mb_now.lt_carts.Contains("cart blood"))
                            lt_at_now.Add(new atack_cart("cart blood", mob[v_x, v_y], pr));
                        if (mb_now.lt_carts.Contains("cart blood 1"))
                            lt_at_now.Add(new atack_cart("cart blood 1", mob[v_x, v_y], pr));
                        if (mb_now.lt_carts.Contains("cart blood 2"))
                            lt_at_now.Add(new atack_cart("cart blood 2", mob[v_x, v_y], pr));
                        if (mb_now.lt_carts.Contains("cart spirit blood"))
                            lt_at_now.Add(new atack_cart("cart spirit blood", mob[v_x, v_y], pr));
                    }
                    if (mb_now.contains_pr_at(priority_atack.type_atack.energy) && !bl_energy)
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.energy);
                        if (mb_now.lt_carts.Contains("cart energy"))
                            lt_at_now.Add(new atack_cart("cart energy", mob[v_x, v_y], pr));
                        if (mb_now.lt_carts.Contains("cart energy 1"))
                            lt_at_now.Add(new atack_cart("cart energy 1", mob[v_x, v_y], pr));
                        if (mb_now.lt_carts.Contains("cart energy 2"))
                            lt_at_now.Add(new atack_cart("cart energy 2", mob[v_x, v_y], pr));
                        if (mb_now.lt_carts.Contains("cart spirit energy"))
                            lt_at_now.Add(new atack_cart("cart spirit energy", mob[v_x, v_y], pr));
                    }

                    if (mb_now.contains_pr_at(priority_atack.type_atack.barier))
                    {
                        pr = mb_now.get_pr_at(priority_atack.type_atack.barier);

                        List<atack_cart> lt_at_now_dm = new List<atack_cart>();
                        List<string> lt_c_dm = new List<string>();
                        lt_c_dm.Add("cart barier");
                        lt_c_dm.Add("cart barier 1");
                        lt_c_dm.Add("cart barier 2");
                        foreach (string st_dm in lt_c_dm)
                        {
                            if (mb_now.lt_carts.Contains(st_dm))
                            {
                                lt_at_now_dm.Add(new atack_cart(st_dm, mob[v_x, v_y], pr));
                                foreach (atack_cart at in lt_at_now)
                                {
                                    Queue<string> q = new Queue<string>();
                                    q.Enqueue(st_dm);
                                    foreach (string st in at.que_cart)
                                    {
                                        q.Enqueue(st);
                                    }
                                    lt_at_now_dm.Add(new atack_cart(q, mob[v_x, v_y], pr));
                                    //lt_gm.Add(eath[v_x, v_y]);

                                }
                            }
                            lt_at_now.AddRange(lt_at_now_dm);
                        }




                    }
                    */
                }
                /*
                if (mb_now.contains_pr_at(priority_atack.type_atack.crystall))
                {
                    pr = mb_now.get_pr_at(priority_atack.type_atack.crystall);
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
                                    lt_at_now_cr.Add(new atack_cart(q, eath[v_x, v_y], pr));
                                    lt_gm.Add(eath[v_x, v_y]);
                                }
                                if (v_x > 0 && nature[v_x - 1, v_y] != null && !lt_gm.Contains(eath[v_x - 1, v_y]) && distance(v_x - 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                {
                                    lt_at_now_cr.Add(new atack_cart(q, eath[v_x - 1, v_y], pr));
                                    lt_gm.Add(eath[v_x - 1, v_y]);
                                }
                                if (v_x < size_global - 1 && nature[v_x + 1, v_y] != null && !lt_gm.Contains(eath[v_x + 1, v_y]) && distance(v_x + 1, v_y, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                {
                                    lt_at_now_cr.Add(new atack_cart(q, eath[v_x + 1, v_y], pr));
                                    lt_gm.Add(eath[v_x + 1, v_y]);
                                }
                                if (v_y > 0 && nature[v_x, v_y - 1] != null && !lt_gm.Contains(eath[v_x, v_y - 1]) && distance(v_x, v_y - 1, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                {
                                    lt_at_now_cr.Add(new atack_cart(q, eath[v_x, v_y - 1], pr));
                                    lt_gm.Add(eath[v_x, v_y - 1]);
                                }
                                if (v_y < size_global - 1 && nature[v_x, v_y + 1] != null && !lt_gm.Contains(eath[v_x, v_y + 1]) && distance(v_x, v_y + 1, mb_now.x, mb_now.y) <= mb_now.distanse_min)
                                {
                                    lt_at_now_cr.Add(new atack_cart(q, eath[v_x, v_y + 1], pr));
                                    lt_gm.Add(eath[v_x, v_y + 1]);
                                }
                            }
                        }
                    }
                    lt_at_now.AddRange(lt_at_now_cr);
                }



*/

            }

            if (mb.friend != mb_now.friend && !mb.nps &&  mb_now.contains_cart_type(par_cart.type_cart.sp_dych))
            {


                Dictionary<List<string>, int> lt_st = new Dictionary<List<string>, int>();
                //lt_st.Add("cart damage");

                //lt_st.Add("cart damage[n]");
                if (lt_con_att.Contains(priority_atack.type_atack.dych))
                {


                    pr = mb_now.get_pr_at(priority_atack.type_atack.dych);

                    List<string> l = new List<string>();
                    l.Add("cart dych[n]-cart damage[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart [e][n]");
                    l.Add("cart dych[n]-cart fire[n]");
                    l.Add("cart dych[n]-cart poition[n]");
                    l.Add("cart dych[n]-cart ice[n]");
                    l.Add("cart dych[n]-cart rock[n]");

                    l.Add("cart dych[n]-cart derlorg[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]-cart [e][n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart fire[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart poition[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart ice[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart rock[n]");



                    l.Add("cart dych[n]-cart damage[n]-cart [e_d]");
                    l.Add("cart dych[n]-cart spirit fire");
                    l.Add("cart dych[n]-cart spirit poition");
                    l.Add("cart dych[n]-cart spirit ice");
                    l.Add("cart dych[n]-cart spirit rock");

                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]-cart [e_d]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit fire");
                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit poition");
                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit ice");
                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit rock");
                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart [e][n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart fire[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart poition[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart ice[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart rock[n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart fire[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart poition[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart ice[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart rock[n]");
                    */
                    lt_st.Add(l, pr);
                    /*
                    lt_st.Add("cart dych[n]-cart bomb[n]-cart damage[n]", pr);
                    lt_st.Add("cart dych[n]-cart bomb[n]-cart damage[n]-cart [e][n]", pr);
                    lt_st.Add("cart dych[n]-cart bomb[n]-cart fire[n]", pr);
                    lt_st.Add("cart dych[n]-cart bomb[n]-cart poition[n]", pr);
                    lt_st.Add("cart dych[n]-cart bomb[n]-cart ice[n]", pr);
                    lt_st.Add("cart dych[n]-cart bomb[n]-cart rock[n]", pr);
                    */
                    //lt_st.Add("cart dych[n]-cart derlorg[n]", pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.damage) && mb_now.contains_cart_type(par_cart.type_cart.damage))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.damage);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart damage[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart [e_d]");

                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]-cart [e_d]");

                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart [e][n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart damage[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.rock) && mb_now.contains_cart_type(par_cart.type_cart.rock))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.rock);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart rock[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart rock[n]");

                    l.Add("cart dych[n]-cart derlorg[n]-cart rock[n]");


                    l.Add("cart dych[n]-cart spirit rock");
                    l.Add("cart dych[n]-cart damage[n]-cart spirit rock");

                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit rock");
                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart rock[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart rock[n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart rock[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.fire) && mb_now.contains_cart_type(par_cart.type_cart.fire))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.fire);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart fire[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart fire[n]");

                    l.Add("cart dych[n]-cart derlorg[n]-cart fire[n]");


                    l.Add("cart dych[n]-cart spirit fire");
                    l.Add("cart dych[n]-cart damage[n]-cart spirit fire");

                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit fire");
                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart fire[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart fire[n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart fire[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.poition) && mb_now.contains_cart_type(par_cart.type_cart.poition))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.poition);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart poition[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart poition[n]");

                    l.Add("cart dych[n]-cart derlorg[n]-cart poition[n]");


                    l.Add("cart dych[n]-cart spirit poition");
                    l.Add("cart dych[n]-cart damage[n]-cart spirit poition");

                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit poition");
                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart poition[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart poition[n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart poition[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.ice) && mb_now.contains_cart_type(par_cart.type_cart.ice))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.ice);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart ice[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart ice[n]");

                    l.Add("cart dych[n]-cart derlorg[n]-cart ice[n]");


                    l.Add("cart dych[n]-cart spirit ice");
                    l.Add("cart dych[n]-cart damage[n]-cart spirit ice");

                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit ice");
                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart ice[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart ice[n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart ice[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.blood) && mb_now.contains_cart_type(par_cart.type_cart.blood))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.blood);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    //l.Add("cart dych[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]-cart blood[n]");

                    l.Add("cart dych[n]-cart damage[n]-cart spirit blood");
                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]-cart spirit blood");
                    //l.Add("cart dych[n]-cart derlorg[n]-cart blood[n]");

                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart blood[n]");
                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart blood[n]");

                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart blood[n]");
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.energy) && mb_now.contains_cart_type(par_cart.type_cart.energy))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.energy);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    //l.Add("cart dych[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart damage[n]-cart energy[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]-cart energy[n]");

                    l.Add("cart dych[n]-cart damage[n]-cart spirit energy");
                    l.Add("cart dych[n]-cart derlorg[n]-cart damage[n]-cart spirit energy");

                    //l.Add("cart dych[n]-cart derlorg[n]-cart blood[n]");

                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart blood[n]");
                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart energy[n]");

                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart blood[n]");
                    lt_st.Add(l, pr);
                }

                List<Vector2> lt_v2 = new List<Vector2>(mb_now.lt_v2_vizited);
                lt_v2 = mb_now.lt_v2_vizited.Where(x =>
                {
                    return (func.distance((int)x.x, (int)x.y, v_x, v_y) <= 2) && (func.distance((int)x.x, (int)x.y, mb_now.x, mb_now.y) <= mb_now.distanse_attack) && func.object_bool((int)x.x, (int)x.y);
                }).ToList();
                //lt_v2.Remove(new Vector2(v_x, v_y));
                foreach (Vector2 v in lt_v2)
                {
                    foreach (List<string> l_s in lt_st.Keys)
                    {
                        lt_at.Add(add_list_attack(l_s, mb_now, eath[(int)v.x, (int)v.y], lt_st[l_s]));
                    }




                    //lt_at.Add(new atack_cart(new Queue<string>(new List<string>() { "cart damage"}), mob[v_x, v_y], pr));
                }
            }

            else if(mb.friend == mb_now.friend && mb_now.contains_cart_type(par_cart.type_cart.sp_dych))
            {
                Dictionary<List<string>, int> lt_st = new Dictionary<List<string>, int>();

                if (lt_con_att.Contains(priority_atack.type_atack.dych))
                {
                    List<string> l = new List<string>();
                    l.Add("cart dych[n]-cart barier[n]");
                    l.Add("cart dych[n]-cart barier[n]-cart [e][n]");
                    l.Add("cart dych[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart energy[n]");

                    l.Add("cart dych[n]-cart derlorg[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart energy[n]");


                    l.Add("cart dych[n]-cart barier[n]-cart [e_d]");
                    l.Add("cart dych[n]-cart spirit blood");
                    l.Add("cart dych[n]-cart spirit energy");

                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit blood");
                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit energy");
                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart [e][n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart blood[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart energy[n]");


                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart barier[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart barier[n]-cart [e][n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart blood[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart energy[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.barier) && mb_now.contains_cart_type(par_cart.type_cart.sp_barier))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.barier);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart barier[n]");
                    l.Add("cart dych[n]-cart barier[n]-cart [e][n]");
                    l.Add("cart dych[n]-cart barier[n]-cart [e_d]");

                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart [e][n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart [e_d]");

                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart [e][n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart barier[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.rock) && mb_now.contains_cart_type(par_cart.type_cart.rock))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.rock);
                    l.Add("cart dych[n]-cart barier[n]-cart rock[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart rock[n]");

                    l.Add("cart dych[n]-cart barier[n]-cart spirit rock");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart spirit rock");
                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart rock[n]");
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.fire) && mb_now.contains_cart_type(par_cart.type_cart.fire))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.fire);
                    l.Add("cart dych[n]-cart barier[n]-cart fire[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart fire[n]");

                    l.Add("cart dych[n]-cart barier[n]-cart spirit fire");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart spirit fire");
                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart fire[n]");
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.ice) && mb_now.contains_cart_type(par_cart.type_cart.ice))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.ice);
                    l.Add("cart dych[n]-cart barier[n]-cart ice[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart ice[n]");

                    l.Add("cart dych[n]-cart barier[n]-cart spirit ice");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart spirit ice");
                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart ice[n]");
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.poition) && mb_now.contains_cart_type(par_cart.type_cart.poition))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.poition);
                    l.Add("cart dych[n]-cart barier[n]-cart poition[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart poition[n]");

                    l.Add("cart dych[n]-cart barier[n]-cart spirit poition");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart spirit poition");
                    //l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart rock[n]");
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.blood) && mb_now.contains_cart_type(par_cart.type_cart.blood))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.blood);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart barier[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart blood[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart blood[n]");

                    l.Add("cart dych[n]-cart spirit blood");
                    l.Add("cart dych[n]-cart barier[n]-cart spirit blood");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart spirit blood");
                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit blood");
                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart blood[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart blood[n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart blood[n]");
                    */
                    lt_st.Add(l, pr);
                }
                if (lt_con_att.Contains(priority_atack.type_atack.energy) && mb_now.contains_cart_type(par_cart.type_cart.energy))
                {
                    List<string> l = new List<string>();
                    pr = mb_now.get_pr_at(priority_atack.type_atack.energy);
                    //lt_st.Add("cart dych[n]-cart damage[n]", pr);
                    l.Add("cart dych[n]-cart energy[n]");
                    l.Add("cart dych[n]-cart barier[n]-cart energy[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart energy[n]");
                    l.Add("cart dych[n]-cart derlorg[n]-cart energy[n]");

                    l.Add("cart dych[n]-cart spirit energy");
                    l.Add("cart dych[n]-cart barier[n]-cart spirit energy");
                    l.Add("cart dych[n]-cart derlorg[n]-cart barier[n]-cart spirit energy");
                    l.Add("cart dych[n]-cart derlorg[n]-cart spirit energy");

                    /*
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart energy[n]");
                    l.Add("cart bomb[n]-" + "cart dych[n]-cart barier[n]-cart energy[n]");

                    l.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart energy[n]");
                    */
                    lt_st.Add(l, pr);
                }

                List<Vector2> lt_v2 = new List<Vector2>(mb_now.lt_v2_vizited);
                lt_v2 = mb_now.lt_v2_vizited.Where(x =>
                {
                    return (func.distance((int)x.x, (int)x.y, v_x, v_y) <= 2) && (func.distance((int)x.x, (int)x.y, mb_now.x, mb_now.y) <= mb_now.distanse_attack) && func.object_bool((int)x.x, (int)x.y);
                }).ToList();
                //lt_v2.Remove(new Vector2(v_x, v_y));
                foreach (Vector2 v in lt_v2)
                {
                    foreach (List<string> l_s in lt_st.Keys)
                    {
                        lt_at.Add(add_list_attack(l_s, mb_now, eath[(int)v.x, (int)v.y], lt_st[l_s]));
                    }




                    //lt_at.Add(new atack_cart(new Queue<string>(new List<string>() { "cart damage"}), mob[v_x, v_y], pr));
                }
            }





            if (lt_con_att.Contains(priority_atack.type_atack.bomb) && mb_now.contains_cart_type(par_cart.type_cart.sp_bomb) && mb.friend != mb_now.friend && !mb.nps)
            {

                pr = mb_now.get_pr_at(priority_atack.type_atack.dych);
                List<string> lt_st = new List<string>();
                //lt_st.Add("cart damage");

                //lt_st.Add("cart damage[n]");
                lt_st.Add("cart bomb[n]-cart damage[n]");
                lt_st.Add("cart bomb[n]-cart damage[n]-cart [e][n]");

                lt_st.Add("cart bomb[n]-cart fire[n]");
                lt_st.Add("cart bomb[n]-cart poition[n]");
                lt_st.Add("cart bomb[n]-cart ice[n]");
                lt_st.Add("cart bomb[n]-cart rock[n]");

                lt_st.Add("cart bomb[n]-cart derlorg[n]");
                lt_st.Add("cart bomb[n]-cart derlorg[n]-cart damage[n]");
                lt_st.Add("cart bomb[n]-cart derlorg-cart damage[n]-cart [e][n]");
                //lt_st.Add("cart bomb[n]-cart derlorg[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart fire[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart poition[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart ice[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart rock[n]");


                lt_st.Add("cart bomb[n]-cart damage[n]-cart [e_d]");

                lt_st.Add("cart bomb[n]-cart spirit fire");
                lt_st.Add("cart bomb[n]-cart spirit poition");
                lt_st.Add("cart bomb[n]-cart spirit ice");
                lt_st.Add("cart bomb[n]-cart spirit rock");

                lt_st.Add("cart bomb[n]-cart derlorg[n]");
                lt_st.Add("cart bomb[n]-cart derlorg[n]-cart damage[n]");
                lt_st.Add("cart bomb[n]-cart derlorg-cart damage[n]-cart [e_d]");

                lt_st.Add("cart bomb[n]-cart derlorg[n]-cart spirit fire");
                lt_st.Add("cart bomb[n]-cart derlorg[n]-cart spirit poition");
                lt_st.Add("cart bomb[n]-cart derlorg[n]-cart spirit ice");
                lt_st.Add("cart bomb[n]-cart derlorg[n]-cart spirit rock");
                //lt_st.Add("cart bomb[n]-cart derlorg[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart fire[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart poition[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart ice[n]");
                lt_st.Add("cart bomb[n]-" + "cart derlorg[n]-cart rock[n]");
                if (false &&mb_now.contains_cart_type(par_cart.type_cart.sp_dych))
                {
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart damage[n]-cart [e][n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart fire[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart poition[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart ice[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart rock[n]");

                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart fire[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart poition[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart ice[n]");
                    lt_st.Add("cart bomb[n]-" + "cart dych[n]-cart derlorg[n]-cart rock[n]");
                }


                List<Vector2> lt_v2 = new List<Vector2>(mb_now.lt_v2_vizited);
                lt_v2 = mb_now.lt_v2_vizited.Where(x =>
                {
                    return (func.distance((int)x.x, (int)x.y, v_x, v_y) <= 1) && (func.distance((int)x.x, (int)x.y, mb_now.x, mb_now.y) <= mb_now.distanse_attack && nature[(int)x.x, (int)x.y]==null) ;
                }).ToList();
                //lt_v2.Remove(new Vector2(v_x, v_y));
                foreach (Vector2 v in lt_v2)
                {
                    lt_at.Add(add_list_attack(lt_st, mb_now, eath[(int)v.x, (int)v.y], pr));
                }

                //lt_at.Add(new atack_cart(new Queue<string>(new List<string>() { "cart damage"}), mob[v_x, v_y], pr));
            }
            //lt_at.AddRange(lt_at_now);
        }
        Debug.Log("final(0) " + lt_at.Count);
        
        if (lt_at.Count > 0)
        {
            lt_at.RemoveAll(x =>
            {
                return x.Count == 0;
            });
            List<atack_cart> l_at = new List<atack_cart>(lt_at.SelectMany(l => l).ToList());
            if (l_at.Count > 0)
            {
                int mx = l_at.Max(x =>
                {
                    return x.priority;
                });

                foreach (List<atack_cart> l_a in lt_at)
                {
                    l_a.RemoveAll(x =>
                    {
                        return (x.priority != mx) || (x.que_cart.Count == 0);
                    });
                }

                Debug.Log("final(1) " + lt_at.Count);

                var a = l_at.GroupBy(x => x.que_cart.ToList()[0]);


                List<List<atack_cart>> lt_at_ = new List<List<atack_cart>>();
                foreach (var item in a)
                {
                    List<atack_cart> l = new List<atack_cart>();
                    foreach (var item1 in item)
                    {
                        l.Add(new atack_cart(item1.que_cart, item1.cel, item1.priority));
                    }
                    lt_at_.Add(l);
                }
                lt_at = lt_at_;
            }

            

            /*
            List<atack_cart> lt_rem = new List<atack_cart>();
            for (int j = 0; j < lt_at.Count; j++)
            {
                for (int i = j + 1; i < lt_at.Count; i++)
                {
                    for (int j1 = 0; j1 < lt_at[j].Count; j1++)
                    {
                        for (int i1 = j1 + 1; i1 < lt_at[i].Count; i1++)
                        {

                            if (lt_at[i][i1].que_cart.ToList().SequenceEqual(lt_at[j][j1].que_cart.ToList()) && lt_at[j][j1].cel == lt_at[i][i1].cel)
                            {
                                Debug.Log("final(2) " + i + " " + j + " " + lt_rem.Count);
                                lt_rem.Add(lt_at[i]);
                            }

                        }
                    }


                }
            }
            foreach (atack_cart a_c in lt_rem)
            {
                lt_at.Remove(a_c);
            }
            */
        }

        Debug.Log("final " + lt_at.Count);

        return lt_at;
    }

    public static bool atack_mob_auto(int num)
    {
        Debug.Log("atack mob auto " + num);
        bool atact = false;
        par_mob pr_mob = mob_list[num].GetComponent<par_mob>();
        par_mob player = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();

        Queue<string> st_carts = new Queue<string>();

        GameObject cel = null;


        List<List<atack_cart>> lt_rd_atack = new List<List<atack_cart>>();
        if (mob_list[num].name == "dych")
        {


            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));

        }
        if (mob_list[num].name == "defender necro" || mob_list[num].name == "defender bomb" || mob_list[num].name == "defender barier")
        {


            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));

        }
        if (mob_list[num].name == "goblin hiller")
        {


            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));

        }
        if (mob_list[num].name == "goblin" || mob_list[num].name == "defender")
        {
            //lt_rd_atack.Add(new atack_cart("cart damage",player.gameObject,1));
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "goblin elit" || mob_list[num].name == "defender 1" || mob_list[num].name == "defender elemental")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }

        List<string> lt_el_mob = new List<string>();
        lt_el_mob.Add("org");
        lt_el_mob.Add("shaman");

        /*
        lt_el_mob.Add("half-fire spirit");
        lt_el_mob.Add("half-poition spirit");
        lt_el_mob.Add("half-energy spirit");
        lt_el_mob.Add("half-ice spirit");
        lt_el_mob.Add("half-blood spirit");
        lt_el_mob.Add("half-rock spirit");
        */
        lt_el_mob.Add("half-blood spirit");

        lt_el_mob.Add("fire defender 1");
        lt_el_mob.Add("poition defender 1");
        lt_el_mob.Add("energy defender 1");
        lt_el_mob.Add("ice defender 1");
        lt_el_mob.Add("blood defender 1");
        lt_el_mob.Add("rock defender 1");

        lt_el_mob.Add("fire master");
        lt_el_mob.Add("poition master");
        lt_el_mob.Add("energy master");
        lt_el_mob.Add("ice master");
        lt_el_mob.Add("blood master");
        lt_el_mob.Add("rock master");

        lt_el_mob.AddRange(get_bosses());
        lt_el_mob.Remove("org berserk");
        lt_el_mob.Remove("goblin shaman");

        if (lt_el_mob.Contains(mob_list[num].name))
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }

        if (mob_list[num].name == "fire elemental")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart fire", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.fire)), new atack_cart("cart spirit fire", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.fire)) });
                //lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart spirit fire", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.fire)) });
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "poition elemental")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart poition", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.poition)), new atack_cart("cart spirit poition", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.poition)) });
                //lt_rd_atack.Add(new atack_cart("cart spirit poition", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.poition)));
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "rock elemental")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart rock", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.rock)) , new atack_cart("cart spirit rock", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.rock)) });
                //lt_rd_atack.Add(new atack_cart("cart spirit rock", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.rock)));
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "ice elemental")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart ice", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.ice))  , new atack_cart("cart spirit ice", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.ice)) });
                //lt_rd_atack.Add(new atack_cart("cart spirit ice", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.ice)));
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "energy elemental")
        {
            bool bl = false;
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
            foreach (List<atack_cart> at_lt in lt_rd_atack)
            {
                foreach (atack_cart at in at_lt)
                {
                    if (at.cel == mob_list[num] && at.que_cart.Count == 1 && (at.que_cart.Peek() == "cart energy" || at.que_cart.Peek() == "cart spirit energy"))
                    {
                        bl = true;
                        break;
                    }

                }
                if (bl)
                    break;
            }

            if (pr_mob.hp < pr_mob.hp_max && !bl)
            {
                lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart energy", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.energy)) , new atack_cart("cart spirit energy", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.energy)) });
                //lt_rd_atack.Add(new atack_cart("cart spirit energy", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.energy)));
            }

            //lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }


        if (mob_list[num].name == "half-fire spirit")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                foreach (par_cart_art art in pr_mob.lt_carts_art)
                {
                    if (art.name == "cart fire heart" && art.q > 0)
                        lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart fire", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.fire)) });
                }
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "half-poition spirit")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                foreach (par_cart_art art in pr_mob.lt_carts_art)
                {
                    if (art.name == "cart poition heart" && art.q > 0)
                        lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart poition", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.poition)) });
                }
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "half-ice spirit")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                foreach (par_cart_art art in pr_mob.lt_carts_art)
                {
                    if (art.name == "cart ice heart" && art.q > 0)
                        lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart ice", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.ice)) });
                }
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "half-rock spirit")
        {
            if (pr_mob.hp < pr_mob.hp_max)
            {
                foreach (par_cart_art art in pr_mob.lt_carts_art)
                {
                    if (art.name == "cart rock heart" && art.q > 0)
                        lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart rock", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.rock)) });
                }
            }

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "half-energy spirit")
        {
            bool bl = false;
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
            foreach (List<atack_cart> at_lt in lt_rd_atack)
            {
                foreach (atack_cart at in at_lt)
                {
                    if (at.cel == mob_list[num] && at.que_cart.Count == 1 && (at.que_cart.Peek() == "cart energy"))
                    {
                        bl = true;
                        break;
                    }

                }
                if (bl)
                    break;
            }


            if (pr_mob.hp < pr_mob.hp_max && !bl)
            {
                foreach (par_cart_art art in pr_mob.lt_carts_art)
                {
                    if (art.name == "cart energy heart" && art.q > 0)
                        lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart energy", mob_list[num], pr_mob.get_pr_at(priority_atack.type_atack.energy)) });
                }
            }

            
        }

        if (mob_list[num].name == "org berserk")
        {


            List<GameObject> lt = pr_mob.lt_mob_vizited;
            lt.Add(mob_list[num]);
            //List<object> lt_now = new List<object>();
            List<GameObject> lt_now_rage = new List<GameObject>();

            bool bl_cent = true;

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
            //Debug.Log("true dist -2 " + pr_mob.lt_carts_art.Count);


            
            if (lt.Count > 0)
            {
                foreach (GameObject obj in lt)
                {


                    par_mob mb = ((GameObject)obj).GetComponent<par_mob>();

                    bool bl_rage = false;
                    foreach (par_baf baf in mb.lt_bafs)
                    {
                        

                        if (baf.type == par_baf.type_baf.rage || baf.long_baf<=2)
                        {
                            bl_rage = true;
                        }
                    }

                    if (distance(mb.x, mb.y, pr_mob.x, pr_mob.y) <= pr_mob.distanse_attack && !bl_rage)
                    {
                        if ((mb.friend== pr_mob.friend)&& (pr_mob.lt_carts.Contains("cart rage(boss)")))
                            lt_now_rage.Add(obj);
                    }

                }

            }

            List<int> lt_rd = new List<int>() {0 };                
            if (lt_now_rage.Count == 0)
                lt_rd.Remove(0);
            int rd = -1;
            if (lt_rd.Count > 0)
                rd = lt_rd[Random.Range(0, lt_rd.Count)];

            if (rd == 0)
            {
                //type = "cart damage";
                //st_carts.Enqueue(type_1);
                //st_carts.Enqueue("cart rage(boss)");
                //cel = rand_list<GameObject>(lt_now_rage);
                foreach (GameObject g in lt_now_rage)
                {
                    lt_rd_atack.Add(new List<atack_cart> { new atack_cart("cart rage(boss)", g, pr_mob.get_pr_at(priority_atack.type_atack.sp_boss)) });
                }
                
                atact = true;
                //next_hod();
            }
            
        }


        if (mob_list[num].name == "goblin shaman")
        {

            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));

            if (pr_mob.lt_carts.Contains("cart spirit(boss)"))
            {
                List<atack_cart> lt_at_sp = new List<atack_cart>();

                foreach (List<atack_cart> at_lt in lt_rd_atack)
                {
                    foreach (atack_cart a in at_lt)
                    {
                        Queue<string> q_now = new Queue<string>();
                        bool b = true;
                        foreach (string st in a.que_cart)
                        {
                            par_cart c = GameObject.Find(st).GetComponent<par_cart>();

                            if (b)
                            {
                                q_now.Enqueue("cart spirit(boss)");
                                b = false;
                            }
                            q_now.Enqueue(st);
                        }
                        if(q_now.Count>0)
                            lt_at_sp.Add(new atack_cart(q_now, a.cel, pr_mob.get_pr_at(priority_atack.type_atack.sp_boss)));

                    }
                }
                /*
                foreach (atack_cart a in lt_rd_atack)
                {
                    Queue<string> q_now = new Queue<string>();
                    bool b = true;
                    foreach (string st in a.que_cart)
                    {
                        par_cart c = GameObject.Find(st).GetComponent<par_cart>();

                        if (b)
                        {
                            q_now.Enqueue("cart spirit(boss)");
                            b = false;
                        }
                        q_now.Enqueue(st);
                    }
                    lt_at_sp.Add(new atack_cart(q_now, a.cel, pr_mob.get_pr_at(priority_atack.type_atack.sp_boss)));
                }
                */
                if(lt_at_sp.Count>0)
                    lt_rd_atack.Add(lt_at_sp);
            }

        }

        
        if (mob_list[num].name == "goblin blood elit" || mob_list[num].name == "defender blood")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "goblin energy elit" || mob_list[num].name == "defender energy")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "goblin ice elit" || mob_list[num].name == "defender ice")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "goblin poition elit" || mob_list[num].name == "defender poition")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "goblin fire elit" || mob_list[num].name == "defender fire")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "goblin rock elit" || mob_list[num].name == "defender rock")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg shaman")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg energy")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg ice")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg fire")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg poition")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg blood")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg rock")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg bomb")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "derlorg necro")
        {
            lt_rd_atack.AddRange(base_proverka_atack(pr_mob));
        }
        if (mob_list[num].name == "dungalorg")
        {
            List<object> lt = way_find_lt(mob, pr_mob.x, pr_mob.y, pr_mob.distanse_attack);
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




        //Debug.Log("atack carts 0");
        if (lt_rd_atack.Count>0)
        {
            atact = true;
            List<atack_cart> lt1 = rand_list(lt_rd_atack);
            atack_cart at_cart_now = rand_list(lt1);
            //Debug.Log(lt1.Count);
            cel = at_cart_now.cel;
            Debug.Log("atack carts");
            foreach (string st in at_cart_now.que_cart)
            {
                Debug.Log(st);
            }
            //Debug.Log("end atack carts");
            string type = at_cart_now.que_cart.Dequeue();
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
            //Debug.Log("sila " + type + "   =" + sila);
            lt_rez.Remove(type);
            
            if (sila > 0)
            {
                int rr = Random.Range(0, sila + 1);
                int r_sila = -1;
                for (int i = 0; i < rr; i++)
                {
                    if (lt_rez.Contains(type))
                    {
                        if (!bl_add)
                        {
                            bl_add = true;
                            pr_mob.lt_carts_added_now.Add(type);
                        }
                        r_sila++;
                        pr_mob.lt_carts_added.Add(type);
                        lt_rez.Remove(type);
                    }
                    else
                        break;
                }
                for (int i = 0; i < r_sila; i++)
                {
                    pr_mob.rem_sila(type);
                }

            }

            pr_mob.lt_carts_active_now.Add(type);

            if (at_cart_now.que_cart.Count > 0)
            {
                if (!bl_add)
                {
                    bl_add = true;
                    pr_mob.lt_carts_added_now.Add(type);
                }
                foreach (string st in at_cart_now.que_cart)
                {
                    pr_mob.lt_carts_added.Add(st);
                    sila = pr_mob.max_sila(st);
                    //Debug.Log("sila " + st + "   =" + sila);

                    int r_sila = -1;

                    if (sila > 0)
                    {
                        int rr = Random.Range(0, sila + 1);
                       
                        //Debug.Log("rr= " + rr);
                        for (int i = 0; i < rr; i++)
                        {
                            
                            if (lt_rez.Contains(st))
                            {
                                r_sila++;
                                pr_mob.lt_carts_added.Add(st);
                                lt_rez.Remove(st);
                            }
                            else
                                break;
                        }
                        for (int i = 0; i < r_sila; i++)
                        {
                            pr_mob.rem_sila(st);
                        }

                    }

                }
            }
            pr_mob.remove_cart(type);

        }



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
                                pr_mob.speed_now= pr_mob.speed;
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
                                pr_mob.speed_now= pr_mob.speed;
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

    public static void clicable_object(bool bl,int x,int y)
    {
        //GameObject gg
        if (mob[x, y] != null && mob[x, y].GetComponent<BoxCollider2D>()!=null)
        {
            mob[x, y].GetComponent<BoxCollider2D>().enabled = bl;
        }
        if (eath[x, y] != null && eath[x, y].GetComponent<BoxCollider2D>()!=null)
        {
            eath[x, y].GetComponent<BoxCollider2D>().enabled = bl;
        }
        if (nature[x, y] != null && nature[x, y].GetComponent<BoxCollider2D>())
        {
            nature[x, y].GetComponent<BoxCollider2D>().enabled = bl;
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

            mb_now.par_lt_sila = new List<string>(mb_now.par_lt_sila_base);

            if (!mb_now.speed_hod)
            {


                //mb_now.par_lt_speed = new List<string>(mb_now.par_lt_speed_base);
                mb_now.par_lt_speed = new List<string>(mb_now.par_lt_speed_base);
                data.data_now.cl_carts_speed = 0;
                mb_now.speed_now--;
                //nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
            }
            if (mb_now.speed_now <= 0)
            {
                if (!mb_now.speed_hod)
                {
                    //mb_now.par_lt_sila = new List<string>(mb_now.par_lt_sila_base);
                    
                    //mb_now.par_lt_speed = new List<string>(mb_now.par_lt_speed_base);
                    mb_now.atacked = false;
                    //data.data_now.cl_carts_speed = 0;

                    nums_mob_wolk.RemoveAt(nums_mob_wolk.Count - 1);
                }
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
                    if (priority_hod != -1)
                        nums_mob_wolk = new List<int> { priority_hod };
                    end_hod_all();
                }


            }


            if (mob_list[hod_mob_now_int] != null)
            {
                mb_now.hod_now = false;
            }
            //if (hod_mob_now_int == 0)
            //Debug.Log("is sp=" + sp);
            //if (mb_now.speed_now <= 0)
            GameObject.Find("func").GetComponent<func>().baf_after_hod(sp);

            if (mb_now.long_life > 0)
                mb_now.long_life--;
            if (mb_now.long_life == 0)
                mb_now.dead();


        }



        timer_now = 0;
        //anim.hod_next_anim = false;
        hod_next_mob_art = false;

        next_hod_art_bool();
    }
    public static void next_hod()
    {


        Debug.Log("next hod debug");


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

        ui_atack.lt_cart_comb = new List<ui_atack.atack_comb>();
        ui_atack.lt_cart_comb_selected = new List<ui_atack.atack_comb>();
        ui_atack.lt_cart_comb_ui = new List<GameObject>();
        //ui_atack.last_type_atack = new List<ui_atack.atack_comb>();
    }

    public static GameObject[,] mob = new GameObject[size_global, size_global];
    public static GameObject[,] nature = new GameObject[size_global, size_global];
    public static GameObject[,] carts = new GameObject[size_global, size_global];
    public static GameObject[,] eath = new GameObject[size_global, size_global];
    public static Queue<GameObject>[,] que_effects = new Queue<GameObject>[size_global, size_global];
    public static List<GameObject>[,] lt_lovyshka=new List<GameObject>[size_global, size_global];
    public static List<GameObject>[,] lt_v2_viz_friend= new List<GameObject>[size_global, size_global];
    public static List<GameObject>[,] lt_v2_viz_mob= new List<GameObject>[size_global, size_global];

    public static List<GameObject> lovyshka_now;
    public static List<GameObject> lovyshka_after;

    public static string wall_now = "rock";
    public static string eath_now = "eath";
    //public func.room rt = new func.room();

    //public static List<GameObject>[,,,] decor = new List<GameObject>[5, 5, 7, 7];
    public static GameObject[] mob_list = new GameObject[size_global * size_global];
    public static List<int> nums_mob = new List<int>();
    public static List<int> nums_mob_wolk = new List<int>();
    public static Dictionary<string, string> cart_effects = new Dictionary<string, string>();
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


    public static Dictionary<type_lvl, string> cave_location = new Dictionary<type_lvl, string>();
    public static Dictionary<type_lvl, string> rock_location = new Dictionary<type_lvl, string>();
    public static Dictionary<type_lvl, string> grass_location = new Dictionary<type_lvl, string>();
    public static Dictionary<type_lvl, string> facel_location = new Dictionary<type_lvl, string>();
    public static Dictionary<type_lvl, string> camp_location = new Dictionary<type_lvl, string>();
    public static Dictionary<type_lvl, string> eath_location = new Dictionary<type_lvl, string>();
    public static Dictionary<type_lvl, string> stalact_location = new Dictionary<type_lvl, string>();

    public static void add_lt_ef(string st_comb,string name)
    {
        Transform tr = GameObject.Find("effects").transform;
        List<List<string>> lt_lt_st = par_cart.cart_combinations.get_comb_now(st_comb);
        Debug.Log("comb "+st_comb+" "+lt_lt_st.Count);
        foreach (List<string> lt in lt_lt_st)
        {
            Debug.Log("count "+lt.Count);
            string st_now = "";
            for (int i = 0; i < lt.Count; i++)
            {
                
                st_now += lt[i];
                if (i < lt.Count - 1)
                    st_now += "-";
            }
            Debug.Log(st_now+" is res");
            cart_effects.Add(st_now, name);
        }
    }
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
            bf_sp.Add(par_baf.type_baf.seal_spetial, sp_lt_bf[13]);
            /*
            bf_sp.Add(par_baf.type_baf.seal_damage, sp_lt_bf[13]);
            bf_sp.Add(par_baf.type_baf.seal_barier, sp_lt_bf[14]);
            bf_sp.Add(par_baf.type_baf.seal_blood, sp_lt_bf[15]);
            bf_sp.Add(par_baf.type_baf.seal_rock, sp_lt_bf[16]);
            bf_sp.Add(par_baf.type_baf.seal_fire, sp_lt_bf[17]);
            bf_sp.Add(par_baf.type_baf.seal_poition, sp_lt_bf[18]);
            bf_sp.Add(par_baf.type_baf.seal_ice, sp_lt_bf[19]);
            bf_sp.Add(par_baf.type_baf.seal_energy, sp_lt_bf[20]);
*/
            bf_sp.Add(par_baf.type_baf.berserk, sp_lt_bf[14]);

            bf_sp.Add(par_baf.type_baf.bar_rock, sp_lt_bf[15]);
            bf_sp.Add(par_baf.type_baf.bar_fire, sp_lt_bf[16]);
            bf_sp.Add(par_baf.type_baf.bar_energy, sp_lt_bf[17]);
            bf_sp.Add(par_baf.type_baf.bar_blood, sp_lt_bf[18]);
            bf_sp.Add(par_baf.type_baf.bar_ice, sp_lt_bf[19]);
            bf_sp.Add(par_baf.type_baf.bar_poition, sp_lt_bf[20]);


        }
        if (music_lvl.Count == 0)
        {
            music_lvl.Add(type_lvl.bases, music_lvl_lt[0]);
            music_lvl.Add(type_lvl.bases_1, music_lvl_lt[1]);
            music_lvl.Add(type_lvl.rock, music_lvl_lt[2]);
            music_lvl.Add(type_lvl.fire, music_lvl_lt[3]);
            music_lvl.Add(type_lvl.poition, music_lvl_lt[4]);
            music_lvl.Add(type_lvl.energy, music_lvl_lt[5]);
            music_lvl.Add(type_lvl.blood, music_lvl_lt[6]);
            music_lvl.Add(type_lvl.ice, music_lvl_lt[7]);
            
            
            music_lvl.Add(type_lvl.derlorg, music_lvl_lt[8]);
           
        }
        if (anim_color.static_colors.Count == 0)
        {
            //anim_color.static_colors.Add(par_cart.type_cart.sp_bomb, new Color32(255, 255, 255, 255));

            anim_color.static_colors.Add(par_cart.type_cart.damage, new Color32(189, 104, 255, 255));
            anim_color.static_colors.Add(par_cart.type_cart.sp_barier, new Color32(255, 104, 177, 255));
            anim_color.static_colors.Add(par_cart.type_cart.sp_bomb, new Color32(255, 222, 177, 255));
            anim_color.static_colors.Add(par_cart.type_cart.sp_dych, new Color32(165, 255, 211, 255));

            anim_color.static_colors.Add(par_cart.type_cart.blood, new Color32(219, 11, 11, 255));
            anim_color.static_colors.Add(par_cart.type_cart.fire, new Color32(255, 160, 29, 255));
            anim_color.static_colors.Add(par_cart.type_cart.energy, new Color32(123, 191, 255, 255));
            anim_color.static_colors.Add(par_cart.type_cart.ice, new Color32(229, 252, 255, 255));
            anim_color.static_colors.Add(par_cart.type_cart.poition, new Color32(96, 229, 83, 255));
            anim_color.static_colors.Add(par_cart.type_cart.rock, new Color32(123, 83, 59, 255));

            anim_color.static_colors.Add(par_cart.type_cart.derlorg, new Color32(255, 250, 144, 255));

            anim_color.static_colors.Add(par_cart.type_cart.rage_boss, anim_color.static_colors[par_cart.type_cart.blood]);
            anim_color.static_colors.Add(par_cart.type_cart.shaman_boss, anim_color.static_colors[par_cart.type_cart.sp_dych]);

            anim_color.static_colors.Add(par_cart.type_cart.spirit_blood, anim_color.static_colors[par_cart.type_cart.blood]);
            anim_color.static_colors.Add(par_cart.type_cart.spirit_fire, anim_color.static_colors[par_cart.type_cart.fire]);
            anim_color.static_colors.Add(par_cart.type_cart.spirit_energy, anim_color.static_colors[par_cart.type_cart.energy]);
            anim_color.static_colors.Add(par_cart.type_cart.spirit_ice, anim_color.static_colors[par_cart.type_cart.ice]);
            anim_color.static_colors.Add(par_cart.type_cart.spirit_poition, anim_color.static_colors[par_cart.type_cart.poition]);
            anim_color.static_colors.Add(par_cart.type_cart.spirit_rock, anim_color.static_colors[par_cart.type_cart.rock]);
        }

        //mob_list = new GameObject[size_global*size_global];
        //mob = new GameObject[size_global, size_global];
        //nature = new GameObject[size_global, size_global];
        //carts = new GameObject[size_global, size_global];
        //eath = new GameObject[size_global, size_global];
        //mob_list = new GameObject[size_global * size_global];
        //que_effects = new Queue<GameObject>[size_global, size_global];
        //lt_lovyshka = new List<GameObject>[size_global, size_global];
        //lt_v2_viz_friend = new List<GameObject>[size_global, size_global];
        //lt_v2_viz_mob = new List<GameObject>[size_global, size_global];
        for (int i = 0; i < size_global; i++)
        {
            for (int j = 0; j < size_global; j++)
            {
                lt_v2_viz_friend[j, i] = new List<GameObject>();
                lt_v2_viz_mob[j, i] = new List<GameObject>();
                que_effects[j, i] = new Queue<GameObject>();
                lt_lovyshka[j, i] = new List<GameObject>();
                lovyshka_now = new List<GameObject>();
                lovyshka_after = new List<GameObject>();
            }
        }

        if (cave_location.Count == 0)
        {
            cave_location.Add(type_lvl.bases, "cave");
            facel_location.Add(type_lvl.bases, "fakel");
            camp_location.Add(type_lvl.bases, "camp");
            rock_location.Add(type_lvl.bases, "rock");
            grass_location.Add(type_lvl.bases, "grass");
            eath_location.Add(type_lvl.bases, "eath");
            stalact_location.Add(type_lvl.bases, "stalact");


            cave_location.Add(type_lvl.bases_1, "cave 1");
            facel_location.Add(type_lvl.bases_1, "fakel 1");
            camp_location.Add(type_lvl.bases_1, "fakel 1");
            rock_location.Add(type_lvl.bases_1, "wall");
            grass_location.Add(type_lvl.bases_1, null);
            eath_location.Add(type_lvl.bases_1, "eath 1");
            stalact_location.Add(type_lvl.bases_1, "colonna");

            cave_location.Add(type_lvl.fire, "fire portal");
            facel_location.Add(type_lvl.fire, "camp fire");
            camp_location.Add(type_lvl.fire, "camp fire");
            rock_location.Add(type_lvl.fire, "rock fire");
            grass_location.Add(type_lvl.fire, "grass fire");
            eath_location.Add(type_lvl.fire, "eath fire");
            stalact_location.Add(type_lvl.fire, "stalact");


            cave_location.Add(type_lvl.poition, "poition portal");
            facel_location.Add(type_lvl.poition, "facel poition");
            camp_location.Add(type_lvl.poition, "facel poition");
            rock_location.Add(type_lvl.poition, "rock poition");
            grass_location.Add(type_lvl.poition, "grass poition");
            eath_location.Add(type_lvl.poition, "eath poition");
            stalact_location.Add(type_lvl.poition, "decor poition");

            cave_location.Add(type_lvl.ice, "ice portal");
            facel_location.Add(type_lvl.ice, "facel ice");
            camp_location.Add(type_lvl.ice, "facel ice");
            rock_location.Add(type_lvl.ice, "rock ice");
            grass_location.Add(type_lvl.ice, null);
            eath_location.Add(type_lvl.ice, "eath ice");
            stalact_location.Add(type_lvl.ice, "decor ice");

            cave_location.Add(type_lvl.blood, "blood portal");
            facel_location.Add(type_lvl.blood, "blood facel");
            camp_location.Add(type_lvl.blood, "blood facel");
            rock_location.Add(type_lvl.blood, "rock blood");
            grass_location.Add(type_lvl.blood, null);
            eath_location.Add(type_lvl.blood, "eath blood");
            stalact_location.Add(type_lvl.blood, "decor blood");

            cave_location.Add(type_lvl.derlorg, "cave derlorg");
            facel_location.Add(type_lvl.derlorg, "fakel derlorg");
            camp_location.Add(type_lvl.derlorg, "camp derlorg");
            rock_location.Add(type_lvl.derlorg, "rock derlorg");
            grass_location.Add(type_lvl.derlorg, null);
            eath_location.Add(type_lvl.derlorg, "eath derlorg");
            stalact_location.Add(type_lvl.derlorg, "stalact");

            cave_location.Add(type_lvl.rock, "rock portal");
            facel_location.Add(type_lvl.rock, "fakel");
            camp_location.Add(type_lvl.rock, "camp");
            rock_location.Add(type_lvl.rock, "rock rock");
            grass_location.Add(type_lvl.rock, null);
            eath_location.Add(type_lvl.rock, "eath rock");
            stalact_location.Add(type_lvl.rock, "decor rock");

            cave_location.Add(type_lvl.energy, "energy portal");
            facel_location.Add(type_lvl.energy, "energy facel");
            camp_location.Add(type_lvl.energy, "energy facel");
            rock_location.Add(type_lvl.energy, "rock energy");
            grass_location.Add(type_lvl.energy, null);
            eath_location.Add(type_lvl.energy, "eath energy");
            stalact_location.Add(type_lvl.energy, "decor energy");
        }


        //List<List<string>> lt_lt_st = par_cart.cart_combinations.get_comb_now("cart rock[n]");
        add_lt_ef("cart damage 0", "atack base");
        add_lt_ef("cart damage[n]", "atack base");
        add_lt_ef("cart barier[n]", "bases bar ef");

        add_lt_ef("cart rock[n]", "rock ef");
        add_lt_ef("cart blood[n]", "blood ef");
        add_lt_ef("cart fire[n]", "fire ef");
        add_lt_ef("cart ice[n]", "ice ef");
        add_lt_ef("cart energy[n]", "energy ef");
        add_lt_ef("cart poition[n]", "poition ef");

        add_lt_ef("cart damage[n]-cart rock[n]", "atack rock");
        add_lt_ef("cart damage[n]-cart blood[n]", "atack blood");
        add_lt_ef("cart damage[n]-cart fire[n]", "atack fire");
        add_lt_ef("cart damage[n]-cart ice[n]", "atack ice");
        add_lt_ef("cart damage[n]-cart energy[n]", "atack energy");
        add_lt_ef("cart damage[n]-cart poition[n]", "atack poition");

        add_lt_ef("cart barier[n]-cart rock[n]", "bases rock bar ef");
        add_lt_ef("cart barier[n]-cart blood[n]", "bases blood bar ef");
        add_lt_ef("cart barier[n]-cart fire[n]", "bases fire bar ef");
        add_lt_ef("cart barier[n]-cart ice[n]", "bases ice bar ef");
        add_lt_ef("cart barier[n]-cart energy[n]", "bases energy bar ef");
        add_lt_ef("cart barier[n]-cart poition[n]", "bases poition bar ef");

        add_lt_ef("cart derlorg[n]", "derlorg ef");
        add_lt_ef("cart bomb[n]", "bomb create");
        Transform tr = GameObject.Find("effects").transform;

        cart_effects.Add("cart rage(boss)", "rage ef");

        cart_effects.Add("spawn dych", "spawn dych");
        cart_effects.Add("spirit base", "spirit base");
        cart_effects.Add("berserk ef", "berserk ef");
        cart_effects.Add("bomb destroy", "bomb destroy");
        cart_effects.Add("create cave derlorg", "create cave derlorg");
        //cart_effects.Add("cart blood", tr.Find("rock ef").gameObject;
        //mob_list[0] = GameObject.Find("player");
        //Debug.Log("func");
        par_cart.cart_combinations.collections();
        //Debug.Log(par_cart.cart_combinations.get_comb(par_cart.cart_combinations.lt_comb).Count);
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

        type_lvl_now = type_lvl.bases;

        Debug.Log(data.data_now.first_game);
        if(data.data_now.first_game)
            generate_room_obych();
        else 
            generate_room();
        GameObject.Find("func").GetComponent<AudioSource>().clip = music_lvl[type_lvl_now];
        GameObject.Find("func").GetComponent<AudioSource>().Play();
        //par_cart.cart_combinations.print(par_cart.cart_combinations.get_comb("cart damage[n]-cart ice[n]"));

    }
    public float aaa = 0;
    public float aaa1 = 0;


    public List<int> lt1 = new List<int>();
    public List<int> lt2 = new List<int>();


    //public List<int> lt_i = new List<int>();
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(num_lev);
        //lt_i = nums_mob_wolk;
        //par_cart.cart_combinations.print(par_cart.cart_combinations.get_comb("cart ice-cart energy"));
        //Debug.Log(col_friend);
        //Debug.Log(par_cart.cart_combinations.lt_comb.Count);
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


