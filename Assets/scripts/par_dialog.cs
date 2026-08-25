using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Func<GameObject> center_camera = null;
        public bool active_camera = true;
        public System.Action action = null;
        public cl_dialog(string name, string tx, string st_anim = "", bool param = false, bool act_camera = true, GameObject cn_camera =null, Action action = null)
        {
            this.name = name;
            this.text = tx;
            this.state_anim = st_anim;
            this.center_camera = get_GameObject(cn_camera);
            this.param = param;
            this.active_camera = act_camera;
            this.action = action;
        }
    }

    public Dictionary<string, List<cl_dialog>> lt_dialog= new Dictionary<string, List<cl_dialog>>();
    public static string type_now;
    public static int id_now;

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
        if (!parent.Find("Canvas dialog").gameObject.activeSelf && lt_dialog[type_now][id_now].active_camera)
            parent.Find("Canvas dialog").gameObject.SetActive(true);
        else if (!lt_dialog[type_now][id_now].active_camera)
            parent.Find("Canvas dialog").gameObject.SetActive(false);
        GameObject gm_m = null;
        if (lt_dialog[type_now][id_now].center_camera() != null)
        {
            gm_m= lt_dialog[type_now][id_now].center_camera();
        }
        else
        {
            gm_m = gameObject;
        }
            //if (gm_m == null)
            //gm_m = gameObject;
        GameObject.Find("Main Camera").transform.parent = gm_m.transform;
        GameObject.Find("Main Camera").gameObject.GetComponent<par_move_camera>().sets(1,false,(Vector2)gm_m.transform.position);
        Debug.Log(type_now);
        Debug.Log(lt_dialog[type_now][id_now].text);
        parent.Find("Canvas dialog").Find("pn dialog").Find("text dialog").GetComponent<ui_anim_text>().anim_text(lt_dialog[type_now][id_now].text);
        //Debug.Log(lt_dialog[type_now][id_now].state_anim+" " + lt_dialog[type_now][id_now].param);
        if (lt_dialog[type_now][id_now].state_anim != "")
        {
            gameObject.GetComponent<Animator>().SetBool(lt_dialog[type_now][id_now].state_anim, lt_dialog[type_now][id_now].param);
        }
        if (lt_dialog[type_now][id_now].action != null)
        {
            lt_dialog[type_now][id_now].action.Invoke();
        }

    }

    public static void end_dialog()
    {
        Transform parent = GameObject.Find("canvases").transform;
        if (!parent.Find("Canvas").gameObject.activeSelf)
            parent.Find("Canvas").gameObject.SetActive(true);
        if (parent.Find("Canvas dialog").gameObject.activeSelf)
            parent.Find("Canvas dialog").gameObject.SetActive(false);

        data.data_now.lt_dialog.Add(type_now);
        GameObject.Find("Main Camera").transform.parent = GameObject.Find(data.data_now.pl_name_now).transform;
        GameObject.Find("Main Camera").gameObject.GetComponent<par_move_camera>().sets(1, false, (Vector2)GameObject.Find(data.data_now.pl_name_now).transform.position);
        //par_dialog_now = null;
        //GameObject.Find("Main Camera").gameObject.GetComponent<par_move_camera>().sets(1, false, (Vector2)gameObject.transform.position);
    }
    static Func<GameObject> get_GameObject(GameObject g)
    {
        return (() => { return g; });
    }
    public void wolk_dialog(int x,int y)
    {
        par_mob mb = gameObject.GetComponent<par_mob>();
        mb.lt_wolk = new Stack<Vector2>();
        mb.lt_wolk.Push(new Vector2(x, y));
        List<Vector2> l_v2 = func.way_wolk(mb.x, mb.y, x, y).ToList();
        l_v2.Reverse();
        foreach (Vector2 v2 in l_v2)
        {
            mb.lt_wolk.Push(v2);

        }
        //return 
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
            l.Add(new cl_dialog("учитель",  "2) Большинство карт тратят энергию. Для того, чтобы использовать магию карт ты должен приложить энергию."));
            l.Add(new cl_dialog("учитель", "Без энергии ты не сможешь использовать карты. И ещё. Чем мощнее карта , тем больше энергии она тратит.", "think 2", false) );
            l.Add(new cl_dialog("учитель", "3) Количество применяемых комбинаций карт ограничено. Карты нужно применять в определённом порядке для определённого эффекта.", "think 2", true));
            l.Add(new cl_dialog("учитель", "Если ты не правильно применишь комбинацию, ты не сможешь применить магию.", "think 2", false));
            l.Add(new cl_dialog("учитель", "Так, вроде бы всё сказал.", "think", true));
            l.Add(new cl_dialog("учитель", "Эти ограничения касаются всех магов , не только тебя. Поэтому при правильной стратегии, ты можешь вымотать своего врага для своей победы.", "think", false));
            //lt_dialog.Add("start", l);

            //l = new List<cl_dialog>();
            l.Add(new cl_dialog("учитель", "А сейчас ты попрактикуешься в применении карт."));
            l.Add(new cl_dialog("учитель", "иди за мной"));

            Action a = () =>
            {
                Debug.Break();
                //func.next_hod();
                wolk_dialog(9, 7);
                
                Action f = () =>
                {
                    func.controller_mob_wait.type_wait_now = func.controller_mob_wait.type_wait.mob;
                    func.controller_mob_wait.num_wait = gameObject.GetComponent<par_mob>().num;
                    //par_mob.priority_hod = 
                    func.bl_wait_hod = false;
                    //func.bl_skip_all_friends = false;
                    id_now++;
                    start_dialog();
                };
                gameObject.GetComponent<par_mob>().action_after_wolk_now = new par_mob.action_after_wolk(f);
                //gameObject.GetComponent<par_mob>().wolk_v2();
                

                par_mob pl= GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                pl.lt_wolk = func.way_wolk(pl.x, pl.y, 9, 5);
                //par_mob.priority_hod = -1;
                //func.bl_skip_all_friends = true;
                func.controller_mob_wait.type_wait_now = func.controller_mob_wait.type_wait.skip_all;
                func.bl_wait_hod = true;
                

                //pl.lt_wolk.Pop();
                //func.next_hod();
                //teacher.wolk_v2();
            };
            l.Add(new cl_dialog("учитель", "", act_camera: false, action:a));
            l.Add(new cl_dialog("учитель", "Держи эту карту.", act_camera: true));
            a = () =>
            {
                gameObject.GetComponent<act_d_teacher>().action_anim = gameObject.GetComponent<act_d_teacher>().spawn_cart0;
            };
            l.Add(new cl_dialog("учитель", "", act_camera: false,param:true,st_anim:"add carts",action:a));
            //Func<GameObject> gg =(()=>{  return func.nature[1, 1]; }); 
            //GameObject gg(x=>{ return func.nature[1, 1]});
            l.Add(new cl_dialog("учитель", "Видишь этот манекен?", act_camera: true, cn_camera:  func.mob[9,2]));


            a = () =>
            {
                par_mob pl = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                foreach (Vector2 v2 in pl.lt_v2_vizited)
                {
                    func.clicable_object(false, (int)v2.x, (int)v2.y);
                }
                func.clicable_object(true, pl.x, pl.y-3);
                //par_mob.priority_hod = -1;
                func.controller_mob_wait.type_wait_now = func.controller_mob_wait.type_wait.none;
                //func.controller_mob_wait.num_wait = gameObject.GetComponent<par_mob>().num;
                func.bl_wait_hod = true;

            };
            l.Add(new cl_dialog("учитель", "Используй карту , чтобы его уничтожить.",action:a));
            //l.Add(new cl_dialog("учитель", "",act_camera:false));

            lt_dialog.Add("start", l);

            l = new List<cl_dialog>();
            l.Add(new cl_dialog("учитель", "Неплохо справляешься."));
            /*
            l.Add(new cl_dialog("учитель", "Прежде чем осваивать комбинации карт, мне стоит рассказать немного о видах магии, которую ты можешь применять"));
            l.Add(new cl_dialog("учитель", "В основном выделяют 11 видов магии:"));
            l.Add(new cl_dialog("учитель", "боевая магия) Эта магия основана на картах, которые наносят урон"));
            l.Add(new cl_dialog("учитель", "антимагия) Эта магия способна защитить от магических эффектов."));
            l.Add(new cl_dialog("учитель", "взрывная магия) Эта магия усиливает магический эффект по мощности и по зоне действия"));
            l.Add(new cl_dialog("учитель", "некромантия) Эта магия создаёт духов, которые способны использовать карты, из которых был создан"));
            l.Add(new cl_dialog("учитель", "огненная магия) Эта магия основана на элементе огня, которая поджигает и наносит урон."));
            l.Add(new cl_dialog("учитель", "кристальная магия) Эта магия основана на элементе огня, которая поджигает и наносит урон."));
            */
            l.Add(new cl_dialog("учитель", "теперь попробуй в применении комбинации карт"));
            l.Add(new cl_dialog("учитель", "иди за мной"));

            a = () =>
            {
                //Debug.Break();
                //func.next_hod();
                wolk_dialog(13, 7);

                Action f = () =>
                {
                    func.controller_mob_wait.type_wait_now = func.controller_mob_wait.type_wait.mob;
                    func.controller_mob_wait.num_wait = gameObject.GetComponent<par_mob>().num;
                    id_now++;
                    start_dialog();
                };
                gameObject.GetComponent<par_mob>().action_after_wolk_now = new par_mob.action_after_wolk(f);
                //gameObject.GetComponent<par_mob>().wolk_v2();


                par_mob pl = GameObject.Find(data.data_now.pl_name_now).GetComponent<par_mob>();
                pl.lt_wolk = func.way_wolk(pl.x, pl.y, 13, 5);

                func.controller_mob_wait.type_wait_now = func.controller_mob_wait.type_wait.skip_all;
                func.bl_wait_hod = true;
                //pl.lt_wolk.Pop();
                //func.next_hod();
                //teacher.wolk_v2();
            };
            l.Add(new cl_dialog("учитель", "", act_camera: false, action: a));
            l.Add(new cl_dialog("учитель", "Держи эти карты.", act_camera: true));
            a = () =>
            {
                gameObject.GetComponent<act_d_teacher>().action_anim = gameObject.GetComponent<act_d_teacher>().spawn_cart1;
            };
            l.Add(new cl_dialog("учитель", "", act_camera: false, param: true, st_anim: "add carts",action:a));
            l.Add(new cl_dialog("учитель", "Видишь этот манекен?", act_camera: true, cn_camera: func.mob[13, 2]));
            l.Add(new cl_dialog("учитель", "Тебе также нужно его уничтожить. Но в этот раз я его усилю защитой"));

            a = () =>
            {
                List<string> q = new List<string>();
                q.Add("cart barier");
                q.Add("cart damage");
                par_mob mb = gameObject.GetComponent<par_mob>();
                mb.lt_atack_comb.Enqueue(new par_mob.action_atack(q,new Vector2(mb.x,mb.y-3)));
                func.bl_wait_hod = true;
                //func.next_hod();
                //вон тут
            };

            l.Add(new cl_dialog("учитель", "", act_camera: false, action:a));
            l.Add(new cl_dialog("учитель", "Для этого примени карты в таком порядке: карта взрыва -> карта атаки -> карта электричества"));

            lt_dialog.Add("start 1", l);
            l = new List<cl_dialog>();
            l.Add(new cl_dialog("учитель", "Отлично."));
            l.Add(new cl_dialog("учитель", "Ты наверное заметил, что у тебя энергия уменьшилась"));
            l.Add(new cl_dialog("учитель", "Подобные комбинации тратят немало энергии. Удивляться тут нечему."));
            l.Add(new cl_dialog("учитель", "Но ты можешь не переживать.Энергия со временем восстанавливается. Каждые 5 ходов восстанавливается 1 единица энергии ."));
            l.Add(new cl_dialog("учитель", "В твоём случае"));

            l.Add(new cl_dialog("учитель", "Теперь стоит устроить настоящее сражение"));
            l.Add(new cl_dialog("учитель", "Сейчас перенесу тебя на боевую площадку"));

            l.Add(new cl_dialog("учитель", "тебе надо одолеть 3 духов"));
            l.Add(new cl_dialog("учитель", "используй эти карты для сражения."));
            l.Add(new cl_dialog("учитель", "их должно хватить."));
            lt_dialog.Add("start 2", l);


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
