Shader "Effects/ReGBCamera/PaletteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PaletteTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _PaletteTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                //Split colors into bands
                //get Luminosity
                float a = max(col.r,max(col.g,col.b));
                float b = min(col.r,min(col.g,col.b));
                float L = 0.5f*(a + b);
                float c = a-b;
                float s = 0;
                float r = 0;
                float g= 0;
                float b2 = 0;
                float h= 0;
                

                if(c==0)
                {
                    s = 0;
                } else if (L<0.5f)
                {
                    s=c/(a+b);
                }
                else
                {
                    s=c/(2-a-b);
                }

                r=(a-col.r)/c;
                g=(a-col.g)/c;
                b2=(a-col.b)/c;
                if(col.r==a)
                {
                    h = b2-g;
                }
                else if (col.g == a)
                {
                    h=2+r-b2;
                }
                else
                {
                    h=4+g-r;
                }

                h=60*h;
                if(h<0)
                {
                    h=h+360;
                }

                h = h *(1/ 360.0);
                
                float2 paletteUV;
                
                if (s < .2f)
                    paletteUV = float2(L,0.1f);

                else if (h <= 0.125f || h > 0.875f)
                    paletteUV = float2(L,0.3f);

                else if (h > 0.125f && h <= 0.375f)
                    paletteUV = float2(L,0.5f);

                else if (h > 0.375f && h <= 0.625f)
                    paletteUV = float2(L,0.7f);

                else if (h > 0.625f && h <=0.875f)
                    paletteUV = float2(L,0.9f);

                else paletteUV = float2(L,0.25f);
                
                col = tex2D(_PaletteTex, paletteUV);
                return col;
            }
            ENDCG
        }
    }
}
