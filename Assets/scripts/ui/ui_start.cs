using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui_start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "menu")
        {
            data.load_data();
            if (data.is_open)
                gameObject.GetComponent<Animator>().speed = 10;
            data.is_open = true;
            //Screen.autorotateToLandscapeLeft = true;
            //Screen.autorotateToLandscapeRight = true;
            //Screen.autorotateToPortrait = false;
            //Screen.autorotateToPortraitUpsideDown = false;
        }
            
        else if (SceneManager.GetActiveScene().name == "menu player")
        {
            if (ui.cart_sprite.Count == 0)
                foreach (Transform tr in GameObject.Find("list carts").transform)
                {
                    //Debug.Log(tr.gameObject.name);
                    Sprite sp = tr.gameObject.GetComponent<SpriteRenderer>().sprite;
                    ui.cart_sprite.Add(tr.gameObject.name, sp);
                }
            ui.update_pn_players();
        }
        else if (SceneManager.GetActiveScene().name == "achives")
        {
            foreach (string st in data.data_now.lt_achives.Keys)
            {
                GameObject gm = Instantiate(GameObject.Find("achive"), GameObject.Find("sc pn ach").transform.Find("Viewport").Find("Content"));
                gm.transform.Find("name").GetComponent<Text>().text = st;
                gm.transform.Find("im ram").Find("infa").GetComponent<Text>().text = data.data_now.lt_achives[st];
                if (data.data_now.lt_achives_open.Contains(st))
                {
                    gm.transform.Find("mask").gameObject.active = false;
                }
            }
        }
        else if(gameObject.name=="Main Camera")
        {
            gameObject.transform.parent = GameObject.Find(data.data_now.pl_name_now).transform;
            GameObject.Find("tx lvl").GetComponent<Text>().text = func.num_lev + " этаж";
            if (data.data_now.first_game)
            {
                GameObject.Find("tx lvl").GetComponent<Text>().text = "тренировка";
                //gameObject.GetComponent<Animator>().enabled = false;
                //GameObject.Find("tx lvl").GetComponent<Animator>().enabled = false;
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
