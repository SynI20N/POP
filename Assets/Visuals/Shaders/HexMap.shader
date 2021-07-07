Shader "Custom/HexMap"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalMap("Normal (RGB)", 2D) = "blue" {}
        _HeightMap("Height Map", 2D) = "white" {}
        _HeightPower("Height Power", Range(0,.25)) = 0
        _Extrusion("Extrusion Amount", Range(0,2)) = 0.5
    }
    SubShader
    {
        Cull back
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Lambert alpha:fade vertex:vert

        #pragma target 5.0

        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _HeightMap;
        float _HeightPower;

        struct Input
        {
            float3 viewDir;
            float2 uv_HeightMap;
            float2 uv_NormalMap;
            float2 uv_MainTex;
        };

        float _Extrusion;

        void vert(inout appdata_full v) 
        {
            v.vertex.y += _Extrusion;
        }

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 texOffset = 0;
            texOffset.y = -ParallaxOffset(tex2D(_HeightMap, IN.uv_HeightMap).r, _HeightPower, IN.viewDir).y / 8;
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + texOffset) * _Color;

            o.Albedo = c.rgb;
            o.Alpha = c.a;

            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap + texOffset));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
