Shader "Custom/HexMap"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
        _NormalMap("Normal", 2D) = "blue" {}
        _HeightMap("Height", 2D) = "white" {}
        _EmissionMap("Emission", 2D) = "white" {}
        _Pos("EmissionPos", Vector) = (0,0,0,0)
        _HeightPower("Height Power", Range(0,.25)) = 0
        _Extrusion("Extrusion Amount", Range(0,2)) = 0.5
    }
    SubShader
    {
        Cull back
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Lambert alpha:fade vertex:vert interpolateview

        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _HeightMap;
        sampler2D _EmissionMap;
        float4 _Pos;
        half _HeightPower;

        struct Input
        {
            float3 viewDir;
            float3 worldPos;
            float2 uv_HeightMap;
            float2 uv_NormalMap;
            float2 uv_EmissionMap;
            float2 uv_MainTex;
        };

        half _Extrusion;

        void vert(inout appdata_full v) 
        {
            v.vertex.y += _Extrusion;
        }

        fixed4 _Color;
        float2 texOffset;
        float mask;

        void surf (Input IN, inout SurfaceOutput o)
        {
            if (abs(IN.worldPos.x - _Pos.x) > 1.5f || abs(IN.worldPos.z - _Pos.y) > 2)
            {
                mask = 1;
            }
            else
            {
                mask = 10;
            }

            texOffset.y = -ParallaxOffset(tex2D(_HeightMap, IN.uv_HeightMap).r, _HeightPower, IN.viewDir).y / 8;
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex + texOffset) * _Color;
            fixed4 e = tex2D(_EmissionMap, IN.uv_EmissionMap);

            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Emission = pow(e.rgb , mask);

            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap + texOffset));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
