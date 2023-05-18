Shader "Unlit/myOrderPanel"
//圆角处理
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _R("R", range(0, 0.5)) = 0.1
        _Color("Color",Color) = (1,1,1,1)
    }
        SubShader
        {
         

            Tags { "Queue" = "Transparent" }
            LOD 100

            Pass
            {
                blend SrcAlpha OneMinusSrcAlpha
                ZWrite off
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed _R;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    half2 uv = i.uv.xy - half2(0.5h, 0.5h);//将UV中心移动到图片中心
                    half threshold = 0.5h - _R;//计算出阈值 uv的xy在 +-threshold范围内为显示，剩下四个角需要根据圆来判断

                    //四个角的范围内
                    half l = length(abs(uv) - half2(threshold, threshold));
                    half stepL = step(_R, l);

                    //除了角的其他部分
                    half stepX = step(threshold, abs(uv.x));
                    half stepY = step(threshold, abs(uv.y));

                    //三个step只要有一个为0，则alpha就为1，所以需要裁减的部分三个step需要都为1
                    float alpha = 1 - stepX * stepY * stepL;

                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.a = alpha;
                    return col;
                }
                ENDCG
            }
        }
}


