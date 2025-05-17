using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_tx : MonoBehaviour
{
    // Start is called before the first frame update
    void del()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text tx = gameObject.GetComponent<Text>();
        if (func.mob_list[func.hod_mob_now_int] != null)
        {
            par_mob mb = func.mob_list[func.hod_mob_now_int].GetComponent<par_mob>();
            if (gameObject.name == "hp tx" && mb.friend && mb.friend_type == par_mob.type_friend.active)
                tx.text = mb.hp + "/" + mb.hp_max;
        }

    }
}
