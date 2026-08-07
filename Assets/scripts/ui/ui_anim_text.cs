using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ui_anim_text : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //anim_text("Start is called once before the first execution of Update after the MonoBehaviour is created. Update is called once per frame");
    }
    public void anim_text(string st)
    {
        text = st;
        timer_now = 0;
        id_char_now = 0;
        gameObject.GetComponent<TMP_Text>().text ="";
        end = false;
    }
    public void end_text()
    {
        gameObject.GetComponent<TMP_Text>().text = text;
        id_char_now = text.Length;
        end = true;
    }
    private float timer = 0.06f;
    private float timer_now = 0;
    private string text = "";
    private int id_char_now = 0;
    public bool end = false;
    //public bool end_gs = false;
    // Update is called once per frame
    void Update()
    {
        timer_now += Time.deltaTime;
        if (timer_now >= timer && (text!=null)&& id_char_now < text.Length)
        {
            //text_now += text[id_char_now];
            gameObject.GetComponent<TMP_Text>().text += text[id_char_now];
            gameObject.GetComponent<AudioSource>().Play();
            id_char_now++;
            timer_now = 0;
            end = false;
        }
        else if(id_char_now >= text.Length)
        {
            end = true;
        }
    }
}
