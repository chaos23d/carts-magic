using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class par_prise : MonoBehaviour
{
    public List<string> lt_price = new List<string>();
    public bool can_buy(List<string> lt_buy)
    {
        bool bl = true;
        foreach (string st in lt_price)
        {
            if (!lt_buy.Contains(st))
                bl = false;
        }
        return bl;
    }
    public void buy(par_mob mb)
    {
        foreach (string st in lt_price)
        {
            mb.remove_cart(st);
        }
        Destroy(this);
    }
    public void add_price(string cart, int col_pr)
    {

        add_price(cart, col_pr, 1);
    }
    public void add_price(string cart,int col, int col_carts)
    {
        GameObject gm = Instantiate(GameObject.Find("ignoring gm").transform.Find("Canvas price").gameObject, gameObject.transform);
        gm.transform.localPosition = new Vector3(0, 0, 0);
        gm.name = "Canvas price";

        GameObject gm_pr = Instantiate(GameObject.Find("Canvas").transform.Find("panels").Find("ignoring").Find("pn prise").gameObject, gameObject.transform.Find("Canvas price"));
        gm_pr.name = "pn prise";
        gm_pr.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        gm_pr.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0.75f, 0);

        for (int i = 0; i < col; i++)
        {
            lt_price.Add(cart);
            gm_pr.transform.Find("im_price").gameObject.GetComponent<Image>().sprite = GameObject.Find(cart).GetComponent<SpriteRenderer>().sprite;
            string st_col = "";
            if (col_carts > 1)
                st_col = "[" + col_carts.ToString() + "]";
            gm_pr.transform.Find("im_price").Find("tx").gameObject.GetComponent<TMP_Text>().text = col.ToString()+st_col;
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
