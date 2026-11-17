using UnityEngine;

public class act_d_teacher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public System.Action action_anim;
    public void anim_action()
    {
        action_anim.Invoke();
    }
    public void spawn_cart0()
    {

            GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().spawn_cart("cart damage 0", gameObject);

        
        anim.next_dialog = true;
        gameObject.GetComponent<Animator>().SetBool("add carts", false);
    }
    public void spawn_cart1()
    {

            GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().spawn_cart("cart damage", gameObject);
            GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>().spawn_cart("cart energy", gameObject);

        
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
