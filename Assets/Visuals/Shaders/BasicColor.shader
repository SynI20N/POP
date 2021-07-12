 Shader "Custom/BasicColor"
 {
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (0,0,0,0)
    }
    SubShader 
    {
        Cull back
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM

        #pragma surface surf Lambert 

        #pragma target 2.5

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
        }
        ENDCG
    }
    Fallback off
 }