using UnityEngine;

public class par_bezie : MonoBehaviour
{
    public Vector2 p1;
    public Vector2 p2;
    public Vector2 p3;
    float a = 0;
    public float time;
    public float time_now;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time_now = 0;
        time = 3;
        p1=new Vector2 (0,0);
        transform.position = p1;
        p2 = new Vector2 (0,3)+new Vector2( Random.Range(-10,10), Random.Range(-10, 10))/20;
        p3= new Vector2 (3,0) + new Vector2(Random.Range(-10, 10), Random.Range(-10, 10))/20;
    }

    // Update is called once per frame
    void Update()
    {
        if (time_now < time)
        {
            a = time_now / time;
            Vector2 pp1 = p1 + (p2 - p1) * a;
            Vector2 pp2 = p2 + (p3 - p2) * a;
            Vector2 pp = pp1 + (pp2 - pp1) * a;
            transform.position = pp;
            time_now += Time.deltaTime;
        }

    }
}
