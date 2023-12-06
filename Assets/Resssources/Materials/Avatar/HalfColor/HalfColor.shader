Shader "Custom/RedBlueMaterial" {
    Properties {
        _Color ("Red Color", Color) = (1,0,0,1)
        _SecondaryColor("Blue Color", Color) = (0,0,1,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            fixed4 _SecondaryColor;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Check if we're on the left or right half of the texture
                if (i.uv.x < 0.5) {
                    return _Color; // Left half is red
                } else {
                    return _SecondaryColor; // Right half is blue
                }
            }
            ENDCG
        }
    }
}
