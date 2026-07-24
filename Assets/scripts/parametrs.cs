using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parametrs : MonoBehaviour
{

    public int hp;
    public int hp_max_prebaf;
    public int hp_max {
        get
        {
            return hp_max_prebaf;
        }
        set
        {
            hp_max_prebaf = value;
            if(gameObject.name== data.data_now.pl_name_now)
            {
                if (hp_max_prebaf >= 30)
                {
                    //if()
                    if(!data.data_now.lt_players.Contains(data.players.berserk))
                        data.data_now.lt_players.Add(data.players.berserk);
                    //data.data_now.lt_players.Add(data.players.berserk);
                    //data.data_now.lt_players.Add(data.players.berserk);
                    data.give_achive("живучий");
                }
            }
        }
    }

    public int speed_att;

    public int x;
    public int y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
