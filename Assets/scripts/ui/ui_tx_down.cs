using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ui_tx_down : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL("https://vk.com/com23d");


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
