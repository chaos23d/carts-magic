using UnityEngine;

public class act_d_teacher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void spawn_cart0()
    {
        if (par_dialog.type_now == "start")
        {
            GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().spawn_cart("cart damage 0", gameObject);
            
        }
        if (par_dialog.type_now == "start 1")
        {
            GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().spawn_cart("cart damage", gameObject);
            GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().spawn_cart("cart bomb", gameObject);

        }
        anim.next_dialog = true;
        gameObject.GetComponent<Animator>().SetBool("add carts", false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
