Shader "Unlit/myOrderPanel"
//Բ�Ǵ���
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
                    half2 uv = i.uv.xy - half2(0.5h, 0.5h);//��UV�����ƶ���ͼƬ����
                    half threshold = 0.5h - _R;//�������ֵ uv��xy�� +-threshold��Χ��Ϊ��ʾ��ʣ���ĸ�����Ҫ����Բ���ж�

                    //�ĸ��ǵķ�Χ��
                    half l = length(abs(uv) - half2(threshold, threshold));
                    half stepL = step(_R, l);

                    //���˽ǵ���������
                    half stepX = step(threshold, abs(uv.x));
                    half stepY = step(threshold, abs(uv.y));

                    //����stepֻҪ��һ��Ϊ0����alpha��Ϊ1��������Ҫ�ü��Ĳ�������step��Ҫ��Ϊ1
                    float alpha = 1 - stepX * stepY * stepL;

                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.a = alpha;
                    return col;
                }
                ENDCG
            }
        }
}


