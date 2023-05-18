Shader "Unlit/orderAppear"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Value("Value", Range(0, 1)) = 0
    }
        SubShader
        {
            // No culling or depth
            Cull Off ZWrite Off ZTest Always

            Pass
            {
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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                sampler2D _MainTex;

                float _Value;

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                //ª“∂»÷µ
                float value = (0.299 * col.r + 0.587 * col.g + 0.114 * col.b);

                clip(_Value - value);

                return col;
            }
            ENDCG
        }
        }
}




