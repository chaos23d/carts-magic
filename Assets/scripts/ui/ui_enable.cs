using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_enable : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        if(gameObject.name== "pn carts add")
        {
            GameObject cont = gameObject.transform.Find("Viewport").Find("Content").gameObject;
            foreach (Transform tr in cont.transform)
            {
                Destroy(tr.gameObject);
            }
            par_mob pl = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
            List<string> carts_now = new List<string>();
            foreach (string st in pl.lt_carts)
            {
                if(!carts_now.Contains(st) && !GameObject.Find(st).GetComponent<par_cart>().art_cart_bl())
                {
                    carts_now.Add(st);
                }
            }
            foreach (string st in carts_now)
            {
                GameObject obj = Instantiate(GameObject.Find("ignoring").transform.Find("cart atack").gameObject, cont.transform);
                obj.GetComponent<ui_button>().type = st;
                obj.GetComponent<Image>().sprite = GameObject.Find(st).GetComponent<SpriteRenderer>().sprite;
                //obj.transform.parent = ;
            }

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
