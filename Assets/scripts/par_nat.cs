using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class par_nat : MonoBehaviour, event_click,IPointerClickHandler
{
    // Start is called before the first frame update
    public string type;
    public bool vizible = false;
    public bool undes = false;

    public bool dam = false;
    public int x;
    public int y;
    
    //public bool bl_down = true;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        click(eventData);
    }
    /*
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        ui_move.click(gameObject);
    }
    
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        ui_move.unclick();
    }
    */
    public void click(PointerEventData eventData)
    {
        func.click(gameObject);
    }
    public void Start()
    {
        func func = GameObject.Find("func").GetComponent<func>();
        if (!func.baza.ContainsKey(gameObject.name))
            func.baza.Add(gameObject.name, gameObject);
    }

}


