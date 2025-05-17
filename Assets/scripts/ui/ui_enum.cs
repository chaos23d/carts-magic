using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ui_enum : MonoBehaviour
{
    // Start is called before the first frame update
    public void select()
    {
        int num = gameObject.GetComponent<TMP_Dropdown>().value;
        par_mob mb = GameObject.Find("Main Camera").transform.parent.gameObject.GetComponent<par_mob>();
        if (num == 0)
        {
            mb.friend_type = par_mob.type_friend.auto;
        }
        else if (num == 1)
        {
            mb.friend_type = par_mob.type_friend.active;
        }
        else if (num == 2)
        {
            mb.friend_type = par_mob.type_friend.disactive;
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
