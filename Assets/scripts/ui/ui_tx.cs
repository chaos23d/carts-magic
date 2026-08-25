using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        TMP_Text tx = gameObject.GetComponent<TMP_Text>();
        if (func.mob_list[func.hod_mob_now] != null)
        {
            par_mob mb = func.mob_list[func.hod_mob_now].GetComponent<par_mob>();
            if (gameObject.name == "hp tx" && mb.friend && mb.friend_type == par_mob.type_friend.active)
                tx.text = mb.hp + "/" + mb.hp_max;
        }

    }
}
