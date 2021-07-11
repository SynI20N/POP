Shader "Custom/HexMap"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
        _NormalMap("Normal", 2D) = "blue" {}
        _EmissionMap("Emission", 2D) = "white" {}
        _Pos("EmissionPos", Vector) = (0,0,0,0)
        _HeightPower("Height Power", Range(0,.125)) = 0
        _Extrusion("Extrusion Amount", Range(0,2)) = 0.5
        _r("Inner radius of Hex", Range(0,2)) = 0.5
        _R("Outer radius of Hex", Range(0,3)) = 0.5
        _xOffset("XOffset", Range(-3,3)) = 0.5
        _yOffset("YOffset", Range(-3,3)) = 0.5
    }
    SubShader
    {
        Cull back
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Lambert alpha:fade vertex:vert interpolateview

        #pragma target 2.5

        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _EmissionMap;
        float4 _Pos;
        half _HeightPower;
        half _r;
        half _R;
        half _xOffset;
        half _yOffset;

        struct Input
        {
            float3 viewDir;
            float3 worldPos;
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

        half lineFunc(Input IN, float2 Pos)
        {
            return Pos.y + _R - (_R / (2 * _r)) * abs(Pos.x - IN.worldPos.x);
        }

        bool isInsideHex(Input IN, float2 Pos)
        {
            if (abs(IN.worldPos.x - (Pos.x - _xOffset)) < _r 
             && (IN.worldPos.z + _yOffset) < lineFunc(IN, Pos)
             && (IN.worldPos.z + _yOffset) > lineFunc(IN, Pos) - 2 * _R + abs(Pos.x - IN.worldPos.x) * 1.2
                )
            {
                return true;
            }
            return false;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            mask = 0;
            if (isInsideHex(IN, _Pos))
            {
                mask = 2;
            }
            texOffset.xy = -ParallaxOffset(tex2D(_MainTex, IN.uv_MainTex).r, _HeightPower, IN.viewDir).xy;
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
