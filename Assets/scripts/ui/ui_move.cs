using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ui_move : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool bl_click = false;
    public static bool bl_down = true;
    public static Vector3 v3_click;

    public static float wait = 0.2f;
    public static float wait_now = 0;
    /*
    public static void click(GameObject gm)
    {
        gm_click = gm;
        Debug.Log("down");
        bl_click = true;
        
        
        //v3_click = Input.GetTouch(0).position;
    }
    public static void unclick()
    {
        /*
        if (gm_click.GetComponent<par_eath>() != null)
            gm_click.GetComponent<par_eath>().bl_down = true;
        else if (gm_click.GetComponent<par_nat>() != null)
            gm_click.GetComponent<par_nat>().bl_down = true;
        else if (gm_click.GetComponent<par_mob>() != null)
            gm_click.GetComponent<par_mob>().bl_down = true;
        
        //gm_click = null;

        //v3_click = Input.GetTouch(0).position;
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        click(null);
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        unclick();
    }
*/
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //Debug.Log(bl_click + " " + wait + " " + wait_now);
        if (Input.touchCount == 1 && !bl_click)
        {
            bl_click = true;
            v3_click = Input.mousePosition;
            wait_now = 0;
        }
            
        else if (Input.touchCount == 0 && bl_click)
        {
            bl_click = false;
            v3_click = new Vector3(0, 0, 0);
            wait_now = 0;
        }

        if (wait_now < wait)
        {
            wait_now += Time.deltaTime;

            //if()
        }

        if (wait_now >= wait)
        {
            bl_down = !bl_click;

            
        }
        if (bl_click && wait_now >= wait)
        {

            Vector3 d_v3 = (Input.mousePosition- v3_click) * -0.005f;
            
            Vector3 v3 = d_v3 + GameObject.Find("Main Camera").transform.position;
            //Debug.Log("");
            //Debug.Log(Input.mousePosition + "  "+v3_click+"      "+d_v3+" "+v3);
            //Debug.Log("");
            GameObject.Find("Main Camera").transform.position = v3;
            v3_click = Input.mousePosition;
        }
    }
}
