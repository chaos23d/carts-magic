using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class par_dialog : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static par_dialog par_dialog_now;
    public class cl_dialog
    {
        public string name;
        public string text;
        public string state_anim = null;
        public bool param = false;
        public bool center_camera = true;
        public cl_dialog(string name,string tx,string st_anim="",bool param=false,bool cn_camera=true)
        {
            this.name = name;
            this.text = tx;
            this.state_anim = st_anim;
            this.center_camera = cn_camera;
            this.param= param;
        }
    }

    public Dictionary<string, List<cl_dialog>> lt_dialog= new Dictionary<string, List<cl_dialog>>();
    public string type_now;
    public int id_now;

    public void start_dialog(string type=null)
    {
        //Debug.Break();
        par_dialog_now = this;

        if (type != null)
        {
            id_now = 0;
            type_now = type;
        }

        Transform parent = GameObject.Find("canvases").transform;
        if(parent.Find("Canvas").gameObject.activeSelf)
            parent.Find("Canvas").gameObject.SetActive(false);
        if (!parent.Find("Canvas dialog").gameObject.activeSelf)
            parent.Find("Canvas dialog").gameObject.SetActive(true);
        GameObject.Find("Main Camera").transform.parent = gameObject.transform;
        GameObject.Find("Main Camera").gameObject.GetComponent<par_move_camera>().sets(1,false,(Vector2)gameObject.transform.position);
        Debug.Log(type_now);
        Debug.Log(lt_dialog[type_now][id_now].text);
        parent.Find("Canvas dialog").Find("pn dialog").Find("text dialog").GetComponent<ui_anim_text>().anim_text(lt_dialog[type_now][id_now].text);
        //Debug.Log(lt_dialog[type_now][id_now].state_anim+" " + lt_dialog[type_now][id_now].param);
        if (lt_dialog[type_now][id_now].state_anim != "")
        {
            gameObject.GetComponent<Animator>().SetBool(lt_dialog[type_now][id_now].state_anim, lt_dialog[type_now][id_now].param);
        }


    }

    public static void end_dialog()
    {
        Transform parent = GameObject.Find("canvases").transform;
        if (!parent.Find("Canvas").gameObject.activeSelf)
            parent.Find("Canvas").gameObject.SetActive(true);
        if (parent.Find("Canvas dialog").gameObject.activeSelf)
            parent.Find("Canvas dialog").gameObject.SetActive(false);
        //GameObject.Find("Main Camera").gameObject.GetComponent<par_move_camera>().sets(1, false, (Vector2)gameObject.transform.position);
    }
    void Start()
    {
        if (gameObject.name == "teacher")
        {
            List<cl_dialog> l = new List<cl_dialog>();

            l.Add(new cl_dialog("учитель","О, я вижу ты пришёл"));
            l.Add(new cl_dialog("учитель", "Значит это правда, что ты хочешь отправиться в подземелье на поиски приключений."));
            l.Add(new cl_dialog("учитель", "Уж много авантюристов пошло в последнее время","think",true));
            l.Add(new cl_dialog("учитель", "Хочу предупредить , что без подготовки ты там недолго протянешь.", "think", false));
            l.Add(new cl_dialog("учитель", "Мне когда-то приходилось сражаться с сильнейшими магами.", "think 1", true));
            l.Add(new cl_dialog("учитель", "И я знаю , что тебя ожидает.", "think 1", false));
            //lt_dialog.Add("start", l);

            //l = new List<cl_dialog>();
            l.Add(new cl_dialog("учитель", "Но не волнуйся."));
            l.Add(new cl_dialog("учитель", "сейчас тебе объясню , что такое магические карты , и как ими пользоваться"));
            l.Add(new cl_dialog("учитель", "магическая карта - это предмет , в которой содержится определённая магия. С помощью таких карт можно творить удивительные вещи.", "think 2", true));
            l.Add(new cl_dialog("учитель", "Кроме того , магию карт можно комбинировать между собой. Это даёт большое преимущество в бою.", "think 2", false));
            l.Add(new cl_dialog("учитель", "Однако есть некоторые ограничения в применения карт."));
            l.Add(new cl_dialog("учитель", "1) Карта одноразовая. Ты не можешь применять одну карту несколько раз. Поэтому следи за тем, сколько карт у тебя осталось", "think 2", true));
            l.Add(new cl_dialog("учитель",  "2) Большинство карт тратят энергию. Для того, чтобы использовать магию карт ты должен приложить энергию. Без энергии ты не сможешь использовать карты."));
            l.Add(new cl_dialog("учитель", "Без энергии ты не сможешь использовать карты. И ещё. Чем мощнее карта , тем больше энергии она тратит.", "think 2", false) );
            l.Add(new cl_dialog("учитель", "3) Количество применяемых комбинаций карт ограничено. Карты нужно применять в определённом порядке для определённого эффекта.", "think 2", true));
            l.Add(new cl_dialog("учитель", "Если ты не правильно применишь комбинацию, ты не сможешь применить магию.", "think 2", false));
            l.Add(new cl_dialog("учитель", "Так, вроде бы всё сказал.", "think", true));
            l.Add(new cl_dialog("учитель", "Эти ограничения касаются всех магов , не только тебя. Поэтому при правильной стратегии, ты можешь вымотать своего врага для своей победы.", "think", false));
            lt_dialog.Add("start", l);

            l = new List<cl_dialog>();
            l.Add(new cl_dialog("учитель", "А сейчас ты попрактикуешься в применении карт."));
            l.Add(new cl_dialog("учитель", "Держи эти карты."));
            l.Add(new cl_dialog("учитель", "Видишь этот манекен? Используй эти карты , чтобы его уничтожить."));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
