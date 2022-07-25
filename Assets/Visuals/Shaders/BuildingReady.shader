Shader "Custom/BuildingReady"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _CrossColor("Cross Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _PercentFinished("Height Finished", Range(-10,10)) = 0.0
        _StencilMask("Stencil Mask", Range(0, 255)) = 255
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Stencil
        {
            Ref[_StencilMask]
            CompBack Always
            PassBack Replace

            CompFront Always
            PassFront Zero
        }
        Cull Back
        LOD 200 
        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows

        #pragma target 2.5

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        half _PercentFinished;
        fixed4 _Color;
        fixed4 _ConstructionColor;

        fixed4 gradient(float yPos)
        {
            if (yPos > _PercentFinished || yPos < 0)
            {
                return (1, 1, 1, 1);
            }
            fixed4 _GradientColor;
            _GradientColor.r = 1;
            _GradientColor.g = (_PercentFinished - yPos) / _PercentFinished;
            _GradientColor.b = (_PercentFinished - yPos) / _PercentFinished;
            _GradientColor.a = 1;
            return _GradientColor;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * gradient(IN.worldPos.y) * 2 * _Color;
            if (IN.worldPos.y > _PercentFinished)
            {
                discard;
            }
            else
            {
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
            }
            o.Albedo = c.rgb;
        }

        ENDCG

            Cull Front
            CGPROGRAM
#pragma surface surf NoLighting  noambient

            struct Input {
            half2 uv_MainTex;
            float3 worldPos;
        };
        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _CrossColor;
        half _PercentFinished;
        fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            fixed4 c;
            c.rgb = s.Albedo;
            c.a = s.Alpha;
            return c;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            if (IN.worldPos.y > _PercentFinished)
            {
                discard;
            }
            o.Albedo = _CrossColor;

        }
        ENDCG
    }
    FallBack "Diffuse"
}
