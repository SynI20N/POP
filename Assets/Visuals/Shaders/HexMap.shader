Shader "Custom/HexMap"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalMap("Normal (RGB)", 2D) = "blue" {}
        _NormalStrength("Normal", Range(-10,10)) = 0.5
        _Metallic("Metallic Amount", Range(-10,10)) = 0.5
        _Extrusion("Extrusion Amount", Range(-1,1)) = 0.5
    }
    SubShader
    {
        Cull off ZWrite Off
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard alpha:fade vertex:vert

        #pragma target 5.0

        sampler2D _MainTex;
        sampler2D _NormalMap;

        struct Input
        {
            float2 uv_NormalMap;
            float2 uv_MainTex;
        };

        float _Extrusion;

        void vert(inout appdata_full v) 
        {
            v.vertex.y += _Extrusion;
        }

        fixed4 _Color;
        float _Metallic;
        float _NormalStrength;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            o.Metallic = _Metallic;
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap)) * _NormalStrength;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
