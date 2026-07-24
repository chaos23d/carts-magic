Shader "Unlit/shader_grad"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AvgColor ("Average Color", Color) = (1,1,1,1)
        _BackgroundColor ("Background Color", Color) = (0,0,0,1)
        _CircleRadius ("Circle Radius", Float) = 0.25
        _PathRadius ("Path Radius", Float) = 0.25
        _Speed ("Rotation Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _AvgColor;
            float4 _BackgroundColor;
            float  _CircleRadius;
            float  _PathRadius;
            float  _Speed;

            // Built-in time: (t/20, t, t*2, t*3) [web:2][web:3][web:6]

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {

                // Переводим UV (0..1) в пространство -0.5..0.5, центр экрана в (0,0)

                // Угол для движения центра окружности
                float t = _Time.y * _Speed;

                // Центр окружности движется по окружности радиуса _PathRadius
                float2 center = float2(
                    0,
                    sin(t)
                )*0.4+0.5;


                // Вектор от текущего пикселя до центра окружности
                float2 diff = i.uv - center;
                float dist  = length(diff);
                fixed4 col_now= tex2D(_MainTex, i.uv);
                float4 color;
                if(dist<0.3){
                    //float nd = saturate(dist / _CircleRadius);

                // Коэффициент от 3 (в центре) до 1 (на границе круга)
                float j=1;
                    float k = (j-(dist/0.3)* j)+1;
                //float k=2;

                // Можно использовать k как множитель яркости среднего цвета
                    color = (col_now-_AvgColor) * k+_AvgColor;
                    //color = fixed4(0,0,0,1);
                }
                else{
                    color = col_now;

                }

                return color;
            }
            ENDHLSL
        }
    }
}
