using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class par_prise : MonoBehaviour
{
    public enum type_price
    {
        gold,
        hp,
        obmen
    }
    public type_price type;
    public List<string> lt_price = new List<string>();
    public bool can_buy(List<string> lt_pr,List<string> lt_buy)
    {
        bool bl = true;
        foreach (string st in lt_pr)
        {
            if (!lt_buy.Contains(st))
                bl = false;
        }
        return bl;
    }
    public void buy(par_mob mb)
    {
        Debug.Log("buy " + type +" "+ lt_price.Count);
        if (type == type_price.gold|| type == type_price.obmen)
        {
            foreach (string st in lt_price)
            {
                mb.remove_cart(st);
            }
        }
        else if (type == type_price.hp)
        {
            mb.hp_max -= int.Parse(lt_price[0]);
            if (mb.hp > mb.hp_max)
                mb.hp = mb.hp_max;
        }
        if(type == type_price.obmen)
        {
            mb.hp_max++;
            mb.hp++;
            
        }
        else
        {
            Destroy(this);
        }
        
    }
    public void add_price(string cart, int col_pr,type_price tp)
    {

        add_price(cart, col_pr, 1,tp);
    }
    public void add_price(string cart,int col, int col_carts,type_price tp)
    {
        GameObject gm = Instantiate(GameObject.Find("ignoring gm").transform.Find("Canvas price").gameObject, gameObject.transform);
        gm.transform.localPosition = new Vector3(0, 0, 0);
        gm.name = "Canvas price";




        GameObject gm_pr = Instantiate(GameObject.Find("Canvas").transform.Find("panels").Find("ignoring").Find("pn prise").gameObject, gameObject.transform.Find("Canvas price"));
        gm_pr.name = "pn prise";
        gm_pr.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        gm_pr.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0.7f, 0);

        string gm_f = null;
        if (tp == type_price.gold)
            gm_f = "im_price";
        else if (tp == type_price.hp)
            gm_f = "im_price derlorg";
        else if (tp == type_price.obmen)
            gm_f = "im_price ber";

        if (tp == type_price.gold || tp == type_price.obmen)
        {
            for (int i = 0; i < col; i++)
            {
                lt_price.Add(cart);

            }
        }
        else if (tp == type_price.hp)
        {
            lt_price.Add(col.ToString());
        }
        type = tp;
        gm_pr.transform.Find(gm_f).gameObject.SetActive(true);
        if (tp != type_price.obmen)
        {
            //gm_pr.transform.Find(gm_f).gameObject.GetComponent<Image>().sprite = GameObject.Find(cart).GetComponent<SpriteRenderer>().sprite;
            string st_col = "";
            if (col_carts > 1)
                st_col = "[" + col_carts.ToString() + "]";
            gm_pr.transform.Find(gm_f).Find("tx").gameObject.GetComponent<TMP_Text>().text = col.ToString() + st_col;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
