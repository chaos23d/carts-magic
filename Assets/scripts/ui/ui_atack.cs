using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_atack : MonoBehaviour
{
    public enum type_atack { 
        bases,
        mob,
        friend,
        mob_friend,
        eath
    }
    public class atack_comb {
        public Queue<string> carts = new Queue<string>();
        public type_atack atack_type = type_atack.bases;
        public bool active = false;

    }

    public GameObject Canvas_atack;
    public static atack_comb atack_comb_now;
    public static type_atack last_type_atack;
    public static List<atack_comb> lt_cart_comb = new List<atack_comb>();
    public static List<GameObject> lt_cart_comb_ui = new List<GameObject>();
    public static List<atack_comb> lt_cart_comb_selected = new List<atack_comb>();
    public static void add_cart_ui(string type)
    {

    }
    void OnDisable()
    {
        atack_comb_now = null;
        func.destroy_childs(Canvas_atack.transform.Find("pn atack dop").Find("Scroll carts atack").Find("Viewport").Find("Content carts atack"));
        
    }
    void OnEnable()
    {
        atack_comb_now = new atack_comb();
        atack_comb_now.atack_type = last_type_atack;
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
