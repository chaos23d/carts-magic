using Unity.VisualScripting;
using UnityEngine;

public class par_move_camera : abs_physics
{
    //bool is_dialog = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(GameObject.Find("canvases").transform.Find("Canvas dialog").gameObject.activeSelf)
            base.FixedUpdate();
    }
}
