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
                float maximum = max(col.r,max(col.g,col.b));
                float minumum = min(col.r,min(col.g,col.b));
                float lum = (maximum + minumum) / 2;

                float hue = 0;

                if(maximum == col.r) {
                    hue = 0+(col.g-col.b)/lum;
                } else if(maximum == col.g) {
                    hue = 2+(col.b-col.r)/lum;
                } else if(maximum == col.b) {
                    hue = 4+(col.r-col.g)/lum;
                }
                
                hue = frac(hue / 6);

                float saturation = lum / maximum;
                float value = maximum;
                float2 paletteUV;
                
                if (saturation > .8f)
                    paletteUV = float2(value,0.1f);

                else if (hue <= 0.25f)
                    paletteUV = float2(value,0.3f);

                else if (hue > 0.25f && hue <= 0.45f)
                    paletteUV = float2(value,0.5f);

                else if (hue > 0.45f && hue <= 0.68f)
                    paletteUV = float2(value,0.7f);

                else if (hue > 0.68f)
                    paletteUV = float2(value,0.9f);

                else paletteUV = float2(value,0.25f);
                
                col = tex2D(_PaletteTex, paletteUV);
                return col;
            }
            ENDCG
        }
    }
}
