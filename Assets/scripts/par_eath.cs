using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class par_eath : MonoBehaviour, event_click, IPointerClickHandler
{

    // Start is called before the first frame update
    public int x, y;
    //public static List<Sprite> rd_im;
    public List<Sprite> rd_im_st = new List<Sprite>();

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

        click(eventData);
    }

    public void click(PointerEventData eventData)
    {
        //if (ui_move.bl_down)
        //{
            func.click(gameObject);

    }
    void Awake()
    {
        /*
        if (rd_im == null)
        {
            rd_im = new List<Sprite>(rd_im_st);
        }
        */
        

        /*
        if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "eath(grass)")
        {
            GameObject gm = Instantiate(GameObject.Find("grass"), gameObject.transform);
            gm.transform.localPosition = new Vector3(0, 0, 0);
        }
        */
        func func = GameObject.Find("func").GetComponent<func>();
        if (!func.baza.ContainsKey(gameObject.name))
            func.baza.Add(gameObject.name, gameObject);
        else
        {
            if (gameObject.GetComponent<Animator>() == null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = rd_im_st[Random.Range(0, rd_im_st.Count)];
            }
            else
            {
                gameObject.GetComponent<Animator>().enabled = true;
                int a_e = Random.Range(0, rd_im_st.Count);
                //Debug.Log("create cuzna "+a_e);
                if (a_e > 0)
                {

                    gameObject.GetComponent<Animator>().enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
}



