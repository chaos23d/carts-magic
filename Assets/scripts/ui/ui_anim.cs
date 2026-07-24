using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_anim : MonoBehaviour
{
    // Start is called before the first frame update
    public void show_add()
    {
        add.create_add();
        anim.col_anim--;
    }
    public void show_player()
    {
        ui.update_pn_players();
        gameObject.GetComponent<Animator>().SetBool("opened", true);
    }
    public void Destroyed_achive()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void en_anim()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
    public void play_music()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
