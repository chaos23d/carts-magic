using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class shader_grad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Awake()
    {
        
        Sprite sprite = gameObject.GetComponent<Image>().sprite;
        Debug.Log(gameObject);
        Texture2D tex = sprite.texture;
        Rect rect = sprite.textureRect;

        // Важно: текстура должна быть Read/Write Enabled в Import Settings.
        Color[] pixels = tex.GetPixels(
            (int)rect.x,
            (int)rect.y,
            (int)rect.width,
            (int)rect.height
        );

        Vector4 sum = Vector4.zero;
        sum.w = 1;
        int count = 0;

        for (int i = 0; i < pixels.Length; i++)
        {
            Color c = pixels[i];

            sum.x += c.r;
            sum.y += c.g;
            sum.z += c.b;
            //sum.w += c.a;
            count++;
        }

        sum /= count;
        gameObject.GetComponent<Image>().material.SetColor("_AvgColor", sum);
        gameObject.GetComponent<Image>().material.SetTexture("_MainTex", tex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
